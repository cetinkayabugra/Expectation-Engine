from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from transformers import AutoTokenizer, AutoModelForSequenceClassification
import torch
from typing import Dict

app = FastAPI(
    title="Expectation Engine NLP Service",
    description="Financial sentiment analysis using ProsusAI/finbert",
    version="1.0.0"
)

# Load FinBERT model and tokenizer
MODEL_NAME = "ProsusAI/finbert"
tokenizer = None
model = None

@app.on_event("startup")
async def load_model():
    """Load the model on startup"""
    global tokenizer, model
    print(f"Loading {MODEL_NAME}...")
    tokenizer = AutoTokenizer.from_pretrained(MODEL_NAME)
    model = AutoModelForSequenceClassification.from_pretrained(MODEL_NAME)
    model.eval()
    print(f"Model loaded successfully!")

class TextInput(BaseModel):
    text: str

class SentimentOutput(BaseModel):
    label: str
    score: float
    scores: Dict[str, float]

@app.get("/")
async def root():
    """Health check endpoint"""
    return {
        "service": "Expectation Engine NLP Service",
        "status": "running",
        "model": MODEL_NAME
    }

@app.get("/health")
async def health():
    """Detailed health check"""
    return {
        "status": "healthy",
        "model_loaded": model is not None and tokenizer is not None
    }

@app.post("/score", response_model=SentimentOutput)
async def score_sentiment(input_data: TextInput):
    """
    Analyze financial sentiment of text
    Returns: positive, negative, or neutral with confidence scores
    """
    if not model or not tokenizer:
        raise HTTPException(status_code=503, detail="Model not loaded yet")
    
    if not input_data.text or len(input_data.text.strip()) == 0:
        raise HTTPException(status_code=400, detail="Text cannot be empty")
    
    try:
        # Tokenize and prepare input
        inputs = tokenizer(
            input_data.text,
            return_tensors="pt",
            truncation=True,
            max_length=512,
            padding=True
        )
        
        # Get predictions
        with torch.no_grad():
            outputs = model(**inputs)
            predictions = torch.nn.functional.softmax(outputs.logits, dim=-1)
        
        # Convert to probabilities
        probs = predictions[0].tolist()
        
        # FinBERT labels: [positive, negative, neutral]
        labels = ["positive", "negative", "neutral"]
        scores_dict = {label: float(prob) for label, prob in zip(labels, probs)}
        
        # Get the label with highest probability
        max_idx = probs.index(max(probs))
        predicted_label = labels[max_idx]
        predicted_score = probs[max_idx]
        
        return SentimentOutput(
            label=predicted_label,
            score=predicted_score,
            scores=scores_dict
        )
    
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Error processing text: {str(e)}")

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
