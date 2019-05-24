using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


class Jogador
{

    Jogador[] dados; //vetor dados de jogadores
    int qtosDados;   //variável de quantos jogadores foram lidos

    const int tamanhoNome = 15;
    const int tamanhoPontuacao = 1;

    const int inicioNome = 0;
    const int inicioPontuacao = inicioNome + tamanhoNome;

    string nome;
    int pontuacao;

    public string Nome { get => nome; set => nome = value; }
    public int Pontuacao { get => pontuacao; set => pontuacao = value; }

    public Jogador() //construtor da classe, que delimita o tamanho máximo do vetor e zera qtosDados
    {
        dados = new Jogador[100];
        qtosDados = 0;
    }
    public void LerArquivo(StreamReader arq)
    {
        while (!arq.EndOfStream && qtosDados < 100) //enquanto o arquivo não acabou e o número de jogadores não for maior que 100
        {
            dados[qtosDados] = new Jogador(); //instanciação do jogador de índice qtosDados
            dados[qtosDados].LerArquivo(arq);   //ler dados do jogador
            qtosDados++;                      //aumentar um jogador
        }
        arq.Close(); //fechar arquivo
    }
    public bool Existe(string nomeProcurado)
    {
        bool achou = false;
        for (int ind = 0; ind < qtosDados; ind++) //percorrer todo o vetor
            if (dados[ind].Nome == nomeProcurado) //se o nome do jogador atual for igual ao nome procurado
                achou = true; //o jogador foi achado
        return achou; //retornar se o jogador foi achado ou não
    }

    public void Incluir(Jogador novoJogador) //método incluir com parâmetro novoJogador da classe Jogador
    {
        if (qtosDados > 99) //se o número de jogadores for maior que 99, não será possível adiconar mais jogadores ao vetor
            throw new ArgumentOutOfRangeException("Não há espaço para mais jogaores!");
        else
        {
            dados[qtosDados] = novoJogador; //dados de qtosDados recebe o jogador
            qtosDados++; //aumenta-se o número de jogadores do vetor
        }
    }

    public void GravarDados(StreamWriter saida)
    {
        for (int ind = 0; ind < qtosDados; ind++) //para cada jogador do vetor
        {
            saida.WriteLine(dados[ind].ToString()); //escrever dados no arquivo texto
        }
        saida.Close(); //fechar arquivo
    }

    public void MudarPontuacao(string nomeDoJogador, int novosPontos)
    {
        if (this.Existe(nomeDoJogador)) //se esse jogador existe
        {
            for (int ind = 0; ind < qtosDados; ind++) //percorrer todo o vetor
                if (dados[ind].Nome == nomeDoJogador) //se for o jogador procurado
                    dados[ind].AtualizarPontos(novosPontos); //atualizar pontuação
        }
    }

    public void AtualizarPontos(int pontosAdiquiridos)
    {
        pontuacao += pontosAdiquiridos; //soma dos pontos antigos com os novos pontos
    }

    public void OrdenarVetor()
    {
        for (int lento = 0; lento < qtosDados; lento++) //índice lento percorre todo o vetor
        {
            int indiceMaisPontos = lento; //indiceMaisPontos começa com o valor do índice lento
            for (int rapido = lento + 1; rapido < qtosDados; rapido++) //índice rápido percorre todo o vetor
                if (dados[rapido].Pontuacao > dados[indiceMaisPontos].Pontuacao) //se os pontos do jogador com índice rápido for maior que os pontos do jogador de índice indiceMaisPontos
                    indiceMaisPontos = rapido; //índice do jogador com mais pontos recebe índice rápido
            if (lento != indiceMaisPontos) //apenas se lento for diferente de indiceMaisPontos, altera-se o vetor
            {
                Jogador aux = dados[indiceMaisPontos]; //auxiliar recebe dados de índice com mais pontos
                dados[indiceMaisPontos] = dados[lento]; //dados de indiceMaisPontos recebe dados de lento
                dados[lento] = aux; //dados de lento recebe auxiliar
                                    //Com isso, o jogador com mais pontos é posicionado no começo
            }
        }
    }
}


