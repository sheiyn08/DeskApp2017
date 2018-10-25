using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DeskApp.DataLayer
{
    public class grievance_record
    {
        [Key]
        public Guid grievance_record_id { get; set; }

        public string old_id { get; set; }

        public int? pincos_id { get; set; }



        public Guid? activity_source_id { get; set; }
        public int? training_category_id { get; set; }

        public virtual lib_training_category lib_training_category { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }

        public int? fund_source_id { get; set; }
        public int? cycle_id { get; set; }

        //RDR08242017 Additional columns for v3.0:
        public bool? is_lgu_led { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }

        public bool? is_central_office_level_only { get; set; }
        public bool? is_field_office_level_only { get; set; }

        public bool? is_individual { get; set; }

        //RDR06042018 Additional columns to match WebApp (4.0 release)
        public bool? is_SharedRPMO { get; set; }
        public bool? is_SharedACT { get; set; }


        public int grs_form_id { get; set; }
        public int grs_intake_level_id { get; set; }

        //intake officer information

        public DateTime? date_intake { get; set; }
        public string intake_officer { get; set; }
        public string intake_officer_designation { get; set; }


        //sender indformation
        public int grs_filling_mode_id { get; set; }
        public string grs_filling_mode_others { get; set; }
        public int? ip_group_id { get; set; }

        public string ip_group_other { get; set; }
        public string sender_name { get; set; }
        public bool sender_sex { get; set; }
        public string sender_organization { get; set; }
        public string sender_designation { get; set; }

        public int? grs_sender_designation_id { get; set; }
        public string sender_designation_other { get; set; }
        public string sender_contact_info { get; set; }

        public int grs_resolution_status_id { get; set; }
        public DateTime? resolution_date { get; set; }
        public int? grs_feedback_id { get; set; }

        //part II Details of the Issue / Concern
        public int grs_nature_id { get; set; }
        public int grs_category_id { get; set; }

        public int grs_complaint_subject_id { get; set; }

        public string grs_complaint_subject_others { get; set; }

        public string details { get; set; }
        public string actions { get; set; }
        public string recommendations { get; set; }

        public int? enrollment_id { get; set; }


        public string email { get; set; }
        public string cellphone { get; set; }


        public bool? req_province { get; set; }
        public bool? req_city { get; set; }
        public bool? req_brgy { get; set; }

        public int sex_id { get; set; }
        public bool? is_ip { get; set; }
        public bool? is_anonymous { get; set; }
        [JsonIgnore]
        public virtual lib_sex lib_sex { get; set; }



        [JsonIgnore]
        public virtual lib_grs_intake_level lib_grs_intake_level { get; set; }
        [JsonIgnore]
        public virtual lib_grs_form lib_grs_form { get; set; }
        [JsonIgnore]
        public virtual lib_grs_filling_mode lib_grs_filling_mode { get; set; }
        [JsonIgnore]
        public virtual lib_grs_resolution_status lib_grs_resolution_status { get; set; }
        [JsonIgnore]
        public virtual lib_grs_feedback lib_grs_feedback { get; set; }
        [JsonIgnore]

        public virtual lib_grs_category lib_grs_category { get; set; }
        [JsonIgnore]
        public virtual lib_grs_complaint_subject lib_grs_complaint_subject { get; set; }
        [JsonIgnore]

        public virtual lib_grs_sender_designation lib_grs_sender_designation { get; set; }
        [JsonIgnore]
        public virtual lib_grs_nature lib_grs_nature { get; set; }
        [JsonIgnore]
        public virtual lib_ip_group lib_ip_group { get; set; }
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
        [JsonIgnore]

        public virtual lib_enrollment lib_enrollment { get; set; }

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

        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion

        public string final_resolution { get; set; }
        public int? grs_intake_officer_id { get; set; }
        [JsonIgnore]
        public virtual lib_grs_intake_officer lib_grs_intake_officer { get; set; }

        [JsonIgnore]
        public virtual ICollection<grievance_record_discussion> grievance_record_discussions { get; set; }


        public bool? is_fund_source_applicable { get; set; }
        public int? last_sync_source_id { get; set; }

        public int? grs_pincos_actor_id { get; set; }
        [JsonIgnore]
        public virtual lib_grs_pincos_actor lib_grs_pincos_actor { get; set; }
    }


    public class lib_grs_pincos_actor
    {
        [Key]
        public int grs_pincos_actor_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }

}
