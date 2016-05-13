namespace AccountingSystem.Domain.Entities
{
    public class Money
    {
        public int Count { get; set; }
        public Currency Currency { get; set; }
        public Money(int count, Currency currency)
        {
            Currency = currency;
            Count = count;
        }

        public void AddMoney(int count)
        {
            Count = Count + count;
        }
    }
}
