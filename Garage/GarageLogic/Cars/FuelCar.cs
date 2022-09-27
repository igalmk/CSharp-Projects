using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Parts;

namespace Ex03.GarageLogic
{
    internal sealed class FuelCar : Car
    {
        public FuelCar(string i_ModelName, string i_LicenseNumber, string i_ManufacutreName, float i_CurrentAirPresure, eVehicleColor i_CarColor,
            uint i_DoorsAmount, float i_CurrentFuelAmount)
            : base(i_ModelName, i_LicenseNumber, eWheelsAmount.Car, i_ManufacutreName, i_CurrentAirPresure, eWheelMaxPressure.Car, i_CarColor, i_DoorsAmount)
        {
            this.m_EnergySourceContainer = new FuelTank(i_CurrentFuelAmount, 38f);
            this.m_EnergySourceType = eEnergySourceTypes.Octan95;
            this.m_CurrentEnergyPercentage = (this.m_EnergySourceContainer.CurrentEnergy / this.m_EnergySourceContainer.MaxEnergy) * 100f;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// Throw ArgumentException!!!
        /// </summary>
        internal static List<object> ConvertStringsToCtorParameters(List<string> i_VehicleInfo)
        {
            List<object> allFuelCarMembersIncludeParents;

            allFuelCarMembersIncludeParents = parseVehicleInfo(i_VehicleInfo);
            logicCheck(allFuelCarMembersIncludeParents);

            return allFuelCarMembersIncludeParents;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// </summary>
        private static List<object> parseVehicleInfo(List<string> i_VehicleInfo)
        {
            List<object> allFuelCarMembersIncludeParents = new List<object>();

            allFuelCarMembersIncludeParents.Add(i_VehicleInfo[0]);
            allFuelCarMembersIncludeParents.Add(i_VehicleInfo[1]);
            allFuelCarMembersIncludeParents.Add(i_VehicleInfo[2]);
            allFuelCarMembersIncludeParents.Add(float.Parse(i_VehicleInfo[3]));
            allFuelCarMembersIncludeParents.Add((eVehicleColor)Enum.Parse(typeof(eVehicleColor), i_VehicleInfo[4]));
            allFuelCarMembersIncludeParents.Add(uint.Parse(i_VehicleInfo[5]));
            allFuelCarMembersIncludeParents.Add(float.Parse(i_VehicleInfo[6]));

            return allFuelCarMembersIncludeParents;
        }

        /// <summary>
        /// Throw ArgumentException!!!
        /// </summary>
        private static void logicCheck(List<object> i_AllFuelCarMembersIncludeParents)
        {
            if(((string)i_AllFuelCarMembersIncludeParents[0]).Length == 0 || ((string)i_AllFuelCarMembersIncludeParents[1]).Length == 0 ||
                ((string)i_AllFuelCarMembersIncludeParents[2]).Length == 0)
            {
                throw new ArgumentException("One or more of the following is empty: Model Name, License Number, Manufacutre Name.");
            }

            if(((float)i_AllFuelCarMembersIncludeParents[3]) > (float)eWheelMaxPressure.Car)
            {
                throw new ArgumentException("Current air presure is higher than the max air presure.");
            }

            if((uint)i_AllFuelCarMembersIncludeParents[5] != 2 && (uint)i_AllFuelCarMembersIncludeParents[5] != 3 &&
                (uint)i_AllFuelCarMembersIncludeParents[5] != 4 && (uint)i_AllFuelCarMembersIncludeParents[5] != 5)
            {
                throw new ArgumentException("Invalid doors amount.");
            }

            if (((float)i_AllFuelCarMembersIncludeParents[6]) > 38f)
            {
                throw new ArgumentException("Current fuel amount is higher than the max fuel tank.");
            }
        }

        /// <summary>
        /// ArgumentException!!!
        /// EnergySourceContainer!!!
        /// </summary>
        internal override void FillEnergy(float i_EnergyAmount, eEnergySourceTypes i_EnergySourceType)
        {
            Refuel(i_EnergyAmount, i_EnergySourceType);
        }

        /// <summary>
        /// ArgumentException!!!
        /// EnergySourceContainer!!!
        /// </summary>
        private void Refuel(float i_FuelAmount, eEnergySourceTypes i_FuelType)
        {
            if (this.m_EnergySourceType != i_FuelType)
            {
                throw new ArgumentException(string.Format("Invalid fuel type, this vehicle energy source is: {0}.", this.m_EnergySourceType));
            }

            ((FuelTank)this.m_EnergySourceContainer).Fuel(i_FuelAmount);
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(
@"
Fuel type: {0}.
Max fuel tank: {1}.
Current fuel amount: {2}.", this.m_EnergySourceType,
this.m_EnergySourceContainer.MaxEnergy,
this.m_EnergySourceContainer.CurrentEnergy);
        }
    }
}
