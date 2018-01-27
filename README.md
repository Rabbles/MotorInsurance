# Motor Insurance Calculator
A C# application to calculate motor insurance based on a set of rules.

Design a motor insurance calculation program that will produce an insurance premium or return a decline message based on a set of predefined rules. The program will allow the user to enter the start date of the insurance policy and add drivers and associated details to be included on the insurance. Based on the details entered the program will return the calculated insurance premium or a decline message. 

1.	The policy details that can be entered:

•	Required: Start Date of the Policy, E.g. 06/10/2015.

•	Required - Add Driver(s) 
Required - Name: E.g. Brian.
Required - Occupation: ‘Chauffeur’ or ‘Accountant’.
Required - Date of Birth: E.g. 01/01/1973.

•	Optional – Add Claim(s) associated to the driver.
Required for each claim - Date of Claim: E.g. 01/01/2014.

A policy has a minimum of 1 and a maximum of 5 drivers.
A driver has a maximum of 5 Claims.

2.	Predefined rules.

a.	 Premium Calculation Rules – to be completed in this order.

1. The starting point for the premium is £500.

2. Driver Occupation

•	If there is driver who is a Chauffeur on the policy increase the premium by 10%
•	If there is driver who is an Accountant on the policy decrease the premium by 10%

3. Driver Age 

•	If the youngest driver is aged between 21 and 25 at the start date of the policy increase the premium by 20%
•	If the youngest driver is aged between 26 and 75 at the start date of the policy decrease the premium by 10%

4. Driver Claims

•	For each claim within 1 year of the start date of the policy increase the premium by 20%
•	For each claim within 2 - 5 years of the start date of the policy increase the premium by 10%

b.	Decline Rules

1. If the start date of the policy is before today decline with the message "Start Date of Policy".
2. If the youngest driver is under the age of 21 at the start date of the policy decline with a message "Age of Youngest Driver" and append the name of the driver.
3. If the oldest driver is over the age of 75 at the start date of the policy decline with a message "Age of Oldest Driver" and append the name of the driver.
4. If a driver has more than 2 claims decline with a message "Driver has more than 2 claims" – include the name of the driver.
5. If the total number of claims exceeds 3 then decline with a message "Policy has more than 3 claims".


Glossary:
•	Policy: The insurance cover as agreed between the insurance company and customer.
•	Premium: The amount to be paid by a customer for an agreed amount of insurance cover.
•	Decline: An insurer may refuse to provide insurance as the customer / event may not meet certain standards. A decline message is displayed and no premium calculated.
•	Claim: What the customer has asked a previous insurance company to pay to sort out problems caused by an event, such as an accident.

