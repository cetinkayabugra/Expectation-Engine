# Getting Started Guide

## ⚡ Quick Start (5 minutes)

### Option 1: One-Command Startup (Easiest!)

```bash
# Clone the repository
git clone https://github.com/cetinkayabugra/Expectation-Engine.git
cd Expectation-Engine

# Start everything with one command
./start.sh          # Linux/Mac
# OR
start.bat           # Windows
```

That's it! The script will:
- ✅ Start all services (SQL Server, API, NLP)
- ✅ Wait for SQL Server to be ready
- ✅ Initialize the database automatically
- ✅ Display all access URLs

### Option 2: Manual Startup

If you prefer to run commands manually:

```bash
# Clone the repository
git clone https://github.com/cetinkayabugra/Expectation-Engine.git
cd Expectation-Engine

# Start all services
docker compose up -d
```

This will start:
- SQL Server on port 1433
- API on port 5000
- NLP Service on port 8000

Then initialize the database (wait 30 seconds first):

**On Linux/Mac:**
```bash
./init-db.sh
```

**On Windows:**
```cmd
init-db.bat
```

## ✅ Verify the Services

Open your browser and check these URLs:

- **🌐 Web UI**: http://localhost:5000 (Interactive guide with demos)
- **📚 Swagger API Docs**: http://localhost:5000/swagger
- **🤖 NLP Service Docs**: http://localhost:8000/docs

### Command-Line Verification

**Check container status:**
```bash
docker compose ps
```

**Get all tickers:**
```bash
curl http://localhost:5000/api/tickers
```

**Test sentiment analysis:**
```bash
curl -X POST http://localhost:8000/score \
  -H "Content-Type: application/json" \
  -d '{"text": "Strong quarterly earnings exceed expectations"}'
```

## 🛑 Stopping Services

**Option 1: Use the stop script**
```bash
./stop.sh          # Linux/Mac
# OR
stop.bat           # Windows
```

**Option 2: Manual stop**
```bash
# Stop all services
docker compose down

# Stop and remove data volumes
docker compose down -v
```

**Get prices for Apple (TickerId=1):**
```bash
curl http://localhost:5000/api/prices/ticker/1
```

**Test sentiment analysis:**
```bash
curl -X POST http://localhost:8000/score \
  -H "Content-Type: application/json" \
  -d '{"text": "Company exceeded earnings expectations with strong revenue growth"}'
```

**Create a new ticker:**
```bash
curl -X POST http://localhost:5000/api/tickers \
  -H "Content-Type: application/json" \
  -d '{
    "symbol": "NVDA",
    "companyName": "NVIDIA Corporation",
    "sector": "Technology",
    "industry": "Semiconductors",
    "exchange": "NASDAQ"
  }'
```

## Development Workflow

### Working with the API

1. **View logs:**
```bash
docker-compose logs -f api
```

2. **Rebuild after code changes:**
```bash
docker-compose up -d --build api
```

3. **Run locally without Docker:**
```bash
cd ExpectationEngine.API
dotnet run
```

### Working with the NLP Service

1. **View logs:**
```bash
docker-compose logs -f nlp-service
```

2. **Rebuild after code changes:**
```bash
docker-compose up -d --build nlp-service
```

3. **Run locally without Docker:**
```bash
cd ExpectationEngine.NLP
python -m venv venv
source venv/bin/activate  # Windows: venv\Scripts\activate
pip install -r requirements.txt
python main.py
```

### Database Management

**Connect with SQL Server Management Studio or Azure Data Studio:**
- Server: localhost,1433
- Authentication: SQL Server Authentication
- Login: sa
- Password: YourStrong@Passw0rd
- Database: ExpectationEngine

**View tables:**
```sql
USE ExpectationEngine;
GO
SELECT * FROM Tickers;
SELECT * FROM Prices;
SELECT * FROM News;
```

**Reset database:**
```bash
docker-compose down -v
docker-compose up -d
./init-db.sh
```

## Common Tasks

### Add a New Ticker and Price Data

```bash
# Create ticker
curl -X POST http://localhost:5000/api/tickers \
  -H "Content-Type: application/json" \
  -d '{
    "symbol": "META",
    "companyName": "Meta Platforms Inc.",
    "sector": "Technology",
    "industry": "Social Media"
  }'

# Add price (replace TickerId with the returned ID)
curl -X POST http://localhost:5000/api/prices \
  -H "Content-Type: application/json" \
  -d '{
    "tickerId": 6,
    "date": "2024-02-17",
    "openPrice": 450.20,
    "highPrice": 455.80,
    "lowPrice": 449.50,
    "closePrice": 454.30,
    "adjustedClose": 454.30,
    "volume": 12500000
  }'
```

### Add News with Sentiment Analysis

```bash
curl -X POST http://localhost:5000/api/news \
  -H "Content-Type: application/json" \
  -d '{
    "tickerId": 1,
    "title": "Apple launches new product line",
    "content": "Apple Inc. today announced a groundbreaking new product line that is expected to drive significant revenue growth in the coming quarters. Analysts are optimistic about the company'\''s innovation strategy.",
    "source": "TechCrunch",
    "publishedAt": "2024-02-17T10:00:00Z",
    "url": "https://example.com/news"
  }'
```

The sentiment will be automatically analyzed by the NLP service.

### Generate a Prediction

```bash
curl -X POST "http://localhost:5000/api/predictions/generate?tickerId=1&targetDate=2024-03-01" \
  -H "Content-Type: application/json"
```

### Run a Backtest

```bash
curl -X POST "http://localhost:5000/api/backtests/run?backtestName=Test%20Strategy&startDate=2024-01-01&endDate=2024-02-17&modelVersion=v1.0" \
  -H "Content-Type: application/json"
```

## Troubleshooting

### Services won't start
```bash
# Check if ports are already in use
netstat -ano | findstr :5000
netstat -ano | findstr :8000
netstat -ano | findstr :1433

# Stop all containers and restart
docker-compose down
docker-compose up -d
```

### Database connection fails
```bash
# Check if SQL Server is running
docker ps | grep expectation-engine-sql

# View SQL Server logs
docker logs expectation-engine-sql

# Wait longer - SQL Server can take 30-60 seconds to start
```

### NLP service errors
```bash
# The first time the NLP service starts, it needs to download the FinBERT model
# This can take several minutes depending on your internet connection
docker logs expectation-engine-nlp

# If it fails, try rebuilding
docker-compose up -d --build nlp-service
```

### Can't access Swagger UI
- Make sure the API is running: `docker ps`
- Check API logs: `docker logs expectation-engine-api`
- Try: http://localhost:5000/swagger/index.html

## Next Steps

1. **Customize the Models**: Modify entity models in `ExpectationEngine.API/Models/`
2. **Add Business Logic**: Implement the stub methods in service classes
3. **Integrate ML Models**: Replace stub predictions with actual ML model calls
4. **Add Authentication**: Implement JWT authentication for API endpoints
5. **Set up CI/CD**: Add GitHub Actions or Azure DevOps pipelines
6. **Deploy to Cloud**: Deploy to Azure App Service, AWS, or your preferred cloud

## Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [FastAPI Documentation](https://fastapi.tiangolo.com)
- [FinBERT Model](https://huggingface.co/ProsusAI/finbert)
- [Docker Compose](https://docs.docker.com/compose)
