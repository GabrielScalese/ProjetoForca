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

    
}


