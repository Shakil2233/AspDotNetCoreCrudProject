using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CoreFinal.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime orderDate { get; set; }
        public bool StockAvailable { get; set; }
        [ForeignKey("Catagory")]
        public int CID { get; set; }
   
        public string ImagePath{ get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public Catagory Catagory { get; set; }


    }
}
