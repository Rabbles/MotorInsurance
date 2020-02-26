using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using VehicleInsurance.Enum;
using VehicleInsurance.Factories;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.Rules;
using Claim = VehicleInsurance.Model.Claim;

namespace VehicleInsurance.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        //private properties
        private Policy _policy;


        //Public Properties
        public string DriverName { get; set; }
        public DateTime DriverDateOfBirth { get; set; }
        public DateTime DateOfClaim { get; set; }


        //Delegate Commands
        public DelegateCommand AddDriverCommand { get; internal set; }
        public DelegateCommand AddClaimCommand { get; internal set; }
        public DelegateCommand CalculatePremiumCommand { get; internal set; }


        /// <summary>
        /// Binds to Occupation ComboBox.
        /// </summary>
        public List<string> OccupationList { get; set; }

        private string _selectedOccupation;

        public string SelectedOccupation
        {
            get { return _selectedOccupation; }
            set
            {
                _selectedOccupation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Binds to Policy Start Date.
        /// </summary>
        public DateTime PolicyStartDate
        {
            get { return _policy.PolicyStartDate; }
            set
            {
                _policy.PolicyStartDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Binds to Driver details Listview.
        /// </summary>
        public ObservableCollection<Driver> Drivers
        {
            get { return _policy.DriversOnPolicy; }
            set
            {
                _policy.DriversOnPolicy = value;
                OnPropertyChanged();
            }
        }

        private Driver _selectedDriver;

        /// <summary>
        /// Binds to selected driver in Driver details Listview.
        /// </summary>
        public Driver SelectedDriver
        {
            get { return _selectedDriver; }
            set
            {
                _selectedDriver = value;
                OnPropertyChanged();
            }
        }

        private Claim _selectedClaim;

        /// <summary>
        /// Binds to selected claim in Claim details Listview.
        /// </summary>
        public Claim SelectedClaim
        {
            get { return _selectedClaim; }
            set
            {
                _selectedClaim = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Claims associated to driver.
        /// </summary>
        public ObservableCollection<Claim> DriverClaims
        {
            get { return SelectedDriver.ClaimsAssociatedToDriver; }
            set
            {
                SelectedDriver.ClaimsAssociatedToDriver = value;
                OnPropertyChanged();
            }
        }

        private string _status;

        /// <summary>
        /// Notification message for user.
        /// </summary>
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Final calculated premium.
        /// </summary>
        public double FinalPremium
        {
            get { return _policy.Premium; }
            set
            {
                _policy.Premium = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            InitialiseDelegates();
            InitialiseUI();
        }

        /// <summary>
        /// Helper method to initialise Delegate Commands in a logical grouping.
        /// </summary>
        private void InitialiseDelegates()
        {
            AddDriverCommand = new DelegateCommand(AddDriver);
            AddClaimCommand = new DelegateCommand(AddClaim);
            CalculatePremiumCommand = new DelegateCommand(CalculatePremium);
        }

        /// <summary>
        /// Helper method to initialise User Interace.
        /// </summary>
        private void InitialiseUI()
        {
            _policy = new Policy()
            {
                PolicyStartDate = DateTime.Today,
                Premium = 0.0
            };

            OccupationList = new List<string>()
            {
                "Other",
                "Accountant",
                "Chauffeur"
            };
            SelectedDriver = new Driver();
            Drivers = new ObservableCollection<Driver>();
            DriverClaims = new ObservableCollection<Model.Claim>();
            DriverDateOfBirth = DateTime.Now;
            DateOfClaim = DateTime.Now;
        }



        /// <summary>
        /// Add claim to a selected driver. Checks a driver has less than five claims.
        /// Creates a claim and displays the status of the claim.
        /// </summary>
        public void AddClaim()
        {
            try
            {
                if (Drivers.Contains(SelectedDriver))
                {
                    if (DriverClaims.Count < 5)
                    {
                        Claim claim = new Claim
                        {
                            DateOfClaim = DateOfClaim
                        };

                        DriverClaims.Add(claim);
                    }
                    else
                    {
                        Status = "Driver cannot exceed five claims.";
                    }
                }
                else
                {
                    Status = "Please select a driver to associate claim.";
                }
            }
            catch (Exception e)
            {
                Status = "An error has occurred. Please ensure the correct information has been entered on the system.\n" + e.Message;
                Logger.Error("Error: " + e);
            }
        }

        /// <summary>
        /// Checks the policy has less than five drivers.
        /// Checks and sets driver occupation.
        /// Adds a driver to the policy.
        /// Or returns a message if not added.
        /// </summary>
        private void AddDriver()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(DriverName))
                {
                    if (Drivers.Count < 5)
                    {

                        Driver driver = new Driver();
                        driver.Name = DriverName;
                        driver.DateOfBirth = DriverDateOfBirth;
                        driver.ClaimsAssociatedToDriver = new ObservableCollection<Claim>();
                        OccupationEnum jobEnum;

                        if (SelectedOccupation == "Chauffeur")
                        {
                            jobEnum = OccupationEnum.Chauffeur;
                        }
                        else if (SelectedOccupation == "Accountant")
                        {
                            jobEnum = OccupationEnum.Accountant;
                        }
                        else
                        {
                            jobEnum = OccupationEnum.Other;
                        }


                        Occupation job = new Occupation()
                        {
                            JobTitle = jobEnum
                        };

                        driver.Occupation = job;

                        Drivers.Add(driver);
                        Status = DriverName + " added to policy";
                    }
                    else
                    {
                        Status = "Policy cannot contain more than five drivers";
                        Logger.Info("Info: User attempted to have more than five drivers on policy.");
                    }
                }
                else
                {
                    Status = "Please enter name and occupation of driver.";
                }

            }
            catch (Exception e)
            {

                Status = "Please check and ammend incorrect entries.";
                Logger.Error("Error: " + e);
            }
        }

        /// <summary>
        /// Calculates the premium based on the calculate and decline sets of rules.
        /// Returns a message based on the decision.
        /// </summary>
        public void CalculatePremium()
        {
            try
            {
                //if (!string.IsNullOrWhiteSpace(DriverName))
                //{
                    IDeclineFactory declineFactory = new DeclineRulesFactory();
                    DeclineRules declineRules = declineFactory.CreateDeclineRules();
                    Result result = declineRules.ImplementRules(_policy);

                    if (result.IsSuccessful)
                    {
                        ICalculateFactory calculateFactory = new CalculateRulesFactory();
                        CalculateRules calculateRules = calculateFactory.CreateCalculationRules();


                        FinalPremium = calculateRules.ImplementRules(_policy);
                        Status = "Premium updated successfully.";
                    }
                    else
                    {
                        Status = result.Message;
                    }
                //}
            }
            catch (Exception e)
            {

                Status = "Please ensure all fields are completed.";
                Logger.Error("Error: " + e.Message);
            }
         }
    }
}
