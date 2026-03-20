# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy just the project file first to cache dependencies
COPY ["Chuckybot.csproj", "./"]
RUN dotnet restore "Chuckybot.csproj"

# Copy the rest of the code and publish the release build
COPY . .
RUN dotnet publish "Chuckybot.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final
WORKDIR /app

# Copy the compiled output from the build stage
COPY --from=build /app/publish .

# Tell Docker how to start the bot
ENTRYPOINT ["dotnet", "Chuckybot.dll"]