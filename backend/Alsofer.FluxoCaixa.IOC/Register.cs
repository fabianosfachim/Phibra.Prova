using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phibra.Prova.Application.Services.Interfaces;
using Phibra.Prova.Data.Repositories.Entities;
using Phibra.Prova.Application.Services;
using Phibra.Prova.Data.Interfaces;
using Phibra.Prova.Data.Repositories;

namespace Phibra.Prova.IOC
{
    public static class Register
    {
        public static void RegisterDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            #region DependencyInjection

            //Services
            services.AddScoped<ITipoMovimentacaoServices, TipoMovimentacaoServices>();


            //Repository//
            services.AddSingleton(typeof(IEntityRepository<>), typeof(EntityBaseRepository<>));

            //Entity
            services.AddScoped<ITipoMovimentacaoRepository, TipoMovimentacaoRepository>();


            #endregion
        }
    }
}