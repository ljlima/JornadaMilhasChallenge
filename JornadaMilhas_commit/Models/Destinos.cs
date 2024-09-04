using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Models;

public class Destinos
{
    public Destinos(string nome, string foto1, string foto2, double preco)
    {
        Nome = nome;
        Foto1 = foto1;
        Foto2 = foto2;
        Preco = preco;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public String Nome { get; set; }
    [Required]
    [StringLength(500)]
    public String Foto1 { get; set; }
    [Required]
    [StringLength(500)]
    public String Foto2 { get; set; }
    [Required]
    [StringLength(160)]
    public String Meta { get; set; }

    public String TextoDescritivo { get; set; } = " ";

    [Required]
    public Double Preco  { get; set; }
}
