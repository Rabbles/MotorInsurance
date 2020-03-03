using System.Collections.Generic;
using VehicleInsurance.BusinessRules.DeclinedRules;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Rules;

namespace VehicleInsurance.Factories
{
    public class DeclineRulesFactory : IDeclineFactory
    {
        /// <summary>
        /// Factory method which returns
        /// a list of decline rules.
        /// </summary>
        /// <returns></returns>
        public DeclineRules CreateDeclineRules()
        {
            List<IDecline> rules = new List<IDecline>()
            {
                new BadStartDateRule(),
                new YoungestDriverUnder21Rule(),
                new OldestDriverOver75Rule(),
                new DriverHasMoreThanTwoClaimsRule(),
                new TotalClaimsExceedsThreeRule()
            };
            return new DeclineRules(rules);
        }
    }
}
