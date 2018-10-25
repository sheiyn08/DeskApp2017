using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class brgy_assemblyDTO
    {
        public string lib_approval_name { get; set; }
        public string lib_barangay_assembly_purpose_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_enrollment_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_push_status_name { get; set; }
        public string lib_region_region_name { get; set; }
        public Guid brgy_assembly_id { get; set; }

        public System.String old_id { get; set; }
        public System.Int32 fund_source_id { get; set; }
        public System.Int32 cycle_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int brgy_code { get; set; }
        public System.String highlights { get; set; }
        public System.Int32 barangay_assembly_purpose_id { get; set; }
        public System.Int32 enrollment_id { get; set; }
        public System.Int32? no_families { get; set; }
        public System.Int32? no_household { get; set; }
        public System.Int32? no_atn_male { get; set; }
        public System.Int32? no_atn_female { get; set; }
        public System.Int32? no_ip_male { get; set; }
        public System.Int32? no_ip_female { get; set; }
        public System.Int32? no_old_male { get; set; }
        public System.Int32? no_old_female { get; set; }
        public System.Int32? no_pantawid_household { get; set; }
        public System.Int32? no_pantawid_family { get; set; }
        public System.Int32? no_slp_household { get; set; }
        public System.Int32? no_slp_family { get; set; }
        public System.Int32? no_ip_household { get; set; }
        public System.Int32? no_ip_family { get; set; }
        public System.Int32? total_household_in_barangay { get; set; }
        public System.Int32? total_families_in_barangay { get; set; }
        public System.Int32? no_lgu_male { get; set; }
        public System.Int32? no_lgu_female { get; set; }
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

        public static System.Linq.Expressions.Expression<Func<brgy_assembly, brgy_assemblyDTO>> SELECT =
            x => new brgy_assemblyDTO
            {
                lib_approval_name = x.lib_approval.name,
                lib_barangay_assembly_purpose_name = x.lib_barangay_assembly_purpose.name,
                lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_name = x.lib_cycle.name,
                lib_enrollment_name = x.lib_enrollment.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_push_status_name = x.lib_push_status.name,
                lib_region_region_name = x.lib_region.region_name,
                brgy_assembly_id = x.brgy_assembly_id,
                old_id = x.old_id,

                fund_source_id = x.fund_source_id,
                cycle_id = x.cycle_id,
                region_code = x.region_code,
                prov_code = x.prov_code,
                city_code = x.city_code,
                brgy_code = x.brgy_code,
                highlights = x.highlights,
                barangay_assembly_purpose_id = x.barangay_assembly_purpose_id,
                enrollment_id = x.enrollment_id,
                no_families = x.no_families,
                no_household = x.no_household,
                no_atn_male = x.no_atn_male,
                no_atn_female = x.no_atn_female,
                no_ip_male = x.no_ip_male,
                no_ip_female = x.no_ip_female,
                no_old_male = x.no_old_male,
                no_old_female = x.no_old_female,
                no_pantawid_household = x.no_pantawid_household,
                no_pantawid_family = x.no_pantawid_family,
                no_slp_household = x.no_slp_household,
                no_slp_family = x.no_slp_family,
                no_ip_household = x.no_ip_household,
                no_ip_family = x.no_ip_family,
                total_household_in_barangay = x.total_household_in_barangay,
                total_families_in_barangay = x.total_families_in_barangay,
                no_lgu_male = x.no_lgu_male,
                no_lgu_female = x.no_lgu_female,
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
