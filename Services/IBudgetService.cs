using MyApi.Models;

namespace MyApi.Services
{
	public interface IBudgetService
	{
		Task<Budget> GetBudgetAsync();
	}
}
