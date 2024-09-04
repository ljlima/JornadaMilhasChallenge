using System.ComponentModel.DataAnnotations;

namespace JornadaMilhas.Models
{
    public class Depoimentos
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public String Nome { get; set; }
        [Required]
        public String Depoimento { get; set; }
        [Required]
        [MaxLength(500)]
        public String Foto { get; set; }
    }
}
