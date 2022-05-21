$migration = Read-Host 'enter migration name'
dotnet ef migrations add $migration --startup-project ..\EnterpriseAssistant.Web\
dotnet ef database update --startup-project ..\EnterpriseAssistant.Web\
Read-Host "press Enter to exit"