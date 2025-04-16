# Salesforce Timer Function App

This is an Azure Function App written in C# .Net that is triggered every 30 minutes. It connects to the Salesforce REST API, retrieves data in JSON format, and sends the results to an Azure Service Bus queue named `DevTest`.

---

## Features

-  Built with the latest .NET version
-  Timer Triggered: Runs every 30 minutes
-  Connects to Salesforce via REST API
- 📦Pushes JSON results to Azure Service Bus Queue (`DevTest`)
-  Configurable settings via `local.settings.json`

---

## Folder Structure 
.vs/                        # Visual Studio user settings
bin/                        # Build output
obj/                        # Build temp files

.gitignore                  # Git ignore rules
host.json                   # Azure Function host settings
local.settings.json         # Local development settings
Program.cs                  # Entry point for the app

readme.md                   # Project documentation

SalesforceClient.cs         # Handles auth & data fetching from Salesforce
SalesForceFunctionApp.cs    # Main timer-triggered Azure Function
ServiceBusSender.cs         # Handles sending JSON to Azure Service Bus

SalesForcePOC.csproj        # Project file
SalesForcePOC.sln           # Solution file


## ⚙️ Configuration

Before running the app, ensure the following settings are defined in `local.settings.json`:
## ---func start--- command to run the function locally


