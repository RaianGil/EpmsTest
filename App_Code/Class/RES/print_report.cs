using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Web.Services;
using System.IO;
/// <summary>
/// Summary description for print_report
/// </summary>
namespace UtilitiesComponents.RES
{
    public class print_report
    {
        public print_report()
        {
            this.effective_dates = "";
            this.expiration_dates = "";
            this.type_policy = "";
            this.policy_number = "";
            this.quote_number = "";
            this.renewal_of = "";
            this.insured_name_mailing_address = "";
            this.agency = "";
            this.name_insured = "";
            this.business_phone = "";
            this.mobile_phone = "";
            this.email = "";
            this.insured_premises = "";
            this.owner = "";
            this.general_lesee = "";
            this.tenant = "";
            this.other = "";
            this.individual = "";
            this.partnership = "";
            this.corporation = "";
            this.join_venture = "";
            this.other_ti = "";
            this.bodily_injury = "";
            this.property_damage = "";
            this.medical_payment = "";
            this.fire_damage = "";
            this.occupation = "";
        }
        #region Variables

        private string _effective_dates = "";
        private string _expiration_dates = "";
        private string _type_policy = "";
        private string _policy_number = "";
        private string _quote_number = "";
        private string _renewal_of = "";
        private string _name_insured = "";
        private string _insured_name_mailing_address = "";
        private string _agency = "";
        //Insured General information
        private string _occupation = "";
        private string _business_phone = "";
        private string _mobile_phone = "";
        private string _email = "";
        private string _insured_premises = "";
        //Insterest of Named Insured in Insured Premises
        private string _owner = "";
        private string _general_lesee = "";
        private string _tenant = "";
        private string _other = "";
        //type insured
        private string _individual = "";
        private string _partnership = "";
        private string _corporation = "";
        private string _join_venture = "";
        private string _other_ti = "";
        //general liability coverages
        private string _bodily_injury = "";   //BILimit
        private string _property_damage = ""; //PDLimit
        private string _medical_payment = "";
        private string _fire_damage = "";
        //
        private string _gross_tax = "";
        private string _total_premium_limit;
        private string _total_premium;


        #endregion
        /*----------------------------------------------------*/
        /*----------------------------------------------------*/
        #region properties

