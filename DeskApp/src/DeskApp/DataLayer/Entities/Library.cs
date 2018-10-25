using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskApp.DataLayer
{

    //public class ncddp_grouping
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    //    public int ncddp_grouping_id { get; set; }
    //    public int region_code { get; set; }
    //    public string prov_code { get; set; }
    //    public int city_code { get; set; }
    //    public int proposed_launch_year { get; set; }

    //    public int no_of_barangays { get; set; }
    //    public int grouping_id { get; set; }
    //    [JsonIgnore]
    //    public virtual lib_region lib_region { get; set; }
    //    [JsonIgnore]
    //    public virtual lib_province lib_province { get; set; }
    //    [JsonIgnore]
    //    public virtual lib_city lib_city { get; set; }

    //    public int? no_of_enrolled_municipalities { get; set; }
    //    public int? no_of_enrolled_barangays { get; set; }

    //    public string is_yolanda { get; set; }
    //}

    public class lib_barangay_assembly_purpose
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int barangay_assembly_purpose_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }


    public class lib_region
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int region_code { get; set; }
        public string region_nick { get; set; }
        public string region_name { get; set; }
        public string psgc { get; set; }

    }


    public class lib_province
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prov_code { get; set; }
        public string prov_name { get; set; }
        public int region_code { get; set; }
        public string psgc { get; set; }

        //public virtual lib_region lib_region { get; set; }

    }


    public class lib_city
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int city_code { get; set; }
        public string city_name { get; set; }
        public int prov_code { get; set; }
        public string psgc { get; set; }


        //public virtual lib_province lib_province { get; set; }
    }


    public class lib_brgy
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brgy_code { get; set; }
        public string brgy_name { get; set; }
        public int city_code { get; set; }
        public Nullable<byte> brgy_mode { get; set; }

        public string district { get; set; }
        public string psgc { get; set; }
        

        //public virtual lib_city lib_city { get; set; }
    }

    public class lib_enrollment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int enrollment_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }

    public class lib_approval
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int approval_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }
    public class lib_cycle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cycle_id { get; set; }
        public string name { get; set; }

        public int fund_source_id { get; set; }
        public virtual lib_fund_source lib_fund_source { get; set; }
        public bool? is_active { get; set; }
    }
    public class lib_fund_source
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fund_source_id { get; set; }
        [Display(Name = "Fund Source")]
        public string name { get; set; }
        public bool? is_active { get; set; }

        public int? geotagging_fund_source_id { get; set; }
    }

    public class lib_lgu_level
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lgu_level_id { get; set; }
        public string name { get; set; }
    }

    #region Beneficiary profile Library

    public class lib_kalahi_committee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int kalahi_committee_id { get; set; }
        public string name { get; set; }


    }
    public class lib_organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int organization_id { get; set; }
        public string name { get; set; }


    }

    public class lib_lgu_position
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lgu_position_id { get; set; }
        public  int lgu_level_id { get; set; }
        public string name { get; set; }

        public bool? is_active { get; set; }
    }

    public class lib_education_attainment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int education_attainment_id { get; set; }
        public string name { get; set; }

        public bool? is_active { get; set; }
    }


    public class lib_occupation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int occupation_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string sub_groups { get; set; }
        public bool? is_active { get; set; }
        public int? return_id { get; set; }
    }

    public class lib_civil_status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int civil_status_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }

    public class lib_sector
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sector_id { get; set; }
        public string name { get; set; }
    }

    #endregion


   
    public class lib_ers_current_work
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ers_current_work_id { get; set; }
        public string name { get; set; }
        public bool? is_skilled { get; set; }
    }

    #region Geotagging
  
    public class lib_physical_status_category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int physical_status_category_id { get; set; }
        public string name { get; set; }
    }
    public class lib_project_type
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int project_type_id { get; set; }
        public string name { get; set; }
        public string project_type_id_old { get; set; }
        public string tsunit { get; set; }
        public int? major_classification_id { get; set; }
        public int? sub_class_id { get; set; }
        public int? geo_category_id { get; set; }
        public int? deleted { get; set; }


        public bool? phy_has_construction_target { get; set; }
        public bool? phy_has_improvement_target { get; set; }
        public bool? phy_has_rehabilitation_target { get; set; }
        public bool? phy_has_purchase_target { get; set; }




        public string phy_construction_unit { get; set; }
        public string phy_improvement_unit { get; set; }
        public string phy_rehabilitation_unit { get; set; }
        public string phy_purchase_unit { get; set; }



        public bool? phy_has_construction_target_secondary { get; set; }
        public bool? phy_has_improvement_target_secondary { get; set; }
        public bool? phy_has_rehabilitation_target_secondary { get; set; }
        public bool? phy_has_purchase_target_secondary { get; set; }




        public string phy_construction_unit_secondary { get; set; }
        public string phy_improvement_unit_secondary { get; set; }
        public string phy_rehabilitation_unit_secondary { get; set; }
        public string phy_purchase_unit_secondary { get; set; }

        [JsonIgnore]
        public virtual lib_major_classification lib_major_classification { get; set; }
    }
    public class lib_major_classification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int major_classification_id { get; set; }
        public string name { get; set; }
    }
    public class lib_physical_status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int physical_status_id { get; set; }
        public string name { get; set; }
    }


    #endregion

    #region 4.0 SP additional library
    public class lib_ancestral_domain
    {
        [Key]
        public int ancestral_domain_id { get; set; }
        public string name { get; set; }
    }

    public class lib_ancestral_domain_coverage
    {
        [Key]
        public int ad_coverage_id { get; set; }
        public int ancestral_domain_id { get; set; }
        public string region_code { get; set; }
        public string prov_code { get; set; }
        public string city_code { get; set; }
        public string brgy_code { get; set; }
        public string sitio { get; set; }
        //json array list for multiple tribe in one barangay maybe 
        public string icc_tribe { get; set; }
        public int? type_of_barangay { get; set; }
        public int? total_brgys { get; set; }
        //foreign key for future reference and create lib_target_year
        public int? target_year_id { get; set; }

        public virtual lib_ancestral_domain lib_ancestral_domain { get; set; }
        public virtual lib_region lib_region { get; set; }
        public virtual lib_province lib_province { get; set; }
        public virtual lib_city lib_city { get; set; }
        public virtual lib_brgy lib_brgy { get; set; }
    }

    public class lib_tranche
    {
        [Key]
        public int tranche_id { get; set; }
        public string name { get; set; }
    }

    public class lib_budget_year
    {
        [Key]
        public int budget_year_id { get; set; }
        public string name { get; set; }
    }

    public class lib_type_of_refund
    {
        [Key]
        public int type_of_refund_id { get; set; }
        public string name { get; set; }
    }

    #endregion


    public class lib_training_category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int training_category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public bool? is_barangay { get; set; }
        public bool? is_municipality { get; set; }
        public bool? is_ceac { get; set; }
        public bool? is_accelerated { get; set; }
        public bool? is_regular { get; set; }

        public bool? is_active { get; set; }

        public int? muni_sort_order { get; set; }
        public int? brgy_sort_order { get; set; }
        public bool? is_ceac_tracking_only { get; set; }
        public int? return_id { get; set; }
    }

    public class lib_volunteer_committee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int volunteer_committee_id { get; set; }
        public string name { get; set; }
         
    }
    public class lib_volunteer_committee_position
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int volunteer_committee_position_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }

    public class lib_push_status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int push_status_id { get; set; }
        public string name { get; set; }
    }

    public class lib_psa_problem_category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int psa_problem_category_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }
    public class lib_psa_solution_category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int psa_solution_category_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
    }

    #region Talakayan
    public class talakayan_year
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int talakayan_yr_id { get; set; }
        public string name { get; set; }
    }

    //v3.0 new table:
    public class lib_mode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mode_id { get; set; }
        public string name { get; set; }
    }

    #endregion

    //#region MLGU Year
    //public class mlgu_financial_year
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    //    public int year_id { get; set; }
    //    public string name { get; set; }
    //}

    //#endregion
}