using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleInsurance.BusinessRules.CalculationRules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VehicleInsurance.Enum;
using VehicleInsurance.Model;

namespace VehicleInsuranceTests.BusinessRules.CalculationRules
{
    [TestClass()]
    public class CalcualtionTests
    {
        private Policy _policy;
        private Driver _driver;

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
        }



        [TestMethod()]
        public void ImplementRuleTestBasePremiumIs500()
        {
            //Assemble
            Policy policy = _policy;
            double premium = 0;
            BasePremiumRule rule = new BasePremiumRule();

            //Act
            double expectedResult = 500.00;
            double actualResult = rule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Boundary test under 500.00
        /// </summary>
        [TestMethod()]
        public void ImplementRuleTestBasePremiumIsLessThan500()
        {
            //Assemble

            Policy policy = _policy;
            double premium = 0;
            BasePremiumRule rule = new BasePremiumRule();

            //Act
            double expectedResult = 499.00;
            double actualResult = rule.ImplementRule(policy, premium);

            //Assert
            Assert.AreNotEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Boundary test above 500.00
        /// </summary>
        [TestMethod()]
        public void ImplementRuleTestBasePremiumIsMoreThan500()
        {
            //Assemble

            Policy policy = _policy;
            double premium = 0;
            BasePremiumRule rule = new BasePremiumRule();

            //Act
            double expectedResult = 500.01;
            double actualResult = rule.ImplementRule(policy, premium);

            //Assert
            Assert.AreNotEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Other occupation test
        /// </summary>
        [TestMethod]
        public void ImplementRuleTestOccupationOther()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            driver.Occupation.JobTitle = OccupationEnum.Other;
            policy.DriversOnPolicy.Add(driver);
            double premium = 500;
            DriverOccupationRule driverOccupationRule = new DriverOccupationRule();

            //Act
            double expectedResult = premium;
            double actualResult = driverOccupationRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Test for chauffeur.  Increase by 10%
        /// </summary>
        [TestMethod]
        public void ImplementRuleTestOccupationChauffeur()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            driver.Occupation.JobTitle = OccupationEnum.Chauffeur;
            policy.DriversOnPolicy.Add(driver);
            double premium = 500;
            DriverOccupationRule driverOccupationRule = new DriverOccupationRule();

            //Act
            double expectedResult = premium + premium * 0.1;
            double actualResult = driverOccupationRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test for Accountant.  Decrease by 10%
        /// </summary>
        [TestMethod]
        public void ImplementRuleTestOccupationAccountant()
        {
            //Assemble
            Policy policy = _policy;
            Driver driver = _driver;
            driver.Occupation.JobTitle = OccupationEnum.Accountant;
            policy.DriversOnPolicy.Add(driver);
            double premium = 500;
            DriverOccupationRule driverOccupationRule = new DriverOccupationRule();

            //Act
            double expectedResult = premium - premium * 0.1;
            double actualResult = driverOccupationRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
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
            double premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            double expectedResult = premium + premium * 0.2;
            double actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
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
            double premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            double expectedResult = premium + premium * 0.2;
            double actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
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
            double premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            double expectedResult = premium + premium * 0.1;
            double actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
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
            double premium = 500;
            YoungestDriverRule youngestDriverRule = new YoungestDriverRule();

            //Act

            double expectedResult = premium + premium * 0.1;
            double actualResult = youngestDriverRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
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
            double premium = 500;

            //Act
            double expectedResult = premium + premium * 0.2;
            double actualResult = driverClaimsRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
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
            double premium = 500;

            //Act
            double expectedResult = premium + premium * 0.1;
            double actualResult = driverClaimsRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
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
            double premium = 500;

            //Act
            double expectedResult = premium + premium * 0.1;
            double actualResult = driverClaimsRule.ImplementRule(policy, premium);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}