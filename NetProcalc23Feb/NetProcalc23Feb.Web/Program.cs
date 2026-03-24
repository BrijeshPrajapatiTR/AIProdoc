using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Application.Interfaces;
using NetProcalc23Feb.Application.Profiles;
using NetProcalc23Feb.Application.Services;
using NetProcalc23Feb.Web.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

// Services
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=netprocalc.db";
    opt.UseSqlite(conn);
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ClarionProfile>());
builder.Services.AddScoped<IClarionTxAParser, ClarionTxAParser>();

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

// Minimal API endpoint to re-import Clarion TXA on demand
app.MapPost("/admin/import-txa", async (IClarionTxAParser parser, IConfiguration cfg, AppDbContext db) =>
{
    var txaPath = cfg["Clarion:TxAPath"] ?? Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "procalc.txa");
    var (menus, procs) = await parser.ParseAsync(txaPath);
    db.Menus.RemoveRange(db.Menus);
    db.Procedures.RemoveRange(db.Procedures);
    await db.SaveChangesAsync();
    await db.Menus.AddRangeAsync(menus);
    await db.Procedures.AddRangeAsync(procs);
    await db.SaveChangesAsync();
    return Results.Ok(new { menus = menus.Count, procedures = procs.Count, txaPath });
});

app.Run();
