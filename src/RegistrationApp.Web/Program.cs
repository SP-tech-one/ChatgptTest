using Microsoft.AspNetCore.Http;
using RegistrationApp.Application;
using RegistrationApp.Application.Abstractions;
using RegistrationApp.Application.Contracts.Registrations;
using RegistrationApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.MapPost("/api/registrations/validate", (RegistrationRequest request, IRegistrationService service) =>
{
    var result = service.ValidateAndCreateRecord(request);
    return result.Succeeded
        ? Results.Ok(result.Record)
        : Results.BadRequest(result.Errors);
});

app.Run();
