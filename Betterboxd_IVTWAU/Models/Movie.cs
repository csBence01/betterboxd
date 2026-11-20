using SQLite;

namespace Betterboxd_IVTWAU.Models;

public class Movie
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Today.Year;
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; } = 3.0;       // 0.5–5, félcsillagos (Letterboxd-kompatibilis)
    public DateTime WatchedOn { get; set; } = DateTime.Today;
    public string Notes { get; set; } = string.Empty;
    public string? PosterPath { get; set; }          // helyi képfájl útvonal
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}