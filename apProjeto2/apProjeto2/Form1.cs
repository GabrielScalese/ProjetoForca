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

// Illy Bordini da Silva   RA: 19180    
// Gabriel Villar Scalese  RA: 19171


namespace apProjeto2
{
    public partial class FrmForca : Form
    {
        int ondeAchar = 0;
        int posicaoAIncluir = 0; // Índice do vetor da classe Jogador
        Jogador[] vet = new Jogador[1000]; // instância de um vetor da classe Jogador
        string nomeUsuario = "";
        int primeiraMatch = 0;
        const int tempoTotal = 60;
        int pontos = 0; // Armazena quantidade de pontos do usuário
        int erro = 0;
        VetorPalavra asPalavras; // Variável que chamará métodos da classe
        string[] nick;
        int tempoPassado;  // Tempo passado em segundos
        Palavra palavraAtual;  // Palavra lido do vetor

        public FrmForca()
        {
            InitializeComponent();
        }

        public void HabilitarBotoes(bool habilitado) // Habilitação dos botões no início da execeução
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

        public void ResetarJogo() // Atualização da interface
        {
            HabilitarBotoes(false);
            pb1.Visible = false;
            pb2.Visible = false;
            pbNew.Visible = false;
            pb4.Visible = false;
            pb5.Visible = false;
            pb6.Visible = false;
            pb7.Visible = false;
            pb8.Visible = false;
            pbAnjo.Visible = false;
            pbMaoBandeira.Visible = false;
            pbMorto.Visible = false;
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
                ResetarJogo(); // Atualização da interface
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

            // Componente da parte 2 do projeto

            int indice = 0; // Índice resposável por percorrer os botões do "imlBotoes"
            barraDeItens.ImageList = imlBotoes;
            foreach (ToolStripItem item in barraDeItens.Items)
                if (item is ToolStripButton) // "Condição de existência"
                    (item as ToolStripButton).ImageIndex = indice++;
        }

        private void TmTemporizador_Tick(object sender, EventArgs e)
        {
            tempoPassado++;

            lbTemporizador.Text = $"Tempo restante: {tempoTotal - tempoPassado}s";
            lbPontos.Text = $"Pontos:{pontos}";
            lbErros.Text = $"Erros: {erro}";
            if (tempoPassado == 60)
            {
                tmTemporizador.Stop();
                if (MessageBox.Show("Infelizmente o tempo esgotou!\nDeseja jogar novamente?", "Fim de jogo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ResetarJogo();
                }
            }
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
                    CadastraJogador("Ranking.txt");
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
                        case 4: pbNew.Visible = true; break;
                        case 5: pb5.Visible = true; break;
                        case 6: pb6.Visible = true; break;
                        case 7: pb7.Visible = true; break;
                        case 8: pb8.Visible = true; pbMorto.Visible = true; pbMaoBandeira.Visible = true; ptbBandeiraEsquerda.Visible = true; ptbBandeiraDireita.Visible = true; pbMaoBandeira.Visible = true; break;
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
                    CadastraJogador("Ranking.txt");
                    if (MessageBox.Show("Parabéns, você ganhou!\nDeseja jogar novamente?", "Fim de jogo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ResetarJogo();
                    }
                    else
                        Close();
                }
                pontos++;
            }
        }

        public void CadastraJogador(string nomeArq) // Grava num arquivo texto o ranking dos usuários
        {
            if (!File.Exists(nomeArq)) // Verifica existência do arquivo
            {
                File.CreateText(nomeArq);
            }
            var arqLido = new StreamReader(nomeArq);
            bool achouNome = false;

            while (!arqLido.EndOfStream)
            {
                string linha = arqLido.ReadLine();
                vet[posicaoAIncluir] = new Jogador(linha);
                if (vet[posicaoAIncluir].Nome.Trim() == txtUsuario.Text)
                {
                    vet[posicaoAIncluir].Somar(pontos);
                    achouNome = true;
                }
                posicaoAIncluir++;
            }
            arqLido.Close();
            var escritor = new StreamWriter(nomeArq);
            for (int i = 0; i < posicaoAIncluir; i++)
                escritor.WriteLine(vet[i].Nome.PadRight(30, ' ') + vet[i].Pontuacao);
            if (!achouNome)
            {
                escritor.WriteLine($"{txtUsuario.Text.PadRight(30, ' ')}{pontos}"); // Gravação dos dados
            }
            escritor.Close();
        }

        public bool Existe(string nomeProcurado) // Verifica a existência do usuário
        {
            bool achou = false;
            for (int ind = 0; ind < vet.Length; ind++) // Percorre todo o vetor
                if (vet[ind].Nome == nomeProcurado) // Caso o nome do jogador atual for igual ao nome procurado
                    achou = true; // O jogador foi achado
            return achou; // Retorna se o jogador foi achado ou não
        }

        private void LimparTela()
        {
            edPalavra.Clear();
            edDica.Clear();
        }

