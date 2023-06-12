using CustomerOdataAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;

namespace CustomerOdataAPI.Controllers
{
    public class OrdersController : ODataController
    {
        private static new Random rand = new Random();
        private static List<Order> orders = new List<Order>(
            Enumerable.Range(1, 3).Select(idx => new Order
            {
                Id = idx,
                Amount = rand.Next(1, 9) * 10
                
            }));
        [EnableQuery]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(orders);
        }

        [EnableQuery]
        public ActionResult<Order> Get([FromRoute] int key)
        {
            var item = orders.SingleOrDefault(d => d.Id.Equals(key));

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
