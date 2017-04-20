using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Entities
{
    //public class Administration
    //{
       
    //}

    public class DownloadLog
    {

        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string type { get; set; }
        public DateTime created_date { get; set; }
        public int created_by { get; set; }
        public bool? is_active { get; set; }

        public DateTime validity { get; set; }
    }

}
