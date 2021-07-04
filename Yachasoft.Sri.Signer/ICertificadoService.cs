using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Yachasoft.Sri.Signer
{
    public interface ICertificadoService
    {
        void CargarDesdeAlmacen(StoreName storeName, StoreLocation storeLocation, string friendlyName);
        void CargarDesdeBase64String(string certificadoBase64, string contrasena);
        void CargarDesdeDialogo(string titulo = null, string mensaje = null);
        void CargarDesdeP12(string rutaCertificado, string contrasena);
        XmlDocument FirmarDocumento<T>(T document);
    }
}