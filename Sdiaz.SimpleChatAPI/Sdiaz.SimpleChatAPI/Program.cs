using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Autofac;
using Sdiaz.SimpleChat.Business.AppSetting;
using Sdiaz.SimpleChat.Core.Entities;
using Sdiaz.SimpleChat.DataRepositories.Context;
using Sdiaz.SimpleChat.DependencyResolver;
using Sdiaz.SimpleChatAPI.Hubs;
using Autofac.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
// Configuration
var Configuration = builder.Configuration;
builder.Services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
builder.Services.AddDbContext<SimpleChatDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("SimpleChatConnectionString")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SimpleChatDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
});

// CORS
builder.Services.AddCors();

// JWT
var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// SignalR, Controllers, Swagger
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Autofac
var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterModule(new RepositoryAutofacModule1());
containerBuilder.RegisterModule(new BusinessAutofacModule1());
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.Populate(builder.Services));

// Build
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(builder =>
    builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString(), "http://localhost:4500")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chathub", options =>
    {
        options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
    });
});

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Simple Chat API V1"); });

app.Run();
