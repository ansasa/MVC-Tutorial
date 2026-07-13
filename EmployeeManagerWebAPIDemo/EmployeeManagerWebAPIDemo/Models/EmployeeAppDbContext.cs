using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerWebAPIDemo.Models
{
    public class EmployeeAppDbContext : DbContext
    {
        public EmployeeAppDbContext(DbContextOptions<EmployeeAppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }

   
}
