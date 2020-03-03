using VehicleInsurance.Model;

namespace VehicleInsurance.Interfaces
{
   public interface ICalculate
   {
      double ImplementRule(Policy policy, double premium);
   }
}
