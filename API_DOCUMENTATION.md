# API Documentation

## Base URL
```
http://localhost:5000/api
```

## Authentication
Currently, the API does not require authentication. This is a starter template - implement JWT or OAuth2 for production use.

## Tickers API

### GET /api/tickers
Get all active tickers.

**Response:**
```json
[
  {
    "id": 1,
    "symbol": "AAPL",
    "companyName": "Apple Inc.",
    "sector": "Technology",
    "industry": "Consumer Electronics",
    "exchange": "NASDAQ",
    "isActive": true,
    "createdAt": "2024-02-17T10:00:00Z",
    "updatedAt": "2024-02-17T10:00:00Z"
  }
]
```

### GET /api/tickers/{id}
Get a specific ticker by ID.

### GET /api/tickers/symbol/{symbol}
Get a specific ticker by symbol.

**Example:**
```bash
curl http://localhost:5000/api/tickers/symbol/AAPL
```

### POST /api/tickers
Create a new ticker.

**Request Body:**
```json
{
  "symbol": "TSLA",
  "companyName": "Tesla Inc.",
  "sector": "Consumer Cyclical",
  "industry": "Auto Manufacturers",
  "exchange": "NASDAQ"
}
```

### PUT /api/tickers/{id}
Update an existing ticker.

### DELETE /api/tickers/{id}
Soft delete a ticker (sets isActive to false).

---

## Prices API

### GET /api/prices/ticker/{tickerId}
Get price history for a ticker.

**Query Parameters:**
- `startDate` (optional): Filter prices from this date
- `endDate` (optional): Filter prices until this date

**Example:**
```bash
curl "http://localhost:5000/api/prices/ticker/1?startDate=2024-01-01&endDate=2024-02-17"
```

**Response:**
```json
[
  {
    "id": 1,
    "tickerId": 1,
    "date": "2024-02-17T00:00:00Z",
    "openPrice": 170.50,
    "highPrice": 172.30,
    "lowPrice": 169.80,
    "closePrice": 171.50,
    "adjustedClose": 171.50,
    "volume": 52000000,
    "createdAt": "2024-02-17T10:00:00Z"
  }
]
```

### POST /api/prices
Add a single price entry.

**Request Body:**
```json
{
  "tickerId": 1,
  "date": "2024-02-17",
  "openPrice": 170.50,
  "highPrice": 172.30,
  "lowPrice": 169.80,
  "closePrice": 171.50,
  "adjustedClose": 171.50,
  "volume": 52000000
}
```

### POST /api/prices/batch
Add multiple price entries at once.

**Request Body:**
```json
[
  {
    "tickerId": 1,
    "date": "2024-02-16",
    "openPrice": 169.00,
    "highPrice": 170.50,
    "lowPrice": 168.50,
    "closePrice": 170.00,
    "adjustedClose": 170.00,
    "volume": 48000000
  },
  {
    "tickerId": 1,
    "date": "2024-02-17",
    "openPrice": 170.50,
    "highPrice": 172.30,
    "lowPrice": 169.80,
    "closePrice": 171.50,
    "adjustedClose": 171.50,
    "volume": 52000000
  }
]
```

---

## News API

### GET /api/news/ticker/{tickerId}
Get news articles for a ticker.

**Query Parameters:**
- `startDate` (optional): Filter news from this date
- `endDate` (optional): Filter news until this date

**Response:**
```json
[
  {
    "id": 1,
    "tickerId": 1,
    "title": "Apple announces record quarterly earnings",
    "content": "Apple Inc. reported record-breaking quarterly earnings...",
    "source": "TechNews",
    "publishedAt": "2024-02-15T10:00:00Z",
    "url": "https://example.com/news/1",
    "sentimentScore": 0.85,
    "sentimentLabel": "positive",
    "createdAt": "2024-02-17T10:00:00Z"
  }
]
```

### POST /api/news
Add a news article. Sentiment will be automatically analyzed.

**Request Body:**
```json
{
  "tickerId": 1,
  "title": "Apple launches new product",
  "content": "Apple Inc. today announced a groundbreaking new product...",
  "source": "TechCrunch",
  "publishedAt": "2024-02-17T10:00:00Z",
  "url": "https://example.com/news"
}
```

---

## Earnings API

### GET /api/earnings/ticker/{tickerId}
Get earnings history for a ticker.

**Response:**
```json
[
  {
    "id": 1,
    "tickerId": 1,
    "fiscalYear": 2024,
    "fiscalQuarter": 1,
    "reportDate": "2024-01-25T00:00:00Z",
    "revenue": 94300000000,
    "netIncome": 23600000000,
    "eps": 1.52,
    "epsEstimate": 1.42,
    "revenueSurprise": 1200000000,
    "epsSurprise": 0.10,
    "createdAt": "2024-02-17T10:00:00Z"
  }
]
```

### POST /api/earnings
Add earnings data.

**Request Body:**
```json
{
  "tickerId": 1,
  "fiscalYear": 2024,
  "fiscalQuarter": 2,
  "reportDate": "2024-04-25",
  "revenue": 95000000000,
  "netIncome": 24000000000,
  "eps": 1.58,
  "epsEstimate": 1.50,
  "revenueSurprise": 1500000000,
  "epsSurprise": 0.08
}
```

---

## Transcripts API

