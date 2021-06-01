using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Quotes;
using EPolicy.Audit;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
    public class PersonalLiability
    {
        public PersonalLiability()
        {

        }

        #region Variable

        private int _PersonalLiabilityID = 0;
        private int _TaskControlID = 0;
        private AdditionalCoverageLiability _AdditionalCoverageLiability = null;

        private string _Residence1 = "";
        private string _Residence2 = "";
        private string _Residence3 = "";
        private string _Residence4 = "";
        private string _Residence5 = "";
        private string _Residence6 = "";
        private string _Residence7 = "";
        private string _Residence8 = "";
        private string _Residence9 = "";
        private string _Residence10 = "";

        private int _Property1ID = 0;
        private int _Property2ID = 0;
        private int _Property3ID = 0;
        private int _Property4ID = 0;
        private int _Property5ID = 0;
        private int _Property6ID = 0;
        private int _Property7ID = 0;
        private int _Property8ID = 0;
        private int _Property9ID = 0;
        private int _Property10ID = 0;

        private bool _SwimmingPool1 = false;
        private bool _SwimmingPool2 = false;
        private bool _SwimmingPool3 = false;
        private bool _SwimmingPool4 = false;
        private bool _SwimmingPool5 = false;
        private bool _SwimmingPool6 = false;
        private bool _SwimmingPool7 = false;
        private bool _SwimmingPool8 = false;
        private bool _SwimmingPool9 = false;
        private bool _SwimmingPool10 = false;

        private int _Families1 = 0;
        private int _MedicalPayment1 = 0;
        private int _PersonalLiability1 = 0;
        private bool _Rented1 = false;

        private int _Families2 = 0;
        private int _MedicalPayment2 = 0;
        private int _PersonalLiability2 = 0;
        private bool _Rented2 = false;

        private int _Families3 = 0;
        private int _MedicalPayment3 = 0;
        private int _PersonalLiability3 = 0;
        private bool _Rented3 = false;

        private int _Families4 = 0;
        private int _MedicalPayment4 = 0;
        private int _PersonalLiability4 = 0;
        private bool _Rented4 = false;

        private int _Families5 = 0;
        private int _MedicalPayment5 = 0;
        private int _PersonalLiability5 = 0;
        private bool _Rented5 = false;

        private int _Families6 = 0;
        private int _MedicalPayment6 = 0;
        private int _PersonalLiability6 = 0;
        private bool _Rented6 = false;

        private int _Families7 = 0;
        private int _MedicalPayment7 = 0;
        private int _PersonalLiability7 = 0;
        private bool _Rented7 = false;

        private int _Families8 = 0;
        private int _MedicalPayment8 = 0;
        private int _PersonalLiability8 = 0;
        private bool _Rented8 = false;

        private int _Families9 = 0;
        private int _MedicalPayment9 = 0;
        private int _PersonalLiability9 = 0;
        private bool _Rented9 = false;

        private int _Families10 = 0;
        private int _MedicalPayment10 = 0;
        private int _PersonalLiability10 = 0;
        private bool _Rented10 = false;

        private double _BasicRate1 = 0.00;
        private double _IncreaseLimit1 = 0.00;
        private double _BasicPremium1 = 0.00;
        private double _MedicalPrem1 = 0.00;
        private double _DiscountFactor1 = 0.00;
        private double _Premium1 = 0.00;

        private double _BasicRate2 = 0.00;
        private double _IncreaseLimit2 = 0.00;
        private double _BasicPremium2 = 0.00;
        private double _MedicalPrem2 = 0.00;
        private double _DiscountFactor2 = 0.00;
        private double _Premium2 = 0.00;

        private double _BasicRate3 = 0.00;
        private double _IncreaseLimit3 = 0.00;
        private double _BasicPremium3 = 0.00;
        private double _MedicalPrem3 = 0.00;
        private double _DiscountFactor3 = 0.00;
        private double _Premium3 = 0.00;

        private double _BasicRate4 = 0.00;
        private double _IncreaseLimit4 = 0.00;
        private double _BasicPremium4 = 0.00;
        private double _MedicalPrem4 = 0.00;
        private double _DiscountFactor4 = 0.00;
        private double _Premium4 = 0.00;

        private double _BasicRate5 = 0.00;
        private double _IncreaseLimit5 = 0.00;
        private double _BasicPremium5 = 0.00;
        private double _MedicalPrem5 = 0.00;
        private double _DiscountFactor5 = 0.00;
        private double _Premium5 = 0.00;

        private double _BasicRate6 = 0.00;
        private double _IncreaseLimit6 = 0.00;
        private double _BasicPremium6 = 0.00;
        private double _MedicalPrem6 = 0.00;
        private double _DiscountFactor6 = 0.00;
        private double _Premium6 = 0.00;

        private double _BasicRate7 = 0.00;
        private double _IncreaseLimit7 = 0.00;
        private double _BasicPremium7 = 0.00;
        private double _MedicalPrem7 = 0.00;
        private double _DiscountFactor7 = 0.00;
        private double _Premium7 = 0.00;

        private double _BasicRate8 = 0.00;
        private double _IncreaseLimit8 = 0.00;
        private double _BasicPremium8 = 0.00;
        private double _MedicalPrem8 = 0.00;
        private double _DiscountFactor8 = 0.00;
        private double _Premium8 = 0.00;

        private double _BasicRate9 = 0.00;
        private double _IncreaseLimit9 = 0.00;
        private double _BasicPremium9 = 0.00;
        private double _MedicalPrem9 = 0.00;
        private double _DiscountFactor9 = 0.00;
        private double _Premium9 = 0.00;

        private double _BasicRate10 = 0.00;
        private double _IncreaseLimit10 = 0.00;
        private double _BasicPremium10 = 0.00;
        private double _MedicalPrem10 = 0.00;
        private double _DiscountFactor10 = 0.00;
        private double _Premium10 = 0.00;

        private int _Limit = 0;


        private double _TotalLiabilityPremium = 0.00;
        private Double _ExperienceCredit = 0;
        private double _SubTotalPremium = 0.00;
        private double _Charge = 0.00;
        private double _TotalPremium = 0.00;
        private int _mode = (int)PersonalLiabilityMode.CLEAR;

        //ADDED BY POR 5/22/2014
        private double _DiscUnderwritter = 0;
        //END 


        #endregion

        #region Public Enumeration

        public enum PersonalLiabilityMode { ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4 };

        #endregion

        #region Properties

        public int PersonalLiabilityID
        {
            get
            {
                return this._PersonalLiabilityID;
            }
            set
            {
                this._PersonalLiabilityID = value;
            }
        }
        public int TaskControlID
        {
            get
            {
                return this._TaskControlID;
            }
            set
            {
                this._TaskControlID = value;
            }
        }

        public AdditionalCoverageLiability AdditionalCoverageLiability
        {
            get
            {
                if (this._AdditionalCoverageLiability == null)
                    this._AdditionalCoverageLiability = new AdditionalCoverageLiability();
                return (this._AdditionalCoverageLiability);
            }
            set
            {
                this._AdditionalCoverageLiability = value;
            }
        }

        public string Residence1
        {
            get
            {
                return this._Residence1;
            }
            set
            {
                this._Residence1 = value;
            }
        }
        public string Residence2
        {
            get
            {
                return this._Residence2;
            }
            set
            {
                this._Residence2 = value;
            }
        }
        public string Residence3
        {
            get
            {
                return this._Residence3;
            }
            set
            {
                this._Residence3 = value;
            }
        }
        public string Residence4
        {
            get
            {
                return this._Residence4;
            }
            set
            {
                this._Residence4 = value;
            }
        }

        public string Residence5
        {
            get
            {
                return this._Residence5;
            }
            set
            {
                this._Residence5 = value;
            }
        }

        public string Residence6
        {
            get
            {
                return this._Residence6;
            }
            set
            {
                this._Residence6 = value;
            }
        }

        public string Residence7
        {
            get
            {
                return this._Residence7;
            }
            set
            {
                this._Residence7 = value;
            }
        }

        public string Residence8
        {
            get
            {
                return this._Residence8;
            }
            set
            {
                this._Residence8 = value;
            }
        }

        public string Residence9
        {
            get
            {
                return this._Residence9;
            }
            set
            {
                this._Residence9 = value;
            }
        }

        public string Residence10
        {
            get
            {
                return this._Residence10;
            }
            set
            {
                this._Residence10 = value;
            }
        }

        public int Property1ID
        {
            get
            {
                return this._Property1ID;
            }
            set
            {
                this._Property1ID = value;
            }
        }

        public int Property2ID
        {
            get
            {
                return this._Property2ID;
            }
            set
            {
                this._Property2ID = value;
            }
        }

        public int Property3ID
        {
            get
            {
                return this._Property3ID;
            }
            set
            {
                this._Property3ID = value;
            }
        }

        public int Property4ID
        {
            get
            {
                return this._Property4ID;
            }
            set
            {
                this._Property4ID = value;
            }
        }

        public int Property5ID
        {
            get
            {
                return this._Property5ID;
            }
            set
            {
                this._Property5ID = value;
            }
        }

        public int Property6ID
        {
            get
            {
                return this._Property6ID;
            }
            set
            {
                this._Property6ID = value;
            }
        }

        public int Property7ID
        {
            get
            {
                return this._Property7ID;
            }
            set
            {
                this._Property7ID = value;
            }
        }

        public int Property8ID
        {
            get
            {
                return this._Property8ID;
            }
            set
            {
                this._Property8ID = value;
            }
        }

        public int Property9ID
        {
            get
            {
                return this._Property9ID;
            }
            set
            {
                this._Property9ID = value;
            }
        }

        public int Property10ID
        {
            get
            {
                return this._Property10ID;
            }
            set
            {
                this._Property10ID = value;
            }
        }

        public bool SwimmingPool1
        {
            get
            {
                return this._SwimmingPool1;
            }
            set
            {
                this._SwimmingPool1 = value;
            }
        }
        public bool SwimmingPool2
        {
            get
            {
                return this._SwimmingPool2;
            }
            set
            {
                this._SwimmingPool2 = value;
            }
        }
        public bool SwimmingPool3
        {
            get
            {
                return this._SwimmingPool3;
            }
            set
            {
                this._SwimmingPool3 = value;
            }
        }
        public bool SwimmingPool4
        {
            get
            {
                return this._SwimmingPool4;
            }
            set
            {
                this._SwimmingPool4 = value;
            }
        }

        public bool SwimmingPool5
        {
            get
            {
                return this._SwimmingPool5;
            }
            set
            {
                this._SwimmingPool5 = value;
            }
        }

        public bool SwimmingPool6
        {
            get
            {
                return this._SwimmingPool6;
            }
            set
            {
                this._SwimmingPool6 = value;
            }
        }

        public bool SwimmingPool7
        {
            get
            {
                return this._SwimmingPool7;
            }
            set
            {
                this._SwimmingPool7 = value;
            }
        }

        public bool SwimmingPool8
        {
            get
            {
                return this._SwimmingPool8;
            }
            set
            {
                this._SwimmingPool8 = value;
            }
        }

        public bool SwimmingPool9
        {
            get
            {
                return this._SwimmingPool9;
            }
            set
            {
                this._SwimmingPool9 = value;
            }
        }

        public bool SwimmingPool10
        {
            get
            {
                return this._SwimmingPool10;
            }
            set
            {
                this._SwimmingPool10 = value;
            }
        }

        public int Families1
        {
            get
            {
                return this._Families1;
            }
            set
            {
                this._Families1 = value;
            }
        }
        public int MedicalPayment1
        {
            get
            {
                return this._MedicalPayment1;
            }
            set
            {
                this._MedicalPayment1 = value;
            }
        }
        public int PersonalLiability1
        {
            get
            {
                return this._PersonalLiability1;
            }
            set
            {
                this._PersonalLiability1 = value;
            }
        }
        public bool Rented1
        {
            get
            {
                return this._Rented1;
            }
            set
            {
                this._Rented1 = value;
            }
        }

        public int Families2
        {
            get
            {
                return this._Families2;
            }
            set
            {
                this._Families2 = value;
            }
        }
        public int MedicalPayment2
        {
            get
            {
                return this._MedicalPayment2;
            }
            set
            {
                this._MedicalPayment2 = value;
            }
        }
        public int PersonalLiability2
        {
            get
            {
                return this._PersonalLiability2;
            }
            set
            {
                this._PersonalLiability2 = value;
            }
        }
        public bool Rented2
        {
            get
            {
                return this._Rented2;
            }
            set
            {
                this._Rented2 = value;
            }
        }

        public int Families3
        {
            get
            {
                return this._Families3;
            }
            set
            {
                this._Families3 = value;
            }
        }
        public int MedicalPayment3
        {
            get
            {
                return this._MedicalPayment3;
            }
            set
            {
                this._MedicalPayment3 = value;
            }
        }
        public int PersonalLiability3
        {
            get
            {
                return this._PersonalLiability3;
            }
            set
            {
                this._PersonalLiability3 = value;
            }
        }
        public bool Rented3
        {
            get
            {
                return this._Rented3;
            }
            set
            {
                this._Rented3 = value;
            }
        }

        public int Families4
        {
            get
            {
                return this._Families4;
            }
            set
            {
                this._Families4 = value;
            }
        }
        public int MedicalPayment4
        {
            get
            {
                return this._MedicalPayment4;
            }
            set
            {
                this._MedicalPayment4 = value;
            }
        }
        public int PersonalLiability4
        {
            get
            {
                return this._PersonalLiability4;
            }
            set
            {
                this._PersonalLiability4 = value;
            }
        }
        public bool Rented4
        {
            get
            {
                return this._Rented4;
            }
            set
            {
                this._Rented4 = value;
            }
        }

        public int Families5
        {
            get
            {
                return this._Families5;
            }
            set
            {
                this._Families5 = value;
            }
        }
        public int MedicalPayment5
        {
            get
            {
                return this._MedicalPayment5;
            }
            set
            {
                this._MedicalPayment5 = value;
            }
        }
        public int PersonalLiability5
        {
            get
            {
                return this._PersonalLiability5;
            }
            set
            {
                this._PersonalLiability5 = value;
            }
        }
        public bool Rented5
        {
            get
            {
                return this._Rented5;
            }
            set
            {
                this._Rented5 = value;
            }
        }

        public int Families6
        {
            get
            {
                return this._Families6;
            }
            set
            {
                this._Families6 = value;
            }
        }
        public int MedicalPayment6
        {
            get
            {
                return this._MedicalPayment6;
            }
            set
            {
                this._MedicalPayment6 = value;
            }
        }
        public int PersonalLiability6
        {
            get
            {
                return this._PersonalLiability6;
            }
            set
            {
                this._PersonalLiability6 = value;
            }
        }
        public bool Rented6
        {
            get
            {
                return this._Rented6;
            }
            set
            {
                this._Rented6 = value;
            }
        }

        public int Families7
        {
            get
            {
                return this._Families7;
            }
            set
            {
                this._Families7 = value;
            }
        }
        public int MedicalPayment7
        {
            get
            {
                return this._MedicalPayment7;
            }
            set
            {
                this._MedicalPayment7 = value;
            }
        }
        public int PersonalLiability7
        {
            get
            {
                return this._PersonalLiability7;
            }
            set
            {
                this._PersonalLiability7 = value;
            }
        }
        public bool Rented7
        {
            get
            {
                return this._Rented7;
            }
            set
            {
                this._Rented7 = value;
            }
        }

        public int Families8
        {
            get
            {
                return this._Families8;
            }
            set
            {
                this._Families8 = value;
            }
        }
        public int MedicalPayment8
        {
            get
            {
                return this._MedicalPayment8;
            }
            set
            {
                this._MedicalPayment8 = value;
            }
        }
        public int PersonalLiability8
        {
            get
            {
                return this._PersonalLiability8;
            }
            set
            {
                this._PersonalLiability8 = value;
            }
        }
        public bool Rented8
        {
            get
            {
                return this._Rented8;
            }
            set
            {
                this._Rented8 = value;
            }
        }

        public int Families9
        {
            get
            {
                return this._Families9;
            }
            set
            {
                this._Families9 = value;
            }
        }
        public int MedicalPayment9
        {
            get
            {
                return this._MedicalPayment9;
            }
            set
            {
                this._MedicalPayment9 = value;
            }
        }
        public int PersonalLiability9
        {
            get
            {
                return this._PersonalLiability9;
            }
            set
            {
                this._PersonalLiability9 = value;
            }
        }
        public bool Rented9
        {
            get
            {
                return this._Rented9;
            }
            set
            {
                this._Rented9 = value;
            }
        }

        public int Families10
        {
            get
            {
                return this._Families10;
            }
            set
            {
                this._Families10 = value;
            }
        }
        public int MedicalPayment10
        {
            get
            {
                return this._MedicalPayment10;
            }
            set
            {
                this._MedicalPayment10 = value;
            }
        }
        public int PersonalLiability10
        {
            get
            {
                return this._PersonalLiability10;
            }
            set
            {
                this._PersonalLiability10 = value;
            }
        }
        public bool Rented10
        {
            get
            {
                return this._Rented10;
            }
            set
            {
                this._Rented10 = value;
            }
        }

        public double TotalLiabilityPremium
        {
            get
            {
                return this._TotalLiabilityPremium;
            }
            set
            {
                this._TotalLiabilityPremium = value;
            }
        }
        public Double ExperienceCredit
        {
            get
            {
                return this._ExperienceCredit;
            }
            set
            {
                this._ExperienceCredit = value;
            }
        }
        public double SubTotalPremium
        {
            get
            {
                return this._SubTotalPremium;
            }
            set
            {
                this._SubTotalPremium = value;
            }
        }
        public double Charge
        {
            get
            {
                return this._Charge;
            }
            set
            {
                this._Charge = value;
            }
        }
        public double TotalPremium
        {
            get
            {
                return this._TotalPremium;
            }
            set
            {
                this._TotalPremium = value;
            }
        }

        public int Mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
            }
        }

        //ADDED BY POR 5/22/2014 
        public double DiscUnderwritter
        {
            get
            {
                return this._DiscUnderwritter;
            }
            set
            {
                this._DiscUnderwritter = value;
            }
        }
        //END

        private double BasicRate1
        {
            get
            {
                return this._BasicRate1;
            }
            set
            {
                this._BasicRate1 = value;
            }
        }
        private double IncreaseLimit1
        {
            get
            {
                return this._IncreaseLimit1;
            }
            set
            {
                this._IncreaseLimit1 = value;
            }
        }
        private double BasicPremium1
        {
            get
            {
                return this._BasicPremium1;
            }
            set
            {
                this._BasicPremium1 = value;
            }
        }
        private double MedicalPrem1
        {
            get
            {
                return this._MedicalPrem1;
            }
            set
            {
                this._MedicalPrem1 = value;
            }
        }
        private double DiscountFactor1
        {
            get
            {
                return this._DiscountFactor1;
            }
            set
            {
                this._DiscountFactor1 = value;
            }
        }
        private double Premium1
        {
            get
            {
                return this._Premium1;
            }
            set
            {
                this._Premium1 = value;
            }
        }

        private double BasicRate2
        {
            get
            {
                return this._BasicRate2;
            }
            set
            {
                this._BasicRate2 = value;
            }
        }
        private double IncreaseLimit2
        {
            get
            {
                return this._IncreaseLimit2;
            }
            set
            {
                this._IncreaseLimit2 = value;
            }
        }
        private double BasicPremium2
        {
            get
            {
                return this._BasicPremium2;
            }
            set
            {
                this._BasicPremium2 = value;
            }
        }
        private double MedicalPrem2
        {
            get
            {
                return this._MedicalPrem2;
            }
            set
            {
                this._MedicalPrem2 = value;
            }
        }
        private double DiscountFactor2
        {
            get
            {
                return this._DiscountFactor2;
            }
            set
            {
                this._DiscountFactor2 = value;
            }
        }
        private double Premium2
        {
            get
            {
                return this._Premium2;
            }
            set
            {
                this._Premium2 = value;
            }
        }

        private double BasicRate3
        {
            get
            {
                return this._BasicRate3;
            }
            set
            {
                this._BasicRate3 = value;
            }
        }
        private double IncreaseLimit3
        {
            get
            {
                return this._IncreaseLimit3;
            }
            set
            {
                this._IncreaseLimit3 = value;
            }
        }
        private double BasicPremium3
        {
            get
            {
                return this._BasicPremium3;
            }
            set
            {
                this._BasicPremium3 = value;
            }
        }
        private double MedicalPrem3
        {
            get
            {
                return this._MedicalPrem3;
            }
            set
            {
                this._MedicalPrem3 = value;
            }
        }
        private double DiscountFactor3
        {
            get
            {
                return this._DiscountFactor3;
            }
            set
            {
                this._DiscountFactor3 = value;
            }
        }
        private double Premium3
        {
            get
            {
                return this._Premium3;
            }
            set
            {
                this._Premium3 = value;
            }
        }

        private double BasicRate4
        {
            get
            {
                return this._BasicRate4;
            }
            set
            {
                this._BasicRate4 = value;
            }
        }
        private double IncreaseLimit4
        {
            get
            {
                return this._IncreaseLimit4;
            }
            set
            {
                this._IncreaseLimit4 = value;
            }
        }
        private double BasicPremium4
        {
            get
            {
                return this._BasicPremium4;
            }
            set
            {
                this._BasicPremium4 = value;
            }
        }
        private double MedicalPrem4
        {
            get
            {
                return this._MedicalPrem4;
            }
            set
            {
                this._MedicalPrem4 = value;
            }
        }
        private double DiscountFactor4
        {
            get
            {
                return this._DiscountFactor4;
            }
            set
            {
                this._DiscountFactor4 = value;
            }
        }
        private double Premium4
        {
            get
            {
                return this._Premium4;
            }
            set
            {
                this._Premium4 = value;
            }
        }

        private double BasicRate5
        {
            get
            {
                return this._BasicRate5;
            }
            set
            {
                this._BasicRate5 = value;
            }
        }
        private double IncreaseLimit5
        {
            get
            {
                return this._IncreaseLimit5;
            }
            set
            {
                this._IncreaseLimit5 = value;
            }
        }
        private double BasicPremium5
        {
            get
            {
                return this._BasicPremium5;
            }
            set
            {
                this._BasicPremium5 = value;
            }
        }
        private double MedicalPrem5
        {
            get
            {
                return this._MedicalPrem5;
            }
            set
            {
                this._MedicalPrem5 = value;
            }
        }
        private double DiscountFactor5
        {
            get
            {
                return this._DiscountFactor5;
            }
            set
            {
                this._DiscountFactor5 = value;
            }
        }
        private double Premium5
        {
            get
            {
                return this._Premium5;
            }
            set
            {
                this._Premium5 = value;
            }
        }

        private double BasicRate6
        {
            get
            {
                return this._BasicRate6;
            }
            set
            {
                this._BasicRate6 = value;
            }
        }
        private double IncreaseLimit6
        {
            get
            {
                return this._IncreaseLimit6;
            }
            set
            {
                this._IncreaseLimit6 = value;
            }
        }
        private double BasicPremium6
        {
            get
            {
                return this._BasicPremium6;
            }
            set
            {
                this._BasicPremium6 = value;
            }
        }
        private double MedicalPrem6
        {
            get
            {
                return this._MedicalPrem6;
            }
            set
            {
                this._MedicalPrem6 = value;
            }
        }
        private double DiscountFactor6
        {
            get
            {
                return this._DiscountFactor6;
            }
            set
            {
                this._DiscountFactor6 = value;
            }
        }
        private double Premium6
        {
            get
            {
                return this._Premium6;
            }
            set
            {
                this._Premium6 = value;
            }
        }

        private double BasicRate7
        {
            get
            {
                return this._BasicRate7;
            }
            set
            {
                this._BasicRate7 = value;
            }
        }
        private double IncreaseLimit7
        {
            get
            {
                return this._IncreaseLimit7;
            }
            set
            {
                this._IncreaseLimit7 = value;
            }
        }
        private double BasicPremium7
        {
            get
            {
                return this._BasicPremium7;
            }
            set
            {
                this._BasicPremium7 = value;
            }
        }
        private double MedicalPrem7
        {
            get
            {
                return this._MedicalPrem7;
            }
            set
            {
                this._MedicalPrem7 = value;
            }
        }
        private double DiscountFactor7
        {
            get
            {
                return this._DiscountFactor7;
            }
            set
            {
                this._DiscountFactor7 = value;
            }
        }
        private double Premium7
        {
            get
            {
                return this._Premium7;
            }
            set
            {
                this._Premium7 = value;
            }
        }

        private double BasicRate8
        {
            get
            {
                return this._BasicRate8;
            }
            set
            {
                this._BasicRate8 = value;
            }
        }
        private double IncreaseLimit8
        {
            get
            {
                return this._IncreaseLimit8;
            }
            set
            {
                this._IncreaseLimit8 = value;
            }
        }
        private double BasicPremium8
        {
            get
            {
                return this._BasicPremium8;
            }
            set
            {
                this._BasicPremium8 = value;
            }
        }
        private double MedicalPrem8
        {
            get
            {
                return this._MedicalPrem8;
            }
            set
            {
                this._MedicalPrem8 = value;
            }
        }
        private double DiscountFactor8
        {
            get
            {
                return this._DiscountFactor8;
            }
            set
            {
                this._DiscountFactor8 = value;
            }
        }
        private double Premium8
        {
            get
            {
                return this._Premium8;
            }
            set
            {
                this._Premium8 = value;
            }
        }

        private double BasicRate9
        {
            get
            {
                return this._BasicRate9;
            }
            set
            {
                this._BasicRate9 = value;
            }
        }
        private double IncreaseLimit9
        {
            get
            {
                return this._IncreaseLimit9;
            }
            set
            {
                this._IncreaseLimit9 = value;
            }
        }
        private double BasicPremium9
        {
            get
            {
                return this._BasicPremium9;
            }
            set
            {
                this._BasicPremium9 = value;
            }
        }
        private double MedicalPrem9
        {
            get
            {
                return this._MedicalPrem9;
            }
            set
            {
                this._MedicalPrem9 = value;
            }
        }
        private double DiscountFactor9
        {
            get
            {
                return this._DiscountFactor9;
            }
            set
            {
                this._DiscountFactor9 = value;
            }
        }
        private double Premium9
        {
            get
            {
                return this._Premium9;
            }
            set
            {
                this._Premium9 = value;
            }
        }

        private double BasicRate10
        {
            get
            {
                return this._BasicRate10;
            }
            set
            {
                this._BasicRate10 = value;
            }
        }
        private double IncreaseLimit10
        {
            get
            {
                return this._IncreaseLimit10;
            }
            set
            {
                this._IncreaseLimit10 = value;
            }
        }
        private double BasicPremium10
        {
            get
            {
                return this._BasicPremium10;
            }
            set
            {
                this._BasicPremium10 = value;
            }
        }
        private double MedicalPrem10
        {
            get
            {
                return this._MedicalPrem10;
            }
            set
            {
                this._MedicalPrem10 = value;
            }
        }
        private double DiscountFactor10
        {
            get
            {
                return this._DiscountFactor10;
            }
            set
            {
                this._DiscountFactor10 = value;
            }
        }
        private double Premium10
        {
            get
            {
                return this._Premium10;
            }
            set
            {
                this._Premium10 = value;
            }
        }

        private int Limit
        {
            get
            {
                return this._Limit;
            }
            set
            {
                this._Limit = value;
            }
        }

        #endregion

        #region Public Methods

        public void SavePersonalLiability(int UserID, DataTable PersonaLiabilityTableTemp, int taskControlID, bool IsOPPQuote)
        {
            DBRequest Executor = new DBRequest();
            Executor.Update("DeletePersonalLiabilityByTaskControlID", DeletePersonalLiabilityByTaskControlIDXml(taskControlID, IsOPPQuote));

            //DataTable dt = PersonaLiabilityTableTemp;
            for (int i = 0; i < PersonaLiabilityTableTemp.Rows.Count; i++)
            {
                this.TaskControlID = taskControlID;
                this.Residence1 = PersonaLiabilityTableTemp.Rows[i]["Residence1"].ToString();
                this.Residence2 = PersonaLiabilityTableTemp.Rows[i]["Residence2"].ToString();
                this.Residence3 = PersonaLiabilityTableTemp.Rows[i]["Residence3"].ToString();
                this.Residence4 = PersonaLiabilityTableTemp.Rows[i]["Residence4"].ToString();
                this.Residence5 = PersonaLiabilityTableTemp.Rows[i]["Residence5"].ToString();
                this.Residence6 = PersonaLiabilityTableTemp.Rows[i]["Residence6"].ToString();
                this.Residence7 = PersonaLiabilityTableTemp.Rows[i]["Residence7"].ToString();
                this.Residence8 = PersonaLiabilityTableTemp.Rows[i]["Residence8"].ToString();
                this.Residence9 = PersonaLiabilityTableTemp.Rows[i]["Residence9"].ToString();
                this.Residence10 = PersonaLiabilityTableTemp.Rows[i]["Residence10"].ToString();
                this.Property1ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property1ID"].ToString());
                this.Property2ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property2ID"].ToString());
                this.Property3ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property3ID"].ToString());
                this.Property4ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property4ID"].ToString());
                this.Property5ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property5ID"].ToString());
                this.Property6ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property6ID"].ToString());
                this.Property7ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property7ID"].ToString());
                this.Property8ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property8ID"].ToString());
                this.Property9ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property9ID"].ToString());
                this.Property10ID = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Property10ID"].ToString());

                this.SwimmingPool1 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool1"].ToString());
                this.SwimmingPool2 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool2"].ToString());
                this.SwimmingPool3 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool3"].ToString());
                this.SwimmingPool4 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool4"].ToString());
                this.SwimmingPool5 = (PersonaLiabilityTableTemp.Rows[i]["SwimmingPool5"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool5"].ToString()) : false;
                this.SwimmingPool6 = (PersonaLiabilityTableTemp.Rows[i]["SwimmingPool6"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool6"].ToString()) : false;
                this.SwimmingPool7 = (PersonaLiabilityTableTemp.Rows[i]["SwimmingPool7"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool7"].ToString()) : false;
                this.SwimmingPool8 = (PersonaLiabilityTableTemp.Rows[i]["SwimmingPool8"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool8"].ToString()) : false;
                this.SwimmingPool9 = (PersonaLiabilityTableTemp.Rows[i]["SwimmingPool9"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool9"].ToString()) : false;
                this.SwimmingPool10 = (PersonaLiabilityTableTemp.Rows[i]["SwimmingPool10"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["SwimmingPool10"].ToString()) : false;

                this.Families1 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families1"].ToString());
                this.Families2 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families2"].ToString());
                this.Families3 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families3"].ToString());
                this.Families4 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families4"].ToString());
                this.Families5 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families5"].ToString());
                this.Families6 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families6"].ToString());
                this.Families7 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families7"].ToString());
                this.Families8 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families8"].ToString());
                this.Families9 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families9"].ToString());
                this.Families10 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Families10"].ToString());

                this.MedicalPayment1 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment1"].ToString());
                this.MedicalPayment2 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment2"].ToString());
                this.MedicalPayment3 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment3"].ToString());
                this.MedicalPayment4 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment4"].ToString());
                this.MedicalPayment5 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment5"].ToString());
                this.MedicalPayment6 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment6"].ToString());
                this.MedicalPayment7 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment7"].ToString());
                this.MedicalPayment8 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment8"].ToString());
                this.MedicalPayment9 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment9"].ToString());
                this.MedicalPayment10 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPayment10"].ToString());

                this.PersonalLiability1 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability1"].ToString());
                this.PersonalLiability2 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability2"].ToString());
                this.PersonalLiability3 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability3"].ToString());
                this.PersonalLiability4 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability4"].ToString());
                this.PersonalLiability5 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability5"].ToString());
                this.PersonalLiability6 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability6"].ToString());
                this.PersonalLiability7 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability7"].ToString());
                this.PersonalLiability8 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability8"].ToString());
                this.PersonalLiability9 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability9"].ToString());
                this.PersonalLiability10 = int.Parse(PersonaLiabilityTableTemp.Rows[i]["PersonalLiability10"].ToString());

                this.Rented1 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented1"].ToString());
                this.Rented2 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented2"].ToString());
                this.Rented3 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented3"].ToString());
                this.Rented4 = bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented4"].ToString());
                this.Rented5 = (PersonaLiabilityTableTemp.Rows[i]["Rented5"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented5"].ToString()) : false;
                this.Rented6 = (PersonaLiabilityTableTemp.Rows[i]["Rented6"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented6"].ToString()) : false;
                this.Rented7 = (PersonaLiabilityTableTemp.Rows[i]["Rented7"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented7"].ToString()) : false;
                this.Rented8 = (PersonaLiabilityTableTemp.Rows[i]["Rented8"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented8"].ToString()) : false;
                this.Rented9 = (PersonaLiabilityTableTemp.Rows[i]["Rented9"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented9"].ToString()) : false;
                this.Rented10 = (PersonaLiabilityTableTemp.Rows[i]["Rented10"] != System.DBNull.Value) ? bool.Parse(PersonaLiabilityTableTemp.Rows[i]["Rented10"].ToString()) : false;

                this.TotalLiabilityPremium = double.Parse(PersonaLiabilityTableTemp.Rows[i]["TotalLiabilityPremium"].ToString());
                this.ExperienceCredit = Double.Parse(PersonaLiabilityTableTemp.Rows[i]["ExperienceCredit"].ToString());
                this.Charge = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Charge"].ToString());
                this.SubTotalPremium = double.Parse(PersonaLiabilityTableTemp.Rows[i]["SubTotalPremium"].ToString());
                this.TotalPremium = double.Parse(PersonaLiabilityTableTemp.Rows[i]["TotalPremium"].ToString());

                this.BasicRate1 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate1"].ToString());
                this.IncreaseLimit1 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit1"].ToString());
                this.BasicPremium1 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium1"].ToString());
                this.MedicalPrem1 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem1"].ToString());
                this.DiscountFactor1 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor1"].ToString());
                this.Premium1 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium1"].ToString());

                this.BasicRate2 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate2"].ToString());
                this.IncreaseLimit2 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit2"].ToString());
                this.BasicPremium2 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium2"].ToString());
                this.MedicalPrem2 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem2"].ToString());
                this.DiscountFactor2 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor2"].ToString());
                this.Premium2 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium2"].ToString());

                this.BasicRate3 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate3"].ToString());
                this.IncreaseLimit3 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit3"].ToString());
                this.BasicPremium3 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium3"].ToString());
                this.MedicalPrem3 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem3"].ToString());
                this.DiscountFactor3 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor3"].ToString());
                this.Premium3 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium3"].ToString());

                this.BasicRate4 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate4"].ToString());
                this.IncreaseLimit4 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit4"].ToString());
                this.BasicPremium4 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium4"].ToString());
                this.MedicalPrem4 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem4"].ToString());
                this.DiscountFactor4 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor4"].ToString());
                this.Premium4 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium4"].ToString());

                this.BasicRate5 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate5"].ToString());
                this.IncreaseLimit5 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit5"].ToString());
                this.BasicPremium5 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium5"].ToString());
                this.MedicalPrem5 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem5"].ToString());
                this.DiscountFactor5 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor5"].ToString());
                this.Premium5 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium5"].ToString());

                this.BasicRate6 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate6"].ToString());
                this.IncreaseLimit6 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit6"].ToString());
                this.BasicPremium6 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium6"].ToString());
                this.MedicalPrem6 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem6"].ToString());
                this.DiscountFactor6 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor6"].ToString());
                this.Premium6 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium6"].ToString());

                this.BasicRate7= double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate7"].ToString());
                this.IncreaseLimit7 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit7"].ToString());
                this.BasicPremium7 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium7"].ToString());
                this.MedicalPrem7 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem7"].ToString());
                this.DiscountFactor7 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor7"].ToString());
                this.Premium7 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium7"].ToString());

                this.BasicRate8 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate8"].ToString());
                this.IncreaseLimit8 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit8"].ToString());
                this.BasicPremium8 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium8"].ToString());
                this.MedicalPrem8 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem8"].ToString());
                this.DiscountFactor8 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor8"].ToString());
                this.Premium8 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium8"].ToString());

                this.BasicRate9= double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate9"].ToString());
                this.IncreaseLimit9 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit9"].ToString());
                this.BasicPremium9 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium9"].ToString());
                this.MedicalPrem9 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem9"].ToString());
                this.DiscountFactor9 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor9"].ToString());
                this.Premium9= double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium9"].ToString());

                this.BasicRate10= double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicRate10"].ToString());
                this.IncreaseLimit10 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["IncreaseLimit10"].ToString());
                this.BasicPremium10 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["BasicPremium10"].ToString());
                this.MedicalPrem10 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["MedicalPrem10"].ToString());
                this.DiscountFactor10 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscountFactor10"].ToString());
                this.Premium10 = double.Parse(PersonaLiabilityTableTemp.Rows[i]["Premium10"].ToString());

                this.Limit = int.Parse(PersonaLiabilityTableTemp.Rows[i]["Limit"].ToString());

                //ADDED BY POR 5/22/2014 
                this.DiscUnderwritter = double.Parse(PersonaLiabilityTableTemp.Rows[i]["DiscUnderwritter"].ToString());
                //END


                this.Mode = 1; //Add
                this.SavePersonalLiability(UserID, IsOPPQuote);
            }

            this.Mode = (int) PersonalLiabilityMode.CLEAR;
        }

        public static DataTable GetPersonalLiabilityByTaskControlID(int TaskControlID, bool IsOPPQuote)
        {
            DataTable dt = GetPersonalLiabilityByTaskControlIDDB(TaskControlID, IsOPPQuote);
            return dt;
        }

        public static DataTable GetPersonalLiabilityByPersonalLiabilityID(int personalLiabilityID, bool IsOPPQuote)
        {
            DataTable dt = GetPersonalLiabilityByPersonalLiabilityIDDB(personalLiabilityID, IsOPPQuote);
            return dt;
        }

        #endregion

        #region Private Methods

        private XmlDocument DeletePersonalLiabilityByTaskControlIDXml(int taskControlID, bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOppQuote.ToString(),
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

        public static DataTable GetPersonalLiabilityByTaskControlIDDB(int taskControlID, bool IsOPPQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
                 ref cookItems);

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            DataTable dt = exec.GetQuery("GetPersonalLiabilityByTaskControlID", xmlDoc);

            return dt;
        }

        private static DataTable GetPersonalLiabilityByPersonalLiabilityIDDB(int PersonalLiabilityID, bool IsOPPQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("PersonalLiabilityID",
                SqlDbType.Int, 0, PersonalLiabilityID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                  SqlDbType.Bit, 0, IsOPPQuote.ToString(),
                  ref cookItems);

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            DataTable dt = exec.GetQuery("GetPersonalLiabilityByPersonalLiabilityID", xmlDoc);
            return dt;
        }

        private void SavePersonalLiability(int UserID, bool IsOPPQuote)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this.Mode)
                {
                    case 1:  //ADD
                        this.PersonalLiabilityID = Executor.Insert("AddPersonalLiability", this.GetInsertPersonalLiabilityXml(IsOPPQuote));
                        //this.History(this._mode, UserID);
                        break;

                    case 3:  //DELETE
                        //Executor.Update("DeleteAutoGuardServicesContract",this.GetDeletePoliciesXml(IsOPPQuote));
                        break;

                    case 4:  //CLEAR						
                        break;

                    default: //UPDATE
                        //this.History(this._mode, UserID);
                        //Executor.Update("UpdateProperties", this.GetUpdatePropertiesXml(IsOPPQuote));
                        break;
                }
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the Personal Liability. " + xcp.Message, xcp);
            }
        } 

        private XmlDocument GetInsertPersonalLiabilityXml(bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[139];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence1",
                SqlDbType.VarChar, 100, this.Residence1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence2",
                SqlDbType.VarChar, 100, this.Residence2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence3",
                SqlDbType.VarChar, 100, this.Residence3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence4",
                SqlDbType.VarChar, 100, this.Residence4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence5",
                SqlDbType.VarChar, 100, this.Residence5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence6",
                SqlDbType.VarChar, 100, this.Residence6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence7",
                SqlDbType.VarChar, 100, this.Residence7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence8",
                SqlDbType.VarChar, 100, this.Residence8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence9",
                SqlDbType.VarChar, 100, this.Residence9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence10",
                SqlDbType.VarChar, 100, this.Residence10.ToString(),
                ref cookItems);


            DbRequestXmlCooker.AttachCookItem("Property1ID",
                SqlDbType.Int, 0, this.Property1ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property2ID",
                SqlDbType.Int, 0, this.Property2ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property3ID",
                SqlDbType.Int, 0, this.Property3ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property4ID",
                SqlDbType.Int, 0, this.Property4ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property5ID",
                SqlDbType.Int, 0, this.Property5ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property6ID",
                SqlDbType.Int, 0, this.Property6ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property7ID",
                SqlDbType.Int, 0, this.Property7ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property8ID",
                SqlDbType.Int, 0, this.Property8ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property9ID",
                SqlDbType.Int, 0, this.Property9ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Property10ID",
                SqlDbType.Int, 0, this.Property10ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool1",
                SqlDbType.Bit, 0, this.SwimmingPool1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool2",
                SqlDbType.Bit, 0, this.SwimmingPool2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool3",
                SqlDbType.Bit, 0, this.SwimmingPool3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool4",
                SqlDbType.Bit, 0, this.SwimmingPool4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool5",
                SqlDbType.Bit, 0, this.SwimmingPool5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool6",
                SqlDbType.Bit, 0, this.SwimmingPool6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool7",
                SqlDbType.Bit, 0, this.SwimmingPool7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool8",
                SqlDbType.Bit, 0, this.SwimmingPool8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool9",
                SqlDbType.Bit, 0, this.SwimmingPool9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SwimmingPool10",
                SqlDbType.Bit, 0, this.SwimmingPool10.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families1",
                SqlDbType.Int, 0, this.Families1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families2",
                SqlDbType.Int, 0, this.Families2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families3",
                SqlDbType.Int, 0, this.Families3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families4",
                SqlDbType.Int, 0, this.Families4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families5",
                SqlDbType.Int, 0, this.Families5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families6",
                SqlDbType.Int, 0, this.Families6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families7",
                SqlDbType.Int, 0, this.Families7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families8",
                SqlDbType.Int, 0, this.Families8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families9",
                SqlDbType.Int, 0, this.Families9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Families10",
                SqlDbType.Int, 0, this.Families10.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment1",
                SqlDbType.Int, 0, this.MedicalPayment1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment2",
                SqlDbType.Int, 0, this.MedicalPayment2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment3",
                SqlDbType.Int, 0, this.MedicalPayment3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment4",
                SqlDbType.Int, 0, this.MedicalPayment4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment5",
                SqlDbType.Int, 0, this.MedicalPayment5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment6",
                SqlDbType.Int, 0, this.MedicalPayment6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment7",
                SqlDbType.Int, 0, this.MedicalPayment7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment8",
                SqlDbType.Int, 0, this.MedicalPayment8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment9",
                SqlDbType.Int, 0, this.MedicalPayment9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment10",
                SqlDbType.Int, 0, this.MedicalPayment10.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PersonalLiability1",
                SqlDbType.Int, 0, this.PersonalLiability1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PersonalLiability2",
                SqlDbType.Int, 0, this.PersonalLiability2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PersonalLiability3",
                SqlDbType.Int, 0, this.PersonalLiability3.ToString(),
                ref cookItems);

               DbRequestXmlCooker.AttachCookItem("PersonalLiability4",
                SqlDbType.Int, 0, this.PersonalLiability4.ToString(),
                ref cookItems);

               DbRequestXmlCooker.AttachCookItem("PersonalLiability5",
                   SqlDbType.Int, 0, this.PersonalLiability5.ToString(),
                   ref cookItems);

               DbRequestXmlCooker.AttachCookItem("PersonalLiability6",
                   SqlDbType.Int, 0, this.PersonalLiability6.ToString(),
                   ref cookItems);

               DbRequestXmlCooker.AttachCookItem("PersonalLiability7",
                   SqlDbType.Int, 0, this.PersonalLiability7.ToString(),
                   ref cookItems);

               DbRequestXmlCooker.AttachCookItem("PersonalLiability8",
                   SqlDbType.Int, 0, this.PersonalLiability8.ToString(),
                   ref cookItems);

               DbRequestXmlCooker.AttachCookItem("PersonalLiability9",
                   SqlDbType.Int, 0, this.PersonalLiability9.ToString(),
                   ref cookItems);

               DbRequestXmlCooker.AttachCookItem("PersonalLiability10",
                   SqlDbType.Int, 0, this.PersonalLiability10.ToString(),
                   ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented1",
                SqlDbType.Bit, 0, this.Rented1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented2",
                SqlDbType.Bit, 0, this.Rented2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented3",
                SqlDbType.Bit, 0, this.Rented3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented4",
                SqlDbType.Bit, 0, this.Rented4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented5",
                SqlDbType.Bit, 0, this.Rented5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented6",
                SqlDbType.Bit, 0, this.Rented6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented7",
                SqlDbType.Bit, 0, this.Rented7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented8",
                SqlDbType.Bit, 0, this.Rented8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented9",
                SqlDbType.Bit, 0, this.Rented9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rented10",
                SqlDbType.Bit, 0, this.Rented10.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("ExperienceCredit",
                SqlDbType.Float, 0, this.ExperienceCredit.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate1",
                SqlDbType.Float, 0, this.BasicRate1.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit1",
                SqlDbType.Float, 0, this.IncreaseLimit1.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium1",
                SqlDbType.Float, 0, this.BasicPremium1.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem1",
                SqlDbType.Float, 0, this.MedicalPrem1.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor1",
                 SqlDbType.Float, 0, this.DiscountFactor1.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium1",
                  SqlDbType.Float, 0, this.Premium1.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate2",
                 SqlDbType.Float, 0, this.BasicRate2.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit2",
                SqlDbType.Float, 0, this.IncreaseLimit2.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium2",
                SqlDbType.Float, 0, this.BasicPremium2.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem2",
                SqlDbType.Float, 0, this.MedicalPrem2.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor2",
                 SqlDbType.Float, 0, this.DiscountFactor2.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium2",
                  SqlDbType.Float, 0, this.Premium2.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate3",
                   SqlDbType.Float, 0, this.BasicRate3.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit3",
                SqlDbType.Float, 0, this.IncreaseLimit3.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium3",
                SqlDbType.Float, 0, this.BasicPremium3.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem3",
                SqlDbType.Float, 0, this.MedicalPrem3.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor3",
                 SqlDbType.Float, 0, this.DiscountFactor3.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium3",
                  SqlDbType.Float, 0, this.Premium3.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate4",
                   SqlDbType.Float, 0, this.BasicRate4.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit4",
                SqlDbType.Float, 0, this.IncreaseLimit4.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium4",
                SqlDbType.Float, 0, this.BasicPremium4.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem4",
                SqlDbType.Float, 0, this.MedicalPrem4.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor4",
                 SqlDbType.Float, 0, this.DiscountFactor4.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium4",
                  SqlDbType.Float, 0, this.Premium4.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate5",
                    SqlDbType.Float, 0, this.BasicRate5.ToString(),
                   ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit5",
                SqlDbType.Float, 0, this.IncreaseLimit5.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium5",
                SqlDbType.Float, 0, this.BasicPremium5.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem5",
                SqlDbType.Float, 0, this.MedicalPrem5.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor5",
                 SqlDbType.Float, 0, this.DiscountFactor5.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium5",
                  SqlDbType.Float, 0, this.Premium5.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate6",
                    SqlDbType.Float, 0, this.BasicRate6.ToString(),
                   ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit6",
                SqlDbType.Float, 0, this.IncreaseLimit6.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium6",
                SqlDbType.Float, 0, this.BasicPremium6.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem6",
                SqlDbType.Float, 0, this.MedicalPrem6.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor6",
                 SqlDbType.Float, 0, this.DiscountFactor6.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium6",
                  SqlDbType.Float, 0, this.Premium6.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate7",
                    SqlDbType.Float, 0, this.BasicRate7.ToString(),
                   ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit7",
                SqlDbType.Float, 0, this.IncreaseLimit7.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium7",
                SqlDbType.Float, 0, this.BasicPremium7.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem7",
                SqlDbType.Float, 0, this.MedicalPrem7.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor7",
                 SqlDbType.Float, 0, this.DiscountFactor7.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium7",
                  SqlDbType.Float, 0, this.Premium7.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate8",
                    SqlDbType.Float, 0, this.BasicRate8.ToString(),
                   ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit8",
                SqlDbType.Float, 0, this.IncreaseLimit8.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium8",
                SqlDbType.Float, 0, this.BasicPremium8.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem8",
                SqlDbType.Float, 0, this.MedicalPrem8.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor8",
                 SqlDbType.Float, 0, this.DiscountFactor8.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium8",
                  SqlDbType.Float, 0, this.Premium8.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate9",
                    SqlDbType.Float, 0, this.BasicRate9.ToString(),
                   ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit9",
                SqlDbType.Float, 0, this.IncreaseLimit9.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium9",
                SqlDbType.Float, 0, this.BasicPremium9.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem9",
                SqlDbType.Float, 0, this.MedicalPrem9.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor9",
                 SqlDbType.Float, 0, this.DiscountFactor9.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium9",
                  SqlDbType.Float, 0, this.Premium9.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicRate10",
                    SqlDbType.Float, 0, this.BasicRate10.ToString(),
                   ref cookItems);

             DbRequestXmlCooker.AttachCookItem("IncreaseLimit10",
                SqlDbType.Float, 0, this.IncreaseLimit10.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BasicPremium10",
                SqlDbType.Float, 0, this.BasicPremium10.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("MedicalPrem10",
                SqlDbType.Float, 0, this.MedicalPrem10.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DiscountFactor10",
                 SqlDbType.Float, 0, this.DiscountFactor10.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Premium10",
                  SqlDbType.Float, 0, this.Premium10.ToString(),
                  ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Limit",
                   SqlDbType.Int, 0, this.Limit.ToString(),
                   ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalLiabilityPremium",
                SqlDbType.Float, 0, this.TotalLiabilityPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SubTotalPremium",
                SqlDbType.Float, 0, this.SubTotalPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Charge",
                 SqlDbType.Float, 0, this.Charge.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
                 SqlDbType.Float, 0, this.TotalPremium.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                    SqlDbType.Bit, 0, IsOppQuote.ToString(),
                    ref cookItems);

            //ADDED BY POR 5/22/2014
            DbRequestXmlCooker.AttachCookItem("DiscUnderwritter", SqlDbType.Float, 0, this.DiscUnderwritter.ToString(), ref cookItems);
            //END

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

        #endregion

    }
}
