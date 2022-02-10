## How to run API

Configure both `EnterpriseAssistant.Web` and `EnterpriseAssistant.Identity`
to run at one time

## Swagger issues

Swagger UI may look weired when application is run on port `5001`

## Database connection

To connect to database create new
`appsettings.{MACHINE_NAME}.json` file in `.Web` project with the following content:

```json
{
  "ConnectionStrings": {
    "Npgsql": "Server=localhost;Port=5432;Database=enterprise_assistant_rba;User Id=user id;Password=password;"
  }
}
```

Replace `User Id` and `Password` with your user id and password

>To get your machine name execute
`hostname` command in terminal