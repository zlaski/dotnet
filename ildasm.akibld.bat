@echo off
setlocal EnableExtensions EnableDelayedExpansion
set "EVENT=%1"

if /i "%EVENT%" == "PreClean" (
    rem Implement PreClean
    exit /b 0
)

if /i "%EVENT%" == "PreBuildEvent" (
    rem Implement PreBuildEvent
    exit /b 0
)

if /i "%EVENT%" == "PreLinkEvent" (
    rem Implement PreLinkEvent
    exit /b 0
)

if /i "%EVENT%" == "PostBuildEvent" (
    rem Implement PostBuildEvent
    exit /b 0
)

if /i "%EVENT%" == "CustomBuildStep" (
    rem Implement CustomBuildStep
    exit /b 0
)

if /i "%EVENT%" == "TestPrep" (
    rem Implement TestPrep
    exit /b 0
)

if /i "%EVENT%" == "Test" (
    rem Implement Test
    exit /b 0
)

echo ************** INVALID BUILD EVENT: %EVENT% ********************
exit /b 4
