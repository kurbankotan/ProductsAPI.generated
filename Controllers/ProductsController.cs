using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Controllers
{
    //localhost:7201/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private static readonly string[] Products =
        {
            "IPhone 14", "IPhone 15","IPhone 16"
        };

        //localhost:7201/api/products => GET
        [HttpGet]
        public string[] GetProducts()
        {
            return Products;
        }


        //localhost:7201/api/products/1 => GET

        [HttpGet("{id}")]                                 //[HttpGet("api/[controller]/{id}")] şeklinde de yazılabilir
        public string GetProducts(int id)
        {
            return Products[id];
        }
    }
}
