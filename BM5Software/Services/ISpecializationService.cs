using BM5Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BM5Software.Services
{
    public interface ISpecializationService
    {
        IEnumerable<Specialization> GetAll();
    }
}
