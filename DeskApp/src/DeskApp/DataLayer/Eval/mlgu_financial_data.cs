using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Eval
{
    public class mlgu_financial_data : base_record_location
    {
        [Key]
        public Guid mlgu_financial_data_record_id { get; set; }

        public int year_id { get; set; }

        public DateTime? talakayan_date_start { get; set; }
        public DateTime? talakayan_date_end { get; set; }

        public int? locally_sourced_revenues { get; set; }
        public int? ira_share { get; set; }
        public int? other_revenues_total { get; set; }
        public int? other_shares_natl_tax { get; set; }
        public int? inter_local_transfers { get; set; }
        public int? extraordinary_receipts { get; set; }
        public int? total_lgu_income { get; set; }
        public int? expenditures_gen_public_services { get; set; }
        public int? expenditures_educ_culture_etc { get; set; }
        public int? expenditures_health_services { get; set; }
        public int? expenditures_labor_and_employment { get; set; }
        public int? expenditures_housing_comm_devt { get; set; }
        public int? expenditures_social_welfare_services { get; set; }
        public int? expenditures_economic_services { get; set; }
        public int? expenditures_other_purposes { get; set; }

        public string locally_sourced_revenues_source { get; set; }
        public string ira_share_source { get; set; }
        public string other_revenues_total_source { get; set; }
        public string other_shares_natl_tax_source { get; set; }
        public string inter_local_transfers_source { get; set; }
        public string extraordinary_receipts_source { get; set; }
        public string total_lgu_income_source { get; set; }
        public string expenditures_gen_public_services_source { get; set; }
        public string expenditures_educ_culture_etc_source { get; set; }
        public string expenditures_health_services_source { get; set; }
        public string expenditures_labor_and_employment_source { get; set; }
        public string expenditures_housing_comm_devt_source { get; set; }
        public string expenditures_social_welfare_services_source { get; set; }
        public string expenditures_economic_services_source { get; set; }
        public string expenditures_other_purposes_source { get; set; }

    }

    public class base_record_location
    {
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }
        [JsonIgnore]
        public virtual dof_blgf_financial_data dof_blgf_financial_data { get; set; }
        [JsonIgnore]
        public virtual lgpms_data lgpms_data { get; set; }
        [JsonIgnore]
        public virtual talakayan_year talakayan_year { get; set; }

        public int talakayan_yr_id { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? cycle_id { get; set; }

        public int? locally_shared_revenues { get; set; }

        public int psgc_code { get; set; }

        public int? overall_performance_index_2009 { get; set; }
        public int? overall_performance_index_2010 { get; set; }
        public int? overall_performance_index_2011 { get; set; }
        public int? overall_performance_index_2012 { get; set; }
        public int? administrative_governance_2009 { get; set; }
        public int? administrative_governance_2010 { get; set; }
        public int? administrative_governance_2011 { get; set; }
        public int? administrative_governance_2012 { get; set; }
        public int? social_governance_2009 { get; set; }
        public int? social_governance_2010 { get; set; }
        public int? social_governance_2011 { get; set; }
        public int? social_governance_2012 { get; set; }
        public int? economic_governance_2009 { get; set; }
        public int? economic_governance_2010 { get; set; }
        public int? economic_governance_2011 { get; set; }
        public int? economic_governance_2012 { get; set; }
        public int? environmental_governance_2009 { get; set; }
        public int? environmental_governance_2010 { get; set; }
        public int? environmental_governance_2011 { get; set; }
        public int? environmental_governance_2012 { get; set; }
        public int? valuing_fundamentals_of_good_gov_2009 { get; set; }
        public int? valuing_fundamentals_of_good_gov_2010 { get; set; }
        public int? valuing_fundamentals_of_good_gov_2011 { get; set; }
        public int? valuing_fundamentals_of_good_gov_2012 { get; set; }


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
