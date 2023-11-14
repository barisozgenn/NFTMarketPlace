FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
# this is a directory inside Docker. It's not a directory inside our file system so name it as you want
WORKDIR /app
# expose the ports internally inside Docker.
# And our containers for each of our different services are going to be running on Port 80.
EXPOSE 80

# Copy the solution file and project files for efficient caching
# So the first part is this is taking it from our development machine's file system and copying this file into our working directory inside Docker.
COPY NFTMarketPlace.sln NFTMarketPlace.sln
COPY src/NFTAuctionService/NFTAuctionService.csproj src/NFTAuctionService/NFTAuctionService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj

# Restore package dependencies with dotnet command
RUN dotnet restore NFTMarketPlace.sln

# Copy the application folders
# And we also need to do the same for the contracts because auction service has a dependency on the contracts projects as well.
COPY src/NFTAuctionService src/NFTAuctionService
# this one dependency classlib project so we need to copy it
COPY src/Contracts src/Contracts
# Set the working directory to the NFTAuctionService project
WORKDIR /app/src/NFTAuctionService
# Publish the application with dotnet command
# at this stage we want to get a runtime image and we don't need to use the SDK, which is quite big for that.
# We can use Net runtime directly once we've got to this stage and we've got a published version of our nftauction service.
RUN dotnet publish -c Release -o /app/src/out

# Build the runtime image. This gives us access to the much smaller .Net runtime.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Set the working directory inside the container
WORKDIR /app
# Copy the published output from the build image to the runtime image
# From build and put it into the out directory of our current working directory.
# we're going to have app source out and all of the published files inside there
COPY --from=build /app/src/out .
# And Specify the entry point for the container
ENTRYPOINT [ "dotnet", "NFTAuctionService.dll" ]