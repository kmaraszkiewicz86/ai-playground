# Python API

A simple Python HTTP server that returns "hello world" text.

## Running the API

```bash
python3 main.py
```

The API will start on port 8001 and be available at:
- http://localhost:8001/api/hello
- http://localhost:8001/

## Response Format

```json
{
    "message": "Hello world from Python API",
    "timestamp": "2026-02-09T11:50:00.000000"
}
```
