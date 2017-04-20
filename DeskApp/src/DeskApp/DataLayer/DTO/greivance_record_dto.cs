using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeskApp.DataLayer
{
    public class grievance_recordDTO
    {
        public string lib_approval_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_enrollment_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_grs_category_name { get; set; }
        public string lib_grs_complaint_subject_name { get; set; }
        public string lib_grs_feedback_name { get; set; }
        public string lib_grs_filling_mode_name { get; set; }
        public string lib_grs_form_name { get; set; }
        public string lib_grs_intake_level_name { get; set; }
        public string lib_grs_intake_officer_name { get; set; }
        public string lib_grs_nature_name { get; set; }
        public string lib_grs_resolution_status_name { get; set; }
        public string lib_grs_sender_designation_name { get; set; }
        public string lib_ip_group_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_region_region_name { get; set; }
        public string lib_sex_name { get; set; }
        public string pincos_complainant_first_name { get; set; }
        public Guid grievance_record_id { get; set; }
        public System.String old_id { get; set; }

        public System.Int32? pincos_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public  int? brgy_code { get; set; }
        public System.Int32? fund_source_id { get; set; }
        public System.Int32? cycle_id { get; set; }
        public System.Boolean? is_central_office_level_only { get; set; }
        public System.Boolean? is_field_office_level_only { get; set; }
        public System.Boolean? is_individual { get; set; }
        public System.Int32 grs_form_id { get; set; }
        public System.Int32 grs_intake_level_id { get; set; }
        public System.DateTime? date_intake { get; set; }
        public System.String intake_officer { get; set; }
        public System.String intake_officer_designation { get; set; }
        public System.Int32 grs_filling_mode_id { get; set; }
        public System.String grs_filling_mode_others { get; set; }
        public System.Int32? ip_group_id { get; set; }
        public System.String ip_group_other { get; set; }
        public System.String sender_name { get; set; }
        public System.Boolean sender_sex { get; set; }
        public System.String sender_organization { get; set; }
        public System.String sender_designation { get; set; }
        public System.Int32? grs_sender_designation_id { get; set; }
        public System.String sender_designation_other { get; set; }
        public System.String sender_contact_info { get; set; }
        public System.Int32 grs_resolution_status_id { get; set; }
        public System.DateTime? resolution_date { get; set; }
        public System.Int32? grs_feedback_id { get; set; }
        public System.Int32 grs_nature_id { get; set; }
        public System.Int32 grs_category_id { get; set; }
        public System.Int32 grs_complaint_subject_id { get; set; }
        public System.String grs_complaint_subject_others { get; set; }
        public System.String details { get; set; }
        public System.String actions { get; set; }
        public System.String recommendations { get; set; }
        public System.Int32? enrollment_id { get; set; }
        public System.String email { get; set; }
        public System.String cellphone { get; set; }
        public System.Boolean? req_province { get; set; }
        public System.Boolean? req_city { get; set; }
        public System.Boolean? req_brgy { get; set; }
        public System.Int32 sex_id { get; set; }
        public System.Boolean? is_ip { get; set; }
        public System.Boolean? is_anonymous { get; set; }
        public System.Int32 created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public System.Int32? last_modified_by { get; set; }
        public System.DateTime? last_modified_date { get; set; }
        public System.Boolean is_deleted { get; set; }
        public System.Int32? deleted_by { get; set; }
        public System.DateTime? deleted_date { get; set; }
        public System.Int32 push_status_id { get; set; }
        public System.DateTime? push_date { get; set; }
        public System.Int32 approval_id { get; set; }
        public System.String final_resolution { get; set; }
        public System.Int32? grs_intake_officer_id { get; set; }
        public System.Boolean? is_fund_source_applicable { get; set; }

        public static System.Linq.Expressions.Expression<Func<grievance_record, grievance_recordDTO>> SELECT =
            x => new grievance_recordDTO
            {
                lib_approval_name = x.lib_approval.name,
                lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_name = x.lib_cycle.name,
                lib_enrollment_name = x.lib_enrollment.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_grs_category_name = x.lib_grs_category.name,
                lib_grs_complaint_subject_name = x.lib_grs_complaint_subject.name,
                lib_grs_feedback_name = x.lib_grs_feedback.name,
                lib_grs_filling_mode_name = x.lib_grs_filling_mode.name,
                lib_grs_form_name = x.lib_grs_form.name,
                lib_grs_intake_level_name = x.lib_grs_intake_level.name,
                lib_grs_intake_officer_name = x.lib_grs_intake_officer.name,
                lib_grs_nature_name = x.lib_grs_nature.name,
                lib_grs_resolution_status_name = x.lib_grs_resolution_status.name,
                lib_grs_sender_designation_name = x.lib_grs_sender_designation.name,
                lib_ip_group_name = x.lib_ip_group.name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_region_region_name = x.lib_region.region_name,
                lib_sex_name = x.lib_sex.name,

                grievance_record_id = x.grievance_record_id,
                old_id = x.old_id,

                pincos_id = x.pincos_id,
                region_code = x.region_code,
                prov_code = x.prov_code,
                city_code = x.city_code,
                brgy_code = x.brgy_code,
                fund_source_id = x.fund_source_id,
                cycle_id = x.cycle_id,
                is_central_office_level_only = x.is_central_office_level_only,
                is_field_office_level_only = x.is_field_office_level_only,
                is_individual = x.is_individual,
                grs_form_id = x.grs_form_id,
                grs_intake_level_id = x.grs_intake_level_id,
                date_intake = x.date_intake,
                intake_officer = x.intake_officer,
                intake_officer_designation = x.intake_officer_designation,
                grs_filling_mode_id = x.grs_filling_mode_id,
                grs_filling_mode_others = x.grs_filling_mode_others,
                ip_group_id = x.ip_group_id,
                ip_group_other = x.ip_group_other,
                sender_name = x.sender_name,
                sender_sex = x.sender_sex,
                sender_organization = x.sender_organization,
                sender_designation = x.sender_designation,
                grs_sender_designation_id = x.grs_sender_designation_id,
                sender_designation_other = x.sender_designation_other,
                sender_contact_info = x.sender_contact_info,
                grs_resolution_status_id = x.grs_resolution_status_id,
                resolution_date = x.resolution_date,
                grs_feedback_id = x.grs_feedback_id,
                grs_nature_id = x.grs_nature_id,
                grs_category_id = x.grs_category_id,
                grs_complaint_subject_id = x.grs_complaint_subject_id,
                grs_complaint_subject_others = x.grs_complaint_subject_others,
                details = x.details,
                actions = x.actions,
                recommendations = x.recommendations,
                enrollment_id = x.enrollment_id,
                email = x.email,
                cellphone = x.cellphone,
                req_province = x.req_province,
                req_city = x.req_city,
                req_brgy = x.req_brgy,
                sex_id = x.sex_id,
                is_ip = x.is_ip,
                is_anonymous = x.is_anonymous,
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
                final_resolution = x.final_resolution,
                grs_intake_officer_id = x.grs_intake_officer_id,
                is_fund_source_applicable = x.is_fund_source_applicable,
            };

    }

    public class grs_installationDTO
    {
        public string lib_approval_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_enrollment_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_lgu_level_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_region_region_name { get; set; }
        public System.Guid grs_installation_id { get; set; }
        public System.String old_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }
        public System.Int32 fund_source_id { get; set; }
        public System.Int32 cycle_id { get; set; }
        public System.Int32 enrollment_id { get; set; }
        public System.Int32 lgu_level_id { get; set; }
        public System.DateTime? date_voliden { get; set; }
        public System.DateTime? date_infodess { get; set; }
        public System.DateTime? date_orientation { get; set; }
        public System.DateTime? date_ffcomm { get; set; }
        public System.DateTime? date_training { get; set; }
        public System.DateTime? date_inspect { get; set; }
        public System.Int32? no_manuals { get; set; }
        public System.Int32? other_mat { get; set; }
        public System.Int32? no_brochures { get; set; }
        public System.Int32? no_tarpauline { get; set; }
        public System.Int32? no_posters { get; set; }
        public System.DateTime? date_meansrept { get; set; }
        public System.Boolean? is_boxinstalled { get; set; }
        public System.String phone_no { get; set; }
        public System.String address { get; set; }
        public System.String remarks { get; set; }
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

        public static System.Linq.Expressions.Expression<Func<grs_installation, grs_installationDTO>> SELECT =
            x => new grs_installationDTO
            {
                lib_approval_name = x.lib_approval.name,
                lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_name = x.lib_cycle.name,
                // lib_enrollment_name = x.lib_enrollment.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_lgu_level_name = x.lib_lgu_level.name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_region_region_name = x.lib_region.region_nick,
                grs_installation_id = x.grs_installation_id,
                old_id = x.old_id,
                region_code = x.region_code,
                prov_code = x.prov_code,
                city_code = x.city_code,
                brgy_code = x.brgy_code,
                fund_source_id = x.fund_source_id,
                cycle_id = x.cycle_id,
                // enrollment_id = x.enrollment_id,
                lgu_level_id = x.lgu_level_id,
                date_voliden = x.date_voliden,
                date_infodess = x.date_infodess,
                date_orientation = x.date_orientation,
                date_ffcomm = x.date_ffcomm,
                date_training = x.date_training,
                date_inspect = x.date_inspect,
                no_manuals = x.no_manuals,
                other_mat = x.other_mat,
                no_brochures = x.no_brochures,
                no_tarpauline = x.no_tarpauline,
                no_posters = x.no_posters,
                date_meansrept = x.date_meansrept,
                is_boxinstalled = x.is_boxinstalled,
                phone_no = x.phone_no,
                address = x.address,
                remarks = x.remarks,
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
 