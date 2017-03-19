using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Model;

namespace VehicleInsurance.Interfaces
{
   public interface IDecline
    {
        Result ImplementRule(Policy policy);
    }
}
