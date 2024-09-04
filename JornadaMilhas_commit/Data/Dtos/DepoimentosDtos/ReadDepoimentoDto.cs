using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Data.Dtos.DepoimentosDtos
{
    public class ReadDepoimentoDto
    {
        public string Nome { get; set; }
        public string Depoimento { get; set; }
        public string Foto { get; set; }
        public DateTime HoraConsulta { get; set; } = DateTime.Now;
    }
}
