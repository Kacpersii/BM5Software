using System.ComponentModel.DataAnnotations;

namespace BM5Software.Models
{
    public class Specialization
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}