using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaRevista : TelaGenerica<Revista>, ICadastravel
    {
        private readonly TelaCaixa telaCaixa;
        private readonly ControladorCaixa controladorCaixa;

        private readonly ControladorRevista controladorRevista;

        public TelaRevista(ControladorRevista ctrlRevista, ControladorCaixa ctrlCaixa, TelaCaixa tlCaixa) : base(ctrlRevista, "Revistas")
        {
            controladorRevista = ctrlRevista;
            telaCaixa = tlCaixa;
            controladorCaixa = ctrlCaixa;
        }

        public override void ApresentarTabela(List<Revista> lista)
        {
            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Coleção", "Caixa");

            foreach (Revista revista in lista)
            {
                Console.WriteLine(configuracaColunasTabela, revista.id, revista.nome,
                    revista.colecao, revista.caixa.cor);
            }
        }

        public override Revista Obterregistro()
        {
            telaCaixa.VisualizarRegistros(TipoVisualizacao.Pesquisando);

            Console.Write("\nDigite o número da caixa: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controladorCaixa.ExisteRegistroComEsteId(idCaixa);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhuma Caixa foi encontrada com este número: "
                    + idCaixa, TipoMensagem.Erro);

                ConfigurarTela($"uma revista...");

                return Obterregistro();
            }

            Caixa caixa = controladorCaixa.SelecionarRegistroPorId(new Caixa(idCaixa));

            Console.Write("Digite a nome da revista: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite o número de edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            Revista revista = new Revista(nome, colecao, numeroEdicao, caixa);
            string validacao = revista.Validar();
            if (validacao == "REVISTA_VALIDA")
            {
                ApresentarMensagem("Revista Pega com sucesso", TipoMensagem.Sucesso);
                return revista;
            }

            ApresentarMensagem(validacao, TipoMensagem.Erro);
            Obterregistro();
            return revista;
        }
    }
}
