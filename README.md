# Expectation Engine

Equity Expectation Shift Detection System - A comprehensive starter repository for detecting and predicting equity expectation shifts using machine learning and NLP.

## Overview

This system combines ASP.NET Core 8 Web API with a Python FastAPI NLP service to analyze financial data, news sentiment, and earnings information to predict equity price movements and expectation shifts.

## Architecture

- **Web UI**: Interactive web interface with step-by-step explanations
- **API Service**: ASP.NET Core 8 Web API with Entity Framework Core
- **NLP Service**: Python FastAPI using ProsusAI/finbert for financial sentiment analysis
- **Database**: Azure SQL / SQL Server
- **Documentation**: Swagger/OpenAPI

## Features

- **📱 Interactive Web UI**: Comprehensive interface explaining every step of the system
  - Visual step-by-step guides with detailed explanations
  - Interactive demos for testing sentiment analysis and API calls
  - Architecture diagrams and data flow visualizations
  - Getting started guides and examples
- **Ticker Management**: Track stocks with company information and metadata
- **Price Data**: Historical price data storage and retrieval
- **News Analysis**: Store news articles with automated sentiment analysis
- **Earnings Data**: Quarterly earnings with surprise metrics
- **Transcripts**: Earnings call transcripts with sentiment scoring
- **Feature Engineering**: Extract and store ML features
- **Predictions**: ML model predictions with confidence scores
- **Backtesting**: Backtest trading strategies with performance metrics

## Getting Started

### Prerequisites

- Docker and Docker Compose
- (Optional) .NET 8 SDK for local development
- (Optional) Python 3.11+ for local NLP service development

### Quick Start with Docker

1. Clone the repository:
```bash
git clone https://github.com/cetinkayabugra/Expectation-Engine.git
cd Expectation-Engine
```

2. Start all services:
```bash
docker compose up --build
```

3. Access the services:
   - **🌐 Web UI**: http://localhost:5000 (NEW! Interactive guide and demos)
   - **API**: http://localhost:5000/api
   - **Swagger UI**: http://localhost:5000/swagger
   - **NLP Service**: http://localhost:8000
   - **NLP Docs**: http://localhost:8000/docs

### Database Setup

The SQL Server container will start automatically. To initialize the schema and seed data:

1. Connect to the SQL Server instance:
   - Server: localhost,1433
   - User: sa
   - Password: YourStrong@Passw0rd

2. Execute the schema script:
```bash
docker exec -it expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -Q "CREATE DATABASE ExpectationEngine;"
```

3. Run schema.sql and seed.sql from the Database folder

Alternatively, use Entity Framework migrations (see Development section).

## Web UI - Interactive Guide

The system now includes a comprehensive **interactive web interface** that explains every step of the Equity Expectation Shift Detection System:

### 🎯 What's Included

1. **Landing Page**: Beautiful overview explaining the problem, solution, and technology
2. **Step-by-Step Guide**: Five detailed steps with expandable sections:
   - **Data Collection**: Understanding what data we collect and why
   - **Sentiment Analysis**: How FinBERT analyzes financial text
   - **Feature Engineering**: Transforming raw data into ML features
   - **Prediction Generation**: Using ML models to predict shifts
   - **Backtesting & Validation**: Testing strategies before deployment

3. **Architecture Diagrams**: Visual representations of:
   - System components and their relationships
   - Data flow through the pipeline
   - Technology stack overview

4. **Interactive Demos**: Try the system live with:
   - **Sentiment Analysis**: Test FinBERT with your own text
   - **Ticker Information**: Browse available stocks
   - **Price Data**: View historical price information

5. **Getting Started Guide**: Quick setup instructions for developers

### 🚀 Accessing the UI

Simply navigate to **http://localhost:5000** after starting the services. The UI provides:
- Clear explanations of **WHY** each step exists
- Technical details on **HOW** each component works
- Interactive examples to **TRY** the system yourself

## API Endpoints

### Tickers
- `GET /api/tickers` - Get all active tickers
- `GET /api/tickers/{id}` - Get ticker by ID
- `GET /api/tickers/symbol/{symbol}` - Get ticker by symbol
- `POST /api/tickers` - Create new ticker
- `PUT /api/tickers/{id}` - Update ticker
- `DELETE /api/tickers/{id}` - Soft delete ticker

### Prices
- `GET /api/prices/ticker/{tickerId}` - Get prices for a ticker
- `POST /api/prices` - Add single price
- `POST /api/prices/batch` - Batch add prices

### News
- `GET /api/news/ticker/{tickerId}` - Get news for a ticker
- `POST /api/news` - Add news article (auto-analyzes sentiment)
- `PUT /api/news/{id}` - Update news article

### Earnings
- `GET /api/earnings/ticker/{tickerId}` - Get earnings for a ticker
- `POST /api/earnings` - Add earnings data

### Transcripts
- `GET /api/transcripts/ticker/{tickerId}` - Get transcripts
- `POST /api/transcripts` - Add transcript (auto-analyzes sentiment)

### Features
- `GET /api/features/ticker/{tickerId}` - Get features for a ticker
- `POST /api/features/compute` - Compute features for a date

### Predictions
- `GET /api/predictions/ticker/{tickerId}` - Get predictions
- `POST /api/predictions/generate` - Generate new prediction

### Backtests
- `GET /api/backtests` - Get all backtests
- `POST /api/backtests/run` - Run a new backtest

## NLP Service

The NLP service uses ProsusAI/finbert for financial sentiment analysis.

### Endpoint

**POST /score**
```json
{
  "text": "Company reported strong quarterly earnings with revenue beating estimates"
}
```

Response:
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

## Development

### Local API Development

1. Navigate to the API directory:
```bash
cd ExpectationEngine.API
```

2. Update connection string in appsettings.Development.json

3. Run the API:
```bash
dotnet run
```

### Local NLP Service Development

1. Navigate to the NLP directory:
```bash
cd ExpectationEngine.NLP
```

2. Create virtual environment:
```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```

3. Install dependencies:
```bash
pip install -r requirements.txt
```

4. Run the service:
```bash
python main.py
```

## Database Schema

The system uses 8 main tables:
- **Tickers**: Stock symbols and company information
- **Prices**: Historical OHLCV price data
- **News**: News articles with sentiment scores
- **Earnings**: Quarterly earnings data
- **Transcripts**: Earnings call transcripts
- **Features**: Computed ML features
- **Predictions**: Model predictions
- **Backtests**: Strategy backtest results

See `ExpectationEngine.API/Database/schema.sql` for complete schema.

## Technology Stack

### Backend (API)
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server
- Swashbuckle (Swagger)

### NLP Service
- Python 3.11
- FastAPI
- Transformers (Hugging Face)
- PyTorch
- ProsusAI/finbert

### Infrastructure
- Docker & Docker Compose
- SQL Server 2022

## Configuration

Key configuration files:
- `appsettings.json` - API configuration
- `docker-compose.yml` - Container orchestration
- `schema.sql` - Database schema
- `seed.sql` - Sample data

## Contributing

This is a starter repository. Feel free to fork and customize for your needs.

## License

This project is provided as-is for educational and development purposes.
