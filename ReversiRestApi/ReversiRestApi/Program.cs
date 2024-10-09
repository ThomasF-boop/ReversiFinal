using ReversiRestApi.Controllers;
using ReversiRestApi;
using ReversiRestApi.DAL;
using Microsoft.EntityFrameworkCore;
using ReversiRestApi.Repository;
using ReversiRestApi.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SpelContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("ReversiDbRestApi")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new KleurArrayConverter());
}); ;

builder.Services.AddCors(p => p.AddPolicy("corsapp", corsPolicyBuilder =>
{
    corsPolicyBuilder
        .SetIsOriginAllowed(url =>
        {
            var host = new Uri(url).Host;

            return host.Equals("localhost") || host.Equals("thomasreversi.hbo-ict.org");
        })
        .WithMethods("GET", "POST", "PUT")
        .AllowAnyHeader();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register services for dependency injection
builder.Services.AddTransient<ISpelRepository, SpelDatabaseRepository>();
builder.Services.AddTransient<SpelController>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
