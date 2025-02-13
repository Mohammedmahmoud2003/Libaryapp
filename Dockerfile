FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# نسخ الملفات إلى داخل الحاوية
COPY . ./

# عرض إصدار .NET للتحقق من الإعدادات
RUN dotnet --version

# استعادة الحزم (Restore)
RUN dotnet restore

# بناء المشروع (Build)
RUN dotnet publish -c Release -o /publish

# صورة التشغيل النهائية
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# تشغيل التطبيق
CMD ["dotnet", "LibrarySystem.Api.dll"]

