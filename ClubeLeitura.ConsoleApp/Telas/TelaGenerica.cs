using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeLeitura.ConsoleApp.Dominio;
using ClubeLeitura.ConsoleApp.Controladores;


namespace ClubeLeitura.ConsoleApp.Telas
{
    public abstract class TelaGenerica<T> : TelaBase where T : EntidadeBase
    {
        protected Controlador<T> controlador;
        protected string titulo;

        public TelaGenerica(Controlador<T> controlador, string titulo) : base ( "Tela "+titulo)
        {
            this.titulo = titulo;
            this.controlador = controlador;
        }
        public void InserirNovoRegistro()
        {
            ConfigurarTela($"Inserindo um(a) {titulo}...");

            controlador.AdicionarRegistro(Obterregistro());
        }
        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando caixas...");

            List<T> registros = controlador.SelecionarTodosRegistros();

            if (registros.Count == 0)
            {
                ApresentarMensagem("Nenhuma caixa cadastrada!", TipoMensagem.Atencao);
                return false;
            }

            ApresentarTabela(registros);

            return true;
        }

        public void EditarRegistro()
        {
            ConfigurarTela($"Editando um(a) {titulo}...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do Registro que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteRegistroComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhum Registro foi encontrada com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            T registro = Obterregistro();

            controlador.EditarRegistro(id, registro);          
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela($"Excluindo um(a) {titulo}...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do Registro que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool numeroEncontrado = controlador.ExisteRegistroComEsteId(id);
            if (numeroEncontrado == false)
            {
                ApresentarMensagem("Nenhuma Registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }

            bool conseguiuExcluir = controlador.ExcluirRegistro(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Excluído(a) com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public abstract void ApresentarTabela(List<T> lista);
        public abstract T Obterregistro();
    }
}
