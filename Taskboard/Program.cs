using Microsoft.EntityFrameworkCore;
using Taskboard.Components;
using MudBlazor.Services;
using Taskboard.Entities.Context;
using Taskboard.Services;
using Taskboard.Services.Format;
using Taskboard.Services.Task;
using Taskboard.Services.Update;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using MudBlazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});
//builder.Services.AddDbContext<TaskContext>(options => options.UseInMemoryDatabase("TaskTest"));
var connString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TaskContext>(options => options.UseNpgsql(connString));

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UpdateService>();
builder.Services.AddScoped<FormatService>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

var supportedCultures = new[] { "en", "de" };
var localizedOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizedOptions);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();