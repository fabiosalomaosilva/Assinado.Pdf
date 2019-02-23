using System;

namespace Assinado.Pdf
{
    public class PdfOptions
    {
        /// <summary>
        /// Razões da assinatura digital ou do documento PDF Assinado
        /// </summary>
        public string NameOwnerOfCertificate { get; set; }

        /// <summary>
        /// Razões da assinatura digital ou do documento PDF Assinado
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Data da assinatura do documento PDF
        /// </summary>
        public DateTime SignDate { get; set; }
        /// <summary>
        /// local onde foi criado assinado o documento PDF
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Criado do documento PDF
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// Criado do documento PDF
        /// </summary>
        public iTextSharp.text.Image SignImage { get; set; }


    }
}
