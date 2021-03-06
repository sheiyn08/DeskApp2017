﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    /// ---------------------------------- Table name : SUB_PROJECT ---------------------------------- ///    
    public class sub_project
    {
        [JsonIgnore]
        public virtual lib_region lib_regions { get; set; }
        [JsonIgnore]
        public virtual lib_project_type lib_project_type { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_provinces { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_cities { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }
        [JsonIgnore]
        public virtual lib_physical_status lib_physical_status { get; set; }
        [JsonIgnore]
        public virtual lib_physical_status_category lib_physical_status_category { get; set; }
        
        public string maintainance_list { get; set; }
        public string damage_list { get; set; }
        public string pow_list { get; set; }
        public string suspension_order_list { get; set; }
        public string resume_order_list { get; set; }
        public string variation_order_list { get; set; }
        public string community_formation_list { get; set; }

        //CATD
        public bool? with_cadt { get; set; }
        public bool? on_process_cadt { get; set; }
        public bool? cadteable { get; set; }
        public bool? ncip_submitted { get; set; }
        public bool? ncip_validated { get; set; }
        public int? cadt_no_ips { get; set; }
        public DateTime? ncip_validated_date { get; set; }
        public DateTime? ncip_submitted_date { get; set; }
        //validation status: additional field: 11-02-2016
        public bool? with_cert_precondition { get; set; }
        public bool? with_cert_non_overlap { get; set; }
        public bool? validation_status_others { get; set; }
        public string validation_status_others_input { get; set; }
        //CDADT
        
        [Key]
        public Guid sub_project_unique_id { get; set; }
        public int? push_status_id { get; set; }
        public int? last_modified_by { get; set; }
        public bool? is_pulled_from_pra { get; set; }
        public bool? with_tariff { get; set; }
        public DateTime? date_start_physical_actual { get; set; }
        public DateTime? date_start_financial_actual { get; set; }
        public DateTime? date_start_physical_planned { get; set; }
        public DateTime? date_start_financial_planned { get; set; }

        /// SET INFORMATION
        public DateTime? set_date_eval { get; set; }
        public string set_physical_description { get; set; }
        public int? set_mode { get; set; }
        public double? set_sp_utilization { get; set; }
        public double? set_organization { get; set; }
        public double? set_institutional_linkage { get; set; }
        public double? set_financial { get; set; }
        public double? set_physical { get; set; }
        public double? set_onm_group { get; set; }
        public double? set_total_score { get; set; }
        /// SET INFORMATION
        
        public DateTime? erfr_mibf_date { get; set; }
        public double? erfr_mibf_grant_cost { get; set; }
        public double? erfr_mibf_lcc_cost { get; set; }
        public string erfr_mibf_project_name { get; set; }
        public DateTime? pims_mibf_date { get; set; }
        public double? pims_mibf_grant_cost { get; set; }
        public double? pims_mibf_lcc_cost { get; set; }
        public string pims_mibf_project_mame { get; set; }
        public Guid? community_training_id { get; set; }
        public Guid mibf_prioritization_id { get; set; }
        
        /// SPCF
        public int? no_target_families { get; set; }
        public int? no_target_pantawid_families { get; set; }
        public int? no_target_slp_families { get; set; }
        public int? no_target_ip_families { get; set; }
        public int? no_target_pwd_families { get; set; }
        public int? no_target_senior_families { get; set; }
        public int? no_actual_families { get; set; }
        public int? no_actual_pantawid_families { get; set; }
        public int? no_actual_slp_families { get; set; }
        public int? no_actual_ip_families { get; set; }
        public int? no_actual_pwd_families { get; set; }
        public int? no_actual_senior_families { get; set; }
        public int? no_target_households { get; set; }
        public int? no_target_pantawid_households { get; set; }
        public int? no_target_slp_households { get; set; }
        public int? no_target_ip_households { get; set; }
        public int? no_target_pwd_households { get; set; }
        public int? no_target_senior_households { get; set; }
        public int? no_actual_households { get; set; }
        public int? no_actual_pantawid_households { get; set; }
        public int? no_actual_slp_households { get; set; }
        public int? no_actual_ip_households { get; set; }
        public int? no_actual_pwd_households { get; set; }
        public int? no_actual_senior_households { get; set; }
        public int? no_target_male { get; set; }
        public int? no_target_female { get; set; }
        public int? no_actual_male { get; set; }
        public int? no_actual_female { get; set; }
        /// SPCR

        public string path { get; set; }
        public bool? is_public_school_for_ip { get; set; }
        public bool? is_kalahi_funded { get; set; }
        public bool? is_deped_funded { get; set; }
        public string dep_ed_amount { get; set; }
        //May 30, 2017 add column to match column in geotagging (replacement for dep_ed_amount string)
        public decimal? _dep_ed_amount { get; set; }
        public string ip_groups { get; set; }
        public bool? has_variation { get; set; }
        public bool? phy_has_construction_target { get; set; }
        public bool? phy_has_improvement_target { get; set; }
        public bool? phy_has_rehabilitation_target { get; set; }
        public bool? phy_has_purchase_target { get; set; }
        public int? movement_id { get; set; }
        public bool? has_t1 { get; set; }
        public bool? has_t2 { get; set; }
        public bool? has_t3 { get; set; }

       
        //erfr
        public decimal? erfr_t1 { get; set; }
        public decimal? erfr_t2 { get; set; }
        public decimal? erfr_t3 { get; set; }
        public int? erfr_id_t1 { get; set; }
        public int? erfr_id_t2 { get; set; }
        public int? erfr_id_t3 { get; set; }
        public bool? phy_has_construction_target_secondary { get; set; }
        public bool? phy_has_improvement_target_secondary { get; set; }
        public bool? phy_has_rehabilitation_target_secondary { get; set; }
        public bool? phy_has_purchase_target_secondary { get; set; }
        public decimal? phy_construction_target { get; set; }
        public decimal? phy_improvement_target { get; set; }
        public decimal? phy_rehabilitation_target { get; set; }
        public decimal? phy_purchase_target { get; set; }
        public decimal? phy_construction_actual { get; set; }
        public decimal? phy_improvement_actual { get; set; }
        public decimal? phy_rehabilitation_actual { get; set; }
        public decimal? phy_purchase_actual { get; set; }
        public decimal? phy_construction_target_secondary { get; set; }
        public decimal? phy_improvement_target_secondary { get; set; }
        public decimal? phy_rehabilitation_target_secondary { get; set; }
        public decimal? phy_purchase_target_secondary { get; set; }
        public decimal? phy_construction_actual_secondary { get; set; }
        public decimal? phy_improvement_actual_secondary { get; set; }
        public decimal? phy_rehabilitation_actual_secondary { get; set; }
        public decimal? phy_purchase_actual_secondary { get; set; }
        public bool? is_correct_sp_id { get; set; }
        public bool? has_local_counterpart { get; set; }

        #region correct
        public string s_phy_construction_target_secondary { get; set; }
        public string s_phy_improvement_target_secondary { get; set; }
        public string s_phy_rehabilitation_target_secondary { get; set; }
        public string s_phy_purchase_target_secondary { get; set; }
        public string s_phy_construction_actual_secondary { get; set; }
        public string s_phy_improvement_actual_secondary { get; set; }
        public string s_phy_rehabilitation_actual_secondary { get; set; }
        public string s_phy_purchase_actual_secondary { get; set; }
        public string s_phy_construction_target { get; set; }
        public string s_phy_improvement_target { get; set; }
        public string s_phy_rehabilitation_target { get; set; }
        public string s_phy_purchase_target { get; set; }
        public string s_phy_construction_actual { get; set; }
        public string s_phy_improvement_actual { get; set; }
        public string s_phy_rehabilitation_actual { get; set; }
        public string s_phy_purchase_actual { get; set; }
        #endregion
        
        #region Original Content        
        public int sub_project_id { get; set; }
        public int EngineeringMigrationId { get; set; }
        [Display(Name = "Modality")]
        public int modality_id { get; set; }
        public long? ID { get; set; }
        public Nullable<long> ReportID { get; set; }
        public Nullable<long> Month { get; set; }
        public Nullable<long> Year { get; set; }
        public int cycle_id { get; set; }
        [Required]
        public int region_code { get; set; }
        [Required]
        public int prov_code { get; set; }
        [Required]
        public int city_code { get; set; }
        [Required]
        public int brgy_code { get; set; }
        [Required]
        [Display(Name = "Sub Project Name")]
        public string sub_project_name { get; set; }
        public Nullable<long> NoOfBgys { get; set; }
        [Display(Name = "No. of Household Beneficiaries")]
        public Nullable<long> NoOfHH { get; set; }
        [Display(Name = "Project Type")]
        [Required]
        public int project_type_id { get; set; }
        public Nullable<double> Physical_Target { get; set; }
        public string Phy_Target_Unit { get; set; }
        public Nullable<double> Kalahi_Amount { get; set; }
        public Nullable<double> LCC_Amount { get; set; }
        public Nullable<double> Total_Amount { get; set; }
        public Nullable<System.DateTime> Date_Started { get; set; }
        public Nullable<System.DateTime> Date_of_Completion { get; set; }
        public Nullable<double> ProjectDuration { get; set; }
        public Nullable<double> Phy_Perc_Previous { get; set; }
        public Nullable<double> Phy_Perc_To_Date { get; set; }
        public Nullable<double> Phy_Perc_This_Month { get; set; }
        public Nullable<double> KC_Fund_Released { get; set; }
        public Nullable<double> Balance { get; set; }
        public string Remarks { get; set; }
        public string Tranche { get; set; }
        public string SchoolType { get; set; }
        public Nullable<long> NoOfClassroom { get; set; }
        public Nullable<long> WaterSystemType { get; set; }
        public Nullable<double> Tranche1amount { get; set; }
        public Nullable<double> Tranche2amount { get; set; }
        public Nullable<double> Tranche3amount { get; set; }
        public Nullable<System.DateTime> Tranche1date { get; set; }
        public Nullable<System.DateTime> Tranche2date { get; set; }
        public Nullable<System.DateTime> Tranche3date { get; set; }
        //Tranche1amount_utilized ... Tranche3amount_utilized, Totaltrancheamount_utilized added 10/27/2016
        public Nullable<double> Tranche1amount_utilized { get; set; }
        public Nullable<double> Tranche2amount_utilized { get; set; }
        public Nullable<double> Tranche3amount_utilized { get; set; }
        public Nullable<double> Tranche1Perc { get; set; }
        public Nullable<double> Tranche2Perc { get; set; }
        public Nullable<double> Tranche3Perc { get; set; }
        public Nullable<double> Tranche1Phy { get; set; }
        public Nullable<double> Tranche2Phy { get; set; }
        public Nullable<double> Tranche3Phy { get; set; }
        public Nullable<double> Totaltrancheamount { get; set; }
        public Nullable<double> Totaltrancheamount_utilized { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<double> Tranche1Amt_Old { get; set; }
        public Nullable<double> Tranche2Amt_Old { get; set; }
        public Nullable<double> Tranche3Amt_Old { get; set; }
        public Nullable<double> KalahiAmt_Old { get; set; }
        public Nullable<double> LCCAmt_Old { get; set; }
        public Nullable<long> Tranch1Flag { get; set; }
        public Nullable<long> NoOfTapstands { get; set; }
        public Nullable<long> NoOfHPumps { get; set; }
        public Nullable<long> WS_wd_Sanitation { get; set; }
        public Nullable<long> Delete_Ind { get; set; }
        public Nullable<long> Add_Ind { get; set; }
        public Nullable<long> Edit_Ind { get; set; }
        public Nullable<long> RFR_Ref_No { get; set; }
        public int physical_status_id { get; set; }
        public Nullable<long> BalanceNotReq { get; set; }
        public Nullable<double> KC_Adj { get; set; }
        public Nullable<double> LCC_Adj { get; set; }
        public Nullable<double> TPC_Adj { get; set; }
        public Nullable<double> ExcessFund { get; set; }
        public Nullable<double> SkilledLaborCost { get; set; }
        public Nullable<double> UnskilledLaborCost { get; set; }
        public Nullable<long> Vdr_Update { get; set; }
        public Nullable<long> RFR_Update { get; set; }
        public Nullable<System.DateTime> DateExtracted { get; set; }
        public Nullable<System.DateTime> PlannedDate_Completion { get; set; }
        public Nullable<long> LandOwnership { get; set; }
        public Nullable<double> PamanaGrant { get; set; }
        public Nullable<long> OldSP_ID { get; set; }
        public string project_sequence { get; set; }
        public int? fund_source_id { get; set; }
        
        //Added June 3 2015
        public int? modality_category_id { get; set; } //--ncddp_groupings in excel table
        public int? erfr_project_id { get; set; }        
        public bool? is_created_via_migration { get; set; } //--determine if project was added via migration or via manual encoding
        
        /// June 25 
        public double? operation_maintainance_cost { get; set; }
        public int? approval_id { get; set; }
        public int? photos { get; set; }
        public int? geotagged_co_approved_1 { get; set; }
        public int? geotagged_co_disapproved_5 { get; set; }
        public int? geotagged_fo_approved_2 { get; set; }
        public int? geotagged_fo_disapproved_4 { get; set; }
        public int? geotagged_srpmo_approved_3 { get; set; }
        public int? geotagged_srpmo_disapproved_6 { get; set; }
        public int? geotagged_act_uploaded_7 { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool? is_duplicate { get; set; }
        public bool? is_updated { get; set; }
        public int? physical_status_category_id { get; set; }



        #endregion
                
        #region Auditing
        public bool? IsActive { get; set; }
        public string deleted_by { get; set; }

        public string deleted_reason { get; set; }
        public DateTime? deleted_date { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }

        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        #endregion
        
        #region GIG Requirement
        public int? target_male { get; set; }
        public int? target_female { get; set; }
        public int? actual_male { get; set; }
        public int? actual_female { get; set; }
        public int? training_category_id { get; set; }
        #endregion

        #region Additional Fields
        //11-6-2015
        public long? NoOfHHActual { get; set; }
        public int? enrollment_type_id { get; set; }
        //11-13-2015
        public bool? has_ip_presence { get; set; }
        public int? ancestral_domain_status_id { get; set; }
        public bool? has_sc_esmp { get; set; }
        public bool? has_sc_ecc { get; set; }
        public bool? has_sc_cnc { get; set; }
        public bool? has_sc_cp { get; set; }
        public bool? has_sc_cno { get; set; }
        //11-02-2016
        public bool? has_sc_essc { get; set; }
        public bool? within_env_crit_area { get; set; }
        public int? land_acquisition_id { get; set; }
        public string other_land_acquisition { get; set; }
        //01-12-2015
        public bool? has_after_photo { get; set; }
        public bool? has_before_photo { get; set; }
        public bool? has_scanned_spcr { get; set; }
        public bool? has_turnover_certificate { get; set; }
        public bool? has_marker { get; set; }
        public bool? has_set { get; set; }
        public double? has_set_score { get; set; }
        //april 
        public bool? has_onm_group { get; set; }
        public bool? has_closed_account { get; set; }
        //01-28-no_year
        public bool? land_aq_deed_of_donation { get; set; }
        public bool? land_aq_unsfruct { get; set; }
        public bool? land_aq_blgu_resolution { get; set; }
        public bool? land_aq_deped_certification { get; set; }
        public bool? land_aq_other { get; set; }
        //11-02-2016 Land Acquisition (Private Land)
        public bool? land_aq_private_right_of_way_agreement { get; set; }
        public bool? land_aq_private_permit_to_construct { get; set; }
        public bool? land_aq_private_quit_claim { get; set; }
        //11-02-2016 Land Acquisition (Public Land)
        public bool? land_aq_permit { get; set; }
        public string land_aq_permit_issued_by { get; set; }
        //revised land ownership
        public bool? land_ownership_dod { get; set; }
        public bool? land_ownership_gov { get; set; }
        public bool? land_ownership_public_lands { get; set; }
        public bool? land_ownership_private_untitled { get; set; }
        public bool? land_ownership_private_titled { get; set; }
        public string bub_unique_id { get; set; }
        #endregion

        #region BUB Additional Field
        public int? no_pantawid_families { get; set; }
        public int? no_non_pantawid_families { get; set; }
        public double? actual_breakdown { get; set; }
        public string mode_of_implementation { get; set; }
        public int? year_of_implementation { get; set; }
        public string dbm_project_name { get; set; }
        public string lgu_engagement { get; set; }
        public string rfr_status { get; set; }
        #endregion
        
        #region May 30, 2017 Additional field: Enhancements
        public bool? is_lcc { get; set; }
        public int? replaced_sub_project_id { get; set; }
        public int? mode_id { get; set; }
        public bool? is_incentive { get; set; }   //--- column for is tier 2?
        public bool? is_savings { get; set; }
        public bool? is_lgu_led { get; set; }
        public bool? is_taf { get; set; } //--------- for v3.0 additional column
        public bool? is_multiple_sps { get; set; }
        public decimal? target_tranching_first { get; set; }
        public decimal? target_tranching_second { get; set; }
        public decimal? target_tranching_third { get; set; }
        public bool? is_sp_functional { get; set; }
        public bool? is_enhancement_functionality { get; set; }
        public int? variation_physical_status_id { get; set; }
        public DateTime? variation_date_started { get; set; }
        public double? variation_phy_perc_to_date { get; set; }
        public DateTime? variation_target_date_completion { get; set; }
        public DateTime? variation_actual_date_completion { get; set; }
        public bool? has_cadt { get; set; }
        public bool? has_on_process { get; set; }
        public bool? has_cadteable { get; set; }
        public bool? has_ncip { get; set; }
        public DateTime? ncip_date { get; set; }
        public bool? has_validation_conducted { get; set; }
        public DateTime? validation_conducted_date { get; set; }
        public DateTime? sc_cp_date { get; set; }
        public DateTime? sc_cno_date { get; set; }
        public bool? right_of_way_agreement { get; set; }
        public bool? permit_to_construct_enter { get; set; }
        public bool? quit_claim { get; set; }

        #endregion

        #region Sept. 14, 2017 additional columns given by Chervin (for v3.0)
        public bool? is_eligible_phy_second_tranche { get; set; }
        public bool? is_eligible_phy_third_tranche { get; set; }
        public bool? is_eligible_fin_second_tranche { get; set; }
        public bool? is_eligible_fin_third_tranche { get; set; }
        #endregion

        #region June 16, 2018 additional column to match Geotagging (4.0 release)
        public string sitio { get; set; }
        #endregion

    }

    /// ---------------------------------- Table name : SUB_PROJECT_COVERAGE ---------------------------------- ///  
    public class sub_project_coverage
    {
        [Key]
        public int id { get; set; }
        public int sub_project_id { get; set; }
        public Guid? sub_project_unique_id { get; set; }
        public int brgy_code { get; set; }
        public string brgy_name { get; set; }
        public bool Selected { get; set; }
        public int push_status_id { get; set; }
        public virtual sub_project sub_project { get; set; }
        public virtual lib_brgy lib_brgy { get; set; }
    }

    /// ---------------------------------- Table name : SPPHOTO ---------------------------------- ///  
    public class SPPhoto
    {
        public Guid? sub_project_unique_id { get; set; }
        public int? sequence_id { get; set; }
        public Guid UniqueName { get; set; }
        public int sub_project_id { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public Nullable<double> Latitude { get; set; }

        [Required]
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Altitude { get; set; }
        public string GpsDateTimeStamp { get; set; }
        public string GetDateTaken { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public int? reject_id { get; set; }
        public int approval_id { get; set; }
        public string Comment { get; set; }
        public int lib_functionality_id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? photo_description_id { get; set; }
        public int? photo_description_id_new { get; set; }
        public int? photo_position_id { get; set; }
        public int? geo_category_id { get; set; }
        public string act_approved { get; set; }
        public DateTime? act_last_approved { get; set; }
        public string srpmo_approved { get; set; }
        public DateTime? srpmo_last_approved { get; set; }
        public string rme_approved { get; set; }
        public DateTime? rme_last_approved { get; set; }
        public string co_approved { get; set; }
        public DateTime? co_last_approved { get; set; }
        public bool IsOtherTypeOfProject { get; set; }
        public int? other_project_type_id { get; set; }

        //06-06-2015 to determine stage of photo when uploaded
        public int? album_id { get; set; }
        public int? Year { get; set; }
        public string Month { get; set; }
        public int? Day { get; set; }
        public bool? is_deleted { get; set; }
        public string deleted_by { get; set; }
        public bool? has_backup { get; set; }
        public bool? is_missing_photo { get; set; }
    }

    /// ---------------------------------- Table name : SUB_PROJECT_SET 02-28-2017 ---------------------------------- ///
    public class sub_project_set
    {
        [Key]
        public Guid sub_project_set_id { get; set; }
        public string old_id { get; set; }
        public Guid? sub_project_unique_id { get; set; }
        public int? sub_project_id { get; set; }
        public DateTime? set_date_eval { get; set; }
        public string set_physical_description { get; set; }
        public double? set_sp_utilization { get; set; }
        public double? set_organization { get; set; }
        public double? set_institutional_linkage { get; set; }
        public double? set_financial { get; set; }
        public double? set_physical { get; set; }
        public double? set_total_score { get; set; }

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
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        #endregion
    }

    /// ---------------------------------- Table name : SUB_PROJECT_SPCF 09-xx-2017 ---------------------------------- ///
    public class sub_project_spcf
    {
        [Key]
        public Guid sub_project_spcf_id { get; set; }
        public Guid? sub_project_unique_id { get; set; }
        public int? sub_project_id { get; set; }
        public decimal? pow_cdd_cost { get; set; }
        public decimal? pow_male_cost { get; set; }
        public decimal? pow_female_cost { get; set; }
        public decimal? pow_blgu_cost { get; set; }
        public decimal? pow_mlgu_cost { get; set; }
        public decimal? pow_plgu_cost { get; set; }
        public decimal? pow_others_cost { get; set; }
        public decimal? training_cdd_cost { get; set; }
        public decimal? training_male_cost { get; set; }
        public decimal? training_female_cost { get; set; }
        public decimal? training_blgu_cost { get; set; }
        public decimal? training_mlgu_cost { get; set; }
        public decimal? training_plgu_cost { get; set; }
        public decimal? training_others_cost { get; set; }
        public decimal? women_cdd_cost { get; set; }
        public decimal? women_male_cost { get; set; }
        public decimal? women_female_cost { get; set; }
        public decimal? women_blgu_cost { get; set; }
        public decimal? women_mlgu_cost { get; set; }
        public decimal? women_plgu_cost { get; set; }
        public decimal? women_others_cost { get; set; }
        public decimal? management_cdd_cost { get; set; }
        public decimal? management_male_cost { get; set; }
        public decimal? management_female_cost { get; set; }
        public decimal? management_blgu_cost { get; set; }
        public decimal? management_mlgu_cost { get; set; }
        public decimal? management_plgu_cost { get; set; }
        public decimal? management_others_cost { get; set; }
        public decimal? others_cdd_cost { get; set; }
        public decimal? others_male_cost { get; set; }
        public decimal? others_female_cost { get; set; }
        public decimal? others_blgu_cost { get; set; }
        public decimal? others_mlgu_cost { get; set; }
        public decimal? others_plgu_cost { get; set; }
        public decimal? others_others_cost { get; set; }

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
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        #endregion
    }

    /// ---------------------------------- Table name : SUB_PROJECT_REFERENCE_TABLE 09-26-2017 ---------------------------------- ///
    public class sub_project_reference_table
    {
        //basic information:
        [Key]
        public int sub_project_id { get; set; }
        public Guid? sub_project_unique_id { get; set; }
        public int? erfr_project_id { get; set; }
        public string sub_project_name { get; set; }
        public int project_type_id { get; set; }
        public bool? is_public_school_for_ip { get; set; }
        public int? training_category_id { get; set; }
        public int modality_id { get; set; }
        public int? fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public bool? is_incentive { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_lgu_led { get; set; }
        public bool? is_taf { get; set; }
        public int? mode_id { get; set; }
        public int? enrollment_type_id { get; set; }
        public int? modality_category_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int brgy_code { get; set; }

        //sub_project_covered_brgys : another table

        //financial information
        public Nullable<double> Kalahi_Amount { get; set; }
        public Nullable<double> Tranche1amount { get; set; }
        public Nullable<double> Tranche2amount { get; set; }
        public Nullable<double> Tranche3amount { get; set; }
        public bool? has_local_counterpart { get; set; }
        public bool? is_lcc { get; set; }
        public Nullable<double> LCC_Amount { get; set; }
        public Nullable<double> LCC_Adj { get; set; }
        public double? operation_maintainance_cost { get; set; }
        public bool? is_multiple_sps { get; set; }

        //multiple SPs : another table

        //BTF data / Target Tranching
        public decimal? target_tranching_first { get; set; }
        public decimal? target_tranching_second { get; set; }
        public decimal? target_tranching_third { get; set; }
        public bool? has_t1 { get; set; }
        public bool? has_t2 { get; set; }
        public bool? has_t3 { get; set; }
        public DateTime? Tranche1date { get; set; }
        public DateTime? Tranche2date { get; set; }
        public DateTime? Tranche3date { get; set; }
        public decimal? erfr_t1 { get; set; }
        public decimal? erfr_t2 { get; set; }
        public decimal? erfr_t3 { get; set; }
        public Nullable<double> Tranche1amount_utilized { get; set; }
        public Nullable<double> Tranche2amount_utilized { get; set; }
        public Nullable<double> Tranche3amount_utilized { get; set; }

        //auto-generated from BTF : from diff. table yata?

        //hidden:
        public decimal? _dep_ed_amount { get; set; }
        public bool? is_deped_funded { get; set; }

        //Physical Progress Information : Original POW
        public int physical_status_id { get; set; }
        public int? replaced_sub_project_id { get; set; }
        public Nullable<System.DateTime> Date_Started { get; set; }
        public Nullable<double> Phy_Perc_To_Date { get; set; }
        public Nullable<System.DateTime> PlannedDate_Completion { get; set; }
        public Nullable<System.DateTime> Date_of_Completion { get; set; }

        //Physical Progress Information : Variation Order
        public bool? has_variation { get; set; }
        public bool? is_sp_functional { get; set; }
        public bool? is_enhancement_functionality { get; set; }
        //variation : multiple (date approved, multiple)
        public int? variation_physical_status_id { get; set; }
        public DateTime? variation_date_started { get; set; }
        public double? variation_phy_perc_to_date { get; set; }
        public DateTime? variation_target_date_completion { get; set; }
        public DateTime? variation_actual_date_completion { get; set; }

        //Physical Targets:
        public bool? phy_has_construction_target { get; set; }
        public bool? phy_has_improvement_target { get; set; }
        public bool? phy_has_rehabilitation_target { get; set; }
        public bool? phy_has_purchase_target { get; set; }
        public bool? phy_has_construction_target_secondary { get; set; }
        public bool? phy_has_improvement_target_secondary { get; set; }
        public bool? phy_has_rehabilitation_target_secondary { get; set; }
        public bool? phy_has_purchase_target_secondary { get; set; }
        public decimal? phy_construction_target { get; set; }
        public decimal? phy_improvement_target { get; set; }
        public decimal? phy_rehabilitation_target { get; set; }
        public decimal? phy_purchase_target { get; set; } //---not used in view
        public string s_phy_purchase_target { get; set; }
        public decimal? phy_construction_target_secondary { get; set; }
        public decimal? phy_improvement_target_secondary { get; set; }
        public decimal? phy_rehabilitation_target_secondary { get; set; }
        public decimal? phy_purchase_target_secondary { get; set; }
        public decimal? phy_construction_actual { get; set; }
        public decimal? phy_improvement_actual { get; set; }
        public decimal? phy_rehabilitation_actual { get; set; }
        public decimal? phy_purchase_actual { get; set; } //---not used in view
        public string s_phy_purchase_actual { get; set; }
        public decimal? phy_construction_actual_secondary { get; set; }
        public decimal? phy_improvement_actual_secondary { get; set; }
        public decimal? phy_rehabilitation_actual_secondary { get; set; }
        public decimal? phy_purchase_actual_secondary { get; set; }

        #region correct (not sure)
        public string s_phy_construction_target_secondary { get; set; }
        public string s_phy_improvement_target_secondary { get; set; }
        public string s_phy_rehabilitation_target_secondary { get; set; }
        public string s_phy_purchase_target_secondary { get; set; }
        public string s_phy_construction_actual_secondary { get; set; }
        public string s_phy_improvement_actual_secondary { get; set; }
        public string s_phy_rehabilitation_actual_secondary { get; set; }
        public string s_phy_purchase_actual_secondary { get; set; }
        public string s_phy_construction_target { get; set; }
        public string s_phy_improvement_target { get; set; }
        public string s_phy_rehabilitation_target { get; set; }
        //public string s_phy_purchase_target { get; set; } -- because already used in view
        public string s_phy_construction_actual { get; set; }
        public string s_phy_improvement_actual { get; set; }
        public string s_phy_rehabilitation_actual { get; set; }
        //public string s_phy_purchase_actual { get; set; } -- because already used in view
        #endregion

        public Nullable<long> NoOfHH { get; set; }
        public int? target_male { get; set; }
        public int? target_female { get; set; }
        public int? actual_male { get; set; }
        public int? actual_female { get; set; }

        //SPCR:
        public long? NoOfHHActual { get; set; }
        public int? no_actual_pantawid_households { get; set; }
        public int? no_actual_slp_households { get; set; }
        public int? no_actual_ip_households { get; set; }
        public int? no_actual_families { get; set; }
        public int? no_actual_pantawid_families { get; set; }
        public int? no_actual_slp_families { get; set; }
        public int? no_actual_ip_families { get; set; }
        public int? no_actual_male { get; set; }
        public int? no_actual_female { get; set; }

        //Safeguards:
        public bool? has_sc_essc { get; set; }
        public bool? has_sc_esmp { get; set; }
        public bool? has_sc_ecc { get; set; }
        public bool? has_sc_cnc { get; set; }
        public bool? has_ip_presence { get; set; }
        public bool? has_cadt { get; set; }
        public bool? has_on_process { get; set; }
        public bool? has_cadteable { get; set; }
        public bool? has_ncip { get; set; }
        public DateTime? ncip_date { get; set; }
        public bool? has_validation_conducted { get; set; }
        public DateTime? validation_conducted_date { get; set; }
        public bool? has_sc_cp { get; set; }
        public DateTime? sc_cp_date { get; set; }
        public bool? has_sc_cno { get; set; }
        public DateTime? sc_cno_date { get; set; }
        public bool? land_aq_deed_of_donation { get; set; }
        public bool? quit_claim { get; set; }
        public bool? land_aq_unsfruct { get; set; }
        public bool? right_of_way_agreement { get; set; }
        public bool? permit_to_construct_enter { get; set; }
        public bool? land_aq_blgu_resolution { get; set; }
        public bool? land_aq_deped_certification { get; set; }
        public bool? land_aq_other { get; set; }
        public string other_land_acquisition { get; set; }

        //Closure Information:
        public bool? has_turnover_certificate { get; set; }
        public bool? has_set { get; set; }
        public double? has_set_score { get; set; }
        public bool? has_marker { get; set; }
        public bool? has_before_photo { get; set; }
        public bool? has_after_photo { get; set; }
        public bool? has_onm_group { get; set; }
        public bool? with_tariff { get; set; }
        public bool? has_closed_account { get; set; }

        //new feature of Geotagging: 10-19-17
        public bool? is_eligible_phy_second_tranche { get; set; }
        public bool? is_eligible_phy_third_tranche { get; set; }
        public bool? is_eligible_fin_second_tranche { get; set; }
        public bool? is_eligible_fin_third_tranche { get; set; }

        //For pairing:
        public bool? is_paired { get; set; }
        public bool? is_verified { get; set; }
        public Guid? paired_sp_unique_id { get; set; }
        public int? paired_by { get; set; }
        public DateTime? paired_date { get; set; }

        //libraries: virtual
        [JsonIgnore]
        public virtual lib_region lib_regions { get; set; }
        [JsonIgnore]
        public virtual lib_project_type lib_project_type { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_provinces { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_cities { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }
        [JsonIgnore]
        public virtual lib_physical_status lib_physical_status { get; set; }
        [JsonIgnore]
        public virtual lib_physical_status_category lib_physical_status_category { get; set; }

        //audit:
        public bool? IsActive { get; set; }
        public string deleted_by { get; set; }
        public string deleted_reason { get; set; }
        public DateTime? deleted_date { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public int? push_status_id { get; set; }
        public int? last_modified_by { get; set; }
        public int? approval_id { get; set; }
    }

    /// ---------------------------------- Table name : SPI_MULTIPLE_SP [09-27-2017] ---------------------------------- ///
    public class spi_multiple_sp
    {
        [Key]
        public int spi_multiple_sp_id { get; set; }
        public int sub_project_id { get; set; }
        public decimal? kalahi_target_amount { get; set; }
        public decimal? kalahi_actual_amount { get; set; }
        public decimal? lcc_target_amount { get; set; }
        public decimal? lcc_actual_amount { get; set; }
        public decimal? operation_and_maintenance_cost { get; set; }

        //Audit
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }
    }

    /// ---------------------------------- Table name : SPI_MULTIPLE_VARIATION_DATE [09-27-2017] ---------------------------------- ///
    public class spi_multiple_variation_date
    {
        [Key]
        public int id { get; set; }
        public int sub_project_id { get; set; }
        public DateTime? date_approved { get; set; }

        //Audit
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }
    }

    /// ---------------------------------- Table name : SP_DESKAPP_OTHERS [06-18-2018] 4.0 release ---------------------------------- ///
    public class sp_deskapp_others
    {
        [Key]
        public Guid sp_deskapp_others_id { get; set; }

        public int sub_project_id { get; set; }
        public Guid sub_project_unique_id { get; set; }

        //SPCF Tab > Project Beneficiaries (Target)
        public int? no_target_families { get; set; }
        public int? no_target_pantawid_households { get; set; }
        public int? no_target_pantawid_families { get; set; }
        public int? no_target_slp_households { get; set; }
        public int? no_target_slp_families { get; set; }
        public int? no_target_ip_households { get; set; }
        public int? no_target_ip_families { get; set; }
        public int? no_target_male { get; set; }
        public int? no_target_female { get; set; }
        public int? target_male { get; set; }
        public int? target_female { get; set; }

        //Orders Tab 
        public string suspension_order_list { get; set; }
        public string resume_order_list { get; set; }
        public string variation_order_list { get; set; }

        //O&M Tab > Community Association Formation Tracking
        public string community_formation_list { get; set; }

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }
        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        public int? approval_id { get; set; }

        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }

    }

    /// .---------------------------------- Table name : SPI_LAND_ACQUISITION [06-18-2018] 4.0 release ---------------------------------- ///
    public class spi_land_acquisition
    {
        [Key]
        public int spi_land_acquisition_id { get; set; }

        public int sub_project_id { get; set; }

        public int? no_ESSC { get; set; }
        public int? no_ESMP { get; set; }
        public int? no_ECC { get; set; }
        public int? no_CNC { get; set; }
        public int? no_land_aq_deed_of_donation { get; set; }
        public int? no_land_aq_unsfruct { get; set; }
        public int? no_right_of_way_agreement { get; set; }
        public int? no_permit_to_construct_enter { get; set; }
        public int? no_quit_claim { get; set; }
        public int? no_land_aq_blgu_resolution { get; set; }
        public int? no_land_aq_deped_certification { get; set; }
        public int? no_land_aq_other { get; set; }

        //Audit
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }
    }

    /// ---------------------------------- Table name : SPI_ANCESTRAL_DOMAIN [06-18-2018] 4.0 release ---------------------------------- ///
    public class spi_ancestral_domain
    {
        [Key]
        public int spi_ancestral_domain_id { get; set; }

        public int? sub_project_id { get; set; }
        public int? ancestral_domain_id { get; set; }
        public string region_code { get; set; }
        public string prov_code { get; set; }
        public string city_code { get; set; }
        public string brgy_code { get; set; }

        //Audit
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }
        
        public virtual lib_ancestral_domain_coverage lib_ancestral_domain_coverage { get; set; }
    }

    /// ---------------------------------- Table name : BARANGAY TRANSFER OF FUNDS [06-19-2018] 4.0 release ---------------------------------- ///
    public class barangay_trasnfer_of_funds
    {
        [Key]
        public int id { get; set; }

        public string region_code { get; set; }
        public string prov_code { get; set; }
        public string city_code { get; set; }
        public string brgy_code { get; set; }
        public string batch_target { get; set; }
        public int? modality_id { get; set; }
        public int? cycle_id { get; set; }
        public int? modality_category_id { get; set; }
        public int? erfr_project_id { get; set; }
        public int? budget_year_id { get; set; }
        public int? tranche_id { get; set; }
        public string check_number { get; set; }
        public DateTime? date_transferred { get; set; }
        public DateTime? date_requested { get; set; }
        public DateTime? date_downloaded { get; set; }
        public string account_number { get; set; }
        public string lbp_branch { get; set; }
        public decimal? amount_request { get; set; }
        public decimal? amount_approved { get; set; }
        public string title { get; set; }
        public int? fund_source_id { get; set; }
        //used in sub_project input
        public decimal? amount_utilized { get; set; }
        //pending soon to be used
        public decimal? actual_amount_utilized { get; set; }
        //pending soon to be used
        public decimal? actual_amount_liquidated { get; set; }
        public bool? is_taf { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }
        public bool? is_lgu_led { get; set; }
        public string remarks { get; set; }

        public int? sub_project_id { get; set; }
        public decimal? refunded_amount { get; set; }
        public int? type_of_refund_id { get; set; }
        public string check_number_ipcdd { get; set; }
        public string or_number { get; set; }
        public DateTime? or_date { get; set; }
        public bool? will_not_request_second_tranche { get; set; }

        public string last_updated_via_spi_by { get; set; }
        public DateTime? last_updated_via_spi_date { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }

        [JsonIgnore]
        public virtual lib_tranche lib_tranche { get; set; }
        [JsonIgnore]
        public virtual lib_budget_year lib_budget_year { get; set; }
        [JsonIgnore]
        public virtual lib_type_of_refund lib_type_of_refund { get; set; }

        //[JsonIgnore]
        //public virtual lib_modality_category lib_modality_category { get; set; }
        //[JsonIgnore]
        //public virtual lib_modality lib_modality { get; set; }
        //[JsonIgnore]
        //public virtual lib_regions lib_regions { get; set; }
        //[JsonIgnore]
        //public virtual lib_provinces lib_provinces { get; set; }
        //[JsonIgnore]
        //public virtual lib_cities lib_cities { get; set; }
        //[JsonIgnore]
        //public virtual lib_brgy lib_brgy { get; set; }
        //[JsonIgnore]
        //public virtual lib_fund_source lib_fund_source { get; set; }
        //[JsonIgnore]
        //public virtual lib_cycle lib_cycle { get; set; }
    }

    #region ------------- NOT CREATED ON SQLITE -----------------------------
    public class sub_project_monitoring_header
    {
        public Guid sub_project_monitoring_id { get; set; }
        public int sub_project_id { get; set; } //di ko sure kung mahuhugot na ba ito sa sub project table o entry pa rin ito


        public int? no_actual_households { get; set; }
        public int? no_target_households { get; set; }
        public int? no_target_pantawid_households { get; set; }
        public int? no_target_slp_households { get; set; }
        public int? no_target_ip_households { get; set; }
        public int? no_target_pwd_households { get; set; }
        public int? no_target_senior_households { get; set; }



        public DateTime? date_start_physical_actual { get; set; } //di ko sure kung mahuhugot na ba ito sa sub project table o entry pa rin ito
        public DateTime? date_start_physical_planned { get; set; } //di ko sure kung mahuhugot na ba ito sa sub project table o entry pa rin ito
        public double? Phy_Perc_To_Date { get; set; }// perc_progress_cummulative { get; set; }
        public float Tranche1amount { get; set; } //di ko sure kung mahuhugot na ba ito sa sub project table o entry pa rin ito
        public float Tranche2amount { get; set; } //di ko sure kung mahuhugot na ba ito sa sub project table o entry pa rin ito
        public float Tranche3amount { get; set; } //di ko sure kung mahuhugot na ba ito sa sub project table o entry pa rin ito

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
    }
    public class sub_project_monitoring_detail
    {
        public int sub_project_monitoring_id { get; set; }
        public float perc_progress_physical { get; set; }

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
    }
    #endregion
    
}
