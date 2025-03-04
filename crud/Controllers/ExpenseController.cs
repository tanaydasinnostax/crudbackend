using Microsoft.AspNetCore.Mvc;
using crud.Models;
using crud.Services;

namespace crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // Get all expenses
        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetAll()
        {
            return Ok(_expenseService.GetAllExpenses());
        }

        // Get an expense by ID
        [HttpGet("{id}")]
        public ActionResult<Expense> GetById(int id)
        {
            var expense = _expenseService.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound(new { message = "Expense not found" });
            }
            return Ok(expense);
        }

        // Create a new expense
        [HttpPost]
        public ActionResult Create([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest(new { message = "Invalid data" });
            }
            _expenseService.CreateExpense(expense);
            return CreatedAtAction(nameof(GetById), new { id = expense.Id }, expense);
        }

        // Update an existing expense
        [HttpPatch("{id}")]
        public ActionResult Update(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest(new { message = "Mismatched ID" });
            }

            try
            {
                var updatedExpense = _expenseService.UpdateExpense(expense);
                return Ok(updatedExpense);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // Delete an expense
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var remainingExpenses = _expenseService.DeleteExpense(id);
                return Ok(remainingExpenses);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }

        }
    }
}
