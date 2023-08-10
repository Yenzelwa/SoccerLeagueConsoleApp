# Soccer League Application

Welcome to the Soccer League Application! This application allows you to manage soccer match data and calculate rankings.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET Core SDK [Installation Guide](https://dotnet.microsoft.com/download)
- SQL Server (or SQL Server Express) installed and running
- Git (optional, but recommended for version control)

## Installation

1. Clone this repository to your local machine using:
git clone https://github.com/Yenzelwa/SoccerLeagueConsoleApp.git

2. Navigate to the project directory:
cd soccer-league-app

3. Configure the Connection String:
- Open `appsettings.json` in the project root.
- Replace the value of `"SoccerLeagueConnection"` with your SQL Server connection string.

4. Install Dependencies and Build:
dotnet restore
dotnet build

5. Apply Database Migrations:
dotnet ef database update


## Usage

1. Run the application:
dotnet run


2. Follow the on-screen instructions to enter match data manually or upload data from a file.

3. The application will process the data, calculate rankings, and display the results.

## Configuration

- `appsettings.json`: Configure database connection and other settings.
- Environment Variables: Customize application behavior using environment variables.

## Troubleshooting

- If you encounter issues, check the logs in the `logs/log.txt` file for more information.
- Make sure your SQL Server is running and the connection string is correct.




