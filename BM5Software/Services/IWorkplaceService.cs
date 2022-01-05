using BM5Software.DTOs;
using BM5Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software.Services
{
    public interface IWorkplaceService
    {
        Workplace GetById(int id);
        IEnumerable<Workplace> GetAll();
        Workplace Create(WorkplaceDto workplaceDto);
        bool Delete(int id);
        bool Update(WorkplaceDto workplace);
    }
}
