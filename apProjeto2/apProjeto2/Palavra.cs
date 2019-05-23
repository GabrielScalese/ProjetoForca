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

    public string PalavraSelec
    {
        get => palavra;
        set
        {
            if (value.Length > tamanhoPalavra)
                value = value.Substring(0, tamanhoPalavra);
            palavra = value.PadRight(tamanhoPalavra, ' ');
        }
    }
}
