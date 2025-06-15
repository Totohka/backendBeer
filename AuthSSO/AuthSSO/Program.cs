using AuthSSO.Common.Constants;
using AuthSSO.Configurations;
using AuthSSO.Handlers;
using AuthSSO.Middlewares;
using AuthSSO.Requirements;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContextFactory<UserContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

ConfigurationServices.Configuration(builder.Services);
ConfigurationRepositories.Configuration(builder.Services);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Constants.TokenPolicy, policy => policy.Requirements.Add(new TokenRequirement()))
    .AddPolicy(Constants.RefreshPolicy, policy => policy.Requirements.Add(new RefreshRequirement()));
builder.Services.AddSingleton<IAuthorizationHandler, TokenHandler>();

//builder.Services.AddSingleton<>
builder.Services.AddHttpContextAccessor();
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("ru")
        };

        options.DefaultRequestCulture = new RequestCulture("ru", "ru");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.CustomSchemaIds(type => type.ToString());

    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "AuthSSO.xml");
    o.IncludeXmlComments(xmlPath);
});
builder.WebHost.UseUrls("http://0.0.0.0:8081", "https://0.0.0.0:8082");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization();
app.MapControllers();
app.UseMiddleware<BearerMiddleware>();
app.UseMiddleware<LanguageMiddleware>();
app.Run();
