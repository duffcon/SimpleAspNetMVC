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
        public LibraryContext mycontext;

        public HomeController(LibraryContext context)
        {
            mycontext = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> mybooks = (from b in mycontext.BookSet
                                         orderby b.Title
                                         select b).AsEnumerable();
            return View(mybooks.ToList());
        }

        public IActionResult CheckBook(int id, bool newvalue)
        {
            Book mybooks = (from b in mycontext.BookSet
                           where b.ID == id
                           select b).FirstOrDefault();
            mybooks.Out = newvalue;
            mycontext.SaveChanges();
            return View(mybooks);
        }
    }
}