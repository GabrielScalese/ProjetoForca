using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace apProjeto2
{
    public partial class Form1 : Form
    {
        string nomeUsuario = "";
        int primeiraMatch = 0;
        const int tempoTotal = 60;
        int pontos = 0; // Armazena quantidade de pontos do usuário
        int erro = 0;
        VetorPalavra asPalavras;
        string[] nick;
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
            btnEa.Enabled = habilitado;
            btnEc.Enabled = habilitado;
            btnIa.Enabled = habilitado;
            btnOa.Enabled = habilitado;
            btnOc.Enabled = habilitado;
            btnOt.Enabled = habilitado;
            btnUa.Enabled = habilitado;
            btnHifen.Enabled = habilitado;
            btnEspaco.Enabled = habilitado;
        }

        public void ResetarJogo()
        {
            HabilitarBotoes(false);
            pb1.Visible = false;
            pb2.Visible = false;
            pbMaoBandeira.Visible = false;
            pbNew.Visible = false;
            pb4.Visible = false;
            pb5.Visible = false;
            pb6.Visible = false;
            pb7.Visible = false;
            pb8.Visible = false;
            pbAnjo.Visible = false;
            tmTemporizador.Stop();
            tempoPassado = 0;
            foreach (DataGridViewCell cell in dgvPalavra.Rows[0].Cells) //  Verifica cada posição da palavra
            {
                cell.Value = "";
            }
            pontos = erro = 0;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "")
            {
                ResetarJogo();
                HabilitarBotoes(true);
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
            (sender as Button).Enabled = false;
            bool achouLetra = false;
            char t = Convert.ToChar((sender as Button).Text); //  Acessa valor interno
            for (int indice = 0; indice < palavraAtual.PalavraSelec.Length; indice++)
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
                    pbAnjo.Visible = true;
                    if (MessageBox.Show("Infelizmente você perdeu!\nDeseja jogar novamente?", "Fim de jogo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ResetarJogo();
                    }
                }
                else
                {
                    switch (erro) // A cada erro uma imagem será exibida
                    {
                        case 1: pb1.Visible = true; break;
                        case 2: pb2.Visible = true; break;

                        case 3: pb4.Visible = true; break;
                        case 4: pb3.Visible = true; break;

                        

                        case 5: pb5.Visible = true; break;
                        case 6: pb6.Visible = true; break;
                        case 7: pb7.Visible = true; break;
                        case 8: pb8.Visible = true; break;
                        case 9: pbMorto.Visible = true;break;
                    }
                }
            }
            else
            {
                bool terminou = true;
                foreach (DataGridViewCell cell in dgvPalavra.Rows[0].Cells) //  Verifica cada posição da palavra
                {
                    if (cell.Value == null || cell.Value.ToString() == "")
                    {
                       terminou = false;
                    }
                }
                if (terminou)
                {
                    if (MessageBox.Show("Parabéns, você ganhou!\nDeseja jogar novamente?", "Fim de jogo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ResetarJogo();
                    }
                }
                pontos++;
            }
        }

        public void LerNicks()
        {
            int indice = 0;
            var arq = new StreamReader("Ranking.txt");
            while (!arq.EndOfStream)
            {
                string linha = arq.ReadLine();
                nick[indice++] = linha;

                //string[indiceNome]nome = linha;
                //indiceNome++;
            }
            arq.Close();
            var arqSaida = new StreamWriter("c:\temp");
            while (!arq.EndOfStream)
            {
                arqSaida.WriteLine();
                
            }
        }



        public void SalvarPontuacao()
        {
            LerNicks();
        }

    }

    public void CadastraJogador()
    {
        string local = dlgAbrir.FileName.Substring(0, dlgAbrir.FileName.IndexOf("PALAVRASEDICAS.txt")) + "Ranking.txt";
        //string do lugar do arquivo, que pega a mesma pasta do projeto, retira "palavras.txt" e adiciona "ranking.txt"
        StreamReader leitura = new StreamReader(local); //arquivo a ser lido
        var vetJogador = new Jogador(); //instanciação de vetor de jogadores
        vetJogador.LerArquivo(leitura); //ler arquivo e armazenar jogadores no vetor
        else //se o nome do jogador não existe
        {
            Jogador novo = new Jogador(nomeUsuario, pontos); //instanciar novo jogador com nome e pontos já definidos
            vetJogador.Incluir(novo); //incluir novo jogaor no vetor
        }
        StreamWriter escrita = new StreamWriter(local); //arquivo a ser escrito
        vetJogador.GravarDados(escrita); //gravar vetor no arquivo de escrita, por cima do que estava no arquivo de leitura 
    }


}
