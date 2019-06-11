using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class Palavra
{
    string palavra; // Variável palavra
    string dica; // Variável dica

    const int tamanhoPalavra = 15; // Tamanho constante da palavra
    const int tamanhoDica = 100; // Tamanho constante da dica

    const int inicioPalavra = 0; // Início da palavra(posição)
    const int inicioDica = inicioPalavra + tamanhoPalavra; // Início da dica(posição)


    public string Dica // Propriedade da dica
    {
        get => dica;  // Retorna string da dica
    }

    public Palavra(string umaPalavra, string umaDica) // Construtor da classe "Palavra"
    {
        this.palavra = umaPalavra;
        this.dica = umaDica;
    }

    public string PalavraSelec // Retorna e alteração do valor da string palavra(componente do vetor)
    {
        get => palavra;
        set
        {
            if (value.Length > tamanhoPalavra)
                value = value.Substring(0, tamanhoPalavra);
            palavra = value.PadRight(tamanhoPalavra, ' ');
        }
    }

    public int CompareTo(Palavra outro) // Método responsável por comparar "strings"
    {
        int resultado = String.Compare(palavra, outro.palavra);
        if (resultado < 0)
            return -1;     // qualquer inteiro negativo

        if (resultado > 0)
            return 1;         // quando os valores comparados são iguais

        return 0;  // qualquer inteiro positivo

        // return matricula - outro.matricula;
    }

    public override String ToString() // Retorna a linha de dados(palavra e dica)
    {
        return PalavraSelec+ "   " + Dica;
    }

    public String ParaArquivo() // Formatação do arquivo texto
    {
        return PalavraSelec.PadRight(15, ' ') + Dica;
    }
}
