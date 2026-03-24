using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Application.Calculators;
using NetProcalc23Feb.Application.Common.Abstractions;
using NetProcalc23Feb.Application.Mapping;
using NetProcalc23Feb.Web.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// EF Core Sqlite (file: app.db)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db"));

builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

// App services
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ChildSupportCalculatorService>();
builder.Services.AddScoped<DelinquentSupportCalculatorService>();

var app = builder.Build();

// DB init/seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    await DbSeeder.SeedAsync(db);
}

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

await app.RunAsync();
