FROM python:3.11-slim

WORKDIR /app
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt
RUN pip show google-generativeai

COPY review_agent.py .

ENTRYPOINT ["python", "/app/review_agent.py"]
