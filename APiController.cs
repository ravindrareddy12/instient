using DavosOilFinder.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DavosOilFinder.Controllers
{
    [Route("api")]
    public class APIController : Controller
    {
        private readonly DavosContext _db;

        public APIController(DavosContext db)
        {
            _db = db;
        }

        // Question 1: Retrieve all customers
        [HttpGet("customers")]
        public IActionResult GetAllCustomers()
        {
            var allCustomers = _db.Customers.ToList();
            return Json(allCustomers);
        }

        // Question 2: Retrieve total order amount for a specific customer
        [HttpGet("customers/{customerId}/orderamount")]
        public IActionResult GetCustomerOrderAmount(int customerId)
        {
            var customerOrderAmount = _db.Orders
                .Where(o => o.CustomerId == customerId)
                .Sum(o => o.Amount);

            var response = new
            {
                CustomerId = customerId,
                OrderAmount = customerOrderAmount
            };

            return Json(response);
        }

        // Question 3: Retrieve top N customers with highest total order amounts
        [HttpGet("topcustomers/{count}")]
        public IActionResult GetTopCustomersByTotalOrderAmount(int count)
        {
            var topCustomers = _db.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalOrderAmount = g.Sum(o => o.Amount)
                })
                .OrderByDescending(c => c.TotalOrderAmount)
                .Take(count)
                .ToList();

            return Json(topCustomers);
        }

        // Question 4: Retrieve top N customers with highest average order amounts
        [HttpGet("topcustomers/{count}/averageorderamount")]
        public IActionResult GetTopCustomersByAverageOrderAmount(int count)
        {
            var topCustomers = _db.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerName = g.First().Customer.Name,
                    AverageOrderAmount = g.Average(o => o.Amount),
                    Country = g.First().Customer.Country
                })
                .OrderByDescending(c => c.AverageOrderAmount)
                .Take(count)
                .ToList();

            return Json(topCustomers);
        }
    }
}