        public string effective_dates
        {
            get
            {
                return this._effective_dates;
            }
            set
            {
                this._effective_dates = value;
            }
        }
        public string expiration_dates
        {
            get
            {
                return this._expiration_dates;
            }
            set
            {
                this._expiration_dates = value;
            }
        }
        public string type_policy
        {
            get
            {
                return this._type_policy;
            }
            set
            {
                this._type_policy = value;
            }
        }
        public string policy_number
        {
            get
            {
                return this._policy_number;
            }
            set
            {
                this._policy_number = value;
            }
        }
        public string quote_number
        {
            get
            {
                return this._quote_number;
            }
            set
            {
                this._quote_number = value;
            }
        }
        public string renewal_of
        {
            get
            {
                return this._renewal_of;
            }
            set
            {
                this._renewal_of = value;
            }
        }
        public string agency
        {
            get
            {
                return this._agency;
            }
            set
            {
                this._agency = value;
            }
        }
        public string name_insured
        {
            get
            {
                return this._name_insured;
            }
            set
            {
                this._name_insured = value;
            }
        }
        public string occupation
        {
            get
            {
                return this._occupation;
            }
            set
            {
                this._occupation = value;
            }
        }
        public string business_phone
        {
            get
            {
                return this._business_phone;
            }
            set
            {
                this._business_phone = value;
            }
        }
        public string mobile_phone
        {
            get
            {
                return this._mobile_phone;
            }
            set
            {
                this._mobile_phone = value;
            }
        }
        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }
        public string insured_premises
        {
            get
            {
                return this._insured_premises;
            }
            set
            {
                this._insured_premises = value;
            }
        }
        public string owner
        {
            get
            {
                return this._owner;
            }
            set
            {
                this._owner = value;
            }
        }
        public string general_lesee
        {
            get
            {
                return this._general_lesee;
            }
            set
            {
                this._general_lesee = value;
            }
        }
        public string tenant
        {
            get
            {
                return this._tenant;
            }
            set
            {
                this._tenant = value;
            }
        }
        public string other
        {
            get
            {
                return this._other;
            }
            set
            {
                this._other = value;
            }
        }
        public string individual
        {
            get
            {
                return this._individual;
            }
            set
            {
                this._individual = value;
            }
        }
        public string partnership
        {
            get
            {
                return this._partnership;
            }
            set
            {
                this._partnership = value;
            }
        }
        public string corporation
        {
            get
            {
                return this._corporation;
            }
            set
            {
                this._corporation = value;
            }
        }
        public string join_venture
        {
            get
            {
                return this._join_venture;
            }
            set
            {
                this._join_venture = value;
            }
        }
        public string other_ti
        {
            get
            {
                return this._other_ti;
            }
            set
            {
                this._other_ti = value;
            }
        }
        public string bodily_injury
        {
            get
            {
                return this._bodily_injury;
            }
            set
            {
                this._bodily_injury = value;
            }
        }
        public string property_damage
        {
            get
            {
                return this._property_damage;
            }
            set
            {
                this._property_damage = value;
            }
        }
        public string medical_payment
        {
            get
            {
                return this._medical_payment;
            }
            set
            {
                this._medical_payment = value;
            }
        }
        public string fire_damage
        {
            get
            {
                return this._fire_damage;
            }
            set
            {
                this._fire_damage = value;
            }
        }
        public string insured_name_mailing_address
        {
            get
            {
                return this._insured_name_mailing_address;
            }
            set
            {
                this._insured_name_mailing_address = value;
            }
        }
        public string gross_tax
        {
            get
            {
                return this._gross_tax;
            }
            set
            {
                this._gross_tax = value;
            }
        }
        public string total_premium_limit
        {
            get
            {
                return this._total_premium_limit;
            }
            set
            {
                this._total_premium_limit = value;
            }
        }
        public string total_premium
        {
            get
            {
                return this._total_premium;
            }
            set
            {
                this._total_premium = value;
            }
        }

