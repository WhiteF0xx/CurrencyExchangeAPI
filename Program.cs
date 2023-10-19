using CurrencyExchangeAPI.Models;
using CurrencyExchangeAPI.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IExchangeServices, ExchangeServices>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:ApiUrl"]);
});

string apiKey = builder.Configuration["ApiSettings:ApiKey"];
string url = builder.Configuration["ApiSettings:ApiUrl"];

builder.Services.AddSingleton<ApiKey>(provider => () => apiKey);


/*//The services for each Model
builder.Services.AddScoped<IExchangeServices, ExchangeServices>();
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
