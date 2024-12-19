using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class CategoryVM
    {
        public Category category { get; set; }

        public IEnumerable<Category> categories { get; set; } = new List<Category>();
    }
}
