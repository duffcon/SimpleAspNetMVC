Html has buttons.
```
 <input type="button" value="Click"/>
```
And perform an Action in a controller.
```
onclick="location.href='@Url.Action("SomeAction", "SomeController", new {param1 = x, param2 = x, ...)'"
```


Make a new action to check the books in and out. 
```
//HomeControllers.cs
public IActionResult CheckBook(int id, bool newvalue)
{
    mybooks[id].Out = newvalue;
    return View(mybooks[id]);
}
```

Create View for the action.

```
//CheckBook.cshtml
@using SimpleAspNetMVC.Data.Models;
@model Book

<h2>Library</h2>

@{
    if (Model.Out == false)
    {
        <h2> You Checked In @Model.Title - @Model.Author</h2>
    }
    else
    {
        <h2> You Checked Out @Model.Title - @Model.Author</h2>
    }
}

 <input type="button" value="Go Back" onclick="location.href='@Url.Action("Index", "Home")'" />
```

Add buttons linking to that new action. Added some styling as well.
```
//Index.cshtml
@using SimpleAspNetMVC.Data.Models;
@model List<Book>


<h2>Library</h2>

@foreach (var b in Model)
{
    if (b.Out == true)
    {
        <p style="color:Red">@b.Title - @b.Author  <input type="button" value="Check In" onclick="location.href='@Url.Action("CheckBook", "Home", new {id = b.ID, newvalue = !b.Out})'" /> </p>
    }
    else
    {
        <p style="color:Green">@b.Title - @b.Author <input type="button" value="Check Out" onclick="location.href='@Url.Action("CheckBook", "Home", new {id = b.ID, newvalue = !b.Out})'" /> </p>
    }
    
}

```







mybooks needs to be static so changes are saved.
```
public static List<Book> mybooks
```



```
```