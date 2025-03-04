using crud.Models;
using crud.Repositories;

namespace crud.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public IEnumerable<Expense> GetAllExpenses()
        {
            return _expenseRepository.GetAllExpenses();
        }
        public Expense? GetExpenseById(int id)
        {
            return _expenseRepository.GetExpenseById(id);
        }
        public void CreateExpense(Expense expense)
        {
            _expenseRepository.AddExpense(expense);
            _expenseRepository.Save();
        }
        public Expense UpdateExpense(Expense expense)
        {
            var existingExpense = _expenseRepository.GetExpenseById(expense.Id);
            if(existingExpense == null)
            {
                throw new ArgumentException("Exception Not Found");
            }
            _expenseRepository.UpdateExpense(expense);
            _expenseRepository.Save();
            return existingExpense;
        }
        public IEnumerable<Expense> DeleteExpense(int id)
        {
            _expenseRepository.DeleteExpense(id);
            _expenseRepository.Save();
            return _expenseRepository.GetAllExpenses();
        }
        public decimal GetTotalExpenses()
        {
            return _expenseRepository.GetAllExpenses().Sum(x => x.Value);
        }
    }
}
