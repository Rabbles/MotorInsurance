using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsurance.Model
{
    public class Driver
    {
        public string Name { get; set; }
        public Occupation Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ObservableCollection<Claim> ClaimsAssociatedToDriver { get; set; }
    }
}
