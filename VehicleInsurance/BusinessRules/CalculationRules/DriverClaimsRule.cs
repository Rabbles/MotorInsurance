using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.Service;

namespace VehicleInsurance.BusinessRules.CalculationRules
{
    public class DriverClaimsRule : ICalculate
    {
        /// <summary>
        ///For each claim within 1 year of the start date of the policy increase the premium by 20%.
        ///For each claim within 2 - 5 years of the start date of the policy increase the premium by 10%.
        ///
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="premium"></param>
        /// <returns></returns>
        public decimal ImplementRule(Policy policy, decimal premium)
        {
            foreach (var driver in policy.DriversOnPolicy)
            {
                foreach (var claim in driver.ClaimsAssociatedToDriver)
                {
                    int age = AgeService.GetAge(claim.DateOfClaim, policy.PolicyStartDate);

                    if (age <= 1)
                    {
                        premium = premium + premium * 0.2M;
                    }
                    else if (age >= 2 && age <= 5)
                    {
                        premium += premium * 0.1M;
                    }
                }
            }

            return premium;
        }
    }
}
