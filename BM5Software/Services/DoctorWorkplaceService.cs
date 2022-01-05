using BM5Software.DAL;
using BM5Software.DTOs;
using BM5Software.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software.Services
{
    public class DoctorWorkplaceService : IDoctorWorkplaceService
    {
        private readonly BM5DbContext _dbContext;
        private readonly IDoctorService _doctorService;
        private readonly IWorkplaceService _workplaceService;

        public DoctorWorkplaceService(BM5DbContext dbContext, IDoctorService doctorService, IWorkplaceService workplaceService)
        {
            _dbContext = dbContext;
            _doctorService = doctorService;
            _workplaceService = workplaceService;
        }


        public bool AddToWorkplace(Doctor doctor, List<Workplace> workplaces)
        {
            foreach(var workplace in workplaces)
            {
                workplace.Doctors.Add(doctor);

                _dbContext.Entry(doctor).State = EntityState.Modified;
                _dbContext.Entry(workplace).State = EntityState.Modified;
            }
            
            _dbContext.SaveChanges();

            return true;
        }
    }
}
