using System.Text;
using AssetManager.Data;
using AssetManager.Helpers;
using AssetManager.Interfaces;
using AssetManager.Profile;
using AssetManager.Repository;
using AssetManager.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    services.AddControllers();
    services.AddCors();

    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    services.AddScoped<IUserService, UserService>();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAssetService, AssetService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<ILocationAssetService, LocationAssetService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<AssetRepository>();
builder.Services.AddTransient<CompanyRepository>();
builder.Services.AddTransient<LocationAssetRepository>();

var connectionString = builder.Configuration["AppSettings:ConnectionString"];
var serverVersion = ServerVersion.AutoDetect(connectionString);


var key = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"]);

var config = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json").Build();


builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<DataContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
