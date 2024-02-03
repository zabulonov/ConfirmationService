using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading;
using ConfirmationService.BusinessLogic;
using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.Host;
using ConfirmationService.Host.Authorization;
using ConfirmationService.Infrastructure.EntityFramework;
using ConfirmationService.Infrastructure.MailConnectService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.Configure<MailConnectConfiguration>(builder.Configuration.GetSection("MailConnect"));
<<<<<<< HEAD
builder.Services.ConfigureSwagger();
=======
//TODO - настройка свагера занимает пол файла, вынести
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Email Confirmation Service API",
        Version = "v1",
        Description = "An API to perform Employee operations",
        Contact = new OpenApiContact
        {
            Name = "Alexey Zabulonov",
            Email = "zabulonov444@yandex.ru",
            Url = new Uri("https://t.me/ulove1337"),
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://github.com/zabulonov/ConfirmationService/blob/main/LICENSE"),
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "MyToken",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
>>>>>>> 7b107bc (Add ConfirmStatusByMail html table)
builder.Services.AddScoped(isp =>
{
    var configuration = isp.GetRequiredService<IOptions<MailConnectConfiguration>>();
    return new MailConnect(configuration.Value);
});
builder.Services.AddScoped<MailSendService>();
<<<<<<< HEAD
builder.Services.AddDbContext<ConfirmServiceContext>(o => {
    var env = builder.Environment.EnvironmentName;
    Console.WriteLine($"Env is {env}");
    var connectionString = builder.Configuration.GetConnectionString("ConfirmationServiceDb");
    Console.WriteLine($"Connection string is {connectionString}");
    o.UseNpgsql(connectionString);
});
=======
// TODO - Sleep, это костыль, нужен для того, чтобы compose-up нормально запускался
Thread.Sleep(1000);
builder.Services.AddDbContext<ConfirmServiceContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("ConfirmationServiceDb")));
>>>>>>> 7b107bc (Add ConfirmStatusByMail html table)

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MailConfirmService>();
builder.Services.AddAuthentication(MyAuthenticationOptions.DefaultScheme)
    .AddScheme<MyAuthenticationOptions, MyAuthenticationHandler>(MyAuthenticationOptions.DefaultScheme,
        _ => { });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DCS.Host v1"));

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ConfirmServiceContext>();
    await dbContext.Database.MigrateAsync();
    var users = dbContext.Users.ToList();
    var tokens = users.Select(x => new UserTokens.UserToken
    {
        Token = x.Token,
        CompanyName = x.CompanyName
    }).ToList();
    UserTokens.Initialize(tokens);
}

app.Run();