using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.Service;

namespace VehicleInsurance.BusinessRules.DeclinedRules
{
    public class OldestDriverOverSeventyFiveRule : IDecline
    {
        private const int MaxAge = 75;

        /// <summary>
        /// Implements the oldest driver over 75 rule.
        /// </summary>
        /// <param name="policy"></param>
        public Result ImplementRule(Policy policy)
        {
            var oldestDriverOnPolicy = AgeService.GetOldestDriver(policy.DriversOnPolicy);
            var age = AgeService.GetAge(oldestDriverOnPolicy.DateOfBirth, policy.PolicyStartDate);

            if (age > MaxAge)
            {
                return new Result($"Age of Oldest Driver {oldestDriverOnPolicy.Name}", false);
            }
            else
            {
                return new Result(string.Empty, true);
            }
        }
    }
}
