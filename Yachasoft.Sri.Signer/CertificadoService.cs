using FirmaXadesNet;
using FirmaXadesNet.Crypto;
using FirmaXadesNet.Signature;
using FirmaXadesNet.Signature.Parameters;
using FirmaXadesNet.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Signer
{
    public class CertificadoService : ICertificadoService
    {
        private X509Certificate2 _X509Certificate2;
        //private readonly SRIDocumentosElectronicosOptions _options;
        private readonly ILogger<CertificadoService> _logger;

        public CertificadoService(
          //SRIDocumentosElectronicosOptions options,
          ILogger<CertificadoService> logger)
        {
            //this._options = options;
            this._logger = logger;
        }

        public void CargarDesdeP12(string rutaCertificado, string contrasena)
        {
            this._logger.LogTrace("Cargando Certificado desde archivo p12 " + rutaCertificado);
            try
            {
                this._X509Certificate2 = new X509Certificate2(rutaCertificado, contrasena, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                this._logger.LogDebug("Certificado cargado");
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error cargando certificado desde archivo p12 " + rutaCertificado + ". " + ex.Message);
                throw;
            }
        }

        public void CargarDesdeAlmacen(
          StoreName storeName,
          StoreLocation storeLocation,
          string friendlyName)
        {
            var x509Store = new X509Store(storeName, storeLocation);
            x509Store.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, true);
            bool flag = false;
            foreach (X509Certificate2 x509Certificate2 in certificate2Collection)
            {
                if (string.Equals(x509Certificate2.FriendlyName, friendlyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    this._X509Certificate2 = x509Certificate2;
                    flag = true;
                    break;
                }
            }
            if (!flag)
                throw new Exception(string.Format("No existe un certificado válido con el Nombre Descriptivo: {0}. Revise que exista en el almacén de certificados {1} de su equipo y que no esté expirado.", (object)friendlyName, (object)storeLocation));
        }

        public void CargarDesdeDialogo(string titulo = null, string mensaje = null) => this._X509Certificate2 = CertUtil.SelectCertificate(mensaje, titulo, true);

        public void CargarDesdeBase64String(string certificadoBase64, string contrasena)
        {
            this._logger.LogTrace("Cargando Certificado desde String base64 p12");
            try
            {
                this._X509Certificate2 = new X509Certificate2(Convert.FromBase64String(certificadoBase64), contrasena, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                this._logger.LogDebug("Certificado cargado");
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error cargando certificado desde String base64 p12. " + ex.Message);
                throw;
            }
        }

        public XmlDocument FirmarDocumento<T>(T document)
        {
            if (this._X509Certificate2 == null)
                throw new Exception("Debe primero cargar un certificado válido");
            var xadesService = new XadesService();
            var parameters = new SignatureParameters()
            {
                SignatureMethod = SignatureMethod.RSAwithSHA1,
                DigestMethod = DigestMethod.SHA1,
                SigningDate = new DateTime?(DateTime.Now)
            };
            var signatureCommitment = new SignatureCommitment(SignatureCommitmentType.ProofOfOrigin);
            parameters.SignatureCommitments.Add(signatureCommitment);
            parameters.SignaturePackaging = SignaturePackaging.ENVELOPED;
            SignatureDocument signatureDocument;
            using (parameters.Signer = new FirmaXadesNet.Crypto.Signer(this._X509Certificate2))
            {
                using (var memoryStream = new MemoryStream())
                {
                    new XmlSerializer(typeof(T)).Serialize(memoryStream, document);
                    memoryStream.Position = 0L;
                    signatureDocument = xadesService.Sign(memoryStream, parameters);
                }
            }
            return signatureDocument.Document;
        }
    }
}
