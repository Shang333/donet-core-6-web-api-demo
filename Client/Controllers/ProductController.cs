using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Client.Models;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>();

        // 查詢並列出所有 product 資料
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(products);
        }

        // 新增資料
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            products.Add(product);
            return CreatedAtAction(nameof(GetAllProducts), products);
        }

        // 編輯資料
        [HttpPut("{id}")]
        public IActionResult EditProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = products.Find(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return NoContent();
        }

        // 刪除資料
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = products.Find(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            products.Remove(existingProduct);
            return NoContent();
        }
    }
}
