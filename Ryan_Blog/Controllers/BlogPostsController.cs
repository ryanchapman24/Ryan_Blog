using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ryan_Blog.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Ryan_Blog.Controllers
{
    [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index(int? page, string query)
        {
            int pageSize = 3; // the number of posts you want to display per page
            int pageNumber = (page ?? 1);
            ViewBag.Query = query;
            var qposts = db.BlogPosts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                qposts = qposts.Where(p => p.Title.Contains(query) || p.Body.Contains(query) || p.Comments.Any(c => c.Body.Contains(query) || c.Author.DisplayName.Contains(query)));
            }
            var posts = qposts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize);
            return View(posts);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            var blogPosts = db.BlogPosts.ToList();
            return View(blogPosts);
        }

        // GET: BlogPosts/Details/5

        public ActionResult Details(string slug)
        {
            if (String.IsNullOrWhiteSpace(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.FirstOrDefault(p => p.Slug == slug);

            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/assets/blog/"), fileName));
                    blogPost.MediaURL = "~/assets/blog/" + fileName;
                }

                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(blogPost);
                }
                if (db.BlogPosts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(blogPost);
                }

                var defaultMedia = "/assets/ryanChapmanLogo_Weave.jpg";
                if (String.IsNullOrWhiteSpace(blogPost.MediaURL))
                {
                    blogPost.MediaURL = defaultMedia;
                }

                blogPost.Slug = Slug;
                blogPost.Created = System.DateTime.Now;
                db.BlogPosts.Add(blogPost);

                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,MediaURL,Body,Slug,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                blogPost.Updated = System.DateTime.Now;
                db.BlogPosts.Attach(blogPost);
                db.Entry(blogPost).Property("Title").IsModified = true;
                db.Entry(blogPost).Property("Body").IsModified = true;
                db.Entry(blogPost).Property("MediaURL").IsModified = true;
                db.Entry(blogPost).Property("Slug").IsModified = true;
                db.Entry(blogPost).Property("Updated").IsModified = true;
                db.Entry(blogPost).Property("Published").IsModified = true;
                //db.Entry(blogPost).State = EntityState.Modified;

                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/assets/blog/"), fileName));
                    blogPost.MediaURL = "~/assets/blog/" + fileName;
                }

                var defaultMedia = "/assets/ryanChapmanLogo_Weave.jpg";
                if (String.IsNullOrWhiteSpace(blogPost.MediaURL))
                {
                    blogPost.MediaURL = defaultMedia;
                }

                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid Title.");
                    return View(blogPost);
                }
                var SlugAlreadyExists = db.BlogPosts.Where(p => p.Id == blogPost.Id && p.Slug == Slug).Select(p => p.Slug);
                if (!SlugAlreadyExists.Any())
                {
                    if (db.BlogPosts.Any(p => p.Slug == Slug))
                    {
                        ModelState.AddModelError("Title", "The title must be unique.");
                        return View(blogPost);
                    }
                }

                blogPost.Slug = Slug;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int? id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateComment(Comment comment, string slug)
        {
            if (ModelState.IsValid)
            {
                comment.AuthorId = User.Identity.GetUserId();
                comment.Created = System.DateTime.Now;
                comment.Body = "<p>"+comment.Body+"</p>";
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "BlogPosts", new { slug });
        }

        // GET: BlogPosts/EditComment/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: BlogPosts/EditComment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult EditComment(Comment comment, string slug)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Attach(comment);
                db.Entry(comment).Property("Body").IsModified = true;
                db.Entry(comment).Property("Updated").IsModified = true;
                comment.Updated = System.DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Details", "BlogPosts", new { slug });
            }
            return View(comment);
        }

        // GET: BlogPosts/DeleteComment/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: BlogPosts/DeleteComment/5
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteCommentConfirmed(int? id, string slug)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new { slug });
        }
    }
}
