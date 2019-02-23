using System.Security.Cryptography.X509Certificates;

namespace Assinado.Pdf.Sign.Model
{
    public class DigitalCertificate
    {
        public string FriendlyName { get; set; }
        public string Name { get; set; }
        public byte[] PublicKey { get; set; }
        public X509Certificate2 Certificate { get; set; }
    }
}
