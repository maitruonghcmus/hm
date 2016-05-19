using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM.WebApp.Models
{
    public class DataTable<T>
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}