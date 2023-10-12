using Microsoft.EntityFrameworkCore;
using Pharmacy.Models;

namespace Pharmacy.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {

        }

        public DbSet<Medicine> Medicines => Set<Medicine>();
    }
}
