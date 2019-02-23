using System;
using System.Collections.Generic;
using System.IO;
using Assinado.Pdf.Sign.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.X509;

namespace Assinado.Pdf.Sign
{
    public class Sign
    {
        public string SignDocument(string documentPdf, DigitalCertificate cert, PdfOptions certOptions, string pathOutput)
        {
            try
            {
                var reader = new PdfReader(documentPdf);
                var output = new FileStream(pathOutput, FileMode.Create, FileAccess.Write, FileShare.None);
                var stamper = PdfStamper.CreateSignature(reader, output, '\0');


                var rect = new iTextSharp.text.Rectangle(10, 10, 0, 0);
                var appearance = stamper.SignatureAppearance;
                appearance.Reason = "Assinatura digital";
                appearance.SignDate = certOptions.SignDate;
                appearance.Contact = certOptions.Contact;
                appearance.SignatureGraphic = certOptions.SignImage;
                appearance.Location = certOptions.Location;
                appearance.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;
                appearance.SetVisibleSignature(rect, 1, certOptions.NameOwnerOfCertificate);
                IExternalSignature pks = new X509Certificate2Signature(cert.Certificate, "sha-256");

                var ce = new X509CertificateParser();
                var chain = ce.ReadCertificate(cert.Certificate.RawData);

                var lista = new List<X509Certificate> { chain };
                ICollection<X509Certificate> lst = lista;

                MakeSignature.SignDetached(appearance, pks, lst, null, null, null, 0, CryptoStandard.CMS);
                return pathOutput;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
