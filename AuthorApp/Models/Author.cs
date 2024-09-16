using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthorApp.Models
{
    public class Author
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual int Age {  get; set; }

        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
        public virtual AuthorDetail AuthorDetail { get; set; }=new AuthorDetail();
        public virtual IList<Book>Books { get; set; } = new List<Book>();
    }
}