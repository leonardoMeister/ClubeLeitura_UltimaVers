using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaPrincipal : TelaBase
    {
        private readonly ControladorAmigo controladorAmigo;
        private readonly ControladorCaixa controladorCaixa;
        private readonly ControladorRevista controladorRevista;
        private readonly ControladorEmprestimo controladorEmprestimo;

        private readonly TelaCaixa telaCaixa;
        private readonly TelaRevista telaRevista;
        private readonly TelaAmigo telaAmigo;
        private readonly TelaEmprestimo telaEmprestimo;

        public TelaPrincipal() : base("Tela Principal")
        {
            controladorAmigo = new ControladorAmigo();
            controladorCaixa = new ControladorCaixa();
            controladorRevista = new ControladorRevista();
            controladorEmprestimo = new ControladorEmprestimo();

            telaAmigo = new TelaAmigo(controladorAmigo);
            telaCaixa = new TelaCaixa(controladorCaixa);
            telaRevista = new TelaRevista(controladorRevista, controladorCaixa, telaCaixa);
            telaEmprestimo = new TelaEmprestimo(controladorEmprestimo, controladorAmigo,
                controladorRevista, telaRevista, telaAmigo);

            PopularAplicacao();
        }

        private void PopularAplicacao()
        {
            Amigo a1 = new Amigo("Helena", "Alexandre", "321", "Colégio");
            controladorAmigo.AdicionarRegistro(a1);

            Caixa caixa = new Caixa("Azul", "xua-654");
            controladorCaixa.AdicionarRegistro(caixa);

            Revista r = new Revista("Superman", "Trilogia", 10, caixa);
            controladorRevista.AdicionarRegistro(r);
        }

        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Amiguinhos");
                Console.WriteLine("Digite 2 para o Cadastro de Caixas");
                Console.WriteLine("Digite 3 para o Cadastro de Revistas");
                Console.WriteLine("Digite 4 para o Controle de Empréstimos");

                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = telaAmigo;

                if (opcao == "2")
                    telaSelecionada = telaCaixa;

                if (opcao == "3")
                    telaSelecionada = telaRevista;

                if (opcao == "4")
                    telaSelecionada = telaEmprestimo;

                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }

}
