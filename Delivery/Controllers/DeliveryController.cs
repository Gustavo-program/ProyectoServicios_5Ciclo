using System;
using System.Collections.Generic;
using System.Data;//
using System.Data.SqlClient;//
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delivery.ServiceReference1;
using Delivery.Models;

namespace Delivery.Controllers
{
    public class DeliveryController : Controller
    {
        /*prueba*/
        SqlConnection cn = new SqlConnection("server=.;database=PROYECTO_DELIVERY;uid=sa;pwd=sql");
        /**/

        ServiceDeliveryClient miServicio = new ServiceDeliveryClient();

        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListadoUsuario()
        {
            return View(miServicio.ListadoUsuarios());
        }

        ///
        public ActionResult Sesion()
        {
            return View();
        }
        //este es el original
        //este es el original
        //este es el original
        //este es el original
        //este es el original

        List<UsuarioO> ListSesion()
        {
            List<UsuarioO> login = new List<UsuarioO>();
            SqlCommand cmd = new SqlCommand("SP_LISTAUSUARIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    login.Add(new UsuarioO()
                    {
                        codigo = int.Parse(dr[0].ToString()),
                        nombre = dr[1].ToString(),
                        apellido = dr[2].ToString(),
                        correo = dr[3].ToString(),
                        contraseña = dr[4].ToString(),
                        dni = dr[5].ToString(),
                        celular = dr[6].ToString(),
                        direccion = dr[7].ToString(),
                        distrito = int.Parse(dr[8].ToString()),
                    });
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return login;
        }




        public ActionResult nuevoUsuario()
        {
            ViewBag.distrito = new SelectList(miServicio.ListadoDistritos(), "codigo", "nombre");
            return View(new UsuarioO());
        }

        [HttpPost]
        public ActionResult nuevoUsuario(UsuarioO objC)
        {
            ViewBag.mensaje = miServicio.nuevoUsuario(objC);
            ViewBag.distrito = new SelectList(miServicio.ListadoDistritos(), "codigo", "nombre", objC.distrito);
            return View(objC);
        }

        public ActionResult modificaUsuario(int id)
        {
            UsuarioO objC = miServicio.ListadoUsuariosO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.distrito = new SelectList(miServicio.ListadoDistritos(), "codigo", "nombre", objC.distrito);
            return View(objC);
        }

        [HttpPost]
        public ActionResult modificaUsuario(UsuarioO objC)
        {
            ViewBag.mensaje = miServicio.modificaUsuario(objC);
            ViewBag.distrito = new SelectList(miServicio.ListadoDistritos(), "codigo", "nombre", objC.distrito);
            return View(objC);
        }

        public ActionResult eliminaUsuario(int id)
        {
            UsuarioO objC = miServicio.ListadoUsuariosO().Where(p => p.codigo == id).FirstOrDefault();
            miServicio.eliminaUsuario(objC);
            return RedirectToAction("ListadoUsuario");
        }
        
        public ActionResult detalleUsuario(int id)
        {
            Usuario objC = miServicio.ListadoUsuarios().Where(p => p.codigo == id).FirstOrDefault();
            return View(objC);
        }

        
        //productos
       
        public ActionResult ListadoProductos()
        {
            return View(miServicio.ListadoProductos());
        }

        //metodo crud
        void CRUD(string proceso, List<SqlParameter> p)
        {
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(proceso, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(p.ToArray());
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            cn.Close();
        }


        //nuevo producto
        public ActionResult nuevoProducto()
        {

            ViewBag.categoria = new SelectList(miServicio.ListadoCategorias(), "codigo", "nombre");
            return View(new ProductosO());
        }

        [HttpPost]
        public ActionResult nuevoProducto(ProductosO objP,HttpPostedFileBase f)
        {
            if (f == null)
            {
                return View(objP);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                return View(objP);
            }
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@NOM",SqlDbType=SqlDbType.VarChar,Value=objP.nombre},
                new SqlParameter(){ParameterName="@DES",SqlDbType=SqlDbType.VarChar,Value=objP.descripcion},
                new SqlParameter(){ParameterName="@PRE",SqlDbType=SqlDbType.Money,Value=objP.precio},
                new SqlParameter(){ParameterName="@STK",SqlDbType=SqlDbType.Int,Value=objP.stock},
                new SqlParameter(){ParameterName="@CAT",SqlDbType=SqlDbType.Int,Value=objP.categoria},
                new SqlParameter(){ParameterName="@IMG",SqlDbType=SqlDbType.VarChar,Value="~/fotos_productos/"+Path.GetFileName(f.FileName)}
            };
            CRUD("SP_REGISTRAPRODUCTO", parametros);
            f.SaveAs(Path.Combine(Server.MapPath("~/fotos_productos/"),
                Path.GetFileName(f.FileName)));
            return RedirectToAction("ListadoProductos");
        }
        //detalle de producto
        public ActionResult detalleProducto(int id)
        {
            Productos objP = miServicio.ListadoProductos().Where(p => p.codigo == id).FirstOrDefault();
            return View(objP);
        }
        //actualiza producto
        public ActionResult modificaProducto(int id)
        {
            ProductosO objP = miServicio.ListadoProductosO().Where(p => p.codigo == id).FirstOrDefault();
            ViewBag.categoria = new SelectList(miServicio.ListadoCategorias(), "codigo", "nombre",objP.categoria);
            return View(objP);
        }

