//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace DeskApp.DataLayer
//{
//    public class sub_project_mapping
//    {

//        public string path { get; set; }
//        public bool? is_public_school_for_ip { get; set; }
//        public bool? is_kalahi_funded { get; set; }
//        public bool? is_deped_funded { get; set; }

//        public string dep_ed_amount { get; set; }

//        public string ip_groups { get; set; }


//        public bool? has_variation { get; set; }
//        public bool? phy_has_construction_target { get; set; }
//        public bool? phy_has_improvement_target { get; set; }
//        public bool? phy_has_rehabilitation_target { get; set; }
//        public bool? phy_has_purchase_target { get; set; }


//        public int? movement_id { get; set; }
//        public bool? has_t1 { get; set; }
//        public bool? has_t2 { get; set; }
//        public bool? has_t3 { get; set; }


//        //erfr
//        public decimal? erfr_t1 { get; set; }
//        public decimal? erfr_t2 { get; set; }
//        public decimal? erfr_t3 { get; set; }

//        public int? erfr_id_t1 { get; set; }

//        public int? erfr_id_t2 { get; set; }

//        public int? erfr_id_t3 { get; set; }

//        public bool? phy_has_construction_target_secondary { get; set; }
//        public bool? phy_has_improvement_target_secondary { get; set; }
//        public bool? phy_has_rehabilitation_target_secondary { get; set; }
//        public bool? phy_has_purchase_target_secondary { get; set; }


//        public decimal? phy_construction_target { get; set; }
//        public decimal? phy_improvement_target { get; set; }
//        public decimal? phy_rehabilitation_target { get; set; }
//        public decimal? phy_purchase_target { get; set; }

//        public decimal? phy_construction_actual { get; set; }
//        public decimal? phy_improvement_actual { get; set; }
//        public decimal? phy_rehabilitation_actual { get; set; }
//        public decimal? phy_purchase_actual { get; set; }


//        public decimal? phy_construction_target_secondary { get; set; }
//        public decimal? phy_improvement_target_secondary { get; set; }
//        public decimal? phy_rehabilitation_target_secondary { get; set; }
//        public decimal? phy_purchase_target_secondary { get; set; }

//        public decimal? phy_construction_actual_secondary { get; set; }
//        public decimal? phy_improvement_actual_secondary { get; set; }
//        public decimal? phy_rehabilitation_actual_secondary { get; set; }
//        public decimal? phy_purchase_actual_secondary { get; set; }


//        #region correct

//        public string s_phy_construction_target_secondary { get; set; }
//        public string s_phy_improvement_target_secondary { get; set; }
//        public string s_phy_rehabilitation_target_secondary { get; set; }
//        public string s_phy_purchase_target_secondary { get; set; }

//        public string s_phy_construction_actual_secondary { get; set; }
//        public string s_phy_improvement_actual_secondary { get; set; }
//        public string s_phy_rehabilitation_actual_secondary { get; set; }
//        public string s_phy_purchase_actual_secondary { get; set; }


//        public string s_phy_construction_target { get; set; }
//        public string s_phy_improvement_target { get; set; }
//        public string s_phy_rehabilitation_target { get; set; }
//        public string s_phy_purchase_target { get; set; }

//        public string s_phy_construction_actual { get; set; }
//        public string s_phy_improvement_actual { get; set; }
//        public string s_phy_rehabilitation_actual { get; set; }
//        public string s_phy_purchase_actual { get; set; }
//        #endregion

//        ////erfr sub_project ownership validation
//        public bool? is_correct_sp_id { get; set; }

//        //public bool? is_spid_confirmed { get; set; }



//        #region Original Content

//        public int sub_project_id { get; set; }
//        public int EngineeringMigrationId { get; set; }

//        public int modality_id { get; set; }
//        public long? ID { get; set; }

//        public Nullable<long> ReportID { get; set; }
//        public Nullable<long> Month { get; set; }
//        public Nullable<long> Year { get; set; }
//        public int cycle_id { get; set; }

//        public int region_code { get; set; }

//        public int prov_code { get; set; }

//        public int city_code { get; set; }

//        public string brgy_code { get; set; }

