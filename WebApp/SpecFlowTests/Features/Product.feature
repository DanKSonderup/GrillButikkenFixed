Feature: Manage Products
  In order to ensure product functionality works as expected
  As a developer
  I want to test the Product class methods and properties

@product1
  Scenario: Add raw material to product
    Given a product named "Widget" with an estimated production time of "02:30:00"
    When I add a raw material "Steel" with amount "5"
    Then the product should have 1 raw material needed
    And the first raw material should be "Steel" with amount "5"

@product2
  Scenario: Remove raw material from product
    Given a product named "Widget" with an estimated production time of "02:30:00"
    And the product has a raw material "Steel" with amount "5"
    When I remove the raw material "Steel"
    Then the product should have 0 raw materials needed

@product3
  Scenario: Check product string representation
    Given a product named "Widget" with an estimated production time of "02:30:00"
    When I call the ToString method
    Then the result should be "Widget: 02:30:00. Created at: 11/26/2024 9:15:00 AM. Updated at: 11/27/2024 9:15:00 AM"
