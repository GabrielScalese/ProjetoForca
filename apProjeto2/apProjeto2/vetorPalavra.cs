using System;
using static System.Console;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

class VetorPalavra
{
    int tamMaxPalavras = 100; //quantidade de palavras que farão parte do vetor
    int qtsPalavras;
    Palavra[] palavra;

    public Palavra this[int i]
    {
        get => palavra[i];
    }

    public VetorPalavra(int tamanhoVetor)
    {
        palavra = new string[tamanhoVetor];
        qtsPalavras = 0;
        tamMaxPalavras = tamanhoVetor;
    }
    public void LerDados(string nomeArquivo)
    {
        var arq = new StreamReader(nomeArquivo);
        qtsPalavras = 0;
        while (!arq.EndOfStream)
        {
            string palavraLida = arq.ReadLine();
            InserirAposFim(palavraLida);
        }
        arq.Close();
    }
    public void InserirAposFim(string palavraAInserir)
    {
        palavra[qtsPalavras] = palavraAInserir;
        qtsPalavras++;
    }
    public void Listar()
    {
        for (int indice = 0; indice < qtsPalavras; indice++)
            Write($"{palavra[indice],5} ");
    }
    public void Listar(ListBox lista)
    {
        lista.Items.Clear();
        for (int indice = 0; indice < qtsPalavras; indice++)
            lista.Items.Add(palavra[indice]);
    }

    public string ToString(string separador)
    {
        string resultado = "";
        for (int indice = 0; indice < qtsPalavras; indice++)
            resultado += palavra[indice] + separador;
        return resultado;
    }



}