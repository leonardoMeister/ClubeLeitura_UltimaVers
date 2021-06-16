using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaAmigo : TelaGenerica<Amigo>, ICadastravel
    {
        private readonly ControladorAmigo controladorAmigo;
        public TelaAmigo(ControladorAmigo controlador):base(controlador , "Amigo")
        {
            controladorAmigo = controlador;
        }

        public override void ApresentarTabela(List<Amigo> lista)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Local");

            foreach (Amigo amigo in lista)
            {
                Console.WriteLine(configuracaoColunasTabela, amigo.id, amigo.nome, amigo.deOndeEh);
            }
        }

        public override Amigo Obterregistro()
        {
            Console.Write("Digite o nome do amiguinho: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amiguinho: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite da onde é o amiguinho: ");
            string deOndeEh = Console.ReadLine();
            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone, deOndeEh);
            string validacao = amigo.Validar();

            if (validacao == "AMIGO_VALIDO")
            {
                ApresentarMensagem("Amigo Pego com sucesso", TipoMensagem.Sucesso);
                return amigo;
            }

            ApresentarMensagem(validacao, TipoMensagem.Erro);
            Obterregistro();
            return null;

        }
    }
}
