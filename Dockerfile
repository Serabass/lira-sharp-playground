ARG SDK_VERSION=8.0

############################### VENDORS ####################################

FROM mcr.microsoft.com/dotnet/sdk:$SDK_VERSION AS vendor
WORKDIR /app

# Копируем файлы проекта и восстанавливаем зависимости
COPY LiraSharpPlayground/LiraSharpPlayground.csproj ./LiraSharpPlayground/LiraSharpPlayground.csproj
COPY lira-sharp-playground.sln ./lira-sharp-playground.sln

RUN dotnet restore

############################### BUILD ######################################

FROM mcr.microsoft.com/dotnet/sdk:$SDK_VERSION AS build
WORKDIR /app

# Копируем все остальные файлы проекта и собираем приложение
COPY --from=vendor /app .
COPY LiraSharpPlayground ./LiraSharpPlayground
COPY lira-sharp-playground.sln ./lira-sharp-playground.sln

RUN dotnet publish -c Release -o out

############################### APP ########################################

# Используем образ Dotnet 8.0 для запуска приложения
FROM mcr.microsoft.com/dotnet/sdk:$SDK_VERSION
WORKDIR /app

COPY --from=build /app/out ./

# Указываем порт, на котором будет работать приложение
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "LiraSharpPlayground.dll"]
