using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Parts;

namespace Ex03.GarageLogic
{
    internal sealed class ElectricCar : Car
    {
        public ElectricCar(string i_ModelName, string i_LicenseNumber,
            string i_ManufacutreName, float i_CurrentAirPresure,
            eVehicleColor i_CarColor, uint i_DoorsAmount, float i_CurrentBatteryCharge)
            : base(i_ModelName, i_LicenseNumber, eWheelsAmount.Car, i_ManufacutreName, i_CurrentAirPresure, eWheelMaxPressure.Car,
             i_CarColor, i_DoorsAmount)
        {
            this.m_EnergySourceType = eEnergySourceTypes.Electricity;
            this.m_EnergySourceContainer = new Battery(i_CurrentBatteryCharge, 3.3f);
            this.m_CurrentEnergyPercentage = (this.m_EnergySourceContainer.CurrentEnergy / this.m_EnergySourceContainer.MaxEnergy) * 100f;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// Throw ArgumentException!!!
        /// </summary>
        internal static List<object> ConvertStringsToCtorParameters(List<string> i_VehicleInfo)
        {
            List<object> allElectricCarMembersIncludeParents;

            allElectricCarMembersIncludeParents = parseVehicleInfo(i_VehicleInfo);
            logicCheck(allElectricCarMembersIncludeParents);

            return allElectricCarMembersIncludeParents;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// </summary>
        private static List<object> parseVehicleInfo(List<string> i_VehicleInfo)
        {
            List<object> allElectricCarMembersIncludeParents = new List<object>();

            allElectricCarMembersIncludeParents.Add(i_VehicleInfo[0]);
            allElectricCarMembersIncludeParents.Add(i_VehicleInfo[1]);
            allElectricCarMembersIncludeParents.Add(i_VehicleInfo[2]);
            allElectricCarMembersIncludeParents.Add(float.Parse(i_VehicleInfo[3]));
            allElectricCarMembersIncludeParents.Add((eVehicleColor)Enum.Parse(typeof(eVehicleColor), i_VehicleInfo[4]));
            allElectricCarMembersIncludeParents.Add(uint.Parse(i_VehicleInfo[5]));
            allElectricCarMembersIncludeParents.Add(float.Parse(i_VehicleInfo[6]));

            return allElectricCarMembersIncludeParents;
        }

        /// <summary>
        /// Throw ArgumentException!!!
        /// </summary>
        private static void logicCheck(List<object> i_AllElectricCarMembersIncludeParents)
        {
            if (((string)i_AllElectricCarMembersIncludeParents[0]).Length == 0 || ((string)i_AllElectricCarMembersIncludeParents[1]).Length == 0 ||
                ((string)i_AllElectricCarMembersIncludeParents[2]).Length == 0)
            {
                throw new ArgumentException("One or more of the following is empty: Model Name, License Number, Manufacutre Name.");
            }

            if (((float)i_AllElectricCarMembersIncludeParents[3]) > (float)eWheelMaxPressure.Car)
            {
                throw new ArgumentException("Current air presure is higher than the max air presure.");
            }

            if ((uint)i_AllElectricCarMembersIncludeParents[5] != 2 && (uint)i_AllElectricCarMembersIncludeParents[5] != 3 &&
                (uint)i_AllElectricCarMembersIncludeParents[5] != 4 && (uint)i_AllElectricCarMembersIncludeParents[5] != 5)
            {
                throw new ArgumentException("Invalid doors amount.");
            }

            if (((float)i_AllElectricCarMembersIncludeParents[6]) > 3.3f)
            {
                throw new ArgumentException("Current battery charge is higher than the max battery.");
            }
        }

        /// <summary>
        /// ArgumentException!!!
        /// EnergySourceContainer!!!
        /// </summary>
        internal override void FillEnergy(float i_EnergyAmount, eEnergySourceTypes i_EnergySourceType)
        {
            if (this.m_EnergySourceType != i_EnergySourceType)
            {
                throw new ArgumentException(string.Format("This is an electric vehicle that runs on electricity."));
            }

            Charge(i_EnergyAmount);
        }

        /// <summary>
        /// ArgumentException!!!
        /// EnergySourceContainer!!!
        /// </summary>
        private void Charge(float i_ChargeAmount)
        {
            ((Battery)this.m_EnergySourceContainer).Charge(i_ChargeAmount);
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(
@"
Max battery charge: {0}.
Current battery charge: {1}.", this.m_EnergySourceContainer.MaxEnergy,
this.m_EnergySourceContainer.CurrentEnergy);
        }
    }
}
