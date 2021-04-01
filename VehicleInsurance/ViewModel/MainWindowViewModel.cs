using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VehicleInsurance.Enum;
using VehicleInsurance.Factories;
using VehicleInsurance.Interfaces;
using VehicleInsurance.Model;
using VehicleInsurance.Properties;
using Claim = VehicleInsurance.Model.Claim;

namespace VehicleInsurance.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
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
            get => _policy.PolicyStartDate;
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
            get => _policy.DriversOnPolicy;
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
            get => SelectedDriver.ClaimsAssociatedToDriver;
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
        /// Add claim to a selected driver. Checks a driver has less than five claims.
        /// Creates a claim and displays the status of the claim.
        /// </summary>
        public void AddClaim()
        {
            if (!Drivers.Contains(SelectedDriver))
            {
                Status = Resources.SelectADriverToAssociateToClaim;
                return;
            }

            if (DriverClaims.Count > 5)
            {
                Status = Resources.DriverCannotExceedFiveClaims;
                return;
            }

            var claim = new Claim
            {
                DateOfClaim = DateOfClaim
            };

            DriverClaims.Add(claim);
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
                Status = "Please check and ammend driver name.";
                Logger.Info("No driver name entered.");
                return;
            }

            if (Drivers.Any(d => d.Name == DriverName))
            {
                Status = "Driver is already included on claim.";
                return;
            }

            if (Drivers.Count > 5)
            {
                Status = "Policy cannot contain more than five drivers.";
                Logger.Info("User attempted to have more than five drivers on policy.");
                return;
            }

            OccupationEnum jobEnum;

            switch (SelectedOccupation)
            {
                case "Chauffeur":
                    jobEnum = OccupationEnum.Chauffeur;
                    break;
                case "Accountant":
                    jobEnum = OccupationEnum.Accountant;
                    break;
                default:
                    jobEnum = OccupationEnum.Other;
                    break;
            }

            var job = new Occupation()
            {
                JobTitle = jobEnum
            };

            var driver = new Driver
            {
                Name = DriverName,
                DateOfBirth = DriverDateOfBirth,
                Occupation = job,
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            Drivers.Add(driver);
            Status = $"{DriverName} added to policy";
        }

        /// <summary>
        /// Calculates the premium based on the calculate and decline sets of rules.
        /// Returns a message based on the decision.
        /// </summary>
        public void CalculatePremium()
        {
            IDeclineFactory declineFactory = new DeclineRulesFactory();
            var declineRules = declineFactory.CreateDeclineRules();
            var result = declineRules.ImplementRules(_policy);

            if (result.IsSuccessful)
            {
                ICalculateFactory calculateFactory = new CalculateRulesFactory();
                var calculateRules = calculateFactory.CreateCalculationRules();

                FinalPremium = calculateRules.ImplementRules(_policy);
                Status = Resources.PremiumUpdatedSuccessfully;
            }
            else
            {
                Status = result.Message;
            }
        }

        private void InitialiseDelegates()
        {
            AddDriverCommand = new DelegateCommand(AddDriver);
            AddClaimCommand = new DelegateCommand(AddClaim);
            CalculatePremiumCommand = new DelegateCommand(CalculatePremium);
        }
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
            DriverClaims = new ObservableCollection<Claim>();
            DriverDateOfBirth = DateTime.Now;
            DateOfClaim = DateTime.Now;
        }
    }
}

     
