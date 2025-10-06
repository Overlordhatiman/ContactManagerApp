using ContactManager.Core.Helpers;
using ContactManager.Core.Interfaces;
using ContactManager.Core.Services;
using ContactManager.Core.Validators;
using ContactManager.Data.Extensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Dependencies
{
    public static class CoreRegistration
    {
        public static void AddCoreLayer(this IServiceCollection services, string connectionString)
        {
            DataRegistration.AddDataLayer(services, connectionString);

            var licenseKey = Environment.GetEnvironmentVariable("AUTOMAPPER_LICENSE");
            services.AddAutoMapper(cfg => cfg.LicenseKey = licenseKey, typeof(MappingProfile));
            services.AddValidatorsFromAssemblyContaining<ContactDtoValidator>();

            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IFileParserService, CsvFileParserService>();
        }
    }
}
