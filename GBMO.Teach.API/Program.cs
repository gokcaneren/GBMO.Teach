using FluentValidation.AspNetCore;
using GBMO.Teach.API.Filters;
using GBMO.Teach.Application.Authentication.Extensions;
using GBMO.Teach.Application.Extensions;
using GBMO.Teach.Infrastructure.Extensions;
using GBMO.Teach.Localization.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.BuildInfrastructureServices(builder.Configuration);
builder.Services.BuildApplicationServices(builder.Configuration);

builder.Services.BuildLocalizationServices(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()));
builder.Services.AddFluentValidationAutoValidation();   

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
