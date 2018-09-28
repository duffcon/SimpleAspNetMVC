
It is bad practice to have the connection string out in the open. It is better to use appsettings which is more "hidden".
```
//Startup.cs
var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true";
```


```
Add Item > [search appsettings] > App Settings File
```

```
//appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=_CHANGE_ME;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

Changed the CHANGE_ME to LibraryDB.
```
//appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

Need to tell the app to use appsettings. Create configuration property and build it inside the constructor.
```
//Startup.cs
using Microsoft.Extensions.Configuration;


public IConfiguration Configuration { get; set; }

public Startup(IHostingEnvironment env)
{
    Configuration = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .Build();
}

```

Get rid of the connection string.

Old
```
//Startup.cs
var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true";
services.AddDbContext<LibraryContext>
    (options => options.UseSqlServer(connectionString));
```



New
```
services.AddDbContext<LibraryContext>
    (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
```



