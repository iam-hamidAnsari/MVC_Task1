using Microsoft.EntityFrameworkCore;
using MVC_Task1.Models;
using System.Data;

namespace MVC_Task1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<Emp> emps { get; set; }


    }
}