//        public string sub_project_name { get; set; }
//        public Nullable<long> NoOfBgys { get; set; }

//        public Nullable<long> NoOfHH { get; set; }

//        public int project_type_id { get; set; }
//        public Nullable<double> Physical_Target { get; set; }
//        public string Phy_Target_Unit { get; set; }
//        public Nullable<double> Kalahi_Amount { get; set; }
//        public Nullable<double> LCC_Amount { get; set; }
//        public Nullable<double> Total_Amount { get; set; }
//        public Nullable<System.DateTime> Date_Started { get; set; }
//        public Nullable<System.DateTime> Date_of_Completion { get; set; }
//        public Nullable<double> ProjectDuration { get; set; }
//        public Nullable<double> Phy_Perc_Previous { get; set; }
//        public Nullable<double> Phy_Perc_To_Date { get; set; }
//        public Nullable<double> Phy_Perc_This_Month { get; set; }
//        public Nullable<double> KC_Fund_Released { get; set; }
//        public Nullable<double> Balance { get; set; }
//        public string Remarks { get; set; }
//        public string Tranche { get; set; }
//        public string SchoolType { get; set; }
//        public Nullable<long> NoOfClassroom { get; set; }
//        public Nullable<long> WaterSystemType { get; set; }
//        public Nullable<double> Tranche1amount { get; set; }
//        public Nullable<double> Tranche2amount { get; set; }
//        public Nullable<double> Tranche3amount { get; set; }
//        public Nullable<System.DateTime> Tranche1date { get; set; }
//        public Nullable<System.DateTime> Tranche2date { get; set; }
//        public Nullable<System.DateTime> Tranche3date { get; set; }
//        public Nullable<double> Tranche1Perc { get; set; }
//        public Nullable<double> Tranche2Perc { get; set; }
//        public Nullable<double> Tranche3Perc { get; set; }
//        public Nullable<double> Tranche1Phy { get; set; }
//        public Nullable<double> Tranche2Phy { get; set; }
//        public Nullable<double> Tranche3Phy { get; set; }
//        public Nullable<double> Totaltrancheamount { get; set; }
//        public string UpdatedBy { get; set; }
//        public Nullable<System.DateTime> DateUpdated { get; set; }
//        public Nullable<double> Tranche1Amt_Old { get; set; }
//        public Nullable<double> Tranche2Amt_Old { get; set; }
//        public Nullable<double> Tranche3Amt_Old { get; set; }
//        public Nullable<double> KalahiAmt_Old { get; set; }
//        public Nullable<double> LCCAmt_Old { get; set; }
//        public Nullable<long> Tranch1Flag { get; set; }
//        public Nullable<long> NoOfTapstands { get; set; }
//        public Nullable<long> NoOfHPumps { get; set; }
//        public Nullable<long> WS_wd_Sanitation { get; set; }
//        public Nullable<long> Delete_Ind { get; set; }
//        public Nullable<long> Add_Ind { get; set; }
//        public Nullable<long> Edit_Ind { get; set; }
//        public Nullable<long> RFR_Ref_No { get; set; }
//        public int physical_status_id { get; set; }
//        public Nullable<long> BalanceNotReq { get; set; }
//        public Nullable<double> KC_Adj { get; set; }
//        public Nullable<double> LCC_Adj { get; set; }
//        public Nullable<double> TPC_Adj { get; set; }
//        public Nullable<double> ExcessFund { get; set; }
//        public Nullable<double> SkilledLaborCost { get; set; }
//        public Nullable<double> UnskilledLaborCost { get; set; }
//        public Nullable<long> Vdr_Update { get; set; }
//        public Nullable<long> RFR_Update { get; set; }
//        public Nullable<System.DateTime> DateExtracted { get; set; }
//        public Nullable<System.DateTime> PlannedDate_Completion { get; set; }
//        public Nullable<long> LandOwnership { get; set; }
//        public Nullable<double> PamanaGrant { get; set; }
//        public Nullable<long> OldSP_ID { get; set; }
//        public string project_sequence { get; set; }


//        public int? fund_source_id { get; set; }

//        //NCDDP Database fields
//        //Added June 3 2015

//        //also known as ncddp_groupings in excel table
//        public int? modality_category_id { get; set; }

