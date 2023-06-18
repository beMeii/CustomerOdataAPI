using CustomerOdataAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;

namespace CustomerOdataAPI.Controllers
{
    public class OrdersController : ODataController
    {
        private readonly EcommerceDbContext _db;

        public OrdersController(EcommerceDbContext db)
        {
            this._db = db;
        }

        private static new Random rand = new Random();
        private static List<Order> orders = new List<Order>(
            Enumerable.Range(1, 3).Select(idx => new Order
            {
                OrderId = idx,
                CreatedDate = DateTime.Now,

            }));
        [EnableQuery]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(_db.Orders);
        }

        [EnableQuery]
        public ActionResult<Order> Get([FromRoute] int key)
        {

            return Ok(_db.Orders.SingleOrDefault((order) => order.OrderId == key));
        }
    }
}
