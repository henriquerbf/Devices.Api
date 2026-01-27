using Devices.Application.UseCases.Devices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateDeviceUseCase>();
            services.AddScoped<GetDeviceByIdUseCase>();
            services.AddScoped<GetDevicesUseCase>();
            services.AddScoped<GetDevicesByBrandUseCase>();
            services.AddScoped<GetDevicesByStateUseCase>();
            services.AddScoped<UpdateDeviceUseCase>();
            services.AddScoped<PatchDeviceUseCase>();
            services.AddScoped<DeleteDeviceUseCase>();

            return services;
        }
    }
}
