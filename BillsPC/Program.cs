using BillsPC.Repos;
using System.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Database connection scoped per request
builder.Services.AddScoped<IDbConnection>(s =>
{
    var db = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    db.Open();
    return db;
});

builder.Services.AddScoped<IPokemonRepo, PokemonRepo>();

// Add HttpClient factory
builder.Services.AddHttpClient();

// Add Blazor Server services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapRazorPages();

app.MapControllers();

app.Run();
