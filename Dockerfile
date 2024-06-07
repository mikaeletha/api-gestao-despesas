# Etapa 1: Imagem base do runtime do .NET 8 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Exponha as portas que sua aplicação usará
EXPOSE 8080
EXPOSE 8081

# Etapa 2: Imagem base do SDK do .NET 8 para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Argumento para definir a configuração de build (padrão é Release)
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie o arquivo de projeto e restaure as dependências
COPY ["api-gestao-despesas.csproj", "./"]
RUN dotnet restore "api-gestao-despesas.csproj"

# Copie todos os arquivos do projeto e compile a aplicação
COPY . .
RUN dotnet publish "api-gestao-despesas.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Etapa 3: Construir a imagem final usando o runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Defina o comando de inicialização
ENTRYPOINT ["dotnet", "api-gestao-despesas.dll"]
