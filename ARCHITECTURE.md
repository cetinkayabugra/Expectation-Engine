# System Architecture

## Overview
```
┌─────────────────────────────────────────────────────────────┐
│                     Docker Compose                          │
│                                                              │
│  ┌────────────────┐  ┌──────────────┐  ┌────────────────┐  │
│  │   SQL Server   │  │   API        │  │  NLP Service   │  │
│  │                │  │              │  │                │  │
│  │  Port: 1433    │  │  Port: 5000  │  │  Port: 8000    │  │
│  │                │  │              │  │                │  │
│  │  ExpectationDB │◄─┤  ASP.NET 8   │◄─┤  FastAPI       │  │
│  │                │  │  + EF Core   │  │  + FinBERT     │  │
│  │  - Tickers     │  │              │  │                │  │
│  │  - Prices      │  │  Swagger UI  │  │  Sentiment     │  │
│  │  - News        │  │              │  │  Analysis      │  │
│  │  - Earnings    │  │              │  │                │  │
│  │  - Transcripts │  │              │  │                │  │
│  │  - Features    │  │              │  │                │  │
│  │  - Predictions │  │              │  │                │  │
│  │  - Backtests   │  │              │  │                │  │
│  └────────────────┘  └──────────────┘  └────────────────┘  │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

## Component Details

### 1. SQL Server Database
- **Image**: mcr.microsoft.com/mssql/server:2022-latest
- **Port**: 1433
- **Database**: ExpectationEngine
- **Credentials**: 
  - User: sa
  - Password: YourStrong@Passw0rd
- **Tables**: 8 main tables with relationships

### 2. API Service (ASP.NET Core 8)
- **Port**: 5000
- **Framework**: .NET 8
- **ORM**: Entity Framework Core
- **Documentation**: Swagger/OpenAPI at `/swagger`
- **Controllers**: 8 REST controllers
- **Services**: 9 service interfaces with implementations
- **Features**:
  - CRUD operations for all entities
  - Automatic sentiment analysis integration
  - Stub implementations for ML features
  - CORS enabled for development

### 3. NLP Service (Python FastAPI)
- **Port**: 8000
- **Framework**: FastAPI
- **Model**: ProsusAI/finbert
- **Endpoint**: POST /score
- **Documentation**: OpenAPI docs at `/docs`
- **Features**:
  - Financial sentiment analysis (positive/negative/neutral)
  - Automatic model loading on startup
  - Health check endpoints

## Data Flow

### Adding News with Sentiment Analysis
```
User → POST /api/news → API Service
                         ↓
                    NewsService
                         ↓
                    SentimentService (HTTP Client)
                         ↓
                    POST /score → NLP Service
                         ↓
                    FinBERT Analysis
                         ↓
                    Sentiment Result (label, score)
                         ↓
                    Save to Database with sentiment
```

### Generating Predictions
```
User → POST /api/predictions/generate → API Service
                                         ↓
                                    PredictionService
                                         ↓
                                    Get Features from DB
                                         ↓
                                    [Stub: ML Model Inference]
                                         ↓
                                    Save Prediction to DB
                                         ↓
                                    Return Prediction
