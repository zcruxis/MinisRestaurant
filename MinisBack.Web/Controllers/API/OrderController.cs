using MinisBack.Web.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MinisBack.Data;
using MinisBack.Data.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MinisBack.Web.Controllers.API
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search()
        {
            using (var repository = new MinisBackContext())
            {
                var dateFrom = DateTime.UtcNow.AddHours(-12);
                var dateTo = DateTime.UtcNow;

                var retVal = repository.Orders
                    .Include(x => x.OrderItems)
                    .Include(x => x.OrderItems.Select(y => y.Order))
                    .Where(x => x.DateMade >= dateFrom && x.DateMade <= dateTo)
                    .ToList();

                return Ok(retVal);
            }
        }

        [HttpPost]
        [Route("new")]
        public IHttpActionResult AddOrder(OrderViewModel vm)
        {
            using (var repository = new MinisBackContext())
            {
                var orderDB = new Data.Model.OrderEntity()
                {
                    Price = vm.Price,
                    DateMade = DateTime.UtcNow,
                    DatePaid = null,
                    Completed = false,
                    Paid = false,
                };

                foreach (var orderItemVM in vm.OrderItems)
                {
                    orderDB.OrderItems.Add(new OrderItemEntity()
                    {
                        SandwichId = orderItemVM.SandwichId,
                        Quantity = orderItemVM.Quantiy,
                        SandwichPrice = orderItemVM.SandwichPrice,
                        TotalPrice = orderItemVM.TotalPrice,
                    });
                }

                repository.Orders.Add(orderDB);
                repository.SaveChanges();
                
                return Ok(repository.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderDB.Id));
            } 
        }
        
    }
}
