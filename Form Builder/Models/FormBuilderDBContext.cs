using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form_Builder.Models
{
    /*
     * Form Builder Database Context
     */
    public class FormBuilderDBContext : DbContext
    {
        public FormBuilderDBContext(DbContextOptions<FormBuilderDBContext> options) : base(options)
        {
        }
        // forms table
        public DbSet<Form> Forms { get; set; }
        // forms' submissions table
        public DbSet<Submission> Submissions { get; set; }
    }
}
