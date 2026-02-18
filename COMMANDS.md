# Command Reference

Quick reference for all Expectation Engine commands.

## 🚀 Starting Services

### One-Command Startup (Recommended)
```bash
./start.sh          # Linux/Mac
start.bat           # Windows
```
Starts all services and initializes the database automatically.

### Manual Startup
```bash
# Start services
docker compose up -d

# Initialize database (wait 30 seconds first)
./init-db.sh        # Linux/Mac
init-db.bat         # Windows
```

### Start with Logs (Foreground)
```bash
docker compose up
# Press Ctrl+C to stop
```

### Rebuild and Start
```bash
docker compose up --build -d
```

---

## 🛑 Stopping Services

### One-Command Stop
```bash
./stop.sh           # Linux/Mac
stop.bat            # Windows
```

### Manual Stop
```bash
# Stop services
docker compose down

# Stop and remove data volumes
docker compose down -v
```

---

## 🔍 Checking Status

### Container Status
```bash
docker compose ps
```

### View Logs
```bash
# All services
docker compose logs

# Specific service
docker compose logs api
docker compose logs nlp-service
docker compose logs sqlserver

# Follow logs (real-time)
docker compose logs -f
docker compose logs -f api
```

### Check Resource Usage
```bash
docker stats
```

---

## 🔄 Restarting Services

### Restart All
```bash
docker compose restart
```

### Restart Specific Service
```bash
docker compose restart api
docker compose restart nlp-service
docker compose restart sqlserver
```

---

## 🧪 Testing Services

### Test API
```bash
# Get all tickers
curl http://localhost:5000/api/tickers

# Get specific ticker
curl http://localhost:5000/api/tickers/symbol/AAPL

# Get prices
curl http://localhost:5000/api/prices/ticker/1
```

### Test NLP Service
```bash
curl -X POST http://localhost:8000/score \
  -H "Content-Type: application/json" \
  -d '{"text": "Strong quarterly earnings exceed expectations"}'
```

### Run Example Script
```bash
./examples.sh       # Runs multiple API examples
```

---

## 🗄️ Database Commands

### Connect to Database
```bash
docker exec -it expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd'
```

### Run SQL Query
```bash
docker exec expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -Q "SELECT * FROM ExpectationEngine.dbo.Tickers"
```

### Reinitialize Database
```bash
# Drop and recreate
docker exec expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -Q "DROP DATABASE ExpectationEngine"

# Then reinitialize
./init-db.sh        # Linux/Mac
init-db.bat         # Windows
```

---

## 🧹 Cleanup

### Remove Containers
```bash
docker compose down
```

### Remove Containers and Volumes
```bash
docker compose down -v
```

### Full Cleanup (Remove Everything)
```bash
docker compose down -v
docker system prune -a
# WARNING: This removes ALL unused Docker resources
```

---

## 🛠️ Development Commands

### Build Specific Service
```bash
docker compose build api
docker compose build nlp-service
```

### Run API Locally (without Docker)
```bash
cd ExpectationEngine.API
dotnet run
```

### Run NLP Service Locally (without Docker)
```bash
cd ExpectationEngine.NLP
python -m venv venv
source venv/bin/activate  # Windows: venv\Scripts\activate
pip install -r requirements.txt
python main.py
```

### Verify Configuration
```bash
./verify.sh         # Checks if all files are present
```

---

## 🌐 Access URLs

| Service | URL |
|---------|-----|
| Web UI | http://localhost:5000 |
| API | http://localhost:5000/api |
| Swagger Docs | http://localhost:5000/swagger |
| NLP Service | http://localhost:8000 |
| NLP Docs | http://localhost:8000/docs |
| Database | localhost:1433 (sa/YourStrong@Passw0rd) |

---

## 💡 Tips

**Background vs Foreground:**
- Add `-d` flag to run in background (detached mode)
- Omit `-d` to see logs in real-time (foreground mode)

**Fresh Start:**
```bash
docker compose down -v
docker compose up --build -d
./init-db.sh
```

**View specific service logs:**
```bash
docker compose logs -f api | grep ERROR
```

**Check if ports are in use:**
```bash
# Linux/Mac
lsof -i :5000
lsof -i :8000
lsof -i :1433

# Windows
netstat -ano | findstr :5000
netstat -ano | findstr :8000
netstat -ano | findstr :1433
```

---

## 📚 More Information

- **Quick Start**: See [QUICKSTART.md](QUICKSTART.md)
- **Detailed Guide**: See [GETTING_STARTED.md](GETTING_STARTED.md)
- **Full Documentation**: See [README.md](README.md)
- **API Reference**: See [API_DOCUMENTATION.md](API_DOCUMENTATION.md)