        #endregion
        #region method
        public List<string> print_RES_report(string str_report_name, int taskcontrolid, System.Web.HttpServerUtility Server)
        {
            ReportViewer rv_viewer = new ReportViewer();
            List<string> mergePaths = new List<string>();
            Warning[] warnings;
            string[] streamIds;
            string mimeType;
            string encoding = string.Empty;
            string extension;
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
            if (policy_number == "")
                isQuote();
            /*-----------------------------------------------------------------------------*/
            ReportParameter rp_effective_date = new ReportParameter("rpt_EffectiveDate", effective_dates);
            ReportParameter rp_expiration_dates = new ReportParameter("rpt_ExpirationDate", expiration_dates);
            ReportParameter rp_type_policy = new ReportParameter("rpt_TypePolicy", type_policy);
            ReportParameter rp_policy_number = new ReportParameter("rpt_PolicyNumber", policy_number);
            ReportParameter rp_renewal_of = new ReportParameter("rpt_RenewalOf", renewal_of);
            ReportParameter rp_ocupation = new ReportParameter("rpt_Ocupation", occupation);
            ReportParameter rp_business_phone = new ReportParameter("rpt_BusinessPhone", business_phone);
            ReportParameter rp_mobile_phone = new ReportParameter("rpt_MobilePhone", mobile_phone);
            ReportParameter rp_insured_premises = new ReportParameter("rpt_InsuredPremise", insured_premises);
            ReportParameter rp_insured_mail_adress = new ReportParameter("rpt_InsuredMailAddress", insured_name_mailing_address);
            ReportParameter rp_email = new ReportParameter("rpt_Email", email);
            ReportParameter rp_agency = new ReportParameter("rpt_Agency", agency);
            ReportParameter rp_owner = new ReportParameter("rpt_Owner", CheckValue(owner));
            ReportParameter rp_general_lesee = new ReportParameter("rpt_GeneralLesee", CheckValue(general_lesee));
            ReportParameter rp_tenant = new ReportParameter("rpt_Tenant", CheckValue(tenant));
            ReportParameter rp_other = new ReportParameter("rpt_Other", CheckValue(other));
            ReportParameter rp_individual = new ReportParameter("rpt_Individual", CheckValue(individual));
            ReportParameter rp_partnership = new ReportParameter("rpt_Partnership", CheckValue(partnership));
            ReportParameter rp_corporation = new ReportParameter("rpt_Corporation", CheckValue(corporation));
            ReportParameter rp_join_venture = new ReportParameter("rpt_JoinVenture", CheckValue(join_venture));
            ReportParameter rp_other_ti = new ReportParameter("rpt_OtherTI", CheckValue(other_ti));
            ReportParameter rp_bodily_injury = new ReportParameter("rpt_RESLiability", bodily_injury);
            ReportParameter rp_property_damage = new ReportParameter("rpt_RESLiability", property_damage);
            ReportParameter rp_medical_payment = new ReportParameter("rpt_MedicalPayment", medical_payment);
            ReportParameter rp_fire_damage = new ReportParameter("rpt_FireDamage", fire_damage);
            ReportParameter rp_gross_tax = new ReportParameter("rpt_GrossTax", gross_tax);
            ReportParameter rp_total_premium_limit = new ReportParameter("rpt_TotalPremiumLimit", total_premium_limit);
            ReportParameter rp_total_premium = new ReportParameter("rpt_TotalPremium", total_premium);
            //
            rv_viewer.LocalReport.DataSources.Clear();
            rv_viewer.ProcessingMode = ProcessingMode.Local;
            rv_viewer.LocalReport.ReportPath = Server.MapPath("Reports/RES/" + str_report_name + ".rdlc");
            //
            rv_viewer.LocalReport.SetParameters(new ReportParameter[] 
         {
             rp_effective_date, 
             rp_expiration_dates,
             rp_type_policy, 
             rp_policy_number,
             rp_renewal_of,
             rp_ocupation,
             rp_business_phone,
             rp_mobile_phone,
             rp_insured_premises,
             rp_insured_mail_adress,
             rp_agency,
             rp_email,
             rp_owner,
             rp_general_lesee,
             rp_tenant,
             rp_other,
             rp_individual,
             rp_partnership,
             rp_corporation,
             rp_join_venture,
             rp_other_ti,
             rp_bodily_injury,
             rp_property_damage,
             rp_medical_payment,
             rp_fire_damage,
             rp_gross_tax,
             rp_total_premium_limit,
             rp_total_premium
         });
            //
            rv_viewer.LocalReport.Refresh();
            string str_file_name = str_report_name + taskcontrolid.ToString().Trim() + "-1";
            string str_file_name_pdf = str_report_name + taskcontrolid.ToString().Trim() + "-1.pdf";
            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + str_file_name))
                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + str_file_name);
            byte[] bt_pdf_byte = null;
            try
            {
                bt_pdf_byte = rv_viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            catch (Exception e) 
            {

            }
            using (System.IO.FileStream fs_save_report = new System.IO.FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + str_file_name_pdf, FileMode.Create))
            {
                fs_save_report.Write(bt_pdf_byte, 0, bt_pdf_byte.Length);
            }
            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + str_file_name_pdf);
            return mergePaths;
        }
        private string CheckValue(string strtInput) 
        {
            int intInput = int.Parse(strtInput);
            //
            String strReturn = "";
            if (intInput == 1)
                strReturn = "X";

            return strReturn;
        }
        #endregion
        private void isQuote() 
        {
            this.policy_number = this.quote_number;
        }
    }
}