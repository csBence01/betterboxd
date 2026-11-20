using Filmnaplo.Services;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Betterboxd_IVTWAU.Services
{
    public class PosterService
    {
        private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };
        private const string SearchUrl = "https://api.themoviedb.org/3/search/movie";
        private const string ImageBase = "https://image.tmdb.org/t/p/w342";
        // Visszaadja a helyi fájl útvonalát, vagy null-t, ha nem sikerült
        public async Task<string?> DownloadPosterAsync(string title, int? year = null)
        {
            try
            {
                var url = $"{SearchUrl}?api_key={TmdbSecrets.ApiKey}&query={Uri.EscapeDataString(title)}";
                if (year is int y)
                    url += $"&year={y}";
                var result = await Http.GetFromJsonAsync<TmdbSearchResponse>(url);
                var posterPath = result?.Results?.FirstOrDefault()?.PosterPath;
                if (string.IsNullOrEmpty(posterPath))
                    return null;
                var bytes = await Http.GetByteArrayAsync(ImageBase + posterPath);
                var dir = Path.Combine(FileSystem.AppDataDirectory, "posters");
                Directory.CreateDirectory(dir);
                var file = Path.Combine(dir, $"{Guid.NewGuid()}.jpg");
                await File.WriteAllBytesAsync(file, bytes);
                return file;
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
            {
                // Offline vagy API-hiba: az app ne omoljon össze
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }
    }
    internal sealed class TmdbSearchResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbMovie>? Results { get; set; }
    }
    internal sealed class TmdbMovie
    {
        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }
    }
}
