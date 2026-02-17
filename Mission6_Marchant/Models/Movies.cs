using System.ComponentModel.DataAnnotations;

namespace Mission6.Models;

public class Movie
{
    [Key]
    [Required]
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Please select a category.")]
    public int CategoryId { get; set; } // Links to Category Table
    public Category? Category { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or newer.")]
    public int Year { get; set; }

    public string? Director { get; set; }
    public string? Rating { get; set; }

    [Required]
    public bool Edited { get; set; }

    public string? LentTo { get; set; }

    [Required]
    public bool CopiedToPlex { get; set; }

    [MaxLength(25, ErrorMessage = "Notes must be 25 characters or less.")]
    public string? Notes { get; set; }
}