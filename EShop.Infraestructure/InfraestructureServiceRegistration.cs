using EShop.Application.Interfaces.Repositories;
using EShop.Domain.Interfaces;
using EShop.Infraestructure.Kafka;
using EShop.Infraestructure.Persistence;
using EShop.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infraestructure
{
    public static class InfraestructureServiceRegistration
    {
        const string ESHOP_BD = "EShop_API";
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EShopDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString(ESHOP_BD)));

            // 🔧 Configuración de Kafka (puedes moverlo a appsettings.json)
            string bootstrapServers = "localhost:9093";

            // ✅ Registro de dependencias
            services.AddScoped<IEventProducer>(sp =>
                new KafkaProducerService(bootstrapServers));


            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IOrigenRepository, OrigenRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ISegundoFARepository, SegundoFARepository>();
            services.AddScoped<ISesionRepository, SesionRepository>();

            return services;
        }
    }
}
