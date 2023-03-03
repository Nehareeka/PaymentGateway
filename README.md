# PaymentGateway

## Project Structure

* Payment Gateway APIs
* Bank Simulator APIs
* Identity Server for authentication

## Assumptions
Data is stored on the Payment Gateway side (unsure of the compliance)

## Test Data

#### Source: https://www.checkout.com/docs/testing/response-code-testing

### Invalid Cards stored in-memory database

| Card Number | Status |   
| :-------------:| :----------:|
| 4024007103573027 | Declined |
| 4544249167673670 | Declined |
| 5279988405398834 | Declined |
| 5328090869100177 | Declined |
| 4095254802642505 | Expired |
| 4897453568485113 | Declined |
| 5420951756276171 | Declined |

| <Any 16 digit card> | Paid |

Bank Simulator processes payments and submits

#### Response Codes

| Amount        | Response         
| ------------- |:-------------:| 
| 200 | Success |
| 201 | Payment Success |
| 422 | Payment Failed |
| 400 | Bad Request |
| 404 | Not Found |
| 500 | Internal Server Error |

#### To Dos:
Tokenize the request
Get all transactions within a date range, sorted by success/failure
Add Docker support
