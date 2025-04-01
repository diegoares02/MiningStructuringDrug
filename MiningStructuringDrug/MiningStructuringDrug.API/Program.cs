using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MiningStructuringDrug.Core.Application.Common;
using MiningStructuringDrug.Core.Application.DrugIndications.Commands;
using MiningStructuringDrug.Core.Application.DrugIndications.Dtos;
using MiningStructuringDrug.Core.Application.DrugIndications.Handlers;
using MiningStructuringDrug.Core.Application.DrugIndications.Interfaces;
using MiningStructuringDrug.Core.Application.DrugIndications.Queries;
using MiningStructuringDrug.Core.Application.Users.Commands;
using MiningStructuringDrug.Core.Application.Users.Dtos;
using MiningStructuringDrug.Core.Application.Users.Handlers;
using MiningStructuringDrug.Core.Application.Users.Interfaces;
using MiningStructuringDrug.Core.Application.Users.Queries;
using MiningStructuringDrug.Core.Domain.Entities;
using MiningStructuringDrug.Core.Domain.Interfaces;
using MiningStructuringDrug.Infrastructure.Authentication;
using MiningStructuringDrug.Infrastructure.Data.Repositories;
using MiningStructuringDrug.Infrastructure.MessageDispatching;
using MiningStructuringDrug.Infrastructure.Services.DailyMed;
using MiningStructuringDrug.Infrastructure.Services.User;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureAuthentication(builder.Services, builder.Configuration);
ConfigureDependencyInjection(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var secretKey = configuration["Jwt:SecretKey"];
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? "Your_Super_Secret_Key")); // Fallback

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = key,
            };
        });

    services.AddSingleton<IAuthenticationService, AuthenticationService>();
    services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
}

void ConfigureDependencyInjection(IServiceCollection services)
{
    // Message Dispatcher
    services.AddScoped<IMessageDispatcher, MessageDispatcher>();

    // Drug Indications
    services.AddScoped<ICommandHandlerResult<CreateDrugIndicationCommand, DrugIndicationDto>, CreateDrugIndicationCommandHandler>();
    services.AddScoped<IQueryHandler<GetDrugIndicationQuery, DrugIndicationDto?>, GetDrugIndicationQueryHandler>();
    services.AddScoped<IQueryHandler<ListDrugIndicationsQuery, List<DrugIndicationDto>>, ListDrugIndicationsQueryHandler>();
    services.AddScoped<ICommandHandler<UpdateDrugIndicationCommand>, UpdateDrugIndicationCommandHandler>();
    services.AddScoped<ICommandHandler<DeleteDrugIndicationCommand>, DeleteDrugIndicationCommandHandler>();

    // Users
    services.AddScoped<ICommandHandlerResult<CreateUserCommand, UserDto>, CreateUserCommandHandler>();
    services.AddScoped<ICommandHandlerResult<AuthenticateUserCommand, AuthResponseDto?>, AuthenticateUserCommandHandler>();
    services.AddScoped<IQueryHandler<GetUserQuery, UserDto?>, GetUserQueryHandler>();
    services.AddScoped<ICommandHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
    services.AddScoped<ICommandHandler<UpdateUserRoleCommand>, UpdateUserRoleCommandHandler>();
    services.AddScoped<IUserService, UserService>();

    // Repositories
    services.AddSingleton<IDrugIndicationRepository, DrugIndicationRepository>();
    services.AddSingleton<IUserRepository, UserRepository>();

    // DailyMed Service
    services.AddScoped<IDailyMedService, DailyMedService>();
}
