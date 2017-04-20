using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskApp.DataLayer
{
    public class person_profile
    {
        public string other_training { get; set; }
        public string other_membership { get; set; }

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

        //   public int? person_identity_key { get; set; }

        [Key]
        public Guid person_profile_id { get; set; }
        public string old_id { get; set; }
        public string household_id { get; set; }
        public int? entry_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }
        public string old_address { get; set; }



        public DateTime? date_appointment { get; set; }

        public bool? is_bspmc { get; set; }
        public bool? is_mdc { get; set; }
        public bool? is_bdc { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public DateTime? birthdate { get; set; }
        public bool sex { get; set; }
        public int? civil_status_id { get; set; }
        public int? no_children { get; set; }

        public string contact_no { get; set; }

        public string sitio { get; set; }
        public bool? is_volunteer { get; set; }
        public bool? is_ip { get; set; }
        public bool? is_ipleader { get; set; }
        public bool? is_pantawid { get; set; }
        public bool? is_pantawid_leader { get; set; }
        public bool? is_slp { get; set; }
        public bool? is_slp_leader { get; set; }
        public bool? is_lguofficial { get; set; }
        public bool? is_worker { get; set; }



        public int? lgu_position_id { get; set; }
        public int? education_attainment_id { get; set; }
        public int? occupation_id { get; set; }
        //  public int? sector { get; set; }
        public string sector { get; set; }
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }

        [JsonIgnore]
        public virtual lib_lgu_position lib_blgu_position { get; set; }
        [JsonIgnore]
        public virtual lib_education_attainment lib_education_attainment { get; set; }
        [JsonIgnore]
        public virtual lib_occupation lib_occupation { get; set; }
        [JsonIgnore]
        public virtual lib_push_status lib_push_status { get; set; }
        [JsonIgnore]
        public virtual lib_civil_status lib_civil_status { get; set; }

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




        public int? ip_group_id { get; set; }
        [JsonIgnore]
        public virtual lib_ip_group lib_ip_group { get; set; }

        public int? last_sync_source_id { get; set; }

    }

    public class person_lgu_record
    {
        public Guid person_lgu_record_id { get; set; }

        public Guid person_profile_id { get; set; }
        public string position { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public string brgy_code { get; set; }
        public int lgu_level_id { get; set; }
        [JsonIgnore]
        public virtual person_profile person_profile { get; set; }
        [JsonIgnore]
        public virtual lib_lgu_level lib_lgu_level { get; set; }
    }


    public class person_sector
    {
        public Guid person_sector_id { get; set; }
        public int sector_id { get; set; }

        public Guid person_profile_id { get; set; }
        [JsonIgnore]
        public virtual lib_sector lib_sector { get; set; }
        [JsonIgnore]
        public virtual person_profile person_profile { get; set; }
    }


    public class person_ers_work
    {
        [Key]
        public Guid person_ers_work_id { get; set; }
        public string old_id { get; set; }


        public Guid sub_project_ers_id { get; set; }
        public Guid person_profile_id { get; set; }



        public decimal? percent { get; set; }
        public string unit_hauling { get; set; }


        public decimal? rate_hauling { get; set; }
        public decimal? work_hauling { get; set; }


        public decimal? rate_hour { get; set; }
        public decimal? rate_day { get; set; }
        public decimal? work_hours { get; set; }
        public decimal? work_days { get; set; }
        public decimal? plan_cash { get; set; }
        public decimal? plan_lcc { get; set; }
        public decimal? actual_cash { get; set; }
        public decimal? actual_lcc { get; set; }

        public int ers_current_work_id { get; set; }
        [JsonIgnore]
        public virtual lib_ers_current_work lib_ers_current_work { get; set; }
        [JsonIgnore]
        public virtual sub_project_ers sub_project_ers { get; set; }
        [JsonIgnore]
        public virtual person_profile person_profile { get; set; }

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


    public class sub_project_ers
    {
        [Key]
        public Guid sub_project_ers_id { get; set; }
        public string old_id { get; set; }
        public Guid? sub_project_unique_id { get; set; }
        public int? sub_project_id { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public string brgy_code { get; set; }



        public int ers_delivery_mode_id { get; set; }

        public DateTime? date_reported { get; set; }
        public DateTime? date_started { get; set; }
        public DateTime? date_ended { get; set; }


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

        [JsonIgnore]
        public virtual lib_ers_delivery_mode lib_ers_delivery_mode { get; set; }
    }


    public class sub_project_spcr
    {
        [Key]
        public Guid sub_project_spcr_id { get; set; }
        /// <summary>
        /// Old Sub Project Id, mus be matched with SPI Profile to get Fund Source and Cycle, Brgy, ETC
        /// </summary>
        public string old_id { get; set; }


        public int? sub_project_id { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }

        public int city_code { get; set; }

        public string brgy_code { get; set; }

        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        public int enrollment_id { get; set; }

        public DateTime? date_csw { get; set; }
        public DateTime? date_prioritization { get; set; }
        public DateTime? date_inaugurate { get; set; }
        public int? no_households { get; set; }
        public int? no_families { get; set; }
        public int? no_pantawid_hh { get; set; }
        public int? no_pantawid_fm { get; set; }
        public float? phys_actual { get; set; }
        public float? total_cost { get; set; }
        public float? total_direct { get; set; }
        public float? total_indirect { get; set; }
        public int? no_male_pop { get; set; }
        public int? no_female_pop { get; set; }
        public int? no_male_serve { get; set; }
        public int? no_female_serve { get; set; }
        public int? no_male_ip { get; set; }
        public int? no_female_ip { get; set; }
        public int? no_slp_hh { get; set; }
        public int? no_slp_family { get; set; }
        public int? no_ip_hh { get; set; }
        public int? no_ip_hh_families { get; set; }
        public int procurement_mode_id { get; set; }
        public float? total_counterpart { get; set; }
        public int? no_skilled_male { get; set; }
        public int? no_skilled_female { get; set; }
        public int? no_unskilled_male { get; set; }
        public int? no_unskilled_female { get; set; }
        public int? days_skilled_male { get; set; }
        public int? days_skilled_female { get; set; }
        public int? days_unskilled_male { get; set; }
        public int? days_unskilled_female { get; set; }
        public float? rate_skilled_male { get; set; }
        public float? rate_skilled_female { get; set; }
        public float? rate_unskilled_male { get; set; }
        public float? rate_unskilled_female { get; set; }
        public int completion_status_id { get; set; }


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
        [JsonIgnore]
        public virtual lib_procurement_mode lib_procurement_mode { get; set; }
        [JsonIgnore]
        public virtual lib_completion_status lib_completion_status { get; set; }
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
    }


    public class lib_procurement_mode
    {
        [Key]
        public int procurement_mode_id { get; set; }
        public string name { get; set; }
    }

    public class lib_completion_status
    {
        [Key]
        public int completion_status_id { get; set; }
        public string name { get; set; }
    }
    public class lib_ers_delivery_mode
    {
        [Key]
        public int ers_delivery_mode_id { get; set; }
        public string name { get; set; }
    }


}

