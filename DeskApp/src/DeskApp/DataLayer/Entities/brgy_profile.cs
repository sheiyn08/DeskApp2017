using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskApp.DataLayer
{
    public class brgy_profile
    {

        public int? socio_econ_hhs_1 { get; set; }
        public int? socio_econ_hhs_2 { get; set; }
        public int? socio_econ_hhs_3 { get; set; }
        public string socio_econ_activity_1 { get; set; }
        public double? socio_econ_amount_1 { get; set; }
        public string socio_econ_activity_2 { get; set; }
        public double? socio_econ_amount_2 { get; set; }
        public string socio_econ_activity_3 { get; set; }
        public double? socio_econ_amount_3 { get; set; }

        public string ip_group_in_area { get; set; }

        public string eca_list { get; set; }




        public string brgy_projects { get; set; }



        /// <summary>
        /// Array
        /// </summary>
        public string gad_activity { get; set; }
        public double? gad_utilized { get; set; }
        public int? no_gad_activities { get; set; }

        /// <summary>
        /// Array
        /// </summary>
        public string drrm_activity { get; set; }
        public double? drrm_utilized { get; set; }
        public int? no_drmm_activities { get; set; }

        /// <summary>
        /// Array
        /// </summary>
        public string other_source { get; set; }
        public double? ira_amount { get; set; }
        public double? others_amount { get; set; }


        public bool? has_ip_presence { get; set; }
        [Key]
        public Guid brgy_profile_id { get; set; }
        public string old_id { get; set; }

        public int? no_sitios { get; set; }
        public int? year_source { get; set; }
        public int? no_households { get; set; }
        public int? no_fmheadedhh { get; set; }
        public int? no_iphouseholds { get; set; }
        public int? no_families { get; set; }
        public int? no_ipfamily { get; set; }
        public int? no_withdisability { get; set; }
        public int? no_indisplaced { get; set; }
        public int? no_male { get; set; }
        public int? no_female { get; set; }
        public int? no_male0_5 { get; set; }
        public int? no_female0_5 { get; set; }
        public int? no_male6_12 { get; set; }
        public int? no_female6_12 { get; set; }
        public int? no_male13_16 { get; set; }
        public int? no_female13_16 { get; set; }
        public int? no_male17_59 { get; set; }
        public int? no_female17_59 { get; set; }
        public int? no_male60up { get; set; }
        public int? no_female60up { get; set; }
        public int? no_pantawid_household { get; set; }
        public int? no_pantawid_family { get; set; }
        public int? no_slp_household { get; set; }
        public int? no_slp_family { get; set; }
        public int? baragay_additiondetails { get; set; }
        public bool? is_armedconflict { get; set; }
        public bool? is_bounddispute { get; set; }
        public bool? is_poldispute { get; set; }
        public bool? is_genderviolence { get; set; }
        public bool? is_rido_war { get; set; }
        public bool? is_crime { get; set; }
        public bool? is_poblacion { get; set; }
        public double? hrs_totown { get; set; }
        public double? km_frmtown { get; set; }
        public bool? is_isolated { get; set; }
        public bool? is_upland { get; set; }
        public bool? is_hilly { get; set; }
        public bool? is_lowland { get; set; }
        public bool? is_island { get; set; }
        public bool? is_coastal { get; set; }
        public bool? is_brgyaffected { get; set; }
        public double? alloc_env { get; set; }
        public double? alloc_econ { get; set; }
        public double? alloc_infra { get; set; }
        public double? alloc_basic { get; set; }
        public double? alloc_educ { get; set; }
        public double? alloc_peace { get; set; }
        public double? alloc_inst { get; set; }
        public double? alloc_gender { get; set; }
        public double? alloc_drrm { get; set; }
        public double? alloc_others { get; set; }
        public bool? is_transaccess { get; set; }
        public int? no_mdied5below { get; set; }
        public int? no_fdied5below { get; set; }
        public string source_5dead { get; set; }
        public int? no_diedpreg { get; set; }
        public string sourse_diedpreg { get; set; }
        public int? no_mkidmaln { get; set; }
        public int? no_fkidmaln { get; set; }
        public string source_kidmaln { get; set; }
        public int? no_mkidnosch6_12 { get; set; }
        public int? no_fkidnosch6_12 { get; set; }
        public string srce_kidnosch6_12 { get; set; }
        public int? no_mkidnosch13_16 { get; set; }
        public int? no_fkidnosch13_16 { get; set; }
        public string srce_kidnosch13_16 { get; set; }
        public int? male_hdmkshhouse { get; set; }
        public int? fem_hdmkshouse { get; set; }
        public string source_mkshouse { get; set; }
        public int? male_hdsquatters { get; set; }
        public int? fem_hdsquatters { get; set; }
        public string source_squatters { get; set; }
        public int? male_hdnotoilet { get; set; }
        public int? fem_hdnotoilet { get; set; }
        public string source_notoilet { get; set; }
        public int? male_hdsfwater { get; set; }
        public int? fem_hdsfwater { get; set; }
        public string source_safewater { get; set; }
        public int? male_hdpoverty { get; set; }
        public int? fem_hdpoverty { get; set; }
        public string source_poverty { get; set; }
        public int? male_hdblwfood { get; set; }
        public int? fem_hdblwfood { get; set; }
        public string source_blwfood { get; set; }
        public int? male_hdfoodshrt { get; set; }
        public int? fem_hdfoodshrt { get; set; }
        public string source_foodshort { get; set; }
        public int? no_malenowork { get; set; }
        public int? no_femnowork { get; set; }
        public string source_nowork { get; set; }
        public int? no_malecrimev { get; set; }
        public int? no_femcrimev { get; set; }
        public string source_crimevic { get; set; }
        public double? avg_hhincome { get; set; }
        public double? avg_mhdincome { get; set; }
        public double? avg_fhdincome { get; set; }
        public double? avg_iphdincome { get; set; }

        public int? no_voting_male { get; set; }
        public int? no_voting_female { get; set; }
        public int? no_labor_male { get; set; }
        public int? no_labor_female { get; set; }
        public string access_details { get; set; }
        public bool? access_addressed { get; set; }
        public string access_remarks { get; set; }
        public string water_details { get; set; }
        public bool? water_address { get; set; }
        public string water_remarks { get; set; }
        public string health_details { get; set; }
        public bool? health_address { get; set; }
        public string health_remarks { get; set; }
        public string literacy_details { get; set; }
        public bool? literacy_addressed { get; set; }
        public string literacy_remarks { get; set; }
        public string employment_details { get; set; }
        public bool? employment_addressed { get; set; }
        public string employment_remarks { get; set; }
        public string landownership_details { get; set; }
        public bool? landownership_addressed { get; set; }
        public string landownership_remarks { get; set; }
        public string agriculture_details { get; set; }
        public bool? agriculture_addressed { get; set; }
        public string agriculture_remarks { get; set; }
        public string peace_details { get; set; }
        public bool? peace_addressed { get; set; }
        public string peace_remarks { get; set; }
        public string environment_details { get; set; }
        public bool? environment_addressed { get; set; }
        public string environment_remarks { get; set; }
        public string powersupply_details { get; set; }
        public bool? powersupply_addressed { get; set; }
        public string powersupply_remarks { get; set; }
        public string communication_details { get; set; }
        public bool? communication_addressed { get; set; }
        public string communication_remarks { get; set; }
        public string others_details { get; set; }
        public bool? others_addressed { get; set; }
        public string others_remarks { get; set; }
        public int? health_number_0_5_value { get; set; }
        public string health_number_0_5_reference { get; set; }
        public int? children_0_5_value { get; set; }
        public string children_0_5_reference { get; set; }
        public int? pregnant_died_value { get; set; }
        public string pregnant_died_reference { get; set; }
        public int? pregnant_total_value { get; set; }
        public string pregnant_total_reference { get; set; }
        public int? malnourished_0_5value { get; set; }
        public string malnourished_0_5reference { get; set; }
        public int? total_malnourished_0_5value { get; set; }
        //  public string total_malnourished_0_5reference { get; set; }
        public int? safewater_value { get; set; }
        public string safewater_reference { get; set; }
        public int? sanity_value { get; set; }
        public string sanity_reference { get; set; }
        public int? totalsanity_value { get; set; }
        public string totalsanity_reference { get; set; }
        public int? squatting_value { get; set; }
        public string squatting_reference { get; set; }
        public int? totalsquatting_value { get; set; }
        public string totalsquatting_reference { get; set; }
        public int? makeshift_value { get; set; }
        public string makeshift_reference { get; set; }
        public int? totalmakeshift_value { get; set; }
        public string totalmakeshift_reference { get; set; }
        public int? victimized_value { get; set; }
        public string victimized_reference { get; set; }
        public int? totalvictimized_value { get; set; }
        public string totalvictimized_reference { get; set; }
        public int? threshold_value { get; set; }
        public string threshold_reference { get; set; }
        public int? totalthreshold_value { get; set; }
        public string totalthreshold_reference { get; set; }
        public int? incomeless_value { get; set; }
        public string incomeless_reference { get; set; }
        public int? totalincomeless_value { get; set; }
        public string totalincomeless_reference { get; set; }
        public int? lessthan_3_meals_value { get; set; }
        public string lessthan_3_meals_reference { get; set; }
        public int? totallessthan_3_meals_value { get; set; }
        // public string totallessthan_3_meals_reference { get; set; }
        public int? children_6_12_elem_value { get; set; }
        public string children_6_12_elem_reference { get; set; }
        public int? totalchildren_6_12_elem_value { get; set; }
        //   public string totalchildren_6_12_elem_reference { get; set; }
        public int? children_13_16_secondary_value { get; set; }
        //    public string children_13_16_secondary_reference { get; set; }
        //   public int? totalchildren_13_16_secondary_value { get; set; }
        //   public string totalchildren_13_16_secondary_reference { get; set; }
        public int? laborforce_value { get; set; }
        public string laborforce_reference { get; set; }
        public int? totallaborforce_value { get; set; }
        public string totallaborforce_reference { get; set; }
        public int? totalsafewater_value { get; set; }
        public string totalsafewater_reference { get; set; }

        public double? mins_totown { get; set; }

        //#region Fund Source

        //public string region_code { get; set; }
        //public string prov_code { get; set; }
        //public string city_code { get; set; }



        //public virtual lib_region lib_region { get; set; }
        //public virtual lib_province lib_province { get; set; }
        //public virtual lib_city lib_city { get; set; }


        public string tot_malnourished_0_5_ref { get; set; }

        public string tot_lessthan_3_meals_ref { get; set; }

        public string totchild_6_12_elem_ref { get; set; }

        public string child_13_16_secondary_ref { get; set; }

        public string totchild_13_16_secondary_ref { get; set; }

        public int? totchild_13_16_secondary_val { get; set; }

        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        #region location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int brgy_code { get; set; }
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
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

   
        public int approval_id { get; set; }
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
     


      


        #region Facilities
        public bool? has_bank { get; set; }
        public bool? has_barangay_hall { get; set; }
        public bool? has_cap_agri { get; set; }
        public bool? has_cap_health { get; set; }
        public bool? has_cap_org_dev { get; set; }
        public bool? has_cap_others { get; set; }
        public bool? has_cementery { get; set; }
        public bool? has_college { get; set; }
        public bool? has_credit { get; set; }
        public bool? has_daycare { get; set; }
        public bool? has_drainage { get; set; }
        public bool? has_electricity { get; set; }
        public bool? has_elementary { get; set; }
        public bool? has_emergency_service { get; set; }
        public bool? has_evac_center { get; set; }
        public bool? has_harvest { get; set; }
        public bool? has_health { get; set; }
        public bool? has_hospital { get; set; }
        public bool? has_housing { get; set; }
        public bool? has_irrigation { get; set; }
        public bool? has_market { get; set; }
        public bool? has_miniport { get; set; }
        public bool? has_multipurpose { get; set; }
        public bool? has_secondary { get; set; }
        public bool? has_stores { get; set; }
        public bool? has_tanod { get; set; }
        public bool? has_telecom { get; set; }
        public bool? has_tribal { get; set; }
        public bool? has_waste { get; set; }
        public bool? has_water_supply_system { get; set; }
        public double? nearest_bank { get; set; }
        public double? nearest_barangay_hall { get; set; }
        public double? nearest_cap_agri { get; set; }
        public double? nearest_cap_health { get; set; }
        public double? nearest_cap_org_dev { get; set; }
        public double? nearest_cap_others { get; set; }
        public double? nearest_cementery { get; set; }
        public double? nearest_college { get; set; }
        public double? nearest_credit { get; set; }
        public double? nearest_daycare { get; set; }
        public double? nearest_drainage { get; set; }
        public double? nearest_electricity { get; set; }
        public double? nearest_elementary { get; set; }
        public double? nearest_emergency_service { get; set; }
        public double? nearest_evac_center { get; set; }
        public double? nearest_harvest { get; set; }
        public double? nearest_health { get; set; }
        public double? nearest_hospital { get; set; }
        public double? nearest_housing { get; set; }
        public double? nearest_irrigation { get; set; }
        public double? nearest_market { get; set; }
        public double? nearest_miniport { get; set; }
        public double? nearest_multipurpose { get; set; }
        public double? nearest_secondary { get; set; }
        public double? nearest_stores { get; set; }
        public double? nearest_tanod { get; set; }
        public double? nearest_telecom { get; set; }
        public double? nearest_tribal { get; set; }
        public double? nearest_waste { get; set; }
        public double? nearest_water_supply_system { get; set; }
        public int? transpo_bank { get; set; }
        public int? transpo_barangay_hall { get; set; }
        public int? transpo_cap_agri { get; set; }
        public int? transpo_cap_health { get; set; }
        public int? transpo_cap_org_dev { get; set; }
        public int? transpo_cap_others { get; set; }
        public int? transpo_cementery { get; set; }
        public int? transpo_college { get; set; }
        public int? transpo_credit { get; set; }
        public int? transpo_daycare { get; set; }
        public int? transpo_drainage { get; set; }
        public int? transpo_electricity { get; set; }
        public int? transpo_elementary { get; set; }
        public int? transpo_emergency_service { get; set; }
        public int? transpo_evac_center { get; set; }
        public int? transpo_harvest { get; set; }
        public int? transpo_health { get; set; }
        public int? transpo_hospital { get; set; }
        public int? transpo_housing { get; set; }
        public int? transpo_irrigation { get; set; }
        public int? transpo_market { get; set; }
        public int? transpo_miniport { get; set; }
        public int? transpo_multipurpose { get; set; }
        public int? transpo_secondary { get; set; }
        public int? transpo_stores { get; set; }
        public int? transpo_tanod { get; set; }
        public int? transpo_telecom { get; set; }
        public int? transpo_tribal { get; set; }
        public int? transpo_waste { get; set; }
        public int? transpo_water_supply_system { get; set; }

        #endregion

 
    }



    public class brgy_eca
    {
        [Key]
        public Guid brgy_eca_id { get; set; }
        public string old_id { get; set; }
        public int eca_type_id { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string effects { get; set; }

        public int approval_id { get; set; }

        #region location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int  brgy_code { get; set; }


        public virtual lib_region lib_region { get; set; }
        public virtual lib_province lib_province { get; set; }
        public virtual lib_city lib_city { get; set; }
        public virtual lib_brgy lib_brgy { get; set; }

        public virtual lib_eca_type lib_eca_type { get; set; }


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
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

    }

    public class lib_eca_type
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int eca_type_id { get; set; }
        public string name { get; set; }
    }


    public class lib_transpo_mode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int transpo_mode_id { get; set; }
        public string name { get; set; }
    }

    public class brgy_eca_mapping
    {
        public Guid brgy_eca_id { get; set; }
        public string old_id { get; set; }
        public int eca_type_id { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string effects { get; set; }

        public int approval_id { get; set; }

        #region location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int brgy_code { get; set; }





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
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

    }


    public class brgy_profile_ip
    {
        [Key]
        public Guid brgy_profile_ip_id { get; set; }
        public Guid brgy_profile_id { get; set; }
        public string old_id { get; set; }
        public string location { get; set; }
        public int ip_group_id { get; set; }

        public int no_household { get; set; }
        public int no_families { get; set; }
        public int no_male { get; set; }
        public int no_female { get; set; }


        public virtual lib_ip_group lib_ip_group { get; set; }
        public virtual brgy_profile brgy_profile { get; set; }
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

    }


}
