using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Rules;

namespace VehicleInsurance.Interfaces
{
   public interface IDeclineFactory
   {
       DeclineRules CreateDeclineRules();
   }
}
