using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace ServiceWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceDelivery" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceDelivery.svc o ServiceDelivery.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceDelivery : IServiceDelivery
    {

        SqlConnection cn = new SqlConnection("server=.;database=PROYECTO_DELIVERY;uid=sa;pwd=sql");


        public void eliminaUsuario(UsuarioO objC)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINAUSUARIO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDE", objC.codigo);
                cn.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                cn.Close();
            }

        }

        public List<Categoria> ListadoCategorias()
        {
            List<Categoria> aCategoria = new List<Categoria>();
            SqlCommand cmd = new SqlCommand("SP_LISTACATEGORIA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCategoria.Add(new Categoria()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()

                });
            }
            dr.Close();
            cn.Close();
            return aCategoria;
        }



        /* public List<Categoria> ListadoCategorias()
         {
             List<Categoria> aCategoria = new List<Categoria>();
             SqlCommand cmd = new SqlCommand("SP_LISTACATEGORIA", cn);
             cmd.CommandType = CommandType.StoredProcedure;
             cn.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 aCategoria.Add(new Categoria()
                 {
                     codigo = int.Parse(dr[0].ToString()),
                     nombre = dr[1].ToString()

                 });
             }
             dr.Close();
             cn.Close();
             return aCategoria;
         }*/

        public List<Distrito> ListadoDistritos()
        {
            List<Distrito> aDistrito = new List<Distrito>();
            SqlCommand cmd = new SqlCommand("SP_LISTADISTRITO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aDistrito.Add(new Distrito()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString()

                });
            }
            dr.Close();
            cn.Close();
            return aDistrito;
        }

        public List<Productos> ListadoProductos()
        {
            List<Productos> aProductos = new List<Productos>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPRODUCTO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProductos.Add(new Productos()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    precio = double.Parse(dr[3].ToString()),
                    stock = int.Parse(dr[4].ToString()),//
                    categoria = dr[5].ToString(),
                    foto = dr[6].ToString()
                });
            }
            cn.Close();
            return aProductos;
        }

        public List<ProductosO> ListadoProductosO()
        {
            List<ProductosO> aProductosO = new List<ProductosO>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPRODUCTOORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProductosO.Add(new ProductosO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    descripcion = dr[2].ToString(),
                    precio = double.Parse(dr[3].ToString()),
                    stock = int.Parse(dr[4].ToString()),//
                    categoria = int.Parse(dr[5].ToString()),
                    foto = dr[6].ToString()
                });
            }
            cn.Close();
            return aProductosO;
        }

        /* public List<Productos> ListadoProductos()
         {
             List<Productos> aProductos = new List<Productos>();
             SqlCommand cmd = new SqlCommand("SP_LISTAPRODUCTO", cn);
             cmd.CommandType = CommandType.StoredProcedure;
             cn.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 aProductos.Add(new Productos()
                 {
                     codigo = int.Parse(dr[0].ToString()),
                     nombre = dr[1].ToString(),
                     descripcion = dr[2].ToString(),
                     precio = double.Parse(dr[3].ToString()),
                     stock=int.Parse(dr[4].ToString()),//
                     categoria = dr[5].ToString(),
                     foto = dr[6].ToString()
                 });
             }
             cn.Close();
             return aProductos;
         }

         public List<ProductosO> ListadoProductosO()
         {
             List<ProductosO> aProductosO = new List<ProductosO>();
             SqlCommand cmd = new SqlCommand("SP_LISTAPRODUCTOORIGINAL", cn);
             cmd.CommandType = CommandType.StoredProcedure;
             cn.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             while (dr.Read())
             {
                 aProductosO.Add(new ProductosO()
                 {
                     codigo = int.Parse(dr[0].ToString()),
                     nombre = dr[1].ToString(),
                     descripcion = dr[2].ToString(),
                     precio = double.Parse(dr[3].ToString()),
                     stock = int.Parse(dr[4].ToString()),//
                     categoria = int.Parse(dr[5].ToString()),
                     foto = dr[6].ToString()
                 });
             }
             cn.Close();
             return aProductosO;
         }
         */
        public List<Usuario> ListadoUsuarios()
        {
            List<Usuario> aUsuario = new List<Usuario>();
            SqlCommand cmd = new SqlCommand("SP_LISTAUSUARIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aUsuario.Add(new Usuario()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    apellido = dr[2].ToString(),
                    correo = dr[3].ToString(),
                    contraseña=dr[4].ToString(),
                    dni = dr[5].ToString(),
                    celular = dr[6].ToString(),
                    direccion = dr[7].ToString(),
                    distrito = dr[8].ToString()

                });
            }
            dr.Close();
            cn.Close();
            return aUsuario;
        }

        public List<UsuarioO> ListadoUsuariosO()
        {
            List<UsuarioO> aUsuarioO = new List<UsuarioO>();
            SqlCommand cmd = new SqlCommand("SP_LISTAUSUARIOORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aUsuarioO.Add(new UsuarioO()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                    apellido = dr[2].ToString(),
                    correo = dr[3].ToString(),
                    contraseña=dr[4].ToString(),
                    dni = dr[5].ToString(),
                    celular = dr[6].ToString(),
                    direccion = dr[7].ToString(),
                    distrito = int.Parse(dr[8].ToString())

                });
            }
            dr.Close();
            cn.Close();
            return aUsuarioO;

        }

        //////

        public string Logueo(Sesion objS)
        {
            throw new NotImplementedException();
        }

        public string modificaUsuario(UsuarioO objC)
        {
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAUSUARIO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDE", objC.codigo);
                cmd.Parameters.AddWithValue("@NOM", objC.nombre);
                cmd.Parameters.AddWithValue("@APE", objC.apellido);
                cmd.Parameters.AddWithValue("@EMA", objC.correo);
                cmd.Parameters.AddWithValue("@CLA", objC.contraseña);
                cmd.Parameters.AddWithValue("@DNI", objC.dni);
                cmd.Parameters.AddWithValue("@CEL", objC.celular);
                cmd.Parameters.AddWithValue("@DIR", objC.direccion);
                cmd.Parameters.AddWithValue("@DIS", objC.distrito);
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Usuario Actualizado correctamente!";
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error al actualizar usuario" + ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }


        public string nuevoUsuario(UsuarioO objC)
        {
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_REGISTRAUSUARIO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NOM", objC.nombre);
                cmd.Parameters.AddWithValue("@APE", objC.apellido);
                cmd.Parameters.AddWithValue("@EMA", objC.correo);
                cmd.Parameters.AddWithValue("@CLA", objC.contraseña);
                cmd.Parameters.AddWithValue("@DNI", objC.dni);
                cmd.Parameters.AddWithValue("@CEL", objC.celular);
                cmd.Parameters.AddWithValue("@DIR", objC.direccion);
                cmd.Parameters.AddWithValue("@DIS", objC.distrito);
                cn.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Usuario registrado correctamente!";
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrio un error al registrar usuario" + ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }
    }
}
