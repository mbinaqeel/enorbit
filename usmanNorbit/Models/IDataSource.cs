using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace usmanNorbit.Models
{
    public interface IDataSource
    {
        void editCategory(category c);
        List<product> getAllProducts();
        bool addCategory(string name);
        List<category> getAllCategories();
        product getProduct(int? id);
        void removeCategory(string id);
        string[] removeProduct(string id);
        void submitComplaint(custComplaint c);
        List<Product_Grid> getProductsByCategory(int? catid);
        void saveProduct(product p);
        List<ComplaintList> getAllComplaints();
        string getComplaintContent(string id);
    }
}
