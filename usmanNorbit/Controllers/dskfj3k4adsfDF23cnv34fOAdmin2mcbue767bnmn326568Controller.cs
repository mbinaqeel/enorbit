using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using usmanNorbit.Models;
using WebApplication2.Models;

namespace usmanNorbit.Controllers
{
    public class dskfj3k4adsfDF23cnv34fOAdmin2mcbue767bnmn326568Controller : Controller
    {
        //
        // GET: /dskfj3k4adsfDF23cnv34fOAdmin2mcbue767bnmn326568/
        IDataSource db;
        string key = "GxG83he4yLTbIVTVaoHb25j9T";
        string secret = "ey0bzA5L9VsHtzx5XyNOoq8yrgSA7fyJrFRImBg5QphXqjvYZb";
        string token = "784025170793099264-ziNR0wBccOF7ckhu3UjMCgAILmvh7u2";
        string tokenSecret = "IJjZ0rQVEBJ8srvSFQRkUKlF9c8xYKZnnpcZX1GvUBw30";
        TweetSharp.TwitterService service = null;
        public dskfj3k4adsfDF23cnv34fOAdmin2mcbue767bnmn326568Controller(IDataSource data)
        {
            
            service = new TweetSharp.TwitterService(key, secret);
            service.AuthenticateWith(token, tokenSecret);
            db = data;
        }

        //
        // GET: /dskfj3k4adsfDF23cnv34fOAdmin2mcbue767bnmn326568/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            string server_path_map = Server.MapPath("~/Scripts/uploads");
            string time = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            string file_time = time + ".jpg";
            var path = Path.Combine(server_path_map, file_time);
            file.SaveAs(path);

            return Json(new { link = "/Scripts/uploads/"+file_time });
        }
        public string jqgrid(string Id)
        {
            string mes = db.getComplaintContent(Id);
            return mes;
        }
        public ActionResult Gallery()
        {
            ViewBag.Curr = "active";
            List<ComplaintList> list = db.getAllComplaints();
            return View(list);
        }
        public ActionResult AddProduct()
        {
            ViewBag.CurrD = "active";
            List<category> list = db.getAllCategories();
            ViewBag.CatList = list;
            return View();
        }

        public ActionResult EditProduct()
        {
            ViewBag.EditP = "active";
            ViewBag.CatList = db.getAllCategories();
            List<product> list = db.getAllProducts();
            return View(list);

        }

        [ValidateInput(false)] 
        public RedirectResult AddaProduct(HttpPostedFileBase file, product p, string edit)
        {
            p.imgpath = null;
            if (file != null && file.ContentLength > 0)
            {
                
                string time = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
                string file_time = time +".jpg";
                
                
                ///////////////////////////////////////////////////////////////////
                // check the algo
                // path is the small image
                string product_page_url = Server.MapPath("~/Product");
                string server_path_map = Server.MapPath("~/Scripts/uploads");
                var path = Path.Combine(server_path_map, "small_"+file_time);

                string htmlfilename = p.name.Replace(' ', '_') +p.model.Replace(' ', '-') + ".html";

                var product_page_path = Path.Combine(product_page_url, htmlfilename);

                
                Bitmap original_image = (Bitmap)Image.FromStream(file.InputStream);

                int height = original_image.Height, width = original_image.Width;
                int size = (width*205/height);
                
                
                //Reducing size of image
                Bitmap upBmp = new Bitmap(original_image, new Size(size, 205));
                upBmp.Save(path, ImageFormat.Jpeg);
                string tweetID = null;

                //tweeting about the product
                //posting image
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var result = service.SendTweetWithMedia(new SendTweetWithMediaOptions
                    {

                        Status = p.name+" "+p.model+" enorbit.co.uk/Product/" + htmlfilename,
                        Images = new Dictionary<string, Stream> { { "Norbit", stream } }
                        
                    });
                    tweetID = result.IdStr;
                    
                }
                

                /*string start = "<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" /><title>"+ p.name +"</title></head><body style=\"background-color: #E2E1DD\">"
                    + "<div style=\"width:100%; height:100px; background-color:white\"><img src=\"/Scripts/images/NORBIT.jpg\" height=\"100px\" /></div><br />"
                    +"<a href=\"/Home/Index\"><div style=\"margin-left:5%; margin-top:5%\">BACK to HOME</div></a><div style=\"margin-left:5%; border:ridge;  margin-right:5%; background-color:white; border-radius:5px\"><br /><br /><div style=\"margin-left:10px\">";
                string end = "</div><br /><br /><br /></div><div style=\"margin-left:5%\"><a href=\"https://twitter.com/bitf13m006/status/"+tweetID+"\">Twitter</a></div></body></html>";*/


                string end2 = "</div><div class=\"clear\"> </div></div><div class=\"clear\"> </div></div><div class=\"clear\"> </div></div></body></html>";
                string title = "<!DOCTYPE html><html><head><title>" + p.name + "</title>";
    

                //server.mappath dont froget to
                string filepath = Server.MapPath("~/Views/Home/page.txt");
                string start2 = System.IO.File.ReadAllText(filepath) ;
                string completeHtmlText = title+start2 + edit + end2;


                //Saving HTML Page

                System.IO.File.WriteAllText(product_page_path, completeHtmlText);



                //var result = service.SendTweet(new SendTweetOptions{Status = @"enorbit.co.uk/Product/"+htmlfilename});
                //ViewBag.Txt = result.Text.ToString();
               


                p.pagepath = "/Product/"+htmlfilename;
                p.imgpath = "/Scripts/uploads/small_" + file_time;





                db.saveProduct(p);
            }


            return Redirect("/dskfj3k4adsfDF23cnv34fOAdmin2mcbue767bnmn326568/AddProduct");
        }
        public string RemoveCategory(string id)
        {
            db.removeCategory(id);
            return id;

        }

        public string RemoveProduct(string id)
        {
            string[] paths = db.removeProduct(id);
            string p = Server.MapPath("~" + paths[1]);
            //image deleted
            System.IO.File.Delete(p);
            
            removePage(paths[0]);
            
            



            return id;

        }

        private void removePage(string relativePath)
        {
            string path = Server.MapPath("~" + relativePath);
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach(var line in lines)
            {
                string[] parts = line.Trim().Split(' ');
                if(parts[0] == "<img")
                {
                    string[] pathi = parts[1].Trim().Split('"');
                    string[] checkImgFolder = pathi[1].Split('/');
                    if(checkImgFolder[2] == "uploads")
                    {
                        string cmpath = Server.MapPath(pathi[1]);
                        System.IO.File.Delete(cmpath);
                    }
                    
                }
            }
            
            
            
            System.IO.File.Delete(path);

        }

        public ActionResult AddCategory()
        {
            ViewBag.CurrS = "active";
            List<category> list = db.getAllCategories();
            ViewBag.Tit = list;
            return View();
        }


       
       

        [HttpPost]
        public RedirectResult AddpCategory(category b)
        {
            if (b.Id == 0)
            {
                db.addCategory(b.name);
            }
            else
            {
                //edit code
                db.editCategory(b);
            }
                
                ViewBag.CurrS = "active";

            return Redirect("/dskfj3k4adsfDF23cnv34fOAdmin2mcbue767bnmn326568/AddCategory");
            /* HttpPostedFileBase file = Request.Files[0]; ;
            
            
             */
        }
	}
}