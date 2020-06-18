using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base (options)  
        {}
        
        public DbSet<Employee> Employees { get; set;  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Pablo",
                    Departament = Dept.IT,
                    Email = "pablo@pablo.com"
                }
                );
        }
    }
}