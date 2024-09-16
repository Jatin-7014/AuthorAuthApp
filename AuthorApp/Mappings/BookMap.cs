using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorApp.Models;
using FluentNHibernate.Mapping;

namespace AuthorApp.Mappings
{
    public class BookMap:ClassMap<Book>
    {
        public BookMap()
        {
            Table("Books");
            Id(b=>b.Id).GeneratedBy.Identity();
            Map(b => b.Name);
            Map(b => b.Description);
            Map(b=>b.Genre);
            References(b=>b.Author).Column("AuthorId").Cascade.None().Nullable();
        }
    }
}