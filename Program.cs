using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Jobs.JobClass.LogOut;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PiggyBank.Interfaces;
using PiggyBank.Jobs.JobsRandomMoneyGive;
using PiggyBank.Reposiroty;
using PiggyBank.Reposiroty.Database;
using PiggyBank.Reposiroty.Entity.UserEntity;
using PiggyBank.Reposiroty.RepositoryInterface;
using PiggyBank.Servises.LoggerService;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES (DI) --------------------
builder.Services.AddControllers();

// Db
builder.Services.AddDbContext<PiggyBankDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PiggyBankDb")));

builder.Services.AddScoped<IPiggyBankDbContext>(sp =>
    sp.GetRequiredService<PiggyBankDbContext>());

// Hangfire (using PiggyBankDb)
builder.Services.AddHangfire(cfg =>
    cfg.UseSqlServerStorage(builder.Configuration.GetConnectionString("PiggyBankDb")));

builder.Services.AddHangfireServer();

// Job classes
builder.Services.AddScoped<JobLogOut>();
builder.Services.AddScoped<GiveRandomMoney>();

builder.Services.AddScoped<IErrorlogerInterface, DbErrorLogger>();
builder.Services.AddScoped<ErrorLoggingMiddleware>();

// Identity password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Your DI
builder.Services.AddScoped<IUserAuTh, UserRegistrationRepository>();
builder.Services.AddScoped<IPiggyBank, PiggyBankRepository>();
builder.Services.AddScoped<IUserBankAccount, UserBankAccount>();

// Rate Limiter
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("3-per-minute", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 3,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            AutoReplenishment = true
        });
    });
});

// -------------------- BUILD APP --------------------
var app = builder.Build();

app.UseMiddleware<ErrorLoggingMiddleware>();

// -------------------- MIDDLEWARE --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

using (var scope = app.Services.CreateScope())
{
    var recurring = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    recurring.AddOrUpdate<JobLogOut>(
        "logout-job",                 
        job => job.Run(),
        Cron.Minutely
    );
  
    recurring.AddOrUpdate<GiveRandomMoney>(
        "give-random-money-job",
        job => job.Run(),
        Cron.MinuteInterval(10) 
    );
}

// -------------------- ENDPOINTS --------------------
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
