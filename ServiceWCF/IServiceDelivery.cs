using System;
using System.Collections.Generic;
using System.Data.SqlClient;//
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;//

namespace ServiceWCF
{
   
    [ServiceContract]
    public interface IServiceDelivery
    {
        //usuarios
        [OperationContract]
        List<Distrito> ListadoDistritos();


        [OperationContract]
        List<Usuario> ListadoUsuarios();


        [OperationContract]
        List<UsuarioO> ListadoUsuariosO();


        [OperationContract]
        string Logueo(Sesion objS);


        [OperationContract]
        string nuevoUsuario(UsuarioO objC);


        [OperationContract]
        string modificaUsuario(UsuarioO objC);

        [OperationContract]
        void eliminaUsuario(UsuarioO objC);

        //productos


        [OperationContract]
        List<Productos> ListadoProductos();

        [OperationContract]
        List<Categoria> ListadoCategorias();

        [OperationContract]
        List<ProductosO> ListadoProductosO();


    }
    //USUARIOS
    [DataContract]
    public class Usuario
    {
        [DataMember] public int codigo { get; set; }
        [DataMember] public String nombre { get; set; }
        [DataMember] public String apellido { get; set; }
        [DataMember] public String correo { get; set; }
        [DataMember] public String contraseña { get; set; }
        [DataMember] public String dni { get; set; }
        [DataMember] public String celular { get; set; }
        [DataMember] public String direccion { get; set; }
        [DataMember] public String distrito { get; set; }
    }

    [DataContract]
    public class Distrito
    {
        [DataMember] public int codigo { get; set; }
        [DataMember] public String nombre { get; set; }
    }
    //////
    [DataContract]
    public class Sesion
    {
        [DataMember] public String usuario { get; set; }
        [DataMember] public String contraseña { get; set; }
    }
    //////

    [DataContract]
    public class UsuarioO
    {
        [DataMember] public int codigo { get; set; }
        [DataMember] public String nombre { get; set; }
        [DataMember] public String apellido { get; set; }
        [DataMember] public String correo { get; set; }
        [DataMember] public String contraseña { get; set; }
        [DataMember] public String dni { get; set; }
        [DataMember] public String celular { get; set; }
        [DataMember] public String direccion { get; set; }
        [DataMember] public int distrito { get; set; }
    }


    //PRODUCTOS
    [DataContract]
    public class Productos
    {
        [DataMember] public int codigo { get; set; }
        [DataMember] public String nombre { get; set; }
        [DataMember] public String descripcion { get; set; }
        [DataMember] public double precio { get; set; }
        [DataMember] public int stock { get; set; }//
        [DataMember] public String categoria { get; set; }
        [DataMember] public String foto { get; set; }

    }

    [DataContract]
    public class ProductosO
    {
        [DataMember] public int codigo { get; set; }
        [DataMember] public String nombre { get; set; }
        [DataMember] public String descripcion { get; set; }
        [DataMember] public double precio { get; set; }
        [DataMember] public int stock { get; set; }//
        [DataMember] public int categoria { get; set; }
        [DataMember] public String foto { get; set; }

    }

    [DataContract]
    public class Categoria
    {
        
        [DataMember] public int codigo { get; set; }
        [DataMember] public String nombre { get; set; }

    }

    

}
