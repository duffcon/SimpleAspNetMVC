using SimpleAspNetMVC.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAspNetMVC.Data.interfaces
{
    public interface ILibraryIntern
    {
        IEnumerable<Book> OrderByTitle { get; }
        IEnumerable<Book> OrderByOut { get; }
        Book SetOut(int id, bool val);
    }
}
