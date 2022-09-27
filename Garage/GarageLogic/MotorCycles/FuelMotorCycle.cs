using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Parts;

namespace Ex03.GarageLogic.MotorCycles
{
    internal sealed class FuelMotorCycle : MotorCycle
    {
        public FuelMotorCycle(string i_ModelName, string i_LicenseNumber,
            string i_ManufacutreName, float i_CurrentAirPresure,
            eLicenseType i_LicenseType, int i_EngineVolume, float i_CurrentFuelAmount)
            : base(i_ModelName, i_LicenseNumber, eWheelsAmount.MotorCycle, i_ManufacutreName, i_CurrentAirPresure, eWheelMaxPressure.MotorCycle,
                  i_LicenseType, i_EngineVolume)
        {
            this.m_EnergySourceContainer = new FuelTank(i_CurrentFuelAmount, 6.2f);
            this.m_EnergySourceType = eEnergySourceTypes.Octan98;
            this.m_CurrentEnergyPercentage = (this.m_EnergySourceContainer.CurrentEnergy / this.m_EnergySourceContainer.MaxEnergy) * 100f;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// Throw ArgumentException!!!
        /// </summary>
        internal static List<object> ConvertStringsToCtorParameters(List<string> i_VehicleInfo)
        {
            List<object> allFuelMotorCycleMembersIncludeParents;

            allFuelMotorCycleMembersIncludeParents = parseVehicleInfo(i_VehicleInfo);
            logicCheck(allFuelMotorCycleMembersIncludeParents);

            return allFuelMotorCycleMembersIncludeParents;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// </summary>
        private static List<object> parseVehicleInfo(List<string> i_VehicleInfo)
        {
            List<object> allFuelMotorCycleMembersIncludeParents = new List<object>();

            allFuelMotorCycleMembersIncludeParents.Add(i_VehicleInfo[0]);
            allFuelMotorCycleMembersIncludeParents.Add(i_VehicleInfo[1]);
            allFuelMotorCycleMembersIncludeParents.Add(i_VehicleInfo[2]);
            allFuelMotorCycleMembersIncludeParents.Add(float.Parse(i_VehicleInfo[3]));
            allFuelMotorCycleMembersIncludeParents.Add((eLicenseType)Enum.Parse(typeof(eLicenseType), i_VehicleInfo[4]));
            allFuelMotorCycleMembersIncludeParents.Add(int.Parse(i_VehicleInfo[5]));
            allFuelMotorCycleMembersIncludeParents.Add(float.Parse(i_VehicleInfo[6]));

            return allFuelMotorCycleMembersIncludeParents;
        }

        /// <summary>
        /// Throw ArgumentException!!!
        /// </summary>
        private static void logicCheck(List<object> i_AllFuelMotorCycleMembersIncludeParents)
        {
            if (((string)i_AllFuelMotorCycleMembersIncludeParents[0]).Length == 0 || ((string)i_AllFuelMotorCycleMembersIncludeParents[1]).Length == 0 ||
                ((string)i_AllFuelMotorCycleMembersIncludeParents[2]).Length == 0)
            {
                throw new ArgumentException("One or more of the following is empty: Model Name, License Number, Manufacutre Name.");
            }

            if (((float)i_AllFuelMotorCycleMembersIncludeParents[3]) > (float)eWheelMaxPressure.MotorCycle)
            {
                throw new ArgumentException("Current air presure is higher than the max air presure.");
            }

            if ((int)i_AllFuelMotorCycleMembersIncludeParents[5] <= 0)
            {
                throw new ArgumentException("Invalid engine volume.");
            }

            if (((float)i_AllFuelMotorCycleMembersIncludeParents[6]) > 6.2f)
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
