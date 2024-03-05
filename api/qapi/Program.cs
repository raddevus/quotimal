using qapi.Model;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/DailyQuote", () =>{
    new Aniquote();
    using var db = new AniquoteContext();
    Console.WriteLine(db.DbPath);
    Console.WriteLine("Inserting a new aniquote");
    db.Add(new Aniquote { ImageLink = "Flinstone Manor",  InfoLink="bedrock library", Quote="Bedrock is made of rocks!", Author="fred flintstone",AuthorLink="fred@Flintone.com", DayNumber=68 });
    db.SaveChanges();

    var aniquote = db.Aniquote
        .Where(s => s.DayNumber == 67)
        //.Select(a => new Person())
        .First();
    

    return aniquote; //  aniquoteX("imageLink","infoLink-pixabay","now is the good time","abe lincoln", "wikiyes!");
    
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Return image link, pixabay link, quote, author name, wikipedia link about author of quote, 
record aniquoteX(string imageLink, string infoLink, String quote, string authorName, string wikiLink){
    
}