        private void AtualizarTela()
        {
            if (!asPalavras.EstaVazio)
            {
                int indice = asPalavras.PosicaoAtual;
                edPalavra.Text = asPalavras[indice].PalavraSelec;
                edDica.Text = asPalavras[indice].Dica;
                TestarBotoes();
                dgvManutencao.RowCount = asPalavras.Tamanho;
                for (int indiceA = 0; indiceA < asPalavras.Tamanho; indiceA++)
                {
                    Palavra palavra = asPalavras[indiceA];
                    dgvManutencao.Rows[indiceA].Cells[0].Value = indiceA;
                    dgvManutencao.Rows[indiceA].Cells[1].Value = palavra.PalavraSelec; // Exibe no "dgvPalavra" a palavra que foi escolhida corretamente
                    dgvManutencao.Rows[indiceA].Cells[2].Value = palavra.Dica;
                }
            }
        }

        private void TestarBotoes()
        {
            btnInicio.Enabled = true;
            btnVoltar.Enabled = true;
            btnAvancar.Enabled = true;
            btnUltimo.Enabled = true;
            if (asPalavras.EstaNoInicio)
            {
                btnInicio.Enabled = false;
                btnVoltar.Enabled = false;
            }
            if (asPalavras.EstaNoFim)
            {
                btnAvancar.Enabled = false;
                btnUltimo.Enabled = false;
            }
        }


        private void BtnInicio_Click(object sender, EventArgs e)
        {
            if (asPalavras.PosicaoAtual != 100)
                dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;
            asPalavras.PosicionarNoPrimeiro();
            AtualizarTela();
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            if (asPalavras.PosicaoAtual != 100)
                dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;
            asPalavras.RetrocederPosicao();
            AtualizarTela();
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void BtnAvancar_Click(object sender, EventArgs e)
        {
            if (asPalavras.PosicaoAtual != -1)
                dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;
            asPalavras.AvancarPosicao();
            AtualizarTela();
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void BtnUltimo_Click(object sender, EventArgs e)
        {
            if (asPalavras.PosicaoAtual != -1)
                dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;
            asPalavras.PosicionarNoUltimo();
            AtualizarTela();
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void DgvManutencao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            asPalavras.PosicaoAtual = e.RowIndex;
            AtualizarTela();
        }

        private void BtnIncluir_Click(object sender, EventArgs e)
        {
            asPalavras.SituacaoAtual = VetorPalavra.Situacao.incluindo;
            LimparTela();
            edPalavra.Focus();
        }

        private void EdPalavra_Leave(object sender, EventArgs e)
        {
            if (edPalavra.Text == "")
                MessageBox.Show("Por favor, digite uma palavra");
            else
            {
                var palavraProc = new Palavra(edPalavra.Text, "");
                switch (asPalavras.SituacaoAtual)
                {
                    case VetorPalavra.Situacao.incluindo:
                        if (asPalavras.Existe(palavraProc, ref ondeAchar))
                        {
                            MessageBox.Show("Palavra repetida, por favor insira outra palavra.");
                            asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando;
                            AtualizarTela();
                        }
                        else
                        {
                            edDica.Focus();
                        }
                        break;
                    case VetorPalavra.Situacao.editando:
                        if (asPalavras.Existe(palavraProc, ref ondeAchar))
                        {
                            asPalavras.PosicaoAtual = ondeAchar;
                            AtualizarTela();
                            asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando;
                        }
                        break;
                }
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (asPalavras.SituacaoAtual == VetorPalavra.Situacao.incluindo) // está no modo de inclusão
            {
                var novaPalavra = new Palavra(edPalavra.Text, edDica.Text);


                asPalavras.Incluir(novaPalavra);

                // para mudar o registro com o qual trabalhamos no momento
                asPalavras.PosicaoAtual = ondeAchar;
                AtualizarTela();
                asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando; // termina o modo de inclusão
            }
            else
        if (asPalavras.SituacaoAtual == VetorPalavra.Situacao.editando)
            {
                asPalavras[asPalavras.PosicaoAtual] = new Palavra(edPalavra.Text, edDica.Text);

                asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando;
                edPalavra.ReadOnly = false;
                AtualizarTela();
            }
        }

        private void FrmForca_FormClosing(object sender, FormClosingEventArgs e)
        {
            asPalavras.GravarEmDisco(dlgAbrir.FileName);
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
             "Deseja realmente excluir?", "Exclusão",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                asPalavras.Excluir(asPalavras.PosicaoAtual);
                if (asPalavras.PosicaoAtual >= asPalavras.Tamanho)
                    asPalavras.PosicionarNoUltimo();
                AtualizarTela();
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            asPalavras.SituacaoAtual = VetorPalavra.Situacao.editando;
            edPalavra.Focus();
        }

        private void BtnOrdenar_Click(object sender, EventArgs e)
        {
            asPalavras.Ordenar();
            AtualizarTela();
        }
    }
}
