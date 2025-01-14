using System.Text;
using KanbanApi.Routes;
using KanBanApplication;
using KanBanApplication.Domain.Interfaces;
using KanBanApplication.Dtos;
using KanBanApplication.InfraStructure;
using KanBanApplication.InfraStructure.Persistence;
using KanBanApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfraServices();

builder.Services.AddSwaggerGen(c =>
    {
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
                []
            }
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."

        });
    }
  );
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<KanbanContext>(options =>
    options.UseNpgsql(connectionString));



builder.Services.AddScoped<ICrudService<KanbanCardModelDto,KanbanCardDto, Guid>, KanbanCrudService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.Configure<LoginSettings>(builder.Configuration.GetSection("LoginSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:JWTSecret"] ?? string.Empty)),
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });
var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
await using (var dbContext = scope.ServiceProvider.GetRequiredService<KanbanContext>())
{
    await dbContext.Database.EnsureCreatedAsync();
}
    
app.UseAuthentication();
app.UseAuthorization();
app.AddCourseRoutes();
app.AddAuthRoutes();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin()
);

app.Run();

