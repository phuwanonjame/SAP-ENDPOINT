using System.Text;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        // Hardcoded SAP user for demo
        private const string SapUser = "PROGRAMMER01";
        private const string SapPass = "Itdev@2020";

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET api/budget?$filter=...
        [HttpGet]
        public async Task<IActionResult> GetBudget(
            [FromQuery(Name = "$filter")] string? filter,
            [FromQuery(Name = "$format")] string? format
        )
        {
            Console.WriteLine("=== Incoming Budget Request ===");
            Console.WriteLine($"Query $filter: {filter}");
            Console.WriteLine($"Query $format: {format}");

            // แสดง header ทั้งหมด
            foreach (var header in Request.Headers)
            {
                Console.WriteLine($"Header: {header.Key} = {header.Value}");
            }

            // เช็ค Authorization header
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                Console.WriteLine("Authorization header missing");
                return Unauthorized(new { error = "Missing Authorization header" });
            }

            var authHeader = Request.Headers["Authorization"].ToString();

            if (!authHeader.StartsWith("Basic "))
            {
                Console.WriteLine("Invalid Authorization scheme");
                return Unauthorized(new { error = "Invalid Authorization scheme" });
            }

            var encodedCredentials = authHeader["Basic ".Length..].Trim();
            var decodedBytes = Convert.FromBase64String(encodedCredentials);
            var decodedCredentials = Encoding.UTF8.GetString(decodedBytes).Split(':');

            // log username ที่ส่งมา
            Console.WriteLine($"Decoded Username: {decodedCredentials[0]}");

            if (decodedCredentials.Length != 2 ||
                decodedCredentials[0] != SapUser ||
                decodedCredentials[1] != SapPass)
            {
                Console.WriteLine("Invalid username or password");
                return Unauthorized(new { error = "Invalid username or password" });
            }

            var budget = await _budgetService.GetBudgetAsync();

            var response = new
            {
                d = new
                {
                    results = new[] { budget }
                }
            };

            Console.WriteLine("=== Response Prepared ===");

            if (!string.IsNullOrEmpty(format) && format.ToLower() == "json")
            {
                return new JsonResult(response);
            }

            return Ok(response);
        }
    }
}
