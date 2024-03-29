using qapi.Model;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(System.Net.IPAddress.Loopback, 5270);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddRouting();

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
    catch(Exception ex){
        aniquote = new Aniquote{
                ImageLink = "https://pixabay.com/photos/owl-burrowing-owl-bird-animal-3649048/",
                InfoLink = "https://pixabay.com/photos/owl-burrowing-owl-bird-animal-3649048/",
                Quote = "The reasonable man adapts himself to the world: the unreasonable one persists in trying to adapt the world to himself. Therefore all progress depends on the unreasonable man.",
                Author = "George Bernard Shaw",
                AuthorLink = "https://en.wikipedia.org/wiki/George_Bernard_Shaw",
                DayNumber = 55
            };
    }
    return aniquote;
    
});
app.UseStaticFiles();

app.Run();