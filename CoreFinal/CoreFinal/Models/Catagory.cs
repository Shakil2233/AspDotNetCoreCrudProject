using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFinal.Models
{
    public class Catagory
    {
        [Key ]
        public int ID { get; set; }
        public string Catagory_Name { get; set; }
        public virtual IList<Product> Products { get; set; }

    }
}
