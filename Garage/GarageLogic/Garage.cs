using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, CustomerVehiclePair> m_LicenseNumberStringToCustomerVehiclePair;

        public Garage()
        {
            this.m_LicenseNumberStringToCustomerVehiclePair = new Dictionary<string, CustomerVehiclePair>();
        }

        public bool InsertVehicle(string i_LicenseNumber, CustomerVehiclePair i_NewCustomerVehiclePair)
        {
            CustomerVehiclePair customerVehiclePair;
            bool                isLicenseNumberExists;

            if (this.m_LicenseNumberStringToCustomerVehiclePair.TryGetValue(i_LicenseNumber, out customerVehiclePair))
            {
                customerVehiclePair.Customer.eVehicleCondition = eVehicleCondition.InRepair;
                isLicenseNumberExists = true;
            }
            else
            {
                this.m_LicenseNumberStringToCustomerVehiclePair.Add(i_LicenseNumber, i_NewCustomerVehiclePair);
                isLicenseNumberExists = false;
            }

            return isLicenseNumberExists;
        }

        public List<string> ShowLicenseNumbers(eVehicleCondition i_EvehicleCondition)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (KeyValuePair<string, CustomerVehiclePair> licenseNumberAndCustomerVehiclePair in m_LicenseNumberStringToCustomerVehiclePair)
            {
                if(licenseNumberAndCustomerVehiclePair.Value.Customer.eVehicleCondition.Equals(i_EvehicleCondition))
                {
                    licenseNumbers.Add(licenseNumberAndCustomerVehiclePair.Value.Vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        public List<string> ShowLicenseNumbers()
        {
            List<string> allLicenseNumbers = new List<string>();

            allLicenseNumbers.AddRange(ShowLicenseNumbers(eVehicleCondition.InRepair));
            allLicenseNumbers.AddRange(ShowLicenseNumbers(eVehicleCondition.Payed));
            allLicenseNumbers.AddRange(ShowLicenseNumbers(eVehicleCondition.Repaired));

            return allLicenseNumbers;
        }

        /// <summary>
        /// KeyNotFoundException!!!
        /// </summary>
        public void ChangeVehicleCondition(string i_LicenseNumber, eVehicleCondition i_UpdateVehicleCondition)
        {
            CustomerVehiclePair customerVehiclePair;

            if(this.m_LicenseNumberStringToCustomerVehiclePair.TryGetValue(i_LicenseNumber, out customerVehiclePair))
            {
                customerVehiclePair.Customer.eVehicleCondition = i_UpdateVehicleCondition;
            }
            else
            {
                throw new KeyNotFoundException("Vehicle not found in garage!");
            }
        }

        /// <summary>
        /// KeyNotFoundException!!!
        /// </summary>
        public void InflateMaxAirPresure(string i_LicenseNumber)
        {
            CustomerVehiclePair customerVehiclePair;

            if (this.m_LicenseNumberStringToCustomerVehiclePair.TryGetValue(i_LicenseNumber, out customerVehiclePair))
            {
                customerVehiclePair.Vehicle.AirPumpAllWheelsToMax();
            }
            else
            {
                throw new KeyNotFoundException("Vehicle not found in garage!");
            }
        }

        /// <summary>
        /// KeyNotFoundException!!!
        /// ArgumentException!!!
        /// EnergySourceContainer!!!
        /// </summary>
        public void FuelVehicle(string i_LicenseNumber, eEnergySourceTypes i_EnergySourceTypes, float i_FuelAmount)
        {
            CustomerVehiclePair customerVehiclePair;

            if (this.m_LicenseNumberStringToCustomerVehiclePair.TryGetValue(i_LicenseNumber, out customerVehiclePair))
            {
                customerVehiclePair.Vehicle.FillEnergy(i_FuelAmount, i_EnergySourceTypes);
            }
            else
            {
                throw new KeyNotFoundException("Vehicle not found in garage!");
            }
        }

        /// <summary>
        /// KeyNotFoundException!!!
        /// ArgumentException!!!
        /// EnergySourceContainer!!!
        /// </summary>
        public void ChargeVehicle(string i_LicenseNumber, float i_ChargingAmountByMinutes)
        {
            CustomerVehiclePair customerVehiclePair;

            if (this.m_LicenseNumberStringToCustomerVehiclePair.TryGetValue(i_LicenseNumber, out customerVehiclePair))
            {
                customerVehiclePair.Vehicle.FillEnergy(i_ChargingAmountByMinutes, eEnergySourceTypes.Electricity);
            }
            else
            {
                throw new KeyNotFoundException("Vehicle not found in garage!");
            }
        }

        /// <summary>
        /// KeyNotFoundException!!!
        /// </summary>
        public string ShowAllVehicleInformation(string i_LicenseNumber)
        {
            string              allVehicleInformation = null;
            CustomerVehiclePair customerVehiclePair;

            if (this.m_LicenseNumberStringToCustomerVehiclePair.TryGetValue(i_LicenseNumber, out customerVehiclePair))
            {
                allVehicleInformation = customerVehiclePair.ToString();
            }
            else
            {
                throw new KeyNotFoundException("Vehicle not found in garage!");
            }

            return allVehicleInformation;
        }
    }
}
