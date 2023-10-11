using n5.permissions.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.permissions.Application.Commands
{
    public class RequestPermissionCommand
    {
        public int Id { get; set; }

        [Required]
        public string NombreEmpleado { get; set; }

        [Required]
        public string ApellidoEmpleado { get; set; }

        [Required]
        public int TipoPermisoId { get; set; }

        [Required]
        public DateTime FechaPermiso { get; set; }

    }
}
