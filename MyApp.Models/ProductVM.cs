using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class ProductVM
    {
        public Product product { get; set; }

        public IEnumerable<Product> products { get; set; } = new List<Product>();
    }
}
