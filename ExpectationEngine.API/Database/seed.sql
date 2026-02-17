-- Seed data for Expectation Engine

-- Insert sample tickers
SET IDENTITY_INSERT Tickers ON;

INSERT INTO Tickers (Id, Symbol, CompanyName, Sector, Industry, Exchange, IsActive, CreatedAt, UpdatedAt)
VALUES 
    (1, 'AAPL', 'Apple Inc.', 'Technology', 'Consumer Electronics', 'NASDAQ', 1, GETUTCDATE(), GETUTCDATE()),
    (2, 'MSFT', 'Microsoft Corporation', 'Technology', 'Software', 'NASDAQ', 1, GETUTCDATE(), GETUTCDATE()),
    (3, 'GOOGL', 'Alphabet Inc.', 'Technology', 'Internet Services', 'NASDAQ', 1, GETUTCDATE(), GETUTCDATE()),
    (4, 'AMZN', 'Amazon.com Inc.', 'Consumer Cyclical', 'E-Commerce', 'NASDAQ', 1, GETUTCDATE(), GETUTCDATE()),
    (5, 'TSLA', 'Tesla Inc.', 'Consumer Cyclical', 'Auto Manufacturers', 'NASDAQ', 1, GETUTCDATE(), GETUTCDATE());

SET IDENTITY_INSERT Tickers OFF;

-- Insert sample price data (last 5 days for AAPL)
INSERT INTO Prices (TickerId, Date, OpenPrice, HighPrice, LowPrice, ClosePrice, AdjustedClose, Volume, CreatedAt)
VALUES
    (1, DATEADD(day, -5, CAST(GETUTCDATE() AS DATE)), 170.50, 172.30, 169.80, 171.50, 171.50, 52000000, GETUTCDATE()),
    (1, DATEADD(day, -4, CAST(GETUTCDATE() AS DATE)), 171.60, 173.20, 171.10, 172.80, 172.80, 48000000, GETUTCDATE()),
    (1, DATEADD(day, -3, CAST(GETUTCDATE() AS DATE)), 172.90, 174.50, 172.40, 174.20, 174.20, 51000000, GETUTCDATE()),
    (1, DATEADD(day, -2, CAST(GETUTCDATE() AS DATE)), 174.30, 175.80, 173.90, 175.40, 175.40, 53000000, GETUTCDATE()),
    (1, DATEADD(day, -1, CAST(GETUTCDATE() AS DATE)), 175.50, 176.20, 174.80, 175.90, 175.90, 49000000, GETUTCDATE());

-- Insert sample news
INSERT INTO News (TickerId, Title, Content, Source, PublishedAt, Url, SentimentScore, SentimentLabel, CreatedAt)
VALUES
    (1, 'Apple announces record quarterly earnings', 'Apple Inc. reported record-breaking quarterly earnings, exceeding analyst expectations with strong iPhone sales and growing services revenue.', 'TechNews', DATEADD(day, -2, GETUTCDATE()), 'https://example.com/news/1', 0.85, 'positive', GETUTCDATE()),
    (2, 'Microsoft cloud revenue surges', 'Microsoft Corporation sees significant growth in Azure cloud services, driving overall revenue higher than expected.', 'BusinessDaily', DATEADD(day, -1, GETUTCDATE()), 'https://example.com/news/2', 0.78, 'positive', GETUTCDATE()),
    (3, 'Alphabet faces regulatory challenges', 'Alphabet Inc. is under scrutiny from regulators regarding its search engine practices and market dominance.', 'FinanceNews', DATEADD(day, -3, GETUTCDATE()), 'https://example.com/news/3', 0.35, 'negative', GETUTCDATE());

