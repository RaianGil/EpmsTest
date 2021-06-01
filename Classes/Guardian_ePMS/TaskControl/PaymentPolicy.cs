using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
	/// <summary>
	/// Summary description for PaymentPolicy.
	/// </summary>
	public class PaymentPolicy
	{
		public PaymentPolicy()
		{
	
		}

		#region Variable

		private int _Mode               = 0;
		private int		  _CurrentIndex = 0;
		private decimal _TotalPaid      = 0.00m;
		private decimal _Balance        = 0.00m;
		private Policy _policy;
		private DataTable _DtPayments;

		#endregion

		#region Properties

		public DataTable PaymentsCollection
		{
			get
			{
				return this._DtPayments;
			}
			set
			{
				this._DtPayments = value;
			}
		}

		public int Mode
		{
			get
			{
				return this._Mode;
			}
			set
			{
				this._Mode = value;
			}
		}

		public int CurrentIndex
		{
			get
			{
				return this._CurrentIndex;
			}
			set
			{
				this._CurrentIndex = value;
			}
		}

		public decimal TotalPaid
		{
			get
			{
				return this._TotalPaid;
			}
			set
			{
				this._TotalPaid = value;
			}
		}

		public decimal Balance
		{
			get
			{
				return this._Balance;
			}
			set
			{
				this._Balance = value;
			}
		}

		#endregion

		#region Public Methods

		public static PaymentPolicy GetPaymentsByTaskControlID(Policy policy)
		{
			PaymentPolicy payments = new PaymentPolicy();
				
			payments = FillProperties(payments,policy);
			return payments;
		}

        public static bool HasCancellationTransaction(int taskControlID)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[2];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                    SqlDbType.Int, 0, taskControlID.ToString(),
                    ref cookItems);

                DbRequestXmlCooker.AttachCookItem("PaymentReference",
                     SqlDbType.VarChar, 30, "Cancelled".ToString(),
                     ref cookItems);

                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
                DataTable dtPayments = Executor.GetQuery("GetCancellationTransaction", xmlDoc);

                if (dtPayments.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception xcp)
            {
                throw new Exception("Could not get the Cancellation Transaction.", xcp);
            }
        }

		public void InsertAutomaticPaymentFromPolicy(Policy policy)
		{
			_policy = (Policy) policy;

			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

			try
			{
				Executor.BeginTrans();
				int _PartialPaymentID = Executor.Insert("AddPartialPayments",this.GetInsertAutomaticPaymentFromPolicyXml());
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Automatic Payment for this Policy. "+xcp.Message,xcp);
			}

			_policy.PaymentsDetail = FillProperties(this, policy);
		}

		public void InsertPartialPayment(TaskControl policy, Payments payments)
		{
			_policy = (Policy) policy;

			VerifyIfApplyPayment(policy, payments);

			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

			try
			{
				Executor.BeginTrans();
				switch (payments.Mode)
				{
					case 1:  //ADD
						int _PartialPaymentID = Executor.Insert("AddPartialPayments",this.GetInsertPaymentXml(policy, payments));
						break;
					default: //UPDATE
						Executor.Update("UpdatePartialPayments",this.GetUpdatePartialPaymentsXml(policy, payments));
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Partial Payment. "+xcp.Message,xcp);
			}

			//Aplicar comision e incentivo.
			ApplySupplierIncentive();
			ApplyAgentCommission(policy,payments);
		}

        public void InsertCancellationPayment(Policy policy)
        {
            _policy = policy;

            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                int _PartialPaymentID = Executor.Insert("AddPartialPayments", this.GetInsertCancellationPaymentXml());
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the Cancellation Payment for this Policy. " + xcp.Message, xcp);
            }

            _policy.PaymentsDetail = FillProperties(this, policy);
        }

		public static void DeletePartialPayment(int partialPaymentID)
		{
			DBRequest Executor = new DBRequest();

			try
			{
				Executor.BeginTrans();
				Executor.Update("DeletePartialPayments",DeletePartialPaymentXml(partialPaymentID));
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error, Please try again. "+xcp.Message,xcp);
			}
		}

		public static void UpdatePolicyPaidAmount(int taskControlID,double paidAmt)
		{
			DBRequest Executor = new DBRequest();

			try
			{
				Executor.BeginTrans();
				Executor.Update("UpdatePolicyPaidAmount",UpdatePolicyPaidAmountXml(taskControlID,paidAmt));
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error, Please try again. "+xcp.Message,xcp);
			}
		}
		#endregion

		#region Private Methods

		private static XmlDocument UpdatePolicyPaidAmountXml(int taskControlID, double paidAmt)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];
	
			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaidAmount",
				SqlDbType.Float, 0, paidAmt.ToString(),
				ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private static XmlDocument DeletePartialPaymentXml(int partialPaymentID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("PartialPaymentID",
				SqlDbType.Int, 0, partialPaymentID.ToString(),
				ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private void ApplyAgentCommission(TaskControl policy, Payments payments)
		{
			Policy pol = (Policy) policy;
			decimal CalcComm;
			
			//Cuando se calcularon las comisiones y aún no se han pagado.
			//y se calcula la comisiones por la cantidad pagada.
			if (((double) pol.PaymentsDetail.TotalPaid >= pol.TotalPremium+pol.Charge) && (pol.CommissionAgent > 0))
			{
				CalcComm = pol.PaymentsDetail.TotalPaid + payments.PaymentAmount;
			}
			else
			{
				CalcComm = decimal.Parse(pol.TotalPremium.ToString());
			}
            
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, _policy.TaskControlID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CALCCOMM",
				SqlDbType.Decimal, 0, CalcComm.ToString(),
				ref cookItems);
		
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
	
			try
			{
				exec.BeginTrans();
				exec.Update("ApplyAgentCommission",xmlDoc);
				exec.CommitTrans();
			}		
			catch (Exception xcp)
			{
				exec.RollBackTrans();
				throw new Exception("Error while trying to apply the agent commission. "+xcp.Message,xcp);
			}
		}

		private void ApplySupplierIncentive()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, _policy.TaskControlID.ToString(),
				ref cookItems);
		
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
	
			try
			{
				exec.BeginTrans();
				exec.Update("ApplySupplierIncentive",xmlDoc);
				exec.CommitTrans();
			}		
			catch (Exception xcp)
			{
				exec.RollBackTrans();
				throw new Exception("Error while trying to apply the supplier incentive. "+xcp.Message,xcp);
			}

		}

		private void VerifyIfApplyPayment(TaskControl policy, Payments payments)
		{
			// Cuando no aplica pagos.
			string errorMessage = String.Empty;

			if (_policy.CancellationDate != "")
				payments.Comments = "This Policy is Cancelled. Can't apply the payment.";
			else
				if (DateTime.Parse(_policy.ExpirationDate) <= DateTime.Parse(DateTime.Now.ToShortDateString()))
				payments.Comments = "This Policy is Expired. Can't apply the payment.";
			else
				payments.Comments = "Payment Applied"; 
		}

		private XmlDocument GetInsertAutomaticPaymentFromPolicyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[12];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0,_policy.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaymentDate",
				SqlDbType.DateTime, 8,DateTime.Now.ToString(),
				ref cookItems);

			decimal totprem = Math.Abs((decimal)_policy.TotalPremium) * -1;
			DbRequestXmlCooker.AttachCookItem("TransactionAmount",
				SqlDbType.Money, 8,totprem.ToString(),
				ref cookItems);

			string checkNumber = "Inception";
			DbRequestXmlCooker.AttachCookItem("CheckNumber",
				SqlDbType.Char, 10,checkNumber.ToString(),
				ref cookItems);
	
			int creditDebitID = 2;
			DbRequestXmlCooker.AttachCookItem("CreditDebitID",
				SqlDbType.Int, 0, creditDebitID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TransactionDate",
				SqlDbType.DateTime, 8, DateTime.Now.ToString(),
				ref cookItems);
	
			float commAgency = 0.00F;
			DbRequestXmlCooker.AttachCookItem("CommissionAgency",
				SqlDbType.Float, 8, commAgency.ToString(),
				ref cookItems);
	
			float commPrem = 0.00F;
			DbRequestXmlCooker.AttachCookItem("CommissionPrem",
				SqlDbType.Float, 8, commPrem.ToString(),
				ref cookItems);

			bool lic = false;
			DbRequestXmlCooker.AttachCookItem("License",
				SqlDbType.Bit, 1, lic.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("PolicyClass",
				SqlDbType.Int, 0, _policy.PolicyClassID.ToString(),
				ref cookItems);

			string paymentReference = "Inception";
			DbRequestXmlCooker.AttachCookItem("PaymentReference",
				SqlDbType.VarChar, 30, paymentReference.ToString(),
				ref cookItems);
	
			int taskpaymentID = 0;
			DbRequestXmlCooker.AttachCookItem("TaskPaymentID",
				SqlDbType.Int, 0,taskpaymentID.ToString(),
				ref cookItems);
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			return xmlDoc;
		}

        private XmlDocument GetInsertCancellationPaymentXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[12];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, _policy.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate",
                SqlDbType.DateTime, 8, DateTime.Now.ToString(),
                ref cookItems);

            decimal totprem = Math.Abs((decimal)_policy.CancellationAmount);
            DbRequestXmlCooker.AttachCookItem("TransactionAmount",
                SqlDbType.Money, 8, totprem.ToString(),
                ref cookItems);

            string checkNumber = "Cancelled";
            DbRequestXmlCooker.AttachCookItem("CheckNumber",
                SqlDbType.Char, 10, checkNumber.ToString(),
                ref cookItems);

            int creditDebitID = 1;
            DbRequestXmlCooker.AttachCookItem("CreditDebitID",
                SqlDbType.Int, 0, creditDebitID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TransactionDate",
                SqlDbType.DateTime, 8, DateTime.Now.ToString(),
                ref cookItems);

            float commAgency = 0.00F;
            DbRequestXmlCooker.AttachCookItem("CommissionAgency",
                SqlDbType.Float, 8, commAgency.ToString(),
                ref cookItems);

            float commPrem = 0.00F;
            DbRequestXmlCooker.AttachCookItem("CommissionPrem",
                SqlDbType.Float, 8, commPrem.ToString(),
                ref cookItems);

            bool lic = false;
            DbRequestXmlCooker.AttachCookItem("License",
                SqlDbType.Bit, 1, lic.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClass",
                SqlDbType.Int, 0, _policy.PolicyClassID.ToString(),
                ref cookItems);

            string paymentReference = "Cancelled";
            DbRequestXmlCooker.AttachCookItem("PaymentReference",
                SqlDbType.VarChar, 30, paymentReference.ToString(),
                ref cookItems);

            int taskpaymentID = 0;
            DbRequestXmlCooker.AttachCookItem("TaskPaymentID",
                SqlDbType.Int, 0, taskpaymentID.ToString(),
                ref cookItems);
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
            return xmlDoc;
        }

		private XmlDocument GetUpdatePartialPaymentsXml(TaskControl taskControl, Payments payments)
		{
			Policy policy = (Policy) taskControl;

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[5];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0,_policy.TaskControlID.ToString(),
				ref cookItems);

			DateTime date = DateTime.Parse(payments.PaymentDate+" 12:01:00 AM");

			DbRequestXmlCooker.AttachCookItem("PaymentDate",
				SqlDbType.DateTime, 8,date.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CheckNumber",
				SqlDbType.Char, 10,payments.CheckNo.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("License",
				SqlDbType.Bit, 1, payments.Licence.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("PolicyClass",
				SqlDbType.Int, 0, _policy.PolicyClassID.ToString(),
				ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			return xmlDoc;
		}

		private XmlDocument GetInsertPaymentXml(TaskControl taskControl, Payments payments)
		{
			Policy policy = (Policy) taskControl;

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[12];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0,_policy.TaskControlID.ToString(),
				ref cookItems);

			DateTime date = DateTime.Parse(payments.PaymentDate+" 12:01:00 AM");

			DbRequestXmlCooker.AttachCookItem("PaymentDate",
				SqlDbType.DateTime, 8,date.ToString(),
				ref cookItems);

			decimal PayAmt = 0;
			if (payments.CreditDebitID == 2) // Debit
			{
				PayAmt = Math.Abs((decimal)payments.PaymentAmount) * -1;
			}
			else
			{
				PayAmt = Math.Abs((decimal)payments.PaymentAmount);
			}

			DbRequestXmlCooker.AttachCookItem("TransactionAmount",
				SqlDbType.Money, 8, PayAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CheckNumber",
				SqlDbType.Char, 10,payments.CheckNo.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("CreditDebitID",
				SqlDbType.Int, 0,payments.CreditDebitID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TransactionDate",
				SqlDbType.DateTime, 8, DateTime.Now.ToString(),
				ref cookItems);
	
			float commAgency = 0.00F;
			float commPrem = 0.00F;


			double totPrem = policy.TotalPremium + policy.Charge;
			decimal totPremCharge = decimal.Parse(totPrem.ToString());
			if((payments.PaymentAmount + policy.PaymentsDetail.TotalPaid)>= totPremCharge)
			{
				commAgency = float.Parse(policy.TotalPremium.ToString()) * (float.Parse(policy.CommissionAgencyPercent)/1000);
				policy.CommissionAgency = commAgency;
				policy.CommissionDate   = DateTime.Now.ToShortDateString();
			}
	

			DbRequestXmlCooker.AttachCookItem("CommissionAgency",
				SqlDbType.Float, 8, commAgency.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("CommissionPrem",
				SqlDbType.Float, 8, commPrem.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("License",
				SqlDbType.Bit, 1, payments.Licence.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("PolicyClass",
				SqlDbType.Int, 0, _policy.PolicyClassID.ToString(),
				ref cookItems);

			string paymentReference = "";
            if (payments.Comments1 == "")
            {
                paymentReference = "Inception";
            }
            else
            {
                paymentReference = payments.Comments1;
            }
			DbRequestXmlCooker.AttachCookItem("PaymentReference",
				SqlDbType.VarChar, 30, paymentReference.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("TaskPaymentID",
				SqlDbType.Int, 0,payments.TaskPaymentID.ToString(),
				ref cookItems);
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			return xmlDoc;
		}

		private static DataTable GetPaymentsDetailsDataBase(int taskControlID)
		{
			try
			{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0,taskControlID.ToString(),
				ref cookItems);
	
				XmlDocument xmlDoc;

				try
				{
					xmlDoc = DbRequestXmlCooker.Cook(cookItems);
				}
				catch(Exception ex)
				{
					throw new Exception("Could not cook items.", ex);
				}

				Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
				DataTable dtPayments = Executor.GetQuery("GetPaymentsDetails",xmlDoc);

				return dtPayments;
			}
			catch (Exception xcp)
			{
				throw new Exception("Could not get the payments.", xcp);
			}
		}

		private static PaymentPolicy GetTotalPaidAndBalance(PaymentPolicy payments, Policy policy)
		{
			decimal AlltotalPaid = 0;

			for (int i = 0; i <= payments._DtPayments.Rows.Count-1; i++)
			{
				AlltotalPaid = AlltotalPaid + (decimal) payments._DtPayments.Rows[i]["Transactionamount"];
			}
			
			if (Convert.ToDecimal(policy.TotalPremium) < Convert.ToDecimal(policy.TotalPremium) + AlltotalPaid)
			{
				payments.TotalPaid = Convert.ToDecimal(policy.TotalPremium) + AlltotalPaid;
				payments.Balance   = Math.Abs(AlltotalPaid);
			}
			else
			{
				payments.TotalPaid = Convert.ToDecimal(policy.TotalPremium) - Math.Abs(AlltotalPaid);
				payments.Balance   = AlltotalPaid ;
			}
			
			return payments;
		}

		private static PaymentPolicy FillProperties(PaymentPolicy payments, Policy policy)
		{
			payments._DtPayments = GetPaymentsDetailsDataBase(policy.TaskControlID);
			
			GetTotalPaidAndBalance(payments,policy);

			return payments;
		}

		#endregion
	}
}
