# Etapa 1: Imagem base do runtime do .NET 8 para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Exponha as portas que sua aplica��o usar�
EXPOSE 8080
EXPOSE 8081

# Etapa 2: Imagem base do SDK do .NET 8 para compilar a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Argumento para definir a configura��o de build (padr�o � Release)
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie o arquivo de projeto e restaure as depend�ncias
COPY ["api-gestao-despesas.csproj", "./"]
RUN dotnet restore "api-gestao-despesas.csproj"

# Copie todos os arquivos do projeto e compile a aplica��o
COPY . .
RUN dotnet publish "api-gestao-despesas.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Etapa 3: Construir a imagem final usando o runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Defina o comando de inicializa��o
ENTRYPOINT ["dotnet", "api-gestao-despesas.dll"]
