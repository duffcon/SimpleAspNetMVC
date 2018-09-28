using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleAspNetMVC.Data.interfaces;
using SimpleAspNetMVC.Data.Models;

namespace SimpleAspNetMVC.Controllers
{
    public class HomeController : Controller
    {
        public ILibraryIntern myintern;

        public HomeController(ILibraryIntern intern)
        {
            myintern = intern;
        }

        public IActionResult Index(int sort = 1)
        {
            switch (sort)
            {
                case 2:
                    return View(myintern.OrderByOut.ToList());

                default:
                    return View(myintern.OrderByTitle.ToList());

            }
        }

        public IActionResult CheckBook(int id, bool newvalue)
        {
            return View(myintern.SetOut(id, newvalue));
        }
    }
}