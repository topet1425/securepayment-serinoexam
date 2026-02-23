# SecurePayment SPA [Serino Technical Exam]

*This repository is for job application assessment only. 

This project is a secure Single Page Application (SPA) built using
**Blazor WebAssembly (ASP.NET Core Hosted)** with **MudBlazor 7** for
the UI layer.

-------------------------------------------------------------------
## Technology Stack
-   .NET 7
-   Blazor WebAssembly (Hosted)
-   ASP.NET Core Web API
-   MudBlazor 7
-   DataAnnotations Validation
-   Policy-Based Authorization
-   Bearer Token Authentication (Demo)

_I intended to use .NET 10 for this project; however, I encountered update issues on my local machine. Given the time constraints, resolving the setup problem may significantly affect the timeline._

-------------------------------------------------------------------

## Architecture Overview

    SecurePayment_SerinoExam.sln
     ├── SecurePayment-SerinoExam.Client   (Blazor WebAssembly UI)
     ├── SecurePayment-SerinoExam.Server   (ASP.NET Core Web API)
     └── SecurePayment-SerinoExam.Shared   (Shared DTO)

### Client (Blazor WebAssembly)

-   MudBlazor 7 UI components
-   DataAnnotations form validation
-   Secure API calls via HttpClient
-   Token management (demo implementation)
-   UX state management (loading, success, error, unauthorized,
    forbidden)

### Server (ASP.NET Core API)

-   `/api/payments` endpoint
-   Custom Bearer authentication handler
-   Policy-based authorization (`payments:write`)
-   Correct HTTP status handling (401, 403, 500)
-   Structured request validation

### Shared

-   `PaymentRequest`
-   `PaymentResponse`

------------------------------------------------------------------------------------------

## Run Instructions

Run the **Server** project only:

Navigate to:

    https://localhost:<port>/payments
-----------------------------------------------------------------------
## Testing the application. 
-  To test the 403 (Forbidden) and 401 (Unauthorized) scenarios, please select expired-token and no-permission-token.
-  To test the 500 (Internal Server Error) response, please add the prefix 500 to the reference ID.

