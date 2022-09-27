using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Parts;

namespace Ex03.GarageLogic
{
    internal sealed class FuelTruck : Truck
    {
        public FuelTruck(string i_ModelName, string i_LicenseNumber,
            string i_ManufacutreName, float i_CurrentAirPresure,
            bool i_IsTransportingRefrigeratedContents, float i_CargoVolume, float i_CurrentFuelAmount)
            : base(i_ModelName, i_LicenseNumber, eWheelsAmount.Truck, i_ManufacutreName, i_CurrentAirPresure, eWheelMaxPressure.Truck,
                  i_IsTransportingRefrigeratedContents, i_CargoVolume)
        {
            this.m_EnergySourceContainer = new FuelTank(i_CurrentFuelAmount, 120f);
            this.m_EnergySourceType = eEnergySourceTypes.Soler;
            this.m_CurrentEnergyPercentage = (this.m_EnergySourceContainer.CurrentEnergy / this.m_EnergySourceContainer.MaxEnergy) * 100f;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// Throw ArgumentException!!!
        /// </summary>
        internal static List<object> ConvertStringsToCtorParameters(List<string> i_VehicleInfo)
        {
            List<object> allFuelTruckMembersIncludeParents;

            allFuelTruckMembersIncludeParents = parseVehicleInfo(i_VehicleInfo);
            logicCheck(allFuelTruckMembersIncludeParents);

            return allFuelTruckMembersIncludeParents;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// </summary>
        private static List<object> parseVehicleInfo(List<string> i_VehicleInfo)
        {
            List<object> allFuelTruckMembersIncludeParents = new List<object>();

            allFuelTruckMembersIncludeParents.Add(i_VehicleInfo[0]);
            allFuelTruckMembersIncludeParents.Add(i_VehicleInfo[1]);
            allFuelTruckMembersIncludeParents.Add(i_VehicleInfo[2]);
            allFuelTruckMembersIncludeParents.Add(float.Parse(i_VehicleInfo[3]));
            if(i_VehicleInfo[4].ToUpper().Equals("YES") || i_VehicleInfo[4].ToUpper().Equals("TRUE") || i_VehicleInfo[4].Equals("1"))
            {
                allFuelTruckMembersIncludeParents.Add(bool.Parse("TRUE"));
            }
            else if(i_VehicleInfo[4].ToUpper().Equals("NO") || i_VehicleInfo[4].ToUpper().Equals("FALSE") || i_VehicleInfo[4].Equals("0"))
            {
                allFuelTruckMembersIncludeParents.Add(bool.Parse("FALSE"));
            }
            else
            {
                throw new ArgumentException("Please enter YES/NO for is Transporting Refrigerated Contents.");
            }

            allFuelTruckMembersIncludeParents.Add(float.Parse(i_VehicleInfo[5]));
            allFuelTruckMembersIncludeParents.Add(float.Parse(i_VehicleInfo[6]));

            return allFuelTruckMembersIncludeParents;
        }

        /// <summary>
        /// Throw ArgumentException!!!
        /// </summary>
        private static void logicCheck(List<object> i_AllFuelTruckMembersIncludeParents)
        {
            if (((string)i_AllFuelTruckMembersIncludeParents[0]).Length == 0 || ((string)i_AllFuelTruckMembersIncludeParents[1]).Length == 0 ||
                ((string)i_AllFuelTruckMembersIncludeParents[2]).Length == 0)
            {
                throw new ArgumentException("One or more of the following is empty: Model Name, License Number, Manufacutre Name.");
            }

            if (((float)i_AllFuelTruckMembersIncludeParents[3]) > (float)eWheelMaxPressure.Truck)
            {
                throw new ArgumentException("Current air presure is higher than the max air presure.");
            }

            if ((float)i_AllFuelTruckMembersIncludeParents[5] < 0f)
            {
                throw new ArgumentException("Cargo Volume can't be negative.");
            }

            if (((float)i_AllFuelTruckMembersIncludeParents[6]) > 120f)
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
