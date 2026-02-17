using ExpectationEngine.API.Data;
using ExpectationEngine.API.Services;
using ExpectationEngine.API.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Expectation Engine API",
        Version = "v1",
        Description = "Equity Expectation Shift Detection System API"
    });
});

// Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=ExpectationEngine;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;";
builder.Services.AddDbContext<ExpectationEngineDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register HttpClient for SentimentService
builder.Services.AddHttpClient<ISentimentService, SentimentService>();

// Register services
builder.Services.AddScoped<ITickerService, TickerService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IEarningService, EarningService>();
builder.Services.AddScoped<ITranscriptService, TranscriptService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddScoped<IBacktestService, BacktestService>();

// Configure CORS for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expectation Engine API v1");
});

// Enable static files (serves wwwroot folder)
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
