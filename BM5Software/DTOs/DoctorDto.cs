using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software.DTOs
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Degree { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}
