using System;
using System.Collections;
using System.IO;
using iTextSharp.text.pdf;

namespace Assinado.Pdf.Utils
{
    public class PdfSplit
    {
        public void ExtractPagesByRange(string filePath, string outputFilePath, int startpage, int endpage)
        {
            var pdfReader = new PdfReader(filePath);
            try
            {
                pdfReader.SelectPages($"{startpage}-{endpage}");
                using (var fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    PdfStamper stamper = null;
                    try
                    {
                        stamper = new PdfStamper(pdfReader, fs);
                    }
                    finally
                    {
                        stamper?.Close();
                    }
                }
            }
            finally
            {
                pdfReader.Close();
            }
        }
        public void SplitByNumberOfFiles(string filePath, string outputFilePath, int numberFiles)
        {
            var pdfReader = new PdfReader(filePath);
            try
            {
                var nPages = pdfReader.NumberOfPages;
                var pagesByFile = 0;
                var listPages = new ArrayList();

                if (nPages >= numberFiles)
                {
                    pagesByFile = nPages / numberFiles;
                    var pagesResto = nPages % numberFiles;

                    if (pagesResto == 0)
                    {
                        var ind = 0;
                        for (int i = 0; i < numberFiles - 1; i++)
                        {
                            listPages.Add(ind + pagesByFile);
                            ind = ind + pagesByFile;
                        }
                        listPages.Add(ind + pagesResto);
                    }
                    else
                    {
                        var n = numberFiles - 1;
                        pagesByFile = nPages / n;
                        pagesResto = nPages % n;
                        var ind = 0;
                        for (int i = 0; i < numberFiles - 1; i++)
                        {
                            listPages.Add(ind + pagesByFile);
                            ind = ind + pagesByFile;
                        }
                        listPages.Add(ind + pagesResto);
                    }
                }

                var index = 1;
                var pageFinal = pagesByFile;
                var numberFile = 1;
                foreach (var i in listPages)
                {

                    pdfReader = new PdfReader(filePath);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputFilePath);
                    var nome = Path.GetFileName(outputFilePath);
                    var caminho = outputFilePath.Replace(nome, "");

                    pdfReader.SelectPages($"{index}-{pageFinal}");
                    using (var fs = new FileStream(caminho + fileNameWithoutExtension + "-" + numberFile.ToString("000") + ".pdf", FileMode.Create, FileAccess.Write))
                    {
                        PdfStamper stamper = null;
                        try
                        {
                            stamper = new PdfStamper(pdfReader, fs);
                            index = index + pagesByFile;
                            pageFinal = (index + pagesByFile) - 1;
                            numberFile = numberFile + 1;
                        }
                        finally
                        {
                            stamper?.Close();
                        }
                    }

                }

            }
            finally
            {
                pdfReader.Close();
            }
        }

    }
}
