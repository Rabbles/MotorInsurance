using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Model;

namespace VehicleInsurance.Interfaces
{
   public interface ICalculate
   {
      double ImplementRule(Policy policy, double premium);
   }
}
