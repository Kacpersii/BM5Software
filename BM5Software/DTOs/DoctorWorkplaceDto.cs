using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BM5Software.DTOs
{
    public class DoctorWorkplaceDto
    {
        public int DoctorId { get; set; }
        public List<int> WorkplaceIds { get; set; }
    }
}
