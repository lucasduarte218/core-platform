using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Application.UseCases.AppointmentUseCase;
using CorePlatform.Application.UseCases.PatientUseCase;
using CorePlatform.Crosscutting.Middlewares;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Infrastructure.Contexts;
using CorePlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorePlatform.Crosscutting
{
    public static class Ioc
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }

        public static IServiceCollection AddIocApplication(this IServiceCollection services)
        {
            // Patient UseCases
            services.AddScoped<ICreatePatientUseCase, CreatePatientUseCase>();
            services.AddScoped<IUpdatePatientUseCase, UpdatePatientUseCase>();
            services.AddScoped<IListPatientsUseCase, ListPatientsUseCase>();
            services.AddScoped<IDeactivatePatientUseCase, DeactivatePatientUseCase>();
            services.AddScoped<IGetPatientDashboardUseCase, GetPatientDashboardUseCase>();

            // Appointment UseCases
            services.AddScoped<ICreateAppointmentUseCase, CreateAppointmentUseCase>();
            services.AddScoped<IUpdateAppointmentUseCase, UpdateAppointmentUseCase>();
            services.AddScoped<IListAppointmentsUseCase, ListAppointmentsUseCase>();
            services.AddScoped<IDeactivateAppointmentUseCase, DeactivateAppointmentUseCase>();
            services.AddScoped<IGetAppointmentDashboardUseCase, GetAppointmentDashboardUseCase>();


            return services;
        }

        public static IServiceCollection AddIocInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            return services;
        }
    }
}
