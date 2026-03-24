using Microsoft.EntityFrameworkCore;
using Serilog;
using NetProcalc23Feb.Web.Data;
using NetProcalc23Feb.Application.Services;
using NetProcalc23Feb.Application.Validation;
using FluentValidation;
using NetProcalc23Feb.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration).WriteTo.Console());

// DB
var cs = builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(cs));

// DI
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFrequencyConverter, FrequencyConverter>();
builder.Services.AddScoped<IAmortizer, Amortizer>();
builder.Services.AddScoped<IChildSupportCalculator, ChildSupportCalculator>();
builder.Services.AddScoped<IValidator<Party>, PartyValidator>();

var app = builder.Build();

// one-time DB init if invoked with --initdb
if (args.Contains("--initdb")) {
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapControllers();

// Minimal API: frequency conversion
app.MapGet("/api/frequency/convert", (IFrequencyConverter svc, decimal amount, int from, int to) => {
    var monthly = svc.ToMonthly(amount, (Frequency)from);
    var result = svc.FromMonthly(monthly, (Frequency)to);
    return Results.Ok(new { amount, from, to, result });
});

app.MapGet("/", () => Results.Redirect("/Home"));
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();