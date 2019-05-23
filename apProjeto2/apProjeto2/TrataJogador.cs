using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apProjeto2
{
    class TrataJogador
    {
        string nome;
        string pontuacao;

        const int tamanhoNome = 15;
        const int tamanhoPontuacao = 100;

        const int inicioNome = 0;
        const int inicioPontuacao = inicioNome + tamanhoNome;


        public string Pontuacao
        {
            get => pontuacao;  // Retorna string da dica
        }

        public TrataJogador(string umNome, string umaPontuacao)
        {
            this.nome = umNome;
            this.pontuacao = umaPontuacao;
        }

        public string NomeSelec
        {
            get => nome;
            set
            {
                if (value.Length > tamanhoNome)
                    value = value.Substring(0, tamanhoNome);
                nome = value.PadRight(tamanhoNome, ' ');
            }
        }



    }
}
