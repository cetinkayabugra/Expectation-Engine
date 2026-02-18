#!/bin/bash

# Expectation Engine Stop Script
# This script stops all services

echo "╔════════════════════════════════════════════════════════╗"
echo "║     Expectation Engine - Stop Script                  ║"
echo "╚════════════════════════════════════════════════════════╝"
echo ""

# Stop services
echo "🛑 Stopping all services..."
docker compose down

if [ $? -eq 0 ]; then
    echo ""
    echo "✓ All services stopped successfully!"
    echo ""
    echo "💡 To also remove data volumes, run:"
    echo "   docker compose down -v"
    echo ""
else
    echo ""
    echo "❌ Error stopping services"
    echo "Run 'docker compose ps' to check container status"
    exit 1
fi
