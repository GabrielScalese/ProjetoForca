using System;
using static System.Console;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

using System.Text;
class VetorPalavra
{
    Situacao situacaoAtual; // Variável responsável por definir o modo atual
    int tamMaxPalavras = 100;   // Quantidade de palavras que farão parte do vetor
    int qtsPalavras;   // Número de palavras
    Palavra[] palavra; // Vetor que armazenará os dados lidos

    const int tamanhoPalavra = 15;  // Tamanho constante da palavra
    const int tamanhoDica = 100;  // Tamanho constante da dica

    const int inicioPalavra = 0; // Início da palavra(posição)
    const int inicioDica = inicioPalavra + tamanhoPalavra; // Início da dica(posição)

    public Palavra this[int i] // Construtor da classe "Palavra"
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

    public VetorPalavra(int tamanhoVetor) // Construtor da classe "VetorPalavra"
    {
        palavra = new Palavra[tamanhoVetor];
        qtsPalavras = 0;
        tamMaxPalavras = tamanhoVetor;
        qtsPalavras = 0;
        posicaoAtual = -1;  // Não está posicionado ainda
        situacaoAtual = Situacao.navegando;
    }
<<<<<<< HEAD
    public void InserirAposFim(Palavra palavraASerUsada) // Inserir na última posição do vetor
=======
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
>>>>>>> bad5e2b71074abba78ed2bafe09d35cbebb843ac
    {
        palavra[qtsPalavras++] = palavraASerUsada;

    }
    public string ToString(string separador) // Conversão do dado
    {
        string resultado = "";
        for (int indice = 0; indice < qtsPalavras; indice++)
            resultado += palavra[indice] + separador;
        return resultado;
    }

    public void LeituraDoVetor(string nomeArq) // Leitura do arquivo texto
    {
        var arq = new StreamReader(nomeArq);
        while (!arq.EndOfStream)
        {
            string linha = arq.ReadLine();
            string palavraLida = linha.Substring(inicioPalavra, tamanhoPalavra).Trim();
            string dicaLida = linha.Substring(inicioDica).Trim();
            var palavraAtual = new Palavra(palavraLida, dicaLida);
            Incluir(palavraAtual); // Insere a palavra no vetor
        }
    }

    public enum Situacao // Permite a existência de modos
    {
        navegando, pesquisando, incluindo, editando, excluindo
    }

    int tamanhoMaximo; // Tamanho máximo do vetor já estabelecido
    int posicaoAtual;      // Índice do registro exibido na tela
    public int Tamanho  // Permite à aplicação consultar o número de registros armazenados
    {
        get => qtsPalavras;
        set => qtsPalavras = value;

    }
    public bool EstaVazio // Permite à aplicação saber se o vetor dados está vazio
    {
        get => qtsPalavras <= 0; // Se qtosPalavras <= 0, retorna true
    }
    public int PosicaoAtual // Retorna a posição atual no vetor
    {
        get => posicaoAtual;
        set
        {
            if (value >= 0 && value < qtsPalavras)
                posicaoAtual = value;
        }
    }

    public void Incluir(Palavra valorAInserir) // Insere após o final do vetor e o expande se necessário
    {
        if (qtsPalavras >= palavra.Length)
            ExpandirVetor();

        palavra[qtsPalavras] = valorAInserir;
        qtsPalavras++;
    }

    // Insere o novo dado na posição indicada por ondeIncluir
    public void Inserir(Palavra valorAInserir, int ondeIncluir)
    {
        if (qtsPalavras >= tamanhoMaximo)
            ExpandirVetor();

        // Desloca para frente os dados posteriores ao novo dado
        for (int indice = qtsPalavras - 1; indice >= ondeIncluir; indice--)
            palavra[indice + 1] = palavra[indice];

        palavra[ondeIncluir] = valorAInserir;
        qtsPalavras++;
    }

    private void ExpandirVetor() // Expansão do vetor caso tamanho "antigo" do vetor não comporte tamanho suficiente à 
                                 // inclusão de dados, por exemplo
    {
        Palavra[] vetorMaior = new Palavra[palavra.Length + 100];
        for (int indice = 0; indice < qtsPalavras; indice++)
            vetorMaior[indice] = palavra[indice];

        palavra = vetorMaior;
    }

    public void Excluir(int posicaoAExcluir) // Exclusão de um dado do vetor
    {
        qtsPalavras--;
        for (int indice = posicaoAExcluir; indice < qtsPalavras; indice++)
            palavra[indice] = palavra[indice + 1];

        
    }

    public bool Existe(Palavra procurado, ref int onde) // Pesquisa binária no vetor
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
            onde = inicio; // Onde deverá ser incluído o novo registro caso não tenha sido achado
        return achou;
    }
    
    public void GravarEmDisco(string nomeArq) // Usado para gravar o nome do usuário; palavras e dicas novas
    {
        var arq = new StreamWriter(nomeArq, false, Encoding.Default);
        for (int i = 0; i < qtsPalavras; i++)
            arq.WriteLine(palavra[i].ParaArquivo());
        arq.Close();
    }

    public void OrdenarSimples() // Ordenação alfabética do vetor
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
<<<<<<< HEAD
    public void PosicionarNoPrimeiro() // Posicionar na primeira posição do vetor
=======

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
>>>>>>> bad5e2b71074abba78ed2bafe09d35cbebb843ac
    {
        if (!EstaVazio)
            posicaoAtual = 0; // primeiro elemento do vetor
        else
            posicaoAtual = -1; // antes do início do vetor
    }
    public void PosicionarNoUltimo() // Posicionar na última posição do vetor
    {
        if (!EstaVazio)
            posicaoAtual = qtsPalavras - 1; // última posição usada do vetor
        else
            posicaoAtual = -1; // indica antes do vetor vazio
    }
    public void AvancarPosicao() // Avançar posição no vetor
    {
        if (!EstaNoFim)
            posicaoAtual++;
    }
    public void RetrocederPosicao() // Voltar uma posição no vetor
    {
        if (!EstaNoInicio)
            posicaoAtual--;
    }
    public bool EstaNoInicio // Verifica se está na primeira posição do vetor
    {
        get => posicaoAtual <= 0; // primeiro índice
    }
    public bool EstaNoFim // Verifica se está na última posição do vetor
    {
        get => posicaoAtual >= qtsPalavras - 1; // último índice
    }
    internal Situacao SituacaoAtual // Retorna o modo atual
    {
        get => situacaoAtual;
        set => situacaoAtual = value;
    }
}
