using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorApp.Models;
using FluentNHibernate.Mapping;
using NHibernate.Cfg.XmlHbmBinding;

namespace AuthorApp.Mappings
{
    public class AuthorMap:ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("Authors");
            Id(a=>a.Id).GeneratedBy.GuidComb();
            Map(a => a.Name);
            Map(a=>a.Email);
            Map(a=>a.Age);
            Map(a=>a.Password);
            HasOne(a => a.AuthorDetail).PropertyRef(a => a.Author).Cascade.All();
            HasMany(a=>a.Books).Inverse().Cascade.All();

        }
    }
}