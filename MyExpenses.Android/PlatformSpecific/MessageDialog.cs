using Android.App;
using MyExpenses.Android;
using MyExpenses.Portable.Interfaces;

namespace MyExpenses.PlatformSpecific
{
    public class MessageDialog : IMessageDialog
    {
        public void SendMessage(string message, string title = null)
        {
            if (MyExpensesApplication.CurrentActivity == null)
                return;

            MyExpensesApplication.CurrentActivity.RunOnUiThread(() =>
            {
                var builder = new AlertDialog.Builder(MyExpensesApplication.CurrentActivity);
                builder.SetMessage(message)
                  .SetTitle(title ?? string.Empty)
                  .SetPositiveButton("OK", delegate { });
                var dialog = builder.Create();
                dialog.Show();
            });
        }
    }
}