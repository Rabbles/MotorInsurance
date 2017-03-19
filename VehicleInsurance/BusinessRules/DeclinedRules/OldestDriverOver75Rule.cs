using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.Service;

namespace VehicleInsurance.BusinessRules.DeclinedRules
{
   public class OldestDriverOver75Rule : IDecline
    {
        /// <summary>
        /// If the oldest driver is over the age of 75 at the start date of the policy decline with a message 
        /// "Age of Oldest Driver" and append the name of the driver.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Result ImplementRule(Policy policy)
        {
            var oldestDriverOnPolicy = AgeService.GetOldestDriver(policy.DriversOnPolicy);
            var age = AgeService.GetAge(oldestDriverOnPolicy.DateOfBirth, policy.PolicyStartDate);

            if (age > 75)
            {
                return new Result("Age of Oldest Driver " + oldestDriverOnPolicy.Name, false);
            }
            else
            {
                return new Result(string.Empty, true);
            }
        }
    }
}
