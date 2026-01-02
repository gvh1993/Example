# Example

An example web application in .NET

## URIs

- Application URI: https://localhost:5001
- Aspire dashboard: http://localhost:18888
- Seq: http://localhost:5341
- OpenAPI URI: https://localhost:5001/openapi/v1.json

## Database

### Prequisites
#### 1. Install EF Core Tools
```Powershell
dotnet tool install --global dotnet-ef
```

### Migrations

Add migration
```Powershell
dotnet ef migrations add [MigrationName] --project ./Padel.Infrastructure/Padel.Infrastructure.csproj --startup-project ./Padel.Migrations/Padel.Migrations.csproj
```

Update database
```Powershell
dotnet ef database update --project ./Padel.Infrastructure/Padel.Infrastructure.csproj --startup-project ./Padel.Migrations/Padel.Migrations.csproj
```