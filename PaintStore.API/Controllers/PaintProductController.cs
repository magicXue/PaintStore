using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.DataAccess;
using PaintStore.Models;

namespace PaintStore.API.Controllers
{
    [Route("api/[controller]")] //    http://localhost:5254/api/PaintProduct
    // [Route("api/PaintProduct")]
    [ApiController]
    public class PaintProductController : ControllerBase
    {
        //http://localhost:5254/api/PaintProduct/GetPaintProduct/1

        private PaintStoreDbContext _dbContext;
        public PaintProductController(PaintStoreDbContext paintStoreDb)
        {
            _dbContext = paintStoreDb;
        }

        [HttpGet("GetPaintProduct/{id}")]
        //ActionResult
        //methods in controller ---> endpoint , action
        //Http response --> 2xx , 4xx , 5xx

        public ActionResult GetPaintProductById(int id)
        {
            //可空类型 ---Nullable , PaintProduct?
            PaintProduct? paintProduct = _dbContext.PaintProducts.FirstOrDefault(p => p.Id == id);
            if (paintProduct == null)
            {
                return NotFound($"PaintProduct with Id: {id} can not be found");
            }

            return Ok(paintProduct);
        }

        //TODO --- Create PaintProduct
        [HttpPost]
        public ActionResult CreatePaintProduct([FromBody] PaintProduct paintProduct)
        {
            _dbContext.PaintProducts.Add(paintProduct);
            _dbContext.SaveChanges();

            return Created($"GetPaintProductById/{paintProduct.Id}", paintProduct);
        }
    }
}
