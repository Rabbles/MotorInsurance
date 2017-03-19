using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.BusinessRules.CalculationRules;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.Service;

namespace VehicleInsurance.BusinessRules.DeclinedRules
{
    
    public class DriverHasMoreThanTwoClaimsRule : IDecline
    {
        /// <summary>
        /// If a driver has more than 2 claims decline with a message 
        /// "Driver has more than 2 claims" – include the name of the driver.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Result ImplementRule(Policy policy)
        {
            if (policy.DriversOnPolicy.Any(d => d.ClaimsAssociatedToDriver.Count > 2))
            {
                Driver driver = policy.DriversOnPolicy.Where(d => d.ClaimsAssociatedToDriver.Count > 2).First();

                return new Result(driver.Name + " has more than 2 claims", false);
            }
            else
            {
                return new Result(String.Empty, true);
            }

        }
    }
}
