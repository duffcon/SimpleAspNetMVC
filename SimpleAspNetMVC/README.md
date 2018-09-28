Instantiate some books in the HomeController
```
//HomeController.cs
public List<Book> mybooks { get; set; } = new List<Book>
{
    new Book {Title= "The Great Gatsby", Author = "F. Scott Fitzgerald", Out = true},
    new Book {Title = "The Adventures of Tom Sawyer", Author = "Mark Twain", Out = false},
    new Book {Title = "Adventures of Huckleberry Finn", Author = "Mark Twain", Out = false},
    new Book {Title= "This Side of Paradise", Author = "F. Scott Fitzgerald", Out = true}
};
```


Prints a list of books.
```
//HomeController.cs
return View(mybooks);
```

```
//Index.cshtml
@using SimpleAspNetMVC.Data.Models;
@model Book

<h2 style="color:brown">Library</h2>

@{ 
    <p>@Model.Title - @Model.Author Out:@Model.Out</p>
}
```

Optional: Modify the model to a single book.
```
//HomeController.cs
return View(mybooks[0]);
```

```
//Index.cshtml
@using SimpleAspNetMVC.Data.Models;
@model List<Book>

<h2 style="color:brown">Library</h2>

@foreach (var b in Model)
{
    <p> @b.Title - @b.Author Out:@b.Out</p>
}
```





```
```