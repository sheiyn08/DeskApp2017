using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskApp.DataLayer
{


    //public class lib_ceac_activity
    //{
    //    [Key]
    //    public  int ceac_activity_id { get; set; }
    //    public  string name { get; set; }
    //    public  int activity_type_id { get; set; }

    //    /// <summary>
    //    /// Primary Key of activity_type e.g. training_type_id
    //    /// </summary>
    //    public int? activity_id { get; set; }

    //}


    public class lib_implementation_status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int implementation_status_id { get; set; }
        public string name { get; set; }
    }


    public class ceac_list
    {
        [Key]
        public Guid ceac_list_id { get; set; }
        public string old_id { get; set; }
        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }





        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int enrollment_id { get; set; }

        public int? implementation_status_id { get; set; }
        


        [JsonIgnore]
        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }
        [JsonIgnore]
        public virtual lib_enrollment lib_enrollment { get; set; }

        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
        #endregion

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
        [JsonIgnore]
        public virtual lib_push_status lib_push_status { get; set; }

        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion
    }

    public class ceac_tracking
    {
        [Key]
        public Guid ceac_tracking_id { get; set; }


        public Guid? reference_id { get; set; }

        public Guid ceac_list_id { get; set; }
        [JsonIgnore]

        public virtual ceac_list ceac_list { get; set; }

        public string old_id { get; set; }


        public int ceac_activity_id { get; set; }

        public DateTime? plan_start { get; set; }
        public DateTime? plan_end { get; set; }

        public DateTime? actual_start { get; set; }
        public DateTime? actual_end { get; set; }

        public DateTime? catch_start { get; set; }
        public DateTime? catch_end { get; set; }


        public int training_category_id { get; set; }

        [JsonIgnore]
        public virtual lib_training_category lib_training_category { get; set; }

        [JsonIgnore]
        public virtual lib_lgu_level lib_lgu_level { get; set; }


        public int lgu_level_id { get; set; }
        public int implementation_status_id { get; set; }

        [JsonIgnore]
        public virtual lib_implementation_status lib_implementation_status { get; set; }
        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }





        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int enrollment_id { get; set; }


        [JsonIgnore]
        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }
        [JsonIgnore]
        public virtual lib_enrollment lib_enrollment { get; set; }

        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
        #endregion

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
        [JsonIgnore]
        public virtual lib_push_status lib_push_status { get; set; }

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
