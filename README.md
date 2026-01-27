Devices API

# 📌 Summary

REST API for managing Devices.

Create, update, retrieve and delete devices

Filter devices by brand and state

Persist data using SQL Server

Apply database migrations and seed initial data automatically

The solution follows Hexagonal Architecture principles and is designed as a microservice.

## 🏗️ Requirements

Framework version: .NET 10
Database: SQL Server with Entity Framework Core
Documentation: Swagger
Testing: xUnit

## 🐳 Docker Instructions

SQL Version:
- docker pull mcr.microsoft.com/mssql/server:2022-latest

Create a “.env” file in the same folder as “docker-compose.yml” with the following variables. Set the values as you prefer:

ACCEPT_EULA=Y
MSSQL_SA_PASSWORD=teste@123
TZ=America/Sao_Paulo
MSSQL_PID=Developer

How to start the container:
Open the terminal in the folder where “docker-compose.yml” is located
Run the command: docker compose up -d --build
Verify that it is running: docker ps

## 🔑 Migrations Instructions
Create migrations
dotnet ef migrations add InitialCreate --project Devices.Infrastructure --startup-project Devices.Api

Apply migrations to the database
dotnet ef database update --project Devices.Infrastructure --startup-project Devices.Api

Ps.: The API automatically applies migrations and seed data on startup.