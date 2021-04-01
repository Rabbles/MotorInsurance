using System;
using System.Collections.ObjectModel;
using System.Linq;
using VehicleInsurance.Model;

namespace VehicleInsurance.Service
{
    public static class AgeService
    {
        /// <summary>
        /// Calculate the age of a driver.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="policyDate"></param>
        public static int GetAge(DateTime date, DateTime policyDate)
        {
            int driverAge = policyDate.Year - date.Year;

            if ((date.Month > policyDate.Month) || (date.Month == policyDate.Month && date.Day > policyDate.Day))
            {
                driverAge--;
            }

            return driverAge;
        }

        /// <summary>
        /// Get the youngest driver from available drivers.
        /// </summary>
        /// <param name="drivers"></param>
        public static Driver GetYoungestDriver(ObservableCollection<Driver> drivers)
        {
            return drivers.OrderBy(d => d.DateOfBirth).ToList().Last();
        }

        /// <summary>
        /// Get the eldest driver from available drivers.
        /// </summary>
        /// <param name="drivers"></param>
        public static Driver GetOldestDriver(ObservableCollection<Driver> drivers)
        {
            return drivers.OrderBy(d => d.DateOfBirth).ToList().First();
        }
    }
}
