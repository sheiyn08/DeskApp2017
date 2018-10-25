using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.DTO
{
    public class sub_project_ersDTO
    {



        public Guid sub_project_ers_id { get; set; }
        public string old_id { get; set; }
        public int? sub_project_id { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public string brgy_code { get; set; }

        public Guid? sub_project_unique_id { get; set; }

        public int ers_delivery_mode_id { get; set; }

        public DateTime? date_reported { get; set; }
        public DateTime? date_started { get; set; }
        public DateTime? date_ended { get; set; }


        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }

        public int approval_id { get; set; }



        public string lib_ers_delivery_mode_name { get; set; }

        public static System.Linq.Expressions.Expression<Func<sub_project_ers, sub_project_ersDTO>> SELECT =
            x => new sub_project_ersDTO
            {


                lib_ers_delivery_mode_name = x.lib_ers_delivery_mode.name,

                sub_project_unique_id = x.sub_project_unique_id,

                old_id = x.old_id,

                fund_source_id = x.fund_source_id,
                cycle_id = x.cycle_id,
                ers_delivery_mode_id = x.ers_delivery_mode_id,

                region_code = x.region_code,
                prov_code = x.prov_code,
                city_code = x.city_code,
                brgy_code = x.brgy_code,
                created_by = x.created_by,
                created_date = x.created_date,
                last_modified_by = x.last_modified_by,
                last_modified_date = x.last_modified_date,
                is_deleted = x.is_deleted,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                push_status_id = x.push_status_id,
                push_date = x.push_date,
                approval_id = x.approval_id,

                sub_project_ers_id = x.sub_project_ers_id,
                sub_project_id = x.sub_project_id,

                date_ended = x.date_ended,
                date_reported = x.date_reported,
                date_started = x.date_started,



            };

    }




    public class sub_project_setDTO
    {
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
        
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }
        public bool is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        public int approval_id { get; set; }
 

        public static System.Linq.Expressions.Expression<Func<sub_project_set, sub_project_setDTO>> 
            SELECT = x => new sub_project_setDTO
            {
                sub_project_set_id = x.sub_project_set_id,
                old_id = x.old_id,
                sub_project_unique_id = x.sub_project_unique_id,
                sub_project_id = x.sub_project_id,

                set_date_eval = x.set_date_eval,
                set_physical_description = x.set_physical_description,
                set_sp_utilization = x.set_sp_utilization,
                set_organization = x.set_organization,
                set_institutional_linkage = x.set_institutional_linkage,
                set_financial = x.set_financial,
                set_physical = x.set_physical,
                set_total_score = x.set_total_score,

                created_by = x.created_by,
                created_date = x.created_date,
                last_modified_by = x.last_modified_by,
                last_modified_date = x.last_modified_date,
                is_deleted = x.is_deleted,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                push_status_id = x.push_status_id,
                push_date = x.push_date,
                approval_id = x.approval_id
                
            };

    }


    //for v3.0 save SPCF
    public class sub_project_spcfDTO
    {
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

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }
        public bool is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        public int approval_id { get; set; }


        public static System.Linq.Expressions.Expression<Func<sub_project_spcf, sub_project_spcfDTO>>
            SELECT = x => new sub_project_spcfDTO
            {
                sub_project_spcf_id = x.sub_project_spcf_id,
                sub_project_unique_id = x.sub_project_unique_id,
                sub_project_id = x.sub_project_id,

                pow_cdd_cost = x.pow_cdd_cost,
                pow_male_cost = x.pow_male_cost,
                pow_female_cost = x.pow_female_cost,
                pow_blgu_cost = x.pow_blgu_cost,
                pow_mlgu_cost = x.pow_mlgu_cost,
                pow_plgu_cost = x.pow_plgu_cost,
                pow_others_cost = x.pow_others_cost,

                training_cdd_cost = x.training_cdd_cost,
                training_male_cost = x.training_male_cost,
                training_female_cost = x.training_female_cost,
                training_blgu_cost = x.training_blgu_cost,
                training_mlgu_cost = x.training_mlgu_cost,
                training_plgu_cost = x.training_plgu_cost,
                training_others_cost = x.training_others_cost,

                women_cdd_cost = x.women_cdd_cost,
                women_male_cost = x.women_male_cost,
                women_female_cost = x.women_female_cost,
                women_blgu_cost = x.women_blgu_cost,
                women_mlgu_cost = x.women_mlgu_cost,
                women_plgu_cost = x.women_plgu_cost,
                women_others_cost = x.women_others_cost,

                management_cdd_cost = x.management_cdd_cost,
                management_male_cost = x.management_male_cost,
                management_female_cost = x.management_female_cost,
                management_blgu_cost = x.management_blgu_cost,
                management_mlgu_cost = x.management_mlgu_cost,
                management_plgu_cost = x.management_plgu_cost,
                management_others_cost = x.management_others_cost,

                others_cdd_cost = x.others_cdd_cost,
                others_male_cost = x.others_male_cost,
                others_female_cost = x.others_female_cost,
                others_blgu_cost = x.others_blgu_cost,
                others_mlgu_cost = x.others_mlgu_cost,
                others_plgu_cost = x.others_plgu_cost,
                others_others_cost = x.others_others_cost,

                created_by = x.created_by,
                created_date = x.created_date,
                last_modified_by = x.last_modified_by,
                last_modified_date = x.last_modified_date,
                is_deleted = x.is_deleted,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                push_status_id = x.push_status_id,
                push_date = x.push_date,
                approval_id = x.approval_id

            };

    }

    //4.0 SPI_ANCESTRAL_DOMAIN
    public class spi_ancestral_domainDTO
    {
        public int spi_ancestral_domain_id { get; set; }
        public int? sub_project_id { get; set; }
        public int? ancestral_domain_id { get; set; }
        public string region_code { get; set; }
        public string prov_code { get; set; }
        public string city_code { get; set; }
        public string brgy_code { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }

        public static System.Linq.Expressions.Expression<Func<spi_ancestral_domain, spi_ancestral_domainDTO>>
            SELECT = x => new spi_ancestral_domainDTO
            {
                spi_ancestral_domain_id = x.spi_ancestral_domain_id,
                sub_project_id = x.sub_project_id,
                ancestral_domain_id = x.ancestral_domain_id,
                region_code = x.region_code,
                prov_code = x.prov_code,
                city_code = x.city_code,
                brgy_code = x.brgy_code,
                created_by = x.created_by,
                created_date = x.created_date,
                last_updated_by = x.last_updated_by,
                last_updated_date = x.last_updated_date,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                is_deleted = x.is_deleted
            };

    }

    //4.0 SPI_MULTIPLE_SP
    public class spi_multiple_spDTO
    {
        public int spi_multiple_sp_id { get; set; }
        public int sub_project_id { get; set; }
        public decimal? kalahi_target_amount { get; set; }
        public decimal? kalahi_actual_amount { get; set; }
        public decimal? lcc_target_amount { get; set; }
        public decimal? lcc_actual_amount { get; set; }
        public decimal? operation_and_maintenance_cost { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }

        public static System.Linq.Expressions.Expression<Func<spi_multiple_sp, spi_multiple_spDTO>>
            SELECT = x => new spi_multiple_spDTO
            {
                spi_multiple_sp_id = x.spi_multiple_sp_id,
                sub_project_id = x.sub_project_id,
                kalahi_target_amount = x.kalahi_target_amount,
                kalahi_actual_amount = x.kalahi_actual_amount,
                lcc_target_amount = x.lcc_target_amount,
                lcc_actual_amount = x.lcc_actual_amount,
                operation_and_maintenance_cost = x.operation_and_maintenance_cost,
                created_by = x.created_by,
                created_date = x.created_date,
                last_updated_by = x.last_updated_by,
                last_updated_date = x.last_updated_date,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                is_deleted = x.is_deleted
            };

    }

    //4.0 BARANGAY TRANSFER OF FUNDS
    public class btf_DTO
    {
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
        public decimal? amount_utilized { get; set; }
        public decimal? actual_amount_utilized { get; set; }
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

        public static System.Linq.Expressions.Expression<Func<barangay_trasnfer_of_funds, btf_DTO>>
            SELECT = x => new btf_DTO
            {
                id = x.id,
                region_code = x.region_code,
                prov_code = x.prov_code,
                city_code = x.city_code,
                brgy_code = x.brgy_code,
                batch_target = x.batch_target,
                modality_id = x.modality_id,
                cycle_id = x.cycle_id,
                modality_category_id = x.modality_category_id,
                erfr_project_id = x.erfr_project_id,
                budget_year_id = x.budget_year_id,
                tranche_id = x.tranche_id,
                check_number = x.check_number,
                date_transferred = x.date_transferred,
                date_requested = x.date_requested,
                date_downloaded = x.date_downloaded,
                account_number = x.account_number,
                lbp_branch = x.lbp_branch,
                amount_request = x.amount_request,
                amount_approved = x.amount_approved,
                title = x.title,
                fund_source_id = x.fund_source_id,
                amount_utilized = x.amount_utilized,
                actual_amount_utilized = x.actual_amount_utilized,
                actual_amount_liquidated = x.actual_amount_liquidated,
                is_taf = x.is_taf,
                is_savings = x.is_savings,
                is_incentive = x.is_incentive,
                is_lgu_led = x.is_lgu_led,
                remarks = x.remarks,
                sub_project_id = x.sub_project_id,
                refunded_amount = x.refunded_amount,
                type_of_refund_id = x.type_of_refund_id,
                check_number_ipcdd = x.check_number_ipcdd,
                or_number = x.or_number,
                or_date = x.or_date,
                will_not_request_second_tranche = x.will_not_request_second_tranche,
                last_updated_via_spi_by = x.last_updated_via_spi_by,
                last_updated_via_spi_date = x.last_updated_via_spi_date,
                created_by = x.created_by,
                created_date = x.created_date,
                last_updated_by = x.last_updated_by,
                last_updated_date = x.last_updated_date,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                is_deleted = x.is_deleted
            };

    }

    //4.0 SPI MULTIPLE VARIATION DATE
    public class spi_multiple_variation_date_DTO
    {
        public int id { get; set; }
        public int sub_project_id { get; set; }
        public DateTime? date_approved { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }

        public static System.Linq.Expressions.Expression<Func<spi_multiple_variation_date, spi_multiple_variation_date_DTO>>
            SELECT = x => new spi_multiple_variation_date_DTO
            {
                id = x.id,
                sub_project_id = x.sub_project_id,
                date_approved = x.date_approved,
                created_by = x.created_by,
                created_date = x.created_date,
                last_updated_by = x.last_updated_by,
                last_updated_date = x.last_updated_date,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                is_deleted = x.is_deleted
            };

    }

    //4.0 SPI DESKAPP OTHERS
    public class sp_deskapp_others_DTO
    {
        public Guid sp_deskapp_others_id { get; set; }
        public int sub_project_id { get; set; }
        public Guid sub_project_unique_id { get; set; }
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
        public string suspension_order_list { get; set; }
        public string resume_order_list { get; set; }
        public string variation_order_list { get; set; }        
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

        public static System.Linq.Expressions.Expression<Func<sp_deskapp_others, sp_deskapp_others_DTO>>
            SELECT = x => new sp_deskapp_others_DTO
            {
                sp_deskapp_others_id = x.sp_deskapp_others_id,
                sub_project_id = x.sub_project_id,
                sub_project_unique_id = x.sub_project_unique_id,
                no_target_families = x.no_target_families,
                no_target_pantawid_households = x.no_target_pantawid_households,
                no_target_pantawid_families = x.no_target_pantawid_families,
                no_target_slp_households = x.no_target_slp_households,
                no_target_slp_families = x.no_target_slp_families,
                no_target_ip_households = x.no_target_ip_households,
                no_target_ip_families = x.no_target_ip_families,
                no_target_male = x.no_target_male,
                no_target_female = x.no_target_female,
                target_male = x.target_male,
                target_female = x.target_female,
                suspension_order_list = x.suspension_order_list,
                resume_order_list = x.resume_order_list,
                variation_order_list = x.variation_order_list,
                community_formation_list = x.community_formation_list,
                created_by = x.created_by,
                created_date = x.created_date,
                last_modified_by = x.last_modified_by,
                last_modified_date = x.last_modified_date,
                is_deleted = x.is_deleted,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                push_status_id = x.push_status_id,
                push_date = x.push_date,
                approval_id = x.approval_id
            };

    }

    //4.0 SPI LAND ACQUISITION
    public class spi_land_acquisitionDTO
    {
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
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_date { get; set; }
        public string deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool? is_deleted { get; set; }

        public static System.Linq.Expressions.Expression<Func<spi_land_acquisition, spi_land_acquisitionDTO>>
            SELECT = x => new spi_land_acquisitionDTO
            {
                spi_land_acquisition_id = x.spi_land_acquisition_id,
                sub_project_id = x.sub_project_id,
                no_ESSC = x.no_ESSC,
                no_ESMP = x.no_ESMP,
                no_ECC = x.no_ECC,
                no_CNC = x.no_CNC,
                no_land_aq_deed_of_donation = x.no_land_aq_deed_of_donation,
                no_land_aq_unsfruct = x.no_land_aq_unsfruct,
                no_right_of_way_agreement = x.no_right_of_way_agreement,
                no_permit_to_construct_enter = x.no_permit_to_construct_enter,
                no_quit_claim = x.no_quit_claim,
                no_land_aq_blgu_resolution = x.no_land_aq_blgu_resolution,
                no_land_aq_deped_certification = x.no_land_aq_deped_certification,
                no_land_aq_other = x.no_land_aq_other,
                created_by = x.created_by,
                created_date = x.created_date,
                last_updated_by = x.last_updated_by,
                last_updated_date = x.last_updated_date,
                deleted_by = x.deleted_by,
                deleted_date = x.deleted_date,
                is_deleted = x.is_deleted
            };

    }

}
