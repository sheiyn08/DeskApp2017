using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DeskApp.DataLayer
{
    
    public class grs_installation
    {
        [Key]
        public Guid grs_installation_id { get; set; }

        public string old_id { get; set; }

    
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        //RDR08242017 Additional columns for v3.0:
        public bool? is_lgu_led { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }

        //   public int enrollment_id { get; set; }
        public int lgu_level_id { get; set; }





        public DateTime? date_voliden { get; set; }


        public DateTime? date_infodess { get; set; }	//barangay
        public DateTime? date_orientation { get; set; }
        public DateTime? date_ffcomm { get; set; }
        public DateTime? date_training { get; set; }
        public DateTime? date_inspect { get; set; }
        public int? no_manuals { get; set; }	//barangay
        public int? other_mat { get; set; }	//municipal
        public int? no_brochures { get; set; }
        public int? no_tarpauline { get; set; }
        public int? no_posters { get; set; }

        public DateTime? date_meansrept { get; set; }
        public bool? is_boxinstalled { get; set; }
        public string phone_no { get; set; }
        public string address { get; set; }
        public string remarks { get; set; }


        public  DateTime? date_means { get; set; }



      




        [JsonIgnore]
        public virtual lib_lgu_level lib_lgu_level { get; set; }
        //[JsonIgnore]
        //public virtual lib_enrollment lib_enrollment { get; set; }

        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
        [JsonIgnore]

        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }

        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        #endregion

        #region Sync
        //used for offline sync purposes
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion
    }
}
