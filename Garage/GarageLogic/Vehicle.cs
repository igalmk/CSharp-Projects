using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Parts;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string         r_ModelName;
        private readonly string         r_LicenseNumber;
        protected eEnergySourceTypes    m_EnergySourceType;
        protected float                 m_CurrentEnergyPercentage;
        protected EnergySourceContainer m_EnergySourceContainer;
        private List<Wheel>             m_Wheels;

        internal Vehicle(string i_ModelName, string i_LicenseNumber,
            eWheelsAmount i_EwheelsAmount, string i_ManufacutreName, float i_CurrentAirPresure, eWheelMaxPressure i_EmaxAirPresure)
        {
            this.r_ModelName = i_ModelName;
            this.r_LicenseNumber = i_LicenseNumber;
            this.m_CurrentEnergyPercentage = 0;
            this.m_Wheels = new List<Wheel>();
            for(int i = 0; i < (int)i_EwheelsAmount; i++)
            {
                m_Wheels.Add(new Wheel(i_ManufacutreName, i_CurrentAirPresure, (float)i_EmaxAirPresure));
            }
    }

        internal string ModelName
        {
            get
            {
                return r_ModelName;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        internal eEnergySourceTypes EnergySourceType
        {
            get
            {
                return m_EnergySourceType;
            }
        }

        internal float CurrentEnergyPercentage
        {
            get
            {
                return m_CurrentEnergyPercentage;
            }
        }

        internal void AirPumpAllWheelsToMax()
        {
            foreach(Wheel wheel in m_Wheels)
            {
                wheel.AirPumpToMax();
            }
        }

        internal abstract void FillEnergy(float i_EnergyAmount, eEnergySourceTypes eEnergySourceType);

        public override string ToString()
        {
            return string.Format(
@"
Model name: {0}.
License number: {1}.
Current energy percentage: {2}.
Wheels amount: {3}.{4}", this.r_ModelName,
this.r_LicenseNumber,
this.m_CurrentEnergyPercentage,
this.m_Wheels.Count,
this.m_Wheels[0].ToString());
        }
    }
}
