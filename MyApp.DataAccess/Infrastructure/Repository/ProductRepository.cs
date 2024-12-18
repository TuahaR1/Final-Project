using MyApp.DataAccess.Infrastructure.IRepository;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Infrastructure.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public void update(Product product)
        {
            var ProductDB = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (ProductDB != null) {
                ProductDB.Name = product.Name;
                ProductDB.Discription = product.Discription;
                ProductDB.Price = product.Price;
                if (product.ImageUrl != null)
                {
                    ProductDB.ImageUrl = product.ImageUrl;
                }
            }
        }
    }   
}
