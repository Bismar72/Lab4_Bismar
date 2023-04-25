using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rp_ef_maria.Models;

public class Game
{
	public uint GameId { get; set; }

	[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Title must start with a capital letter and contain only letters, numbers, and spaces")]
	[MaxLength(64, ErrorMessage = "Title must be 64 characters or less")]
	public string Title { get; set; } = "";

	[Display(Name = "Release Date")]
	[DataType(DataType.Date)]
	public DateTime ReleaseDate { get; set; }

	[MaxLength(32, ErrorMessage = "Genre must be 32 characters or less")]
	public string Genre { get; set; } = "";

	[DataType(DataType.Currency)]
	[Column(TypeName = "decimal(10,2)")]
	[Range(0, 99999999.99, ErrorMessage = "Price must be between 0 and 9999999999.99")]
	public decimal Price { get; set; }


	public virtual List<Rating>? Ratings { get; set; }

}



