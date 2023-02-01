namespace EFWithSQLite.Models
{
    public class Expense
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public float Value { get; set; }

        public string Type { get; set; }
    }
}