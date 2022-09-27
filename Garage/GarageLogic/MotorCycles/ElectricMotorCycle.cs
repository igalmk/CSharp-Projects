using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Parts;

namespace Ex03.GarageLogic.MotorCycles
{
    internal sealed class ElectricMotorCycle : MotorCycle
    {
        public ElectricMotorCycle(string i_ModelName, string i_LicenseNumber,
            string i_ManufacutreName, float i_CurrentAirPresure,
            eLicenseType i_LicenseType, int i_EngineVolume, float i_CurrentBatteryCharge)
            : base(i_ModelName, i_LicenseNumber, eWheelsAmount.MotorCycle, i_ManufacutreName, i_CurrentAirPresure, eWheelMaxPressure.MotorCycle,
                  i_LicenseType, i_EngineVolume)
        {
            this.m_EnergySourceType = eEnergySourceTypes.Electricity;
            this.m_EnergySourceContainer = new Battery(i_CurrentBatteryCharge, 2.5f);
            this.m_CurrentEnergyPercentage = (this.m_EnergySourceContainer.CurrentEnergy / this.m_EnergySourceContainer.MaxEnergy) * 100f;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// Throw ArgumentException!!!
        /// </summary>
        internal static List<object> ConvertStringsToCtorParameters(List<string> i_VehicleInfo)
        {
            List<object> allElectricMotorCycleMembersIncludeParents;

            allElectricMotorCycleMembersIncludeParents = parseVehicleInfo(i_VehicleInfo);
            logicCheck(allElectricMotorCycleMembersIncludeParents);

            return allElectricMotorCycleMembersIncludeParents;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// </summary>
        private static List<object> parseVehicleInfo(List<string> i_VehicleInfo)
        {
            List<object> allElectricMotorCycleMembersIncludeParents = new List<object>();

            allElectricMotorCycleMembersIncludeParents.Add(i_VehicleInfo[0]);
            allElectricMotorCycleMembersIncludeParents.Add(i_VehicleInfo[1]);
            allElectricMotorCycleMembersIncludeParents.Add(i_VehicleInfo[2]);
            allElectricMotorCycleMembersIncludeParents.Add(float.Parse(i_VehicleInfo[3]));
            allElectricMotorCycleMembersIncludeParents.Add((eLicenseType)Enum.Parse(typeof(eLicenseType), i_VehicleInfo[4]));
            allElectricMotorCycleMembersIncludeParents.Add(int.Parse(i_VehicleInfo[5]));
            allElectricMotorCycleMembersIncludeParents.Add(float.Parse(i_VehicleInfo[6]));

            return allElectricMotorCycleMembersIncludeParents;
        }

        /// <summary>
        /// Throw ArgumentException!!!
        /// </summary>
        private static void logicCheck(List<object> i_AllElectricMotorCycleMembersIncludeParents)
        {
            if (((string)i_AllElectricMotorCycleMembersIncludeParents[0]).Length == 0 || ((string)i_AllElectricMotorCycleMembersIncludeParents[1]).Length == 0 ||
                ((string)i_AllElectricMotorCycleMembersIncludeParents[2]).Length == 0)
            {
                throw new ArgumentException("One or more of the following is empty: Model Name, License Number, Manufacutre Name.");
            }

            if (((float)i_AllElectricMotorCycleMembersIncludeParents[3]) > (float)eWheelMaxPressure.MotorCycle)
            {
                throw new ArgumentException("Current air presure is higher than the max air presure.");
            }

            if ((int)i_AllElectricMotorCycleMembersIncludeParents[5] <= 0)
            {
                throw new ArgumentException("Invalid engine volume.");
            }

            if (((float)i_AllElectricMotorCycleMembersIncludeParents[6]) > 2.5f)
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
