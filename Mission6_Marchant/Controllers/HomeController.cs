using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6.Models;
using Microsoft.EntityFrameworkCore;

namespace Mission6.Controllers;

public class HomeController : Controller
    
{
    private MoviesContext _context;
    
    public HomeController(MoviesContext temp) //Constructor
    {
        _context = temp;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    [HttpGet]
    public IActionResult MovieForm()
    {
        ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
    
        // Explicitly pass a new Movie object so MovieId defaults to 0 instead of null
        return View(new Movie()); 
    }
    [HttpPost]
    public IActionResult MovieForm(Movie response)
    {
        if (ModelState.IsValid)
        {
            _context.Movies.Add(response);
            _context.SaveChanges();
            return View("Confirmation", response);
        }
        else // If validation fails (e.g., Year < 1888), reload the form with the categories
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(response);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpGet]
    public IActionResult ViewMovies()
    {
        // Replace "_context" with whatever your variable name is for your Database Context
        var movies = _context.Movies
            .Include(x => x.Category) // This "joins" the Category table
            .OrderBy(x => x.Title)    // Optional: Keeps the list alphabetical
            .ToList();

        return View(movies);
    }
    // GET: Edit Movie
    [HttpGet]
    public IActionResult Edit(int id) // 'id' comes from the asp-route-id in your table
    {
        // Find the single movie that matches the ID
        var movie = _context.Movies.Single(x => x.MovieId == id);

        // We still need the categories for the dropdown menu
        ViewBag.Categories = _context.Categories
            .OrderBy(x => x.CategoryName)
            .ToList();

        // Pass the movie object to the View so the fields pre-fill
        return View("MovieForm", movie); 
    }

// POST: Edit Movie
    [HttpPost]
    public IActionResult Edit(Movie updatedInfo)
    {
        if (ModelState.IsValid)
        {
            // Update the database with the changes
            _context.Update(updatedInfo);
            _context.SaveChanges();

            // Send them back to the list to see their changes
            return RedirectToAction("ViewMovies");
        }
        else
        {
            // If validation fails, reload categories and stay on the form
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View("MovieForm", updatedInfo);
        }
    }
    // GET: Show Delete Confirmation
    [HttpGet]
    public IActionResult Delete(int id)
    {
        // Fetch the movie record so we can show the user what they are deleting
        var record = _context.Movies
            .Include(x => x.Category)
            .Single(x => x.MovieId == id);

        return View(record);
    }

// POST: Actually delete the record
    [HttpPost]
    public IActionResult Delete(Movie movie)
    {
        // Tell the context to remove this specific record
        _context.Movies.Remove(movie);
        _context.SaveChanges();

        // Redirect back to the collection list
        return RedirectToAction("ViewMovies");
    }
}
