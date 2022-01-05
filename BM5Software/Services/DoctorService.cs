using AutoMapper;
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
    public class DoctorService : IDoctorService
    {
        private readonly BM5DbContext _dbContext;

        public DoctorService(BM5DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Doctor GetById(int id)
        {
            var doctor = _dbContext.Doctor.Include(d => d.Specialization).Include(d => d.Workplaces).FirstOrDefault(d => d.Id == id);

            return doctor;
        }
        
        public IEnumerable<Doctor> GetAll()
        {
            var doctors = _dbContext
                .Doctor
                .Include(d => d.Specialization)
                .Include(d => d.Workplaces)
                .ToList();

            return doctors;
        }

        public IEnumerable<Doctor> GetAll(string specialization)
        {
            var doctors = _dbContext
                .Doctor
                .Include(d => d.Specialization)
                .Include(d => d.Workplaces)
                .Where(d => d.Specialization.Name == specialization)
                .ToList();

            return doctors;
        }
        
        public Doctor Create(Doctor doctor)
        {
            _dbContext.Doctor.Add(doctor);
            _dbContext.SaveChanges();

            return doctor;
        }

        public bool Update(Doctor doctorToUpdate)
        {
            _dbContext.Entry(doctorToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var doctor = _dbContext
                  .Doctor
                  .Include(d => d.Workplaces)
                  .FirstOrDefault(d => d.Id == id);

            if (doctor is null) return false;


            foreach( var workplace in _dbContext.Workplace.Where(w => w.Doctors.Contains(doctor)))
            {
                doctor.Workplaces.Remove(workplace);
            }
            _dbContext.Doctor.Remove(doctor);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