        [HttpPost]
        public ActionResult modificaProducto(ProductosO objP)
        {
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDE",SqlDbType=SqlDbType.Int,Value=objP.codigo},
                new SqlParameter(){ParameterName="@NOM",SqlDbType=SqlDbType.VarChar,Value=objP.nombre},
                new SqlParameter(){ParameterName="@DES",SqlDbType=SqlDbType.VarChar,Value=objP.descripcion},
                new SqlParameter(){ParameterName="@PRE",SqlDbType=SqlDbType.Money,Value=objP.precio},
                new SqlParameter(){ParameterName="@STK",SqlDbType=SqlDbType.Int,Value=objP.stock},
                new SqlParameter(){ParameterName="@CAT",SqlDbType=SqlDbType.Int,Value=objP.categoria},
                new SqlParameter(){ParameterName="@IMG",SqlDbType=SqlDbType.VarChar,Value=objP.foto}
            };
            CRUD("SP_ACTUALIZAPRODUCTO", parametros);
            return RedirectToAction("ListadoProductos");
        }
        //ELIMINAR PRODUCTOS
        public ActionResult eliminaProducto(int id)
        {
            ProductosO objP = miServicio.ListadoProductosO().Where(p => p.codigo == id).FirstOrDefault();
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDE",SqlDbType=SqlDbType.Int,Value=objP.codigo}
            };
            CRUD("SP_ELIMINARPRODUCTO", parametros);
            return RedirectToAction("ListadoProductos");
        }

        //carrito de compras 
        List<ProdCarrito> ListProdCarrito()
        {
            List<ProdCarrito> aProductos = new List<ProdCarrito>();
            SqlCommand cmd = new SqlCommand("SP_PRODUCTOCARRITO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    aProductos.Add(new ProdCarrito()
                    {
                        codigo = int.Parse(dr[0].ToString()),
                        descripcion = dr[1].ToString(),
                        precio = double.Parse(dr[2].ToString()),
                        stock = int.Parse(dr[3].ToString()),
                        foto = dr[4].ToString()
                    });
                }

                dr.Close();
                cn.Close();
            }

            catch (Exception ex)
            {
            }
            return aProductos;
        }


        public ActionResult carritoCompras()
        {
            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<Item>();
            }
            return View(ListProdCarrito());
        }

        public ActionResult seleccionaProducto(int id)
        {
            ProdCarrito objP = ListProdCarrito().Where(p => p.codigo == id).FirstOrDefault();
            return View(objP);
        }

        //ENVIAR EL PRODUCTO SELECCIONADO AL CARRITO DE COMPRAS
        public ActionResult agregarProducto(int id, int cant = 0)
        {
            var miProducto = ListProdCarrito().Where(p => p.codigo == id).FirstOrDefault();

            //enviar informacion a la clase item
            Item objI = new Item()
            {
                codigo = miProducto.codigo,
                descripcion = miProducto.descripcion,
                precio = miProducto.precio,
                cantidad = cant,
                foto = miProducto.foto

            };
            var miCarrito = (List<Item>)Session["carrito"];
            miCarrito.Add(objI);
            Session["carrito"] = miCarrito;
            return RedirectToAction("carritoCompras");

        }

        //metodo que calcula el total de la compra
        public ActionResult comprar()
        {
            if (Session["carrito"] == null)
            {
                return RedirectToAction("carritoCompras");
            }

            var miCarrito = (List<Item>)Session["carrito"];
            ViewBag.total = miCarrito.Sum(s => s.subtotal);
            return View(miCarrito);

        }


        public ActionResult eliminaItem(int id = 0)
        {
            if (id == null) return RedirectToAction("carritoCompras");
            var miCarrito = (List<Item>)Session["carrito"];
            var item = miCarrito.Where(i => i.codigo == id).FirstOrDefault();
            miCarrito.Remove(item);

            Session["carrito"] = miCarrito;
            return RedirectToAction("comprar");
        }

        public ActionResult Final()
        {
            List<Item> detalle = (List<Item>)Session["carrito"];
            double mt = 0;
            foreach (Item it in detalle)
            {
                mt += it.subtotal;
            }
            ViewBag.mt = mt;
            return View();
        }
    }
}