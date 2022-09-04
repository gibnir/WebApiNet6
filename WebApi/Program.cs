
// http://www.omdbapi.com/?i=tt3896198&apikey=yourapikey

using Newtonsoft.Json.Serialization;
using Microsoft.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
    var resolver = options. SerializerSettings.ContractResolver;
    if (resolver != null)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        (resolver as DefaultContractResolver).NamingStrategy = null;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
});

builder.Services.AddHttpClient("Movie", a => {
    a.BaseAddress = new Uri("http://www.omdbapi.com/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.UseHttpsRedirection();

// app.UseRouting();

app.UseAuthorization();

// original
app.MapControllers();
/*
app.UseEndpoints(Endpoints => {
    Endpoints.MapControllers();
});
*/
app.Run();
