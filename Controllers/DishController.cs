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



public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    
    // Add field - adding context into our class // "db" can eb any name
    private MyContext db;

    // update below adding context, etc
    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }



    //  Dishes/gets all dishes Home * ============================================
    [HttpGet("/dishes")]
    public IActionResult AllDishes()
    {
        List<Dish> allDishes = db.Dishes.Include(p => p.Creator).OrderByDescending(d => d.CreatedAt).ToList();
        return View("AllDishes", allDishes);
    }


    // New Dish  ============================================
    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        ViewBag.allChefs = db.Chefs.ToList();
        return View("NewDish");
    }

    //? All Dishes  ============================================
    // [HttpGet("all/dishes")]
    // public IActionResult AllDishes()
    // {
    //     return View("Index");
    // }


    // CreateDish method ============================================
    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            ViewBag.allChefs = db.Chefs.ToList();
            return View("NewDish");
        }
        // newDish.DishId = (int) HttpContext.Session.GetInt32("UUID");
        // using db table name "Dishes"
        db.Dishes.Add(newDish);
        db.SaveChanges();
        return RedirectToAction("AllDishes");
    }


    //? view one Dish method ============================================
    // [HttpGet("Dishes/{dishId}")]
    // public IActionResult ViewDish(int dishId)
    // {
    //     //Query below:
    //     Dish dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

    //     if(dish == null) 
    //     {
    //         return RedirectToAction("Index");
    //     }
    //     return View("ViewDish", dish );
    // }


    //? EDIT dish method ============================================
    // [HttpGet("dishes/{dishId}/edit")]
    // public IActionResult Edit(int dishId)
    // {
    //     //Query below:
    //     Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
    //     //Stops users from editing Dishes that are not theirs
    //     if(dish == null || dish.DishId != HttpContext.Session.GetInt32("UUID")) 
    //     {
    //         return RedirectToAction("Index");
    //     }
    //     return View("Edit", dish );
    // }

    //?Update Method ============================================
    // [HttpPost("dishes/{dishId}/update")]
    // // MatchCasing to the postId route
    // public IActionResult Update(Dish editDish, int dishId)

    // {
    //     if(!ModelState.IsValid)
    //     {
    //         return Edit(dishId);
    //         // return View("Edit");
    //     }
    //     Dish? post = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        
    //     //added || statement to stop users from messing with other user's posted data
    //     if(dish == null || dish.UserId != HttpContext.Session.GetInt32("UUID")) 
    //     {
    //         return RedirectToAction("Index");
    //     }
    //     dish.DishName = editDish.DishName;
    //     dish.Calories = editDish.Calories;
    //     dish.DishChef = editDish.DishChef;
    //     dish.Tastiness = editDish.Tastiness;
    //     dish.CreatedAt = DateTime.Now;
    //     dish.UpdatedAt = DateTime.Now;
    //     db.Dishes.Update(dish);
    //     db.SaveChanges();
    //     return RedirectToAction("ViewDish", new {dishId = dishId});

    // }


    //?Delete Method ============================================
    // [HttpPost("dishes/{dishId}/delete")]
    // public IActionResult Delete(int dishId)

    
    // {
    //     Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

    //     //added to stop from deleting other's input data
    //     if(dish == null || dish.ChefId != HttpContext.Session.GetInt32("UUID")) 
    //     {
    //         return RedirectToAction("Index");
    //     }

    //     db.Dishes.Remove(dish);
    //     db.SaveChanges();
    //     // ListSortDescription in the all Dishes for Index*
    //     return RedirectToAction("Index");
    // }


    //? Privacy Route ============================================
    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
//? ATTRIBUTE OUTSIDE OF CONTROLLER =========================
//? SESSION CHECK ===========================================
// // Name this anything you want with the word "Attribute" at the end -- adding filter for session at top*
// public class SessionCheckAttribute : ActionFilterAttribute
// {
//     public override void OnActionExecuting(ActionExecutingContext context)
//     {
//         // Find the session, but remember it may be null so we need int?
//         int? userId = context.HttpContext.Session.GetInt32("UUID");
//         // Check to see if we got back null
//         if(userId == null)
//         {
//             // Redirect to the Index page if there was nothing in session
//             // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
//             context.Result = new RedirectToActionResult("Index", "User", null);
//         }
//     }

// }
