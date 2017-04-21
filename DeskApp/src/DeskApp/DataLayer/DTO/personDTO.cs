using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer 
{
    public class person_profileDTO
    {
        public string lib_approval_name { get; set; }
        public string lib_blgu_position_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_civil_status_name { get; set; }
        public string lib_education_attainment_name { get; set; }
        public string lib_ip_group_name { get; set; }
        public string lib_occupation_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_push_status_name { get; set; }
        public string lib_region_region_name { get; set; }
        public System.Guid person_profile_id { get; set; }
        public System.String old_id { get; set; }
        public System.String household_id { get; set; }
        public System.Int32? entry_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }
        public System.String old_address { get; set; }
        public System.String last_name { get; set; }
        public System.String first_name { get; set; }
        public System.String middle_name { get; set; }
        public System.DateTime? birthdate { get; set; }
        public System.Boolean sex { get; set; }
        public System.Int32? civil_status_id { get; set; }
        public System.Int32? no_children { get; set; }
        public System.String contact_no { get; set; }
        public System.Boolean? is_volunteer { get; set; }
        public System.Boolean? is_ip { get; set; }
        public System.Boolean? is_ipleader { get; set; }
        public System.Boolean? is_pantawid { get; set; }
        public System.Boolean? is_slp { get; set; }
        public System.Boolean? is_lguofficial { get; set; }
        public System.Boolean? is_worker { get; set; }
        public System.Int32? lgu_position_id { get; set; }
        public System.Int32? education_attainment_id { get; set; }
        public System.Int32? occupation_id { get; set; }
        public string sector { get; set; }
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
        public System.Int32? ip_group_id { get; set; }

        public static System.Linq.Expressions.Expression<Func<person_profile, person_profileDTO>> SELECT =
            x => new person_profileDTO
            {
                //lib_approval_name = x.lib_approval.name,
                //lib_blgu_position_name = x.lib_blgu_position.name,
                 lib_brgy_brgy_name =  x.brgy_code.ToString() ,// ? x.lib_brgy.brgy_name : "",
                lib_city_city_name = x.lib_city.city_name,
                //lib_civil_status_name = x.lib_civil_status.name,
                //lib_education_attainment_name = x.lib_education_attainment.name,
                //lib_ip_group_name = x.lib_ip_group.name,
                //lib_occupation_name = x.lib_occupation.name,
                lib_province_prov_name = x.lib_province.prov_name,
                //lib_push_status_name = x.lib_push_status.name,
                lib_region_region_name = x.lib_region.region_name,
                person_profile_id = x.person_profile_id,
                old_id = x.old_id,
                household_id = x.household_id,
                entry_id = x.entry_id,
                region_code = x.region_code,
                prov_code = x.prov_code,
                city_code = x.city_code,
                brgy_code = x.brgy_code,
                old_address = x.old_address,
                last_name = x.last_name,
                first_name = x.first_name,
                middle_name = x.middle_name,
                birthdate = x.birthdate,
                sex = x.sex.Value,
                civil_status_id = x.civil_status_id,
                no_children = x.no_children,
                contact_no = x.contact_no,
                is_volunteer = x.is_volunteer,
                is_ip = x.is_ip,
                is_ipleader = x.is_ipleader,
                is_pantawid = x.is_pantawid,
                is_slp = x.is_slp,
                is_lguofficial = x.is_lguofficial,
                is_worker = x.is_worker,
                lgu_position_id = x.lgu_position_id,
                education_attainment_id = x.education_attainment_id,
                occupation_id = x.occupation_id,
                sector = x.sector,
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
                ip_group_id = x.ip_group_id,
            };

    }
}
