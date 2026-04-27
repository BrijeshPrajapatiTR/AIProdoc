using FluentValidation;
using FluentValidation.AspNetCore;
using NetProcalc.Application.Services;
using NetProcalc.Application.Validators;
using NetProcalc.Domain.Entities;
using Serilog;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PartyValidator>();

builder.Services.AddSingleton<IAmortizerService, AmortizerService>();

// demo in-memory storage
builder.Services.AddDbContextFactory<InMemoryDb>(opt => opt.UseInMemoryDatabase("NetProcalc"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

// simple in-memory db using EF Core InMemory
public class InMemoryDb : DbContext
{
    public InMemoryDb(DbContextOptions<InMemoryDb> options) : base(options) { }
    public DbSet<Party> Parties => Set<Party>();
}
