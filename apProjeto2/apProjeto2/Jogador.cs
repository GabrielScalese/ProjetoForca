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

    const int tamanhoNome = 30; // Tamanho constante do nome
    

    const int inicioNome = 0; // Início do nome(posição)
    const int inicioPontuacao = inicioNome + tamanhoNome; // Início da pontuação(posição)

    string nome; // Variável nome
    int pontuacao; // Variável pontuação

    public string Nome { get => nome; set => nome = value; } // Propriedade do nome
    public int Pontuacao { get => pontuacao; set => pontuacao = value; } // Propriedade da pontuação

    public Jogador() // Construtor da classe, que zera as variáveis
    {
        nome = "";
        pontuacao = 0;
    }

    public Jogador(string nomeDoNovoJogador, int pontosDesseJogador) //construtor de um jogador com parâmetros nome e pontos já definidos,
                                                                     //para o caso de inclusão de um novo jogador, que não estava presente no arquivo texto
    {
        nome = nomeDoNovoJogador;
        pontuacao = pontosDesseJogador;
    }

    public Jogador(string linha) // Construtor da classe
    {
        if (linha != "")
        {
            Nome = linha.Substring(inicioNome, tamanhoNome);

            Pontuacao = int.Parse(linha.Substring(inicioPontuacao));
        }
    }

    public void Somar(int pontuacao) // Soma da pontuação do usuário
    {
        Pontuacao += pontuacao;
    }
}


