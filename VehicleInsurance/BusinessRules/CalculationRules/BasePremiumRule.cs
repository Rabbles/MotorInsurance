using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public double ImplementRule(Policy policy, double premium)
        {
            return 500.00;
        }
    }
}
