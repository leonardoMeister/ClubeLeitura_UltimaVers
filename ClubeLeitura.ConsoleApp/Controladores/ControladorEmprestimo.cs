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
                AdicionarRegistro(emprestimo);
            }
            return resultadoValidacao;
        }

        internal bool RegistrarDevolucao(int idEmprestimo, DateTime data)
        {
            Emprestimo emprestimo = SelecionarRegistroPorId(new Emprestimo(idEmprestimo));
            emprestimo.Fechar(data);

            return true;
        }

        internal List<Emprestimo> SelecionarEmprestimosAbertos()
        {
            List<Emprestimo> emprestimosAbertos = new List<Emprestimo>();

            List<Emprestimo> lista = SelecionarTodosRegistros();

            foreach (Emprestimo e in lista)
            {
                if (e.estaAberto)
                {
                    emprestimosAbertos.Add(e);
                }
            }

            return emprestimosAbertos;
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