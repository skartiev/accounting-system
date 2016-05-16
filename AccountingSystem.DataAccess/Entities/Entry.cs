using System;

namespace AccountingSystem.Domain.Entities
{
    public class Entry
    {
        public Money Amount { get; set; }
        public DateTime Date { get; set; }
        public Entry(Money amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }

        public int GetAmount()
        {
            return Amount.Count;
        }
    }
}
