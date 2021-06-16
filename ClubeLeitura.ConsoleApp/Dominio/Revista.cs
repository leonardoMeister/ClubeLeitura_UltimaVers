using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Revista : EntidadeBase
    {
        public string nome;
        public Caixa caixa;
        public string colecao;
        public int numeroEdicao;
        private List<Emprestimo> historicoEmprestimos;
        private static int idClassEmp = 0;

        public int GerarId()
        {
            int ids = idClassEmp;
            idClassEmp++;
            return ids;
        }

        internal string Validar()
        {
            return "REVISTA_VALIDA";
        }
        public Revista(int id)
        {
            this.id = id;
        }
        public Revista(string nome, string colecao, int numeroEdicao, Caixa caixa)
        {
            historicoEmprestimos = new List<Emprestimo>();
            this.id = GerarId();
            this.nome = nome;
            this.colecao = colecao;
            this.numeroEdicao = numeroEdicao;
            this.caixa = caixa;
        }

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos.Add( emprestimo);
        }

        internal bool EstaEmprestada()
        {
            bool temEmprestimoEmAberto = false;

            foreach (Emprestimo emprestimo in historicoEmprestimos)
            {
                if (emprestimo != null && emprestimo.estaAberto)
                {
                    temEmprestimoEmAberto = true;
                    break;
                }
            }
            return temEmprestimoEmAberto;
        }
    }
}
