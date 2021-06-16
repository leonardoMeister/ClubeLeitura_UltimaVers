using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Amigo : EntidadeBase
    {
        public string nome;
        public string nomeResponsavel;
        public string telefone;
        public string deOndeEh;
        private List<Emprestimo> historicoEmprestimos;
        private static int idClassEmp = 0;

        public int GerarId()
        {
            int ids = idClassEmp;
            idClassEmp++;
            return ids;
        }
        public Amigo()
        {
        }
        public Amigo(int id)
        {
            this.id = id;
        }

        public Amigo(string nome, string nomeResponsavel,string telefone, string deOndeEh)
        {
            this.id = GerarId();
            this.nome = nome;
            this.nomeResponsavel = nomeResponsavel;
            this.telefone = telefone;
            this.deOndeEh = deOndeEh;
        }

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos.Add( emprestimo);
        }

        public bool TemEmprestimoEmAberto()
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

        public string Validar()
        {
            return "AMIGO_VALIDO";
        }

        public override bool Equals(object obj)
        {
            Amigo a = (Amigo)obj;

            return id == a.id;
        }
    }
}