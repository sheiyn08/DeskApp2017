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

}
