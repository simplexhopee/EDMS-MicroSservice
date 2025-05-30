using Microsoft.EntityFrameworkCore;
using System;
using UserService.API.Extensions;
using UserService.API.Middlewares;
using UserService.Application.Commands.RegisterUser;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Auth;
using UserService.Infrastructure.DbContexts;
using UserService.Infrastructure.Repositories;
using UserService.Shared.Auth;
using UserService.Shared.MultiTenancy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 29)) 
    )
);
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

app.UseMiddleware<MultiTenantMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseMiddleware<GetCurrentUserMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
