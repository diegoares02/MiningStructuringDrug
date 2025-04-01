# Mining Destructuring Drug API

This project is a Mining Destructuring Drug API built with .NET and SQL Server, containerized with Docker.

## Prerequisites

* Docker and Docker Compose installed on your machine.

## Project Structure

MiningDestructuringDrug
      MiningDestructuringDrug.API/           # ASP.NET Core API project
      MiningDestructuringDrug.Core/          # Core application logic
      MiningDestructuringDrug.Infrastructure/ # Infrastructure layer (data access, services)
      MiningDestructuringDrug.Tests/         # Unit tests
docker-compose.yml           # Docker Compose configuration
README.md                    # This file


## Building and Running the API with Docker Compose

1.  **Clone the Repository:**

    ```bash
    git clone <your-repository-url>
    cd MiningDestructuringDrug
    ```

2.  **Build and Run the Docker Containers:**

    Use the following command to build the Docker images and start the containers defined in `docker-compose.yml`:

    ```bash
    docker-compose up --build
    ```

    * `docker-compose up`: Starts the containers.
    * `--build`: Builds the images if they don't exist or if there are changes.

    This command will:

    * Build the .NET API image.
    * Start a SQL Server container.
    * Establish the necessary network connections between the containers.
    * Set the database connection string via environment variables, as specified in the `docker-compose.yml`.

3.  **Access the API:**

    Once the containers are running, you can access the API at `http://localhost:<API_PORT>`, where `<API_PORT>` is the port exposed by your API container (defined in `docker-compose.yml`). The API will be available when the console logs indicate that the application has started.

4.  **Stopping the Containers:**

    To stop the containers, use the following command:

    ```bash
    docker-compose down
    ```