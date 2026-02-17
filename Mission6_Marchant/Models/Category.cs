using System.ComponentModel.DataAnnotations;

namespace Mission6.Models;

public class Category
{
    [Key]
    [Required]
    public int CategoryId { get; set; } // 
    
    [Required]
    public string CategoryName { get; set; } // Note: The DB uses "CategoryName" [cite: 27, 29]
}