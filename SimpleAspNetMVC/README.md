

The Good, The Bad, and the Ugly.

The Ugly: Directly instantiate data.
```
//HomeController.cs
public static List<Book> mybooks { get; set; } = new List<Book>{...}
```


The Bad (But better): Passing the context as a parameter.
```
//HomeController.cs
public LibraryContext mycontext;

public HomeController(LibraryContext _libraryContext)
{
    mycontext = _libraryContext;
}
```




Will interact with DBSet<Book> instead of a List<Book> using LINQ.

Modify the Index action.
```
//HomeController.cs
public IActionResult Index()
{
    IEnumerable<Book> mybooks = (from b in mycontext.BookSet
                    orderby b.Title
                    select b).AsEnumerable();
    return View(mybooks.ToList());
}

```





And the CheckBook action.
```
//HomeController.cs
public IActionResult CheckBook(int id, bool newvalue)
{
    Book mybooks = (from b in mycontext.BookSet
                    where b.ID == id
                    select b).FirstOrDefault();
    mybooks.Out = newvalue;
    mycontext.SaveChanges();
    return View(mybooks);
}
```


Optional: Using some more advanced LINQ add sorting feature
```
//HomeController.cs
public IActionResult Index(int sort = 1)
{
    IEnumerable<Book> mybooks = null;
    switch (sort)
    {
        case 1:
            mybooks = (from b in mycontext.BookSet
                            orderby b.Title
                            select b).AsEnumerable();
            break;
        case 2:
            mybooks = (from b in mycontext.BookSet
                            orderby b.Out
                            select b).AsEnumerable();
            break;
               
    }
    return View(mybooks.ToList());
}

```




```
//Index.cshtml
<br /> 
<input type="button" value="SortTitle" onclick="location.href='@Url.Action("Index", "Home", new {sort =  1})'" />
<input type="button" value="SortOut" onclick="location.href='@Url.Action("Index", "Home", new {sort =  2})'" />
```

