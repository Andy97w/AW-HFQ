# HighfieldTechTest

A full-stack solution demonstrating user data processing with a .NET 8 API, React frontend, and comprehensive testing suite.

## Solution Overview

This solution consists of 3 projects:

### API (`HighfieldTechTest.Api`)
A .NET 8 Web API that retrieves users from Highfield's recruitment API and processes the data to:
- Calculate user ages and age plus twenty
- Analyze favorite color statistics
- Return structured summary data

### Tests (`HighfieldTechTest.Tests`)
Comprehensive test suite including:
- Unit tests for business logic
- Integration tests against Highfield's API
- Controller testing with mocking

### Web (`HighfieldTechTest.Web`)
A React frontend with Vite that:
- Consumes the API data
- Displays users in organized tables
- Supports dark/light mode themes


Website should be available at http://highfield-tech-test-frontend-dev.s3-website-eu-west-1.amazonaws.com/
