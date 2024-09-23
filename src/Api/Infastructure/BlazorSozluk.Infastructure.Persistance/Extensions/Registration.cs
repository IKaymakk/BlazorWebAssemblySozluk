using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Infastructure.Persistance.Context;
using BlazorSozluk.Infastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlazorSozlukContext>(conf =>
        {
            var constr = configuration["BlazorSozlukDbConnectionString"].ToString();
            conf.UseSqlServer(constr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
