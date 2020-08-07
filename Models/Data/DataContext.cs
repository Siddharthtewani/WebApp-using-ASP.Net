using Microsoft.EntityFrameworkCore;
using FrndshipApp.API.Models;
namespace FrndshipApp.API.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){ }
        public DbSet<Value> Values { get; set; }
    }
}