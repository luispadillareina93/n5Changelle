using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace n5.permissions.Domain.Entity
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string NombreEmpleado { get; set; }

        [Required]
        public string ApellidoEmpleado { get; set; }

        [Required]
        public int TipoPermisoId { get; set; }
        public virtual PermissionType TipoPermiso { get; set; }

        [Required]
        public DateTime FechaPermiso { get; set; }

       
    }
}
