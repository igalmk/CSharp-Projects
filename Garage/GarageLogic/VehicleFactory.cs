using System;
using System.Collections.Generic;
using System.Reflection;
using Ex03.GarageLogic.MotorCycles;

namespace Ex03.GarageLogic
{
    public abstract class VehicleFactory
    {
        public enum eVehicleTypes
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorCycle,
            ElectricMotorCycle,
            FuelTruck,
        }

        public static List<string> GetAllVehicleTypes()
        {
            List<string> allVehicleTypes = new List<string>();

            allVehicleTypes.Add("Fuel Car");
            allVehicleTypes.Add("Electric Car");
            allVehicleTypes.Add("Fuel MotorCycle");
            allVehicleTypes.Add("Electric MotorCycle");
            allVehicleTypes.Add("Fuel Truck");

            return allVehicleTypes;
        }

        public static List<string> GetAllFieldsNames(eVehicleTypes i_ChosenVehicle)
        {
            List<string> allFieldsNames = new List<string>();

            switch(i_ChosenVehicle)
            {
                case eVehicleTypes.FuelCar:
                    {
                        allFieldsNames = getAllFieldsNamesFromCtor(typeof(FuelCar));
                        break;
                    }

                case eVehicleTypes.ElectricCar:
                    {
                        allFieldsNames = getAllFieldsNamesFromCtor(typeof(ElectricCar));
                        break;
                    }

                case eVehicleTypes.FuelMotorCycle:
                    {
                        allFieldsNames = getAllFieldsNamesFromCtor(typeof(FuelMotorCycle));
                        break;
                    }

                case eVehicleTypes.ElectricMotorCycle:
                    {
                        allFieldsNames = getAllFieldsNamesFromCtor(typeof(ElectricMotorCycle));
                        break;
                    }

                case eVehicleTypes.FuelTruck:
                    {
                        allFieldsNames = getAllFieldsNamesFromCtor(typeof(FuelTruck));
                        break;
                    }
            }

            return allFieldsNames;
        }

        private static List<string> getAllFieldsNamesFromCtor(Type i_VehicleType)
        {
            List<string>      allFieldsNamesFromCtor = new List<string>();
            string            temporaryParameterName = null;
            string            finalParameterName = null;
            ConstructorInfo[] constructorsInfos = i_VehicleType.GetConstructors();
            ParameterInfo[]   parameterInfos = constructorsInfos[0].GetParameters();

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                temporaryParameterName = parameterInfo.Name.Substring(2);
                finalParameterName = temporaryParameterName[0].ToString();
                for (int i = 1, j = 1; i < temporaryParameterName.Length; i++)
                {
                    if (temporaryParameterName[i] >= 'A' && temporaryParameterName[i] <= 'Z')
                    {
                        finalParameterName = finalParameterName + " ";
                    }

                    finalParameterName = finalParameterName + temporaryParameterName[j].ToString();
                    j++;
                }

                allFieldsNamesFromCtor.Add(finalParameterName);
            }

            return allFieldsNamesFromCtor;
        }

        /// <summary>
        /// Throws FormatException!!!
        /// Throw ArgumentException!!!
        /// </summary>
        public static Vehicle CreateVehicle(eVehicleTypes i_ChosenVehicle, List<string> i_VehicleInfo)
        {
            Vehicle      newVehicle = null;
            List<object> allNewVehicleMembers;

            switch (i_ChosenVehicle)
            {
                case eVehicleTypes.FuelCar:
                    {
                        allNewVehicleMembers = FuelCar.ConvertStringsToCtorParameters(i_VehicleInfo);
                        newVehicle = new FuelCar((string)allNewVehicleMembers[0], (string)allNewVehicleMembers[1], (string)allNewVehicleMembers[2],
                            (float)allNewVehicleMembers[3], (eVehicleColor)allNewVehicleMembers[4], (uint)allNewVehicleMembers[5], (float)allNewVehicleMembers[6]);
                        break;
                    }

                case eVehicleTypes.ElectricCar:
                    {
                        allNewVehicleMembers = ElectricCar.ConvertStringsToCtorParameters(i_VehicleInfo);
                        newVehicle = new ElectricCar((string)allNewVehicleMembers[0], (string)allNewVehicleMembers[1], (string)allNewVehicleMembers[2],
                            (float)allNewVehicleMembers[3], (eVehicleColor)allNewVehicleMembers[4], (uint)allNewVehicleMembers[5], (float)allNewVehicleMembers[6]);
                        break;
                    }

                case eVehicleTypes.FuelMotorCycle:
                    {
                        allNewVehicleMembers = FuelMotorCycle.ConvertStringsToCtorParameters(i_VehicleInfo);
                        newVehicle = new FuelMotorCycle((string)allNewVehicleMembers[0], (string)allNewVehicleMembers[1], (string)allNewVehicleMembers[2],
                                     (float)allNewVehicleMembers[3], (eLicenseType)allNewVehicleMembers[4], (int)allNewVehicleMembers[5], (float)allNewVehicleMembers[6]);
                        break;
                    }

                case eVehicleTypes.ElectricMotorCycle:
                    {
                        allNewVehicleMembers = ElectricMotorCycle.ConvertStringsToCtorParameters(i_VehicleInfo);
                        newVehicle = new ElectricMotorCycle((string)allNewVehicleMembers[0], (string)allNewVehicleMembers[1], (string)allNewVehicleMembers[2],
                                     (float)allNewVehicleMembers[3], (eLicenseType)allNewVehicleMembers[4], (int)allNewVehicleMembers[5], (float)allNewVehicleMembers[6]);
                        break;
                    }

                case eVehicleTypes.FuelTruck:
                    {
                        allNewVehicleMembers = FuelTruck.ConvertStringsToCtorParameters(i_VehicleInfo);
                        newVehicle = new FuelTruck((string)allNewVehicleMembers[0], (string)allNewVehicleMembers[1], (string)allNewVehicleMembers[2],
                            (float)allNewVehicleMembers[3], (bool)allNewVehicleMembers[4], (float)allNewVehicleMembers[5], (float)allNewVehicleMembers[6]);
                        break;
                    }
            }

            return newVehicle;
        }
    }
}
