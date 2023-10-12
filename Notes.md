# Databse Error in EF cmd execution

- Run "dotnet tools intall --global ef"
- Install properly EF packages from NuGet
- Set <InvariantGlobalization>false</InvariantGlobalization> in _project_.csproj PropertyGroup tag
- If updating databse throw error, try to check misspelled character in connection string or add "Integrated Security=False" to connection string in appsettings.json


# CMD command for setting up database and migration

- Run "dotnet tool install --global dotnet-ef"
- Run "dotnet ef migrations add migration_name_here"
- Run "dotnet ef database update"