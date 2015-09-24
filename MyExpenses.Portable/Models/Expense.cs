
using System;
using MyExpenses.Portable.BusinessLayer.Contracts;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace MyExpenses.Portable.Models
{
  public class Expense : BusinessEntityBase
  {
    public Expense()
    {
      Name = string.Empty;
      Notes = string.Empty;
      Due = DateTime.Now;
      Total = "0.00";
      Category = string.Empty;
      Billable = true;
      IsVisible = true;
      IsDirty = true;
    }

    

    [JsonProperty("Id")]
    public string AzureId { get; set; }

    [JsonProperty(PropertyName = "userId")]
    public string UserId { get; set; }

    public bool IsDirty { get; set; }

    public bool IsVisible { get; set; }

    public string Name { get; set; }
    public string Notes { get; set; }
    public DateTime Due { get; set; }

    public string Total { get; set; }

    public string Category { get; set; }
    public bool Billable { get; set; }

    [Ignore]
    public string DueDateLongDisplay
    {
      get { return Due.ToLocalTime().ToString("D"); }
    }


    [Ignore]
    public string TotalDisplay
    {
      get { return "$" + Total; }
    }

    [Ignore]
    public string DueDateShortDisplay
    {
      get { return Due.ToLocalTime().ToString("d"); }
    }

    public Expense(Expense expense)
    {
      SyncProperties(expense);
    }

    public void SyncProperties(Expense expense)
    {
      this.AzureId = expense.AzureId;
      this.Billable = expense.Billable;
      this.Category = expense.Category;
      this.Due = expense.Due;
      this.IsVisible = expense.IsVisible;
      this.Name = expense.Name;
      this.Notes = expense.Notes;
      this.Total = expense.Total;
      this.UserId = expense.UserId;
    }
  }
}
