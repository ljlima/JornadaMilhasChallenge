using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Data.Dtos.DestinosDtos;

public class ReadDestinosDto
{
    public string Nome { get; set; }
    [Required]
    public string Foto1 { get; set; }
    [Required]
    public string Foto2 { get; set; }
    [Required]
    public string Meta { get; set; }
    public string TextoDescritivo { get; set; }

    public DateTime HoraConsulta { get; set; } = DateTime.Now;
}
