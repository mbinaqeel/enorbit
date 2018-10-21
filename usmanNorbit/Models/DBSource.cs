using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace usmanNorbit.Models
{
    public class DBSource: IDataSource
    {

        public List<product> getAllProducts()
        {
            using(NORBITEntities b = new NORBITEntities())
            {
                var q = from x in b.products
                        select x;
                return q.ToList();
            }
        }
        public string getComplaintContent(string id)
        {
            int i = Convert.ToInt32(id);
            using(NORBITEntities b = new NORBITEntities())
            {
                var q = from x in b.custComplaints
                        where x.Id == i
                        select x;
                custComplaint c = q.ToList().ElementAt(0);
                string message = c.message;
                c.status = 1;
                b.SaveChanges();
                
                
                return message;

            }
        }

        public void submitComplaint(custComplaint c)
        {
            using(NORBITEntities b = new NORBITEntities())
            {
                c.date = DateTime.Now;
                b.custComplaints.Add(c);
                b.SaveChanges();
            }
        }
        public product getProduct(int? id)
        {
            using(NORBITEntities b = new NORBITEntities())
            {
                var q = from x in b.products
                        where x.Id == id
                        select x;
                return q.ToList().ElementAt(0);
            }
        }
        public List<Product_Grid> getProductsByCategory(int? catid)
        {
            using (NORBITEntities b = new NORBITEntities())
            {
                var q = from x in b.products
                        where x.catid == catid
                        select new Product_Grid {pagepath = x.pagepath, Id = x.Id, name = x.name, imgpath = x.imgpath , model=x.model};
                return q.ToList();
            }
        }
        public List<ComplaintList> getAllComplaints()
        {
            using (NORBITEntities db = new NORBITEntities())
            {
                var q = from x in db.custComplaints
                        orderby x.Id descending
                        select new ComplaintList {subject = x.subject, Id = x.Id, name = x.name, email = x.email, status = x.status, date = x.date };
                return q.ToList();
            }
        }
        public bool addCategory(string name)
        {
            using (NORBITEntities db = new NORBITEntities())
            {
                category cat = new category();
                cat.name = name;
                db.categories.Add(cat);

                db.SaveChanges();
                return true;
            }

        }

        public void saveProduct(product p)
        {
            using (NORBITEntities db = new NORBITEntities())
            {
                db.products.Add(p);
                db.SaveChanges();
            }
        }

        public void editCategory(category c)
        {
            using(NORBITEntities b = new NORBITEntities())
            {
                var q = from x in b.categories
                        where x.Id == c.Id
                        select x;
                category toEdit = q.ToList().ElementAt(0);
                toEdit.name = c.name;
                b.SaveChanges();
            }
        }
        public string[] removeProduct(string id)
        {
            int pid=  Convert.ToInt32(id);
            using(NORBITEntities db = new NORBITEntities())
            {
                product p = new product();
                var q = from x in db.products
                        where x.Id == pid
                        select x;
                p = q.ToList().ElementAt(0);
                string pagepath = p.pagepath, imgpath = p.imgpath;
                string[] paths = new string[]{p.pagepath, p.imgpath};
                db.products.Remove(p);
                db.SaveChanges();
                return paths;

            }
        }

        
        
        public void removeCategory(string id)
        {
            int cid = Convert.ToInt32(id);
            using (NORBITEntities db = new NORBITEntities())
            {
                category c = new category();
                var q = from x in db.categories
                        where x.Id == cid
                        select x;

                c = q.ToList().ElementAt(0);
                db.categories.Remove(c);
                db.SaveChanges();

            }
        }

        public List<category> getAllCategories()
        {
            using (NORBITEntities db = new NORBITEntities())
            {
                var q = from c in db.categories
                        select c;
                List<category> list = q.ToList();
                return list;
            }
        }
    }
}