#!/bin/bash

# Expectation Engine Startup Script
# This script starts all services and initializes the database

set -e  # Exit on error

echo "╔════════════════════════════════════════════════════════╗"
echo "║     Expectation Engine - Startup Script               ║"
echo "╚════════════════════════════════════════════════════════╝"
echo ""

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    echo "❌ Error: Docker is not installed"
    echo "Please install Docker from: https://docs.docker.com/get-docker/"
    exit 1
fi

echo "✓ Docker is installed"

# Check if docker compose is available
if ! docker compose version &> /dev/null; then
    echo "❌ Error: Docker Compose is not available"
    echo "Please update Docker to get Compose V2"
    exit 1
fi

echo "✓ Docker Compose is available"
echo ""

# Start services
echo "🚀 Starting all services..."
echo "   This may take a few minutes on first run..."
echo ""

docker compose up -d

if [ $? -ne 0 ]; then
    echo "❌ Failed to start services"
    echo "Run 'docker compose logs' to see error details"
    exit 1
fi

echo ""
echo "✓ Services started successfully!"
echo ""

# Wait for SQL Server to be ready
echo "⏳ Waiting for SQL Server to initialize (30 seconds)..."
sleep 30

# Check if SQL Server is ready
echo "🔍 Checking SQL Server status..."
for i in {1..10}; do
    if docker exec expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
        -S localhost -U sa -P 'YourStrong@Passw0rd' -Q "SELECT 1" &> /dev/null; then
        echo "✓ SQL Server is ready!"
        break
    fi
    
    if [ $i -eq 10 ]; then
        echo "⚠️  Warning: SQL Server may not be ready yet"
        echo "   You may need to run ./init-db.sh manually in a moment"
    else
        echo "   Still waiting... (attempt $i/10)"
        sleep 5
    fi
done

echo ""

# Initialize database
echo "📊 Initializing database..."
./init-db.sh

if [ $? -ne 0 ]; then
    echo "⚠️  Database initialization may have failed"
    echo "   You can try running ./init-db.sh manually"
fi

echo ""
echo "╔════════════════════════════════════════════════════════╗"
echo "║              🎉 Startup Complete!                      ║"
echo "╚════════════════════════════════════════════════════════╝"
echo ""
echo "📍 Your services are now running:"
echo ""
echo "   🌐 Web UI:         http://localhost:5000"
echo "   📚 API Docs:       http://localhost:5000/swagger"
echo "   🤖 NLP Service:    http://localhost:8000/docs"
echo ""
echo "💡 Quick Commands:"
echo "   • View logs:       docker compose logs -f"
echo "   • Stop services:   docker compose down"
echo "   • Restart:         docker compose restart"
echo ""
echo "📖 See QUICKSTART.md for more information"
echo ""
