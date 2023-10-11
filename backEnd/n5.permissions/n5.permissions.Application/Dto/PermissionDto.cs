using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.permissions.Application.Dto
{
    public class PermissionDto
    {
        public int Id { get; set; }

        public string NombreEmpleado { get; set; }

        public string ApellidoEmpleado { get; set; }

        public int TipoPermisoId { get; set; }

        public string TipoPermiso { get; set; }


        public DateTime FechaPermiso { get; set; }


    }
}
