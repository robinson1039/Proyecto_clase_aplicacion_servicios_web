using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Core;
using WebApplicationApi.Data;
using WebApplicationApi.Services.Abstractions;
using WebApplicationApi.Services.Implementations;

namespace WebApplicationApi
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            //  1. Cargar el archivo .env (antes de usar la configuración)
            DotNetEnv.Env.Load();


            //  2. Leer la variable desde el .env
            var envConnection = Environment.GetEnvironmentVariable("MY_DB_CONNECTION");

            //  3. Si existe, sobrescribir el valor del appsettings.json
            if (!string.IsNullOrEmpty(envConnection))
            {
                builder.Configuration["ConnectionStrings:MyConnection"] = envConnection;
            }

            //  4. Verificar qué conexión se está usando
            string? cnn = builder.Configuration.GetConnectionString("MyConnection");
            Console.WriteLine($" Usando conexión: {cnn}");

            //  5. Configurar DbContext con la conexión ya inyectada
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(cnn);
            });

            // 6. AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            // 7. Registrar servicios personalizados
            AddServices(builder);

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
        // Registrar servicios personalizados
        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRedService, RedService>();
        }

    }
}
