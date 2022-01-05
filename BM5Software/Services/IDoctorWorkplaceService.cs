using BM5Software.DTOs;
using BM5Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BM5Software.Services
{
    public interface IDoctorWorkplaceService
    {
        bool AddToWorkplace(Doctor doctor, List<Workplace> workplaces);
    }
}
