using System.ComponentModel.DataAnnotations;
namespace rp_ef_maria.Models;

public class Rating
{
	public uint RatingId { get; set; }

	[Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
	public uint StarRating { get; set; } = 0;

	[MaxLength(100, ErrorMessage = "your comment is too long, please use 100 characters or less")]
	[MinLength(4)]
	public string Comment { get; set; } = "";

	[Required]
	public virtual Game Game { get; set; } = default!;
}