using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Model;

namespace VehicleInsurance.Service
{
    public static class AgeService
    {
        /// <summary>
        /// Calculate the age of x.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="policyDate"></param>
        /// <returns></returns>
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
        /// Iterate through collection of drivers on policy,
        /// Order by age in descending order,
        /// Return youngest driver.
        /// </summary>
        /// <param name="drivers"></param>
        /// <returns></returns>
        public static Driver GetYoungestDriver(ObservableCollection<Driver> drivers)
        {
            Driver youngestDriver = drivers.OrderBy(d => d.DateOfBirth).ToList().Last();

            return youngestDriver;
        }


        /// <summary>
        /// Iterate through collection of drivers on policy,
        /// Order by age in descending order,
        /// Return oldest driver.
        /// </summary>
        /// <param name="drivers"></param>
        /// <returns></returns>
        public static Driver GetOldestDriver(ObservableCollection<Driver> drivers)
        {
            Driver oldestDriver = drivers.OrderBy(d => d.DateOfBirth).ToList().First();

            return oldestDriver;
        }
    }
}
