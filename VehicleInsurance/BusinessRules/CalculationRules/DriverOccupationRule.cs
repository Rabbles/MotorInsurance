using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Enum;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;

namespace VehicleInsurance.BusinessRules.CalculationRules
{
  public class DriverOccupationRule : ICalculate
    {
        /// <summary>
        ///If there is driver who is a Chauffeur on the policy increase the premium by 10%.
        ///If there is driver who is an Accountant on the policy decrease the premium by 10%.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="premium"></param>
        /// <returns></returns>
        public double ImplementRule(Policy policy, double premium)
        {
            
            if (policy.DriversOnPolicy.Any(d => d.Occupation.JobTitle.Equals(OccupationEnum.Accountant)))
            {

                premium = premium - premium * 0.1;
            }

            
            else if (policy.DriversOnPolicy.Any(d => d.Occupation.JobTitle.Equals(OccupationEnum.Chauffeur)))
            {
                premium = premium + premium * 0.1;
            }

            
            else if (policy.DriversOnPolicy.Any(d => d.Occupation.JobTitle.Equals(OccupationEnum.Other)))
            {
                return premium;
            }

            return premium;
        }
    }
}
