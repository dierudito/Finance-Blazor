using FinaFlow.Api;
using FinaFlow.Api.Common.Api;
using FinaFlow.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDbContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfigurations.CorsPolicyName);
app.MapEndpoints();

app.Run();
