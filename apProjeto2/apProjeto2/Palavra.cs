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

    /*public Palavra(string palavraSelec, string dicaSelec)
    {
        var palavra = palavraSelec;
        var dica = dicaSelec;
    }*/

    public  Palavra()
    {

    }




    /*public string PalavraSelec()
    {
        get => palavra;
        set
        {
            if (value.Length > tamanhoPalavra)
                value = value.Substring(0, tamanhoPalavra);
            palavra = value.PadRight(tamanhoPalavra, ' ');
        }
    }*/
   
    /* public double
    {
        get => salario;
        set
        {
            if (value < 0)
                throw new InvalidDataException("Salário inválido!");
            salario = value;
        }
    }

    public Funcionario(int mat, string nom, double sal)
    {
        Matricula = mat;
        Nome = nom;
        Salario = sal;
    }
    public Funcionario(string linha)
    {
        Matricula = int.Parse(linha.Substring(inicioMatricula, tamanhoMatricula));
        Nome = linha.Substring(inicioNome, tamanhoNome);
        Salario = double.Parse(linha.Substring(inicioSalario, tamanhoSalario));
    }
    public override String ToString()
    {
        return Matricula.ToString().PadLeft(5, ' ') + "   " + Nome + Math.Round(Salario, 2).ToString().PadLeft(13, ' ');
    }

    public String ParaArquivo()
    {
        return Matricula.ToString().PadLeft(5, ' ') + Nome + Math.Round(Salario, 2).ToString().PadLeft(10, ' ');
    }
}*/
