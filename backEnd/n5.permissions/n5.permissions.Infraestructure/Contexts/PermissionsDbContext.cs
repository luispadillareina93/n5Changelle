using Microsoft.EntityFrameworkCore;
using n5.permissions.Domain.Entity;

namespace n5.permissions.Infraestructure.Contexts
{
    public class PermissionsDbContext : DbContext
    {

        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options) : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionsType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionType>().HasData(
                new PermissionType { Id = 1, Descripcion = "Admin" },
                new PermissionType { Id = 2, Descripcion = "Inspector" },
                new PermissionType { Id = 3, Descripcion = "Jefe Area" },
                new PermissionType { Id = 4, Descripcion = "Asesor" }
                );
        }
    }
}
