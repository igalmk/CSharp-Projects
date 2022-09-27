using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.MotorCycles
{
    internal abstract class MotorCycle : Vehicle
    {
        private readonly int r_EngineVolume;
        private eLicenseType m_LicenseType;

        internal MotorCycle(string i_ModelName, string i_LicenseNumber,
            eWheelsAmount i_EwheelsAmount, string i_ManufacutreName, float i_CurrentAirPresure, eWheelMaxPressure i_EmaxAirPresure,
            eLicenseType i_Licensetype, int i_EngineVolume)
            : base(i_ModelName, i_LicenseNumber, i_EwheelsAmount, i_ManufacutreName, i_CurrentAirPresure, i_EmaxAirPresure)
        {
            this.m_LicenseType = i_Licensetype;
            this.r_EngineVolume = i_EngineVolume;
        }

        internal eLicenseType Licensetype
        {
            get
            {
                return m_LicenseType;
            }
        }

        internal int EngineVolume
        {
            get
            {
                return r_EngineVolume;
            }
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(
@"
License type: {0}.
Engine volume: {1}.", this.m_LicenseType,
this.r_EngineVolume);
        }
    }
}