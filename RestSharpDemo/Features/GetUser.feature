Feature: GetUser

Scenario: GetUser
	Given I perform GET operation for "users/{userid}"
	And I perform operation for user "5"
	Then I should see "first_name" as "Charles"
	And I should see "last_name" as "Morris"
