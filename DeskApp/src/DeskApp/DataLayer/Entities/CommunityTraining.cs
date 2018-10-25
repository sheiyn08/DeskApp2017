using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DeskApp.DataLayer
{
    public class community_training

    {
        [Key]
        public Guid community_training_id { get; set; }

        public string speaker { get; set; }
        //sectors

        public bool? is_sector_academe { get; set; }
        public bool? is_sector_business { get; set; }
        public bool? is_sector_pwd { get; set; }
        public bool? is_sector_farmer { get; set; }
        public bool? is_sector_fisherfolks { get; set; }
        public bool? is_sector_government { get; set; }
        public bool? is_sector_ip { get; set; }
        public bool? is_sector_ngo { get; set; }
        public bool? is_sector_po { get; set; }
        public bool? is_sector_religios { get; set; }
        public bool? is_sector_senior { get; set; }
        public bool? is_sector_women { get; set; }
        public bool? is_sector_youth { get; set; }
        
        public string old_id { get; set; }

        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        //RDR08242017 Additional columns for v3.0:
        public bool? is_lgu_led { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }

        public string training_title { get; set; }
        public int training_category_id { get; set; }
        public string venue { get; set; }
        public DateTime? date_conducted { get; set; }
        public int duration { get; set; }
        public int lgu_level_id { get; set; }

        public string reported_by { get; set; }

        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }

        public DateTime? plan_date_start { get; set; }
        public DateTime? plan_date_end { get; set; }

        public bool is_draft { get; set; }
        public int enrollment_id { get; set; }

        //RDR06042018 New columns for BAR training category (4.0 release)
        //Backlog item #786 - Do not require checking of participants, only input number of participants who attended
        public int? bar_participant_male { get; set; }
        public int? bar_participant_female { get; set; }
        public int? bar_participant_ip_male { get; set; }
        public int? bar_participant_ip_female { get; set; }
        public int? bar_participant_pantawid_male { get; set; }
        public int? bar_participant_pantawid_female { get; set; }
        public int? bar_participant_slp_male { get; set; }
        public int? bar_participant_slp_female { get; set; }

        [JsonIgnore]

        public virtual lib_training_category lib_training_category { get; set; }
        [JsonIgnore]

        public virtual lib_enrollment lib_enrollment { get; set; }
        [JsonIgnore]

        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]

        public virtual lib_cycle lib_cycle { get; set; }
        [JsonIgnore]

        public virtual lib_lgu_level lib_lgu_level { get; set; }

        public int? no_brgy_rep { get; set; }
        public int? no_atn_male { get; set; }
        public int? no_atn_female { get; set; }
        public int? no_ip_male { get; set; }
        public int? no_ip_female { get; set; }
        public int? no_atn_pantawid { get; set; }
        public int? no_atn_slp { get; set; }

        public bool? requirement_1 { get; set; }
        public bool? requirement_2 { get; set; }
        public bool? requirement_3 { get; set; }
        public bool? requirement_4 { get; set; }
        public bool? requirement_5 { get; set; }
        public bool? requirement_6 { get; set; }

        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }
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

        public int? last_sync_source_id { get; set; }
    }


    public class person_training
    {
        [Key]
        public Guid person_training_id { get; set; }
        public Guid person_profile_id { get; set; }
        [JsonIgnore]
        public virtual person_profile person_profile { get; set; }
        public Guid community_training_id { get; set; }
        [JsonIgnore]
        public virtual community_training community_training { get; set; }

        public bool? is_participant { get; set; }
         

        public bool? is_mdc { get; set; }
        public  bool? ig_bdc { get; set; }

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
        public virtual lib_push_status lib_push_status { get; set; }

        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        public virtual lib_approval lib_approval { get; set; }
        #endregion

 
    }


    public class community_training_facilitator
    {
        [Key]
        public Guid community_training_facilitator_id { get; set; }
        public Guid community_training_id { get; set; }
        public virtual community_training community_training { get; set; }


        public string name { get; set; }
        public string  designation { get; set; }
     
        public string topic { get; set; }


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
        public virtual lib_push_status lib_push_status { get; set; }

        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        public virtual lib_approval lib_approval { get; set; }
        #endregion
    }
}
