using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.Host;
using ConfirmationService.Infrastructure.EntityFramework;
using ConfirmationService.Infrastructure.MailConnectService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<MailConnectConfiguration>(builder.Configuration.GetSection("MailConnect"));
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "DCS.Host", Version = "v1" }); });
builder.Services.AddScoped(isp =>
{
    var configuration = isp.GetRequiredService<IOptions<MailConnectConfiguration>>();
    return new MailConnect(configuration.Value);
});
builder.Services.AddScoped<MailSendService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddDbContext<ConfirmServiceContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("ConfirmationServiceDb")));


builder.Services.AddSingleton(async isp =>
{
    var dbContext = isp.GetRequiredService<ConfirmServiceContext>();
    var clientTokens = await dbContext.Users.ToListAsync();
    var tokens = clientTokens.Select(x => new UserTokens.UserToken
    {
        Token = x.Token,
        CompanyName = x.CompanyName
    }).ToList();
    return new UserTokens(tokens);
});

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DCS.Host v1"));


app.Run();