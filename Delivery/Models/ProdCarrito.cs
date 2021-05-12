using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Delivery.Models
{
    public class ProdCarrito
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }


        [DisplayName("NOMBRE")]
        public string descripcion { get; set; }


        [DisplayName("PRECIO")]
        public double precio { get; set; }


        [DisplayName("STOCK")]
        public int stock { get; set; }


        [DisplayName("FOTO")]
        public string foto { get; set; }
    }
}