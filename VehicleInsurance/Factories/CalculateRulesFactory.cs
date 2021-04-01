using System.Collections.Generic;
using VehicleInsurance.BusinessRules.CalculationRules;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Rules;

namespace VehicleInsurance.Factories
{
    public class CalculateRulesFactory : ICalculateFactory
    {
        /// <summary>
        /// Factory method which returns a list of calculation rules.
        /// </summary>
        /// <returns></returns>
        public CalculateRules CreateCalculationRules()
        {
            List<ICalculate> rules = new List<ICalculate>()
            {
                new BasePremiumRule(),
                new DriverOccupationRule(),
                new YoungestDriverRule(),
                new DriverClaimsRule()
            };

            return new CalculateRules(rules);
        }
    }
}
