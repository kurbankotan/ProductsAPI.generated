using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    //localhost:7201/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ProductsContext _context;


        public ProductsController(ProductsContext context)
        {
            _context = context;
        }


        //Tüm ürünleri getirir
        //localhost:7201/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }




        //Id'ye göre tek ürün getirir
        //localhost:7201/api/products/1 => GET

        [HttpGet("{id}")]                                 //[HttpGet("api/[controller]/{id}")] şeklinde de yazılabilir
        public async Task<IActionResult> GetProduct(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            var p = await _context.Products.FirstOrDefaultAsync(i=>i.ProductID==id);


            if(p== null)
            {
                return NotFound();
            }

            return Ok(p);
 
        }




        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductID }, entity); // Ürün eklendikten sonra kendisine ürün ile ilgili geridönüş yapılması için

        }











    }
}
