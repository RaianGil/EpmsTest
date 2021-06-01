using System;

namespace EPolicy.Quotes
{
	/// <summary>
	/// Summary description for Enumerators.
	/// </summary>
	public class Enumerators
	{
		private Enumerators()
		{
			
		}

		public enum Gender	{ Male=1, Female };

		public enum MaritalStatus	{ Single=1, Married, Separated, Divorced, Widower};

		public enum Modes	{ Nothing, Insert, Update, Delete };

		public enum NewUse { Used = 1, New };

		public enum Premiums  { Collision = 1, Comprehensive, 
			ActualValue, IsoCode, LeaseLoanGap, Towing,
			Assistance, SeatBelt, Medical, PersonalAccidentRider, 
			AnnualPremium, BodilyInjury, PropertyDamage, 
			CombinedSingle, Periods, Depreciation, VehicleRental,
            AccidentalDeath, UninsuredSingle, UninsuredSplit,
            EquipmentAudio,EquipmentTapes,CustomizeEquipmentColl,
            CustomizeEquipmentComp};

		public static String DecodePremiums(int type)
		{
			switch((Premiums)type)
			{
				case Premiums.Collision: // Premiums.Collision
					return "Collision";
				case Premiums.Comprehensive: // Premiums.Comprehensive
					return "Comprehensive";
				case Premiums.ActualValue: // Premiums.ActualValue
					return "Actual Value";
				case Premiums.IsoCode: // Premiums.IsoCode
					return "ISO Code";
				case Premiums.LeaseLoanGap: // Premiums.LeaseLoanGap
					return "Lease Loan Gap";
				case Premiums.Towing: // Premiums.Towing
					return "Towing";
				case Premiums.VehicleRental: // Premiums.VehicleRental
					return "Vehicle Rental";
				case Premiums.Assistance: // Premiums.Assistance
					return "Assistance";
				case Premiums.SeatBelt: // Premiums.SeatBelt
					return "Seat Belt";
				case Premiums.Medical: // Premiums.Medical
					return "Medical";
				case Premiums.PersonalAccidentRider: // Premiums.PersonalAccidentRider
					return "Personal Accident Rider";
				case Premiums.AnnualPremium: // Premiums.AnnualPremium
					return "Annual Premium";
				case Premiums.BodilyInjury: // Premiums.BodilyInjury
					return "Bodily Injury";
				case Premiums.PropertyDamage: // Premiums.PropertyDamage
					return "Property Damage";
				case Premiums.CombinedSingle: // Premiums.CombinedSingle
					return "Combined Single";
				case Premiums.Periods: // Premiums.Periods
					return "Periods";
				case Premiums.Depreciation: // Premiums.Depreciation
					return "Depreciation";
                case Premiums.AccidentalDeath: // Premiums.Depreciation
                    return "Accidental Death";
                case Premiums.UninsuredSingle: // Premiums.Depreciation
                    return "Uninsured Single";
                case Premiums.UninsuredSplit: // Premiums.Depreciation
                    return "Uninsured Split";
                case Premiums.EquipmentAudio: // Premiums.Depreciation
                    return "Equipment Audio";
                case Premiums.EquipmentTapes: // Premiums.Depreciation
                    return "Equipment Tapes";
                case Premiums.CustomizeEquipmentColl: // Premiums.Depreciation
                    return "Customize Equipment Coll";
                case Premiums.CustomizeEquipmentComp: // Premiums.Depreciation
                    return "Customize Equipment Comp";
				default:
					return type.ToString();
			}
		}

	}
}
