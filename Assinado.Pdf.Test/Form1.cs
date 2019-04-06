using Assinado.Pdf.Sign;
using Assinado.Pdf.Sign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Assinado.Pdf.Test
{
    public partial class Form1 : Form
    {
        private List<DigitalCertificate> listaCertificados;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var store = new Store();
            var cd = new DigitalCertificate { FriendlyName = "Sem assinatura digital" };
            listaCertificados = new List<DigitalCertificate>();
            listaCertificados.Add(cd);

            listaCertificados.AddRange(store.GetListCertificates(CertSignStoreName.My, CertSignStoreLocation.CurrentUser));
            cbxCertificados.DataSource = listaCertificados;
            cbxCertificados.SelectionStart = 1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
            }

            var pdf = new Pdf.Utils.PdfMerge();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //pdf.MergePdfForms(file,saveFileDialog1.FileName);

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (cbxCertificados.SelectedItem != null)
            {
                openFileDialog1.ShowDialog();
                saveFileDialog1.ShowDialog();
                var file = openFileDialog1.FileName;
                var store = new Store();
                var sign = new Sign.Sign();

                var cert = listaCertificados[cbxCertificados.SelectedIndex];
                var opt = new PdfOptions
                {
                    SignDate = DateTime.Now,
                    Contact = "email@email.com.br",
                    NameOwnerOfCertificate = cert.FriendlyName,
                    Reason = "Assinatura digital"
                };
                sign.SignDocument(file, cert, opt, saveFileDialog1.FileName);
            }

        }

    }
}
