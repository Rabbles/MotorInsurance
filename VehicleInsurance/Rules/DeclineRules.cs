using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;

namespace VehicleInsurance.Rules
{
   public class DeclineRules
   {
       private List<IDecline> Rules;

       public DeclineRules(List<IDecline> rules)
       {
           this.Rules = rules;
       }

        /// <summary>
        /// Implements each rule and checks for a decline reason.
        /// Returns the result.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Result ImplementRules(Policy policy)
       {
           Result result = new Result(string.Empty, true);

           foreach (var rule in Rules)
           {
               result = rule.ImplementRule(policy);

               if (!result.IsSuccessful)
               {
                   return result;
               }
           }

           return result;
       }
   }
}
