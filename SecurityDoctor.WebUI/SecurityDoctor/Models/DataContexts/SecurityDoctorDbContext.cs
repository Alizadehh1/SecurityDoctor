using Microsoft.EntityFrameworkCore;
using SecurityDoctor.Models.Entities;
using SecurityDoctor.WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDoctor.Models.DataContexts
{
    public class SecurityDoctorDbContext : DbContext
    {
        public SecurityDoctorDbContext(DbContextOptions options)
           : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
