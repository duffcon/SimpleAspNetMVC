The server will now use the MVC architecture.

![File](file.png)

Use MVC instead of static files.
```
services.AddMvc();
app.UseMvcWithDefaultRoute();
```


Create folders
```
Controllers
SomeFolder/AnotherFolder
```

Create Files
```
Controllers/HomeController.cs
SomeFolder/AnotherFolder/Index.cshtml
```

Change returned view
```
return View("SomeFolder/AnotherFolder/index.cshtml");
```


More control over file access. The client can no longer access passwords and has no concept of the folder structure.
```
localhost:XXXX/passwords.txt
```
