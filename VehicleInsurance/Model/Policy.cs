using System;
using System.Collections.ObjectModel;

namespace VehicleInsurance.Model
{
    public class Policy
    {
        public double Premium { get; set; }

        public ObservableCollection<Driver> DriversOnPolicy { get; set; }

        public DateTime PolicyStartDate { get; set; }
    }
}
