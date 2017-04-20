using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class perception
    {
        [Key]
        public Guid perception_id { get; set; }

        public string name { get; set; }
        public bool sex { get; set; }
        public int age { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int brgy_code { get; set; }

        public int? no_ip_family { get; set; }
        public int? no_pantawid_family { get; set; }
        public int? no_slp_family { get; set; }
    }
}
