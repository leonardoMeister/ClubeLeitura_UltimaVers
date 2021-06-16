using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaCaixa : TelaGenerica<Caixa>, ICadastravel
    {
        private readonly ControladorCaixa controladorCaixa;
        public TelaCaixa(ControladorCaixa controlador) : base(controlador, "Caixa")
        {
            controladorCaixa = controlador;
        }
      
        public override Caixa Obterregistro()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Caixa caixa = new Caixa(cor, etiqueta);
            string validacao = caixa.Validar();
            if (validacao == "CAIXA_VALIDA")
            {
                ApresentarMensagem("Caixa Pega com sucesso", TipoMensagem.Sucesso);
                return caixa;
            }

            ApresentarMensagem(validacao, TipoMensagem.Erro);
            Obterregistro();
            return null;            
        }

        public override void ApresentarTabela(List<Caixa> Lista)
        {
            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Etiqueta", "Cor");

            foreach (Caixa caixa in Lista)
            {
                Console.WriteLine(configuracaColunasTabela, caixa.id, caixa.etiqueta, caixa.cor);
            }
        }
    }
}