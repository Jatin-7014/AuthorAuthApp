using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthorApp.Data;
using AuthorApp.Models;
using AuthorApp.ViewModels;

namespace AuthorApp.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM data)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var author=session.Query<Author>().FirstOrDefault(a=>a.Name==data.AuthorName);
                    if (author!=null)
                    {
                        Session["AuthorId"] = author.Id;
                        if(BCrypt.Net.BCrypt.Verify(data.Password, author.Password))
                        {
                            FormsAuthentication.SetAuthCookie(data.AuthorName, true);
                            return RedirectToAction("Index", "Author");
                        }
                    }
                    ModelState.AddModelError("", "AuthorName/Password doesn't exists");
                    return View(data);
                }
            }

        }
        public ActionResult GetAllBooks()
        {
            using(var session = NHibernateHelper.CreateSession())
            {
                var books=session.Query<Book>().ToList();
                return View(books);
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Author author)
        {
            using(var session = NHibernateHelper.CreateSession())
            {
                using(var txn = session.BeginTransaction())
                {
                    author.AuthorDetail.Author = author;
                    author.Password = BCrypt.Net.BCrypt.HashPassword(author.Password);
                    session.Save(author);
                    txn.Commit();
                    return RedirectToAction("Login");
                }
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}