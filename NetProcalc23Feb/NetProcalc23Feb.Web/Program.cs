using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NetProcalc23Feb.Application.Mappings;
using NetProcalc23Feb.Application.Validation;
using NetProcalc23Feb.Web.Data;
using NetProcalc23Feb.Web.Reports;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(DomainProfiles).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<PartyValidator>();

builder.Services.AddScoped<IReportService, PdfReportService>();

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
