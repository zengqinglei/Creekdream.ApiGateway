FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 53211
EXPOSE 44376

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/Creekdream.ApiGateway/Creekdream.ApiGateway.csproj", "src/Creekdream.ApiGateway/"]
RUN dotnet restore "src/Creekdream.ApiGateway/Creekdream.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/Creekdream.ApiGateway"
RUN dotnet build "Creekdream.ApiGateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Creekdream.ApiGateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Creekdream.ApiGateway.dll"]