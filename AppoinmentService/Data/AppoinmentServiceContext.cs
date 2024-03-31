using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppointmentAPI.Domain;

namespace AppoinmentService.Data
{
    public class AppoinmentServiceContext : DbContext
    {
        public AppoinmentServiceContext (DbContextOptions<AppoinmentServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AppointmentAPI.Domain.Appointment> Appointment { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Doctor> Doctors { get; set; } = default!;
        public DbSet<Patient> Patients { get; set; } = default!;
    }
}
