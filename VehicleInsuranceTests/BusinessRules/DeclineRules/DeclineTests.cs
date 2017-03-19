using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleInsurance.BusinessRules.DeclinedRules;
using VehicleInsurance.Model;

namespace VehicleInsuranceTests.BusinessRules.DeclineRules
{
    [TestClass()]
    public class DeclineTests
    {
        private Policy _policy;
        private Driver _driver;
        private Claim _claim;


        [TestInitialize]
        public void Initialise()
        {
            _policy = new Policy()
            {
                PolicyStartDate = new DateTime(),
                DriversOnPolicy = new ObservableCollection<Driver>()
            };

            _driver = new Driver()
            {
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>(),
                DateOfBirth = new DateTime(),
                Occupation = new Occupation()
            };

            _claim = new Claim()
            {
                DateOfClaim = new DateTime()
            };
        }

        [TestMethod]
        //check to test date before today fails
        public void ImplementRuleTestBadStartDatePast()
        {
            //Assemble
            Policy policy = _policy;
            policy.PolicyStartDate = DateTime.Today.AddDays(-1);
            BadStartDateRule badStartDate = new BadStartDateRule();

            //Act
            bool expectedResult = false;
            bool actualResult = badStartDate.ImplementRule(policy).IsSuccessful;
            
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        //check to test todays date passes
        public void ImplementRuleTestBadStartDateNow()
        {
            //Assemble
            Policy policy = _policy;
            policy.PolicyStartDate = DateTime.Today;
            BadStartDateRule badStartDate = new BadStartDateRule();

            //Act
            bool expectedResult = true;
            bool actualResult = badStartDate.ImplementRule(policy).IsSuccessful;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        //test if driver is 21 or over
        public void ImplementRuleTestYoungestDriverIs21()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1996, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            YoungestDriverUnder21Rule driverUnder21 = new YoungestDriverUnder21Rule();

            //Act
            bool expectedResult = true;
            bool actualResult = driverUnder21.ImplementRule(policy).IsSuccessful;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ImplementRuleTestYoungestDriverIsUnder21()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1997, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            YoungestDriverUnder21Rule driverUnder21 = new YoungestDriverUnder21Rule();

            //Act
            bool expectedResult = false;
            bool actualResult = driverUnder21.ImplementRule(policy).IsSuccessful;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ImplementRuleTestOldestDriverIs75()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1942, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            OldestDriverOver75Rule over75Rule = new OldestDriverOver75Rule();

            //Act
            bool expectedResult = true;
            bool actualResult = over75Rule.ImplementRule(policy).IsSuccessful;


            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        //One year boundary check
        public void ImplementRuleTestOldestDriverIsOver75()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1941, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            OldestDriverOver75Rule over75Rule = new OldestDriverOver75Rule();

            //Act
            bool expectedResult = false;
            bool actualResult = over75Rule.ImplementRule(policy).IsSuccessful;

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void ImplementRuleTestExceedTwoClaimsPerDriver()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            Claim claim = _claim;
            int maxClaims = 2;

            //exceed allowable claims
            for (int i = 0; i < maxClaims + 1; i++)
            {
                driver.ClaimsAssociatedToDriver.Add(claim);
            }

            policy.DriversOnPolicy.Add(driver);

            DriverHasMoreThanTwoClaimsRule driverMoreThanTwoClaims = new DriverHasMoreThanTwoClaimsRule();
           
           
            //Act
            bool expectedResult = false;
            bool actualResult = driverMoreThanTwoClaims.ImplementRule(policy).IsSuccessful;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ImplementTestRuleTotalClaimsIsThree()
        {
            //Assemble
            Policy policy = _policy;
            Claim claim = new Claim();

            //Only three drivers required
            Driver driverA = new Driver()
            {
                DateOfBirth = new DateTime(),
                Occupation = new Occupation(),
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            Driver driverB = new Driver()
            {
                DateOfBirth = new DateTime(),
                Occupation = new Occupation(),
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            Driver driverC = new Driver()
            {
                DateOfBirth = new DateTime(),
                Occupation = new Occupation(),
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            driverA.ClaimsAssociatedToDriver.Add(claim);
            driverB.ClaimsAssociatedToDriver.Add(claim);
            driverC.ClaimsAssociatedToDriver.Add(claim);

            policy.DriversOnPolicy.Add(driverA);
            policy.DriversOnPolicy.Add(driverB);
            policy.DriversOnPolicy.Add(driverC);

            TotalClaimsExceedsThreeRule exceedsThree = new TotalClaimsExceedsThreeRule();

            //Act
            bool expectedResult = true;
            bool actualResult = exceedsThree.ImplementRule(policy).IsSuccessful;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        //Same as above but add an additional claim to driverA
        public void ImplementTestRuleTotalClaimsExceedsThree()
        {
            //Assemble
            Policy policy = _policy;
            Claim claim = new Claim();

            //Only three drivers required
            Driver driverA = new Driver()
            {
                DateOfBirth = new DateTime(),
                Occupation = new Occupation(),
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            Driver driverB = new Driver()
            {
                DateOfBirth = new DateTime(),
                Occupation = new Occupation(),
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            Driver driverC = new Driver()
            {
                DateOfBirth = new DateTime(),
                Occupation = new Occupation(),
                ClaimsAssociatedToDriver = new ObservableCollection<Claim>()
            };

            driverA.ClaimsAssociatedToDriver.Add(claim);
            driverB.ClaimsAssociatedToDriver.Add(claim);
            driverC.ClaimsAssociatedToDriver.Add(claim);

            policy.DriversOnPolicy.Add(driverA);
            policy.DriversOnPolicy.Add(driverB);
            policy.DriversOnPolicy.Add(driverC);

            //create excessive claim
            driverA.ClaimsAssociatedToDriver.Add(claim);

            TotalClaimsExceedsThreeRule exceedsThree = new TotalClaimsExceedsThreeRule();

            //Act
            bool expectedResult = false;
            bool actualResult = exceedsThree.ImplementRule(policy).IsSuccessful;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


    }
    }

