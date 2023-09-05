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
    public class FacturasController : Controller
    {
        //Son el estado de registro y el tipo de mensaje que devolvera
        private bool registrado;
        private string mensaje;
        private DataTable datos = new DataTable();
        private DataTable facturas = new DataTable(); 

        //Usado para devolver el tipo de peticion con la vista a la vez
        public ActionResult RegistrarFactura()
        {
            return View();
        }
        public ActionResult listarFacturas()
        {
            return View();
        }
        public ActionResult filtrarFacturas()
        {
            return View();
        }

        public ActionResult AgregarProductos()
        {
            return View();
        }

        public ActionResult EliminarFacturas()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegistrarFactura(Facturas facturas)
        {


            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {
                //se agregan los valores a el proceso sp_RegistrarAlquiler
                SqlCommand cmd = new SqlCommand("sp_RegistrarFacturas", cn);
                cmd.Parameters.AddWithValue("IdCliente", facturas.IdCliente);
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
                return RedirectToAction("listaFacturas", "Facturas");
            }
            else
            {
                ViewData["Mensaje"] = mensaje;
                return View();
            }
        }


        [HttpGet]
        public ActionResult listaFacturas(Facturas factura)
        {


            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {

                cn.Open();
                
                SqlDataAdapter sqlAD = new SqlDataAdapter("SELECT F.*, C.Nombres AS NombreCliente  FROM Facturas F INNER JOIN Clientes C ON F.IdCliente = C.Identificacion", cn);

                sqlAD.Fill(datos);


            }

            return View(datos);
        }


        [HttpPost]
        public ActionResult filtrarFacturas(string datoBuscar)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {

                string fechaInicio = datoBuscar;
                //se agrega un dia de mas para poder dar bien los parametros a la hora de la consulta
                string fechaFin = DateTime.Parse(datoBuscar).AddDays(1).ToString("yyyy-MM-dd");


                string busquedaSQL = string.Format("SELECT * FROM Facturas a WHERE a.Fecha >= '{0}' AND a.Fecha < '{1}'", fechaInicio, fechaFin);
                cn.Open();
                SqlDataAdapter sqlAD = new SqlDataAdapter(busquedaSQL, cn);


                sqlAD.Fill(datos);
            }
            return View(datos);
        }



        [HttpPost]
        public ActionResult EliminarFacturas(int IdFactura)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {
                string borrarproductosSQL = string.Format("DELETE FROM ProductosFactura WHERE IdFactura = {0}", IdFactura);
                string borrarFacturasSQL = string.Format("DELETE FROM Facturas WHERE IdFactura = {0}", IdFactura);

                cn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = borrarproductosSQL;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = borrarFacturasSQL;
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("listaFacturas", "Facturas");
        }


        [HttpPost]
        public ActionResult AgregarProductos(int IdFactura, ProductosFactura productosFactura){
            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {
                //se agregan los valores a el proceso sp_RegistrarAlquiler
                SqlCommand cmd = new SqlCommand("sp_AgregarProductoAFactura", cn);
                cmd.Parameters.AddWithValue("IdFactura", IdFactura);
                cmd.Parameters.AddWithValue("IdProducto", productosFactura.IdProducto);
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
                return RedirectToAction("listaFacturas", "Facturas");
            }
            else
            {
                ViewData["Mensaje"] = mensaje;
                return View();
            }
        }


        [HttpGet]
        public ActionResult VerFacturas(int IdFactura) {

            using (SqlConnection cn = new SqlConnection(ConexionDB.conexion))
            {


                string busquedaSQL = string.Format("SELECT F.IdFactura, C.Nombres AS Cliente, PF.IdProducto FROM Facturas F JOIN Clientes C ON F.IdCliente = C.Identificacion LEFT JOIN ProductosFactura PF ON F.IdFactura = PF.IdFactura WHERE F.IdFactura = {0}", IdFactura);
                cn.Open();
                SqlDataAdapter sqlAD = new SqlDataAdapter(busquedaSQL, cn);


                sqlAD.Fill(facturas);
            }

            return View(facturas);
        }
    }
}