```

## API Endpoints Summary

### Tickers
- GET /api/tickers
- GET /api/tickers/{id}
- GET /api/tickers/symbol/{symbol}
- POST /api/tickers
- PUT /api/tickers/{id}
- DELETE /api/tickers/{id}

### Prices
- GET /api/prices/ticker/{tickerId}
- POST /api/prices
- POST /api/prices/batch

### News
- GET /api/news/ticker/{tickerId}
- POST /api/news (auto sentiment)
- PUT /api/news/{id}

### Earnings
- GET /api/earnings/ticker/{tickerId}
- POST /api/earnings
- PUT /api/earnings/{id}

### Transcripts
- GET /api/transcripts/ticker/{tickerId}
- POST /api/transcripts (auto sentiment)
- PUT /api/transcripts/{id}

### Features
- GET /api/features/ticker/{tickerId}
- POST /api/features
- POST /api/features/compute

### Predictions
- GET /api/predictions/ticker/{tickerId}
- POST /api/predictions
- POST /api/predictions/generate

### Backtests
- GET /api/backtests
- POST /api/backtests
- POST /api/backtests/run

### NLP Service
- POST /score

## Technology Stack

### Backend
- **Language**: C# 12
- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server 2022
- **API Documentation**: Swashbuckle (Swagger)

### NLP Service
- **Language**: Python 3.11
- **Framework**: FastAPI
- **ML Library**: Transformers (Hugging Face)
- **Model**: ProsusAI/finbert
- **Deep Learning**: PyTorch

### Infrastructure
- **Containerization**: Docker
- **Orchestration**: Docker Compose
- **Networking**: Bridge network for inter-container communication

## Database Schema

### Tickers Table
```
Id (PK), Symbol (UNIQUE), CompanyName, Sector, Industry, 
Exchange, IsActive, CreatedAt, UpdatedAt
```

### Prices Table
```
Id (PK), TickerId (FK), Date, OpenPrice, HighPrice, LowPrice, 
ClosePrice, AdjustedClose, Volume, CreatedAt
UNIQUE: (TickerId, Date)
```

### News Table
```
Id (PK), TickerId (FK), Title, Content, Source, PublishedAt, 
Url, SentimentScore, SentimentLabel, CreatedAt
```

### Earnings Table
```
Id (PK), TickerId (FK), FiscalYear, FiscalQuarter, ReportDate,
Revenue, NetIncome, EPS, EPSEstimate, RevenueSurprise, 
EPSSurprise, CreatedAt
UNIQUE: (TickerId, FiscalYear, FiscalQuarter)
```

### Transcripts Table
```
Id (PK), EarningsId (FK), TickerId (FK), TranscriptText, 
TranscriptDate, SentimentScore, SentimentLabel, KeyPhrases, 
CreatedAt
```

### Features Table
```
Id (PK), TickerId (FK), FeatureDate, PriceReturn5D, 
PriceReturn20D, Volatility20D, Volume20DAvg, NewsSentiment7D,
NewsCount7D, EarningsSurprise, TranscriptSentiment, 
MarketCapChange, SectorMomentum, CreatedAt
UNIQUE: (TickerId, FeatureDate)
```

### Predictions Table
```
Id (PK), TickerId (FK), PredictionDate, TargetDate, 
PredictedReturn, Confidence, ModelVersion, FeatureSnapshot,
ActualReturn, CreatedAt
```

### Backtests Table
```
Id (PK), BacktestName, ModelVersion, StartDate, EndDate,
TotalReturn, SharpeRatio, MaxDrawdown, WinRate, TotalTrades,
AvgTradeReturn, Parameters, Results, CreatedAt
```

## Service Architecture

### Service Layer Pattern
```
Controller → Service Interface → Service Implementation → DbContext → Database
```

### Services Implemented
1. **TickerService**: Ticker CRUD operations
2. **PriceService**: Price data management
3. **NewsService**: News with auto sentiment
4. **EarningService**: Earnings data management
5. **TranscriptService**: Transcripts with auto sentiment
6. **FeatureService**: Feature engineering (stub)
7. **PredictionService**: ML predictions (stub)
8. **BacktestService**: Strategy backtesting (stub)
9. **SentimentService**: NLP service integration

## Development vs Production

### Current State (Development/Starter)
- No authentication
- Default credentials in docker-compose
- Stub implementations for ML features
- CORS allows all origins
- Development environment settings

### Production Recommendations
- Implement JWT/OAuth2 authentication
- Use Azure Key Vault or similar for secrets
- Implement actual ML models for predictions
- Implement feature engineering logic
- Restrict CORS to specific origins
- Add rate limiting
- Add logging and monitoring
- Use production database with backups
- Implement CI/CD pipeline
- Add comprehensive unit/integration tests

## Extension Points

### Easy to Extend
1. Replace stub ML implementations with actual models
2. Add more features to the Features table
3. Implement additional endpoints
4. Add authentication and authorization
5. Add data validation and business rules
6. Integrate external data sources
7. Add caching layer (Redis)
8. Add message queue (RabbitMQ/Kafka)
9. Implement real-time updates (SignalR)
10. Add comprehensive logging (Serilog)
