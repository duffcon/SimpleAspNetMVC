using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleAspNetMVC.Data.Models;

namespace SimpleAspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        public List<Book> mybooks { get; set; } = new List<Book>
        {
            new Book {Title= "The Great Gatsby", Author = "F. Scott Fitzgerald", Out = true},
            new Book {Title = "The Adventures of Tom Sawyer", Author = "Mark Twain", Out = false},
            new Book {Title = "Adventures of Huckleberry Finn", Author = "Mark Twain", Out = false},
            new Book {Title= "This Side of Paradise", Author = "F. Scott Fitzgerald", Out = true}
        };

        public IActionResult Index()
        {
            return View(mybooks);
        }
    }
}