using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface ISRIDocumentosElectronicosBuilder
    {
        IServiceCollection Services { get; }
    }
}