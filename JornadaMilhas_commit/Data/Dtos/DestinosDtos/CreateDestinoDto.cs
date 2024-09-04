using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Data.Dtos.DestinosDtos;

public class CreateDestinoDto
{
    public CreateDestinoDto(string nome, string foto1, string foto2, double preco)
    {
        Nome = nome;
        Foto1 = foto1;
        Foto2 = foto2;
        Preco = preco;
    }

    [Required]

    public String Nome { get; set; }
    [Required]

    public String Foto1 { get; set; }
    [Required]

    public String Foto2 { get; set; }
    [Required]

    public String Meta { get; set; }

    public String TextoDescritivo { get; set; }

    [Required]
    public Double Preco { get; set; }
}
