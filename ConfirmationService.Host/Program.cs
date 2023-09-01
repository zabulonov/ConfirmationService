using ConfirmationService.BusinessLogic;
using ConfirmationService.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<MailConnect>();
builder.Services.AddSingleton<MailSendService>();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.MapGet("/", () => "Hello World!");

app.Run();