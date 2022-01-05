using System.ComponentModel.DataAnnotations;

namespace BM5Software.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required]
        [MaxLength(20)]
        public string BuildingNumber { get; set; }
        [MaxLength(20)]
        public string ApartmentNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string Province { get; set; }
    }
}