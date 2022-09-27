namespace Ex03.GarageLogic
{
    public class Customer
    {
        private readonly string   r_OwnerName;
        private string            m_PhoneNumber;
        private eVehicleCondition m_VehicleCondition;

        public Customer(string i_OwnerName, string i_PhoneNumber)
        {
            this.r_OwnerName = i_OwnerName;
            this.m_PhoneNumber = i_PhoneNumber;
            this.m_VehicleCondition = eVehicleCondition.InRepair;
        }

        internal string OwnerName
        {
            get
            {
                return r_OwnerName;
            }
        }

        internal string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
        }

        internal eVehicleCondition eVehicleCondition
        {
            get
            {
                return m_VehicleCondition;
            }

            set
            {
                this.m_VehicleCondition = value;
            }
        }

        public override string ToString()
        {
            return string.Format(
@"
Owner name: {0}.
Phone number: {1}.
Vehicle condition: {2}.", this.r_OwnerName,
this.m_PhoneNumber,
this.m_VehicleCondition);
        }
    }
}