//        public int? erfr_project_id { get; set; }
//        //   public DateTime? DateOfReporting { get; set; }

//        //determine if project was added via migration or via manual encoding
//        public bool? is_created_via_migration { get; set; }

//        /// <summary>
//        /// June 25 
//        /// </summary>
//        public double? operation_maintainance_cost { get; set; }
//        public int? approval_id { get; set; }


//        public int? photos { get; set; }


//        public int? geotagged_co_approved_1 { get; set; }
//        public int? geotagged_co_disapproved_5 { get; set; }
//        public int? geotagged_fo_approved_2 { get; set; }
//        public int? geotagged_fo_disapproved_4 { get; set; }
//        public int? geotagged_srpmo_approved_3 { get; set; }
//        public int? geotagged_srpmo_disapproved_6 { get; set; }
//        public int? geotagged_act_uploaded_7 { get; set; }
//        public double? Longitude { get; set; }
//        public double? Latitude { get; set; }
//        public bool? is_duplicate { get; set; }
//        public bool? is_updated { get; set; }

//        public int? physical_status_category_id { get; set; }



//        #endregion


//        #region Auditing
//        public bool? IsActive { get; set; }
//        public string deleted_by { get; set; }

//        public string deleted_reason { get; set; }
//        public DateTime? deleted_date { get; set; }
//        public string created_by { get; set; }
//        public DateTime created_date { get; set; }

//        public string last_updated_by { get; set; }
//        public DateTime? last_updated_date { get; set; }
//        #endregion


//        //determine if has 
//        public bool? has_local_counterpart { get; set; }

//        #region GIG Requirement
//        public int? target_male { get; set; }
//        public int? target_female { get; set; }
//        public int? actual_male { get; set; }
//        public int? actual_female { get; set; }

//        public int? training_category_id { get; set; }



//        #endregion


//        #region Additional Fields
//        //11-6-2015
//        public long? NoOfHHActual { get; set; }
//        public int? enrollment_type_id { get; set; }


//        //11-13-2015
//        //Safeguards
//        public bool? has_ip_presence { get; set; }
//        public int? ancestral_domain_status_id { get; set; }

//        public bool? has_sc_esmp { get; set; }
//        public bool? has_sc_ecc { get; set; }
//        public bool? has_sc_cnc { get; set; }
//        public bool? has_sc_cp { get; set; }
//        public bool? has_sc_cno { get; set; }

//        public bool? within_env_crit_area { get; set; }
//        public int? land_acquisition_id { get; set; }
//        //other
//        public string other_land_acquisition { get; set; }

//        #endregion


//        #region BUB Additional Field
//        public int? no_pantawid_families { get; set; }
//        public int? no_non_pantawid_families { get; set; }

//        public double? actual_breakdown { get; set; }

//        public string mode_of_implementation { get; set; }
//        public int? year_of_implementation { get; set; }

//        public string dbm_project_name { get; set; }

//        public string lgu_engagement { get; set; }
//        public string rfr_status { get; set; }

//        #endregion


//        //January 12 2015
//        public bool? has_after_photo { get; set; }
//        public bool? has_before_photo { get; set; }

//        public bool? has_scanned_spcr { get; set; }
//        public bool? has_turnover_certificate { get; set; }
//        public bool? has_marker { get; set; }
//        public bool? has_set { get; set; }
//        public double? has_set_score { get; set; }
//        //april 
//        public bool? has_onm_group { get; set; }
//        public bool? has_closed_account { get; set; }
//        //revised land acquisition
//        //jan 28

//        public bool? land_aq_deed_of_donation { get; set; }
//        public bool? land_aq_unsfruct { get; set; }
//        public bool? land_aq_blgu_resolution { get; set; }
//        public bool? land_aq_deped_certification { get; set; }
//        public bool? land_aq_other { get; set; }

//        //revised land ownership
//        public bool? land_ownership_dod { get; set; }
//        public bool? land_ownership_gov { get; set; }
//        public bool? land_ownership_public_lands { get; set; }
//        public bool? land_ownership_private_untitled { get; set; }
//        public bool? land_ownership_private_titled { get; set; }


//        public string bub_unique_id { get; set; }

//    }


//}