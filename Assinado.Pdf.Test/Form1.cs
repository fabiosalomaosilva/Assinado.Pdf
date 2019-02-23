using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assinado.Pdf.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file = "";
            var newFile = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
            }

            var pdf = new Pdf.Utils.PdfCompressing();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pdf.Compress(
                    file, 
                    saveFileDialog1.FileName
                    );

            }
        }
    }
}
