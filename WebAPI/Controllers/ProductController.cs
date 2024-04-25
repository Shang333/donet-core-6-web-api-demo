using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                return await _dataContext.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        [HttpPost, ActionName("Add")]
        public async Task<IEnumerable<Product>> Post(Product product)
        {
            try
            {
                if (product == null)
                {
                    return Enumerable.Empty<Product>();
                }
                await _dataContext.Products.AddAsync(product);
                await _dataContext.SaveChangesAsync();
                return await _dataContext.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }        
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) 
        {
            try
            {
                if(id == 0)
                {
                    return NotFound();
                }
                var product = await _dataContext.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }          
        }
        [HttpPut("{id}"), ActionName("Edit")]
        public async Task<ActionResult> Put(int id, Product product)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                if (product ==  null)
                {
                    return BadRequest();
                }
                _dataContext.Entry(product).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var product = await _dataContext.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception();
            };

        }
    }
}
