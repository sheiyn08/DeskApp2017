using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{

    public class PagedCollection<T>
    {
        public int Page { get; set; }
        public int Ref_Page { get; set; }

        public int Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int Ref_Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int Ref_TotalPages { get; set; }
        public int Ref_TotalCount { get; set; }
        public int TotalSync { get; set; }
        public int TotalUnAuthorized { get; set; }
        public int TotalForDeletion { get; set; }
        public IEnumerable<T> Items { get; set; }
        public IEnumerable<T> References { get; set; } //--for SP reference table
        //added July 10, 2017 to be used for displaying count of participants already added on a specific training
        public int TotalCountParticipants { get; set; }

        //added 09-11-2017 to be used for identifying if record has attachment
        public bool with_attachment { get; set; }
    }

    public class AngularFilterModel
    {
        public Guid? person_profile_id { get; set; }

        public int? mov_list_id { get; set; }
        //public int? age_bracket { get; set; }
        public int? project_type_id { get; set; }
        public int? major_classification_id { get; set; }
        public int? physical_status_id { get; set; }
        public  Guid? sub_project_ers_id { get; set; }
        public  Guid? activity_source_id { get; set; }
        public int? table_name_id { get; set; }
        public int? id { get; set; }
        public int? barangay_assembly_purpose_id { get; set; }
        public string name { get; set; }
        public int? lgu_level_id { get; set; }
        public Guid? record_id { get; set; }
        public int? region_code { get; set; }
        public int? prov_code { get; set; }
        public int? city_code { get; set; }
        public int? brgy_code { get; set; }

        //perception survey:
        public int? age { get; set; }
        public int? year { get; set; }
        public DateTime? talakayan_date_from { get; set; }
        public DateTime? talakayan_date_to { get; set; }
        public string activity_name { get; set; }
        public DateTime? survey_date_from { get; set; }
        public DateTime? survey_date_to { get; set; }

        public int? talakayan_yr_id { get; set; }

        //talakayan evaluation:
        public int? evaluation_form_version { get; set; }
        public string talakayan_name { get; set; }
        public DateTime? talakayan_date_start { get; set; }
        public DateTime? talakayan_date_end { get; set; }
        public DateTime? evaluation_date_start { get; set; }
        public DateTime? evaluation_date_end { get; set; }
        public string talakayan_venue { get; set; }
        public int? participant_type { get; set; }

        //#2: dof-blgf financial data:
        public int? year_id { get; set; }
        public int? psgc_code { get; set; }
        public int? locally_shared_revenues { get; set; }
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

        //#3: municipal financial profile:
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

        //#4: mlgu financial data:
        public int? mlgu_locally_sourced_revenues { get; set; }
        public int? mlgu_ira_share { get; set; }
        public int? mlgu_other_revenues_total { get; set; }
        public int? mlgu_other_shares_natl_tax { get; set; }
        public int? mlgu_inter_local_transfers { get; set; }
        public int? mlgu_extraordinary_receipts { get; set; }
        public int? mlgu_total_lgu_income { get; set; }
        public int? mlgu_expenditures_gen_public_services { get; set; }
        public int? mlgu_expenditures_educ_culture_etc { get; set; }
        public int? mlgu_expenditures_health_services { get; set; }
        public int? mlgu_expenditures_labor_and_employment { get; set; }
        public int? mlgu_expenditures_housing_comm_devt { get; set; }
        public int? mlgu_expenditures_social_welfare_services { get; set; }
        public int? mlgu_expenditures_economic_services { get; set; }
        public int? mlgu_expenditures_other_purposes { get; set; }
                
        public string mlgu_locally_sourced_revenues_source { get; set; }
        public string mlgu_ira_share_source { get; set; }
        public string mlgu_other_revenues_total_source { get; set; }
        public string mlgu_other_shares_natl_tax_source { get; set; }
        public string mlgu_inter_local_transfers_source { get; set; }
        public string mlgu_extraordinary_receipts_source { get; set; }
        public string mlgu_total_lgu_income_source { get; set; }
        public string mlgu_expenditures_gen_public_services_source { get; set; }
        public string mlgu_expenditures_educ_culture_etc_source { get; set; }
        public string mlgu_expenditures_health_services_source { get; set; }
        public string mlgu_expenditures_labor_and_employment_source { get; set; }
        public string mlgu_expenditures_housing_comm_devt_source { get; set; }
        public string mlgu_expenditures_social_welfare_services_source { get; set; }
        public string mlgu_expenditures_economic_services_source { get; set; }
        public string mlgu_expenditures_other_purposes_source { get; set; }

        //public int? population_2015 { get; set; }

        public int? fund_source_id { get; set; }
        public int? cycle_id { get; set; }

        public int? enrollment_id { get; set; }

        //person profile
        public bool? is_male { get; set; }
        public bool? is_ip { get; set; }
        public int? ip_group_id { get; set; }

        public int? occupation_id { get; set; }

        public bool? is_lguofficial { get; set; }
        public int? lgu_position_id { get; set; }
        public int? education_attainment_id { get; set; }
        public int? civil_status_id { get; set; }

        //pager 
        public int? pageSize { get; set; }
        public int? currPage { get; set; }
        public int? ref_pageSize { get; set; }
        public int? ref_currPage { get; set; }

        public int? push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        public int? approval_id { get; set; }

        //volunteer
        public bool? is_volunteer { get; set; }
        public int? volunteer_committee_position_id { get; set; }
        public int? volunteer_committee_id { get; set; }

        //worker
        public bool? is_worker { get; set; }

        //training
        public bool? is_trained { get; set; }
        public bool? is_slp { get; set; }
        public bool? is_pantawid { get; set; }
        public int? training_category_id { get; set; }
        public Guid? community_training_id { get; set; }


        //SUBPROJECT: SET
        public DateTime? set_date_eval { get; set; }
        public string set_physical_description { get; set; }
        public double? set_sp_utilization { get; set; }
        public double? set_organization { get; set; }
        public double? set_institutional_linkage { get; set; }
        public double? set_financial { get; set; }
        public double? set_physical { get; set; }
        public double? set_total_score { get; set; }


        //GRIEVANCE
        public int? grs_intake_level_id { get; set; }
        public int? grs_form_id { get; set; }
        public int? grs_filling_mode_id { get; set; }
        public int? grs_complainant_position_id { get; set; }
        public int? grs_resolution_status_id { get; set; }
        public int? grs_feedback_id { get; set; }
        public int? grs_category_id { get; set; }
        public int? grs_complaint_subject_id { get; set; }
        public int? grs_nature_id { get; set; }
        public DateTime? intake_date { get; set; }
        public DateTime? resolved_date { get; set; }

        //Worker
        public int? spi_nature_work_id { get; set; }

        //perception:
        public string person_name { get; set; }

        public int? trust_1 { get; set; }
        public int? trust_2 { get; set; }
        public int? trust_3 { get; set; }
        public int? trust_4 { get; set; }
        public int? trust_5 { get; set; }
        public int? trust_6 { get; set; }
        public int? trust_7 { get; set; }
        public int? trust_8 { get; set; }
        public int? access_1 { get; set; }
        public int? access_2 { get; set; }
        public int? access_3 { get; set; }
        public int? access_4 { get; set; }
        public int? access_5 { get; set; }
        public int? access_6 { get; set; }
        public int? access_7 { get; set; }
        public int? access_8 { get; set; }
        public int? access_9 { get; set; }
        public int? access_10 { get; set; }
        public int? access_11 { get; set; }
        public int? access_12 { get; set; }
        public int? access_13 { get; set; }
        public int? access_14 { get; set; }
        public int? access_15 { get; set; }
        public int? access_16 { get; set; }
        public int? participation_1 { get; set; }
        public int? participation_2 { get; set; }
        public int? participation_3 { get; set; }
        public int? participation_4 { get; set; }
        public int? participation_5 { get; set; }
        public int? participation_6 { get; set; }
        public int? participation_7 { get; set; }
        public int? participation_8 { get; set; }
        public int? participation_9 { get; set; }
        public int? participation_10 { get; set; }
        public int? participation_11 { get; set; }
        public int? participation_12 { get; set; }
        public int? disaster_1 { get; set; }
        public int? disaster_2 { get; set; }
        public int? disaster_3 { get; set; }
        public int? disaster_4 { get; set; }
        public int? disaster_5 { get; set; }
        public int? disaster_6 { get; set; }
        public int? disaster_7 { get; set; }
        public int? disaster_8 { get; set; }
        public int? disaster_9 { get; set; }

        //ceac_tracking:
        
        public DateTime? plan_start { get; set; }
        public DateTime? plan_end { get; set; }
        public DateTime? actual_start { get; set; }
        public DateTime? actual_end { get; set; }
        public DateTime? catch_start { get; set; }
        public DateTime? catch_end { get; set; }        
        public int? implementation_status_id { get; set; }

        public DateTime? fortheperiodof_from { get; set; }
        public DateTime? fortheperiodof_to { get; set; }
        public DateTime? as_of { get; set; }
        public string lib_fund_source_name { get; set; }

        //ba act report:
        public string status_of_schedule { get; set; }

        //mlcc:
        public DateTime? mlcc_start_filterdate { get; set; }
        public DateTime? mlcc_end_filterdate { get; set; }

        //v3.0 Filters:
        public bool? is_lgu_led { get; set; }
        public bool? is_savings { get; set; }
        public bool? is_incentive { get; set; }

        //v3.0 filter for unauthorized:
        public bool? is_unauthorized { get; set; }

        //revised, to remove using of separate api for filter:
        public bool? is_recently_added { get; set; }
        public bool? is_recently_edited { get; set; }

    }
}
