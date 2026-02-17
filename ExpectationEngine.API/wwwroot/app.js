// Toggle step card expansion
function toggleStep(element) {
    element.classList.toggle('active');
}

// Switch demo tabs
function switchTab(tabName, event) {
    // Hide all demo contents
    document.querySelectorAll('.demo-content').forEach(content => {
        content.classList.remove('active');
    });
    
    // Remove active class from all tabs
    document.querySelectorAll('.demo-tab').forEach(tab => {
        tab.classList.remove('active');
    });
    
    // Show selected content and activate tab
    document.getElementById('demo-' + tabName).classList.add('active');
    event.target.classList.add('active');
}

// Test Sentiment Analysis
async function testSentiment() {
    const input = document.getElementById('sentiment-input').value;
    const resultBox = document.getElementById('sentiment-result');
    
    if (!input.trim()) {
        resultBox.innerHTML = '<p style="color: var(--warning-color);">Please enter some text to analyze.</p>';
        return;
    }
    
    resultBox.innerHTML = '<div class="loading"></div> <span>Analyzing sentiment...</span>';
    resultBox.className = 'result-box';
    
    try {
        const response = await fetch('http://localhost:8000/score', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ text: input })
        });
        
        if (!response.ok) {
            throw new Error('Failed to analyze sentiment');
        }
        
        const data = await response.json();
        
        resultBox.className = 'result-box success';
        resultBox.innerHTML = `
            <h4>Sentiment Analysis Result:</h4>
            <div>
                <span class="sentiment-label ${data.label}">${data.label}</span>
                <p style="margin-top: 1rem;"><strong>Overall Confidence:</strong> ${(data.score * 100).toFixed(2)}%</p>
            </div>
            <div style="margin-top: 1rem;">
                <h5>Detailed Scores:</h5>
                <ul>
                    <li><strong>Positive:</strong> ${(data.scores.positive * 100).toFixed(2)}%</li>
                    <li><strong>Negative:</strong> ${(data.scores.negative * 100).toFixed(2)}%</li>
                    <li><strong>Neutral:</strong> ${(data.scores.neutral * 100).toFixed(2)}%</li>
                </ul>
            </div>
            <div style="margin-top: 1rem;">
                <h5>Raw JSON Response:</h5>
                <pre>${JSON.stringify(data, null, 2)}</pre>
            </div>
        `;
    } catch (error) {
        resultBox.className = 'result-box error';
        resultBox.innerHTML = `
            <h4>Error:</h4>
            <p>${error.message}</p>
            <p style="margin-top: 1rem; font-size: 0.9rem;">Make sure the NLP service is running on http://localhost:8000</p>
            <p style="font-size: 0.9rem;">You can start it with: <code>docker-compose up nlp-service</code></p>
        `;
    }
}

// Get All Tickers
async function getAllTickers() {
    const resultBox = document.getElementById('ticker-result');
    
    resultBox.innerHTML = '<div class="loading"></div> <span>Fetching tickers...</span>';
    resultBox.className = 'result-box';
    
    try {
        const response = await fetch('/api/tickers');
        
        if (!response.ok) {
            throw new Error('Failed to fetch tickers');
        }
        
        const data = await response.json();
        
        if (data.length === 0) {
            resultBox.innerHTML = '<p>No tickers found. Initialize the database with seed data first.</p>';
            return;
        }
        
        resultBox.className = 'result-box success';
        
        let html = '<h4>Available Tickers:</h4>';
        html += '<div style="margin-top: 1rem;">';
        
        data.forEach(ticker => {
            html += `
                <div style="background: rgba(255, 255, 255, 0.1); padding: 1rem; margin-bottom: 0.5rem; border-radius: 8px;">
                    <strong>${ticker.symbol}</strong> - ${ticker.companyName}
                    <div style="font-size: 0.9rem; margin-top: 0.3rem; opacity: 0.8;">
                        ${ticker.sector || 'N/A'} | ${ticker.industry || 'N/A'} | ${ticker.exchange || 'N/A'}
                    </div>
                    <div style="font-size: 0.85rem; margin-top: 0.3rem; opacity: 0.7;">
                        ID: ${ticker.id} | Active: ${ticker.isActive ? 'Yes' : 'No'}
                    </div>
                </div>
            `;
        });
        
        html += '</div>';
        html += '<div style="margin-top: 1rem;"><h5>Raw JSON Response:</h5><pre>' + JSON.stringify(data, null, 2) + '</pre></div>';
        
        resultBox.innerHTML = html;
    } catch (error) {
        resultBox.className = 'result-box error';
        resultBox.innerHTML = `
            <h4>Error:</h4>
            <p>${error.message}</p>
            <p style="margin-top: 1rem; font-size: 0.9rem;">Make sure the API service is running and the database is initialized.</p>
            <p style="font-size: 0.9rem;">Initialize with: <code>./init-db.sh</code> (Linux/Mac) or <code>init-db.bat</code> (Windows)</p>
        `;
    }
}

