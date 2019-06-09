using System;
using System.IO;
using System.Windows.Forms;
using static System.Console;

public enum Situacao
{
  navegando, pesquisando, incluindo, editando, excluindo
}
class VetorFuncionario 
{
  int tamanhoMaximo;     // tamanho físico
  Funcionario[] dados;   // vetor interno à classe, armazena os registros
  int qtosDados;         // tamanho lógico
  Situacao situacaoAtual;
  int posicaoAtual;      // índice o registro exibido na tela
  public int Tamanho  // permite à aplicação consultar o número de registros armazenados
  {
    get => qtosDados;
  }
  public Situacao SituacaoAtual
  {
    get => situacaoAtual;
    set => situacaoAtual = value;
  }

  public bool EstaVazio // permite à aplicação saber se o vetor dados está vazio
  {
    get => qtosDados <= 0; // se qtosDados <= 0, retorna true
  }
  public int PosicaoAtual
  {
    get => posicaoAtual;
    set
    {
      if (value >= 0 && value < qtosDados)
         posicaoAtual = value;
    }
  }

  public VetorFuncionario(int tamanhoDesejado)
  {
    tamanhoMaximo = tamanhoDesejado;
    dados = new Funcionario[tamanhoMaximo];
    qtosDados = 0;
    posicaoAtual = -1;  // não está posicionado ainda
    situacaoAtual = Situacao.navegando;
  }
  public void LeituraDoVetor(string nomeArq)
  {
    if (!File.Exists(nomeArq))   // se o arquivo não existe
    {
      var arqNovo = File.CreateText(nomeArq);  // criamos o arquivo vazio
      arqNovo.Close();
    }
    var arq = new StreamReader(nomeArq);
    qtosDados = 0;
    while (!arq.EndOfStream)
    {
      string linha = arq.ReadLine();
      var func = new Funcionario(linha);
      Inserir(func);
    }
    arq.Close();
  }

  public void Inserir(Funcionario valorAInserir) // insere após o final do vetor e o expande se necessário
  {
    if (qtosDados >= tamanhoMaximo)
      ExpandirVetor();

    dados[qtosDados] = valorAInserir;
    qtosDados++;
  }

  // insere o novo dado na posição indicada por ondeIncluir
  public void Inserir(Funcionario valorAInserir, int ondeIncluir)
  {
    if (qtosDados >= tamanhoMaximo)
      ExpandirVetor();

    // desloca para frente os dados posteriores ao novo dado
    for (int indice = qtosDados - 1; indice >= ondeIncluir; indice--)
      dados[indice + 1] = dados[indice];

    dados[ondeIncluir] = valorAInserir;
    qtosDados++;
  }

  private void ExpandirVetor()
  {
    tamanhoMaximo += 10;
    Funcionario[] vetorMaior = new Funcionario[tamanhoMaximo];
    for (int indice = 0; indice < qtosDados; indice++)
      vetorMaior[indice] = dados[indice];

    dados = vetorMaior;
  }

  public void Excluir(int posicaoAExcluir)
  {
    qtosDados--;
    for (int indice = posicaoAExcluir; indice < qtosDados; indice++)
      dados[indice] = dados[indice + 1];

    // pensar em como diminuir o tamanho físico do vetor, para economizar
  }

  public bool ExisteSequencial(Funcionario funcProcurado, ref int indice)
  {
    bool achouIgual = false;
    bool fim = false;
    bool achouMaior = false;
    indice = 0;

    while (!achouIgual && !achouMaior && !fim)
      if (indice >= qtosDados)
        fim = true; // indice passou do fim do vetor
      else
        if (dados[indice].CompareTo(funcProcurado) > 0)
        achouMaior = true;   // achamos matrícula maior que a procurado
      else
          if (dados[indice].CompareTo(funcProcurado) == 0)
        achouIgual = true;
      else
        indice++;

    return achouIgual;
  }

  public bool Existe(Funcionario procurado, ref int onde) // Pesquisa binária
  {
    bool achou = false;
    int inicio = 0;
    int fim = qtosDados - 1;
    while (!achou && inicio <= fim)
    {
      onde = (inicio + fim) / 2;
      if (dados[onde].CompareTo(procurado) == 0)
        achou = true;
      else
        if (procurado.CompareTo(dados[onde]) < 0)
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
    for (int indice = 0; indice < qtosDados; indice++)
      lista.Items.Add(dados[indice]);
  }

  public void Listar()
  {
    for (int indice = 0; indice < qtosDados; indice++)
      WriteLine($"{dados[indice],5} ");
  }

  public void Listar(ComboBox lista)
  {
    lista.Items.Clear();
    for (int indice = 0; indice < qtosDados; indice++)
      lista.Items.Add(dados[indice]);
  }
  public void Listar(TextBox lista)
  {
    lista.Multiline = true;
    lista.ScrollBars = ScrollBars.Both;
    lista.Clear();
    for (int indice = 0; indice < qtosDados; indice++)
      lista.AppendText(dados[indice] + Environment.NewLine);
  }

  public void GravarEmDisco(string nomeArq)
  {
    var arq = new StreamWriter(nomeArq);
    for (int i = 0; i < qtosDados; i++)
      arq.WriteLine(dados[i].ParaArquivo());
    arq.Close();
  }

  public void OrdenarSimples()
  {
    for (int lento = 0; lento < qtosDados; lento++)
      for (int rapido = lento + 1; rapido < qtosDados; rapido++)
        if (dados[rapido].CompareTo(dados[lento]) < 0)
        {
          Funcionario aux = dados[lento];
          dados[lento] = dados[rapido];
          dados[rapido] = aux;
        }
  }

  public void Ordenar()  // Straight-select sort
  {
    for (int lento = 0; lento < qtosDados; lento++)
    {
      int indiceDoMenor = lento;
      for (int rapido = lento + 1; rapido < qtosDados; rapido++)
        if (dados[rapido].CompareTo(dados[indiceDoMenor]) < 0)
          indiceDoMenor = rapido;
      if (indiceDoMenor != lento)
      {
        Funcionario aux = dados[lento];
        dados[lento] = dados[indiceDoMenor];
        dados[indiceDoMenor] = aux;
      }
    }
  }

  public Funcionario ValorDe(int qualPosicao)
  {
    if (qualPosicao < 0 || qualPosicao >= qtosDados)  // inválido
      throw new Exception("Índice inválido!");

    return dados[qualPosicao];
  }

  public void Alterar(int indice, Funcionario novoDado)
  {
    if (indice >= 0 && indice < qtosDados)
      dados[indice] = novoDado;
    else
      throw new Exception("Índice fora dos limites do vetor!");
  }

  public Funcionario this[int indice]
  {
    get
    {
      if (indice < 0 || indice >= qtosDados)  // inválido
        throw new Exception("Índice inválido!");

      return dados[indice];
    }
    set
    {
      if (indice < 0 || indice >= qtosDados)
        throw new Exception("Índice fora dos limites do vetor!");

      dados[indice] = value;
    }
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
      posicaoAtual = qtosDados - 1; // última posição usada do vetor
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
    get => posicaoAtual >= qtosDados - 1; // último índice
  }
}