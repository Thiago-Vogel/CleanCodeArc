﻿using AppCore.Implementations;
using AppCore.Services;
using Infrastructure.DataTypes;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration _configuration)
        {
            //Infrastructure
            services.AddDbContext<EF_Context>(options => options.UseSqlServer(_configuration.GetConnectionString("SqlServer"),
               b => b.MigrationsAssembly("Infrastructure")));
            services.AddScoped(typeof(IEF_Repository<>), typeof(EF_Repository<>));

            //AppCore
            services.AddScoped(typeof(IEF_Service<>), typeof(EF_Service<>));
        }
    }
}