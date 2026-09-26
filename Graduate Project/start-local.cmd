@echo off
setlocal

set "PROJECT_ROOT=%~dp0"
set "PHP_EXE=C:\xampp\php\php.exe"
set "PYTHON_EXE=%PROJECT_ROOT%services\pronunciation\.venv\Scripts\python.exe"

if not exist "%PHP_EXE%" (
    echo PHP was not found at %PHP_EXE%
    echo Update PHP_EXE inside start-local.cmd if PHP is installed elsewhere.
    pause
    exit /b 1
)

if not exist "%PYTHON_EXE%" (
    echo Python environment was not found at:
    echo %PYTHON_EXE%
    echo Follow services\pronunciation\README.md to create it.
    pause
    exit /b 1
)

if /i "%~1"=="--check" (
    echo Local runtime paths are valid.
    exit /b 0
)

start "Thriving Together - Web" cmd /k "cd /d ""%PROJECT_ROOT%web"" && ""%PHP_EXE%"" artisan serve"
start "Thriving Together - Arabic" cmd /k "cd /d ""%PROJECT_ROOT%services\pronunciation\arabic"" && ""%PYTHON_EXE%"" server.py"
start "Thriving Together - English" cmd /k "cd /d ""%PROJECT_ROOT%services\pronunciation\english"" && ""%PYTHON_EXE%"" server.py"

echo.
echo Local services are starting:
echo Web:     http://127.0.0.1:8000
echo Arabic:  http://127.0.0.1:5000
echo English: http://127.0.0.1:5001
echo.
endlocal
