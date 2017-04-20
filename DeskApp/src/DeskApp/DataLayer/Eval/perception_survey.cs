using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Eval
{
    public class perception_survey : base_record_no_fs_req_brgy
    {
        [Key]
        public Guid perception_survey_id { get; set; }
        
        public int year { get; set; }
        public DateTime? talakayan_date_from { get; set; }
        public DateTime? talakayan_date_to { get; set; }

        public DateTime? survey_date_from { get; set; }
        public DateTime? survey_date_to { get; set; }
        public string activity_name { get; set; }

        public string person_name { get; set; }
        public int? age { get; set; }
        public bool? is_male { get; set; }
        public bool? is_ip { get; set; }        
        public bool? is_pantawid { get; set; }
        public bool? is_slp { get; set; }

        /// <summary>
        /// "Barangay residents believe that our local officials are working for the benefit of our barangay 2016 Version"
        /// </summary>
        public int? trust_1 { get; set; }
        public int? trust_2 { get; set; }
        public int? trust_3 { get; set; }
        public int? trust_4 { get; set; }
        public int? trust_5 { get; set; }
        public int? trust_6 { get; set; }
        public int? trust_7 { get; set; }
        public int? trust_8 { get; set; }
        public int? access_1 { get; set; }
        public int? access_2 { get; set; }
        public int? access_3 { get; set; }
        public int? access_4 { get; set; }
        public int? access_5 { get; set; }
        public int? access_6 { get; set; }
        public int? access_7 { get; set; }
        public int? access_8 { get; set; }
        public int? access_9 { get; set; }
        public int? access_10 { get; set; }
        public int? access_11 { get; set; }
        public int? access_12 { get; set; }
        public int? access_13 { get; set; }
        public int? access_14 { get; set; }
        public int? access_15 { get; set; }
        public int? access_16 { get; set; }
        public int? participation_1 { get; set; }
        public int? participation_2 { get; set; }
        public int? participation_3 { get; set; }
        public int? participation_4 { get; set; }
        public int? participation_5 { get; set; }
        public int? participation_6 { get; set; }
        public int? participation_7 { get; set; }
        public int? participation_8 { get; set; }
        public int? participation_9 { get; set; }
        public int? participation_10 { get; set; }
        public int? participation_11 { get; set; }
        public int? participation_12 { get; set; }
        public int? disaster_1 { get; set; }
        public int? disaster_2 { get; set; }
        public int? disaster_3 { get; set; }
        public int? disaster_4 { get; set; }
        public int? disaster_5 { get; set; }
        public int? disaster_6 { get; set; }
        public int? disaster_7 { get; set; }
        public int? disaster_8 { get; set; }
        public int? disaster_9 { get; set; }
        
        public int talakayan_yr_id { get; set; }        
        
    }


    public class base_record_no_fs_req_brgy
    {
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }

        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }


        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int brgy_code { get; set; }

        public int? cycle_id { get; set; }
        

        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool is_deleted { get; set; }
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
    }
}
