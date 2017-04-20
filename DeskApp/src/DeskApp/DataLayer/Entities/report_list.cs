using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Entities
{
    public class report_list
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int report_list_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int table_name_id { get; set; }
        public string url { get; set; }
        public bool? is_deleted { get; set; }
    }

    public class mov_list
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mov_list_id { get; set; }
        public string name { get; set; }
        public int max { get; set; }
        public int table_name_id { get; set; }
    }
    public class attached_mov
    {
        [Key]
        public Guid attached_mov_id { get; set; }
        public Guid record_id { get; set; }
        public string filename { get; set; }

        public int mov_list_id { get; set; }
        public int table_name_id { get; set; }

        [JsonIgnore]
        public virtual mov_list mov_list { get; set; }
        [JsonIgnore]
        public virtual table_name table_name { get; set; }



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


        #endregion


        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }

        public int fund_source_id { get; set; }
        public int? cycle_id { get; set; }
        public int? enrollment_id { get; set; }

        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }


        [JsonIgnore]
        public virtual lib_enrollment lib_enrollment { get; set; }
        [JsonIgnore]

        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]

        public virtual lib_cycle lib_cycle { get; set; }


    }

}
