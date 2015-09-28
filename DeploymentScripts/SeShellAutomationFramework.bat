del /s C:\FinalTestResult\*.xml 
cd ..
cd SeShellTest
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe SeShell.Test.csproj /p:configuration=debug
cd bin\debug
nunit-console /fixture:SeShell.Test.TestCases.TestSuite SeShell.Test.dll
cd ..
cd ..
cd ..
cd SeShell.Test.XMLTestResult\bin\Debug\
SeShell.Test.XMLTestResult.exe
