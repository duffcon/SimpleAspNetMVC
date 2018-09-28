If you want a navigation bar you will have to redundanly add the HTML to both pages. The process of adding HTML to both pages is really redundant. As a programmer it is your job to reduce redundancy.

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

Views will be rendered inside of the _Layout view.

![File](file.png)

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

Let the app know that this view should be inside of the layout view. However this is just as redundant.
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

Now every view in the app will be "inside" the layout view.


