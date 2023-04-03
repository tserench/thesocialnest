using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;
using System.Net.Mail;
using System.Web.Helpers;
using System.Xml.Linq;
using System.Configuration;
using System.Net.Http;

namespace WebApplication4.Controllers
{
    public class Posts1Controller : DbConnection
    {
        
        // GET: Posts1

        // GET: Posts1/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.myId = User.Identity.GetUserId();
            ViewBag.postUserId = post.UserId;
            return View(post);

        }
        public ActionResult AdminPosts() 
        {
            
            var posts = db.Posts.Include(p => p.AspNetUser);

        
            return View(posts.ToList());
        }
        public String GetUserId()
        {
            var id = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
               .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    id = userIdClaim.Value;
                }
            }

            return id;
        }
        public int GetPostId([Bind(Include = "Content,Visibility")] Post post)
        {
            var id = db.Posts
               .Where(x => x.PostId == post.PostId)
               .Select(x => x.PostId)
               .FirstOrDefault();
            return id;
        }
        // GET: Posts1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Content,Visibility")] Post post)
        {
            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {
                post.UserId = GetUserId();
                post.Time = DateTime.Now;
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                SendEmailNotification();
                return RedirectToAction("Index","Home");
            }
           
        }
        public void SendEmailNotification()
        {
            List<AspNetUser> users = db.AspNetUsers.ToList();

            // Create an email message
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            message.From = new MailAddress("tserench@outlook.com");
            foreach (AspNetUser user in users)
            {
                message.To.Add(new MailAddress(user.Email));
            }
            message.Subject = "New Post posted on your test web";
            message.Body = "A new post has been created. Please visit the website to view the post.";

            // Send the email using an SMTP client
         
            client.EnableSsl = true;
            client.Port = 587;
            client.Host = "smtp.office365.com";
            //client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("tserench@outlook.com", "Totoro2825");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(message);



        }

        // GET: Posts1/Edit/5
        public ActionResult Edit(int? id, string IdOfPost)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOfPost = IdOfPost;
            return View(post);

        }
        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Content,Visibility")] Post post)
        {

            post.UserId = GetUserId();

            var PostId = GetPostId(post);

            post.Time = DateTime.Now;

            UpdatePostToDatabase(post, PostId);

            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        //UpdatePost
        public void UpdatePostToDatabase([Bind(Include = "Content,Visibility")] Post post, int PostId)
        {
            if (ModelState.IsValid)
            {
                if (PostId != null)
                {
                    UpdateModel(post);
                    db.Posts.Attach(post);
                    db.Entry(post).State = EntityState.Modified;
                }
            }
        }
        // GET: Posts1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int Id)
        {
            Post post = await db.Posts.FindAsync(id);
            
            db.Posts.Remove(post);
         
             db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

