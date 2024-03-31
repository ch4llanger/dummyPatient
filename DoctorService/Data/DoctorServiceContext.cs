using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DepartmentAPI.Domain;

namespace DoctorService.Data
{
    public class DoctorServiceContext : DbContext
    {
        public DoctorServiceContext (DbContextOptions<DoctorServiceContext> options)
            : base(options)
        {
        }

        public DbSet<DepartmentAPI.Domain.Doctor> Doctor { get; set; } = default!;
        public DbSet<DepartmentAPI.Domain.Department> Department { get; set; } = default!;
    }
}
