using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using usmanNorbit.Models;
using WebApplication2.Models;

namespace usmanNorbit.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        IDataSource db;

        public ActionResult bdms()
        {
            ViewBag.CatList = db.getAllCategories();
            return View();
        }
        public ActionResult pd()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.CatList = db.getAllCategories();
            ViewBag.Abt = "active";
            return View();
        }

        public ActionResult mobileVersionProducts()
        {
            ViewBag.CatList = db.getAllCategories();
            return View();
        }
        public ActionResult Applications()
        {
            ViewBag.CatList = db.getAllCategories();
            ViewBag.App = "active";
            return View();
        }
        public HomeController(IDataSource data)
        {
            db = data;
        }
        //
        // GET: /Home/
        public RedirectResult SubmitComplaints(custComplaint b)
        {
            //db.addCategory(b.name);
            ViewBag.CurrS = "active";

            db.submitComplaint(b);


            return Redirect("/Home/Thank"); ;
            /* HttpPostedFileBase file = Request.Files[0]; ;
            
            
             */
        }
        public ActionResult ProdcutDetail(int? id)
        {
            List<category> list = db.getAllCategories();
            ViewBag.CatList = list;

            product p = db.getProduct(id);


            return View(p);
        }
        public ActionResult Thank()
        {
            return View();
        }
        public ActionResult Index()
        {

            List<category> list = db.getAllCategories();
            ViewBag.CatList = list;
            ViewBag.HA = "active";

            return View();
        }

        public ActionResult Complaint()
        {
            List<category> list = db.getAllCategories();
            ViewBag.CatList = list;
            ViewBag.CA = "active";
            return View();
        }

        [HttpPost]
        public ActionResult Image(custComplaint b)
        {
            //db.addCategory(b.name);
            ViewBag.CurrS = "active";

            return View("com");
            /* HttpPostedFileBase file = Request.Files[0]; ;
            
            
             */
        }

        public ActionResult Products(int? id)
        {
            List<category> list = db.getAllCategories();
            ViewBag.CatList = list;

            List<Product_Grid> li = db.getProductsByCategory(id);

            return View(li);
        }


       
	}
}