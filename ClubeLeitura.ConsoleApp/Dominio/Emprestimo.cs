﻿using System;

namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo amiguinho;
        public Revista revistinha;
        public DateTime dataEmprestimo;
        public bool estaAberto;
        public DateTime dataDevolucao;
        private static int idClassEmp = 0;

        public int  GerarId()
        {
            int ids = idClassEmp;
            idClassEmp++;
            return ids;
        }

        public int Mes 
        { 
            get 
            {
                return dataDevolucao.Month; 
            }
        }
        public Emprestimo(int id)
        {
            this.id = id;
        }
        public Emprestimo(Amigo amigo, Revista revista, DateTime dtEmprestimo)
        {
            this.id = GerarId();
            amiguinho = amigo;
            revistinha = revista;
            dataEmprestimo = dtEmprestimo;

            Abrir();
        }

        private void Abrir()
        {
            if (estaAberto == false)
            {
                estaAberto = true;
            }
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            if (amiguinho.TemEmprestimoEmAberto())
                resultadoValidacao += "O amiguinho está com empréstimo em aberto \n";

            if (revistinha.EstaEmprestada())
                resultadoValidacao += "A Revistinha está emprestada \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "EMPRESTIMO_VALIDO";

            return resultadoValidacao;
        }

        public void Fechar(DateTime data)
        {
            if (estaAberto)
            {
                estaAberto = false;
                dataDevolucao = data;
            }
        }

        internal bool EstaFechado()
        {
            return estaAberto == false;
        }
    }
}
