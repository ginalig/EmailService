using EmailService.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));

builder.Services.AddSingleton<EmailService.Repositories.IEmailRepository, EmailService.Repositories.EmailRepository>();

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();