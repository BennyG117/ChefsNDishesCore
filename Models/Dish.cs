#pragma warning disable CS8618


using System.ComponentModel.DataAnnotations;
namespace ChefsNDishesCore.Models;


public class Dish
{
    //*KEY*
    [Key]
    // DishId =========================
    public int DishId {get; set;}


    // DishName ========================= 
    [Required]
    // [MinLength(2, ErrorMessage = "Must be 2 characters long")]
    // [MaxLength(40, ErrorMessage = "No longer than 40 characters long")]
    public string DishName {get; set;}
    

    // Calories ======================== 
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0!")]
    public int Calories {get; set;}


    // Tastiness ======================== 
    [Required]
    [Range(1,5, ErrorMessage = "You must include a Tastiness score between 1 - 5.")]    
    public int Tastiness {get; set;}


    // CreatedAt ======================== 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    


    // UpdatedAt ======================== 
    public DateTime UpdatedAt { get; set; } = DateTime.Now;



    //! foreign key ============================
    public int ChefId {get; set;}


    public Chef? Creator {get; set;}





}
