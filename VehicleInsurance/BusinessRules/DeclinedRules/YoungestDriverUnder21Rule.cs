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
   public class YoungestDriverUnder21Rule : IDecline
    {
        /// <summary>
        /// If the youngest driver is under the age of 21 at the start date of the policy decline with a message 
        /// "Age of Youngest Driver" and append the name of the driver.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Result ImplementRule(Policy policy)
        {
            //get youngest driver and age
            var youngestDriverOnPolicy = AgeService.GetYoungestDriver(policy.DriversOnPolicy);
            var age = AgeService.GetAge(youngestDriverOnPolicy.DateOfBirth, policy.PolicyStartDate);

            if (age >= 21)
            {
               return new Result(string.Empty, true);
            }
            else
            {
                return new Result("Age of Youngest Driver - " + youngestDriverOnPolicy.Name, false);
            }
        }
    }
}