### GET /api/transcripts/ticker/{tickerId}
Get earnings call transcripts for a ticker.

**Response:**
```json
[
  {
    "id": 1,
    "earningsId": 1,
    "tickerId": 1,
    "transcriptText": "We are very pleased with our Q1 results...",
    "transcriptDate": "2024-01-25T00:00:00Z",
    "sentimentScore": 0.82,
    "sentimentLabel": "positive",
    "keyPhrases": "strong momentum, great opportunities",
    "createdAt": "2024-02-17T10:00:00Z"
  }
]
```

### POST /api/transcripts
Add a transcript. Sentiment will be automatically analyzed.

**Request Body:**
```json
{
  "earningsId": 1,
  "tickerId": 1,
  "transcriptText": "We are very pleased with our Q1 results. iPhone sales exceeded expectations...",
  "transcriptDate": "2024-01-25T16:00:00Z",
  "keyPhrases": "exceeded expectations, strong growth"
}
```

---

## Features API

### GET /api/features/ticker/{tickerId}
Get computed features for a ticker.

**Query Parameters:**
- `startDate` (optional)
- `endDate` (optional)

**Response:**
```json
[
  {
    "id": 1,
    "tickerId": 1,
    "featureDate": "2024-02-17T00:00:00Z",
    "priceReturn5D": 0.0316,
    "priceReturn20D": 0.0842,
    "volatility20D": 0.0145,
    "volume20DAvg": 50000000,
    "newsSentiment7D": 0.85,
    "newsCount7D": 3,
    "earningsSurprise": 0.10,
    "transcriptSentiment": 0.82,
    "marketCapChange": null,
    "sectorMomentum": null,
    "createdAt": "2024-02-17T10:00:00Z"
  }
]
```

### POST /api/features/compute
Compute features for a ticker on a specific date (stub implementation).

**Query Parameters:**
- `tickerId`: Ticker ID
- `date`: Date to compute features for

**Example:**
```bash
curl -X POST "http://localhost:5000/api/features/compute?tickerId=1&date=2024-02-17"
```

---

## Predictions API

### GET /api/predictions/ticker/{tickerId}
Get predictions for a ticker.

**Query Parameters:**
- `startDate` (optional)
- `endDate` (optional)

**Response:**
```json
[
  {
    "id": 1,
    "tickerId": 1,
    "predictionDate": "2024-02-12T00:00:00Z",
    "targetDate": "2024-02-22T00:00:00Z",
    "predictedReturn": 0.0350,
    "confidence": 0.72,
    "modelVersion": "v1.0",
    "featureSnapshot": "{\"priceReturn5D\": 0.0316}",
    "actualReturn": null,
    "createdAt": "2024-02-17T10:00:00Z"
  }
]
```

### POST /api/predictions/generate
Generate a new prediction (stub implementation).

**Query Parameters:**
- `tickerId`: Ticker ID
- `targetDate`: Target date for prediction

**Example:**
```bash
curl -X POST "http://localhost:5000/api/predictions/generate?tickerId=1&targetDate=2024-03-01"
```

---

## Backtests API

### GET /api/backtests
Get all backtest results.

**Response:**
```json
[
  {
    "id": 1,
    "backtestName": "Q1 2024 Backtest",
    "modelVersion": "v1.0",
    "startDate": "2024-01-01T00:00:00Z",
    "endDate": "2024-02-16T00:00:00Z",
    "totalReturn": 0.1250,
    "sharpeRatio": 1.85,
    "maxDrawdown": -0.0450,
    "winRate": 0.62,
    "totalTrades": 45,
    "avgTradeReturn": 0.0028,
    "parameters": "{\"threshold\": 0.6}",
    "results": "{\"trades\": 45}",
    "createdAt": "2024-02-17T10:00:00Z"
  }
]
```

### POST /api/backtests/run
Run a new backtest (stub implementation).

**Query Parameters:**
- `backtestName`: Name for the backtest
- `startDate`: Backtest start date
- `endDate`: Backtest end date
- `modelVersion`: Model version to use

**Example:**
```bash
curl -X POST "http://localhost:5000/api/backtests/run?backtestName=Test&startDate=2024-01-01&endDate=2024-02-17&modelVersion=v1.0"
```

---

## NLP Service API

### POST /score
Analyze sentiment of financial text using FinBERT.

**Base URL:** `http://localhost:8000`

**Request Body:**
```json
{
  "text": "The company reported strong earnings with revenue growth exceeding expectations"
}
```

**Response:**
```json
{
  "label": "positive",
  "score": 0.87,
  "scores": {
    "positive": 0.87,
    "negative": 0.05,
    "neutral": 0.08
  }
}
```

**Labels:**
- `positive`: Positive financial sentiment
- `negative`: Negative financial sentiment
- `neutral`: Neutral financial sentiment

**Example:**
```bash
curl -X POST http://localhost:8000/score \
  -H "Content-Type: application/json" \
  -d '{"text": "Stock prices plummeted after disappointing earnings report"}'
```

---

## Error Responses

All endpoints return standard HTTP status codes:

- `200 OK`: Request successful
- `201 Created`: Resource created successfully
- `204 No Content`: Successful deletion
- `400 Bad Request`: Invalid request body or parameters
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

**Error Response Format:**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Symbol": ["The Symbol field is required."]
  }
}
```
