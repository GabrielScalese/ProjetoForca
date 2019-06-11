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
        int ondeAchar = 0; // Posição a ser usado no modo de inclusão e modo de edição, além de ser usado no método de verificar 
                           // a existência de uma palavra
        int posicaoAIncluir = 0; // Índice do vetor da classe Jogador
        Jogador[] vet = new Jogador[1000]; // instância de um vetor da classe Jogador
        const int tempoTotal = 60; // Tempo total previsto
        int pontos = 0; // Armazena quantidade de pontos do usuário
        int erro = 0; // Armazenará a quantidade de erros
        VetorPalavra asPalavras; // Variável que chamará métodos da classe
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
            pontos = erro = 0; // Zerar os pontos adquiridos pelo usuário
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "") // Caso algum usuário tenha inserido seu nome
            {
                ResetarJogo(); // Atualização da interface
                HabilitarBotoes(true);
                int aleatorio = new Random().Next(25); // Onjeto responsável por sortear as palavras do arquivo texto lido
                palavraAtual = asPalavras[aleatorio]; // Sorteio de uma palavra
                if (chkDica.Checked) // Verifica se o usuário teve a escolha do uso da dica
                {
                    lbDica.Text = $"Dica: {palavraAtual.Dica}"; // Exibição da dica
                }
                tmTemporizador.Start(); // Inicia o "crônometro"
                dgvPalavra.ColumnCount = palavraAtual.PalavraSelec.Length; // Formatação do "dgvPalavra", comportando o tamanho necessário
                                                                           // à palavra
            }
            else
                MessageBox.Show("Por favor, insira seu nome"); // Usuário não inseriu seu nome
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            asPalavras = new VetorPalavra(100); // Novo vetor que contém os dados lidos
            dlgAbrir.Title = "Selecione o arquivo desejado";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                asPalavras.LeituraDoVetor(dlgAbrir.FileName); // Leitura do arquivo texto
            }

            // Componente da parte 2 do projeto

            int indice = 0; // Índice resposável por percorrer os botões do "imlBotoes"
            barraDeItens.ImageList = imlBotoes;
            foreach (ToolStripItem item in barraDeItens.Items)
                if (item is ToolStripButton) // "Condição de existência"
                    (item as ToolStripButton).ImageIndex = indice++; // Cada posição do botão, para que possa ser exibido
        }

        private void TmTemporizador_Tick(object sender, EventArgs e)
        {
            tempoPassado++;

            lbTemporizador.Text = $"Tempo restante: {tempoTotal - tempoPassado}s"; // Exibição do tempo restante
            lbPontos.Text = $"Pontos:{pontos}"; // Constante atualização dos pontos
            lbErros.Text = $"Erros: {erro}"; // Constante atualizãção dos erros
            if (tempoPassado == 60) // Caso ultrapasse o tempo previsto
            {
                tmTemporizador.Stop(); // Parada do "cronômetro"
                if (MessageBox.Show("Infelizmente o tempo esgotou!\nDeseja jogar novamente?", "Fim de jogo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ResetarJogo(); // Atualização do formulário
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
                    CadastraJogador("Ranking.txt"); // Grava o usuário ou soma pontos feitos pelo usuário
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
                if (terminou) // Caso o usuário tenha vencido o jogo e consequentemente encerrado
                {
                    CadastraJogador("Ranking.txt"); // Grava o usuário ou soma pontos feitos pelo usuário
                    if (MessageBox.Show("Parabéns, você ganhou!\nDeseja jogar novamente?", "Fim de jogo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ResetarJogo(); // Atualização do formulário
                    }
                    else
                        Close();
                }
                pontos++; // Soma dos pontos feitos pelo usuário
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
                vet[posicaoAIncluir] = new Jogador(linha); // Novo usuário cadastrado
                if (vet[posicaoAIncluir].Nome.Trim() == txtUsuario.Text)
                {
                    vet[posicaoAIncluir].Somar(pontos); // Soma dos pontos do usuário já cadastrado
                    achouNome = true;
                }
                posicaoAIncluir++; // Avança posição
            }
            arqLido.Close();
            var escritor = new StreamWriter(nomeArq); // Escritor do arquivo
            for (int i = 0; i < posicaoAIncluir; i++)
                escritor.WriteLine(vet[i].Nome.PadRight(30, ' ') + vet[i].Pontuacao); // Inclusão de um novo usuário
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

        private void LimparTela() // Método responsável por atualizar o "TextBox" de cada tipo de dado, limpando tal campo
        {
            edPalavra.Clear();
            edDica.Clear();
        }

        private void AtualizarTela() // Exibir constantemente no formulário os dados atuais(palavra e dica)
        {
            if (!asPalavras.EstaVazio) // Caso haja conteúdo no vetor
            {
                int indice = asPalavras.PosicaoAtual;
                edPalavra.Text = asPalavras[indice].PalavraSelec; // Escreve a palavra no "TextBox"
                edDica.Text = asPalavras[indice].Dica; // Escreve a dica no "TextBox"
                TestarBotoes();
                dgvManutencao.RowCount = asPalavras.Tamanho;
                for (int indiceA = 0; indiceA < asPalavras.Tamanho; indiceA++) // Escreverá todos os dados do vetor(palavra e dica)
                {
                    Palavra palavra = asPalavras[indiceA];
                    dgvManutencao.Rows[indiceA].Cells[0].Value = indiceA;
                    dgvManutencao.Rows[indiceA].Cells[1].Value = palavra.PalavraSelec; 
                    dgvManutencao.Rows[indiceA].Cells[2].Value = palavra.Dica;
                }
            }
        }

        private void TestarBotoes() // Hablitar botões durante o carregar do formulário
        {
            btnInicio.Enabled = true;
            btnVoltar.Enabled = true;
            btnAvancar.Enabled = true;
            btnUltimo.Enabled = true;
            if (asPalavras.EstaNoInicio) // Caso esteja no início do vetor
            {
                btnInicio.Enabled = false;
                btnVoltar.Enabled = false;
            }
            if (asPalavras.EstaNoFim) // Caso esteja no fim do vetor
            {
                btnAvancar.Enabled = false;
                btnUltimo.Enabled = false;
            }
        }


        private void BtnInicio_Click(object sender, EventArgs e)
        {
            // Atualizar "dgvManutencao" para que os dados(palavra e dica)selecionados anteriormente
            // não estejam marcados
            if (asPalavras.PosicaoAtual != 100)
                dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;
            asPalavras.PosicionarNoPrimeiro();
            AtualizarTela(); // Atualização constante do formulário
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            // Atualizar "dgvManutencao" para que os dados(palavra e dica)selecionados anteriormente
            // não estejam marcados
            if (asPalavras.PosicaoAtual != 100)
                dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;
            asPalavras.RetrocederPosicao();
            AtualizarTela(); // Atualização constante do formulário
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void BtnAvancar_Click(object sender, EventArgs e)
        {
            // Atualizar "dgvManutencao" para que os dados(palavra e dica)selecionados anteriormente
            // não estejam marcados
            if (asPalavras.PosicaoAtual != -1)
                dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;
            asPalavras.AvancarPosicao();
            AtualizarTela(); // Atualização constante do formulário
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void BtnUltimo_Click(object sender, EventArgs e)
        {
            // Atualizar "dgvManutencao" para que os dados(palavra e dica)selecionados anteriormente
            // não estejam marcados
            if (asPalavras.PosicaoAtual != -1) 
              dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = false;   
            asPalavras.PosicionarNoUltimo();
            AtualizarTela(); // Atualização constante do formulário
            dgvManutencao.Rows[asPalavras.PosicaoAtual].Selected = true;
        }

        private void DgvManutencao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            asPalavras.PosicaoAtual = e.RowIndex; // Posição pressionado pelo usuário
            AtualizarTela(); // Atualização constante do formulário 
        }

        private void BtnIncluir_Click(object sender, EventArgs e)
        {
            asPalavras.SituacaoAtual = VetorPalavra.Situacao.incluindo; // Está no modo de inclusão
            LimparTela(); // Atualização constante do formulário
            edPalavra.Focus();
        }

        private void EdPalavra_Leave(object sender, EventArgs e)
        {
            if (edPalavra.Text == "") // Caso algo não fosse digitado pelo usuário, um alerta seria disparado
                MessageBox.Show("Por favor, digite uma palavra");
            else
            {
                var palavraProc = new Palavra(edPalavra.Text, "");
                switch (asPalavras.SituacaoAtual) // Escolhas possíveis ao usuário
                {
                    case VetorPalavra.Situacao.incluindo: // Modo de inclusão
                        if (asPalavras.Existe(palavraProc, ref ondeAchar)) // Verifica se a palavra já existe no vetor
                        {
                            MessageBox.Show("Palavra repetida, por favor insira outra palavra.");
                            asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando; // Está no modo de navegação
                            AtualizarTela(); // Atualização constante do formulário
                        }
                        else
                        {
                            edDica.Focus();
                        }
                        break;
                    case VetorPalavra.Situacao.editando: // Está no mode de edição
                        if (asPalavras.Existe(palavraProc, ref ondeAchar)) // Verifica se a palavra já existe no vetor
                        {
                            asPalavras.PosicaoAtual = ondeAchar; // Atualizar a posição do dado para a posição a ser alterada
                            AtualizarTela(); // Atualização constante do formulário
                            asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando; // Está no modo de navegação
                        }
                        break;
                }
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (asPalavras.SituacaoAtual == VetorPalavra.Situacao.incluindo) // Verifica se está no modo de inclusão
            {
                var novaPalavra = new Palavra(edPalavra.Text, edDica.Text);


                asPalavras.Incluir(novaPalavra); // Inclusão da nova palavra digitada pelo usuário
                
                
                asPalavras.PosicaoAtual = ondeAchar; // Atualizar a posição onde está a linha atual(dica e palavra)
                AtualizarTela(); // Atualização constante do formulário
                asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando; // Encerra o modo de inclusão
            }
            else
        if (asPalavras.SituacaoAtual == VetorPalavra.Situacao.editando) // Verifica se está no modo de edição
            {
                asPalavras[asPalavras.PosicaoAtual] = new Palavra(edPalavra.Text, edDica.Text);

                asPalavras.SituacaoAtual = VetorPalavra.Situacao.navegando; // Está no modo de navegação
                edPalavra.ReadOnly = false;
                AtualizarTela(); // Atualização constante do formulário
            }
        }

        private void FrmForca_FormClosing(object sender, FormClosingEventArgs e)
        {
            asPalavras.GravarEmDisco(dlgAbrir.FileName); // Gravação de palavra e dica caso o usuário tenha feito a inclusão
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
                    asPalavras.PosicionarNoUltimo(); // Posicionar na última posição do vetor
                AtualizarTela(); // Atualização constante do formulário
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            asPalavras.SituacaoAtual = VetorPalavra.Situacao.editando; // Está no modo de edição
            edPalavra.Focus();
        }

        private void BtnOrdenar_Click(object sender, EventArgs e)
        {
            asPalavras.OrdenarSimples(); // Ordenação alfabética do vetor
            AtualizarTela(); // Atualização constante do formulário
        }
    }
}
