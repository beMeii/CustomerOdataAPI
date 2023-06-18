using System.Reflection.Emit;

namespace CustomerOdataAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CustomerOdataAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.OData.Deltas;
    using Microsoft.AspNetCore.OData.Formatter;
    using Microsoft.AspNetCore.OData.Query;
    using Microsoft.AspNetCore.OData.Results;
    using Microsoft.AspNetCore.OData.Routing.Controllers;
    using Microsoft.EntityFrameworkCore;

    public class CustomersController : ODataController
    {
        private readonly EcommerceDbContext db;

        public CustomersController(EcommerceDbContext db)
        {
            this.db = db;
        }

        [EnableQuery]
        public ActionResult<IEnumerable<Customer>> Get()
        {
  
            return Ok(db.Customers.AsQueryable());
        }

        [EnableQuery]
        public SingleResult<Customer> Get([FromODataUri] int key)
        {

            return SingleResult.Create(db.Customers.Where((customer) => customer.CustomerId == key));
        }
        [EnableQuery]
        public ActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return Conflict();
            }
            db.Customers.Add(customer);
            db.SaveChanges();

            return Created(customer);
        }
        public ActionResult Put([FromRoute] int key, [FromBody] Customer updatedCustomer)
        {
            var customer = db.Customers.SingleOrDefault(d => d.CustomerId == key);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = updatedCustomer.Name;

            db.SaveChanges();

            return Ok(customer);
        }

        public ActionResult Patch([FromRoute] int key, [FromBody] Delta<Customer> delta)
        {
            var customer = db.Customers.SingleOrDefault(d => d.CustomerId == key);

            if (customer == null)
            {
                return NotFound();
            }

            delta.Patch(customer);

            db.SaveChanges();

            return Ok(customer);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var customer = db.Customers.SingleOrDefault(d => d.CustomerId == key);

            if (customer != null)
            {
                db.Customers.Remove(customer);
            }

            db.SaveChanges();

            return NoContent();
        }
    }
}
