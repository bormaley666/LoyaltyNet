using Microsoft.AspNetCore.Builder;
using LoyaltyNet.Web.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigureMiddlewares();
app.MapControllers();

app.Run();
