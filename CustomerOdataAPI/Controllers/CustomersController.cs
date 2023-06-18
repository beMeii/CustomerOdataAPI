using System.Reflection.Emit;

namespace CustomerOdataAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using CustomerOdataAPI.DTOs;
    using CustomerOdataAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.OData.Query;
    using Microsoft.AspNetCore.OData.Routing.Controllers;

    public class CustomersController : ODataController
    {
        private readonly IMapper _mapper;
        private static Random random = new Random();
        private static List<Customer> customers = new List<Customer>(
            Enumerable.Range(1, 3).Select(idx => new Customer
            {
                Id = idx,
                Name = $"Customer {idx}",
                Orders = new List<Order>(
                    Enumerable.Range(1, 2).Select(dx => new Order
                    {
                        Id = (idx - 1) * 2 + dx,
                        Amount = random.Next(1, 9) * 10
                    }))
            }));

        public CustomersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [EnableQuery]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(customers);
        }

        [EnableQuery]
        public ActionResult<Customer> Get([FromRoute] int key)
        {
            var item = customers.SingleOrDefault(d => d.Id.Equals(key));

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            using (EcommerceDbContext dbContext = new EcommerceDbContext())
            {
                await dbContext.Customers.AddAsync(customer);
                await dbContext.SaveChangesAsync();
                return Created(customer);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] int customerId, [FromBody] CustomerDTO customerDto)
        {
            var newCustomer = _mapper.Map<Customer>(customerDto);
            using(EcommerceDbContext dbContext = new EcommerceDbContext())
            {
                var lookedUpCustomer = dbContext.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
                if(lookedUpCustomer != null)
                {
                    dbContext.Entry<Customer>(lookedUpCustomer).CurrentValues.SetValues(newCustomer);
                    await dbContext.SaveChangesAsync();
                    return Updated(newCustomer);
                }
                else
                {
                    return BadRequest("Customer ID not found");
                }
            }
            
        }
    }
}
