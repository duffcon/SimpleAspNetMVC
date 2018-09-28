Currently using "In-Memory" data, will use a database instead.

Normally you interact with a database directly using an SQL query.

![File](file.png)

This project will use the EntityFramework (middle-man) which makes the interaction less tedious.

![File2](file2.png)

You will have a DbContext class containing a DBSet<> for every table in the database. 

![File3](file3.png)

Install EntityFramework
```
Tools > NuGet Package Manager > Package Manager Console
cd SimpleAspNetMVC
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```




Create LibraryContext
```
//LibraryContext.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAspNetMVC.Data.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        public DbSet<Book> BookSet { get; set; }

    }
}
```


You connect to the database with a connection string like this.
```
"Server=(localdb)\\MSSQLLocalDB;Database=_CHANGE_ME_;Trusted_Connection=True;MultipleActiveResultSets=true"
```

I will call the database: LibraryDB.
```
"Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
```

Pass the context into our app
```
//Startup.cs
using Microsoft.EntityFrameworkCore;
using SimpleAspNetMVC.Data.Models;

var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true";
services.AddDbContext<LibraryContext>
    (options => options.UseSqlServer(connectionString));
```

Right now we have a context object and a connection string pointing to a theoretical database.

![File4](file4.png)

Type the following to let EntityFramework create it for you.
```
[Package Manager Console]
add-migration initial
update-database
```

An empty database should now exist.

![File5](file5.png)


```
View > SQL Server Object Explorer
[Refresh]
SQL Server > (localdb)\MSSQLocalDB.... > Databases > LibraryDB
```


Need to populate the database. Create DBInit.cs with a Seed function.

```
//DBInit.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAspNetMVC.Data.Models
{
    public class DBInit
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            LibraryContext context = applicationBuilder.ApplicationServices.GetRequiredService<LibraryContext>();

            //If The database has no books
            if (!context.BookSet.Any())
            {
                context.AddRange
                (
                    new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Out = true },
                    new Book { Title = "The Adventures of Tom Sawyer", Author = "Mark Twain", Out = false },
                    new Book { Title = "Adventures of Huckleberry Finn", Author = "Mark Twain", Out = false },
                    new Book { Title = "This Side of Paradise", Author = "F Scott Fitzgerald", Out = true }
                );
            }

            context.SaveChanges();

        }
    }
}
```

Seed function called at startup.
```
//Startup.cs
DBInit.Seed(app);
```

You will get thrown and error add this code to Program.cs

Old
```
//Program.cs
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
```

New
```
//Program.cs
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options =>
                        options.ValidateScopes = false);
```

Run the program and exit. The database has values.
```
SQL Server Object Explorer
[Refresh]
SQL Server > (localdb)\MSSQLocalDB.... > Databases > LibraryDB >Tables > dbo.BookSet > [right click] > View Data
```

You now have an idle populated database which will be used in the next branch.
