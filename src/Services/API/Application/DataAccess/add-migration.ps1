$migration = Read-Host 'enter migration name'
dotnet ef migrations add $migration --startup-project ..\Web\
dotnet ef database update --startup-project ..\Web\
Read-Host "press Enter to exit"