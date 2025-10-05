using ContactManager.Data.Context;
using ContactManager.Data.Entities;
using ContactManager.Data.Interfaces;
using ContactManager.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Extensions;
public static class DataRegistration
{
    public static void AddDataLayer(this IServiceCollection services, string connectionString)
    {
        _ = services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        _ = services.AddScoped<IContact, ContactRepository>();
    }
}