using VehicleInsurance.Rules;

namespace VehicleInsurance.Interfaces
{
    public interface IDeclineFactory
   {
       DeclineRules CreateDeclineRules();
   }
}
