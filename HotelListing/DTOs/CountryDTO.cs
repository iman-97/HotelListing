using HotelListing.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTOs;

public class CountryDTO:CreateCountryDTO
{
    public int Id { get; set; }
    public IList<HotelDTO>? Hotels { get; set; }
}

public class CreateCountryDTO
{
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Country Name Is Too Long")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(maximumLength: 2, ErrorMessage = "Country Short Name Is Too Long")]
    public string ShortName { get; set; } = string.Empty;
}
