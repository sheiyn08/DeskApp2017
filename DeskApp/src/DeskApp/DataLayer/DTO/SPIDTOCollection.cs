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

}
