using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsurance.Model
{
    public class Policy
    {
        public double Premium { get; set; }

        public ObservableCollection<Driver> DriversOnPolicy { get; set; }

        public DateTime PolicyStartDate { get; set; }
    }
}
