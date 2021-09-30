using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form_Builder.Models
{
    public class FormBuilderDBContext : DbContext
    {
        public FormBuilderDBContext(DbContextOptions<FormBuilderDBContext> options) : base(options)
        {
        }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Submission> Submissions { get; set; }
    }
}
