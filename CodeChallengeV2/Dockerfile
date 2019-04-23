FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-stretch AS build
WORKDIR /src
COPY ["CodeChallengeV2/CodeChallengeV2.csproj", "CodeChallengeV2/"]
RUN dotnet restore "CodeChallengeV2/CodeChallengeV2.csproj"
COPY . .
WORKDIR "/src/CodeChallengeV2"
RUN dotnet build "CodeChallengeV2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CodeChallengeV2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CodeChallengeV2.dll"]