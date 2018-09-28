
Just using in memory data, will use a database instead using the EntityFramework.

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

"Put" the context into our services
```
//Startup.cs
    var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true";
    services.AddDbContext<LibraryContext>
        (options => options.UseSqlServer(connectionString));
```

Add using statements
```
//Startup.cs
using Microsoft.EntityFrameworkCore;
using SimpleAspNetMVC.Data.Models;
```


Package Manager Console
```
update-database
```

Created database, but empty
```
View > SQL Server Object Explorer
[Refresh]
SQL Server > (localdb)\MSSQLocalDB.... > Databases > LibraryDB
```


Need to populate or initialize the database. Create DBInit.cs. Call the Seed function to populate the database with books.

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