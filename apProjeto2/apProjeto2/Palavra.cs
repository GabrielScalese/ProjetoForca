using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Palavra
{
    string palavra;
    string dica;

    const int tamanhoPalavra = 15;
    const int tamanhoDica = 100;

    const int inicioPalavra = 0;
    const int inicioDica = inicioPalavra + tamanhoPalavra;

    
    public string Dica
    {
        get => dica;  // Retorna string da dica
    }

    public Palavra(string umaPalavra, string umaDica)
    {
        this.palavra = umaPalavra;
        this.dica = umaDica;
    }

    public string PalavraSelec // Retorna e alteração do valor da string palavra (componente do vetor)
    {
        get => palavra;
        set
        {
            if (value.Length > tamanhoPalavra)
                value = value.Substring(0, tamanhoPalavra);
            palavra = value.PadRight(tamanhoPalavra, ' ');
        }
    }

    public int CompareTo(Palavra outro)
    {
        int resultado = String.Compare(palavra, outro.palavra);
        if (resultado < 0)
            return -1;     // qualquer inteiro negativo

        if (resultado > 0)
            return 1;         // quando os valores comparados são iguais

        return 0;  // qualquer inteiro positivo

        // return matricula - outro.matricula;
    }

    public override String ToString()
    {
        return PalavraSelec+ "   " + Dica;
    }

    public String ParaArquivo()
    {
        return PalavraSelec.PadRight(15, ' ') + Dica;
    }

}
