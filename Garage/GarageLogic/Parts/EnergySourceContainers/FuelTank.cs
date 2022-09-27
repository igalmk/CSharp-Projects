using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Parts
{
    internal class FuelTank : EnergySourceContainer
    {
        internal FuelTank(float i_CurrentEnergy, float i_MaxEnergy)
            : base(i_CurrentEnergy, i_MaxEnergy)
        {
        }

        /// <summary>
        /// ValueOutOfRangeException!!!
        /// </summary>
        internal void Fuel(float i_FuelAmount)
        {
            if(this.CurrentEnergy + i_FuelAmount > this.MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergy);
            }

            this.CurrentEnergy = this.CurrentEnergy + i_FuelAmount;
        }
    }
}
