using Microsoft.EntityFrameworkCore;
using OwlreportAPI.Models;

namespace OwlreportAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<TimeReport> TimeReports => Set<TimeReport>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<User> Users => Set<User>();
    }
}
