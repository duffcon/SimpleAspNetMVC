using SimpleAspNetMVC.Data.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAspNetMVC.Data.Models
{
    public class LibraryIntern : ILibraryIntern
    {

        private readonly LibraryContext libraryContext;

        public LibraryIntern(LibraryContext _libraryContext)
        {
            libraryContext = _libraryContext;
        }


        public IEnumerable<Book> OrderByTitle
        {
            get
            {
                var query = (from b in libraryContext.BookSet
                             orderby b.Title
                             select b
                ).AsEnumerable();
                return query;
            }

        }

        public IEnumerable<Book> OrderByOut
        {
            get
            {
                var query = (from b in libraryContext.BookSet
                             orderby b.Out
                             select b
                            ).AsEnumerable();
                return query;
            }

        }


        public Book SetOut(int id, bool val)
        {
            var query = (from b in libraryContext.BookSet
                         where b.ID == id
                         select b).FirstOrDefault();
            query.Out = val;
            libraryContext.SaveChanges();
            return query;


        }


    }
}
