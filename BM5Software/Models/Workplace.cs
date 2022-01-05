using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BM5Software.Models
{
    public class Workplace
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<Doctor> Doctors { get; set; }
    }
}