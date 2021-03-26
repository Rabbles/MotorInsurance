using VehicleInsurance.BusinessRules.CalculationRules;
using System;
using System.Collections.ObjectModel;
using VehicleInsurance.Enum;
using VehicleInsurance.Model;
using NUnit.Framework;

namespace VehicleInsuranceTests.BusinessRules.CalculationRules
{
    [TestFixture]
    public class CalcualtionTests
    {
        private Policy _policy;
        private Driver _driver;
        private BasePremiumRule _basePremiumRule;

        [SetUp]
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

            _basePremiumRule = new BasePremiumRule();
        }

        [TestCase (500)]
        public void ImplementRuleTestBasePremiumIs500(decimal input)
        {
            //Assemble
            decimal premium = 0;
            var rule = _basePremiumRule;
            decimal expectedResult = input;

            //Act
            decimal actualResult = rule.ImplementRule(_policy, premium);

            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Boundary tests at 500.00
        /// </summary>
        [TestCase (499)]
        [TestCase(499.5)]
        [TestCase(500.5)]
        [TestCase (501)]
        public void ImplementRuleTestBasePremiumIsLessThan500(decimal input)
        {
            //Assemble
            decimal premium = 0;
            var rule = _basePremiumRule;
            decimal expectedResult = input;

            //Act
            decimal actualResult = rule.ImplementRule(_policy, premium);

            //Assert
            Assert.That(actualResult, !Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Other occupation test
        /// </summary>
        [Test]
        public void ImplementRuleTestOccupationOther()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            driver.Occupation.JobTitle = OccupationEnum.Other;
            policy.DriversOnPolicy.Add(driver);
            decimal premium = 500;
            DriverOccupationRule driverOccupationRule = new DriverOccupationRule();

            //Act
            decimal expectedResult = premium;
            decimal actualResult = driverOccupationRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Test for chauffeur.  Increase by 10%
        /// </summary>
        [Test]
        public void ImplementRuleTestOccupationChauffeur()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            driver.Occupation.JobTitle = OccupationEnum.Chauffeur;
            policy.DriversOnPolicy.Add(driver);
            decimal premium = 500;
            DriverOccupationRule driverOccupationRule = new DriverOccupationRule();

            //Act
            decimal expectedResult = premium + premium * 0.1m;
            decimal actualResult = driverOccupationRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test for Accountant.  Decrease by 10%
        /// </summary>
        [Test]
        public void ImplementRuleTestOccupationAccountant()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            driver.Occupation.JobTitle = OccupationEnum.Accountant;
            policy.DriversOnPolicy.Add(driver);
            decimal premium = 500;
            DriverOccupationRule driverOccupationRule = new DriverOccupationRule();

            //Act
            decimal expectedResult = premium - premium * 0.1m;
            decimal actualResult = driverOccupationRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ImplementRuleYoungestDriver21()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1996, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            decimal premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            decimal expectedResult = premium + premium * 0.2m;
            decimal actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ImplementRuleYoungestDriver25()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1992, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            decimal premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            decimal expectedResult = premium + premium * 0.2m;
            decimal actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        public void ImplementRuleYoungestDriver26()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1991, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            decimal premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            decimal expectedResult = premium + premium * 0.1m;
            decimal actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ImplementRuleYoungestDriver75()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime startDate = new DateTime(2017, 03, 20);
            DateTime driverDateOfBirth = new DateTime(1942, 03, 20);
            policy.PolicyStartDate = startDate;
            policy.DriversOnPolicy.Add(driver);
            driver.DateOfBirth = driverDateOfBirth;
            decimal premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            decimal expectedResult = premium + premium * 0.1m;
            decimal actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ImplementRuleClaimsWithinYear()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime policyStartDate = new DateTime(2017, 03, 20);
            policy.PolicyStartDate = policyStartDate;
            DateTime claimDate = new DateTime(2016, 03, 20);
            Claim claim = new Claim() { DateOfClaim = claimDate };
            DriverClaimsRule driverClaimsRule = new DriverClaimsRule();
            driver.ClaimsAssociatedToDriver.Add(claim);
            policy.DriversOnPolicy.Add(driver);
            decimal premium = 500;

            //Act
            decimal expectedResult = premium + premium * 0.2m;
            decimal actualResult = driverClaimsRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        public void ImplementRuleClaimsWithinTwoYears()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime policyStartDate = new DateTime(2017, 03, 20);
            policy.PolicyStartDate = policyStartDate;
            DateTime claimDate = new DateTime(2015, 03, 20);
            Claim claim = new Claim() { DateOfClaim = claimDate };
            DriverClaimsRule driverClaimsRule = new DriverClaimsRule();
            driver.ClaimsAssociatedToDriver.Add(claim);
            policy.DriversOnPolicy.Add(driver);
            decimal premium = 500;

            //Act
            decimal expectedResult = premium + premium * 0.1m;
            decimal actualResult = driverClaimsRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [Test]
        public void ImplementRuleClaimsWithinFiveYears()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            DateTime policyStartDate = new DateTime(2017, 03, 20);
            policy.PolicyStartDate = policyStartDate;
            DateTime claimDate = new DateTime(2012, 03, 20);
            Claim claim = new Claim() { DateOfClaim = claimDate };
            DriverClaimsRule driverClaimsRule = new DriverClaimsRule();
            driver.ClaimsAssociatedToDriver.Add(claim);
            policy.DriversOnPolicy.Add(driver);
            decimal premium = 500;

            //Act
            decimal expectedResult = premium + premium * 0.1m;
            decimal actualResult = driverClaimsRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}