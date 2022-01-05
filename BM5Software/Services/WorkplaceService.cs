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
    public class WorkplaceService : IWorkplaceService
    {
        private readonly BM5DbContext _dbContext;
        private readonly IMapper _mapper;

        public WorkplaceService(BM5DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Workplace GetById(int id)
        {
            var workplace = _dbContext
                .Workplace
                .Include(w => w.Address)
                .Include(w => w.Doctors)
                .ThenInclude(d => d.Specialization)
                .FirstOrDefault(d => d.Id == id);

            if (workplace is null) return null;

            return workplace;
        }

        public IEnumerable<Workplace> GetAll()
        {
            var workplaces = _dbContext
                .Workplace
                .Include(w => w.Address)
                .Include(w => w.Doctors)
                .ThenInclude(d => d.Specialization)
                .ToList();

            return workplaces;
        }

        public Workplace Create(WorkplaceDto workplaceDto)
        {
            var workplace = _mapper.Map<Workplace>(workplaceDto);
            _dbContext.Workplace.Add(workplace);
            _dbContext.SaveChanges();

            return workplace;
        }

        public bool Update(WorkplaceDto workplaceDto)
        {
            var workplace = _mapper.Map<Workplace>(workplaceDto);
            _dbContext.Entry(workplace).State = EntityState.Modified;
            _dbContext.Entry(workplace.Address).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var workplace = _dbContext
                  .Workplace
                  .Include(w => w.Doctors)
                  .FirstOrDefault(d => d.Id == id);

            if (workplace is null) return false;

            foreach (var doctor in _dbContext.Doctor.Where(d => d.Workplaces.Contains(workplace)))
            {
                workplace.Doctors.Remove(doctor);
            }
            _dbContext.Workplace.Remove(workplace);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
