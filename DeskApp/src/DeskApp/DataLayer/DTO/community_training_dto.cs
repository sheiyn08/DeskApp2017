using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace DeskApp.DataLayer
{

    
    public class community_trainingDTO
    {

  

        public string lib_approval_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_enrollment_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_lgu_level_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_push_status_name { get; set; }
        public string lib_region_region_name { get; set; }
        public string lib_training_category_name { get; set; }
        public Guid community_training_id { get; set; }
        public System.String old_id { get; set; }

        public System.Int32 fund_source_id { get; set; }
        public System.Int32 cycle_id { get; set; }
        public System.String training_title { get; set; }
        public System.Int32 training_category_id { get; set; }
        public System.String venue { get; set; }
        public System.DateTime? date_conducted { get; set; }
        public System.Int32 duration { get; set; }
        public System.Int32 lgu_level_id { get; set; }
        public System.String reported_by { get; set; }
        public System.DateTime? start_date { get; set; }
        public System.DateTime? end_date { get; set; }
        public System.Boolean is_draft { get; set; }
        public System.Int32 enrollment_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }
        public System.Int32 created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public System.Int32? last_modified_by { get; set; }
        public System.DateTime? last_modified_date { get; set; }
        public System.Boolean? is_deleted { get; set; }
        public System.Int32? deleted_by { get; set; }
        public System.DateTime? deleted_date { get; set; }
        public System.Int32 push_status_id { get; set; }
        public System.DateTime? push_date { get; set; }
        public System.Int32 approval_id { get; set; }


        public int? no_brgy_rep { get; set; }
        public int? no_atn_male { get; set; }
        public int? no_atn_female { get; set; }
        public int? no_ip_male { get; set; }
        public int? no_ip_female { get; set; }
        public int? no_atn_pantawid { get; set; }
        public int? no_atn_slp { get; set; }


      
        

        public static System.Linq.Expressions.Expression<Func<community_training, community_trainingDTO>> SELECT =
            x => new community_trainingDTO
            {
                

                lib_approval_name = x.lib_approval.name,
                lib_brgy_brgy_name =  "",  // (?bool)x.lib_brgy, // : "", // x.lib_brgy.brgy_name,
                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_name = x.lib_cycle.name,
                lib_enrollment_name = x.lib_enrollment.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_lgu_level_name = x.lib_lgu_level.name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_push_status_name = x.lib_push_status.name,
                lib_region_region_name = x.lib_region.region_name,
                lib_training_category_name = x.lib_training_category.name,
                community_training_id = x.community_training_id,
                old_id = x.old_id,

                fund_source_id = x.fund_source_id,
                cycle_id = x.cycle_id,
                training_title = x.training_title,
                training_category_id = x.training_category_id,
                venue = x.venue,
                date_conducted = x.date_conducted,
                duration = x.duration,
                lgu_level_id = x.lgu_level_id,
                reported_by = x.reported_by,
                start_date = x.start_date,
                end_date = x.end_date,
                is_draft = x.is_draft,
                enrollment_id = x.enrollment_id,
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


            

            };

    }
}