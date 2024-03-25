// <copyright file="Program.cs" company="CBCanteen">
// Copyright (c) CBCanteen. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using CBCanteen.Server.Data.Data;
using CBCanteen.Server.Services;
using CBCanteen.Server.WebHost.Helpers;
using CBCanteen.Server.WebHost.Models;
using CBCanteen.Server.WebHost.SwaggerConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Tailwind;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") !, o =>
    {
        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        o.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName);
    });
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
    options.EnableThreadSafetyChecks();
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services
    .AddLogging(conf =>
    {
        conf.ClearProviders();

        // conf.AddSeq(configuration.GetSection("Seq"));
        conf.AddConsole();
    });

builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwagger();
builder.Services.AddServices();

var app = builder.Build();

await app.InitAppAsync();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.RunTailwind("tailwind", "../CBCanteen.Client.ComponentLibrary");
    app.UseWebAssemblyDebugging();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Logger.LogInformation("Starting the app.");

app.Run();
