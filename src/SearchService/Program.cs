using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

//let move DB initializer code to DbInitializer.cs
/*
await DB.InitAsync("SearchNftAuctionDb",
                MongoClientSettings.FromConnectionString(builder.Configuration.
                                                        GetConnectionString("MongoDbBarisDevConnection")));//mongodb://username:password@hostname:port
//create an index for our item for the certain fields that we want to be able to search on.
await DB.Index<NFTAuctionItem>()
        .Key(k => k.Name, KeyType.Text)
        .Key(k => k.Collection, KeyType.Text)
        .Key(k => k.Tags, KeyType.Text)
        .Key(k => k.Artist, KeyType.Text)
        .CreateAsync();
*/
try{
    await DbInitializer.InitDb(app: app);
}
catch(Exception ex){
    Console.WriteLine(ex);
}

app.Run();

