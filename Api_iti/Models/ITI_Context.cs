using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api_iti.Models
{
    public class ITI_Context : IdentityDbContext<ApplicationUser>
    {
        public ITI_Context(DbContextOptions<ITI_Context> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employee { get; set; }

    }
}
