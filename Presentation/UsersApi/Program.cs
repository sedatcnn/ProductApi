using CaseStudy.Application.Features.CQRS.Handlers.ProductCommandHandlers;
using CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers;
using CaseStudy.Application.Tools;
using CaseStudy.Domian.Interfaces;
using CaseStudy.Persistence.Context;
using CaseStudy.Persistence.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using ProductApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// --- CORS ---
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(_ => true)
               .AllowCredentials();
    });
});

// --- Redis ---
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "UserApi_";
});

// --- Rate Limiter ---
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("global", fixedOptions =>
    {
        fixedOptions.PermitLimit = 5;
        fixedOptions.Window = TimeSpan.FromSeconds(1);
        fixedOptions.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        fixedOptions.QueueLimit = 0;
    });
    options.OnRejected = (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        return new ValueTask();
    };
});

// --- Serilog ---
builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
                 .Enrich.FromLogContext()
                 .WriteTo.Console()
                 .WriteTo.Seq("http://localhost:7270"));

// ** NOT: JWT doðrulama kodlarý bu kýsýmdan kaldýrýldý. **

// --- DbContext ---
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- DI ---
builder.Services.AddScoped(typeof(IProductRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IWithRepository), typeof(WithRepository));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<TokenService>();

// --- Handler'lar (CQRS) ---
builder.Services.AddScoped<CreateUserCommandHandler>();
builder.Services.AddScoped<UpdateUserCommandHandler>();
builder.Services.AddScoped<DeleteUserCommandHandler>();
builder.Services.AddScoped<GetAllUsersQueryHandler>();
builder.Services.AddScoped<GetUserByIdQueryHandler>();
builder.Services.AddScoped<GetCheckAppUsersQueryHandlers>();

builder.Services.AddScoped<IEventBus, RabbitMQEventBus>();
builder.Services.AddControllers();

// --- Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ** NOT: `app.UseAuthentication()` ve `app.UseAuthorization()` kaldýrýldý. **
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRateLimiter();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();