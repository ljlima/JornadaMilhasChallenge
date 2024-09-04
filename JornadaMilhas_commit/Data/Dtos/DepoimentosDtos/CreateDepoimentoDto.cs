using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Data.Dtos.DepoimentosDtos;

public class CreateDepoimentoDto
{
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Depoimento { get; set; }
    [Required]
    public string Foto { get; set; }
}
