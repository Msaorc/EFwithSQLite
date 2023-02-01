using System.ComponentModel.DataAnnotations;
namespace EFWithSQLite.ViewModels
{
    public class CreateExpenseViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage= "The Value field is required.")]
        public float Value { get; set; }

        [Required]
        public string Type { get; set; }
    }
}