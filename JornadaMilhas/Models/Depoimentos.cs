using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Models
{
    public class Depoimentos
    {
        public Depoimentos()
        {
        }

        public Depoimentos(string nome, string depoimento, string foto)
        {
            Nome = nome;
            Depoimento = depoimento;
            Foto = foto;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public String Nome { get; set; }
        [Required]
        public String Depoimento { get; set; }
        [Required]
        [StringLength(500)]
        public String Foto { get; set; }
    }
}
