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
        private DriverOccupationRule _driverOccupationRule;
        private YoungestDriverRule _youngestDriverRule;
        private decimal _basePremium;

        [SetUp]
        public void Initialise()
        {
            _basePremium = 500m;

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
            _driverOccupationRule = new DriverOccupationRule();
            _youngestDriverRule = new YoungestDriverRule();
        }

        [TestCase (500)]
        public void ImplementRuleTestBasePremiumIs500(decimal input)
        {
            //Assemble
            var rule = _basePremiumRule;

            //Act
            var actualResult = rule.ImplementRule(_policy, _basePremium);

            //Assert
            Assert.That(actualResult, Is.EqualTo(input));
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
            var rule = _basePremiumRule;

            //Act
            decimal actualResult = rule.ImplementRule(_policy, _basePremium);

            //Assert
            Assert.That(actualResult, !Is.EqualTo(input));
        }

        /// <summary>
        /// Other occupation test
        /// </summary>
        [Test]
        public void ImplementRuleTestOccupationOther()
        {
            //Assemble
            _driver.Occupation.JobTitle = OccupationEnum.Other;
            _policy.DriversOnPolicy.Add(_driver);

            //Act
            var actualResult = _driverOccupationRule.ImplementRule(_policy, _basePremium);

            //Assert
            Assert.AreEqual(_basePremium, actualResult);
        }

        /// <summary>
        /// Test for chauffeur.  Increase by 10%
        /// </summary>
        [Test]
        public void ImplementRuleTestOccupationChauffeur()
        {
            //Assemble
            _driver.Occupation.JobTitle = OccupationEnum.Chauffeur;
            _policy.DriversOnPolicy.Add(_driver);

            //Act
            var expectedResult = _basePremium + _basePremium * 0.1m;
            var actualResult = _driverOccupationRule.ImplementRule(_policy, _basePremium);

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
            _driver.Occupation.JobTitle = OccupationEnum.Accountant;
            _policy.DriversOnPolicy.Add(_driver);

            //Act
            var expectedResult = _basePremium - _basePremium * 0.1m;
            var actualResult = _driverOccupationRule.ImplementRule(_policy, _basePremium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ImplementRuleYoungestDriver21()
        {
            //Assemble
            _policy.PolicyStartDate = new DateTime(2017, 03, 20);
            _policy.DriversOnPolicy.Add(_driver);
            _driver.DateOfBirth = new DateTime(1996, 03, 20);
            var youngestDriverRule = new YoungestDriverRule();

            //Act
            var expectedResult = _basePremium + _basePremium * 0.2m;
            var actualResult = youngestDriverRule.ImplementRule(_policy, _basePremium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ImplementRuleYoungestDriver25()
        {
            //Assemble
            _policy.PolicyStartDate = new DateTime(2017, 03, 20);
            _policy.DriversOnPolicy.Add(_driver);
            _driver.DateOfBirth = new DateTime(1992, 03, 20);
            var youngestDriverRule = new YoungestDriverRule();

            //Act
            var expectedResult = _basePremium + _basePremium * 0.2m;
            var actualResult = youngestDriverRule.ImplementRule(_policy, _basePremium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ImplementRuleYoungestDriver26()
        {
            //Assemble 
            _policy.PolicyStartDate = new DateTime(2017, 03, 20);
            _policy.DriversOnPolicy.Add(_driver);
            _driver.DateOfBirth = new DateTime(1991, 03, 20);

            //Act
            var expectedResult = _basePremium + _basePremium * 0.1m;
            var actualResult = _youngestDriverRule.ImplementRule(_policy, _basePremium);

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
            decimal _basePremium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            decimal expectedResult = _basePremium + _basePremium * 0.1m;
            decimal actualResult = youngestDriverRule.ImplementRule(policy, _basePremium);

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
            decimal _basePremium = 500;

            //Act
            decimal expectedResult = _basePremium + _basePremium * 0.2m;
            decimal actualResult = driverClaimsRule.ImplementRule(policy, _basePremium);

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
            decimal _basePremium = 500;

            //Act
            decimal expectedResult = _basePremium + _basePremium * 0.1m;
            decimal actualResult = driverClaimsRule.ImplementRule(policy, _basePremium);

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
            decimal _basePremium = 500;

            //Act
            decimal expectedResult = _basePremium + _basePremium * 0.1m;
            decimal actualResult = driverClaimsRule.ImplementRule(policy, _basePremium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}