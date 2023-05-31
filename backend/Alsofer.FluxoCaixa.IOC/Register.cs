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
            services.AddScoped<IMovimentacaoServices, MovimentacaoServices>();
            //services.AddScoped<ISaldoMovimentacaoServices, SaldoMovimentacaoServices>();

            //Repository//
            services.AddSingleton(typeof(IEntityRepository<>), typeof(EntityBaseRepository<>));

            //Entity
            services.AddScoped<ITipoMovimentacaoRepository, TipoMovimentacaoRepository>();
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
            services.AddScoped<ISaldoMovimentacaoRepository, SaldoMovimentacaoRepository>();

            #endregion
        }
    }
}