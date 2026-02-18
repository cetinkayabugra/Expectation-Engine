@echo off
REM Expectation Engine Startup Script for Windows
REM This script starts all services and initializes the database

echo ========================================================
echo      Expectation Engine - Startup Script
echo ========================================================
echo.

REM Check if Docker is installed
docker --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: Docker is not installed
    echo Please install Docker from: https://docs.docker.com/get-docker/
    exit /b 1
)

echo [OK] Docker is installed

REM Check if docker compose is available
docker compose version >nul 2>&1
if errorlevel 1 (
    echo ERROR: Docker Compose is not available
    echo Please update Docker to get Compose V2
    exit /b 1
)

echo [OK] Docker Compose is available
echo.

REM Start services
echo Starting all services...
echo This may take a few minutes on first run...
echo.

docker compose up -d

if errorlevel 1 (
    echo ERROR: Failed to start services
    echo Run 'docker compose logs' to see error details
    exit /b 1
)

echo.
echo [OK] Services started successfully!
echo.

REM Wait for SQL Server to be ready
echo Waiting for SQL Server to initialize (30 seconds)...
timeout /t 30 /nobreak >nul

REM Check if SQL Server is ready
echo Checking SQL Server status...
docker exec expectation-engine-sql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -Q "SELECT 1" >nul 2>&1

if errorlevel 1 (
    echo WARNING: SQL Server may not be ready yet
    echo You may need to run init-db.bat manually in a moment
) else (
    echo [OK] SQL Server is ready!
)

echo.

REM Initialize database
echo Initializing database...
call init-db.bat

echo.
echo ========================================================
echo              Startup Complete!
echo ========================================================
echo.
echo Your services are now running:
echo.
echo    Web UI:         http://localhost:5000
echo    API Docs:       http://localhost:5000/swagger
echo    NLP Service:    http://localhost:8000/docs
echo.
echo Quick Commands:
echo    - View logs:       docker compose logs -f
echo    - Stop services:   docker compose down
echo    - Restart:         docker compose restart
echo.
echo See QUICKSTART.md for more information
echo.
pause
