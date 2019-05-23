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
        VetorPalavra asPalavras;
        int tempoPassado;  // Tempo passado em segundos
        Palavra palavraAtual;  // Palavra lido do vetor
       
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            int aleatorio = new Random().Next(100);
            palavraAtual = asPalavras[aleatorio];
            tmTemporizador.Start();
            dgvPalavra.ColumnCount = palavraAtual.PalavraString.Length;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            asPalavras = new VetorPalavra(100);
            dlgAbrir.Title = "Selecione o arquivo desejado";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                asPalavras.LeituraDoVetor(dlgAbrir.FileName);
            }
        }

        private void TmTemporizador_Tick(object sender, EventArgs e)
        {
            tempoPassado++;
        }

        private void BtnA_Click(object sender, EventArgs e)
        {
            string t = (sender as Button).Text; //  Acessa valor interno

        }
    }
}
