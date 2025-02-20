# استخدم صورة .NET الرسمية
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# بناء المشروع
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LibrarySystem.Api/LibrarySystem.Api.csproj", "LibrarySystem.Api/"]
RUN dotnet restore "LibrarySystem.Api/LibrarySystem.Api.csproj"
COPY . .
WORKDIR "/src/LibrarySystem.Api"
RUN dotnet build -c Release -o /app/build

# نشر التطبيق
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# تشغيل التطبيق في بيئة الإنتاج
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibrarySystem.Api.dll"]

