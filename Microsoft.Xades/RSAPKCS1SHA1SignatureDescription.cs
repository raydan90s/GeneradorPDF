using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591

namespace Microsoft.Xades
{
    public sealed class RSAPKCS1SHA1SignatureDescription : SignatureDescription
    {
        public RSAPKCS1SHA1SignatureDescription()
        {
            this.KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
            this.DigestAlgorithm = typeof(SHA1Managed).FullName;
            this.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).FullName;
            this.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).FullName;
        }

        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
        {
            RSAPKCS1SignatureDeformatter deformatter = key != null ? new RSAPKCS1SignatureDeformatter(key) : throw new ArgumentNullException(nameof(key));
            deformatter.SetHashAlgorithm("SHA1");
            return deformatter;
        }

        public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
        {
            RSAPKCS1SignatureFormatter formatter = key != null ? new RSAPKCS1SignatureFormatter(key) : throw new ArgumentNullException(nameof(key));
            formatter.SetHashAlgorithm("SHA1");
            return formatter;
        }
    }
}
#pragma warning restore 1591