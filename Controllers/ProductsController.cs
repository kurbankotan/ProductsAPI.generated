﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
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
            var products = await _context.Products
                                                  .Where(i=>i.IsActive == true)
                                                  .Select(p => ProductToDTO(p))
                                                  .ToListAsync();

            return Ok(products);
        }




        //Id'ye göre tek ürün getirir
        //localhost:7201/api/products/1 => GET

        [HttpGet("{id}")]                                 //[HttpGet("api/[controller]/{id}")] şeklinde de yazılabilir
        [Authorize]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            var p = await _context
                                 .Products
                                 .Where(i=>i.ProductID == id)
                                 .Select(p=> ProductToDTO(p))
                                 .FirstOrDefaultAsync();


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


        //localhost:7201/api/products/1 => PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if (id != entity.ProductID)
            {
                return BadRequest(); //400'lü hata gönder. Yanlış kullanma
            }

            var product = await _context.Products.FirstOrDefaultAsync(p=>p.ProductID==id);
            if (product == null)
            {
                return NotFound();            
            }

            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }


            return NoContent(); //200'lü durum. 204 herşey normal. Güncelleme yapıldı demek

        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p=> p.ProductID==id);

            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent(); //200'lü durum. 204 herşey normal. Silme yapıldı demek

        }




        private static ProductDTO ProductToDTO(Product p)
        {
            var entity = new ProductDTO();
            if (p != null)
            {
                entity.ProductID = p.ProductID;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }
            return entity;
        }











    }
}
