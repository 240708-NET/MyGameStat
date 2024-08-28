using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure;
using MyGameStat.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5101", "https://*:7094");
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.Date.ToString("yyyy-MM-dd"))
    });
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();
//credential check
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var allowUI = "AllowUI";

builder.Services.AddCors(options => 
{  
    options.AddPolicy(name: allowUI,
                      policy  =>  
                      {  
                        //we must allow communication from any origin (.SetIsOriginAllowed (hostname => true)  checks origins individually and checks credential )
                          policy.WithOrigins("http://localhost:3000" )
                          .AllowCredentials()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });  
});  

var app = builder.Build();

app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(allowUI);

app.Run();

[ExcludeFromCodeCoverageAttribute]
public partial class Program {}