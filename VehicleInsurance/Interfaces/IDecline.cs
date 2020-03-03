using VehicleInsurance.Model;

namespace VehicleInsurance.Interfaces
{
    public interface IDecline
    {
        Result ImplementRule(Policy policy);
    }
}
