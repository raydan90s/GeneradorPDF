using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.DocumentosElectronicos.Configuracion;
using Yachasoft.Sri.FacturacionElectronica.Configuracion;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SRIDocumentosElectronicosServiceCollectionExtensions
    {
        public static ISRIDocumentosElectronicosBuilder AddSRIDocumentosElectronicosBuilder(
          this IServiceCollection services)
        {
            return new SRIDocumentosElectronicosBuilder(services);
        }

        public static ISRIDocumentosElectronicosBuilder AddSRIDocumentosElectronicos(
          this IServiceCollection services)
        {
            ISRIDocumentosElectronicosBuilder builder = services.AddSRIDocumentosElectronicosBuilder();
            builder.AddRequiredPlatformServices().AddDefaultServices();
            return builder;
        }

        public static ISRIDocumentosElectronicosBuilder AddSRIDocumentosElectronicos(
          this IServiceCollection services,
          Action<SRIDocumentosElectronicosOptions> setupAction)
        {
            services.Configure(setupAction);
            return services.AddSRIDocumentosElectronicos();
        }

        public static ISRIDocumentosElectronicosBuilder AddSRIDocumentosElectronicos(
          this IServiceCollection services,
          IConfiguration configuration)
        {
            services.Configure<SRIDocumentosElectronicosOptions>(configuration);
            return services.AddSRIDocumentosElectronicos();
        }
    }
}
