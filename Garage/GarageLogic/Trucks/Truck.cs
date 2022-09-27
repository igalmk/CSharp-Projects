using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal abstract class Truck : Vehicle
    {
        private bool  m_IsTransportingRefrigeratedContents;
        private float m_CargoVolume;

        internal Truck(string i_ModelName, string i_LicenseNumber,
            eWheelsAmount i_EwheelsAmount, string i_ManufacutreName, float i_CurrentAirPresure, eWheelMaxPressure i_EmaxAirPresure,
            bool i_IsTransportingRefrigeratedContents, float i_CargoVolume)
            : base(i_ModelName, i_LicenseNumber, i_EwheelsAmount, i_ManufacutreName, i_CurrentAirPresure, i_EmaxAirPresure)
        {
            this.m_IsTransportingRefrigeratedContents = i_IsTransportingRefrigeratedContents;
            this.m_CargoVolume = i_CargoVolume;
        }

        internal bool IsTransportingRefrigeratedContents
        {
            get
            {
                return m_IsTransportingRefrigeratedContents;
            }
        }

        internal float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(
@"
Is Transporting refrigerated contents: {0}.
Cargo volume: {1}.", this.m_IsTransportingRefrigeratedContents,
this.m_CargoVolume);
        }
    }
}
