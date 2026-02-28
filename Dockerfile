# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
COPY *.sln .
COPY API/*.csproj ./API/
COPY Domain/*.csproj ./Domain/
COPY Models/*.csproj ./Models/
COPY Repositories/*.csproj ./Repositories/
COPY Services/*.csproj ./Services/
RUN dotnet restore

# Copia o restante e compila
COPY . .
WORKDIR /app/API
RUN dotnet publish -c Release -o /out

# Estágio de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# O Render injeta a porta automaticamente, o ASP.NET Core 8 já escuta na 8080 por padrão
EXPOSE 8080
ENTRYPOINT ["dotnet", "API.dll"]