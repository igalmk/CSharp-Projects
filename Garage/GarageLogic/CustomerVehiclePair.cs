namespace Ex03.GarageLogic
{
    public class CustomerVehiclePair
    {
        private Customer m_Customer;
        private Vehicle  m_Vehicle;

        public CustomerVehiclePair(Customer i_Customer, Vehicle i_Vehicle)
        {
            this.m_Customer = i_Customer;
            this.m_Vehicle = i_Vehicle;
        }

        internal Customer Customer
        {
            get
            {
                return m_Customer;
            }
        }

        internal Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public override string ToString()
        {
            return this.m_Customer.ToString() + this.m_Vehicle.ToString();
        }
    }
}
