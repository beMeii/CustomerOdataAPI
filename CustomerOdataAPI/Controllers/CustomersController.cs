using System.Reflection.Emit;

namespace CustomerOdataAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CustomerOdataAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.OData.Query;
    using Microsoft.AspNetCore.OData.Routing.Controllers;

    public class CustomersController : ODataController
    {

        private static Random random = new Random();
        private static List<Customer> customers = new List<Customer>(
            Enumerable.Range(1, 3).Select(idx => new Customer
            {
                CustomerId = idx,
                Name = $"Customer {idx}",
                Orders = new List<Order>(
                    Enumerable.Range(1, 2).Select(dx => new Order
                    {
                        CustomerId = (idx - 1) * 2 + dx,
                        CreatedDate = DateTime.Now,
                    }))
            }));

        [EnableQuery]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(customers);
        }

        [EnableQuery]
        public ActionResult<Customer> Get([FromRoute] int key)
        {
            var item = customers.SingleOrDefault(d => d.CustomerId.Equals(key));

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
