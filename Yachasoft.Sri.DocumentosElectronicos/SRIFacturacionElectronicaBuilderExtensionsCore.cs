using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.DocumentosElectronicos.Configuracion;
using Yachasoft.Sri.Ride;
using Yachasoft.Sri.Signer;
using Yachasoft.Sri.WebService;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SRIFacturacionElectronicaBuilderExtensionsCore
    {
        public static ISRIDocumentosElectronicosBuilder AddRequiredPlatformServices(
          this ISRIDocumentosElectronicosBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.AddSingleton<SRIDocumentosElectronicosOptions>((Func<IServiceProvider, SRIDocumentosElectronicosOptions>)(resolver => resolver.GetRequiredService<IOptions<SRIDocumentosElectronicosOptions>>().Value));
            return builder;
        }

        public static ISRIDocumentosElectronicosBuilder AddDefaultServices(
          this ISRIDocumentosElectronicosBuilder builder)
        {
            builder.Services.AddTransient<ICertificadoService, CertificadoService>().AddTransient<ISriWebService, SriWebService>().AddTransient<IRIDEService, RIDEService>();
            return builder;
        }
    }
}
