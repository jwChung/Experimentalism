@ECHO OFF
cd build
Verify.cmd /t:CI /p:SetApiKey=%SetApiKey%;PrivateKey=%PrivateKey%;GitHubAccount=%GitHubAccount%
