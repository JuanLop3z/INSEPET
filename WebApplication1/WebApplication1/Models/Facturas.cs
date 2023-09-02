using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Facturas
    {
        public int IdFacturas { get; set; }
        public int IdProductos { get; set; }
        public int IdClientes { get; set; }
        public Boolean Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}