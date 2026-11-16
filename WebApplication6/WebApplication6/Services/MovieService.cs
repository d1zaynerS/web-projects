using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace WebApplication6.Services;

public class MovieResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<MovieDto> Results { get; set; } = new();

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
}

public class MovieDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }

    public string FullPosterUrl => string.IsNullOrEmpty(PosterPath)
        ? "/img/covers/cover.jpg"
        : $"https://image.tmdb.org/t/p/w500{PosterPath}";
}

public class GenreResponse
{
    [JsonPropertyName("genres")]
    public List<GenreDto> Genres { get; set; } = new();
}

public class GenreDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class MovieService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public MovieService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["TMDB:ApiKey"] ?? "";
    }

    public async Task<MovieResponse?> GetPopularMoviesAsync(int page = 1)
    {
        return await SendApiRequestAsync<MovieResponse>($"movie/popular?api_key={_apiKey}&language=uk-UA&page={page}");
    }

    public async Task<MovieResponse?> SearchMoviesAsync(string query, int page = 1)
    {
        return await SendApiRequestAsync<MovieResponse>($"search/movie?api_key={_apiKey}&language=uk-UA&query={Uri.EscapeDataString(query)}&page={page}");
    }

    public async Task<MovieDto?> GetMovieDetailsAsync(int movieId)
    {
        return await SendApiRequestAsync<MovieDto>($"movie/{movieId}?api_key={_apiKey}&language=uk-UA");
    }

    public async Task<List<MovieDto>> GetSimilarMoviesAsync(int movieId)
    {
        var response = await SendApiRequestAsync<MovieResponse>($"movie/{movieId}/similar?api_key={_apiKey}&language=uk-UA");
        return response?.Results.Take(5).ToList() ?? new List<MovieDto>();
    }

    public async Task<List<GenreDto>> GetGenresAsync()
    {
        var response = await SendApiRequestAsync<GenreResponse>($"genre/movie/list?api_key={_apiKey}&language=uk-UA");
        return response?.Genres ?? new List<GenreDto>();
    }

    private async Task<T?> SendApiRequestAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[TMDb Error]: Ошибка {response.StatusCode} при запросе {endpoint}");
                return default;
            }
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"[Network Error]: Ошибка сети: {ex.Message}");
            return default;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error]: Непредвиденная ошибка: {ex.Message}");
            return default;
        }
    }
}