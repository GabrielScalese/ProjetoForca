using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apProjeto2
{
    public partial class Form1 : Form
    {

        VetorPalavra PalavraDica;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dlgAbrir.Title = "Selecione o arquivo com as palavras e as dicas do jogo.";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                LerVetor(dlgAbrir.FileName)
            }
        }
    }
}
