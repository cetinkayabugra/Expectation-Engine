#!/bin/bash

# Example usage script for the Expectation Engine API
# This demonstrates basic operations

API_URL="http://localhost:5000/api"
NLP_URL="http://localhost:8000"

echo "================================"
echo "Expectation Engine API Examples"
echo "================================"
echo ""

# 1. Test NLP Service
echo "1. Testing NLP Service..."
curl -s -X POST "${NLP_URL}/score" \
  -H "Content-Type: application/json" \
  -d '{"text": "The company reported record-breaking quarterly earnings with strong revenue growth"}' | jq '.'
echo ""

# 2. Get all tickers
echo "2. Getting all tickers..."
curl -s "${API_URL}/tickers" | jq '.'
echo ""

# 3. Get specific ticker by symbol
echo "3. Getting AAPL ticker..."
curl -s "${API_URL}/tickers/symbol/AAPL" | jq '.'
echo ""

# 4. Get prices for a ticker
echo "4. Getting prices for AAPL (TickerId=1)..."
curl -s "${API_URL}/prices/ticker/1" | jq '.'
echo ""

# 5. Get news for a ticker
echo "5. Getting news for AAPL..."
curl -s "${API_URL}/news/ticker/1" | jq '.'
echo ""

# 6. Get earnings for a ticker
echo "6. Getting earnings for AAPL..."
curl -s "${API_URL}/earnings/ticker/1" | jq '.'
echo ""

# 7. Get features for a ticker
echo "7. Getting features for AAPL..."
curl -s "${API_URL}/features/ticker/1" | jq '.'
echo ""

# 8. Get predictions for a ticker
echo "8. Getting predictions for AAPL..."
curl -s "${API_URL}/predictions/ticker/1" | jq '.'
echo ""

# 9. Get all backtests
echo "9. Getting all backtests..."
curl -s "${API_URL}/backtests" | jq '.'
echo ""

echo "================================"
echo "Examples completed!"
echo "================================"
