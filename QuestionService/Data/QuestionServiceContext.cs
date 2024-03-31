using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionAPI.Domain;
using Microsoft.EntityFrameworkCore;
using QuestionService.Domain;

namespace QuestionService.Data
{
    public class QuestionServiceContext : DbContext
    {
        public QuestionServiceContext (DbContextOptions<QuestionServiceContext> options)
            : base(options)
        {
        }

        public DbSet<QuestionService.Domain.Answer> Answer { get; set; } = default!;
        public DbSet<QuestionService.Domain.Question> Question { get; set; } = default!;
        public DbSet<Doctor> Doctors { get; set; } = default!;
        public DbSet<Patient> Patients { get; set; } = default!;
    }
}