-- Insert sample earnings
INSERT INTO Earnings (TickerId, FiscalYear, FiscalQuarter, ReportDate, Revenue, NetIncome, EPS, EPSEstimate, RevenueSurprise, EPSSurprise, CreatedAt)
VALUES
    (1, 2024, 1, DATEADD(day, -30, GETUTCDATE()), 94300000000, 23600000000, 1.52, 1.42, 1200000000, 0.10, GETUTCDATE()),
    (2, 2024, 1, DATEADD(day, -28, GETUTCDATE()), 56500000000, 18700000000, 2.48, 2.35, 800000000, 0.13, GETUTCDATE()),
    (3, 2024, 1, DATEADD(day, -25, GETUTCDATE()), 76500000000, 15500000000, 1.89, 1.85, 500000000, 0.04, GETUTCDATE());

-- Insert sample transcripts
INSERT INTO Transcripts (EarningsId, TickerId, TranscriptText, TranscriptDate, SentimentScore, SentimentLabel, KeyPhrases, CreatedAt)
VALUES
    (1, 1, 'We are very pleased with our Q1 results. iPhone sales exceeded our expectations, and our services business continues to show strong momentum. We see great opportunities ahead in AI and wearables.', DATEADD(day, -30, GETUTCDATE()), 0.82, 'positive', 'strong momentum, great opportunities, exceeded expectations', GETUTCDATE()),
    (2, 2, 'Azure growth remains robust, and we are seeing increased enterprise adoption. Our investments in AI are paying off, and we expect continued strong performance.', DATEADD(day, -28, GETUTCDATE()), 0.79, 'positive', 'robust growth, strong performance, paying off', GETUTCDATE());

-- Insert sample features
INSERT INTO Features (TickerId, FeatureDate, PriceReturn5D, PriceReturn20D, Volatility20D, Volume20DAvg, NewsSentiment7D, NewsCount7D, EarningsSurprise, TranscriptSentiment, CreatedAt)
VALUES
    (1, DATEADD(day, -1, CAST(GETUTCDATE() AS DATE)), 0.0316, 0.0842, 0.0145, 50000000, 0.85, 3, 0.10, 0.82, GETUTCDATE()),
    (2, DATEADD(day, -1, CAST(GETUTCDATE() AS DATE)), 0.0245, 0.0723, 0.0132, 45000000, 0.78, 2, 0.13, 0.79, GETUTCDATE()),
    (3, DATEADD(day, -1, CAST(GETUTCDATE() AS DATE)), -0.0123, 0.0234, 0.0178, 38000000, 0.35, 1, 0.04, NULL, GETUTCDATE());

-- Insert sample predictions
INSERT INTO Predictions (TickerId, PredictionDate, TargetDate, PredictedReturn, Confidence, ModelVersion, FeatureSnapshot, ActualReturn, CreatedAt)
VALUES
    (1, DATEADD(day, -5, GETUTCDATE()), DATEADD(day, 5, GETUTCDATE()), 0.0350, 0.72, 'v1.0', '{"priceReturn5D": 0.0316, "volatility": 0.0145}', NULL, GETUTCDATE()),
    (2, DATEADD(day, -5, GETUTCDATE()), DATEADD(day, 5, GETUTCDATE()), 0.0280, 0.68, 'v1.0', '{"priceReturn5D": 0.0245, "volatility": 0.0132}', NULL, GETUTCDATE()),
    (3, DATEADD(day, -10, GETUTCDATE()), GETUTCDATE(), -0.0120, 0.65, 'v1.0', '{"priceReturn5D": -0.0123, "volatility": 0.0178}', -0.0098, GETUTCDATE());

-- Insert sample backtest
INSERT INTO Backtests (BacktestName, ModelVersion, StartDate, EndDate, TotalReturn, SharpeRatio, MaxDrawdown, WinRate, TotalTrades, AvgTradeReturn, Parameters, Results, CreatedAt)
VALUES
    ('Q1 2024 Backtest', 'v1.0', DATEADD(day, -90, GETUTCDATE()), DATEADD(day, -1, GETUTCDATE()), 0.1250, 1.85, -0.0450, 0.62, 45, 0.0028, '{"threshold": 0.6, "holdingPeriod": 5}', '{"trades": 45, "winners": 28, "losers": 17}', GETUTCDATE());
