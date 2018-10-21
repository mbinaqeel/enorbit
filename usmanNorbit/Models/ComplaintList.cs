using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ComplaintList
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public byte status { get; set; }
        public string subject { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    }
}