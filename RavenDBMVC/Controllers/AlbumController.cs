using Raven.Abstractions.Commands;
using Raven.Client.Document;
using RavenDBMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RavenDBMVC.Controllers
{
    public class AlbumController : Controller
    {
        DocumentStore documentStore = null;

        public AlbumController()
        {
            documentStore = new DocumentStore
            {
                Url = "http://localhost:8080"
            };
            documentStore.Initialize();
        }

        public ActionResult Index()
        {
            using (var session = documentStore.OpenSession())
            {
                var booksData = session.Query<Album>().ToList();
                return View(booksData);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                using (var session = documentStore.OpenSession())
                {
                    session.Store(album);
                    session.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            using (var session = documentStore.OpenSession())
            {
                var bookData = session.Load<Album>(id);
                return View(bookData);
            }
        }

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            using (var session = documentStore.OpenSession())
            {
                var albumData = session.Load<Album>(album.Id);
                albumData.Title = album.Title;
                albumData.Genre = album.Genre;
                albumData.ReleaseDate = album.ReleaseDate;
                session.Store(albumData);
                session.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            using (var session = documentStore.OpenSession())
            {
                var bookData = session.Load<Album>(id);
                return View(bookData);
            }
        }

        [HttpPost]
        public ActionResult Delete(Album album)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Advanced.Defer(new DeleteCommandData { Key = "albums/" + album.Id });
                session.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public ActionResult Details(int? id)
        {
            using (var session = documentStore.OpenSession())
            {
                var bookData = session.Load<Album>(id);
                return View(bookData);
            }
        }
    }
}