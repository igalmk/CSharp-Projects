using System;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        private readonly uint r_DoorsAmount;
        private eVehicleColor m_CarColor;

        internal Car(string i_ModelName, string i_LicenseNumber, eWheelsAmount i_EwheelsAmount, string i_ManufacutreName, float i_CurrentAirPresure,
            eWheelMaxPressure i_EmaxAirPresure, eVehicleColor i_CarColor, uint i_DoorsAmount)
            : base(i_ModelName, i_LicenseNumber, i_EwheelsAmount, i_ManufacutreName, i_CurrentAirPresure, i_EmaxAirPresure)
        {
            this.m_CarColor = i_CarColor;
            this.r_DoorsAmount = i_DoorsAmount;
        }

        internal eVehicleColor CarColor
        {
            get
            {
                return m_CarColor;
            }
        }

        internal uint DoorsAmount
        {
            get
            {
                return r_DoorsAmount;
            }
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(
@"
Car color: {0}.
Doors amount: {1}.", this.m_CarColor,
this.r_DoorsAmount);
        }
    }
}
