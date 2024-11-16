using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC_Task1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//dbconnection service
builder.Services.AddDbContext<ApplicationDbContext>
(
    options => options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("dbconn")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseStaticFiles();  // This is needed to serve static files from wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Emp}/{action=Index}/{id?}");

app.Run();
