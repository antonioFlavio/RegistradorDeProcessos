namespace RegistradorDeProcessos.WebAPI.Model
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Descricao { get; set; }
        public bool PossuiAnexo { get; set; }
        public string Legenda { get; set; }
    }
}
