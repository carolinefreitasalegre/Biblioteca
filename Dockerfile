# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos
COPY ["Biblioteca.sln", "./"]
COPY ["Biblioteca.API/Biblioteca.API.csproj", "Biblioteca.API/"]
COPY ["Biblioteca.Client/Biblioteca.Client.csproj", "Biblioteca.Client/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj", "Services/"]

# --- A MÁGICA AQUI ---
# Esse comando substitui a versão 8.0.22 pela 8.0.24 no arquivo csproj antes do restore
RUN sed -i 's/8.0.22/8.0.24/g' Biblioteca.API/Biblioteca.API.csproj

# Agora o restore deve passar sem erros de downgrade
RUN dotnet restore "Biblioteca.API/Biblioteca.API.csproj"

# Copiar o restante do código
COPY . .

WORKDIR "/src/Biblioteca.API"
RUN dotnet publish "Biblioteca.API.csproj" -c Release -o /app/publish

# Estágio de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Biblioteca.API.dll"]