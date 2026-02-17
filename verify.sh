#!/bin/bash

echo "Checking Docker Compose configuration..."

# Check if docker is installed
if ! command -v docker &> /dev/null; then
    echo "ERROR: Docker is not installed"
    exit 1
fi

echo "✓ Docker is installed"

# Check docker-compose.yml syntax
if docker compose config > /dev/null 2>&1; then
    echo "✓ docker-compose.yml is valid"
else
    echo "ERROR: docker-compose.yml has syntax errors"
    exit 1
fi

# Check if required files exist
FILES=(
    "Dockerfile"
    "ExpectationEngine.NLP/Dockerfile"
    "ExpectationEngine.API/ExpectationEngine.API.csproj"
    "ExpectationEngine.NLP/main.py"
    "ExpectationEngine.NLP/requirements.txt"
    "ExpectationEngine.API/Database/schema.sql"
    "ExpectationEngine.API/Database/seed.sql"
)

for file in "${FILES[@]}"; do
    if [ -f "$file" ]; then
        echo "✓ Found $file"
    else
        echo "ERROR: Missing $file"
        exit 1
    fi
done

echo ""
echo "All checks passed! You can now run:"
echo "  docker compose up -d"
echo ""
echo "After the services start, initialize the database with:"
echo "  ./init-db.sh"
