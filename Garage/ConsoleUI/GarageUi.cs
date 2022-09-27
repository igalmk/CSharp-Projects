using Ex03.GarageLogic;
using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class GarageUi
    {
        private Garage m_Garage;

        public GarageUi()
        {
            this.m_Garage = new Garage();
        }

        public void Run()
        {
            bool         wishToExit = false;
            eMenuOptions menuChoice;
            
            Console.WriteLine("Welcome to our garage");
            while(!wishToExit)
            {
                printMenu();
                menuChoice = getMenuChoice();
                executeMenuChoice(menuChoice, ref wishToExit);
            }
        }

        private void executeMenuChoice(eMenuOptions menuChoice, ref bool io_WishToExit)
        {
            switch(menuChoice)
            {
                case eMenuOptions.InsertVehicle :
                    {
                        uiInsertVehicle();
                        break;
                    }
                case eMenuOptions.ShowLicenseNumbers:
                    {
                        uiShowLicenseNumbers();
                        break;
                    }
                case eMenuOptions.ChangeVehicleCondition:
                    {
                        uiChangeVehicleCondition();
                        break;
                    }
                case eMenuOptions.InflateMaxAirPresure:
                    {
                        uiInflateMaxAirPresure();
                        break;
                    }
                case eMenuOptions.FuelVehicle:
                    {
                        uiFuelVehicle();
                        break;
                    }
                case eMenuOptions.ChargeVehicle:
                    {
                        uiChargeVehicle();
                        break;
                    }
                case eMenuOptions.ShowAllVehicleInformation:
                    {
                        uiShowAllVehicleInformation();
                        break;
                    }
                case eMenuOptions.Exit:
                    {
                        io_WishToExit = true;
                        break;
                    }
            }
        }

        private void uiShowAllVehicleInformation()
        {
            String licenseNumber = null, allVehicleInformation = null;

            Console.WriteLine("Please enter license number:");
            licenseNumber = Console.ReadLine();
            try
            {
                allVehicleInformation = this.m_Garage.ShowAllVehicleInformation(licenseNumber);
                Console.WriteLine(allVehicleInformation);
            }
            catch (KeyNotFoundException knfex)
            {
                Console.WriteLine(knfex.Message);
            }
        }

        private void uiChargeVehicle()
        {
            string licenseNumber = null;
            float  chargeByMinutes;
            bool   isValidchargeAmount;

            Console.WriteLine("Please enter license number:");
            licenseNumber = Console.ReadLine();
            try
            {
                Console.WriteLine("Please enter charge by minutes amount:");
                isValidchargeAmount = float.TryParse(Console.ReadLine(), out chargeByMinutes);
                if (!isValidchargeAmount)
                {
                    throw new FormatException("Invalid charge by minutes amount!");
                }
                this.m_Garage.ChargeVehicle(licenseNumber, chargeByMinutes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void uiFuelVehicle()
        {
            String             licenseNumber = null;
            eEnergySourceTypes eEnergySourceType;
            float              fuelAmount;
            bool               isValidFuelAmount;

            Console.WriteLine("Please enter license number:");
            licenseNumber = Console.ReadLine();
            eEnergySourceType = getEnergySourceTypes();
            try
            {
                Console.WriteLine("Please enter fuel amount:");
                isValidFuelAmount = float.TryParse(Console.ReadLine(), out fuelAmount);
                if(!isValidFuelAmount)
                {
                    throw new FormatException("Invalid fuel amount!");
                }
                this.m_Garage.FuelVehicle(licenseNumber, eEnergySourceType, fuelAmount);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private eEnergySourceTypes getEnergySourceTypes()
        {
            bool   validChoice = false;
            int    userInput = 0;
            string fuelTypeMessage = string.Format(@"
Fuel types:
1. Soler.
2. Octan95.
3. Octan96.
4. Octan98.");

            Console.WriteLine(fuelTypeMessage);
            while (!validChoice)
            {
                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput >= 1 && userInput <= 4)
                    {
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Input needs to be between 1 to 4.");
                    }
                }
                else
                {
                    Console.WriteLine("Input needs to be a number.");
                }
            }

            return (eEnergySourceTypes)(Enum.Parse(typeof(eEnergySourceTypes), userInput.ToString()));
        }

        private void uiInflateMaxAirPresure()
        {
            String licenseNumber = null;

            Console.WriteLine("Please enter license number:");
            licenseNumber = Console.ReadLine();
            try
            {
                this.m_Garage.InflateMaxAirPresure(licenseNumber);
            }
            catch (KeyNotFoundException knfex)
            {
                Console.WriteLine(knfex.Message);
            }
        }

        private void uiChangeVehicleCondition()
        {
            string            licenseNumber = null;
            eVehicleCondition eVehicleCondition;

            Console.WriteLine("Please enter license number:");
            licenseNumber = Console.ReadLine();
            eVehicleCondition = getVehicleConditionChoice();
            try
            {
                this.m_Garage.ChangeVehicleCondition(licenseNumber, eVehicleCondition);
            }
            catch(KeyNotFoundException knfex)
            {
                Console.WriteLine(knfex.Message);
            }
        }

        private void uiShowLicenseNumbers()
        {
            eVehicleCondition eVehicleCondition;
            List<String>      licenseNumbers;

            Console.WriteLine("Do you wish to watch the license numbers specified by the vehicles conditions: YES/NO.");
            if(Console.ReadLine().ToUpper().Equals("YES"))
            {
                eVehicleCondition = getVehicleConditionChoice();
                licenseNumbers = this.m_Garage.ShowLicenseNumbers(eVehicleCondition);
            }
            else
            {
                licenseNumbers = this.m_Garage.ShowLicenseNumbers();
            }

            foreach(String licenseNumber in licenseNumbers)
            {
                Console.WriteLine(licenseNumber);
            }

            if(licenseNumbers.Count == 0)
            {
                Console.WriteLine("There are no vehicles match.");
            }
        }

        private eVehicleCondition getVehicleConditionChoice()
        {
            bool   validChoice = false;
            int    userInput = 0;
            string vehicleConditionMessage = string.Format(@"
Vehicle Conditions:
1. In Repair.
2. Repaired.
3. Payed.");

            Console.WriteLine(vehicleConditionMessage);
            while (!validChoice)
            {
                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput >= 1 && userInput <= 3)
                    {
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Input needs to be between 1 to 3.");
                    }
                }
                else
                {
                    Console.WriteLine("Input needs to be a number.");
                }
            }

            return (eVehicleCondition)(Enum.Parse(typeof(eVehicleCondition), userInput.ToString()));
        }

        private void uiInsertVehicle()
        {
            Customer customer;
            Vehicle  vehicle = null;
            bool     isVehicleAlreadyExistsInGarage = false;

            customer = createCustomer();
            try
            {
                vehicle = createVehicle();
                isVehicleAlreadyExistsInGarage = this.m_Garage.InsertVehicle(vehicle.LicenseNumber, new CustomerVehiclePair(customer, vehicle));
                if(isVehicleAlreadyExistsInGarage)
                {
                    Console.WriteLine("Vehicle is already in garage, updating its condition to: In repair.");
                }
            }
            catch(FormatException fex)
            {
                Console.WriteLine("Parsing error!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Vehicle createVehicle()
        {
            VehicleFactory.eVehicleTypes vehicleType =  getVehicleTypeChoice();
            List<String>                 vehicleInfo = getVehicleInfo(vehicleType);

            return VehicleFactory.CreateVehicle(vehicleType, vehicleInfo);
        }

        private List<string> getVehicleInfo(VehicleFactory.eVehicleTypes i_VehicleType)
        {
            List<String> allVehicleInfo = new List<string>();

            Console.WriteLine("Please enter the following information:");
            foreach(String vehicleParam in VehicleFactory.GetAllFieldsNames(i_VehicleType))
            {
                Console.WriteLine(vehicleParam);
                allVehicleInfo.Add(Console.ReadLine());
            }

            return allVehicleInfo;
        }

        private VehicleFactory.eVehicleTypes getVehicleTypeChoice()
        {
            int          rowIndex = 1, userInput = 0;
            bool         validChoice = false;
            List<String> allVehicleTypes = VehicleFactory.GetAllVehicleTypes();

            Console.WriteLine("Vehicle Types:");
            foreach (string vehicleType in allVehicleTypes)
            {
                Console.WriteLine(rowIndex + ". " + vehicleType);
                rowIndex++;
            }

            while (!validChoice)
            {
                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput >= 1 && userInput <= allVehicleTypes.Count)
                    {
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Input needs to be between 1 to {0}.", allVehicleTypes.Count);
                    }
                }
                else
                {
                    Console.WriteLine("Input needs to be a number.");
                }
            }

            return (VehicleFactory.eVehicleTypes)(Enum.Parse(typeof(VehicleFactory.eVehicleTypes), userInput.ToString()));
        }

        private Customer createCustomer()
        {
            String customerName = null, customerPhoneNumber = null;
            bool   validName = false, validPhoneNumber = false;

            while(!validName)
            {
                Console.WriteLine("Enter your name:");
                customerName = Console.ReadLine();
                validName = customerName.All(char.IsLetter);
            }

            while(!validPhoneNumber)
            {
                Console.WriteLine("Enter your phone number:");
                customerPhoneNumber = Console.ReadLine();
                validPhoneNumber = customerPhoneNumber.All(char.IsDigit);
            }

            return new Customer(customerName, customerPhoneNumber);
        }

        private eMenuOptions getMenuChoice()
        {
            bool validChoice = false;
            int  userInput = 0;

            while(!validChoice)
            {
                if(int.TryParse(Console.ReadLine(), out userInput))
                {
                    if(userInput >= 1 && userInput <= 8)
                    {
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Input needs to be between 1 to 8.");
                    }
                }
                else
                {
                    Console.WriteLine("Input needs to be a number.");
                }
            }

            return (eMenuOptions)(Enum.Parse(typeof(eMenuOptions), userInput.ToString()));
        }

        private void printMenu()
        {
            string menuOptions = string.Format(@"
Menu
1. Insert Vehicle.
2. Show License Numbers.
3. Change Vehicle Condition.
4. Inflate Max Air Presure.
5. Fuel Vehicle.
6. Charge Vehicle.
7. Show All Vehicle Information.
8. Exit.");
            Console.WriteLine(menuOptions);
        }
    }
}
