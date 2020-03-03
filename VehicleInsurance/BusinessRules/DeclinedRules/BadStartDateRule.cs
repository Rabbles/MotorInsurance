using System;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;

namespace VehicleInsurance.BusinessRules.DeclinedRules
{
    public class BadStartDateRule : IDecline
    {
        /// <summary>
        /// If the start date of the policy is before today decline with the message "Start Date of Policy".
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public Result ImplementRule(Policy policy)
        {
            var todaysDate = DateTime.Today;
            var policyStartDate = policy.PolicyStartDate;

            if (policyStartDate < todaysDate)
            {
                Result result = new Result("Start Date of Policy", false);

                return result;
            }

            return new Result(string.Empty, true);


        }
    }
}