// Get Prices for a Ticker
async function getPrices() {
    const tickerId = document.getElementById('price-ticker-id').value;
    const resultBox = document.getElementById('prices-result');
    
    if (!tickerId || tickerId < 1) {
        resultBox.innerHTML = '<p style="color: var(--warning-color);">Please enter a valid ticker ID.</p>';
        return;
    }
    
    resultBox.innerHTML = '<div class="loading"></div> <span>Fetching prices...</span>';
    resultBox.className = 'result-box';
    
    try {
        const response = await fetch(`/api/prices/ticker/${tickerId}`);
        
        if (!response.ok) {
            if (response.status === 404) {
                throw new Error('No prices found for this ticker ID');
            }
            throw new Error('Failed to fetch prices');
        }
        
        const data = await response.json();
        
        if (data.length === 0) {
            resultBox.innerHTML = '<p>No price data found for this ticker. Try ticker ID 1 (Apple).</p>';
            return;
        }
        
        resultBox.className = 'result-box success';
        
        let html = `<h4>Price Data for Ticker ID ${tickerId}:</h4>`;
        html += '<div style="margin-top: 1rem;">';
        
        // Show first 5 prices
        const displayData = data.slice(0, 5);
        displayData.forEach(price => {
            const date = new Date(price.date).toLocaleDateString();
            html += `
                <div style="background: rgba(255, 255, 255, 0.1); padding: 1rem; margin-bottom: 0.5rem; border-radius: 8px;">
                    <strong>${date}</strong>
                    <div style="display: grid; grid-template-columns: repeat(2, 1fr); gap: 0.5rem; margin-top: 0.5rem; font-size: 0.9rem;">
                        <div>Open: $${price.openPrice.toFixed(2)}</div>
                        <div>High: $${price.highPrice.toFixed(2)}</div>
                        <div>Low: $${price.lowPrice.toFixed(2)}</div>
                        <div>Close: $${price.closePrice.toFixed(2)}</div>
                        <div>Volume: ${price.volume.toLocaleString()}</div>
                        <div>Adj Close: $${price.adjustedClose.toFixed(2)}</div>
                    </div>
                </div>
            `;
        });
        
        if (data.length > 5) {
            html += `<p style="opacity: 0.8; margin-top: 0.5rem;">Showing 5 of ${data.length} records...</p>`;
        }
        
        html += '</div>';
        html += '<div style="margin-top: 1rem;"><h5>Raw JSON Response (first 3 records):</h5><pre>' + JSON.stringify(data.slice(0, 3), null, 2) + '</pre></div>';
        
        resultBox.innerHTML = html;
    } catch (error) {
        resultBox.className = 'result-box error';
        resultBox.innerHTML = `
            <h4>Error:</h4>
            <p>${error.message}</p>
            <p style="margin-top: 1rem; font-size: 0.9rem;">Make sure:</p>
            <ul style="margin-left: 1.5rem; font-size: 0.9rem;">
                <li>The API service is running</li>
                <li>The database is initialized with seed data</li>
                <li>The ticker ID exists (try ID 1 for Apple)</li>
            </ul>
        `;
    }
}

// Add example text to sentiment input on load
window.addEventListener('DOMContentLoaded', () => {
    const sentimentInput = document.getElementById('sentiment-input');
    sentimentInput.value = "The company reported strong quarterly earnings with revenue beating estimates by 15%. Management expressed confidence in continued growth and announced plans to expand into new markets. Analysts are optimistic about the company's future prospects.";
    
    // Smooth scroll for navigation links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
});

// Auto-expand first step card after a delay
setTimeout(() => {
    const firstStep = document.querySelector('.step-card');
    if (firstStep) {
        firstStep.classList.add('active');
    }
}, 1000);
