If we want a navigation bar at the top you will have to add the html to both pages which is redundant
```
//Index.cshtml
<ul>
    <li><a class="active" href="#home">Home</a></li>
</ul>
```


```
//BookClass.cshtml
<ul>
    <li><a class="active" href="#home">Home</a></li>
</ul>
```

Create Views/Shared

```
Add Item > Layout > _Layout.cshtml
```

```
//_Layout.cshtml
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title</title>
</head>
<body>
    <style>
        ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
            overflow: hidden;
            background-color: #333;
        }

        li {
            float: left;
        }

            li a {
                display: block;
                color: white;
                text-align: center;
                padding: 14px 16px;
                text-decoration: none;
            }
    </style>
    <ul>
        <li><a class="active" href="#home">Library</a></li>
    </ul>
   

    <div>
        @RenderBody()
    </div>
</body>
</html>

```

Let the app know that the views should be inside of the layout view. However this is just as redundant.
```
//Index.cshtml
@{
    Layout = "_Layout";
}
```


```
//BookClass.cshtml
@{
    Layout = "_Layout";
}
```

ViewStart is the solution.
```
Add Item > View Start > _ViewStart.cshtml
```

```
_ViewStart.cshtml
@{
    Layout = "_Layout";
}
```

Now every view you create will be "inside" the layout view.


