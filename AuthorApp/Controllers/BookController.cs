using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorApp.Data;
using AuthorApp.Models;

namespace AuthorApp.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBook()
        {
            var book = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var books = session.Query<Book>().Where(b => b.Author.Id == book).ToList();
                return View(books);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(Book book)
        {

            var authorId = Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var author = session.Get<Author>((Guid)authorId);
                    book.Author = author;
                    session.Save(book);
                    txn.Commit();
                    return RedirectToAction("GetBook", new { authId = (Guid)authorId });
                }
            }

        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {

                    var book = session.Get<Book>(id);
                    return View(book);
                }
            }
        }
        [HttpPost]

        public ActionResult Edit(Book book)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var toUpdateBook = session.Get<Book>(book.Id);
                    toUpdateBook.Name = book.Name;
                    toUpdateBook.Genre = book.Genre;
                    toUpdateBook.Description = book.Description;
                    session.Update(toUpdateBook);
                    txn.Commit();
                    return RedirectToAction("GetBook");

                }
            }
        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {

                    var book = session.Get<Book>(id);
                    return View(book);
                }
            }
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteBook(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var book = session.Get<Book>(id);
                    session.Delete(book);
                    txn.Commit();
                    return RedirectToAction("GetBook");
                }
            }
        }
    }
}