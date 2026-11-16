using Microsoft.AspNetCore.Mvc;
using WebApplication6.Services;

namespace WebApplication6.Controllers;

public class HomeController : Controller
{
    private readonly MovieService _movieService;

    public HomeController(MovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {

        var popularMovies = await _movieService.GetPopularMoviesAsync();


        return View(popularMovies?.Results);
    }
}