using System;
using static System.Console;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;

class VetorPalavra
{
    Situacao situacaoAtual;
    int tamMaxPalavras = 100;   // Quantidade de palavras que farão parte do vetor
    int qtsPalavras;   // Número de palavras
    Palavra[] palavra;

    const int tamanhoPalavra = 15;
    const int tamanhoDica = 100;

    const int inicioPalavra = 0;
    const int inicioDica = inicioPalavra + tamanhoPalavra;

    public Palavra this[int i]
    {
        get
        {
            if (i < 0 || i >= qtsPalavras)  // inválido
                throw new Exception("Índice inválido!");

            return palavra[i];
        }
        set
        {
            if (i < 0 || i >= qtsPalavras)
                throw new Exception("Índice fora dos limites do vetor!");

            palavra[i] = value;
        }
    }

    public VetorPalavra(int tamanhoVetor)
    {
        palavra = new Palavra[tamanhoVetor];
        qtsPalavras = 0;
        tamMaxPalavras = tamanhoVetor;
        qtsPalavras = 0;
        posicaoAtual = -1;  // não está posicionado ainda
        situacaoAtual = Situacao.navegando;
    }
    public void LerDados(string nomeArquivo)
    {
        var arq = new StreamReader(nomeArquivo, Encoding.Default);
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
            string palavraLida = linha.Substring(inicioPalavra, tamanhoPalavra).Trim();
            string dicaLida = linha.Substring(inicioDica).Trim();
            var palavraAtual = new Palavra(palavraLida, dicaLida);
            Incluir(palavraAtual); // Insere a palavra no veto
        }
    }

    public enum Situacao
    {
        navegando, pesquisando, incluindo, editando, excluindo
    }


    int tamanhoMaximo;     // tamanho físico
    Funcionario[] dados;   // vetor interno à classe, armazena os registros

    int posicaoAtual;      // índice o registro exibido na tela
    public int Tamanho  // permite à aplicação consultar o número de registros armazenados
    {
        get => qtsPalavras;
        set => qtsPalavras = value;

    }


    public bool EstaVazio // permite à aplicação saber se o vetor dados está vazio
    {
        get => qtsPalavras <= 0; // se qtosDados <= 0, retorna true
    }
    public int PosicaoAtual
    {
        get => posicaoAtual;
        set
        {
            if (value >= 0 && value < qtsPalavras)
                posicaoAtual = value;
        }
    }




    public void Incluir(Palavra valorAInserir) // insere após o final do vetor e o expande se necessário
    {
        if (qtsPalavras >= palavra.Length)
            ExpandirVetor();

        palavra[qtsPalavras] = valorAInserir;
        qtsPalavras++;
    }

    // insere o novo dado na posição indicada por ondeIncluir
    public void Inserir(Palavra valorAInserir, int ondeIncluir)
    {
        if (qtsPalavras >= tamanhoMaximo)
            ExpandirVetor();

        // desloca para frente os dados posteriores ao novo dado
        for (int indice = qtsPalavras - 1; indice >= ondeIncluir; indice--)
            palavra[indice + 1] = palavra[indice];

        palavra[ondeIncluir] = valorAInserir;
        qtsPalavras++;
    }

    private void ExpandirVetor()
    {
        Palavra[] vetorMaior = new Palavra[palavra.Length + 100];
        for (int indice = 0; indice < qtsPalavras; indice++)
            vetorMaior[indice] = palavra[indice];

        palavra = vetorMaior;
    }

    public void Excluir(int posicaoAExcluir)
    {
        qtsPalavras--;
        for (int indice = posicaoAExcluir; indice < qtsPalavras; indice++)
            palavra[indice] = palavra[indice + 1];

        // pensar em como diminuir o tamanho físico do vetor, para economizar
    }

    public bool ExisteSequencial(Palavra palavraProcurada, ref int indice)
    {
        bool achouIgual = false;
        bool fim = false;
        bool achouMaior = false;
        indice = 0;

        while (!achouIgual && !achouMaior && !fim)
            if (indice >= qtsPalavras)
                fim = true; // indice passou do fim do vetor
            else
              if (palavra[indice].CompareTo(palavraProcurada) > 0)
                achouMaior = true;   // achamos matrícula maior que a procurado
            else
                if (palavra[indice].CompareTo(palavraProcurada) == 0)
                achouIgual = true;
            else
                indice++;

        return achouIgual;
    }



    public bool Existe(Palavra procurado, ref int onde) // Pesquisa binária
    {
        bool achou = false;
        int inicio = 0;
        int fim = qtsPalavras - 1;
        while (!achou && inicio <= fim)
        {
            onde = (inicio + fim) / 2;
            if (palavra[onde].CompareTo(procurado) == 0)
                achou = true;
            else
              if (procurado.CompareTo(palavra[onde]) < 0)
                fim = onde - 1;
            else
                inicio = onde + 1;
        }
        if (!achou)
            onde = inicio; // onde deverá ser incluído o novo registro caso não tenha sido achado
        return achou;
    }
    public void Listar(ListBox lista, string cabecalho)
    {
        lista.Items.Clear();
        lista.Items.Add(cabecalho);
        for (int indice = 0; indice < qtsPalavras; indice++)
            lista.Items.Add(dados[indice]);
    }

    public void Listar()
    {
        for (int indice = 0; indice < qtsPalavras; indice++)
            WriteLine($"{dados[indice],5} ");
    }

    public void Listar(ComboBox lista)
    {
        lista.Items.Clear();
        for (int indice = 0; indice < qtsPalavras; indice++)
            lista.Items.Add(dados[indice]);
    }
    public void Listar(TextBox lista)
    {
        lista.Multiline = true;
        lista.ScrollBars = ScrollBars.Both;
        lista.Clear();
        for (int indice = 0; indice < qtsPalavras; indice++)
            lista.AppendText(dados[indice] + Environment.NewLine);
    }

    public void GravarEmDisco(string nomeArq)
    {
        var arq = new StreamWriter(nomeArq, false, Encoding.Default);
        for (int i = 0; i < qtsPalavras; i++)
            arq.WriteLine(palavra[i].ParaArquivo());
        arq.Close();
    }

    

    public void OrdenarSimples()
    {
        for (int lento = 0; lento < qtsPalavras; lento++)
            for (int rapido = lento + 1; rapido < qtsPalavras; rapido++)
                if (palavra[rapido].CompareTo(palavra[lento]) < 0)
                {
                    Palavra aux = palavra[lento];
                    palavra[lento] = palavra[rapido];
                    palavra[rapido] = aux;
                }
    }

    public void Ordenar()  // Straight-select sort
    {
        for (int lento = 0; lento < qtsPalavras; lento++)
        {
            int indiceDoMenor = lento;
            for (int rapido = lento + 1; rapido < qtsPalavras; rapido++)
                if (palavra[rapido].CompareTo(palavra[indiceDoMenor]) < 0)
                    indiceDoMenor = rapido;
            if (indiceDoMenor != lento)
            {
                Palavra aux = palavra[lento];
                palavra[lento] = palavra[indiceDoMenor];
                palavra[indiceDoMenor] = aux;
            }
        }
    }

    public Funcionario ValorDe(int qualPosicao)
    {
        if (qualPosicao < 0 || qualPosicao >= qtsPalavras)  // inválido
            throw new Exception("Índice inválido!");

        return dados[qualPosicao];
    }

    public void Alterar(int indice, Funcionario novoDado)
    {
        if (indice >= 0 && indice < qtsPalavras)
            dados[indice] = novoDado;
        else
            throw new Exception("Índice fora dos limites do vetor!");
    }

    public void PosicionarNoPrimeiro()
    {
        if (!EstaVazio)
            posicaoAtual = 0; // primeiro elemento do vetor
        else
            posicaoAtual = -1; // antes do início do vetor
    }
    public void PosicionarNoUltimo()
    {
        if (!EstaVazio)
            posicaoAtual = qtsPalavras - 1; // última posição usada do vetor
        else
            posicaoAtual = -1; // indica antes do vetor vazio
    }
    public void AvancarPosicao()
    {
        if (!EstaNoFim)
            posicaoAtual++;
    }
    public void RetrocederPosicao()
    {
        if (!EstaNoInicio)
            posicaoAtual--;
    }
    public bool EstaNoInicio
    {
        get => posicaoAtual <= 0; // primeiro índice
    }
    public bool EstaNoFim
    {
        get => posicaoAtual >= qtsPalavras - 1; // último índice
    }
    internal Situacao SituacaoAtual
    {
        get => situacaoAtual;
        set => situacaoAtual = value;
    }
}
