using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using UserService.API.Extensions;
using UserService.API.Middlewares;
using UserService.Application.Commands.RegisterUser;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Auth;
using UserService.Infrastructure.DbContexts;
using UserService.Infrastructure.Repositories;
using UserService.Shared.Auth;
using UserService.Shared.Database;
using UserService.Shared.Emailing;
using UserService.Shared.MultiTenancy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository<User, Guid>, UserRepository>();

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRepository<RefreshToken, Guid>, RefreshTokenRepository>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IEmailer, Emailer>();
builder.Services.AddScoped<IPasswordGenerator, PasswordGenerator>();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(8, 0, 29)) 
    )
);
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<UserDbContext>>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
builder.Services.AddHttpContextAccessor();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerRequirement();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GetCurrentUserMiddleWare>();
app.UseMiddleware<MultiTenantMiddleware>();


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
