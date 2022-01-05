using BM5Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software.Services
{
    public interface IDoctorService
    {
        Doctor GetById(int id);
        IEnumerable<Doctor> GetAll();
        IEnumerable<Doctor> GetAll(string specialization);
        Doctor Create(Doctor doctor);
        bool Delete(int id);
        bool Update(Doctor doctor);
    }
}
