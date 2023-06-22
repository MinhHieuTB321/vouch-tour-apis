using Infrastructures;
using WebAPI.Middlewares;
using WebAPI;
using System.Reflection;
using Application.GlobalExceptionHandling.Utility;
using FirebaseAdmin;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Google.Apis.Auth.OAuth2;
using Domain.Entities;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructuresService(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddWebAPIService(builder.Configuration["JWTSecretKey"]!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketAPI v1"));
}
app.UseCors();

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
app.MapHealthChecks("/healthchecks");
app.UseHttpsRedirection();
// todo authentication
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

// this line tell intergrasion test
// https://stackoverflow.com/questions/69991983/deps-file-missing-for-dotnet-6-integration-tests
public partial class Program { }