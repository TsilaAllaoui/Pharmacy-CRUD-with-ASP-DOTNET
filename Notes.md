# Databse Error in EF cmd execution

- Run "dotnet tools intall --global ef"
- Install properly EF packages from NuGet
- Set <InvariantGlobalization>false</InvariantGlobalization> in _project_.csproj PropertyGroup tag
- If updating databse throw error, try to check misspelled character in connection string or add "Integrated Security=False" to connection string in appsettings.json
