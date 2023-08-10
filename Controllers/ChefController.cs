using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishesCore.Models;
using System.ComponentModel;
//Added for Include:
using Microsoft.EntityFrameworkCore;

//ADDED for session check
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChefsNDishesCore.Controllers;


//? removed SessionCheck above class controller* 
// [SessionCheck]



public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;
    
    // Add field - adding context into our class // "db" can eb any name
    private MyContext db;

    // update below adding context, etc
    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }



    //  Chefs/gets all chefs Home * ============================================
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> allChefs = db.Chefs.Include(c => c.ChefDishes).ToList();
        return View("Index", allChefs);
    }


    // New Chef  ============================================
    [HttpGet("chefs/new")]
    public IActionResult NewChef()
    {
        return View("NewChef");
    }

    //? All Chefs  ============================================
    // [HttpGet("all/chefs")]
    // public IActionResult AllChefs()
    // {
    //     return View("Index");
    // }


    // CreateChef method ============================================
    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if(!ModelState.IsValid)
        {
            return View("NewChef");
        }
        // newChef.ChefId = (int) HttpContext.Session.GetInt32("UUID");
        // using db table name "Chefs"
        db.Chefs.Add(newChef);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}