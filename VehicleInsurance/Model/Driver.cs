using System;
using System.Collections.ObjectModel;

namespace VehicleInsurance.Model
{
    public class Driver : IDriver
    {
        public string Name { get; set; }
        public Occupation Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ObservableCollection<Claim> ClaimsAssociatedToDriver { get; set; }
    }
}
