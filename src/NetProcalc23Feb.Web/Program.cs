using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Web.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var cs = builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db";
    opt.UseSqlite(cs);
});

var app = builder.Build();

// initdb
if (args.Contains("--initdb"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Minimal API: frequency convert
app.MapPost("/api/frequency/convert", (decimal amount, int from) =>
{
    var f = (NetProcalc23Feb.Domain.Enums.Frequency)from;
    var m = NetProcalc23Feb.Application.Services.FrequencyConverter.ToMonthly(amount, f);
    return Results.Ok(new { monthly = m });
});

app.Run();
