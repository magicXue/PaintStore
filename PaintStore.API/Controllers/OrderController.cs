using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintStore.DataAccess;
using PaintStore.Models;
using PaintStore.Models.DTOs;

namespace PaintStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private PaintStoreDbContext _dbContext;
        public OrderController(PaintStoreDbContext paintStoreDb)
        {
            _dbContext = paintStoreDb;
        }

        [HttpGet("{id}")]
        public ActionResult GetOrderById(int id)
        {
            //eager loading
            Order? order = _dbContext.Orders.Include(o => o.User)
                                            .Include(o => o.PaintProducts)
                                            .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //Create a new Order 
        //key-value data ------> {} ------> json
        [HttpPost]
        //我们在直接的使用数据库模型 来 传递http post 请求
        public ActionResult CreateOrder([FromBody] OrderCreateDto orderCreateDto) //auto binding from Json(Http body) to C# model
        {
            // if (order.PaintProducts.Count <= 0)
            // {
            //     throw new Exception("Can not create empty Order");
            // }
            Order order = new Order();
            User? user = new User();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (orderCreateDto.IsNewUser)
                    {
                        bool isUserExist = _dbContext.Users.Any(u => u.Email == orderCreateDto.UserEmail);
                        if (isUserExist)
                        {
                            throw new ArgumentException("User email exists, create new user failed");
                        }

                        //create new user based on params
                        user = new User()
                        {
                            Name = orderCreateDto.UserName,
                            Email = orderCreateDto.UserEmail
                        };

                        _dbContext.Users.Add(user);
                        _dbContext.SaveChanges();
                    }

                    else
                    {
                        user = _dbContext.Users.Find(orderCreateDto.UserId);

                        if (user == null)
                        {
                            throw new ArgumentException("User can not be found");
                        }
                    }

                    //order must be associated to a valid user
                    order.UserId = user.Id;

                    List<PaintProduct> paintProducts = _dbContext.PaintProducts.Where(p => orderCreateDto.PaintProductIds.Contains(p.Id)).ToList();

                    order.PaintProducts = paintProducts;

                    _dbContext.Orders.Add(order);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                
                catch (System.Exception)
                {
                    //捕获错误并且处理， 日志， 销毁内存占用对象
                    //revert 回滚所有刚才触发的数据库改动
                    transaction.Rollback();
                }
            }


            return Created($"GetOrderById/{order.Id}", order); //when we return C# model, auto binding to JSON format
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrderById(int id)
        {
            Order order = new Order();

            if (id == order.Id)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
