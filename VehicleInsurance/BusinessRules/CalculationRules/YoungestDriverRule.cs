using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.Service;

namespace VehicleInsurance.BusinessRules.CalculationRules
{
    public class YoungestDriverRule : ICalculate
    {
        /// <summary>
        ///If the youngest driver is aged between 21 and 25 at the start date of the policy increase the premium by 20%.
        ///If the youngest driver is aged between 26 and 75 at the start date of the policy decrease the premium by 10%.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="premium"></param>
        /// <returns></returns>
        public decimal ImplementRule(Policy policy, decimal premium)
        {
            var youngestDriver = AgeService.GetYoungestDriver(policy.DriversOnPolicy);
            var youngestDriverAge = AgeService.GetAge(youngestDriver.DateOfBirth, policy.PolicyStartDate);


            if (youngestDriverAge >= 21 && youngestDriverAge <= 25)
            {
                premium += premium * 0.2m; 
            }
            else if (youngestDriverAge >= 26 && youngestDriverAge <= 75)
            {
                premium += premium * 0.1m; 
            }

            return premium;
        }
    }
}
