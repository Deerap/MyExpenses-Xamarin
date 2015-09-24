using SQLite.Net.Attributes;

namespace MyExpenses.Portable.BusinessLayer.Contracts
{

    public abstract class BusinessEntityBase : Interfaces.IBusinessEntity
    {
        public BusinessEntityBase()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}

