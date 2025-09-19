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
            var budget = await _budgetService.GetBudgetAsync();

            // TODO: parse filter ถ้าต้องการกรองจริง ๆ
            // ตอนนี้จำลองตอบกลับเหมือน SAP OData

            var response = new
            {
                d = new
                {
                    results = new[] { budget }
                }
            };

            if (!string.IsNullOrEmpty(format) && format.ToLower() == "json")
            {
                return new JsonResult(response);
            }

            return Ok(response);
        }
    }
}
