using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }

        public string ConfirmarClave { get; set; }
    }
}