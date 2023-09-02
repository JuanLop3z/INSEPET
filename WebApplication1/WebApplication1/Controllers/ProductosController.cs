using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductosController : Controller
    {
        //Son el estado de registro y el tipo de mensaje que devolvera
        private bool registrado;
        private string mensaje;
        DataTable data = new DataTable();

        public ActionResult CrearProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearProducto(Productos producto)
        {

            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {
                //se agregan los valores a el proceso sp_CrearProducto
                SqlCommand cmd = new SqlCommand("sp_CrearProducto", cn);
                cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("Precio", producto.Precio);
                cmd.Parameters.AddWithValue("Stock", producto.Stock);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();


            }

            ViewData["Mensajes"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("listarProductos", "Productos");
            }
            else
            {
                ViewData["Mensaje"] = "El producto ya Existe";
                return View();
            }
        }


        [HttpGet]
        public ActionResult listarProductos()
        {

            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {
                //
                cn.Open();
                SqlDataAdapter sqlAD = new SqlDataAdapter("select * from Productos", cn);

                sqlAD.Fill(data);

            }
            return View(data);
        }



        [HttpPost]
        public ActionResult Buscar(string datoBuscar)
        {

            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {

                string busquedaSQL = string.Format("select * from Productos order by Nombre asc", datoBuscar);
                cn.Open();
                SqlDataAdapter sqlAD = new SqlDataAdapter(busquedaSQL, cn);

                sqlAD.Fill(data);


            }
            return View(data);
        }

        }
    }