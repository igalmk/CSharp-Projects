using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.Parts
{
    internal class Battery : EnergySourceContainer
    {
        internal Battery(float i_CurrentEnergy, float i_MaxEnergy)
            : base(i_CurrentEnergy, i_MaxEnergy)
        {
        }

        /// <summary>
        /// ValueOutOfRangeException!!!
        /// </summary>
        internal void Charge(float i_ChargeAmount)
        {
            if (this.CurrentEnergy + i_ChargeAmount > this.MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergy);
            }

            this.CurrentEnergy = this.CurrentEnergy + i_ChargeAmount;
        }
    }
}
