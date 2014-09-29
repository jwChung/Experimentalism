@ECHO OFF
IF "%VS120COMNTOOLS%"=="" (
    SET VsVersionProperty=""
) ELSE (
    SET VsVersionProperty="/p:VisualStudioVersion=12.0"
)

"%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild" %~dp0Build.proj /v:minimal /maxcpucount /nodeReuse:false %VsVersionProperty% %*
