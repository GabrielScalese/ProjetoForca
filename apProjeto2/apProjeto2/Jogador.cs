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
        Nome = linha.Substring(inicioNome, tamanhoNome);

        Pontuacao = int.Parse(linha.Substring(inicioPontuacao, tamanhoPontuacao));
    }

    public void Somar(int pontuacao)
    {
        Pontuacao += pontuacao;
    }


}


