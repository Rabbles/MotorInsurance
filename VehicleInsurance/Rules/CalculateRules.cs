using System;
using System.Collections.Generic;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;

namespace VehicleInsurance.Rules
{
    public class CalculateRules
   {
       private readonly List<ICalculate> Rules;

       public CalculateRules(List<ICalculate> rules)
       {
            Rules = rules;
       }

        /// <summary>
        /// Implements each rule and calculates the premium.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns>premium</returns>
       public decimal ImplementRules(Policy policy)
       {
           decimal premium = 0.0m;

           foreach (var rule in Rules)
           {
               premium = rule.ImplementRule(policy, premium);
           }

           return Math.Round(premium, 0);
       }
   }
}
