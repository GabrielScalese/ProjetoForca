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
        const int tempoTotal = 60;
        int pontos = 0; // Armazena quantidade de pontos do usuário
        int erro = 0;
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
            
            lbTemporizador.Text = $"Tempo restante: {tempoTotal - tempoPassado}s";
        }

        private void BtnA_Click(object sender, EventArgs e) // Tratamento de strings
        {
            bool achouLetra = false;
            char t = Convert.ToChar((sender as Button).Text); //  Acessa valor interno
            for (int indice = 0; indice < palavraAtual.PalavraString.Length ; indice++)
            {
                char letra = palavraAtual.PalavraString[indice];
                if (letra == t) // Verifica se letra digitada existe
                {
                    dgvPalavra.Rows[0].Cells[indice].Value = letra; // Exibe no "dgvPalavra" a palavra que foi escolhida corretamente
                    achouLetra = true;
                }
            }
            if (!achouLetra) // Erro cometido pelo usuário(por letra)
            {
                erro++;
                pontos--;
                if (erro > 8)
                {
                    ptbAnjo.Visible = true;
                    if (MessageBox.Show("Infelizmente você perdeu!\nDeseja jogar novamente?", "Fim de jogo!" ,MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Resetar(); 
                    }
                   
                }
                else
                {
                    switch (erro) // A cada erro uma imagem será exibida
                    {
                        case 1: pb1.Visible = true; break;
                        case 2: pb2.Visible = true; break;
                        case 3: pb3.Visible = true; break;
                        case 4: pb4.Visible = true; break;
                        case 5: pb5.Visible = true; break;
                        case 6: pb6.Visible = true; break;
                        case 7: pb7.Visible = true; break;
                        case 8: pb8.Visible = true; break;
                    }
                }
            }
            else
                pontos++;
        }
    }
}
