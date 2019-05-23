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
        VetorPalavra atual;

        VetorPalavra PalavraDica;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            int aleatorio = new Random().Next(100);
            atual = vetorPalavra[aleatorio - 1];

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            atual = new VetorPalavra(100);
            dlgAbrir.Title = "Selecione o arquivo desejado";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                atual.LeituraDoVetor(dlgAbrir.FileName);
            }
        }
    }
}
