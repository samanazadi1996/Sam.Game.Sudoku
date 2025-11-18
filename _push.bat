@echo off
setlocal enabledelayedexpansion

:: Get current date and time
for /f "tokens=1-4 delims=/ " %%a in ("%date%") do set "date=%%c-%%a-%%b"
for /f "tokens=1-2 delims=:." %%a in ("%time%") do set "time=%%a-%%b"

set "datetime=%date%_%time%"
set "datetime=%datetime::=-%"
set "datetime=%datetime:.=-%"

echo.
set /p userInput=Do you want to push your changes? (y/n): 
echo.

set "userInput=%userInput:~0,1%"

if /I "%userInput%"=="y" (
    echo ----------------------------------------
    echo Committing changes...
    echo ----------------------------------------
    git add .
    git commit -m "Push at %datetime%"
    if errorlevel 1 (
        echo ❌ Failed to commit changes.
        exit /b 1
    )

    echo ----------------------------------------
    echo Pushing changes...
    echo ----------------------------------------
    git push
    if errorlevel 1 (
        echo ❌ Failed to push changes.
        exit /b 1
    )

    echo ✅ Changes have been pushed successfully!
) else (
    echo ⚠️ No changes were pushed.
)

endlocal
pause
