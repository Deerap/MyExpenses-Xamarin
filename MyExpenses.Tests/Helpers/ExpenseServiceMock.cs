using System.Collections;
using MyExpenses.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExpenses.Portable.Models;

namespace MyExpenses.Tests.Helpers
{
    public class ExpenseServiceMock : IExpenseService
    {
        public List<Expense> Expenses = new List<Expense>();
        public Task<Portable.Models.Expense> GetExpense(int id)
        {
            return Task.Run(() => Expenses.FirstOrDefault(e => e.Id == id));
        }

        public Task<IEnumerable<Portable.Models.Expense>> GetExpenses()
        {
            return Task.FromResult<IEnumerable<Expense>>(Expenses);
        }

        public async Task<Portable.Models.Expense> SaveExpense(Portable.Models.Expense expense)
        {

            var ex = await GetExpense(expense.Id);
            if (ex == null)
            {
                expense.Id = Expenses.Count;
                Expenses.Add(expense);
            }
            else
            {
                Expenses.Remove(ex);
                Expenses.Add(expense);
            }
            return expense;
        }

        public async Task<int> DeleteExpense(int id)
        {
            var ex = await GetExpense(id);
            if (ex != null)
            {
                Expenses.Remove(ex);
            }

            return 0;
        }


        public Task<Alert> GetExpenseAlert()
        {
            throw new NotImplementedException();
        }


        public Task<bool> SyncExpenses()
        {
            throw new NotImplementedException();
        }
    }
}
