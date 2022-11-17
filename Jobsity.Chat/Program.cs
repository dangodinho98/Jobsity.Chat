using Jobsity.Chat.Borders;
using Jobsity.Chat.Borders.Configuration;
using Jobsity.Chat.Data;
using Jobsity.Chat.Hubs;
using Jobsity.Chat.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

var loggerConfig = new LoggerConfiguration().WriteTo.Console();
Log.Logger = loggerConfig.CreateLogger();

// Add services to the container.
var applicationConfig = builder.Configuration.GetSection(nameof(ApplicationConfig)).Get<ApplicationConfig>();
applicationConfig.Validate();
builder.Services.AddSingleton(applicationConfig);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(applicationConfig.ConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddValidators();

builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddHttpClient(Constants.StockApiClientName, c =>
{
    c.BaseAddress = new Uri(applicationConfig.BaseUrl);
    c.DefaultRequestHeaders.Add("Accept", "application/json");
}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler()
{
    AutomaticDecompression = DecompressionMethods.GZip
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chatHub");

app.MapRazorPages();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.Run();
