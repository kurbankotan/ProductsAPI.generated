using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    //localhost:7201/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private static List<Product>? _products;


        public ProductsController()
        {
            _products = new List<Product>
            {
                new()  { ProductID = 1, ProductName = "IPhone 14", Price = 60000, IsActive = true },
                new()  { ProductID = 2, ProductName = "IPhone 15", Price = 70000, IsActive = true },

                new()  { ProductID = 3, ProductName = "IPhone 16", Price = 80000, IsActive = true },
                new()  { ProductID = 4, ProductName = "IPhone 17", Price = 90000, IsActive = true }
            };
        }


        //Tüm ürünleri getirir
        //localhost:7201/api/products => GET
        [HttpGet]
        public IActionResult GetProducts()
        {
            if(_products == null)
            {
                return NotFound();
            }

            return Ok(_products);
        }




        //Id'ye göre tek ürün getirir
        //localhost:7201/api/products/1 => GET

        [HttpGet("{id}")]                                 //[HttpGet("api/[controller]/{id}")] şeklinde de yazılabilir
        public IActionResult GetProducts(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            var p = _products?.FirstOrDefault(i=>i.ProductID==id);

            if(p== null)
            {
                return NotFound();
            }

            return Ok(p);
 
        }











    }
}
