using Microsoft.Extensions.DependencyInjection;
using System;

namespace Yachasoft.Sri.FacturacionElectronica.Configuracion
{
    public class SRIDocumentosElectronicosBuilder : ISRIDocumentosElectronicosBuilder
    {
        public IServiceCollection Services { get; }

        public SRIDocumentosElectronicosBuilder(IServiceCollection services) => this.Services = services ?? throw new ArgumentNullException(nameof(services));
    }
}
