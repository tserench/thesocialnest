using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebApplication4.Models
{
    public class DbConnection : Controller
    {
        protected Model1 db = new Model1();
    }
}