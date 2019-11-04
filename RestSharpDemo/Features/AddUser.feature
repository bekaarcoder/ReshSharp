Feature: AddUser

Scenario: AddUser
	Given I perform POST operation for "users"
	And I perform operation with body
	 | name | job    |
	 | John | Artist |
	Then I should see the "name" as "John"
	And I should see the "job" as "Artist"
