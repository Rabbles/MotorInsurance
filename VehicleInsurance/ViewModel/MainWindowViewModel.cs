using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// List of available occupations.
        /// </summary>
        public List<string> OccupationList { get; set; }

        public string SelectedOccupation { get; set; }

        /// <summary>
        /// Start date of Policy
        /// </summary>
        public DateTime PolicyStartDate
        {
            get { return _policy.PolicyStartDate; }
            set
            {
                _policy.PolicyStartDate = value;
            }
        }

        /// <summary>
        /// Drivers on the Policy
        /// </summary>
        public ObservableCollection<Driver> Drivers
        {
            get { return _policy.DriversOnPolicy; }
            set
            {
                _policy.DriversOnPolicy = value;
            }
        }

        /// <summary>
        /// Selected Driver
        /// </summary>
        public Driver SelectedDriver { get; set; }

        /// <summary>
        /// Selected Claim
        /// </summary>
        public Claim SelectedClaim { get; set; }

        /// <summary>
        /// Claims associated to driver.
        /// </summary>
        public ObservableCollection<Claim> DriverClaims
        {
            get { return SelectedDriver.ClaimsAssociatedToDriver; }
            set
            {
                SelectedDriver.ClaimsAssociatedToDriver = value;
            }
        }

        /// <summary>
        /// Notification message for user.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Final calculated premium.
        /// </summary>
        public decimal FinalPremium { get; set; }


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
        /// Helper method to initialise User Interface state.
        /// </summary>
        private void InitialiseUI()
        {
            _policy = new Policy()
            {
                PolicyStartDate = DateTime.Today,
                Premium = 0.0m
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
            if (string.IsNullOrWhiteSpace(DriverName))
            {
                Status = "Please check and ammend Driver Name";
                Logger.Error("No Driver Name entered.");
                return;
            }

            if (Drivers.Count > 5)
            {
                Status = "Policy cannot contain more than five drivers";
                Logger.Info("Info: User attempted to have more than five drivers on policy.");
                return;
            }

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

            Driver driver = new Driver
            {
                Name = DriverName,
                DateOfBirth = DriverDateOfBirth,
                Occupation = job,
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            Drivers.Add(driver);
            Status = DriverName + " added to policy";
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

     
