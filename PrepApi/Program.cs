using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using ServiceContracts;
using Services;
using Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Entities.JwtEntity;
using PrepApi.Middlewares;
using PrepApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options=>options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//filter adding
builder.Services.AddScoped<ActionFilter>();

builder.Services.AddMemoryCache();
//builder.Services.AddOutputCache(options =>
//{
//    options.AddBasePolicy(basepolicy => basepolicy.Expire(TimeSpan.FromSeconds(60)));
//    options.AddPolicy("Expire10", policyBuilder => policyBuilder.Expire(TimeSpan.FromSeconds(30)));
//});

//builder.Services.AddResponseCaching();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var jwtSetting = new JwtSetting();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSetting);
builder.Services.AddSingleton(jwtSetting);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
       ValidateIssuer = true,
       ValidateAudience = true,
       ValidateLifetime = true,
       ValidateIssuerSigningKey = true,
       ValidIssuer = jwtSetting.Issuer,
       ValidAudience = jwtSetting.Audience,
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey))
    };
});

builder.Services.AddAuthorization(options => options.AddPolicy("AllowAdmin", policy => policy.RequireRole("Admin")));

builder.Services.AddTransient<AuthorizationMiddleware>();
builder.Services.AddSingleton<RateLimitingMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseRouting();
//app.UseOutputCache();
//app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<AuthorizationMiddleware>();
app.UseAuthMiddleware();
app.UseMiddleware<RateLimitingMiddleware>();


app.UseEndpoints(endpoints =>
 endpoints.MapControllerRoute(name: "default", pattern: "{controller=Book}/{action=Index}/{id?}")
);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
