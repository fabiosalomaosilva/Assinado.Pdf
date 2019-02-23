using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfCompressorLibrary;

namespace Assinado.Pdf.Utils
{
    public class PdfCompressing
    {
        public void Compress(string filePath, string outputFilePath)
        {
            PdfReader reader = new PdfReader(filePath);
            PdfStamper stamper = new PdfStamper(reader, new FileStream(outputFilePath, FileMode.Create), PdfWriter.VERSION_1_5);
            PdfWriter writer = stamper.Writer;
            writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_5);
            writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
            reader.SetPageContent(1, reader.GetPageContent(1), PdfStream.BEST_COMPRESSION, true);
            reader.RemoveFields();
            reader.RemoveUnusedObjects();
            stamper.SetFullCompression();
            stamper.Reader.RemoveUnusedObjects();
            stamper.Close();
        }



    }

}
