Upgrade to Good: passing interface. 

The HomeController has a lot on its plate: dealing with actions AND books. We will outsource the book stuff to an intern.

Create Data/inerfaces.

Create interface ILibraryIntern.cs (Job posting of responsibilities).
```
//ILibraryIntern.cs
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

```



LibraryIntern has passed the interview they need to train for the job (inplement interface). Create LibraryIntern.cs
```
//LibraryIntern.cs
using SimpleAspNetMVC.Data.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAspNetMVC.Data.Models
{
    public class LibraryIntern : ILibraryIntern
    {

        private readonly LibraryContext libraryContext;

        public LibraryIntern(LibraryContext _libraryContext)
        {
            _libraryContext = libraryContext;
        }


        public IEnumerable<Book> OrderByTitle
        {
            get
            {
                var query = (from b in libraryContext.BookSet
                             orderby b.Title
                             select b
                ).AsEnumerable();
                return query;
            }

        }

        public IEnumerable<Book> OrderByOut
        {
            get
            {
                var query = (from b in libraryContext.BookSet
                             orderby b.Out
                             select b
                            ).AsEnumerable();
                return query;
            }

        }


        public Book SetOut(int id, bool val)
        {
            var query = (from b in libraryContext.BookSet
                         where b.ID == id
                         select b).FirstOrDefault();
            query.Out = val;
            libraryContext.SaveChanges();
            return query;


        }


    }
}
```



Inject the intern(let the intern inside the building).
```
//Startup.cs
using SimpleAspNetMVC.Data.interfaces;

services.AddTransient<ILibraryIntern, LibraryIntern>();
```

Modify the constructor.
```
//HomeController.cs
using SimpleAspNetMVC.Data.interfaces;

public ILibraryIntern myintern;

public HomeController(ILibraryIntern intern)
{
    myintern = intern;
}

```


Add sorting button
```
//Index.cshtml
<input type="button" value="SortTitle" onclick="location.href='@Url.Action("Index", "Home", new {sort =  1})'" />
<input type="button" value="SortOut" onclick="location.href='@Url.Action("Index", "Home", new {sort =  2})'" />
```

Modify actions to use the intern (put the intern to work).
```
//HomeController.cs
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
```

