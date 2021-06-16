using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorEmprestimo : Controlador<Emprestimo>
    {
        public string RegistrarEmprestimo(Amigo amigo, Revista revista, DateTime data)
        {
            Emprestimo emprestimo = new Emprestimo(amigo, revista, data);

            string resultadoValidacao = emprestimo.Validar();

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
            {
                amigo.RegistrarEmprestimo(emprestimo);
                revista.RegistrarEmprestimo(emprestimo);

                emprestimo.id = emprestimo.GerarId();
                AdicionarRegistro(emprestimo);
            }

            return resultadoValidacao;
        }

        internal bool RegistrarDevolucao(int idEmprestimo, DateTime data)
        {
            Emprestimo emprestimo = (Emprestimo)SelecionarRegistroPorId(new Emprestimo(idEmprestimo));

            emprestimo.Fechar(data);

            return true;
        }
        internal Emprestimo[] SelecionarEmprestimosEmAberto()
        {
            Emprestimo[] emprestimosEmAberto = new Emprestimo[QtdEmprestimosEmAberto()];

            List<Emprestimo> lista= SelecionarTodosRegistros();

            int i = 0;

            foreach (Emprestimo e in lista)
            {
                if (e.estaAberto)
                {
                    emprestimosEmAberto[i++] = e;
                }
            }

            return emprestimosEmAberto;
        }
        private int QtdEmprestimosEmAberto()
        {
            List<Emprestimo> lista = SelecionarTodosRegistros();

            int numeroEmprestimosEmAberto = 0;

            foreach (Emprestimo emprestimo in lista)
            {
                if (emprestimo.estaAberto)
                {
                    numeroEmprestimosEmAberto++;
                }
            }

            return numeroEmprestimosEmAberto;
        }
        internal List<Emprestimo> SelecionarEmprestimosFechados(int mes)
        {
            List<Emprestimo> emprestimosFechados = new List<Emprestimo>();

            List<Emprestimo> lista = SelecionarTodosRegistros();

            foreach (Emprestimo e in lista)
            {
                if (e.EstaFechado() && e.Mes == mes)
                {
                    emprestimosFechados.Add(e);
                }
            }

            return emprestimosFechados;
        }
    }
}