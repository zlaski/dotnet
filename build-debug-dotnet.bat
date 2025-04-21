@echo off
:: set "CL=/MTd"
_r %~dp0build -clean
:: _r %~dp0build -configuration Debug /p:RuntimeLibrary=MTd_StaticDebug
_r %~dp0build 
