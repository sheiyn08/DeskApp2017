using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class municipal_lcc
    {
        public DateTime? history { get; set; }  //----- for v3.0

        [Key]
        public Guid municipal_lcc_id { get; set; }
        public string old_id { get; set; }
        public double planned_cash { get; set; }
        public double planned_inkind { get; set; }
        public double actual_cash { get; set; }
        public double actual_inkind { get; set; }

        public int no_of_barangays { get; set; }


        public double cbis_plgu_planned { get; set; }
        public double cbis_plgu_actual { get; set; }
        public double cbis_mlgu_planned { get; set; }
        public double cbis_mlgu_actual { get; set; }
        public double cbis_blgu_planned { get; set; }
        public double cbis_blgu_actual { get; set; }
        public double cbis_others_planned { get; set; }
        public double cbis_others_actual { get; set; }


        public double me_plgu_planned { get; set; }
        public double me_plgu_actual { get; set; }
        public double me_mlgu_planned { get; set; }
        public double me_mlgu_actual { get; set; }
        public double me_blgu_planned { get; set; }
        public double me_blgu_actual { get; set; }
        public double me_others_planned { get; set; }
        public double me_others_actual { get; set; }

        public double spi_plgu_planned { get; set; }
        public double spi_plgu_actual { get; set; }
        public double spi_mlgu_planned { get; set; }
        public double spi_mlgu_actual { get; set; }
        public double spi_blgu_planned { get; set; }
        public double spi_blgu_actual { get; set; }
        public double spi_others_planned { get; set; }
        public double spi_others_actual { get; set; }

        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int enrollment_id { get; set; }

        //RDR08242017 Additional columns for v3.0:
        public bool? is_lgu_led { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }


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


    public class municipal_pta
    {



        public string kc_equipment_list { get; set; }

        public int? no_lgu_cso_meeting { get; set; }
        public int? no_gad_plan_assessment { get; set; }
        public int? no_ngo_pim { get; set; }
        [Key]
        public Guid municipal_pta_id { get; set; }
        public string old_id { get; set; }

        public DateTime? moa_signed_date { get; set; }
        public string pta_resolution_no { get; set; }
        public DateTime? pta_approval_date { get; set; }
        public string nga_resolution_no { get; set; }
        public DateTime? nga_approval_date { get; set; }
        public string miacmct_resolution_no { get; set; }
        public DateTime? miacmct_approval_date { get; set; }
        public string ngopo_resolution_no { get; set; }
        public DateTime? ngopo_approval_date { get; set; }
        public string gad_resolution_no { get; set; }
        public DateTime? gad_approval_date { get; set; }
        public string lcc_resolution_no { get; set; }
        public DateTime? lcc_approval_date { get; set; }
        public string trust_account_no { get; set; }
        public DateTime? trust_opened_date { get; set; }
        public string kc_office { get; set; }
        public string kc_equipment { get; set; }
        public int? no_staff { get; set; }
        public int? no_tas { get; set; }
        public string incexp_location_post { get; set; }
        public DateTime? incexp_post_date { get; set; }
        public string budget_location_post { get; set; }
        public DateTime? budget_post_date { get; set; }
        public string plan_location_post { get; set; }
        public DateTime? plan_post_date { get; set; }
        public int? no_ngopo_accredited { get; set; }
        public int? no_ngopo_represented { get; set; }
        public int? ngo_total { get; set; }
        public int? no_4p_male { get; set; }
        public int? no_4p_female { get; set; }
        public int? no_ip_male { get; set; }
        public int? no_ip_female { get; set; }
        public int? no_women_male { get; set; }
        public int? no_women_female { get; set; }
        public int? no_youth_male { get; set; }
        public int? no_youth_female { get; set; }
        public int? no_elderly_male { get; set; }
        public int? no_elderly_female { get; set; }
        public int? no_pwd_male { get; set; }
        public int? no_pwd_female { get; set; }
        public string miac_eo_no { get; set; }
        public DateTime? miac_eo_date { get; set; }
        public string mct_eo_no { get; set; }
        public DateTime? mct_eo_date { get; set; }
        public string focal_person { get; set; }
        public string encoder { get; set; }
        public string office_address { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string mlgu_logistics { get; set; }
        public string mdc_resolution_no { get; set; }
        public string mdc_date { get; set; }
        public int? no_cdd_male { get; set; }
        public int? no_cdd_female { get; set; }
        public int? no_ngopo_male { get; set; }
        public int? no_ngopo_female { get; set; }
        public int? no_lgu_male { get; set; }
        public int? no_lgu_female { get; set; }

        public int? no_ngopo_represented_male { get; set; }
        public int? no_ngopo_represented_female { get; set; }
        public string miac_resolution_no { get; set; }
        public DateTime? miac_approval_date { get; set; }
        public string mct_resolution_no { get; set; }
        public DateTime? mct_approval_date { get; set; }
        public string mdcmem_resolution_no { get; set; }
        public DateTime? mdcmem_approval_date { get; set; }
        public int? mdcmem_male_no { get; set; }
        public int? mdcmem_female_no { get; set; }
        public bool? with_equipment { get; set; }
        public int? lsb_represented_male { get; set; }
        public int? lsb_represented_female { get; set; }
        public int? ngopo_accredited { get; set; }

        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int enrollment_id { get; set; }

        //RDR08242017 Additional columns for v3.0:
        public bool? is_lgu_led { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }


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
