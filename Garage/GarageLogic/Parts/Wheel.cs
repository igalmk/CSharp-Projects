using System;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private readonly string r_ManufacutreName;
        private float m_CurrentAirPresure;
        private float m_MaxAirPresure;

        internal Wheel(string i_ManufacutreName, float i_CurrentAirPresure, float i_MaxAirPresure)
        {
            this.r_ManufacutreName = i_ManufacutreName;
            this.m_CurrentAirPresure = i_CurrentAirPresure;
            this.m_MaxAirPresure = i_MaxAirPresure;
        }

        private void AirPump(float i_AirInjectionAmount)
        {
            this.m_CurrentAirPresure = this.m_CurrentAirPresure + i_AirInjectionAmount;
        }

        internal void AirPumpToMax()
        {
            AirPump(this.m_MaxAirPresure - this.m_CurrentAirPresure);
        }

        internal string ManufacutreName
        {
            get
            {
                return r_ManufacutreName;
            }
        }

        internal float CurrentAirPresure
        {
            get
            {
                return m_CurrentAirPresure;
            }
        }

        internal float MaxAirPresure
        {
            get
            {
                return m_MaxAirPresure;
            }
        }

        public override string ToString()
        {
            return string.Format(
@"
Wheels information:
Manufacutre name: {0}.
Current air presure: {1}.
Max air presure: {2}.", this.r_ManufacutreName,
this.m_CurrentAirPresure,
this.m_MaxAirPresure);
        }
    }
}
