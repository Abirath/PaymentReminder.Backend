# Payment Reminder Backend

A .Net core (3.1) WebAPI project to expose list of payment values.
- The API is implemented using REST-ful architecture and Factory design pattern in .NET Core 3.1. The implementation is done in such a way that no new or third party
Nuget packages needs to be installed for the successfull execution of the API.
- The API is well documented using Swagger UI.

## Implementations 

The API exposes two operations or end-points.

- The first is a get call which loads a list of upcoming rental payment items from payments.json (temporary db) file into memory,
and the values are returned so that they can be consumed by the website developed in the [frontend](https://github.com/Abirath/paymentreminder_frontend). 
- The amounts for each payment are stored as/in cents (as plain numerical value in db file), The money value is converted to dollars while exposing it to the client.
- The second is a update/post call to update the status of an individual payment to `Paid` (a mock payment operation), when the user clicks the pay button in the dashboard of the
website developed in the [frontend](https://github.com/Abirath/paymentreminder_frontend).

### Running the Bankend Project

- The backend project can be run via the [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run) or using Visual Studio.
- Ideally this server should run on port 5000.

#### _Backend Error Scenario_

- A specific error scenario is handled just to implement error handling in API. 
- If the amount of the payment being paid is over \$1000 (100,000 cents), then the API will return a error/non-success response indicating that the client 
does not have sufficient funds.

Note: The error is not displayed in the [frontend](https://github.com/Abirath/paymentreminder_frontend) using pop-ups or alert windows.

