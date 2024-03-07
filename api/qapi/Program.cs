using qapi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(System.Net.IPAddress.Loopback, 5270);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/getdatetime", () =>
{
    return DateTime.Now;
})
.WithName("GetDateTime")
.WithOpenApi();

app.MapGet("/DailyQuote", () =>{
    new Aniquote();
    using var db = new AniquoteContext();
    Console.WriteLine(db.DbPath);
    Console.WriteLine("Inserting a new aniquote");
    db.Add(new Aniquote { ImageLink = "AbeLincoln pix",  InfoLink="info about pic", Quote="Now is the time for all men.", Author="abe lincoln",AuthorLink="abe@wiki", DayNumber=67 });
    db.Add(new Aniquote { ImageLink = "Flinstone Manor",  InfoLink="bedrock library", Quote="Bedrock is made of rocks!", Author="fred flintstone",AuthorLink="fred@Flintone.com", DayNumber=68 });
    db.SaveChanges();

    // TODO! Handle the issue if the query returns 0 records
    var aniquote = db.Aniquote
        .Where(s => s.DayNumber == 67)
        //.Select(a => new Person())
        .First();
    

    return aniquote; //  aniquoteX("imageLink","infoLink-pixabay","now is the good time","abe lincoln", "wikiyes!");
    
});

app.Run();