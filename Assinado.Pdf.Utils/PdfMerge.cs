using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Assinado.Pdf.Utils
{
    public class PdfMerge
    {
        /// <summary>
        /// Lista de arquivos a serem concatenados
        /// </summary>
        private readonly List<string> _pdfList;

        /// <summary>
        /// Objeto que representa um documento (pdf) do iTextSharp
        /// </summary>
        private Document _document;

        /// <summary>
        /// Objeto responsável por salvar o pdf em disco.
        /// </summary>
        private PdfWriter _writer;

        /// <summary>
        /// Construtor
        /// </summary>
        public PdfMerge()
        {
            _pdfList = new List<string>();
        }

        /// <summary>
        /// Adiciona o arquivo que será concatenado ao PDF final.
        /// </summary>
        /// <param name="filePath">Caminho para o arquivo PDF</param>
        public void Add(string filePath)
        {
            _pdfList.Add(filePath);
        }

        /// <summary>
        /// Adiciona uma lista de arquivos pdf para serem concatenados.
        /// </summary>
        /// <param name="files">Lista contendo o caminho para os arquivos</param>
        public void AddList(List<string> files)
        {
            _pdfList.AddRange(files);
        }

        /// <summary>
        /// Concatena os arquivos de entrada, gerando um novo arquivo PDF.
        /// </summary>
        public void Save(string pathToDestFile)
        {
            PdfReader reader = null;
            PdfContentByte cb = null;
            var index = 0;
            try
            {
                // Percorre a lista de arquivos a serem concatenados.
                foreach (var file in _pdfList)
                {
                    // Cria o PdfReader para ler o arquivo
                    reader = new PdfReader(_pdfList[index]);
                    // Obtém o número de páginas deste pdf
                    var numPages = reader.NumberOfPages;

                    if (index == 0)
                    {
                        // Cria o objeto do novo documento
                        _document = new Document(reader.GetPageSizeWithRotation(1));
                        // Cria um writer para gravar o novo arquivo
                        _writer = PdfWriter.GetInstance(_document, new FileStream(pathToDestFile, FileMode.Create));
                        // Abre o documento
                        _document.Open();
                        cb = _writer.DirectContent;
                    }

                    // Adiciona cada página do pdf origem ao pdf destino.
                    var i = 0;
                    while (i < numPages)
                    {
                        i++;
                        _document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        _document.NewPage();
                        var page = _writer.GetImportedPage(reader, i);
                        var rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        else
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                // Tratar a exceção de acordo com a necessidade de cada projeto.
                // Não vamos realizar o tratamento da exceção de forma mais elaborada 
                // por motivos didáticos.
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (_document != null)
                    _document.Dispose();
                if (_writer != null)
                    _writer.Dispose();
            }
        }

        public MemoryStream MergePdfForms(List<byte[]> files)
        {
            if (files.Count > 1)
            {
                string[] names;
                PdfStamper stamper;
                MemoryStream msTemp = null;
                PdfReader pdfTemplate = null;
                PdfReader pdfFile;
                Document doc;
                PdfWriter pCopy;
                var msOutput = new MemoryStream();

                pdfFile = new PdfReader(files[0]);

                doc = new Document();
                pCopy = new PdfSmartCopy(doc, msOutput) { PdfVersion = PdfWriter.VERSION_1_7 };

                doc.Open();

                for (var k = 0; k < files.Count; k++)
                {
                    for (var i = 1; i < pdfFile.NumberOfPages + 1; i++)
                    {
                        msTemp = new MemoryStream();
                        pdfTemplate = new PdfReader(files[k]);

                        stamper = new PdfStamper(pdfTemplate, msTemp);

                        names = new string[stamper.AcroFields.Fields.Keys.Count];
                        stamper.AcroFields.Fields.Keys.CopyTo(names, 0);
                        foreach (var name in names)
                        {
                            stamper.AcroFields.RenameField(name, name + "_file" + k.ToString());
                        }

                        stamper.Close();
                        pdfFile = new PdfReader(msTemp.ToArray());
                        ((PdfSmartCopy)pCopy).AddPage(pCopy.GetImportedPage(pdfFile, i));
                        pCopy.FreeReader(pdfFile);
                    }
                }

                pdfFile.Close();
                pCopy.Close();
                doc.Close();

                return msOutput;
            }
            else if (files.Count == 1)
            {
                return new MemoryStream(files[0]);
            }

            return null;
        }

    }
}
