﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Extensions;

public static class Registration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assm = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddAutoMapper(assm);
        services.AddValidatorsFromAssembly(assm);

        return services;

    }
}
