using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Models.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
    }
}
