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
        int primeiraMatch = 0;
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

        public void HabilitarBotoes(bool habilitado)
        {
            btnA.Enabled = habilitado;
            btnB.Enabled = habilitado;
            btnC.Enabled = habilitado;
            btnD.Enabled = habilitado;
            btnE.Enabled = habilitado;
            btnF.Enabled = habilitado;
            btnG.Enabled = habilitado;
            btnH.Enabled = habilitado;
            btnI.Enabled = habilitado;
            btnJ.Enabled = habilitado;
            btnK.Enabled = habilitado;
            btnL.Enabled = habilitado;
            btnM.Enabled = habilitado;
            btnN.Enabled = habilitado;
            btnO.Enabled = habilitado;
            btnP.Enabled = habilitado;
            btnQ.Enabled = habilitado;
            btnR.Enabled = habilitado;
            btnS.Enabled = habilitado;
            btnT.Enabled = habilitado;
            btnU.Enabled = habilitado;
            btnV.Enabled = habilitado;
            btnW.Enabled = habilitado;
            btnX.Enabled = habilitado;
            btnY.Enabled = habilitado;
            btnZ.Enabled = habilitado;
            btnCc.Enabled = habilitado;
            btnAa.Enabled = habilitado;
            btnAt.Enabled = habilitado;
            btnAc.Enabled = habilitado;
            btnEc.Enabled = habilitado;
            btnIa.Enabled = habilitado;
            btnOa.Enabled = habilitado;
            btnOc.Enabled = habilitado;
            btnOt.Enabled = habilitado;
            btnUa.Enabled = habilitado;
            btnHifen.Enabled = habilitado;
            btnEspaco.Enabled = habilitado;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "")
            {
                if (primeiraMatch == 0)
                {
                    primeiraMatch++;
                    HabilitarBotoes(true);
                    pb1.Visible = false;
                    pb2.Visible = false;
                    pb3.Visible = false;
                    pbNew.Visible = false;
                    pb4.Visible = false;
                    pb5.Visible = false;
                    pb6.Visible = false;
                    pb7.Visible = false;
                    pb8.Visible = false;
                    int aleatorio = new Random().Next(25);
                    palavraAtual = asPalavras[aleatorio];
                    if (chkDica.Checked)
                    {
                        lbDica.Text = $"Dica: {palavraAtual.Dica}";
                    }
                    tmTemporizador.Start();
                    dgvPalavra.ColumnCount = palavraAtual.PalavraSelec.Length;
                }
                else
                {
                    ResetarJogo();
                }
            }
            else
                MessageBox.Show("Por favor, insira seu nome");
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
            for (int indice = 0; indice < palavraAtual.PalavraSelec.Length ; indice++)
            {
                char letra = palavraAtual.PalavraSelec[indice];
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
