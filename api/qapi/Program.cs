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
    var daynum = Aniquote.getDayNumber(DateTime.Now);
    Console.WriteLine($"daynum: {daynum}");
    using var db = new AniquoteContext();
    Console.WriteLine(db.DbPath);

    Aniquote aniquote = null;
    try{
        // if you can't get a quote with daynumber then always return the 1st quote.
        // this insures it works, even if there is only one quote in db.
        aniquote = db.Aniquote
            .Where(s => s.DayNumber == daynum)
            .First();
    }
    catch{
        aniquote = db.Aniquote
            .Where(s => s.DayNumber == 1)
            .First();
    }
    return aniquote; //  aniquoteX("imageLink","infoLink-pixabay","now is the good time","abe lincoln", "wikiyes!");
    
});

app.UseRouting();
app.UseStaticFiles();

app.Run();