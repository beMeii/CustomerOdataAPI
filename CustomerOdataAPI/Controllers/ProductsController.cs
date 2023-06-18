using CustomerOdataAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOdataAPI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EcommerceDbContext db;
        public ProductsController(EcommerceDbContext db)
        {
            this.db = db;
        }

    }
}
