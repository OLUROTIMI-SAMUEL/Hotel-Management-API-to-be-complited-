using FluentValidation;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Infrastructure.UnitOfWork;
using HotelManagement.Services.Services;

namespace HotelManagement.Api.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add Service Injections Here
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ILogger, Logger<TransactionService>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Add Repository Injections Here
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITransactionRepo, TransactionRepo >();
            

            // Add Model Services Injection Here


            // Add Fluent Validator Injections Here

        }
    }
}

