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
                    new Book { Title = "This Side of Paradise", Author = "F. Scott Fitzgerald", Out = true }
                );
            }

            context.SaveChanges();

        }
    }
}
