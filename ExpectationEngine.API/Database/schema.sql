-- Equity Expectation Shift Detection System Database Schema

-- Tickers table: stores stock ticker information
CREATE TABLE Tickers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Symbol NVARCHAR(10) NOT NULL UNIQUE,
    CompanyName NVARCHAR(255) NOT NULL,
    Sector NVARCHAR(100),
    Industry NVARCHAR(100),
    Exchange NVARCHAR(50),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Prices table: historical price data
CREATE TABLE Prices (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TickerId INT NOT NULL FOREIGN KEY REFERENCES Tickers(Id),
    Date DATETIME2 NOT NULL,
    OpenPrice DECIMAL(18, 4) NOT NULL,
    HighPrice DECIMAL(18, 4) NOT NULL,
    LowPrice DECIMAL(18, 4) NOT NULL,
    ClosePrice DECIMAL(18, 4) NOT NULL,
    AdjustedClose DECIMAL(18, 4) NOT NULL,
    Volume BIGINT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT UQ_Ticker_Date UNIQUE (TickerId, Date)
);

-- News table: news articles related to tickers
CREATE TABLE News (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TickerId INT NOT NULL FOREIGN KEY REFERENCES Tickers(Id),
    Title NVARCHAR(500) NOT NULL,
    Content NVARCHAR(MAX),
    Source NVARCHAR(100),
    PublishedAt DATETIME2 NOT NULL,
    Url NVARCHAR(500),
    SentimentScore DECIMAL(5, 4),
    SentimentLabel NVARCHAR(20),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Earnings table: quarterly earnings data
CREATE TABLE Earnings (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TickerId INT NOT NULL FOREIGN KEY REFERENCES Tickers(Id),
    FiscalYear INT NOT NULL,
    FiscalQuarter INT NOT NULL,
    ReportDate DATETIME2 NOT NULL,
    Revenue DECIMAL(18, 2),
    NetIncome DECIMAL(18, 2),
    EPS DECIMAL(10, 4),
    EPSEstimate DECIMAL(10, 4),
    RevenueSurprise DECIMAL(10, 4),
    EPSSurprise DECIMAL(10, 4),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT UQ_Ticker_Fiscal UNIQUE (TickerId, FiscalYear, FiscalQuarter)
);

-- Transcripts table: earnings call transcripts
CREATE TABLE Transcripts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EarningsId INT NOT NULL FOREIGN KEY REFERENCES Earnings(Id),
    TickerId INT NOT NULL FOREIGN KEY REFERENCES Tickers(Id),
    TranscriptText NVARCHAR(MAX) NOT NULL,
    TranscriptDate DATETIME2 NOT NULL,
    SentimentScore DECIMAL(5, 4),
    SentimentLabel NVARCHAR(20),
    KeyPhrases NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Features table: extracted features for model training
CREATE TABLE Features (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TickerId INT NOT NULL FOREIGN KEY REFERENCES Tickers(Id),
    FeatureDate DATETIME2 NOT NULL,
    PriceReturn5D DECIMAL(10, 6),
    PriceReturn20D DECIMAL(10, 6),
    Volatility20D DECIMAL(10, 6),
    Volume20DAvg BIGINT,
    NewsSentiment7D DECIMAL(5, 4),
    NewsCount7D INT,
    EarningsSurprise DECIMAL(10, 4),
    TranscriptSentiment DECIMAL(5, 4),
    MarketCapChange DECIMAL(18, 2),
    SectorMomentum DECIMAL(10, 6),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT UQ_Ticker_FeatureDate UNIQUE (TickerId, FeatureDate)
);

-- Predictions table: model predictions
CREATE TABLE Predictions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TickerId INT NOT NULL FOREIGN KEY REFERENCES Tickers(Id),
    PredictionDate DATETIME2 NOT NULL,
    TargetDate DATETIME2 NOT NULL,
    PredictedReturn DECIMAL(10, 6) NOT NULL,
    Confidence DECIMAL(5, 4),
    ModelVersion NVARCHAR(50),
    FeatureSnapshot NVARCHAR(MAX),
    ActualReturn DECIMAL(10, 6),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Backtests table: backtest results
CREATE TABLE Backtests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BacktestName NVARCHAR(100) NOT NULL,
    ModelVersion NVARCHAR(50) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    TotalReturn DECIMAL(10, 6),
    SharpeRatio DECIMAL(10, 6),
    MaxDrawdown DECIMAL(10, 6),
    WinRate DECIMAL(5, 4),
    TotalTrades INT,
    AvgTradeReturn DECIMAL(10, 6),
    Parameters NVARCHAR(MAX),
    Results NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Create indexes for performance
CREATE INDEX IX_Prices_TickerId_Date ON Prices(TickerId, Date);
CREATE INDEX IX_News_TickerId_PublishedAt ON News(TickerId, PublishedAt);
CREATE INDEX IX_News_PublishedAt ON News(PublishedAt);
CREATE INDEX IX_Earnings_TickerId_ReportDate ON Earnings(TickerId, ReportDate);
CREATE INDEX IX_Transcripts_TickerId ON Transcripts(TickerId);
CREATE INDEX IX_Features_TickerId_FeatureDate ON Features(TickerId, FeatureDate);
CREATE INDEX IX_Predictions_TickerId_PredictionDate ON Predictions(TickerId, PredictionDate);
CREATE INDEX IX_Backtests_ModelVersion ON Backtests(ModelVersion);
