# 🚀 Quick Start Guide

Get the Expectation Engine up and running in 5 minutes!

## Prerequisites

Before you begin, make sure you have:
- ✅ **Docker** installed ([Get Docker](https://docs.docker.com/get-docker/))
- ✅ **Docker Compose** (included with Docker Desktop)

Check your installation:
```bash
docker --version
docker compose version
```

---

## 🎯 Start Everything (3 Commands)

### 1. Clone the Repository
```bash
git clone https://github.com/cetinkayabugra/Expectation-Engine.git
cd Expectation-Engine
```

### 2. Start All Services
```bash
docker compose up -d
```

This command will:
- 📦 Pull/build Docker images (first time only - takes 2-5 minutes)
- 🚀 Start 3 services: SQL Server, API, NLP Service
- ✅ Run in background (detached mode with `-d`)

**Wait 30 seconds** for SQL Server to fully initialize.

### 3. Initialize the Database
```bash
# On Linux/Mac:
./init-db.sh

# On Windows:
init-db.bat
```

This will create the database schema and load sample data.

---

## ✅ Verify Everything is Running

### Check Container Status
```bash
docker compose ps
```

You should see 3 containers running:
- ✅ `expectation-engine-sql` - Database
- ✅ `expectation-engine-api` - API Service  
- ✅ `expectation-engine-nlp` - NLP Service

### Test the Services

**1. Open the Web UI:**
```
http://localhost:5000
```

**2. Test the API:**
```bash
curl http://localhost:5000/api/tickers
```

**3. Test the NLP Service:**
```bash
curl -X POST http://localhost:8000/score \
  -H "Content-Type: application/json" \
  -d '{"text": "Strong quarterly earnings exceed expectations"}'
```

**4. Open Swagger Documentation:**
```
http://localhost:5000/swagger
```

---

## 📊 What's Running?

| Service | URL | Description |
|---------|-----|-------------|
| **Web UI** | http://localhost:5000 | Interactive guide and demos |
| **API** | http://localhost:5000/api | REST API endpoints |
| **Swagger** | http://localhost:5000/swagger | API documentation |
| **NLP Service** | http://localhost:8000 | Sentiment analysis |
| **NLP Docs** | http://localhost:8000/docs | FastAPI documentation |
| **Database** | localhost:1433 | SQL Server (sa/YourStrong@Passw0rd) |

---

## 🛑 Stop the Services

```bash
docker compose down
```

To also remove data volumes:
```bash
docker compose down -v
```

---

## 🔧 Troubleshooting

### Services Won't Start

**Check if ports are already in use:**
```bash
# On Linux/Mac:
lsof -i :5000
lsof -i :8000
lsof -i :1433

# On Windows:
netstat -ano | findstr :5000
netstat -ano | findstr :8000
netstat -ano | findstr :1433
```

**View service logs:**
```bash
# All services:
docker compose logs

# Specific service:
docker compose logs api
docker compose logs nlp-service
docker compose logs sqlserver
```

### Database Won't Initialize

**Wait longer:** SQL Server can take 30-60 seconds to fully start.

**Check SQL Server is ready:**
```bash
docker exec expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' -Q "SELECT 1"
```

**Manually create database:**
```bash
docker exec expectation-engine-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -Q "CREATE DATABASE ExpectationEngine;"
```

### NLP Service Takes Long to Start

The first time the NLP service starts, it needs to download the FinBERT model (~400MB). This can take 3-5 minutes depending on your internet speed.

**Check download progress:**
```bash
docker compose logs -f nlp-service
```

### API Returns Database Errors

Make sure you ran `./init-db.sh` or `init-db.bat` to initialize the database schema.

---

## 🎓 Next Steps

1. **Explore the Web UI:** http://localhost:5000
   - Learn how each component works
   - Try interactive demos
   - View architecture diagrams

2. **Read the Documentation:**
   - [README.md](README.md) - Full project documentation
   - [GETTING_STARTED.md](GETTING_STARTED.md) - Detailed setup guide
   - [API_DOCUMENTATION.md](API_DOCUMENTATION.md) - Complete API reference

3. **Try Example API Calls:**
   ```bash
   ./examples.sh
   ```

4. **Connect to Database:**
   - Server: `localhost,1433`
   - User: `sa`
   - Password: `YourStrong@Passw0rd`
   - Database: `ExpectationEngine`

---

## 💡 Pro Tips

**Run in foreground to see logs:**
```bash
docker compose up
```
Press `Ctrl+C` to stop.

**Rebuild after code changes:**
```bash
docker compose up --build
```

**View real-time logs:**
```bash
docker compose logs -f
```

**Restart a specific service:**
```bash
docker compose restart api
```

**Check resource usage:**
```bash
docker stats
```

---

## 🆘 Still Having Issues?

1. Make sure Docker is running
2. Check Docker has enough resources (4GB RAM minimum)
3. Try restarting Docker Desktop
4. Remove old containers and volumes:
   ```bash
   docker compose down -v
   docker system prune -a
   ```
   Then start fresh from step 2.

---

## 📞 Get Help

- 📖 [Full Documentation](README.md)
- 🐛 [Report Issues](https://github.com/cetinkayabugra/Expectation-Engine/issues)
- 💬 [Discussions](https://github.com/cetinkayabugra/Expectation-Engine/discussions)

---

**Ready to start? Run:** `docker compose up -d` 🚀
