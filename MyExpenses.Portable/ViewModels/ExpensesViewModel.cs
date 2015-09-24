
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using MyExpenses.Portable.Helpers;
using MyExpenses.Portable.Interfaces;
using MyExpenses.Portable.Models;
using MyExpenses.Portable.Services;

namespace MyExpenses.Portable.ViewModels
{
    public class ExpensesViewModel : ViewModelBase
    {
        private IExpenseService expenseService;
        private IMessageDialog messageDialog;

        public ExpensesViewModel()
        {
            expenseService = ServiceContainer.Resolve<IExpenseService>();
            messageDialog = ServiceContainer.Resolve<IMessageDialog>();
            NeedsUpdate = true;
        }

        /// <summary>
        /// Gets or sets if an update is needed
        /// </summary>
        public bool NeedsUpdate { get; set; }

        /// <summary>
        /// Gets or sets if an update is needed
        /// </summary>
        public bool IsSynced { get; set; }

        /// <summary>
        /// Gets or sets if we have loaded alert
        /// </summary>
        public bool LoadedAlert { get; set; }



        private ObservableCollection<Expense> expenses = new ObservableCollection<Expense>();

        public ObservableCollection<Expense> Expenses
        {
            get { return expenses; }
            set { expenses = value; OnPropertyChanged("Expenses"); }
        }


        private async Task UpdateExpenses()
        {
            Expenses.Clear();
            NeedsUpdate = false;
            try
            {
                var exps = await expenseService.GetExpenses();

                foreach (var expense in exps)
                {

                    Expenses.Add(expense);
                }

            }
            catch (Exception exception)
            {
                Debug.WriteLine("Unable to query and gather expenses");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private RelayCommand loadExpensesCommand;

        public ICommand LoadExpensesCommand
        {
            get { return loadExpensesCommand ?? (loadExpensesCommand = new RelayCommand(async () => await ExecuteLoadExpensesCommand())); }
        }

        public async Task ExecuteLoadExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await UpdateExpenses();
        }

        private RelayCommand<Expense> deleteExpensesCommand;

        public ICommand DeleteExpenseCommand
        {
            get { return deleteExpensesCommand ?? (deleteExpensesCommand = new RelayCommand<Expense>(async (item) => await ExecuteDeleteExpenseCommand(item))); }
        }

        public async Task ExecuteDeleteExpenseCommand(Expense exp)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {

                await expenseService.DeleteExpense(exp.Id);
                Expenses.Remove(Expenses.FirstOrDefault(ex => ex.Id == exp.Id));


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to delete expenses");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private RelayCommand syncExpensesCommand;

        public ICommand SyncExpensesCommand
        {
            get { return syncExpensesCommand ?? (syncExpensesCommand = new RelayCommand(async () => await ExecuteSyncExpensesCommand())); }
        }

        public async Task ExecuteSyncExpensesCommand()
        {
            if (IsBusy)
                return;

            IsSynced = false;

            IsBusy = true;

            try
            {
                //If we want to test out an alert comment this back in
                //if (!LoadedAlert)
                //  await ExecuteLoadAlert();

                await expenseService.SyncExpenses();
            }
            catch (Exception ex)
            {
                //log exception
            }
            await UpdateExpenses();
            IsBusy = false;
            IsSynced = true;

        }


        /// <summary>
        /// Gets the current expense alert from the server
        /// </summary>
        /// <returns>Alert from server</returns>
        public async Task<Alert> ExecuteLoadAlert()
        {

            LoadedAlert = true;
            try
            {
                //var client = new HttpClient();
                //client.Timeout = new TimeSpan(0,0,0,5);

                //var response = await client.GetStringAsync("http://someexample.com/expense");
                //var alert = await ExpenseService.DeserializeObjectAsync<Alert>(response);

                var message = DateTime.Now.ToString("MMM") + "'s expenses are processing.";
                var dtTimeStamp = DateTime.Now;

                var alert = new Alert();
                alert.AlertDate = dtTimeStamp;
                alert.Details = message;

                messageDialog.SendMessage(message, dtTimeStamp.ToString("dd/MM/yyyy"));
                return alert;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Unable to query and gather expenses");
            }
            finally
            {
            }

            return null;
        }
    }
}
