# Expectation Engine

Equity Expectation Shift Detection System - A comprehensive starter repository for detecting and predicting equity expectation shifts using machine learning and NLP.

## 🚀 Quick Start

**Get started in 3 commands:**

```bash
# 1. Clone and navigate
git clone https://github.com/cetinkayabugra/Expectation-Engine.git
cd Expectation-Engine

# 2. Start everything (use start.sh on Linux/Mac or start.bat on Windows)
./start.sh          # Linux/Mac
# OR
start.bat           # Windows

# That's it! Open http://localhost:5000 in your browser
```

**OR start manually:**
```bash
docker compose up -d     # Start services
./init-db.sh            # Initialize database (Linux/Mac)
# OR init-db.bat        # Initialize database (Windows)
```

📖 **See [QUICKSTART.md](QUICKSTART.md) for detailed instructions and troubleshooting.**

---

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

## 📍 Access the Services

Once started, you can access:

| Service | URL | Description |
|---------|-----|-------------|
| **🌐 Web UI** | http://localhost:5000 | Interactive guide and demos |
| **📚 API** | http://localhost:5000/api | REST API endpoints |
| **📖 Swagger** | http://localhost:5000/swagger | API documentation |
| **🤖 NLP Service** | http://localhost:8000 | Sentiment analysis |
| **📘 NLP Docs** | http://localhost:8000/docs | FastAPI documentation |

## 🛑 Stopping Services

```bash
# Stop all services
docker compose down

# Stop and remove data
docker compose down -v

# Or use the convenience script:
./stop.sh           # Linux/Mac
stop.bat            # Windows
```

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
