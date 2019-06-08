using System;
using System.IO;
public class Funcionario : IComparable<Funcionario>
{
  const int tamanhoMatricula = 5;
  const int tamanhoNome      = 30;
  const int tamanhoSalario   = 10;

  const int inicioMatricula = 0;
  const int inicioNome = inicioMatricula + tamanhoMatricula;
  const int inicioSalario = inicioNome + tamanhoNome;

  int matricula;
  string nome;
  double salario;

  public int CompareTo(Funcionario outro)
  {
    if (matricula < outro.matricula)
      return -1;     // qualquer inteiro negativo

    if (matricula == outro.matricula)
      return 0;         // quando os valores comparados são iguais

    return 1;  // qualquer inteiro positivo

    // return matricula - outro.matricula;
  }
  public int Matricula
  {
    get => matricula;
    set
    {
      if (value < 1)
        throw new InvalidDataException("Matrícula inválida!");
      matricula = value;
    }
  }
  public string Nome
  {
    get => nome;
    set
    {
      if (value.Length > tamanhoNome)
         value = value.Substring(0, tamanhoNome);
      nome = value.PadRight(tamanhoNome, ' ');
    }
  }
  public double Salario
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
    return Matricula.ToString().PadLeft(5,' ') + "   "+ Nome + Math.Round(Salario, 2).ToString().PadLeft(13,' ');
  }

  public String ParaArquivo()
  {
    return Matricula.ToString().PadLeft(5, ' ') + Nome + Math.Round(Salario, 2).ToString().PadLeft(10, ' ');
  }

}

