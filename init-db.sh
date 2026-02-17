#!/bin/bash

# Wait for SQL Server to be ready
echo "Waiting for SQL Server to be ready..."
sleep 30

# Create database
echo "Creating database..."
docker exec expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ExpectationEngine') CREATE DATABASE ExpectationEngine;"

# Run schema script
echo "Running schema script..."
docker exec -i expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -d ExpectationEngine < ExpectationEngine.API/Database/schema.sql

# Run seed script
echo "Running seed script..."
docker exec -i expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -d ExpectationEngine < ExpectationEngine.API/Database/seed.sql

echo "Database initialization complete!"
