using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Data.Dtos.DestinosDtos;

public class UpdateDestinosDto
{
    public UpdateDestinosDto(string nome, string foto, double preco)
    {
        Nome = nome;
        Foto = foto;
        Preco = preco;
    }

    [Required]
    public String Nome { get; set; }
    [Required]
    public String Foto { get; set; }
    [Required]
    public Double Preco { get; set; }
}
