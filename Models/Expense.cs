namespace EFWithSQLite.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public float Value { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}