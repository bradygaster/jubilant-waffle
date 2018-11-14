## Setup

### Create the SQL DB in a container
1. Run `docker-compose up` using the `src/docker-compose.yml` file.
1. Use the `mssql` extension for VS Code to connect to the in-container DB to run this script:

    ```sql
    CREATE DATABASE DispatchR
    ```
    THis script will create the database and prepare it for being accessed by the Entity Framework components to preload the database with sample data.
1. F5 in VS Code to run the `DispatchR.API` **and** the `DispatchR.Web` project simultaneously.
1. If you want to reset (or load-up for the first time) the sample data, hit the `/api/reset` URL to reload the database. 
1. Go back to the DispatchR UI and click the Step 1 button to load up the initial state.