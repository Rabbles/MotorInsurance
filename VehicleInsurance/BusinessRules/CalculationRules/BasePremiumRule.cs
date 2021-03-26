using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;

namespace VehicleInsurance.BusinessRules.CalculationRules
{
    public class BasePremiumRule : ICalculate
    {
        /// <summary>
        /// The starting point for the premium is £500.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="premium"></param>
        /// <returns></returns>
        public decimal ImplementRule(Policy policy, decimal premium)
        {
            return 500.00M;
        }
    }
}
