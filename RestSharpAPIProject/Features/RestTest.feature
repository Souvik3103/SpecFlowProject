Feature: RestTest

A short summary of the feature

Scenario: Status Test
	Given User creates a request with endpoint "books"
	Then User Checks the status to be "OK"

Scenario: Response must be valid and have a body
	Given User creates a request with endpoint "books"
	Then User Checks the response to have a valid body

Scenario Outline: Test to check book availability
	Given User creates a request with endpoint "books"
	Then User Checks the Availability of <Book> to be <Available>

	Examples: 
	| Book         | Available |
	| The Russian  | true      |
	| Just as I Am | false     |

Scenario Outline: Test to check type of Selected book
	Given User creates a request with endpoint "books"
	Then User Checks the <Book> to be of Type <Type>

	Examples: 
	| Book                 | Type        |
	| The Midnight Library | fiction     |
	| Untamed              | non-fiction |