using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;

namespace VehicleInsurance.BusinessRules.DeclinedRules
{
    public class TotalClaimsExceedsThreeRule : IDecline
    {
        /// <summary>
        /// If the total number of claims exceeds 3 then 
        /// decline with a message "Policy has more than 3 claims".
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Result ImplementRule(Policy policy)
        {
            //count claims on policy
            int claimsOnPolicy = policy.DriversOnPolicy.SelectMany(d => d.ClaimsAssociatedToDriver).Count();

            if (claimsOnPolicy <= 3)
            {
                return new Result(string.Empty, true);
            }
            else
            {
                return new Result("Policy has more than 3 claims.", false);
            }
        }
    }
}
