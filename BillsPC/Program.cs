using BillsPC.Repos;
using BillsPC.Services;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(s =>
{
    var db = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    db.Open();
    return db;
});

builder.Services.AddScoped<IPokemonRepo, PokemonRepo>();
builder.Services.AddHttpClient<PokeApiService>();
builder.Services.AddScoped<PokeApiService>();

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

app.MapRazorPages();
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // last
app.Run();
