#For more comment and detail explanation, check NFTAuctionService dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

# copy all .csproj files and restore as distinct layers.   Use of the same COPY command
# for every dockerfile in the project to take advantage of docker caching
COPY NFTMarketPlace.sln NFTMarketPlace.sln
COPY src/NFTAuctionService/NFTAuctionService.csproj src/NFTAuctionService/NFTAuctionService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj

# Restore package deps
RUN dotnet restore NFTMarketPlace.sln

# Copy the app folders over and in this project there is no dependencies so we do not add another services or classlib here
COPY src/IdentityService src/IdentityService
WORKDIR /app/src/IdentityService
RUN dotnet publish -c Release -o /app/src/out

# Build runtime image
# IdentityService should run on dotnet 7 because still dotnet 8 not avaible for it
# also we created this microservice with dotnet 7
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "IdentityService.dll" ]


