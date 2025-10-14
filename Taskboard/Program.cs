using Microsoft.EntityFrameworkCore;
using Taskboard.Components;
using MudBlazor.Services;
using Taskboard.Entities.Context;
using Taskboard.Services;
using Taskboard.Services.Task;
using Taskboard.Services.Update;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
//builder.Services.AddDbContext<TaskContext>(options => options.UseInMemoryDatabase("TaskTest"));
var connString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TaskContext>(options => options.UseNpgsql(connString));

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<UpdateService>();

var app = builder.Build();

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