Feature: Assignment

Scenario Outline: To_Verify_Product_Search_and_Add_to_Cart_Functionality

Given User Opens Amazon Home Page
When User Searches for <Product_Name>
Then User is displayed the search result
When User Selects the item <Product_Name> from search page
Then User Checks the price and adds the product to cart
When User navigates to the Cart
Then User Verifies the <Product_Name> and price

Examples: 
| Product_Name                                                      |
| TP-Link TL-WR820N 300 Mbps Single_Band Speed Wireless WiFi Router |