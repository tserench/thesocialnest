﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    [RequireHttps]
    [Authorize]
    public class HomeController : DbConnection
    {

        public ActionResult Index()
        {
            var model = db.Posts;
            ViewBag.myId = User.Identity.GetUserId();
            ViewBag.myFriends = GetMyFriends().ToList();
            return View(model);
        }

        private IQueryable<string> GetMyFriends()
        {
            var myId = User.Identity.GetUserId();
            return from friend in db.FriendOfUsers
                   where friend.UserId == myId
                   select friend.FriendId;
        }
       
        
    }
}