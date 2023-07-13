using AMVA.JwtAuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Tracing.Butterfly;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddOcelot().AddButterfly(option =>
{
    //this is the url that the butterfly collector server is running on...
    option.CollectorUrl = "http://localhost:9618";
    option.Service = "Ocelot";
});



builder.Services.AddCustomJwtAuthenticationApp();



builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
