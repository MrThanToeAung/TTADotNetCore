using Microsoft.EntityFrameworkCore;
using TTADotNetCore.ConsoleApp.Dtos;
using TTADotNetCore.ConsoleApp.Services;

namespace TTADotNetCore.ConsoleApp.EFCoreExamples
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogDto> Blogs { get; set; }

    }
}
