using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Productos
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
    }
}