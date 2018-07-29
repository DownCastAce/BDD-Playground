Feature: NDD Calculator
	In order to send orders at the right time
	I want to calculate the next delivery date for a patient

@ndd
Scenario: Patient is stopped
	Given I have a patient
	And The patient status is "stopped"
	And The patient NDD is set to today
	When I recalculate the NDD
	Then The patient NDD should be null

@ndd
Scenario: Patient is removed
	Given I have a patient
	And The patient status is "removed"
	And The patient NDD is set to today
	When I recalculate the NDD
	Then The patient NDD should be null

@ndd
Scenario: Patient is active with no prescription items
	Given I have a patient
	And The patient NDD is set to today
	And The patient status is "active"
	And Their prescription is empty
	When I recalculate the NDD
	Then The patient NDD should be null

@ndd
Scenario: Patient is active with one prescription item and no order history
	Given I have a patient
	And The patient NDD is null
	And The patient status is "active"
	And Their prescription has items
		| id | title | frequency |
		| 1  | pad1  | 2         |
	And The patient has no previous orders
	When I recalculate the NDD
	Then The patient NDD should be today

@ndd
Scenario: Patient is Active with one prescription item and an order history
	Given I have a patient
	And The patient NDD is null
	And The patient status is "active"
	And Their prescription has items
		| id | title | frequency |
		| 1  | pad1  | 2         |
	And The patient has previous orders
		| product_id | date       |
		| 1          | 01/01/2018 |
	When I recalculate the NDD
	Then The patient NDD should be today
