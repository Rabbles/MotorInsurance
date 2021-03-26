using VehicleInsurance.Model;

namespace VehicleInsurance.Interfaces
{
   public interface ICalculate
   {
      decimal ImplementRule(Policy policy, decimal premium);
   }
}
