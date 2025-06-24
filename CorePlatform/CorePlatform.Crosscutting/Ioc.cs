using CorePlatform.Application.UseCases.AppointmentUseCase;
using CorePlatform.Application.UseCases.PatientUseCase;
using CorePlatform.Crosscutting.Middlewares;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;
using CorePlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CorePlatform.Crosscutting
{
    public static class Ioc
    {
        // Adiciona middlewares customizados
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }

        // Registra UseCases e serviços da camada Application
        public static IServiceCollection AddIocApplication(this IServiceCollection services)
        {
            // Exemplo:
            services.AddScoped<ICreatePatientUseCase, CreatePatientUseCase>();
            services.AddScoped<IUpdatePatientUseCase, UpdatePatientUseCase>();
            services.AddScoped<IListPatientsUseCase, ListPatientsUseCase>();
            services.AddScoped<IDeactivatePatientUseCase, DeactivatePatientUseCase>();
            services.AddScoped<ICreateAppointmentUseCase, CreateAppointmentUseCase>();
            // ...demais use cases

            return services;
        }

        // Registra repositórios e serviços da camada Infrastructure
        public static IServiceCollection AddIocInfrastructure(this IServiceCollection services)
        {
            // Exemplo:
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            // ...demais repositórios

            return services;
        }
    }
}
