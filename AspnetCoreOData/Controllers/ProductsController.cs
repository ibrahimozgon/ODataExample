using System;
using System.Linq;
using AspnetCoreOData.Contexts;
using AspnetCoreOData.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;
        Random rnd = new Random();
        public ProductsController(MyDbContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Product>> Get()
        {
            return _context.Products;
        }

        [HttpPost]
        public void Post()
        {
            foreach (var category in _context.Categories)
            {
                _context.Categories.Remove(category);
            }

            foreach (var product in _context.Products)
            {
                _context.Products.Remove(product);
            }

            _context.SaveChanges();

            var cat = new Category
            {
                Name = "Category " + Guid.NewGuid()
            };
            _context.Categories.Add(cat);

            for (var i = 0; i < 10; i++)
            {
                _context.Products.Add(new Product
                {
                    Amount = (rnd.Next(1000) + 100),
                    Name = "Product " + Guid.NewGuid(),
                    Category = cat
                });
            }
            _context.SaveChanges();
        }
    }
}
