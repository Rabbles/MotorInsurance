using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Factories;
using VehicleInsurance.Rules;

namespace VehicleInsurance.Interfaces
{
    interface ICalculateFactory
    {
        CalculateRules CreateCalculationRules();
    }
}
