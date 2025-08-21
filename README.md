# Job Application Challenge (In-Memory .NET 8 API)

This is a lightweight coding exercise designed to evaluate design choices and thought processes for building a .NET 8 API. The challenge involves adding new application-related functionality to an existing API 
that manages job listings. All data is managed using in-memory lists; no database or ORM is required.

---

## Your Task

Your primary goal is to add two new endpoints to the `ApplicationsController`.

### Endpoints to Implement

1.  **Create Application**: An endpoint to create a new application tied to a specific job.
2.  **Get Applications for a Job**: An endpoint to retrieve all applications associated with a given `jobId`.

---

## Getting Started

You can run this project using GitHub Codespaces or on your local machine.

### Run in GitHub Codespaces (Recommended)

1.  Log in to your GitHub account and open this repository.
2.  Click the **Code** button, navigate to the **Codespaces** tab, and select **Create codespace on main**.
3.  Once the dev container is ready, run the application from the terminal:
    ```bash
    dotnet run
    ```
4.  Navigate to the **Ports** tab, locate the running port, set its visibility to **Public**, and click the globe icon to open the API in a new browser tab.

### Run Locally

1.  Ensure you have the **.NET 8 SDK** installed.
2.  Clone the repository and navigate to the project folder in your terminal.
3.  Run the application:
    ```bash
    dotnet run
    ```
4.  Open the URL shown in the console (e.g., `http://localhost:5176`) in your browser.

---

## API Endpoints

The data is **ephemeral** and will be cleared every time the application restarts. If the application starts without seed data, you must first create a job before you can post an application for it.

### Existing Job Endpoints

* **Create Job**: `POST /jobs`
* **List Jobs**: `GET /jobs`

### Application Endpoints (To Be Added)

* **Create Application**: `POST /applications`
* **List Applications for Job**: `GET /applications/job/{jobId}`

---

## Notes

* This exercise does not require authentication or data persistence. We value thoughtful design over complex boilerplate.
* Feel free to add helper classes, DTOs, or validators if you believe they clarify your intent and improve the design.
