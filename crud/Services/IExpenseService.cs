using crud.Models;

namespace crud.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAllExpenses();
        Expense? GetExpenseById(int id);
        void CreateExpense(Expense expense);
        Expense? UpdateExpense(Expense expense);
        IEnumerable<Expense> DeleteExpense(int id);
        decimal GetTotalExpenses();
    }
}
