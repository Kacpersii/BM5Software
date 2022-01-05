using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software.Models
{
    public class Doctor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(100)]
        public string Degree { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        public int SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual List<Workplace> Workplaces { get; set; }

    }
}
