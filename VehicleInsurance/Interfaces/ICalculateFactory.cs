using VehicleInsurance.Rules;

namespace VehicleInsurance.Interfaces
{
    public interface ICalculateFactory
    {
        CalculateRules CreateCalculationRules();
    }
}
