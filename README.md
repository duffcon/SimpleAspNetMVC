Before you can access the database you need to learn about dependency injection.

The Good, the Bad, and the Ugly.

The Ugly: A pizza place that only serves EXTRA pepperoni.
![File](file.png)

The Bad: A pizza place that only serves pepperoni, but you can specify the amount.
![File2](file2.png)

The Good: A pizza place that allows you to choose your toppings (a normal pizza place).
![File3](file3.png)

Our app is ugly as it directly instantiates data.
```
//HomeController.cs
public static List<Book> mybooks { get; set; } = new List<Book>{...}
```


Upgrade to bad: passing the context as a parameter.
```
//HomeController.cs
public LibraryContext mycontext;

public HomeController(LibraryContext _libraryContext)
{
    mycontext = _libraryContext;
}
```




The controller will now interact with a context object instead of a list object. The syntax is LINQ which is similar to SQL. 

Modify the Index and CheckBook action.
```
//HomeController.cs
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
```


Optional: Add sorting feature.
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

