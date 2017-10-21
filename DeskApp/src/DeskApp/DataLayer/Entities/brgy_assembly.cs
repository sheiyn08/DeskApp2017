using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DeskApp.DataLayer
{
    public class brgy_assembly

    {

        public string venue { get; set; }

        public string ip_leader { get; set; }
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
        [Key]
        public Guid brgy_assembly_id { get; set; }

        public string old_id { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int  brgy_code { get; set; }

        //RDR08242017 Additional columns for v3.0:
        public bool? is_lgu_led { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }

        public string highlights { get; set; }
        public int barangay_assembly_purpose_id { get; set; }

        public int enrollment_id { get; set; }

        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }


        public DateTime? plan_date_start { get; set; }
        public DateTime? plan_date_end { get; set; }




        public int? no_families { get; set; }
        public int? no_household { get; set; }

        public int? no_atn_male { get; set; }
        public int? no_atn_female { get; set; }
        public int? no_ip_male { get; set; }
        public int? no_ip_female { get; set; }
        public int? no_old_male { get; set; }
        public int? no_old_female { get; set; }
        public int? no_pantawid_household { get; set; }
   

        public int? no_slp_household { get; set; }



        public int? no_slp_family { get; set; }
        public int? no_pantawid_family { get; set; }
        public int? no_ip_family { get; set; }




        public int? no_slp_family_in_barangay { get; set; }
        public int? no_pantawid_family_in_barangay { get; set; }    
        public int? no_ip_family_in_barangay { get; set; }




        public int? no_ip_household { get; set; }
     

        public int? total_household_in_barangay { get; set; }

        public int? total_household_ip_in_barangay { get; set; }
        public int? total_household_pantawid_in_barangay { get; set; }
        public int? total_household_slp_in_barangay { get; set; }


        public int? total_families_in_barangay { get; set; }


        public int? no_lgu_male { get; set; }
        public int? no_lgu_female { get; set; }

        [JsonIgnore]
        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
        [JsonIgnore]
        public virtual lib_barangay_assembly_purpose lib_barangay_assembly_purpose { get; set; }
        [JsonIgnore]
        public virtual lib_enrollment lib_enrollment { get; set; }
        [JsonIgnore]
        public virtual lib_push_status lib_push_status { get; set; }

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


        public bool? requirement_1 { get; set; }
        public bool? requirement_2 { get; set; }
        public bool? requirement_3 { get; set; }
        public bool? requirement_4 { get; set; }
        public bool? requirement_5 { get; set; }
        public bool? requirement_6 { get; set; }

        //public bool? first_ba_grs_committee_formed { get; set; } requirement 1
        //public bool? first_ba_psa_volunteers_formed { get; set; }requirement 2
        //public bool? first_ba_resolution_support { get; set; } requirement 3


        //public bool? second_ba_brgy_rep_team_formed { get; set; } requirement 1
        //public bool? second_ba_proj_prep_team_formed { get; set; } requirement 2
        //public bool? second_ba_community_mon_team_formed { get; set; }requirement 3
        //public bool? second_ba_reso_adopting_psa_results { get; set; } requirement 4

        //public bool? third_ba_reso_prio { get; set; } requirement 1
        //public bool? third_ba_approved_cm_plan { get; set; } requirement 2


        //public bool? fourth_ba_reso_mibf { get; set; } requirement 1
        //public bool? fourth_ba_complete_rfr_doc { get; set; } requirement 2

        //public bool? fifth_ba_sp_management { get; set; } requirement 1
        //public bool? fifth_ba_bac_formed { get; set; } requirement 2
        //public bool? fifth_ba_audit_comm { get; set; } requirement 3
        //public bool? fifth_ba_brgy_comm_formation { get; set; } requirement 4
        //public bool? fifth_ba_brgy_bank_accounts { get; set; } requirement 5
        //public bool? fifth_ba_cmi_action_plan { get; set; } requirement 6
    }


    public class brgy_assembly_ip
    {
        [Key]
        public Guid brgy_assembly_ip_id { get; set; }
        public Guid brgy_assembly_id { get; set; }
        public int ip_group_id { get; set; }
        public string ip_group_name { get; set; }
        public bool Selected { get; set; }

        [JsonIgnore]
        public virtual brgy_assembly brgy_assembly { get; set; }
        [JsonIgnore]
        public virtual lib_ip_group lib_ip_group { get; set; }


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
