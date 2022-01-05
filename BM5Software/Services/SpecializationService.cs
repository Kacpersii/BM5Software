using BM5Software.DAL;
using BM5Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly BM5DbContext _dbContext;

        public SpecializationService(BM5DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Specialization> GetAll()
        {
            var specializations = _dbContext.Specialization.ToList();

            return specializations;
        }
    }
}
