using SQLite;
using Betterboxd_IVTWAU.Models;

namespace Betterboxd_IVTWAU.Services;

public class MovieDatabase
{
    private SQLiteAsyncConnection? _db;

    private async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_db is not null)
            return _db;

        var path = Path.Combine(FileSystem.AppDataDirectory, "movies.db3");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<Movie>();
        return _db;
    }

    public async Task<List<Movie>> GetMoviesAsync()
    {
        var db = await GetConnectionAsync();
        return await db.Table<Movie>().OrderByDescending(m => m.WatchedOn).ToListAsync();
    }

    public async Task<Movie?> GetMovieAsync(int id)
    {
        var db = await GetConnectionAsync();
        return await db.Table<Movie>().Where(m => m.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveMovieAsync(Movie movie)
    {
        var db = await GetConnectionAsync();
        return movie.Id != 0
            ? await db.UpdateAsync(movie)
            : await db.InsertAsync(movie);
    }

    public async Task<int> DeleteMovieAsync(Movie movie)
    {
        var db = await GetConnectionAsync();
        return await db.DeleteAsync(movie);
    }
}