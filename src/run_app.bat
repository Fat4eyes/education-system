cd EducationSystem
dotnet build EducationSystem.sln
cd EducationSystem.WebApp
start http://localhost:61606/
dotnet run --project EducationSystem.WebApp.csproj