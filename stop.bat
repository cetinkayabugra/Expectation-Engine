@echo off
REM Expectation Engine Stop Script for Windows
REM This script stops all services

echo ========================================================
echo      Expectation Engine - Stop Script
echo ========================================================
echo.

REM Stop services
echo Stopping all services...
docker compose down

if errorlevel 1 (
    echo.
    echo ERROR: Error stopping services
    echo Run 'docker compose ps' to check container status
    exit /b 1
)

echo.
echo [OK] All services stopped successfully!
echo.
echo To also remove data volumes, run:
echo    docker compose down -v
echo.
pause
