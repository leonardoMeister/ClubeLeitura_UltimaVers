namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Caixa : EntidadeBase
    {
        public string cor;

        public string etiqueta;
        private static int idClassEmp = 0;

        public int GerarId()
        {
            int ids = idClassEmp;
            idClassEmp++;
            return ids;
        }
        public Caixa(int id)
        {
            this.id = id;
        }
        public Caixa()
        {

        }

        public Caixa(string cor, string etiqueta)
        {
            this.id = GerarId();
            this.cor = cor;
            this.etiqueta = etiqueta;
        }

        public string Validar()
        {
            return "CAIXA_VALIDA";
        }
    }
}
