Feature: CreatePost

Scenario: Create Post
	Given The user gets authenticated for "api/users/login"
	Then I create a post request to "api/posts" with text "Posting this with specflow feature"
	Then I should see the post as "Posting this with specflow feature"
