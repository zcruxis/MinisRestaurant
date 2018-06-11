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
using System.Collections;

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
                int[] IdsArray = new int[vm.OrderItems.Count];
                var counter = new int();
                foreach (var orderItemVM in vm.OrderItems)
                {
                    orderDB.OrderItems.Add(new OrderItemEntity()
                    {
                        SandwichId = orderItemVM.SandwichId,
                        Quantity = orderItemVM.Quantiy,
                        SandwichPrice = orderItemVM.SandwichPrice,
                        TotalPrice = orderItemVM.TotalPrice,
                    });
                    IdsArray[counter] = orderItemVM.SandwichId;
                }
                
                foreach (var IdsA in IdsArray)
                {
                    using (MinisBackContext context = new MinisBackContext())
                    {
                        /* Queries needed to set the chain of commands.
                        var QIngredients = from SandwichIngredient in context.Ingredients where SandwichEntity.Id == IdsA select context.Ingredients;
                        var QSandwich = from SandwichEntity in repository.SandwichTypes where SandwichEntity.Id == IdsA;
                        var QSandwichType = from SandwichTypeEntity in repository.Sandwichs select SandwichTypeEntity;
                        */
                        //Testing variables
                        var Command = "";
                        var RIngredients = "1,2";
                        var RSalsa = "1";
                        var RCut = "1";
                        var RCompressed = true;

                        Command = "";
                        Command += "Addb()+";
                        Command += "AddIng("+RIngredients+")+";
                        if (RSalsa == "2") Command += "AddS()+";                    
                        Command += "Addb()+";
                        if(RCompressed) Command += "Compress()+";

                        if (RSalsa == "1") { Command += "Cut(" + RCut + ")+"; Command += "AddS()"; }
                        else Command += "Cut(" + RCut + ")";

                        var Machine = new MachineService();
                        /* Queries needed to OrderId the chain of commands.
                         * 
                         * 
                         * QOrderID =  FROM OrdersEntity WHERE   id = (SELECT MAX(id)  Select id)
                         * 
                         */
                        /* Queries needed to OrderItemId the chain of commands.
                         * 
                         * 
                         * QOrderID =  FROM OrdersItemsEntity WHERE   id = (SELECT MAX(id)  Select id)
                         * 
                         */
                        int OrderId = 1;
                        int OrderItemId = 1;
                        Machine.ProcessInbounds(Command, OrderItemId, OrderId, counter);
                    }
            }

                repository.Orders.Add(orderDB);
                repository.SaveChanges();
                return Ok(repository.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderDB.Id));
            } 
        }
        [HttpPost]
        [Route("pay")]
        public IHttpActionResult PayOrder(OrderViewModel vm)
        {
            using (var repository = new MinisBackContext())
            {
                var orderDB = new Data.Model.OrderEntity()
                {
                    DatePaid = DateTime.UtcNow,
                    Paid = true,
                };
                repository.Orders.Add(orderDB);
                repository.SaveChanges();
                return Ok(repository.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderDB.Id));
            }
        }
        [HttpPost]
        [Route("complete")]
        public IHttpActionResult CompleteOrder()
        {
            using (var repository = new MinisBackContext())
            {
                var orderDB = new Data.Model.OrderEntity()
                {
                    Completed = true,
                };
                repository.Orders.Add(orderDB);
                repository.SaveChanges();

                return Ok(repository.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderDB.Id));
            }
        }

    }
}
