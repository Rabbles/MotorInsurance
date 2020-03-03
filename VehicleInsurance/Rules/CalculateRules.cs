using System.Collections.Generic;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;

namespace VehicleInsurance.Rules
{
    public class CalculateRules
   {
       private List<ICalculate> Rules;

        //constructor
       public CalculateRules(List<ICalculate> rules)
       {
           this.Rules = rules;
       }

        /// <summary>
        /// Implements each rule and calculates in order.
        /// Returns the final premium.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
       public double ImplementRules(Policy policy)
       {
           double premium = 0.0;

           foreach (var rule in Rules)
           {
               premium = rule.ImplementRule(policy, premium);
           }

           return premium;
       }
   }
}
