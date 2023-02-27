﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AssetManager.Infra.Data.Context;
using AssetManager.Infra.Data.Repository;
using AssetManager.Domain.Interfaces.Repositorys;
using Microsoft.Extensions.Configuration;
using AssetManager.Application.Profiles;
using MediatR;
using AssetManager.Application.Interfaces;
using AssetManager.Application.Service;
using AutoMapper;
using System.Reflection;

namespace AssetManager.Infra.IoC
{
    public static class DependecyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<DataContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var myhandlers = AppDomain.CurrentDomain.Load("AssetManager.Application");
            services.AddMediatR(myhandlers);

            services.AddTransient<IAssetEventsService, AssetEventsService>();
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<IAssetEventsRepository, AssetEventsRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
