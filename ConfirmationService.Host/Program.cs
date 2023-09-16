using System.Text.Json.Serialization;
using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.Infrastructure.EntityFramework;
using ConfirmationService.Infrastructure.MailConnectService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.Configure<MailConnectConfiguration>(builder.Configuration.GetSection("MailConnect"));
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "DCS.Host", Version = "v1" }); });
builder.Services.AddScoped(isp =>
{
    var configuration = isp.GetRequiredService<IOptions<MailConnectConfiguration>>();
    return new MailConnect(configuration.Value);
});
builder.Services.AddScoped<MailSendService>();
builder.Services.AddDbContext<ConfirmServiceContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("ConfirmationServiceDb")));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SendConfirmationService>();
builder.Services.AddScoped<MailConfirmService>();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DCS.Host v1"));

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ConfirmServiceContext>();
    await db.Database.MigrateAsync();
}

app.Run();