using System;
using static System.Console;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

class VetorPalavra
{
    int tamMaxPalavras = 100;   // Quantidade de palavras que farão parte do vetor
    int qtsPalavras;   // Número de palavras
    Palavra[] palavra;

    const int tamanhoPalavra = 15;
    const int tamanhoDica = 100;

    const int inicioPalavra = 0;
    const int inicioDica = inicioPalavra + tamanhoPalavra;

    public Palavra this[int i]
    {
        get => palavra[i];
    }

    public VetorPalavra(int tamanhoVetor)
    {
        palavra = new Palavra[tamanhoVetor];
        qtsPalavras = 0;
        tamMaxPalavras = tamanhoVetor;
    }
    public void LerDados(string nomeArquivo)
    {
        var arq = new StreamReader(nomeArquivo);
        qtsPalavras = 0;
        while (!arq.EndOfStream)
        {
            string linha = arq.ReadLine();
            string palavraLida = linha.Substring(inicioPalavra, tamanhoPalavra);
            string dicaLida = linha.Substring(inicioDica, tamanhoDica);
            InserirAposFim(new Palavra(palavraLida, dicaLida));
        }
        arq.Close();
    }
    public void InserirAposFim(Palavra palavraASerUsada)
    {
        palavra[qtsPalavras++] = palavraASerUsada;

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

    public void LeituraDoVetor(string nomeArq)
    {
        var arq = new StreamReader(nomeArq);
        while (!arq.EndOfStream)
        {
            string linha = arq.ReadLine();

        }
    }

    public string aPalavra //propriedade string que retorna a palavra
    {
        get => palavra;
    }
    public string Dica //propriedade string que retorna a dica
    {
        get => dica;
        set
        {
            if (value.Length > tamanhoDica)  //se a dica for maior que o tamanho máximo
                dica = value.Substring(0, tamanhoDica); //o programa pega 200 caracteres da dica
        }
    }
}