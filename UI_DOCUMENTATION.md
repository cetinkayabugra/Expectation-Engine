# Web UI Documentation

## Overview

The Expectation Engine Web UI provides an interactive, educational interface that explains every aspect of the Equity Expectation Shift Detection System. It's designed to help users understand not just WHAT the system does, but WHY each component exists and HOW it works.

## Features

### 1. Interactive Landing Page

The landing page immediately communicates the core value proposition:
- **The Problem**: Stock prices reflect expectations, and traditional analysis misses expectation shifts
- **Our Solution**: Multi-source data analysis to detect shifts early
- **The Technology**: Modern stack with ASP.NET Core 8, Python FastAPI, and FinBERT

### 2. Step-by-Step System Explanation

Each of the five main steps is presented as an expandable card with three sections:

#### Step 1: Data Collection
- **What We Collect**: Stock prices, news, earnings data, and transcripts
- **Why This Matters**: Different data sources capture different aspects of expectations
- **How It Works**: SQL Server database with 8 tables and REST API endpoints

#### Step 2: Sentiment Analysis
- **What We Analyze**: Financial text using the FinBERT AI model
- **Why This Matters**: Text contains forward-looking information that numbers can't capture
- **How It Works**: Automatic NLP service integration via Python FastAPI

#### Step 3: Feature Engineering
- **What We Compute**: Price features, sentiment features, earnings features, and market features
- **Why This Matters**: ML models need structured, normalized numerical features
- **How It Works**: Feature computation endpoints that transform raw data

#### Step 4: Prediction Generation
- **What We Predict**: Price changes with confidence scores using ML models
- **Why This Matters**: Enable proactive decision-making before market fully adjusts
- **How It Works**: ML model inference (stub implementation ready for customization)

#### Step 5: Backtesting & Validation
- **What We Test**: Historical performance, risk metrics, and strategy effectiveness
- **Why This Matters**: Validate models before risking capital
- **How It Works**: Backtest execution over historical periods with comprehensive metrics

### 3. Architecture Visualization

Visual diagrams show:
- **Component Architecture**: User Interface → API Service → Database/NLP Service
- **Technology Stack**: Visual badges for .NET 8, Python, SQL Server, Docker, etc.
- **Data Flow Example**: Step-by-step flow of adding news with sentiment analysis

### 4. Interactive Demos

Three live demos allow users to interact with the actual API:

#### Demo 1: Sentiment Analysis
- Input custom financial text
- See real-time sentiment analysis from FinBERT
- View detailed scores (positive, negative, neutral)
- Example text pre-populated for quick testing

#### Demo 2: Get Ticker Information
- Fetch all available stock tickers
- See company names, sectors, and industries
- View formatted data and raw JSON

#### Demo 3: Get Historical Prices
- Enter a ticker ID
- View OHLCV price data
- See formatted price history

### 5. Getting Started Section

Quick setup guide with:
- Prerequisites checklist
- Quick start commands
- Database initialization steps
- Service access URLs

## Design Philosophy

### Educational First
Every section answers three questions:
1. **What**: What does this component do?
2. **Why**: Why does this exist in the system?
3. **How**: How does it technically work?

### Progressive Disclosure
- Overview cards provide quick understanding
- Expandable sections reveal deep technical details
- Code examples show actual API usage
- Interactive demos enable hands-on learning

### Beautiful & Modern
- Gradient backgrounds and smooth animations
- Responsive design works on all devices
- Clear typography and visual hierarchy
- Intuitive navigation and smooth scrolling

## Technical Implementation

### Architecture
- **Static Files**: Served from `wwwroot` directory in the API project
- **Single Page Application**: One HTML file with CSS and JavaScript
- **No Build Process**: Pure HTML/CSS/JS for maximum simplicity
- **API Integration**: JavaScript fetch calls to local API endpoints

### File Structure
```
ExpectationEngine.API/wwwroot/
├── index.html    # Main HTML structure
├── styles.css    # Complete styling
└── app.js        # Interactive functionality
```

### Key Technologies
- **HTML5**: Semantic markup with sections
- **CSS3**: Modern styling with flexbox/grid, gradients, animations
- **Vanilla JavaScript**: No frameworks, pure DOM manipulation
- **Fetch API**: Async calls to REST endpoints

## Customization

### Updating Content
Edit `index.html` to:
- Modify explanatory text
- Add new steps or sections
- Change examples

### Styling Changes
Edit `styles.css` to:
- Adjust color scheme (CSS variables at top)
- Modify layouts and spacing
- Change animations and transitions

### Adding Features
Edit `app.js` to:
- Add new demo functions
- Integrate additional API endpoints
- Create new interactive elements

## Browser Compatibility

The UI works in all modern browsers:
- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+

## Accessibility

- Semantic HTML structure
- Keyboard navigation support
- High contrast color scheme
- Readable font sizes
- ARIA labels where appropriate

## Performance

- **Fast Load**: No external dependencies
- **Lightweight**: ~47KB total (HTML + CSS + JS)
- **Efficient**: Minimal DOM manipulation
- **Responsive**: Smooth animations and transitions

## Future Enhancements

Potential improvements:
- [ ] Add data visualization charts (e.g., Chart.js)
- [ ] Create an admin dashboard for data management
- [ ] Add user authentication and personalization
- [ ] Implement WebSocket for real-time updates
- [ ] Add more interactive tutorials and walkthroughs
- [ ] Create a dark mode toggle
- [ ] Add print-friendly styles
- [ ] Implement multilingual support

## Troubleshooting

### UI Not Loading
- Ensure API is running on port 5000
- Check that wwwroot folder contains all three files
- Verify Program.cs has `UseStaticFiles()` and `UseDefaultFiles()`

### Demos Not Working
- **Sentiment Analysis**: Verify NLP service is running on port 8000
- **Tickers/Prices**: Ensure database is initialized with seed data
- **CORS Errors**: Check that CORS policy allows localhost

### Styling Issues
- Clear browser cache
- Check browser console for CSS loading errors
- Verify styles.css is in wwwroot folder

## Deployment Considerations

When deploying to production:
1. Update API URLs if not using localhost
2. Enable HTTPS for all endpoints
3. Minify CSS and JavaScript for faster loading
4. Add proper error handling and user feedback
5. Consider adding authentication if needed
6. Test on multiple devices and browsers

## Contributing

To improve the UI:
1. Follow existing code style and structure
2. Test on multiple browsers
3. Ensure mobile responsiveness
4. Update this documentation
5. Add comments for complex logic

## License

This UI is part of the Expectation Engine project and follows the same license terms.
