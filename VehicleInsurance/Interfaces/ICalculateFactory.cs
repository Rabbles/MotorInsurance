using VehicleInsurance.Rules;

namespace VehicleInsurance.Interfaces
{
    interface ICalculateFactory
    {
        CalculateRules CreateCalculationRules();
    }
}
