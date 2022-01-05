using BM5Software.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software.DAL
{
    public class BM5DbContext : DbContext
    {
        private string _connectionString = "server=127.0.0.1;user id=root;password=root;port=3306;database=bm5;";

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Workplace> Workplace { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}
