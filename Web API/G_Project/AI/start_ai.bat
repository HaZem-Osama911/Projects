@echo off
echo Activating virtual environment...
call ..\venv\Scripts\activate.bat

echo Installing Python dependencies...
pip install -r requirements.txt

echo Starting AI Service...
uvicorn tester:app --host 127.0.0.1 --port 8000 --reload
pause
