using System.ComponentModel.DataAnnotations;
 
namespace eCommerce.Models
{
    public class ProductViewModel : BaseEntity
    {
//------------------------------------------------------------------------------------
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters.")]
        public string ProductName {get; set;}
//------------------------------------------------------------------------------------
        [Required]
        [MinLength(5, ErrorMessage = "Must be at least 5 letters.")]
        public string ImageUrl {get; set;}
//------------------------------------------------------------------------------------
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters.")]
        public string Description {get; set;}
//------------------------------------------------------------------------------------
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "No negative numbers or 0")]
        public int TotalQty {get; set;}
//------------------------------------------------------------------------------------
    }
}