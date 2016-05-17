using System;
using System.Collections.Generic;

namespace AccountingSystem.Domain.Entities
{
    public class BankAccount
    {
        private List<Entry> Entries { get; set; }

        private Currency Currency { get; set; }

        private void AddEntry(Money amount, DateTime date)
        {
            Entries.Add(new Entry(amount, date));
        }

        private Money GetBalance(DateRange range)
        {
            var currency = new Currency();
            var resultMoney = new Money(0, currency);
            foreach (var entry in Entries)
            {
                if (range.Includes(entry.Date))
                {
                    resultMoney.AddMoney(entry.GetAmount());
                }
            }
            return resultMoney;
        }
    }
}
