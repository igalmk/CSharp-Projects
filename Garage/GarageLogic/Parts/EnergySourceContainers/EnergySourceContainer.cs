namespace Ex03.GarageLogic.Parts
{
    public abstract class EnergySourceContainer
    {
        private readonly float r_MaxEnergy;
        private float          m_CurrentEnergy;

        internal EnergySourceContainer(float i_CurrentEnergy, float i_MaxEnergy)
        {
            this.m_CurrentEnergy = i_CurrentEnergy;
            this.r_MaxEnergy = i_MaxEnergy;
        }

        internal float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        internal float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                m_CurrentEnergy = value;
            }
        }
    }
}
