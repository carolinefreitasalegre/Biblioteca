# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a Solution e os Projetos usando os nomes REAIS das suas pastas
COPY ["Biblioteca.sln", "./"]
COPY ["Biblioteca.API/Biblioteca.API.csproj", "Biblioteca.API/"]
COPY ["Biblioteca.Client/Biblioteca.Client.csproj", "Biblioteca.Client/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj", "Services/"]

# Restaura as dependências
RUN dotnet restore

# Copia o restante dos arquivos
COPY . .

# Compila a API
WORKDIR "/src/Biblioteca.API"
RUN dotnet publish "Biblioteca.API.csproj" -c Release -o /app/publish

# Estágio de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta (Render usa 8080 por padrão no Docker)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# O nome da DLL deve ser o nome do projeto da API
ENTRYPOINT ["dotnet", "Biblioteca.API.dll"]