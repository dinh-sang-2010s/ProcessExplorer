mode 800

rem echo off

for /f "tokens=1 delims=" %%a in ('dir /b /s "c:\Program Files (x86)\Microsoft Visual Studio\2019\*msbuild.exe"') do (
	set KK=%%a
	goto next
)

:next

rem echo %KK%

for /f "tokens=1 delims=" %%a in ('dir /b /s *.sln') do (
	call "d:\06. Tools\RandomDateTime.exe"	
	"%KK%" /m "%%a" /t:rebuild /p:Configuration=Release
	rem %windir%\microsoft.net\framework\v4.0.30319\msbuild /m "%%a" /t:rebuild /p:Configuration=Release3.5
	rem %windir%\microsoft.net\framework\v4.0.30319\msbuild /m "%%a" /t:rebuild /p:Configuration=Release4
	call "%%a.clean.bat"
)

call "d:\06. Tools\SyncTime.exe"

@PAUSE
