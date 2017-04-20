using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeskApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lib_condition",
                columns: table => new
                {
                    condition_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_condition", x => x.condition_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_disaster_type",
                columns: table => new
                {
                    disaster_type_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_disaster_type", x => x.disaster_type_id);
                });

            migrationBuilder.CreateTable(
                name: "mov_list",
                columns: table => new
                {
                    mov_list_id = table.Column<int>(nullable: false),
                    max = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    table_name_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mov_list", x => x.mov_list_id);
                });

            migrationBuilder.CreateTable(
                name: "report_list",
                columns: table => new
                {
                    report_list_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    table_name_id = table.Column<int>(nullable: false),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_list", x => x.report_list_id);
                });

            migrationBuilder.CreateTable(
                name: "erfr_sub_project",
                columns: table => new
                {
                    SPID = table.Column<int>(nullable: false),
                    Barangay = table.Column<string>(nullable: true),
                    Municipality = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Year = table.Column<decimal>(nullable: true),
                    community_direct_cost = table.Column<decimal>(nullable: true),
                    community_indirect_cost = table.Column<decimal>(nullable: true),
                    cycle = table.Column<decimal>(nullable: true),
                    date_of_mibf = table.Column<DateTime>(nullable: true),
                    first_tranch_amount = table.Column<decimal>(nullable: true),
                    first_tranch_date_required = table.Column<DateTime>(nullable: true),
                    fund_source = table.Column<string>(nullable: true),
                    grant_amount_contingency_cost = table.Column<decimal>(nullable: true),
                    grant_amount_direct_cost = table.Column<decimal>(nullable: true),
                    grant_amount_indirect_cost = table.Column<decimal>(nullable: true),
                    grouping = table.Column<string>(nullable: true),
                    lcc_blgu_direct_cost = table.Column<decimal>(nullable: true),
                    lcc_blgu_indirect_cost = table.Column<decimal>(nullable: true),
                    mlgu_direct_cost = table.Column<decimal>(nullable: true),
                    mlgu_indirect_cost = table.Column<decimal>(nullable: true),
                    nscb_code = table.Column<string>(nullable: true),
                    plgu_others_direct_cost = table.Column<decimal>(nullable: true),
                    plgu_others_indirect_cost = table.Column<decimal>(nullable: true),
                    second_tranch_amount = table.Column<decimal>(nullable: true),
                    second_tranch_date_required = table.Column<DateTime>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    third_tranch_amount = table.Column<decimal>(nullable: true),
                    third_tranch_date_required = table.Column<DateTime>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    total_grant = table.Column<decimal>(nullable: true),
                    total_lcc = table.Column<decimal>(nullable: true),
                    total_lcc_cash_direct_cost = table.Column<decimal>(nullable: true),
                    total_lcc_cash_indirect_cost = table.Column<decimal>(nullable: true),
                    total_lcc_in_kind_direct_cost = table.Column<decimal>(nullable: true),
                    total_lcc_in_kind_indirect_cost = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_erfr_sub_project", x => x.SPID);
                });

            migrationBuilder.CreateTable(
                name: "lib_approval",
                columns: table => new
                {
                    approval_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_approval", x => x.approval_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_barangay_assembly_purpose",
                columns: table => new
                {
                    barangay_assembly_purpose_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_barangay_assembly_purpose", x => x.barangay_assembly_purpose_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_brgy",
                columns: table => new
                {
                    brgy_code = table.Column<int>(nullable: false),
                    brgy_mode = table.Column<byte>(nullable: true),
                    brgy_name = table.Column<string>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    district = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_brgy", x => x.brgy_code);
                });

            migrationBuilder.CreateTable(
                name: "lib_city",
                columns: table => new
                {
                    city_code = table.Column<int>(nullable: false),
                    city_name = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_city", x => x.city_code);
                });

            migrationBuilder.CreateTable(
                name: "lib_civil_status",
                columns: table => new
                {
                    civil_status_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_civil_status", x => x.civil_status_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_eca_type",
                columns: table => new
                {
                    eca_type_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_eca_type", x => x.eca_type_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_education_attainment",
                columns: table => new
                {
                    education_attainment_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_education_attainment", x => x.education_attainment_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_enrollment", x => x.enrollment_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_ers_current_work",
                columns: table => new
                {
                    ers_current_work_id = table.Column<int>(nullable: false),
                    is_skilled = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_ers_current_work", x => x.ers_current_work_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_ers_delivery_mode",
                columns: table => new
                {
                    ers_delivery_mode_id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_ers_delivery_mode", x => x.ers_delivery_mode_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_fund_source",
                columns: table => new
                {
                    fund_source_id = table.Column<int>(nullable: false),
                    geotagging_fund_source_id = table.Column<int>(nullable: true),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_fund_source", x => x.fund_source_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_category",
                columns: table => new
                {
                    grs_category_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_category", x => x.grs_category_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_complainant_position",
                columns: table => new
                {
                    grs_complainant_position_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_complainant_position", x => x.grs_complainant_position_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_complaint_subject",
                columns: table => new
                {
                    grs_complaint_subject_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_complaint_subject", x => x.grs_complaint_subject_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_feedback",
                columns: table => new
                {
                    grs_feedback_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_feedback", x => x.grs_feedback_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_filling_mode",
                columns: table => new
                {
                    grs_filling_mode_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_filling_mode", x => x.grs_filling_mode_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_form",
                columns: table => new
                {
                    grs_form_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_form", x => x.grs_form_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_intake_level",
                columns: table => new
                {
                    grs_intake_level_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    office_level_id = table.Column<int>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_intake_level", x => x.grs_intake_level_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_intake_officer",
                columns: table => new
                {
                    grs_intake_officer_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    office_level_id = table.Column<int>(nullable: false),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_intake_officer", x => x.grs_intake_officer_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_nature",
                columns: table => new
                {
                    grs_nature_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_nature", x => x.grs_nature_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_pincos_actor",
                columns: table => new
                {
                    grs_pincos_actor_id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_pincos_actor", x => x.grs_pincos_actor_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_resolution_status",
                columns: table => new
                {
                    grs_resolution_status_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_resolution_status", x => x.grs_resolution_status_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_grs_sender_designation",
                columns: table => new
                {
                    grs_sender_designation_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sort_order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_grs_sender_designation", x => x.grs_sender_designation_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_implementation_status",
                columns: table => new
                {
                    implementation_status_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_implementation_status", x => x.implementation_status_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_ip_group",
                columns: table => new
                {
                    ip_group_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_ip_group", x => x.ip_group_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_kalahi_committee",
                columns: table => new
                {
                    kalahi_committee_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_kalahi_committee", x => x.kalahi_committee_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_lgu_level",
                columns: table => new
                {
                    lgu_level_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_lgu_level", x => x.lgu_level_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_lgu_position",
                columns: table => new
                {
                    lgu_position_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    lgu_level_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_lgu_position", x => x.lgu_position_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_major_classification",
                columns: table => new
                {
                    major_classification_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_major_classification", x => x.major_classification_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_occupation",
                columns: table => new
                {
                    occupation_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true),
                    sub_groups = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_occupation", x => x.occupation_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_organization",
                columns: table => new
                {
                    organization_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_organization", x => x.organization_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_physical_status",
                columns: table => new
                {
                    physical_status_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_physical_status", x => x.physical_status_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_physical_status_category",
                columns: table => new
                {
                    physical_status_category_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_physical_status_category", x => x.physical_status_category_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_province",
                columns: table => new
                {
                    prov_code = table.Column<int>(nullable: false),
                    prov_name = table.Column<string>(nullable: true),
                    region_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_province", x => x.prov_code);
                });

            migrationBuilder.CreateTable(
                name: "lib_psa_problem_category",
                columns: table => new
                {
                    psa_problem_category_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_psa_problem_category", x => x.psa_problem_category_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_psa_solution_category",
                columns: table => new
                {
                    psa_solution_category_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_psa_solution_category", x => x.psa_solution_category_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_push_status",
                columns: table => new
                {
                    push_status_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_push_status", x => x.push_status_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_region",
                columns: table => new
                {
                    region_code = table.Column<int>(nullable: false),
                    region_name = table.Column<string>(nullable: true),
                    region_nick = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_region", x => x.region_code);
                });

            migrationBuilder.CreateTable(
                name: "lib_sector",
                columns: table => new
                {
                    sector_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_sector", x => x.sector_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_sex",
                columns: table => new
                {
                    sex_id = table.Column<int>(nullable: false),
                    desktop_value = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_sex", x => x.sex_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_source_income",
                columns: table => new
                {
                    source_income_id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_source_income", x => x.source_income_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_training_category",
                columns: table => new
                {
                    training_category_id = table.Column<int>(nullable: false),
                    brgy_sort_order = table.Column<int>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    is_accelerated = table.Column<bool>(nullable: true),
                    is_active = table.Column<bool>(nullable: true),
                    is_barangay = table.Column<bool>(nullable: true),
                    is_ceac = table.Column<bool>(nullable: true),
                    is_ceac_tracking_only = table.Column<bool>(nullable: true),
                    is_municipality = table.Column<bool>(nullable: true),
                    is_regular = table.Column<bool>(nullable: true),
                    muni_sort_order = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    return_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_training_category", x => x.training_category_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_transpo_mode",
                columns: table => new
                {
                    transpo_mode_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_transpo_mode", x => x.transpo_mode_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_volunteer_committee",
                columns: table => new
                {
                    volunteer_committee_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_volunteer_committee", x => x.volunteer_committee_id);
                });

            migrationBuilder.CreateTable(
                name: "lib_volunteer_committee_position",
                columns: table => new
                {
                    volunteer_committee_position_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_volunteer_committee_position", x => x.volunteer_committee_position_id);
                });

            migrationBuilder.CreateTable(
                name: "SPPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Altitude = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Day = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    GetDateTaken = table.Column<string>(nullable: true),
                    GpsDateTimeStamp = table.Column<string>(nullable: true),
                    IsOtherTypeOfProject = table.Column<bool>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Month = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UniqueName = table.Column<Guid>(nullable: false),
                    Year = table.Column<int>(nullable: true),
                    act_approved = table.Column<string>(nullable: true),
                    act_last_approved = table.Column<DateTime>(nullable: true),
                    album_id = table.Column<int>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    co_approved = table.Column<string>(nullable: true),
                    co_last_approved = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<string>(nullable: true),
                    geo_category_id = table.Column<int>(nullable: true),
                    has_backup = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_missing_photo = table.Column<bool>(nullable: true),
                    lib_functionality_id = table.Column<int>(nullable: false),
                    other_project_type_id = table.Column<int>(nullable: true),
                    photo_description_id = table.Column<int>(nullable: true),
                    photo_description_id_new = table.Column<int>(nullable: true),
                    photo_position_id = table.Column<int>(nullable: true),
                    reject_id = table.Column<int>(nullable: true),
                    rme_approved = table.Column<string>(nullable: true),
                    rme_last_approved = table.Column<DateTime>(nullable: true),
                    sequence_id = table.Column<int>(nullable: true),
                    srpmo_approved = table.Column<string>(nullable: true),
                    srpmo_last_approved = table.Column<DateTime>(nullable: true),
                    sub_project_id = table.Column<int>(nullable: false),
                    sub_project_unique_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPPhoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_name",
                columns: table => new
                {
                    table_name_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_name", x => x.table_name_id);
                });

            migrationBuilder.CreateTable(
                name: "talakayan_year",
                columns: table => new
                {
                    talakayan_yr_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_talakayan_year", x => x.talakayan_yr_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "sub_project_pre_disaster",
                columns: table => new
                {
                    sub_project_pre_disaster_id = table.Column<Guid>(nullable: false),
                    can_be_used_as_evac_site = table.Column<bool>(nullable: true),
                    capacity = table.Column<int>(nullable: false),
                    condition_id = table.Column<int>(nullable: false),
                    contact_no = table.Column<string>(nullable: true),
                    contact_person = table.Column<string>(nullable: true),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    date_assessed = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    sub_project_id = table.Column<int>(nullable: false),
                    with_fist_aid_kit = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_project_pre_disaster", x => x.sub_project_pre_disaster_id);
                    table.ForeignKey(
                        name: "FK_sub_project_pre_disaster_lib_condition_condition_id",
                        column: x => x.condition_id,
                        principalTable: "lib_condition",
                        principalColumn: "condition_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "disaster",
                columns: table => new
                {
                    disaster_id = table.Column<Guid>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    disaster_type_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disaster", x => x.disaster_id);
                    table.ForeignKey(
                        name: "FK_disaster_lib_disaster_type_disaster_type_id",
                        column: x => x.disaster_type_id,
                        principalTable: "lib_disaster_type",
                        principalColumn: "disaster_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attached_document",
                columns: table => new
                {
                    attached_document_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    mov_list_id = table.Column<int>(nullable: false),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    record_id = table.Column<Guid>(nullable: false),
                    region_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attached_document", x => x.attached_document_id);
                    table.ForeignKey(
                        name: "FK_attached_document_mov_list_mov_list_id",
                        column: x => x.mov_list_id,
                        principalTable: "mov_list",
                        principalColumn: "mov_list_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_project_ers",
                columns: table => new
                {
                    sub_project_ers_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<string>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    date_ended = table.Column<DateTime>(nullable: true),
                    date_reported = table.Column<DateTime>(nullable: true),
                    date_started = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    ers_delivery_mode_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    sub_project_id = table.Column<int>(nullable: true),
                    sub_project_unique_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_project_ers", x => x.sub_project_ers_id);
                    table.ForeignKey(
                        name: "FK_sub_project_ers_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_ers_lib_ers_delivery_mode_ers_delivery_mode_id",
                        column: x => x.ers_delivery_mode_id,
                        principalTable: "lib_ers_delivery_mode",
                        principalColumn: "ers_delivery_mode_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lib_cycle",
                columns: table => new
                {
                    cycle_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_cycle", x => x.cycle_id);
                    table.ForeignKey(
                        name: "FK_lib_cycle_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lib_project_type",
                columns: table => new
                {
                    project_type_id = table.Column<int>(nullable: false),
                    deleted = table.Column<int>(nullable: true),
                    geo_category_id = table.Column<int>(nullable: true),
                    major_classification_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    phy_construction_unit = table.Column<string>(nullable: true),
                    phy_construction_unit_secondary = table.Column<string>(nullable: true),
                    phy_has_construction_target = table.Column<bool>(nullable: true),
                    phy_has_construction_target_secondary = table.Column<bool>(nullable: true),
                    phy_has_improvement_target = table.Column<bool>(nullable: true),
                    phy_has_improvement_target_secondary = table.Column<bool>(nullable: true),
                    phy_has_purchase_target = table.Column<bool>(nullable: true),
                    phy_has_purchase_target_secondary = table.Column<bool>(nullable: true),
                    phy_has_rehabilitation_target = table.Column<bool>(nullable: true),
                    phy_has_rehabilitation_target_secondary = table.Column<bool>(nullable: true),
                    phy_improvement_unit = table.Column<string>(nullable: true),
                    phy_improvement_unit_secondary = table.Column<string>(nullable: true),
                    phy_purchase_unit = table.Column<string>(nullable: true),
                    phy_purchase_unit_secondary = table.Column<string>(nullable: true),
                    phy_rehabilitation_unit = table.Column<string>(nullable: true),
                    phy_rehabilitation_unit_secondary = table.Column<string>(nullable: true),
                    project_type_id_old = table.Column<string>(nullable: true),
                    sub_class_id = table.Column<int>(nullable: true),
                    tsunit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lib_project_type", x => x.project_type_id);
                    table.ForeignKey(
                        name: "FK_lib_project_type_lib_major_classification_major_classification_id",
                        column: x => x.major_classification_id,
                        principalTable: "lib_major_classification",
                        principalColumn: "major_classification_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "psa_solution",
                columns: table => new
                {
                    psa_solution_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    psa_problem_id = table.Column<Guid>(nullable: false),
                    psa_solution_category_id = table.Column<int>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    solution = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_psa_solution", x => x.psa_solution_id);
                    table.ForeignKey(
                        name: "FK_psa_solution_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_psa_solution_lib_psa_solution_category_psa_solution_category_id",
                        column: x => x.psa_solution_category_id,
                        principalTable: "lib_psa_solution_category",
                        principalColumn: "psa_solution_category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "brgy_eca",
                columns: table => new
                {
                    brgy_eca_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    eca_type_id = table.Column<int>(nullable: false),
                    effects = table.Column<string>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brgy_eca", x => x.brgy_eca_id);
                    table.ForeignKey(
                        name: "FK_brgy_eca_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_eca_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_eca_lib_eca_type_eca_type_id",
                        column: x => x.eca_type_id,
                        principalTable: "lib_eca_type",
                        principalColumn: "eca_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_eca_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_eca_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dof_blgf_financial_data",
                columns: table => new
                {
                    dof_blgf_financial_data_record_id = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    expenditures_economic_services = table.Column<int>(nullable: true),
                    expenditures_educ_culture_etc = table.Column<int>(nullable: true),
                    expenditures_gen_public_services = table.Column<int>(nullable: true),
                    expenditures_health_services = table.Column<int>(nullable: true),
                    expenditures_housing_comm_devt = table.Column<int>(nullable: true),
                    expenditures_labor_and_employment = table.Column<int>(nullable: true),
                    expenditures_other_purposes = table.Column<int>(nullable: true),
                    expenditures_social_welfare_services = table.Column<int>(nullable: true),
                    extraordinary_receipts = table.Column<int>(nullable: true),
                    inter_local_transfers = table.Column<int>(nullable: true),
                    ira_share = table.Column<int>(nullable: true),
                    locally_shared_revenues = table.Column<int>(nullable: true),
                    other_revenues_total = table.Column<int>(nullable: true),
                    other_shares_natl_tax = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    psgc_code = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    total_lgu_income = table.Column<int>(nullable: true),
                    year_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dof_blgf_financial_data", x => x.dof_blgf_financial_data_record_id);
                    table.ForeignKey(
                        name: "FK_dof_blgf_financial_data_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dof_blgf_financial_data_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dof_blgf_financial_data_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lgpms_data",
                columns: table => new
                {
                    lgpms_data_id = table.Column<int>(nullable: false),
                    administrative_governance_2009 = table.Column<int>(nullable: true),
                    administrative_governance_2010 = table.Column<int>(nullable: true),
                    administrative_governance_2011 = table.Column<int>(nullable: true),
                    administrative_governance_2012 = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    economic_governance_2009 = table.Column<int>(nullable: true),
                    economic_governance_2010 = table.Column<int>(nullable: true),
                    economic_governance_2011 = table.Column<int>(nullable: true),
                    economic_governance_2012 = table.Column<int>(nullable: true),
                    environmental_governance_2009 = table.Column<int>(nullable: true),
                    environmental_governance_2010 = table.Column<int>(nullable: true),
                    environmental_governance_2011 = table.Column<int>(nullable: true),
                    environmental_governance_2012 = table.Column<int>(nullable: true),
                    overall_performance_index_2009 = table.Column<int>(nullable: true),
                    overall_performance_index_2010 = table.Column<int>(nullable: true),
                    overall_performance_index_2011 = table.Column<int>(nullable: true),
                    overall_performance_index_2012 = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    psgc_code = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    social_governance_2009 = table.Column<int>(nullable: true),
                    social_governance_2010 = table.Column<int>(nullable: true),
                    social_governance_2011 = table.Column<int>(nullable: true),
                    social_governance_2012 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2009 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2010 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2011 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2012 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lgpms_data", x => x.lgpms_data_id);
                    table.ForeignKey(
                        name: "FK_lgpms_data_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lgpms_data_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lgpms_data_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_profile",
                columns: table => new
                {
                    person_profile_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    birthdate = table.Column<DateTime>(nullable: true),
                    brgy_code = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    civil_status_id = table.Column<int>(nullable: true),
                    contact_no = table.Column<string>(nullable: true),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    date_appointment = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    education_attainment_id = table.Column<int>(nullable: true),
                    entry_id = table.Column<int>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    household_id = table.Column<string>(nullable: true),
                    ip_group_id = table.Column<int>(nullable: true),
                    is_bdc = table.Column<bool>(nullable: true),
                    is_bspmc = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_ip = table.Column<bool>(nullable: true),
                    is_ipleader = table.Column<bool>(nullable: true),
                    is_lguofficial = table.Column<bool>(nullable: true),
                    is_mdc = table.Column<bool>(nullable: true),
                    is_pantawid = table.Column<bool>(nullable: true),
                    is_pantawid_leader = table.Column<bool>(nullable: true),
                    is_sector_academe = table.Column<bool>(nullable: true),
                    is_sector_business = table.Column<bool>(nullable: true),
                    is_sector_farmer = table.Column<bool>(nullable: true),
                    is_sector_fisherfolks = table.Column<bool>(nullable: true),
                    is_sector_government = table.Column<bool>(nullable: true),
                    is_sector_ip = table.Column<bool>(nullable: true),
                    is_sector_ngo = table.Column<bool>(nullable: true),
                    is_sector_po = table.Column<bool>(nullable: true),
                    is_sector_pwd = table.Column<bool>(nullable: true),
                    is_sector_religios = table.Column<bool>(nullable: true),
                    is_sector_senior = table.Column<bool>(nullable: true),
                    is_sector_women = table.Column<bool>(nullable: true),
                    is_sector_youth = table.Column<bool>(nullable: true),
                    is_slp = table.Column<bool>(nullable: true),
                    is_slp_leader = table.Column<bool>(nullable: true),
                    is_volunteer = table.Column<bool>(nullable: true),
                    is_worker = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    last_sync_source_id = table.Column<int>(nullable: true),
                    lgu_position_id = table.Column<int>(nullable: true),
                    middle_name = table.Column<string>(nullable: true),
                    no_children = table.Column<int>(nullable: true),
                    occupation_id = table.Column<int>(nullable: true),
                    old_address = table.Column<string>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    other_membership = table.Column<string>(nullable: true),
                    other_training = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    sector = table.Column<string>(nullable: true),
                    sex = table.Column<bool>(nullable: false),
                    sitio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_profile", x => x.person_profile_id);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_civil_status_civil_status_id",
                        column: x => x.civil_status_id,
                        principalTable: "lib_civil_status",
                        principalColumn: "civil_status_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_education_attainment_education_attainment_id",
                        column: x => x.education_attainment_id,
                        principalTable: "lib_education_attainment",
                        principalColumn: "education_attainment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_ip_group_ip_group_id",
                        column: x => x.ip_group_id,
                        principalTable: "lib_ip_group",
                        principalColumn: "ip_group_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_lgu_position_lgu_position_id",
                        column: x => x.lgu_position_id,
                        principalTable: "lib_lgu_position",
                        principalColumn: "lgu_position_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_occupation_occupation_id",
                        column: x => x.occupation_id,
                        principalTable: "lib_occupation",
                        principalColumn: "occupation_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_profile_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "means_of_verification",
                columns: table => new
                {
                    means_of_verification_id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    max = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    table_name_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_means_of_verification", x => x.means_of_verification_id);
                    table.ForeignKey(
                        name: "FK_means_of_verification_table_name_table_name_id",
                        column: x => x.table_name_id,
                        principalTable: "table_name",
                        principalColumn: "table_name_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "talakayan_eval",
                columns: table => new
                {
                    talakayan_evaluation_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    evaluation_date_end = table.Column<DateTime>(nullable: true),
                    evaluation_date_start = table.Column<DateTime>(nullable: true),
                    evaluation_form_version = table.Column<int>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    is_male = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    participant_type = table.Column<int>(nullable: true),
                    person_name = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    talakayan_date_end = table.Column<DateTime>(nullable: true),
                    talakayan_date_start = table.Column<DateTime>(nullable: true),
                    talakayan_name = table.Column<string>(nullable: true),
                    talakayan_venue = table.Column<string>(nullable: true),
                    talakayan_yr_id = table.Column<int>(nullable: false),
                    v2015_evaluation_a = table.Column<int>(nullable: false),
                    v2015_evaluation_b = table.Column<string>(nullable: true),
                    v2015_obj_a = table.Column<int>(nullable: false),
                    v2015_obj_b = table.Column<int>(nullable: false),
                    v2015_partsoftalakayan_part1 = table.Column<int>(nullable: false),
                    v2015_partsoftalakayan_part2 = table.Column<int>(nullable: false),
                    v2015_partsoftalakayan_part3 = table.Column<int>(nullable: false),
                    v2015_partsoftalakayan_part4 = table.Column<int>(nullable: false),
                    v2015_partsoftalakayan_part5 = table.Column<int>(nullable: false),
                    v2015_satisfaction_a = table.Column<int>(nullable: false),
                    v2015_satisfaction_a_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_b = table.Column<int>(nullable: false),
                    v2015_satisfaction_b_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_c = table.Column<int>(nullable: false),
                    v2015_satisfaction_c_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_d = table.Column<int>(nullable: false),
                    v2015_satisfaction_d_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_e = table.Column<int>(nullable: false),
                    v2015_satisfaction_e_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_f = table.Column<int>(nullable: false),
                    v2015_satisfaction_f_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_g = table.Column<int>(nullable: false),
                    v2015_satisfaction_g_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_h = table.Column<int>(nullable: false),
                    v2015_satisfaction_h_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_i = table.Column<int>(nullable: false),
                    v2015_satisfaction_i_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_j = table.Column<int>(nullable: false),
                    v2015_satisfaction_j_remarks = table.Column<string>(nullable: true),
                    v2015_satisfaction_k = table.Column<int>(nullable: false),
                    v2015_satisfaction_k_remarks = table.Column<string>(nullable: true),
                    v2015_timeallotment_a_part1 = table.Column<int>(nullable: false),
                    v2015_timeallotment_a_part2 = table.Column<int>(nullable: false),
                    v2015_timeallotment_a_part3 = table.Column<int>(nullable: false),
                    v2015_timeallotment_a_part4 = table.Column<int>(nullable: false),
                    v2015_timeallotment_a_part5 = table.Column<int>(nullable: false),
                    v2015_timeallotment_b_part1 = table.Column<int>(nullable: false),
                    v2015_timeallotment_b_part2 = table.Column<int>(nullable: false),
                    v2015_timeallotment_b_part3 = table.Column<int>(nullable: false),
                    v2015_timeallotment_b_part4 = table.Column<int>(nullable: false),
                    v2015_timeallotment_b_part5 = table.Column<int>(nullable: false),
                    v2015_venue_a = table.Column<int>(nullable: false),
                    v2015_venue_b = table.Column<int>(nullable: false),
                    v2015_venue_c = table.Column<int>(nullable: false),
                    v2015_venue_d = table.Column<int>(nullable: false),
                    v2015_venue_e = table.Column<int>(nullable: false),
                    v2015_visual_a = table.Column<int>(nullable: false),
                    v2015_visual_b = table.Column<int>(nullable: false),
                    v2015_visual_c = table.Column<int>(nullable: false),
                    v2015_visual_d = table.Column<int>(nullable: false),
                    v2015_visual_e = table.Column<int>(nullable: false),
                    v2016_comments = table.Column<string>(nullable: true),
                    v2016_fgd = table.Column<int>(nullable: false),
                    v2016_gallery_walk = table.Column<int>(nullable: false),
                    v2016_knowledge_part1 = table.Column<int>(nullable: false),
                    v2016_knowledge_part2 = table.Column<int>(nullable: false),
                    v2016_knowledge_part3 = table.Column<int>(nullable: false),
                    v2016_knowledge_part4 = table.Column<int>(nullable: false),
                    v2016_knowledge_part5 = table.Column<int>(nullable: false),
                    v2016_meals_a = table.Column<int>(nullable: false),
                    v2016_meals_b = table.Column<int>(nullable: false),
                    v2016_meals_c = table.Column<int>(nullable: false),
                    v2016_obj = table.Column<int>(nullable: false),
                    v2016_overall_satisfaction = table.Column<int>(nullable: false),
                    v2016_sound_system = table.Column<int>(nullable: false),
                    v2016_team_effectiveness = table.Column<int>(nullable: false),
                    v2016_time_alloted = table.Column<int>(nullable: false),
                    v2016_venue_a = table.Column<int>(nullable: false),
                    v2016_venue_b = table.Column<int>(nullable: false),
                    v2016_venue_c = table.Column<int>(nullable: false),
                    v2016_venue_d = table.Column<int>(nullable: false),
                    v2016_visual_a = table.Column<int>(nullable: false),
                    v2016_visual_b = table.Column<int>(nullable: false),
                    v2016_visual_c = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_talakayan_eval", x => x.talakayan_evaluation_id);
                    table.ForeignKey(
                        name: "FK_talakayan_eval_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_talakayan_eval_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_talakayan_eval_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_talakayan_eval_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_talakayan_eval_talakayan_year_talakayan_yr_id",
                        column: x => x.talakayan_yr_id,
                        principalTable: "talakayan_year",
                        principalColumn: "talakayan_yr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diaster_coverage",
                columns: table => new
                {
                    disaster_coverage_id = table.Column<int>(nullable: false),
                    affected = table.Column<bool>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    disaster_id = table.Column<Guid>(nullable: false),
                    prov_code = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diaster_coverage", x => x.disaster_coverage_id);
                    table.ForeignKey(
                        name: "FK_diaster_coverage_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diaster_coverage_disaster_disaster_id",
                        column: x => x.disaster_id,
                        principalTable: "disaster",
                        principalColumn: "disaster_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diaster_coverage_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diaster_coverage_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_project_post_disaster",
                columns: table => new
                {
                    sub_project_post_disaster_id = table.Column<Guid>(nullable: false),
                    affected = table.Column<bool>(nullable: true),
                    condition_id = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    disaster_id = table.Column<Guid>(nullable: false),
                    estimated_damage = table.Column<double>(nullable: true),
                    estimated_repair = table.Column<double>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    no_served = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    sub_project_id = table.Column<int>(nullable: false),
                    utilized = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_project_post_disaster", x => x.sub_project_post_disaster_id);
                    table.ForeignKey(
                        name: "FK_sub_project_post_disaster_lib_condition_condition_id",
                        column: x => x.condition_id,
                        principalTable: "lib_condition",
                        principalColumn: "condition_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_post_disaster_disaster_disaster_id",
                        column: x => x.disaster_id,
                        principalTable: "disaster",
                        principalColumn: "disaster_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brgy_assembly",
                columns: table => new
                {
                    brgy_assembly_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    barangay_assembly_purpose_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    highlights = table.Column<string>(nullable: true),
                    ip_leader = table.Column<string>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_sector_academe = table.Column<bool>(nullable: true),
                    is_sector_business = table.Column<bool>(nullable: true),
                    is_sector_farmer = table.Column<bool>(nullable: true),
                    is_sector_fisherfolks = table.Column<bool>(nullable: true),
                    is_sector_government = table.Column<bool>(nullable: true),
                    is_sector_ip = table.Column<bool>(nullable: true),
                    is_sector_ngo = table.Column<bool>(nullable: true),
                    is_sector_po = table.Column<bool>(nullable: true),
                    is_sector_pwd = table.Column<bool>(nullable: true),
                    is_sector_religios = table.Column<bool>(nullable: true),
                    is_sector_senior = table.Column<bool>(nullable: true),
                    is_sector_women = table.Column<bool>(nullable: true),
                    is_sector_youth = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    no_atn_female = table.Column<int>(nullable: true),
                    no_atn_male = table.Column<int>(nullable: true),
                    no_families = table.Column<int>(nullable: true),
                    no_household = table.Column<int>(nullable: true),
                    no_ip_family = table.Column<int>(nullable: true),
                    no_ip_family_in_barangay = table.Column<int>(nullable: true),
                    no_ip_female = table.Column<int>(nullable: true),
                    no_ip_household = table.Column<int>(nullable: true),
                    no_ip_male = table.Column<int>(nullable: true),
                    no_lgu_female = table.Column<int>(nullable: true),
                    no_lgu_male = table.Column<int>(nullable: true),
                    no_old_female = table.Column<int>(nullable: true),
                    no_old_male = table.Column<int>(nullable: true),
                    no_pantawid_family = table.Column<int>(nullable: true),
                    no_pantawid_family_in_barangay = table.Column<int>(nullable: true),
                    no_pantawid_household = table.Column<int>(nullable: true),
                    no_slp_family = table.Column<int>(nullable: true),
                    no_slp_family_in_barangay = table.Column<int>(nullable: true),
                    no_slp_household = table.Column<int>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    plan_date_end = table.Column<DateTime>(nullable: true),
                    plan_date_start = table.Column<DateTime>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    requirement_1 = table.Column<bool>(nullable: true),
                    requirement_2 = table.Column<bool>(nullable: true),
                    requirement_3 = table.Column<bool>(nullable: true),
                    requirement_4 = table.Column<bool>(nullable: true),
                    requirement_5 = table.Column<bool>(nullable: true),
                    requirement_6 = table.Column<bool>(nullable: true),
                    total_families_in_barangay = table.Column<int>(nullable: true),
                    total_household_in_barangay = table.Column<int>(nullable: true),
                    total_household_ip_in_barangay = table.Column<int>(nullable: true),
                    total_household_pantawid_in_barangay = table.Column<int>(nullable: true),
                    total_household_slp_in_barangay = table.Column<int>(nullable: true),
                    venue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brgy_assembly", x => x.brgy_assembly_id);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_barangay_assembly_purpose_barangay_assembly_purpose_id",
                        column: x => x.barangay_assembly_purpose_id,
                        principalTable: "lib_barangay_assembly_purpose",
                        principalColumn: "barangay_assembly_purpose_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brgy_profile",
                columns: table => new
                {
                    brgy_profile_id = table.Column<Guid>(nullable: false),
                    access_addressed = table.Column<bool>(nullable: true),
                    access_details = table.Column<string>(nullable: true),
                    access_remarks = table.Column<string>(nullable: true),
                    agriculture_addressed = table.Column<bool>(nullable: true),
                    agriculture_details = table.Column<string>(nullable: true),
                    agriculture_remarks = table.Column<string>(nullable: true),
                    alloc_basic = table.Column<double>(nullable: true),
                    alloc_drrm = table.Column<double>(nullable: true),
                    alloc_econ = table.Column<double>(nullable: true),
                    alloc_educ = table.Column<double>(nullable: true),
                    alloc_env = table.Column<double>(nullable: true),
                    alloc_gender = table.Column<double>(nullable: true),
                    alloc_infra = table.Column<double>(nullable: true),
                    alloc_inst = table.Column<double>(nullable: true),
                    alloc_others = table.Column<double>(nullable: true),
                    alloc_peace = table.Column<double>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    avg_fhdincome = table.Column<double>(nullable: true),
                    avg_hhincome = table.Column<double>(nullable: true),
                    avg_iphdincome = table.Column<double>(nullable: true),
                    avg_mhdincome = table.Column<double>(nullable: true),
                    baragay_additiondetails = table.Column<int>(nullable: true),
                    brgy_code = table.Column<int>(nullable: false),
                    brgy_projects = table.Column<string>(nullable: true),
                    child_13_16_secondary_ref = table.Column<string>(nullable: true),
                    children_0_5_reference = table.Column<string>(nullable: true),
                    children_0_5_value = table.Column<int>(nullable: true),
                    children_13_16_secondary_value = table.Column<int>(nullable: true),
                    children_6_12_elem_reference = table.Column<string>(nullable: true),
                    children_6_12_elem_value = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    communication_addressed = table.Column<bool>(nullable: true),
                    communication_details = table.Column<string>(nullable: true),
                    communication_remarks = table.Column<string>(nullable: true),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    drrm_activity = table.Column<string>(nullable: true),
                    drrm_utilized = table.Column<double>(nullable: true),
                    eca_list = table.Column<string>(nullable: true),
                    employment_addressed = table.Column<bool>(nullable: true),
                    employment_details = table.Column<string>(nullable: true),
                    employment_remarks = table.Column<string>(nullable: true),
                    environment_addressed = table.Column<bool>(nullable: true),
                    environment_details = table.Column<string>(nullable: true),
                    environment_remarks = table.Column<string>(nullable: true),
                    fem_hdblwfood = table.Column<int>(nullable: true),
                    fem_hdfoodshrt = table.Column<int>(nullable: true),
                    fem_hdmkshouse = table.Column<int>(nullable: true),
                    fem_hdnotoilet = table.Column<int>(nullable: true),
                    fem_hdpoverty = table.Column<int>(nullable: true),
                    fem_hdsfwater = table.Column<int>(nullable: true),
                    fem_hdsquatters = table.Column<int>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: false),
                    gad_activity = table.Column<string>(nullable: true),
                    gad_utilized = table.Column<double>(nullable: true),
                    has_bank = table.Column<bool>(nullable: true),
                    has_barangay_hall = table.Column<bool>(nullable: true),
                    has_cap_agri = table.Column<bool>(nullable: true),
                    has_cap_health = table.Column<bool>(nullable: true),
                    has_cap_org_dev = table.Column<bool>(nullable: true),
                    has_cap_others = table.Column<bool>(nullable: true),
                    has_cementery = table.Column<bool>(nullable: true),
                    has_college = table.Column<bool>(nullable: true),
                    has_credit = table.Column<bool>(nullable: true),
                    has_daycare = table.Column<bool>(nullable: true),
                    has_drainage = table.Column<bool>(nullable: true),
                    has_electricity = table.Column<bool>(nullable: true),
                    has_elementary = table.Column<bool>(nullable: true),
                    has_emergency_service = table.Column<bool>(nullable: true),
                    has_evac_center = table.Column<bool>(nullable: true),
                    has_harvest = table.Column<bool>(nullable: true),
                    has_health = table.Column<bool>(nullable: true),
                    has_hospital = table.Column<bool>(nullable: true),
                    has_housing = table.Column<bool>(nullable: true),
                    has_ip_presence = table.Column<bool>(nullable: true),
                    has_irrigation = table.Column<bool>(nullable: true),
                    has_market = table.Column<bool>(nullable: true),
                    has_miniport = table.Column<bool>(nullable: true),
                    has_multipurpose = table.Column<bool>(nullable: true),
                    has_secondary = table.Column<bool>(nullable: true),
                    has_stores = table.Column<bool>(nullable: true),
                    has_tanod = table.Column<bool>(nullable: true),
                    has_telecom = table.Column<bool>(nullable: true),
                    has_tribal = table.Column<bool>(nullable: true),
                    has_waste = table.Column<bool>(nullable: true),
                    has_water_supply_system = table.Column<bool>(nullable: true),
                    health_address = table.Column<bool>(nullable: true),
                    health_details = table.Column<string>(nullable: true),
                    health_number_0_5_reference = table.Column<string>(nullable: true),
                    health_number_0_5_value = table.Column<int>(nullable: true),
                    health_remarks = table.Column<string>(nullable: true),
                    hrs_totown = table.Column<double>(nullable: true),
                    incomeless_reference = table.Column<string>(nullable: true),
                    incomeless_value = table.Column<int>(nullable: true),
                    ip_group_in_area = table.Column<string>(nullable: true),
                    ira_amount = table.Column<double>(nullable: true),
                    is_armedconflict = table.Column<bool>(nullable: true),
                    is_bounddispute = table.Column<bool>(nullable: true),
                    is_brgyaffected = table.Column<bool>(nullable: true),
                    is_coastal = table.Column<bool>(nullable: true),
                    is_crime = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_genderviolence = table.Column<bool>(nullable: true),
                    is_hilly = table.Column<bool>(nullable: true),
                    is_island = table.Column<bool>(nullable: true),
                    is_isolated = table.Column<bool>(nullable: true),
                    is_lowland = table.Column<bool>(nullable: true),
                    is_poblacion = table.Column<bool>(nullable: true),
                    is_poldispute = table.Column<bool>(nullable: true),
                    is_rido_war = table.Column<bool>(nullable: true),
                    is_transaccess = table.Column<bool>(nullable: true),
                    is_upland = table.Column<bool>(nullable: true),
                    km_frmtown = table.Column<double>(nullable: true),
                    laborforce_reference = table.Column<string>(nullable: true),
                    laborforce_value = table.Column<int>(nullable: true),
                    landownership_addressed = table.Column<bool>(nullable: true),
                    landownership_details = table.Column<string>(nullable: true),
                    landownership_remarks = table.Column<string>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lessthan_3_meals_reference = table.Column<string>(nullable: true),
                    lessthan_3_meals_value = table.Column<int>(nullable: true),
                    literacy_addressed = table.Column<bool>(nullable: true),
                    literacy_details = table.Column<string>(nullable: true),
                    literacy_remarks = table.Column<string>(nullable: true),
                    makeshift_reference = table.Column<string>(nullable: true),
                    makeshift_value = table.Column<int>(nullable: true),
                    male_hdblwfood = table.Column<int>(nullable: true),
                    male_hdfoodshrt = table.Column<int>(nullable: true),
                    male_hdmkshhouse = table.Column<int>(nullable: true),
                    male_hdnotoilet = table.Column<int>(nullable: true),
                    male_hdpoverty = table.Column<int>(nullable: true),
                    male_hdsfwater = table.Column<int>(nullable: true),
                    male_hdsquatters = table.Column<int>(nullable: true),
                    malnourished_0_5reference = table.Column<string>(nullable: true),
                    malnourished_0_5value = table.Column<int>(nullable: true),
                    mins_totown = table.Column<double>(nullable: true),
                    nearest_bank = table.Column<double>(nullable: true),
                    nearest_barangay_hall = table.Column<double>(nullable: true),
                    nearest_cap_agri = table.Column<double>(nullable: true),
                    nearest_cap_health = table.Column<double>(nullable: true),
                    nearest_cap_org_dev = table.Column<double>(nullable: true),
                    nearest_cap_others = table.Column<double>(nullable: true),
                    nearest_cementery = table.Column<double>(nullable: true),
                    nearest_college = table.Column<double>(nullable: true),
                    nearest_credit = table.Column<double>(nullable: true),
                    nearest_daycare = table.Column<double>(nullable: true),
                    nearest_drainage = table.Column<double>(nullable: true),
                    nearest_electricity = table.Column<double>(nullable: true),
                    nearest_elementary = table.Column<double>(nullable: true),
                    nearest_emergency_service = table.Column<double>(nullable: true),
                    nearest_evac_center = table.Column<double>(nullable: true),
                    nearest_harvest = table.Column<double>(nullable: true),
                    nearest_health = table.Column<double>(nullable: true),
                    nearest_hospital = table.Column<double>(nullable: true),
                    nearest_housing = table.Column<double>(nullable: true),
                    nearest_irrigation = table.Column<double>(nullable: true),
                    nearest_market = table.Column<double>(nullable: true),
                    nearest_miniport = table.Column<double>(nullable: true),
                    nearest_multipurpose = table.Column<double>(nullable: true),
                    nearest_secondary = table.Column<double>(nullable: true),
                    nearest_stores = table.Column<double>(nullable: true),
                    nearest_tanod = table.Column<double>(nullable: true),
                    nearest_telecom = table.Column<double>(nullable: true),
                    nearest_tribal = table.Column<double>(nullable: true),
                    nearest_waste = table.Column<double>(nullable: true),
                    nearest_water_supply_system = table.Column<double>(nullable: true),
                    no_diedpreg = table.Column<int>(nullable: true),
                    no_drmm_activities = table.Column<int>(nullable: true),
                    no_families = table.Column<int>(nullable: true),
                    no_fdied5below = table.Column<int>(nullable: true),
                    no_female = table.Column<int>(nullable: true),
                    no_female0_5 = table.Column<int>(nullable: true),
                    no_female13_16 = table.Column<int>(nullable: true),
                    no_female17_59 = table.Column<int>(nullable: true),
                    no_female60up = table.Column<int>(nullable: true),
                    no_female6_12 = table.Column<int>(nullable: true),
                    no_femcrimev = table.Column<int>(nullable: true),
                    no_femnowork = table.Column<int>(nullable: true),
                    no_fkidmaln = table.Column<int>(nullable: true),
                    no_fkidnosch13_16 = table.Column<int>(nullable: true),
                    no_fkidnosch6_12 = table.Column<int>(nullable: true),
                    no_fmheadedhh = table.Column<int>(nullable: true),
                    no_gad_activities = table.Column<int>(nullable: true),
                    no_households = table.Column<int>(nullable: true),
                    no_indisplaced = table.Column<int>(nullable: true),
                    no_ipfamily = table.Column<int>(nullable: true),
                    no_iphouseholds = table.Column<int>(nullable: true),
                    no_labor_female = table.Column<int>(nullable: true),
                    no_labor_male = table.Column<int>(nullable: true),
                    no_male = table.Column<int>(nullable: true),
                    no_male0_5 = table.Column<int>(nullable: true),
                    no_male13_16 = table.Column<int>(nullable: true),
                    no_male17_59 = table.Column<int>(nullable: true),
                    no_male60up = table.Column<int>(nullable: true),
                    no_male6_12 = table.Column<int>(nullable: true),
                    no_malecrimev = table.Column<int>(nullable: true),
                    no_malenowork = table.Column<int>(nullable: true),
                    no_mdied5below = table.Column<int>(nullable: true),
                    no_mkidmaln = table.Column<int>(nullable: true),
                    no_mkidnosch13_16 = table.Column<int>(nullable: true),
                    no_mkidnosch6_12 = table.Column<int>(nullable: true),
                    no_pantawid_family = table.Column<int>(nullable: true),
                    no_pantawid_household = table.Column<int>(nullable: true),
                    no_sitios = table.Column<int>(nullable: true),
                    no_slp_family = table.Column<int>(nullable: true),
                    no_slp_household = table.Column<int>(nullable: true),
                    no_voting_female = table.Column<int>(nullable: true),
                    no_voting_male = table.Column<int>(nullable: true),
                    no_withdisability = table.Column<int>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    other_source = table.Column<string>(nullable: true),
                    others_addressed = table.Column<bool>(nullable: true),
                    others_amount = table.Column<double>(nullable: true),
                    others_details = table.Column<string>(nullable: true),
                    others_remarks = table.Column<string>(nullable: true),
                    peace_addressed = table.Column<bool>(nullable: true),
                    peace_details = table.Column<string>(nullable: true),
                    peace_remarks = table.Column<string>(nullable: true),
                    powersupply_addressed = table.Column<bool>(nullable: true),
                    powersupply_details = table.Column<string>(nullable: true),
                    powersupply_remarks = table.Column<string>(nullable: true),
                    pregnant_died_reference = table.Column<string>(nullable: true),
                    pregnant_died_value = table.Column<int>(nullable: true),
                    pregnant_total_reference = table.Column<string>(nullable: true),
                    pregnant_total_value = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    safewater_reference = table.Column<string>(nullable: true),
                    safewater_value = table.Column<int>(nullable: true),
                    sanity_reference = table.Column<string>(nullable: true),
                    sanity_value = table.Column<int>(nullable: true),
                    socio_econ_activity_1 = table.Column<string>(nullable: true),
                    socio_econ_activity_2 = table.Column<string>(nullable: true),
                    socio_econ_activity_3 = table.Column<string>(nullable: true),
                    socio_econ_amount_1 = table.Column<double>(nullable: true),
                    socio_econ_amount_2 = table.Column<double>(nullable: true),
                    socio_econ_amount_3 = table.Column<double>(nullable: true),
                    socio_econ_hhs_1 = table.Column<int>(nullable: true),
                    socio_econ_hhs_2 = table.Column<int>(nullable: true),
                    socio_econ_hhs_3 = table.Column<int>(nullable: true),
                    source_5dead = table.Column<string>(nullable: true),
                    source_blwfood = table.Column<string>(nullable: true),
                    source_crimevic = table.Column<string>(nullable: true),
                    source_foodshort = table.Column<string>(nullable: true),
                    source_kidmaln = table.Column<string>(nullable: true),
                    source_mkshouse = table.Column<string>(nullable: true),
                    source_notoilet = table.Column<string>(nullable: true),
                    source_nowork = table.Column<string>(nullable: true),
                    source_poverty = table.Column<string>(nullable: true),
                    source_safewater = table.Column<string>(nullable: true),
                    source_squatters = table.Column<string>(nullable: true),
                    sourse_diedpreg = table.Column<string>(nullable: true),
                    squatting_reference = table.Column<string>(nullable: true),
                    squatting_value = table.Column<int>(nullable: true),
                    srce_kidnosch13_16 = table.Column<string>(nullable: true),
                    srce_kidnosch6_12 = table.Column<string>(nullable: true),
                    threshold_reference = table.Column<string>(nullable: true),
                    threshold_value = table.Column<int>(nullable: true),
                    tot_lessthan_3_meals_ref = table.Column<string>(nullable: true),
                    tot_malnourished_0_5_ref = table.Column<string>(nullable: true),
                    total_malnourished_0_5value = table.Column<int>(nullable: true),
                    totalchildren_6_12_elem_value = table.Column<int>(nullable: true),
                    totalincomeless_reference = table.Column<string>(nullable: true),
                    totalincomeless_value = table.Column<int>(nullable: true),
                    totallaborforce_reference = table.Column<string>(nullable: true),
                    totallaborforce_value = table.Column<int>(nullable: true),
                    totallessthan_3_meals_value = table.Column<int>(nullable: true),
                    totalmakeshift_reference = table.Column<string>(nullable: true),
                    totalmakeshift_value = table.Column<int>(nullable: true),
                    totalsafewater_reference = table.Column<string>(nullable: true),
                    totalsafewater_value = table.Column<int>(nullable: true),
                    totalsanity_reference = table.Column<string>(nullable: true),
                    totalsanity_value = table.Column<int>(nullable: true),
                    totalsquatting_reference = table.Column<string>(nullable: true),
                    totalsquatting_value = table.Column<int>(nullable: true),
                    totalthreshold_reference = table.Column<string>(nullable: true),
                    totalthreshold_value = table.Column<int>(nullable: true),
                    totalvictimized_reference = table.Column<string>(nullable: true),
                    totalvictimized_value = table.Column<int>(nullable: true),
                    totchild_13_16_secondary_ref = table.Column<string>(nullable: true),
                    totchild_13_16_secondary_val = table.Column<int>(nullable: true),
                    totchild_6_12_elem_ref = table.Column<string>(nullable: true),
                    transpo_bank = table.Column<int>(nullable: true),
                    transpo_barangay_hall = table.Column<int>(nullable: true),
                    transpo_cap_agri = table.Column<int>(nullable: true),
                    transpo_cap_health = table.Column<int>(nullable: true),
                    transpo_cap_org_dev = table.Column<int>(nullable: true),
                    transpo_cap_others = table.Column<int>(nullable: true),
                    transpo_cementery = table.Column<int>(nullable: true),
                    transpo_college = table.Column<int>(nullable: true),
                    transpo_credit = table.Column<int>(nullable: true),
                    transpo_daycare = table.Column<int>(nullable: true),
                    transpo_drainage = table.Column<int>(nullable: true),
                    transpo_electricity = table.Column<int>(nullable: true),
                    transpo_elementary = table.Column<int>(nullable: true),
                    transpo_emergency_service = table.Column<int>(nullable: true),
                    transpo_evac_center = table.Column<int>(nullable: true),
                    transpo_harvest = table.Column<int>(nullable: true),
                    transpo_health = table.Column<int>(nullable: true),
                    transpo_hospital = table.Column<int>(nullable: true),
                    transpo_housing = table.Column<int>(nullable: true),
                    transpo_irrigation = table.Column<int>(nullable: true),
                    transpo_market = table.Column<int>(nullable: true),
                    transpo_miniport = table.Column<int>(nullable: true),
                    transpo_multipurpose = table.Column<int>(nullable: true),
                    transpo_secondary = table.Column<int>(nullable: true),
                    transpo_stores = table.Column<int>(nullable: true),
                    transpo_tanod = table.Column<int>(nullable: true),
                    transpo_telecom = table.Column<int>(nullable: true),
                    transpo_tribal = table.Column<int>(nullable: true),
                    transpo_waste = table.Column<int>(nullable: true),
                    transpo_water_supply_system = table.Column<int>(nullable: true),
                    victimized_reference = table.Column<string>(nullable: true),
                    victimized_value = table.Column<int>(nullable: true),
                    water_address = table.Column<bool>(nullable: true),
                    water_details = table.Column<string>(nullable: true),
                    water_remarks = table.Column<string>(nullable: true),
                    year_source = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brgy_profile", x => x.brgy_profile_id);
                    table.ForeignKey(
                        name: "FK_brgy_profile_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_profile_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_profile_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_profile_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_profile_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_profile_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_profile_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ceac_list",
                columns: table => new
                {
                    ceac_list_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    implementation_status_id = table.Column<int>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ceac_list", x => x.ceac_list_id);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_list_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "community_training",
                columns: table => new
                {
                    community_training_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    date_conducted = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    duration = table.Column<int>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_draft = table.Column<bool>(nullable: false),
                    is_sector_academe = table.Column<bool>(nullable: true),
                    is_sector_business = table.Column<bool>(nullable: true),
                    is_sector_farmer = table.Column<bool>(nullable: true),
                    is_sector_fisherfolks = table.Column<bool>(nullable: true),
                    is_sector_government = table.Column<bool>(nullable: true),
                    is_sector_ip = table.Column<bool>(nullable: true),
                    is_sector_ngo = table.Column<bool>(nullable: true),
                    is_sector_po = table.Column<bool>(nullable: true),
                    is_sector_pwd = table.Column<bool>(nullable: true),
                    is_sector_religios = table.Column<bool>(nullable: true),
                    is_sector_senior = table.Column<bool>(nullable: true),
                    is_sector_women = table.Column<bool>(nullable: true),
                    is_sector_youth = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    last_sync_source_id = table.Column<int>(nullable: true),
                    lgu_level_id = table.Column<int>(nullable: false),
                    no_atn_female = table.Column<int>(nullable: true),
                    no_atn_male = table.Column<int>(nullable: true),
                    no_atn_pantawid = table.Column<int>(nullable: true),
                    no_atn_slp = table.Column<int>(nullable: true),
                    no_brgy_rep = table.Column<int>(nullable: true),
                    no_ip_female = table.Column<int>(nullable: true),
                    no_ip_male = table.Column<int>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    plan_date_end = table.Column<DateTime>(nullable: true),
                    plan_date_start = table.Column<DateTime>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    reported_by = table.Column<string>(nullable: true),
                    requirement_1 = table.Column<bool>(nullable: true),
                    requirement_2 = table.Column<bool>(nullable: true),
                    requirement_3 = table.Column<bool>(nullable: true),
                    requirement_4 = table.Column<bool>(nullable: true),
                    requirement_5 = table.Column<bool>(nullable: true),
                    requirement_6 = table.Column<bool>(nullable: true),
                    speaker = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(nullable: true),
                    training_category_id = table.Column<int>(nullable: false),
                    training_title = table.Column<string>(nullable: true),
                    venue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_community_training", x => x.community_training_id);
                    table.ForeignKey(
                        name: "FK_community_training_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_community_training_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_lgu_level_lgu_level_id",
                        column: x => x.lgu_level_id,
                        principalTable: "lib_lgu_level",
                        principalColumn: "lgu_level_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_training_lib_training_category_training_category_id",
                        column: x => x.training_category_id,
                        principalTable: "lib_training_category",
                        principalColumn: "training_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "community_organization",
                columns: table => new
                {
                    community_organization_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_formal = table.Column<bool>(nullable: true),
                    is_lgu_accredited = table.Column<bool>(nullable: true),
                    is_onm = table.Column<bool>(nullable: true),
                    is_sector_academe = table.Column<bool>(nullable: true),
                    is_sector_business = table.Column<bool>(nullable: true),
                    is_sector_farmer = table.Column<bool>(nullable: true),
                    is_sector_fisherfolks = table.Column<bool>(nullable: true),
                    is_sector_government = table.Column<bool>(nullable: true),
                    is_sector_ip = table.Column<bool>(nullable: true),
                    is_sector_ngo = table.Column<bool>(nullable: true),
                    is_sector_po = table.Column<bool>(nullable: true),
                    is_sector_pwd = table.Column<bool>(nullable: true),
                    is_sector_religios = table.Column<bool>(nullable: true),
                    is_sector_senior = table.Column<bool>(nullable: true),
                    is_sector_women = table.Column<bool>(nullable: true),
                    is_sector_youth = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lgu_level_id = table.Column<int>(nullable: false),
                    list_activities = table.Column<string>(nullable: true),
                    list_advocacy = table.Column<string>(nullable: true),
                    list_area_operation = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    no_female = table.Column<int>(nullable: true),
                    no_ip_female = table.Column<int>(nullable: true),
                    no_ip_male = table.Column<int>(nullable: true),
                    no_male = table.Column<int>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    organization_type_id = table.Column<int>(nullable: false),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    years_operating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_community_organization", x => x.community_organization_id);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_lgu_level_lgu_level_id",
                        column: x => x.lgu_level_id,
                        principalTable: "lib_lgu_level",
                        principalColumn: "lgu_level_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_organization_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "perception_survey",
                columns: table => new
                {
                    perception_survey_id = table.Column<Guid>(nullable: false),
                    access_1 = table.Column<int>(nullable: false),
                    access_10 = table.Column<int>(nullable: false),
                    access_11 = table.Column<int>(nullable: false),
                    access_12 = table.Column<int>(nullable: false),
                    access_13 = table.Column<int>(nullable: false),
                    access_14 = table.Column<int>(nullable: false),
                    access_15 = table.Column<int>(nullable: false),
                    access_16 = table.Column<int>(nullable: false),
                    access_2 = table.Column<int>(nullable: false),
                    access_3 = table.Column<int>(nullable: false),
                    access_4 = table.Column<int>(nullable: false),
                    access_5 = table.Column<int>(nullable: false),
                    access_6 = table.Column<int>(nullable: false),
                    access_7 = table.Column<int>(nullable: false),
                    access_8 = table.Column<int>(nullable: false),
                    access_9 = table.Column<int>(nullable: false),
                    activity_name = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    disaster_1 = table.Column<int>(nullable: false),
                    disaster_2 = table.Column<int>(nullable: false),
                    disaster_3 = table.Column<int>(nullable: false),
                    disaster_4 = table.Column<int>(nullable: false),
                    disaster_5 = table.Column<int>(nullable: false),
                    disaster_6 = table.Column<int>(nullable: false),
                    disaster_7 = table.Column<int>(nullable: false),
                    disaster_8 = table.Column<int>(nullable: false),
                    disaster_9 = table.Column<int>(nullable: false),
                    ip_group_id = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    is_ip = table.Column<bool>(nullable: true),
                    is_male = table.Column<bool>(nullable: true),
                    is_pantawid = table.Column<bool>(nullable: true),
                    is_slp = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lib_ip_groupip_group_id = table.Column<int>(nullable: true),
                    participation_1 = table.Column<int>(nullable: false),
                    participation_10 = table.Column<int>(nullable: false),
                    participation_11 = table.Column<int>(nullable: false),
                    participation_12 = table.Column<int>(nullable: false),
                    participation_2 = table.Column<int>(nullable: false),
                    participation_3 = table.Column<int>(nullable: false),
                    participation_4 = table.Column<int>(nullable: false),
                    participation_5 = table.Column<int>(nullable: false),
                    participation_6 = table.Column<int>(nullable: false),
                    participation_7 = table.Column<int>(nullable: false),
                    participation_8 = table.Column<int>(nullable: false),
                    participation_9 = table.Column<int>(nullable: false),
                    person_name = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    survey_date_from = table.Column<DateTime>(nullable: true),
                    survey_date_to = table.Column<DateTime>(nullable: true),
                    talakayan_date_from = table.Column<DateTime>(nullable: true),
                    talakayan_date_to = table.Column<DateTime>(nullable: true),
                    talakayan_yr_id = table.Column<int>(nullable: false),
                    trust_1 = table.Column<int>(nullable: false),
                    trust_2 = table.Column<int>(nullable: false),
                    trust_3 = table.Column<int>(nullable: false),
                    trust_4 = table.Column<int>(nullable: false),
                    trust_5 = table.Column<int>(nullable: false),
                    trust_6 = table.Column<int>(nullable: false),
                    trust_7 = table.Column<int>(nullable: false),
                    trust_8 = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perception_survey", x => x.perception_survey_id);
                    table.ForeignKey(
                        name: "FK_perception_survey_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_perception_survey_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_perception_survey_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_perception_survey_lib_ip_group_lib_ip_groupip_group_id",
                        column: x => x.lib_ip_groupip_group_id,
                        principalTable: "lib_ip_group",
                        principalColumn: "ip_group_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_perception_survey_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_perception_survey_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_perception_survey_talakayan_year_talakayan_yr_id",
                        column: x => x.talakayan_yr_id,
                        principalTable: "talakayan_year",
                        principalColumn: "talakayan_yr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grievance_record",
                columns: table => new
                {
                    grievance_record_id = table.Column<Guid>(nullable: false),
                    actions = table.Column<string>(nullable: true),
                    activity_source_id = table.Column<Guid>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: true),
                    cellphone = table.Column<string>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: true),
                    date_intake = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    details = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: true),
                    final_resolution = table.Column<string>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: true),
                    grs_category_id = table.Column<int>(nullable: false),
                    grs_complaint_subject_id = table.Column<int>(nullable: false),
                    grs_complaint_subject_others = table.Column<string>(nullable: true),
                    grs_feedback_id = table.Column<int>(nullable: true),
                    grs_filling_mode_id = table.Column<int>(nullable: false),
                    grs_filling_mode_others = table.Column<string>(nullable: true),
                    grs_form_id = table.Column<int>(nullable: false),
                    grs_intake_level_id = table.Column<int>(nullable: false),
                    grs_intake_officer_id = table.Column<int>(nullable: true),
                    grs_nature_id = table.Column<int>(nullable: false),
                    grs_pincos_actor_id = table.Column<int>(nullable: true),
                    grs_resolution_status_id = table.Column<int>(nullable: false),
                    grs_sender_designation_id = table.Column<int>(nullable: true),
                    intake_officer = table.Column<string>(nullable: true),
                    intake_officer_designation = table.Column<string>(nullable: true),
                    ip_group_id = table.Column<int>(nullable: true),
                    ip_group_other = table.Column<string>(nullable: true),
                    is_anonymous = table.Column<bool>(nullable: true),
                    is_central_office_level_only = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    is_field_office_level_only = table.Column<bool>(nullable: true),
                    is_fund_source_applicable = table.Column<bool>(nullable: true),
                    is_individual = table.Column<bool>(nullable: true),
                    is_ip = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    last_sync_source_id = table.Column<int>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    pincos_id = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    recommendations = table.Column<string>(nullable: true),
                    region_code = table.Column<int>(nullable: false),
                    req_brgy = table.Column<bool>(nullable: true),
                    req_city = table.Column<bool>(nullable: true),
                    req_province = table.Column<bool>(nullable: true),
                    resolution_date = table.Column<DateTime>(nullable: true),
                    sender_contact_info = table.Column<string>(nullable: true),
                    sender_designation = table.Column<string>(nullable: true),
                    sender_designation_other = table.Column<string>(nullable: true),
                    sender_name = table.Column<string>(nullable: true),
                    sender_organization = table.Column<string>(nullable: true),
                    sender_sex = table.Column<bool>(nullable: false),
                    sex_id = table.Column<int>(nullable: false),
                    training_category_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grievance_record", x => x.grievance_record_id);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_category_grs_category_id",
                        column: x => x.grs_category_id,
                        principalTable: "lib_grs_category",
                        principalColumn: "grs_category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_complaint_subject_grs_complaint_subject_id",
                        column: x => x.grs_complaint_subject_id,
                        principalTable: "lib_grs_complaint_subject",
                        principalColumn: "grs_complaint_subject_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_feedback_grs_feedback_id",
                        column: x => x.grs_feedback_id,
                        principalTable: "lib_grs_feedback",
                        principalColumn: "grs_feedback_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_filling_mode_grs_filling_mode_id",
                        column: x => x.grs_filling_mode_id,
                        principalTable: "lib_grs_filling_mode",
                        principalColumn: "grs_filling_mode_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_form_grs_form_id",
                        column: x => x.grs_form_id,
                        principalTable: "lib_grs_form",
                        principalColumn: "grs_form_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_intake_level_grs_intake_level_id",
                        column: x => x.grs_intake_level_id,
                        principalTable: "lib_grs_intake_level",
                        principalColumn: "grs_intake_level_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_intake_officer_grs_intake_officer_id",
                        column: x => x.grs_intake_officer_id,
                        principalTable: "lib_grs_intake_officer",
                        principalColumn: "grs_intake_officer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_nature_grs_nature_id",
                        column: x => x.grs_nature_id,
                        principalTable: "lib_grs_nature",
                        principalColumn: "grs_nature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_pincos_actor_grs_pincos_actor_id",
                        column: x => x.grs_pincos_actor_id,
                        principalTable: "lib_grs_pincos_actor",
                        principalColumn: "grs_pincos_actor_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_resolution_status_grs_resolution_status_id",
                        column: x => x.grs_resolution_status_id,
                        principalTable: "lib_grs_resolution_status",
                        principalColumn: "grs_resolution_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_grs_sender_designation_grs_sender_designation_id",
                        column: x => x.grs_sender_designation_id,
                        principalTable: "lib_grs_sender_designation",
                        principalColumn: "grs_sender_designation_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_ip_group_ip_group_id",
                        column: x => x.ip_group_id,
                        principalTable: "lib_ip_group",
                        principalColumn: "ip_group_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_sex_sex_id",
                        column: x => x.sex_id,
                        principalTable: "lib_sex",
                        principalColumn: "sex_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grievance_record_lib_training_category_training_category_id",
                        column: x => x.training_category_id,
                        principalTable: "lib_training_category",
                        principalColumn: "training_category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "grs_installation",
                columns: table => new
                {
                    grs_installation_id = table.Column<Guid>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    date_ffcomm = table.Column<DateTime>(nullable: true),
                    date_infodess = table.Column<DateTime>(nullable: true),
                    date_inspect = table.Column<DateTime>(nullable: true),
                    date_means = table.Column<DateTime>(nullable: true),
                    date_meansrept = table.Column<DateTime>(nullable: true),
                    date_orientation = table.Column<DateTime>(nullable: true),
                    date_training = table.Column<DateTime>(nullable: true),
                    date_voliden = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: false),
                    is_boxinstalled = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lgu_level_id = table.Column<int>(nullable: false),
                    no_brochures = table.Column<int>(nullable: true),
                    no_manuals = table.Column<int>(nullable: true),
                    no_posters = table.Column<int>(nullable: true),
                    no_tarpauline = table.Column<int>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    other_mat = table.Column<int>(nullable: true),
                    phone_no = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grs_installation", x => x.grs_installation_id);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_lgu_level_lgu_level_id",
                        column: x => x.lgu_level_id,
                        principalTable: "lib_lgu_level",
                        principalColumn: "lgu_level_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grs_installation_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "muni_profile",
                columns: table => new
                {
                    muni_profile_id = table.Column<Guid>(nullable: false),
                    alloc_basic = table.Column<int>(nullable: true),
                    alloc_drrm = table.Column<int>(nullable: true),
                    alloc_econ = table.Column<int>(nullable: true),
                    alloc_educ = table.Column<int>(nullable: true),
                    alloc_env = table.Column<int>(nullable: true),
                    alloc_gender = table.Column<int>(nullable: true),
                    alloc_infra = table.Column<int>(nullable: true),
                    alloc_inst = table.Column<int>(nullable: true),
                    alloc_others = table.Column<int>(nullable: true),
                    alloc_peace = table.Column<int>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    drrm_activity = table.Column<string>(nullable: true),
                    drrm_utilized = table.Column<double>(nullable: true),
                    femaleheaded_hhs = table.Column<int>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: false),
                    gad_activity = table.Column<string>(nullable: true),
                    gad_utilized = table.Column<double>(nullable: true),
                    headfemale_renting = table.Column<int>(nullable: true),
                    headfemale_squatting = table.Column<int>(nullable: true),
                    headfemale_tenant = table.Column<int>(nullable: true),
                    headffemale_owner = table.Column<int>(nullable: true),
                    headmale_owner = table.Column<int>(nullable: true),
                    headmale_renting = table.Column<int>(nullable: true),
                    headmale_squatting = table.Column<int>(nullable: true),
                    headmale_tenant = table.Column<int>(nullable: true),
                    hhs_owner = table.Column<int>(nullable: true),
                    hhs_renting = table.Column<int>(nullable: true),
                    hhs_squatting = table.Column<int>(nullable: true),
                    hhs_tenant = table.Column<int>(nullable: true),
                    householdincome_average = table.Column<double>(nullable: true),
                    ipheaded_hhs = table.Column<int>(nullable: true),
                    ira_amount = table.Column<double>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    maleheaded_hhs = table.Column<int>(nullable: true),
                    no_drmm_activities = table.Column<int>(nullable: true),
                    no_gad_activities = table.Column<int>(nullable: true),
                    no_of_brgys = table.Column<int>(nullable: true),
                    old_activity_id = table.Column<string>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    other_source = table.Column<string>(nullable: true),
                    others_amount = table.Column<double>(nullable: true),
                    pop_index = table.Column<double>(nullable: true),
                    population = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    socio_econ_activity_1 = table.Column<string>(nullable: true),
                    socio_econ_activity_2 = table.Column<string>(nullable: true),
                    socio_econ_activity_3 = table.Column<string>(nullable: true),
                    socio_econ_amount_1 = table.Column<double>(nullable: true),
                    socio_econ_amount_2 = table.Column<double>(nullable: true),
                    socio_econ_amount_3 = table.Column<double>(nullable: true),
                    socio_econ_hhs_1 = table.Column<int>(nullable: true),
                    socio_econ_hhs_2 = table.Column<int>(nullable: true),
                    socio_econ_hhs_3 = table.Column<int>(nullable: true),
                    transportation = table.Column<string>(nullable: true),
                    year_source = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_muni_profile", x => x.muni_profile_id);
                    table.ForeignKey(
                        name: "FK_muni_profile_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_muni_profile_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_muni_profile_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_muni_profile_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_muni_profile_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_muni_profile_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "municipal_lcc",
                columns: table => new
                {
                    municipal_lcc_id = table.Column<Guid>(nullable: false),
                    actual_cash = table.Column<double>(nullable: false),
                    actual_inkind = table.Column<double>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    cbis_blgu_actual = table.Column<double>(nullable: false),
                    cbis_blgu_planned = table.Column<double>(nullable: false),
                    cbis_mlgu_actual = table.Column<double>(nullable: false),
                    cbis_mlgu_planned = table.Column<double>(nullable: false),
                    cbis_others_actual = table.Column<double>(nullable: false),
                    cbis_others_planned = table.Column<double>(nullable: false),
                    cbis_plgu_actual = table.Column<double>(nullable: false),
                    cbis_plgu_planned = table.Column<double>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    history = table.Column<string>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    me_blgu_actual = table.Column<double>(nullable: false),
                    me_blgu_planned = table.Column<double>(nullable: false),
                    me_mlgu_actual = table.Column<double>(nullable: false),
                    me_mlgu_planned = table.Column<double>(nullable: false),
                    me_others_actual = table.Column<double>(nullable: false),
                    me_others_planned = table.Column<double>(nullable: false),
                    me_plgu_actual = table.Column<double>(nullable: false),
                    me_plgu_planned = table.Column<double>(nullable: false),
                    no_of_barangays = table.Column<int>(nullable: false),
                    old_id = table.Column<string>(nullable: true),
                    planned_cash = table.Column<double>(nullable: false),
                    planned_inkind = table.Column<double>(nullable: false),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    spi_blgu_actual = table.Column<double>(nullable: false),
                    spi_blgu_planned = table.Column<double>(nullable: false),
                    spi_mlgu_actual = table.Column<double>(nullable: false),
                    spi_mlgu_planned = table.Column<double>(nullable: false),
                    spi_others_actual = table.Column<double>(nullable: false),
                    spi_others_planned = table.Column<double>(nullable: false),
                    spi_plgu_actual = table.Column<double>(nullable: false),
                    spi_plgu_planned = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipal_lcc", x => x.municipal_lcc_id);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_lcc_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "municipal_pta",
                columns: table => new
                {
                    municipal_pta_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    budget_location_post = table.Column<string>(nullable: true),
                    budget_post_date = table.Column<DateTime>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    encoder = table.Column<string>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    focal_person = table.Column<string>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: false),
                    gad_approval_date = table.Column<DateTime>(nullable: true),
                    gad_resolution_no = table.Column<string>(nullable: true),
                    incexp_location_post = table.Column<string>(nullable: true),
                    incexp_post_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    kc_equipment = table.Column<string>(nullable: true),
                    kc_equipment_list = table.Column<string>(nullable: true),
                    kc_office = table.Column<string>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    latitude = table.Column<string>(nullable: true),
                    lcc_approval_date = table.Column<DateTime>(nullable: true),
                    lcc_resolution_no = table.Column<string>(nullable: true),
                    longitude = table.Column<string>(nullable: true),
                    lsb_represented_female = table.Column<int>(nullable: true),
                    lsb_represented_male = table.Column<int>(nullable: true),
                    mct_approval_date = table.Column<DateTime>(nullable: true),
                    mct_eo_date = table.Column<DateTime>(nullable: true),
                    mct_eo_no = table.Column<string>(nullable: true),
                    mct_resolution_no = table.Column<string>(nullable: true),
                    mdc_date = table.Column<string>(nullable: true),
                    mdc_resolution_no = table.Column<string>(nullable: true),
                    mdcmem_approval_date = table.Column<DateTime>(nullable: true),
                    mdcmem_female_no = table.Column<int>(nullable: true),
                    mdcmem_male_no = table.Column<int>(nullable: true),
                    mdcmem_resolution_no = table.Column<string>(nullable: true),
                    miac_approval_date = table.Column<DateTime>(nullable: true),
                    miac_eo_date = table.Column<DateTime>(nullable: true),
                    miac_eo_no = table.Column<string>(nullable: true),
                    miac_resolution_no = table.Column<string>(nullable: true),
                    miacmct_approval_date = table.Column<DateTime>(nullable: true),
                    miacmct_resolution_no = table.Column<string>(nullable: true),
                    mlgu_logistics = table.Column<string>(nullable: true),
                    moa_signed_date = table.Column<DateTime>(nullable: true),
                    nga_approval_date = table.Column<DateTime>(nullable: true),
                    nga_resolution_no = table.Column<string>(nullable: true),
                    ngo_total = table.Column<int>(nullable: true),
                    ngopo_accredited = table.Column<int>(nullable: true),
                    ngopo_approval_date = table.Column<DateTime>(nullable: true),
                    ngopo_resolution_no = table.Column<string>(nullable: true),
                    no_4p_female = table.Column<int>(nullable: true),
                    no_4p_male = table.Column<int>(nullable: true),
                    no_cdd_female = table.Column<int>(nullable: true),
                    no_cdd_male = table.Column<int>(nullable: true),
                    no_elderly_female = table.Column<int>(nullable: true),
                    no_elderly_male = table.Column<int>(nullable: true),
                    no_gad_plan_assessment = table.Column<int>(nullable: true),
                    no_ip_female = table.Column<int>(nullable: true),
                    no_ip_male = table.Column<int>(nullable: true),
                    no_lgu_cso_meeting = table.Column<int>(nullable: true),
                    no_lgu_female = table.Column<int>(nullable: true),
                    no_lgu_male = table.Column<int>(nullable: true),
                    no_ngo_pim = table.Column<int>(nullable: true),
                    no_ngopo_accredited = table.Column<int>(nullable: true),
                    no_ngopo_female = table.Column<int>(nullable: true),
                    no_ngopo_male = table.Column<int>(nullable: true),
                    no_ngopo_represented = table.Column<int>(nullable: true),
                    no_ngopo_represented_female = table.Column<int>(nullable: true),
                    no_ngopo_represented_male = table.Column<int>(nullable: true),
                    no_pwd_female = table.Column<int>(nullable: true),
                    no_pwd_male = table.Column<int>(nullable: true),
                    no_staff = table.Column<int>(nullable: true),
                    no_tas = table.Column<int>(nullable: true),
                    no_women_female = table.Column<int>(nullable: true),
                    no_women_male = table.Column<int>(nullable: true),
                    no_youth_female = table.Column<int>(nullable: true),
                    no_youth_male = table.Column<int>(nullable: true),
                    office_address = table.Column<string>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    plan_location_post = table.Column<string>(nullable: true),
                    plan_post_date = table.Column<DateTime>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    pta_approval_date = table.Column<DateTime>(nullable: true),
                    pta_resolution_no = table.Column<string>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    trust_account_no = table.Column<string>(nullable: true),
                    trust_opened_date = table.Column<DateTime>(nullable: true),
                    with_equipment = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipal_pta", x => x.municipal_pta_id);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_pta_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oversight_committee",
                columns: table => new
                {
                    oversight_committee_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    date_organized = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    frequency = table.Column<int>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    no_female = table.Column<int>(nullable: true),
                    no_male = table.Column<int>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oversight_committee", x => x.oversight_committee_id);
                    table.ForeignKey(
                        name: "FK_oversight_committee_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oversight_committee_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oversight_committee_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oversight_committee_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oversight_committee_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oversight_committee_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oversight_committee_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_project",
                columns: table => new
                {
                    sub_project_unique_id = table.Column<Guid>(nullable: false),
                    Add_Ind = table.Column<long>(nullable: true),
                    Balance = table.Column<double>(nullable: true),
                    BalanceNotReq = table.Column<long>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    Date_Started = table.Column<DateTime>(nullable: true),
                    Date_of_Completion = table.Column<DateTime>(nullable: true),
                    Delete_Ind = table.Column<long>(nullable: true),
                    Edit_Ind = table.Column<long>(nullable: true),
                    EngineeringMigrationId = table.Column<int>(nullable: false),
                    ExcessFund = table.Column<double>(nullable: true),
                    ID = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    KC_Adj = table.Column<double>(nullable: true),
                    KC_Fund_Released = table.Column<double>(nullable: true),
                    KalahiAmt_Old = table.Column<double>(nullable: true),
                    Kalahi_Amount = table.Column<double>(nullable: true),
                    LCCAmt_Old = table.Column<double>(nullable: true),
                    LCC_Adj = table.Column<double>(nullable: true),
                    LCC_Amount = table.Column<double>(nullable: true),
                    LandOwnership = table.Column<long>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Month = table.Column<long>(nullable: true),
                    NoOfBgys = table.Column<long>(nullable: true),
                    NoOfClassroom = table.Column<long>(nullable: true),
                    NoOfHH = table.Column<long>(nullable: true),
                    NoOfHHActual = table.Column<long>(nullable: true),
                    NoOfHPumps = table.Column<long>(nullable: true),
                    NoOfTapstands = table.Column<long>(nullable: true),
                    OldSP_ID = table.Column<long>(nullable: true),
                    PamanaGrant = table.Column<double>(nullable: true),
                    Phy_Perc_Previous = table.Column<double>(nullable: true),
                    Phy_Perc_This_Month = table.Column<double>(nullable: true),
                    Phy_Perc_To_Date = table.Column<double>(nullable: true),
                    Phy_Target_Unit = table.Column<string>(nullable: true),
                    Physical_Target = table.Column<double>(nullable: true),
                    PlannedDate_Completion = table.Column<DateTime>(nullable: true),
                    ProjectDuration = table.Column<double>(nullable: true),
                    RFR_Ref_No = table.Column<long>(nullable: true),
                    RFR_Update = table.Column<long>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ReportID = table.Column<long>(nullable: true),
                    SchoolType = table.Column<string>(nullable: true),
                    SkilledLaborCost = table.Column<double>(nullable: true),
                    TPC_Adj = table.Column<double>(nullable: true),
                    Total_Amount = table.Column<double>(nullable: true),
                    Totaltrancheamount = table.Column<double>(nullable: true),
                    Tranch1Flag = table.Column<long>(nullable: true),
                    Tranche = table.Column<string>(nullable: true),
                    Tranche1Amt_Old = table.Column<double>(nullable: true),
                    Tranche1Perc = table.Column<double>(nullable: true),
                    Tranche1Phy = table.Column<double>(nullable: true),
                    Tranche1amount = table.Column<double>(nullable: true),
                    Tranche1date = table.Column<DateTime>(nullable: true),
                    Tranche2Amt_Old = table.Column<double>(nullable: true),
                    Tranche2Perc = table.Column<double>(nullable: true),
                    Tranche2Phy = table.Column<double>(nullable: true),
                    Tranche2amount = table.Column<double>(nullable: true),
                    Tranche2date = table.Column<DateTime>(nullable: true),
                    Tranche3Amt_Old = table.Column<double>(nullable: true),
                    Tranche3Perc = table.Column<double>(nullable: true),
                    Tranche3Phy = table.Column<double>(nullable: true),
                    Tranche3amount = table.Column<double>(nullable: true),
                    Tranche3date = table.Column<DateTime>(nullable: true),
                    UnskilledLaborCost = table.Column<double>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Vdr_Update = table.Column<long>(nullable: true),
                    WS_wd_Sanitation = table.Column<long>(nullable: true),
                    WaterSystemType = table.Column<long>(nullable: true),
                    Year = table.Column<long>(nullable: true),
                    actual_breakdown = table.Column<double>(nullable: true),
                    actual_female = table.Column<int>(nullable: true),
                    actual_male = table.Column<int>(nullable: true),
                    ancestral_domain_status_id = table.Column<int>(nullable: true),
                    approval_id = table.Column<int>(nullable: true),
                    brgy_code = table.Column<int>(nullable: false),
                    bub_unique_id = table.Column<string>(nullable: true),
                    cadt_no_ips = table.Column<int>(nullable: true),
                    cadteable = table.Column<bool>(nullable: true),
                    city_code = table.Column<int>(nullable: false),
                    community_formation_list = table.Column<string>(nullable: true),
                    community_training_id = table.Column<Guid>(nullable: true),
                    created_by = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    damage_list = table.Column<string>(nullable: true),
                    date_start_financial_actual = table.Column<DateTime>(nullable: true),
                    date_start_financial_planned = table.Column<DateTime>(nullable: true),
                    date_start_physical_actual = table.Column<DateTime>(nullable: true),
                    date_start_physical_planned = table.Column<DateTime>(nullable: true),
                    dbm_project_name = table.Column<string>(nullable: true),
                    deleted_by = table.Column<string>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    deleted_reason = table.Column<string>(nullable: true),
                    dep_ed_amount = table.Column<string>(nullable: true),
                    enrollment_type_id = table.Column<int>(nullable: true),
                    erfr_id_t1 = table.Column<int>(nullable: true),
                    erfr_id_t2 = table.Column<int>(nullable: true),
                    erfr_id_t3 = table.Column<int>(nullable: true),
                    erfr_mibf_date = table.Column<DateTime>(nullable: true),
                    erfr_mibf_grant_cost = table.Column<double>(nullable: true),
                    erfr_mibf_lcc_cost = table.Column<double>(nullable: true),
                    erfr_mibf_project_name = table.Column<string>(nullable: true),
                    erfr_project_id = table.Column<int>(nullable: true),
                    erfr_t1 = table.Column<decimal>(nullable: true),
                    erfr_t2 = table.Column<decimal>(nullable: true),
                    erfr_t3 = table.Column<decimal>(nullable: true),
                    fund_source_id = table.Column<int>(nullable: true),
                    geotagged_act_uploaded_7 = table.Column<int>(nullable: true),
                    geotagged_co_approved_1 = table.Column<int>(nullable: true),
                    geotagged_co_disapproved_5 = table.Column<int>(nullable: true),
                    geotagged_fo_approved_2 = table.Column<int>(nullable: true),
                    geotagged_fo_disapproved_4 = table.Column<int>(nullable: true),
                    geotagged_srpmo_approved_3 = table.Column<int>(nullable: true),
                    geotagged_srpmo_disapproved_6 = table.Column<int>(nullable: true),
                    has_after_photo = table.Column<bool>(nullable: true),
                    has_before_photo = table.Column<bool>(nullable: true),
                    has_closed_account = table.Column<bool>(nullable: true),
                    has_ip_presence = table.Column<bool>(nullable: true),
                    has_local_counterpart = table.Column<bool>(nullable: true),
                    has_marker = table.Column<bool>(nullable: true),
                    has_onm_group = table.Column<bool>(nullable: true),
                    has_sc_cnc = table.Column<bool>(nullable: true),
                    has_sc_cno = table.Column<bool>(nullable: true),
                    has_sc_cp = table.Column<bool>(nullable: true),
                    has_sc_ecc = table.Column<bool>(nullable: true),
                    has_sc_esmp = table.Column<bool>(nullable: true),
                    has_scanned_spcr = table.Column<bool>(nullable: true),
                    has_set = table.Column<bool>(nullable: true),
                    has_set_score = table.Column<double>(nullable: true),
                    has_t1 = table.Column<bool>(nullable: true),
                    has_t2 = table.Column<bool>(nullable: true),
                    has_t3 = table.Column<bool>(nullable: true),
                    has_turnover_certificate = table.Column<bool>(nullable: true),
                    has_variation = table.Column<bool>(nullable: true),
                    ip_groups = table.Column<string>(nullable: true),
                    is_correct_sp_id = table.Column<bool>(nullable: true),
                    is_created_via_migration = table.Column<bool>(nullable: true),
                    is_deped_funded = table.Column<bool>(nullable: true),
                    is_duplicate = table.Column<bool>(nullable: true),
                    is_kalahi_funded = table.Column<bool>(nullable: true),
                    is_public_school_for_ip = table.Column<bool>(nullable: true),
                    is_pulled_from_pra = table.Column<bool>(nullable: true),
                    is_updated = table.Column<bool>(nullable: true),
                    land_acquisition_id = table.Column<int>(nullable: true),
                    land_aq_blgu_resolution = table.Column<bool>(nullable: true),
                    land_aq_deed_of_donation = table.Column<bool>(nullable: true),
                    land_aq_deped_certification = table.Column<bool>(nullable: true),
                    land_aq_other = table.Column<bool>(nullable: true),
                    land_aq_unsfruct = table.Column<bool>(nullable: true),
                    land_ownership_dod = table.Column<bool>(nullable: true),
                    land_ownership_gov = table.Column<bool>(nullable: true),
                    land_ownership_private_titled = table.Column<bool>(nullable: true),
                    land_ownership_private_untitled = table.Column<bool>(nullable: true),
                    land_ownership_public_lands = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_updated_by = table.Column<string>(nullable: true),
                    last_updated_date = table.Column<DateTime>(nullable: true),
                    lgu_engagement = table.Column<string>(nullable: true),
                    maintainance_list = table.Column<string>(nullable: true),
                    mibf_prioritization_id = table.Column<Guid>(nullable: false),
                    modality_category_id = table.Column<int>(nullable: true),
                    modality_id = table.Column<int>(nullable: false),
                    mode_of_implementation = table.Column<string>(nullable: true),
                    movement_id = table.Column<int>(nullable: true),
                    ncip_submitted = table.Column<bool>(nullable: true),
                    ncip_submitted_date = table.Column<DateTime>(nullable: true),
                    ncip_validated = table.Column<bool>(nullable: true),
                    ncip_validated_date = table.Column<DateTime>(nullable: true),
                    no_actual_families = table.Column<int>(nullable: true),
                    no_actual_female = table.Column<int>(nullable: true),
                    no_actual_households = table.Column<int>(nullable: true),
                    no_actual_ip_families = table.Column<int>(nullable: true),
                    no_actual_ip_households = table.Column<int>(nullable: true),
                    no_actual_male = table.Column<int>(nullable: true),
                    no_actual_pantawid_families = table.Column<int>(nullable: true),
                    no_actual_pantawid_households = table.Column<int>(nullable: true),
                    no_actual_pwd_families = table.Column<int>(nullable: true),
                    no_actual_pwd_households = table.Column<int>(nullable: true),
                    no_actual_senior_families = table.Column<int>(nullable: true),
                    no_actual_senior_households = table.Column<int>(nullable: true),
                    no_actual_slp_families = table.Column<int>(nullable: true),
                    no_actual_slp_households = table.Column<int>(nullable: true),
                    no_non_pantawid_families = table.Column<int>(nullable: true),
                    no_pantawid_families = table.Column<int>(nullable: true),
                    no_target_families = table.Column<int>(nullable: true),
                    no_target_female = table.Column<int>(nullable: true),
                    no_target_households = table.Column<int>(nullable: true),
                    no_target_ip_families = table.Column<int>(nullable: true),
                    no_target_ip_households = table.Column<int>(nullable: true),
                    no_target_male = table.Column<int>(nullable: true),
                    no_target_pantawid_families = table.Column<int>(nullable: true),
                    no_target_pantawid_households = table.Column<int>(nullable: true),
                    no_target_pwd_families = table.Column<int>(nullable: true),
                    no_target_pwd_households = table.Column<int>(nullable: true),
                    no_target_senior_families = table.Column<int>(nullable: true),
                    no_target_senior_households = table.Column<int>(nullable: true),
                    no_target_slp_families = table.Column<int>(nullable: true),
                    no_target_slp_households = table.Column<int>(nullable: true),
                    on_process_cadt = table.Column<bool>(nullable: true),
                    operation_maintainance_cost = table.Column<double>(nullable: true),
                    other_land_acquisition = table.Column<string>(nullable: true),
                    path = table.Column<string>(nullable: true),
                    photos = table.Column<int>(nullable: true),
                    phy_construction_actual = table.Column<decimal>(nullable: true),
                    phy_construction_actual_secondary = table.Column<decimal>(nullable: true),
                    phy_construction_target = table.Column<decimal>(nullable: true),
                    phy_construction_target_secondary = table.Column<decimal>(nullable: true),
                    phy_has_construction_target = table.Column<bool>(nullable: true),
                    phy_has_construction_target_secondary = table.Column<bool>(nullable: true),
                    phy_has_improvement_target = table.Column<bool>(nullable: true),
                    phy_has_improvement_target_secondary = table.Column<bool>(nullable: true),
                    phy_has_purchase_target = table.Column<bool>(nullable: true),
                    phy_has_purchase_target_secondary = table.Column<bool>(nullable: true),
                    phy_has_rehabilitation_target = table.Column<bool>(nullable: true),
                    phy_has_rehabilitation_target_secondary = table.Column<bool>(nullable: true),
                    phy_improvement_actual = table.Column<decimal>(nullable: true),
                    phy_improvement_actual_secondary = table.Column<decimal>(nullable: true),
                    phy_improvement_target = table.Column<decimal>(nullable: true),
                    phy_improvement_target_secondary = table.Column<decimal>(nullable: true),
                    phy_purchase_actual = table.Column<decimal>(nullable: true),
                    phy_purchase_actual_secondary = table.Column<decimal>(nullable: true),
                    phy_purchase_target = table.Column<decimal>(nullable: true),
                    phy_purchase_target_secondary = table.Column<decimal>(nullable: true),
                    phy_rehabilitation_actual = table.Column<decimal>(nullable: true),
                    phy_rehabilitation_actual_secondary = table.Column<decimal>(nullable: true),
                    phy_rehabilitation_target = table.Column<decimal>(nullable: true),
                    phy_rehabilitation_target_secondary = table.Column<decimal>(nullable: true),
                    physical_status_category_id = table.Column<int>(nullable: true),
                    physical_status_id = table.Column<int>(nullable: false),
                    pims_mibf_date = table.Column<DateTime>(nullable: true),
                    pims_mibf_grant_cost = table.Column<double>(nullable: true),
                    pims_mibf_lcc_cost = table.Column<double>(nullable: true),
                    pims_mibf_project_mame = table.Column<string>(nullable: true),
                    pow_list = table.Column<string>(nullable: true),
                    project_sequence = table.Column<string>(nullable: true),
                    project_type_id = table.Column<int>(nullable: false),
                    prov_code = table.Column<int>(nullable: false),
                    push_status_id = table.Column<int>(nullable: true),
                    region_code = table.Column<int>(nullable: false),
                    resume_order_list = table.Column<string>(nullable: true),
                    rfr_status = table.Column<string>(nullable: true),
                    s_phy_construction_actual = table.Column<string>(nullable: true),
                    s_phy_construction_actual_secondary = table.Column<string>(nullable: true),
                    s_phy_construction_target = table.Column<string>(nullable: true),
                    s_phy_construction_target_secondary = table.Column<string>(nullable: true),
                    s_phy_improvement_actual = table.Column<string>(nullable: true),
                    s_phy_improvement_actual_secondary = table.Column<string>(nullable: true),
                    s_phy_improvement_target = table.Column<string>(nullable: true),
                    s_phy_improvement_target_secondary = table.Column<string>(nullable: true),
                    s_phy_purchase_actual = table.Column<string>(nullable: true),
                    s_phy_purchase_actual_secondary = table.Column<string>(nullable: true),
                    s_phy_purchase_target = table.Column<string>(nullable: true),
                    s_phy_purchase_target_secondary = table.Column<string>(nullable: true),
                    s_phy_rehabilitation_actual = table.Column<string>(nullable: true),
                    s_phy_rehabilitation_actual_secondary = table.Column<string>(nullable: true),
                    s_phy_rehabilitation_target = table.Column<string>(nullable: true),
                    s_phy_rehabilitation_target_secondary = table.Column<string>(nullable: true),
                    set_date_eval = table.Column<DateTime>(nullable: true),
                    set_financial = table.Column<double>(nullable: true),
                    set_institutional_linkage = table.Column<double>(nullable: true),
                    set_mode = table.Column<int>(nullable: true),
                    set_onm_group = table.Column<double>(nullable: true),
                    set_organization = table.Column<double>(nullable: true),
                    set_physical = table.Column<double>(nullable: true),
                    set_physical_description = table.Column<string>(nullable: true),
                    set_sp_utilization = table.Column<double>(nullable: true),
                    set_total_score = table.Column<double>(nullable: true),
                    sub_project_id = table.Column<int>(nullable: false),
                    sub_project_name = table.Column<string>(nullable: false),
                    suspension_order_list = table.Column<string>(nullable: true),
                    target_female = table.Column<int>(nullable: true),
                    target_male = table.Column<int>(nullable: true),
                    training_category_id = table.Column<int>(nullable: true),
                    variation_order_list = table.Column<string>(nullable: true),
                    with_cadt = table.Column<bool>(nullable: true),
                    with_tariff = table.Column<bool>(nullable: true),
                    within_env_crit_area = table.Column<bool>(nullable: true),
                    year_of_implementation = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_project", x => x.sub_project_unique_id);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_physical_status_category_physical_status_category_id",
                        column: x => x.physical_status_category_id,
                        principalTable: "lib_physical_status_category",
                        principalColumn: "physical_status_category_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_physical_status_physical_status_id",
                        column: x => x.physical_status_id,
                        principalTable: "lib_physical_status",
                        principalColumn: "physical_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_project_type_project_type_id",
                        column: x => x.project_type_id,
                        principalTable: "lib_project_type",
                        principalColumn: "project_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mlgu_financial_data",
                columns: table => new
                {
                    mlgu_financial_data_record_id = table.Column<Guid>(nullable: false),
                    administrative_governance_2009 = table.Column<int>(nullable: true),
                    administrative_governance_2010 = table.Column<int>(nullable: true),
                    administrative_governance_2011 = table.Column<int>(nullable: true),
                    administrative_governance_2012 = table.Column<int>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    dof_blgf_financial_data_record_id = table.Column<int>(nullable: true),
                    economic_governance_2009 = table.Column<int>(nullable: true),
                    economic_governance_2010 = table.Column<int>(nullable: true),
                    economic_governance_2011 = table.Column<int>(nullable: true),
                    economic_governance_2012 = table.Column<int>(nullable: true),
                    environmental_governance_2009 = table.Column<int>(nullable: true),
                    environmental_governance_2010 = table.Column<int>(nullable: true),
                    environmental_governance_2011 = table.Column<int>(nullable: true),
                    environmental_governance_2012 = table.Column<int>(nullable: true),
                    expenditures_economic_services = table.Column<int>(nullable: true),
                    expenditures_economic_services_source = table.Column<string>(nullable: true),
                    expenditures_educ_culture_etc = table.Column<int>(nullable: true),
                    expenditures_educ_culture_etc_source = table.Column<string>(nullable: true),
                    expenditures_gen_public_services = table.Column<int>(nullable: true),
                    expenditures_gen_public_services_source = table.Column<string>(nullable: true),
                    expenditures_health_services = table.Column<int>(nullable: true),
                    expenditures_health_services_source = table.Column<string>(nullable: true),
                    expenditures_housing_comm_devt = table.Column<int>(nullable: true),
                    expenditures_housing_comm_devt_source = table.Column<string>(nullable: true),
                    expenditures_labor_and_employment = table.Column<int>(nullable: true),
                    expenditures_labor_and_employment_source = table.Column<string>(nullable: true),
                    expenditures_other_purposes = table.Column<int>(nullable: true),
                    expenditures_other_purposes_source = table.Column<string>(nullable: true),
                    expenditures_social_welfare_services = table.Column<int>(nullable: true),
                    expenditures_social_welfare_services_source = table.Column<string>(nullable: true),
                    extraordinary_receipts = table.Column<int>(nullable: true),
                    extraordinary_receipts_source = table.Column<string>(nullable: true),
                    inter_local_transfers = table.Column<int>(nullable: true),
                    inter_local_transfers_source = table.Column<string>(nullable: true),
                    ira_share = table.Column<int>(nullable: true),
                    ira_share_source = table.Column<string>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lgpms_data_id = table.Column<int>(nullable: true),
                    locally_shared_revenues = table.Column<int>(nullable: true),
                    locally_sourced_revenues = table.Column<int>(nullable: true),
                    locally_sourced_revenues_source = table.Column<string>(nullable: true),
                    other_revenues_total = table.Column<int>(nullable: true),
                    other_revenues_total_source = table.Column<string>(nullable: true),
                    other_shares_natl_tax = table.Column<int>(nullable: true),
                    other_shares_natl_tax_source = table.Column<string>(nullable: true),
                    overall_performance_index_2009 = table.Column<int>(nullable: true),
                    overall_performance_index_2010 = table.Column<int>(nullable: true),
                    overall_performance_index_2011 = table.Column<int>(nullable: true),
                    overall_performance_index_2012 = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    psgc_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    social_governance_2009 = table.Column<int>(nullable: true),
                    social_governance_2010 = table.Column<int>(nullable: true),
                    social_governance_2011 = table.Column<int>(nullable: true),
                    social_governance_2012 = table.Column<int>(nullable: true),
                    talakayan_date_end = table.Column<DateTime>(nullable: true),
                    talakayan_date_start = table.Column<DateTime>(nullable: true),
                    talakayan_yr_id = table.Column<int>(nullable: false),
                    total_lgu_income = table.Column<int>(nullable: true),
                    total_lgu_income_source = table.Column<string>(nullable: true),
                    valuing_fundamentals_of_good_gov_2009 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2010 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2011 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2012 = table.Column<int>(nullable: true),
                    year_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mlgu_financial_data", x => x.mlgu_financial_data_record_id);
                    table.ForeignKey(
                        name: "FK_mlgu_financial_data_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mlgu_financial_data_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mlgu_financial_data_dof_blgf_financial_data_dof_blgf_financial_data_record_id",
                        column: x => x.dof_blgf_financial_data_record_id,
                        principalTable: "dof_blgf_financial_data",
                        principalColumn: "dof_blgf_financial_data_record_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mlgu_financial_data_lgpms_data_lgpms_data_id",
                        column: x => x.lgpms_data_id,
                        principalTable: "lgpms_data",
                        principalColumn: "lgpms_data_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mlgu_financial_data_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mlgu_financial_data_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mlgu_financial_data_talakayan_year_talakayan_yr_id",
                        column: x => x.talakayan_yr_id,
                        principalTable: "talakayan_year",
                        principalColumn: "talakayan_yr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "municipal_financial_profile",
                columns: table => new
                {
                    municipal_financial_profile_id = table.Column<Guid>(nullable: false),
                    administrative_governance_2009 = table.Column<int>(nullable: true),
                    administrative_governance_2010 = table.Column<int>(nullable: true),
                    administrative_governance_2011 = table.Column<int>(nullable: true),
                    administrative_governance_2012 = table.Column<int>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    dof_blgf_financial_data_record_id = table.Column<int>(nullable: true),
                    economic_governance_2009 = table.Column<int>(nullable: true),
                    economic_governance_2010 = table.Column<int>(nullable: true),
                    economic_governance_2011 = table.Column<int>(nullable: true),
                    economic_governance_2012 = table.Column<int>(nullable: true),
                    environmental_governance_2009 = table.Column<int>(nullable: true),
                    environmental_governance_2010 = table.Column<int>(nullable: true),
                    environmental_governance_2011 = table.Column<int>(nullable: true),
                    environmental_governance_2012 = table.Column<int>(nullable: true),
                    expenditures_economic_services = table.Column<int>(nullable: true),
                    expenditures_educ_culture_etc = table.Column<int>(nullable: true),
                    expenditures_gen_public_services = table.Column<int>(nullable: true),
                    expenditures_health_services = table.Column<int>(nullable: true),
                    expenditures_housing_comm_devt = table.Column<int>(nullable: true),
                    expenditures_labor_and_employment = table.Column<int>(nullable: true),
                    expenditures_other_purposes = table.Column<int>(nullable: true),
                    expenditures_social_welfare_services = table.Column<int>(nullable: true),
                    extraordinary_receipts = table.Column<int>(nullable: true),
                    inter_local_transfers = table.Column<int>(nullable: true),
                    ira_share = table.Column<int>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lgpms_data_id = table.Column<int>(nullable: true),
                    locally_shared_revenues = table.Column<int>(nullable: false),
                    mlgu_expenditures_economic_services = table.Column<int>(nullable: true),
                    mlgu_expenditures_economic_services_source = table.Column<string>(nullable: true),
                    mlgu_expenditures_educ_culture_etc = table.Column<int>(nullable: true),
                    mlgu_expenditures_educ_culture_etc_source = table.Column<string>(nullable: true),
                    mlgu_expenditures_gen_public_services = table.Column<int>(nullable: true),
                    mlgu_expenditures_gen_public_services_source = table.Column<string>(nullable: true),
                    mlgu_expenditures_health_services = table.Column<int>(nullable: true),
                    mlgu_expenditures_health_services_source = table.Column<string>(nullable: true),
                    mlgu_expenditures_housing_comm_devt = table.Column<int>(nullable: true),
                    mlgu_expenditures_housing_comm_devt_source = table.Column<string>(nullable: true),
                    mlgu_expenditures_labor_and_employment = table.Column<int>(nullable: true),
                    mlgu_expenditures_labor_and_employment_source = table.Column<string>(nullable: true),
                    mlgu_expenditures_other_purposes = table.Column<int>(nullable: true),
                    mlgu_expenditures_other_purposes_source = table.Column<string>(nullable: true),
                    mlgu_expenditures_social_welfare_services = table.Column<int>(nullable: true),
                    mlgu_expenditures_social_welfare_services_source = table.Column<string>(nullable: true),
                    mlgu_extraordinary_receipts = table.Column<int>(nullable: true),
                    mlgu_extraordinary_receipts_source = table.Column<string>(nullable: true),
                    mlgu_inter_local_transfers = table.Column<int>(nullable: true),
                    mlgu_inter_local_transfers_source = table.Column<string>(nullable: true),
                    mlgu_ira_share = table.Column<int>(nullable: true),
                    mlgu_ira_share_source = table.Column<string>(nullable: true),
                    mlgu_locally_sourced_revenues = table.Column<decimal>(nullable: true),
                    mlgu_locally_sourced_revenues_source = table.Column<string>(nullable: true),
                    mlgu_other_revenues_total = table.Column<int>(nullable: true),
                    mlgu_other_revenues_total_source = table.Column<string>(nullable: true),
                    mlgu_other_shares_natl_tax = table.Column<int>(nullable: true),
                    mlgu_other_shares_natl_tax_source = table.Column<string>(nullable: true),
                    mlgu_total_lgu_income = table.Column<int>(nullable: true),
                    mlgu_total_lgu_income_source = table.Column<string>(nullable: true),
                    other_revenues_total = table.Column<int>(nullable: true),
                    other_shares_natl_tax = table.Column<int>(nullable: true),
                    overall_performance_index_2009 = table.Column<int>(nullable: true),
                    overall_performance_index_2010 = table.Column<int>(nullable: true),
                    overall_performance_index_2011 = table.Column<int>(nullable: true),
                    overall_performance_index_2012 = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    psgc_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    region_code = table.Column<int>(nullable: false),
                    social_governance_2009 = table.Column<int>(nullable: true),
                    social_governance_2010 = table.Column<int>(nullable: true),
                    social_governance_2011 = table.Column<int>(nullable: true),
                    social_governance_2012 = table.Column<int>(nullable: true),
                    talakayan_date_end = table.Column<DateTime>(nullable: true),
                    talakayan_date_start = table.Column<DateTime>(nullable: true),
                    talakayan_yr_id = table.Column<int>(nullable: false),
                    total_lgu_income = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2009 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2010 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2011 = table.Column<int>(nullable: true),
                    valuing_fundamentals_of_good_gov_2012 = table.Column<int>(nullable: true),
                    year_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipal_financial_profile", x => x.municipal_financial_profile_id);
                    table.ForeignKey(
                        name: "FK_municipal_financial_profile_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_financial_profile_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_municipal_financial_profile_dof_blgf_financial_data_dof_blgf_financial_data_record_id",
                        column: x => x.dof_blgf_financial_data_record_id,
                        principalTable: "dof_blgf_financial_data",
                        principalColumn: "dof_blgf_financial_data_record_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_municipal_financial_profile_lgpms_data_lgpms_data_id",
                        column: x => x.lgpms_data_id,
                        principalTable: "lgpms_data",
                        principalColumn: "lgpms_data_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_municipal_financial_profile_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_financial_profile_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_financial_profile_talakayan_year_talakayan_yr_id",
                        column: x => x.talakayan_yr_id,
                        principalTable: "talakayan_year",
                        principalColumn: "talakayan_yr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_ers_work",
                columns: table => new
                {
                    person_ers_work_id = table.Column<Guid>(nullable: false),
                    actual_cash = table.Column<decimal>(nullable: true),
                    actual_lcc = table.Column<decimal>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    ers_current_work_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    percent = table.Column<decimal>(nullable: true),
                    person_profile_id = table.Column<Guid>(nullable: false),
                    plan_cash = table.Column<decimal>(nullable: true),
                    plan_lcc = table.Column<decimal>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    rate_day = table.Column<decimal>(nullable: true),
                    rate_hauling = table.Column<decimal>(nullable: true),
                    rate_hour = table.Column<decimal>(nullable: true),
                    sub_project_ers_id = table.Column<Guid>(nullable: false),
                    unit_hauling = table.Column<string>(nullable: true),
                    work_days = table.Column<decimal>(nullable: true),
                    work_hauling = table.Column<decimal>(nullable: true),
                    work_hours = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_ers_work", x => x.person_ers_work_id);
                    table.ForeignKey(
                        name: "FK_person_ers_work_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_ers_work_lib_ers_current_work_ers_current_work_id",
                        column: x => x.ers_current_work_id,
                        principalTable: "lib_ers_current_work",
                        principalColumn: "ers_current_work_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_ers_work_person_profile_person_profile_id",
                        column: x => x.person_profile_id,
                        principalTable: "person_profile",
                        principalColumn: "person_profile_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_ers_work_sub_project_ers_sub_project_ers_id",
                        column: x => x.sub_project_ers_id,
                        principalTable: "sub_project_ers",
                        principalColumn: "sub_project_ers_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_volunteer_record",
                columns: table => new
                {
                    person_volunteer_record_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    person_profile_id = table.Column<Guid>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: true),
                    volunteer_committee_id = table.Column<int>(nullable: false),
                    volunteer_committee_position_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_volunteer_record", x => x.person_volunteer_record_id);
                    table.ForeignKey(
                        name: "FK_person_volunteer_record_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_volunteer_record_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_volunteer_record_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_volunteer_record_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_volunteer_record_person_profile_person_profile_id",
                        column: x => x.person_profile_id,
                        principalTable: "person_profile",
                        principalColumn: "person_profile_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_volunteer_record_lib_volunteer_committee_volunteer_committee_id",
                        column: x => x.volunteer_committee_id,
                        principalTable: "lib_volunteer_committee",
                        principalColumn: "volunteer_committee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_volunteer_record_lib_volunteer_committee_position_volunteer_committee_position_id",
                        column: x => x.volunteer_committee_position_id,
                        principalTable: "lib_volunteer_committee_position",
                        principalColumn: "volunteer_committee_position_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "file_attachment",
                columns: table => new
                {
                    file_attachment_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    means_of_verification_id = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    record_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_attachment", x => x.file_attachment_id);
                    table.ForeignKey(
                        name: "FK_file_attachment_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_file_attachment_means_of_verification_means_of_verification_id",
                        column: x => x.means_of_verification_id,
                        principalTable: "means_of_verification",
                        principalColumn: "means_of_verification_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brgy_assembly_ip",
                columns: table => new
                {
                    brgy_assembly_ip_id = table.Column<Guid>(nullable: false),
                    Selected = table.Column<bool>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_assembly_id = table.Column<Guid>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    ip_group_id = table.Column<int>(nullable: false),
                    ip_group_name = table.Column<string>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brgy_assembly_ip", x => x.brgy_assembly_ip_id);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_ip_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_ip_brgy_assembly_brgy_assembly_id",
                        column: x => x.brgy_assembly_id,
                        principalTable: "brgy_assembly",
                        principalColumn: "brgy_assembly_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_assembly_ip_lib_ip_group_ip_group_id",
                        column: x => x.ip_group_id,
                        principalTable: "lib_ip_group",
                        principalColumn: "ip_group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brgy_profile_ip",
                columns: table => new
                {
                    brgy_profile_ip_id = table.Column<Guid>(nullable: false),
                    brgy_profile_id = table.Column<Guid>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    ip_group_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    no_families = table.Column<int>(nullable: false),
                    no_female = table.Column<int>(nullable: false),
                    no_household = table.Column<int>(nullable: false),
                    no_male = table.Column<int>(nullable: false),
                    old_id = table.Column<string>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brgy_profile_ip", x => x.brgy_profile_ip_id);
                    table.ForeignKey(
                        name: "FK_brgy_profile_ip_brgy_profile_brgy_profile_id",
                        column: x => x.brgy_profile_id,
                        principalTable: "brgy_profile",
                        principalColumn: "brgy_profile_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brgy_profile_ip_lib_ip_group_ip_group_id",
                        column: x => x.ip_group_id,
                        principalTable: "lib_ip_group",
                        principalColumn: "ip_group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ceac_tracking",
                columns: table => new
                {
                    ceac_tracking_id = table.Column<Guid>(nullable: false),
                    actual_end = table.Column<DateTime>(nullable: true),
                    actual_start = table.Column<DateTime>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: true),
                    catch_end = table.Column<DateTime>(nullable: true),
                    catch_start = table.Column<DateTime>(nullable: true),
                    ceac_activity_id = table.Column<int>(nullable: false),
                    ceac_list_id = table.Column<Guid>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    cycle_id = table.Column<int>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    enrollment_id = table.Column<int>(nullable: false),
                    fund_source_id = table.Column<int>(nullable: false),
                    implementation_status_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lgu_level_id = table.Column<int>(nullable: false),
                    old_id = table.Column<string>(nullable: true),
                    plan_end = table.Column<DateTime>(nullable: true),
                    plan_start = table.Column<DateTime>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    reference_id = table.Column<Guid>(nullable: true),
                    region_code = table.Column<int>(nullable: false),
                    training_category_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ceac_tracking", x => x.ceac_tracking_id);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_ceac_list_ceac_list_id",
                        column: x => x.ceac_list_id,
                        principalTable: "ceac_list",
                        principalColumn: "ceac_list_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_cycle_cycle_id",
                        column: x => x.cycle_id,
                        principalTable: "lib_cycle",
                        principalColumn: "cycle_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_enrollment_enrollment_id",
                        column: x => x.enrollment_id,
                        principalTable: "lib_enrollment",
                        principalColumn: "enrollment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_fund_source_fund_source_id",
                        column: x => x.fund_source_id,
                        principalTable: "lib_fund_source",
                        principalColumn: "fund_source_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_implementation_status_implementation_status_id",
                        column: x => x.implementation_status_id,
                        principalTable: "lib_implementation_status",
                        principalColumn: "implementation_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_lgu_level_lgu_level_id",
                        column: x => x.lgu_level_id,
                        principalTable: "lib_lgu_level",
                        principalColumn: "lgu_level_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ceac_tracking_lib_training_category_training_category_id",
                        column: x => x.training_category_id,
                        principalTable: "lib_training_category",
                        principalColumn: "training_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mibf_criteria",
                columns: table => new
                {
                    mibf_criteria_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    community_training_id = table.Column<Guid>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    criteria = table.Column<string>(nullable: true),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    rate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mibf_criteria", x => x.mibf_criteria_id);
                    table.ForeignKey(
                        name: "FK_mibf_criteria_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mibf_criteria_community_training_community_training_id",
                        column: x => x.community_training_id,
                        principalTable: "community_training",
                        principalColumn: "community_training_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mibf_prioritization",
                columns: table => new
                {
                    mibf_prioritization_id = table.Column<Guid>(nullable: false),
                    added_to_spi = table.Column<bool>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    brgy_code = table.Column<int>(nullable: false),
                    city_code = table.Column<int>(nullable: false),
                    community_training_id = table.Column<Guid>(nullable: false),
                    coverage = table.Column<string>(nullable: true),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_priority = table.Column<bool>(nullable: true),
                    kc_amount = table.Column<double>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lcc_amount = table.Column<double>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    pamana_amount = table.Column<double>(nullable: true),
                    priority = table.Column<int>(nullable: true),
                    project_name = table.Column<string>(nullable: true),
                    project_type_id = table.Column<int>(nullable: true),
                    prov_code = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    rank = table.Column<int>(nullable: true),
                    region_code = table.Column<int>(nullable: false),
                    type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mibf_prioritization", x => x.mibf_prioritization_id);
                    table.ForeignKey(
                        name: "FK_mibf_prioritization_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mibf_prioritization_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mibf_prioritization_lib_city_city_code",
                        column: x => x.city_code,
                        principalTable: "lib_city",
                        principalColumn: "city_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mibf_prioritization_community_training_community_training_id",
                        column: x => x.community_training_id,
                        principalTable: "community_training",
                        principalColumn: "community_training_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mibf_prioritization_lib_project_type_project_type_id",
                        column: x => x.project_type_id,
                        principalTable: "lib_project_type",
                        principalColumn: "project_type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mibf_prioritization_lib_province_prov_code",
                        column: x => x.prov_code,
                        principalTable: "lib_province",
                        principalColumn: "prov_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mibf_prioritization_lib_region_region_code",
                        column: x => x.region_code,
                        principalTable: "lib_region",
                        principalColumn: "region_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_training",
                columns: table => new
                {
                    person_training_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    community_training_id = table.Column<Guid>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    ig_bdc = table.Column<bool>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_mdc = table.Column<bool>(nullable: true),
                    is_participant = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    person_profile_id = table.Column<Guid>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_training", x => x.person_training_id);
                    table.ForeignKey(
                        name: "FK_person_training_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_training_community_training_community_training_id",
                        column: x => x.community_training_id,
                        principalTable: "community_training",
                        principalColumn: "community_training_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_training_person_profile_person_profile_id",
                        column: x => x.person_profile_id,
                        principalTable: "person_profile",
                        principalColumn: "person_profile_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_training_lib_push_status_push_status_id",
                        column: x => x.push_status_id,
                        principalTable: "lib_push_status",
                        principalColumn: "push_status_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "psa_problem",
                columns: table => new
                {
                    psa_problem_id = table.Column<Guid>(nullable: false),
                    approval_id = table.Column<int>(nullable: false),
                    community_training_id = table.Column<Guid>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    old_id = table.Column<string>(nullable: true),
                    problem = table.Column<string>(nullable: true),
                    psa_problem_category_id = table.Column<int>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    rank = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_psa_problem", x => x.psa_problem_id);
                    table.ForeignKey(
                        name: "FK_psa_problem_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_psa_problem_community_training_community_training_id",
                        column: x => x.community_training_id,
                        principalTable: "community_training",
                        principalColumn: "community_training_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_psa_problem_lib_psa_problem_category_psa_problem_category_id",
                        column: x => x.psa_problem_category_id,
                        principalTable: "lib_psa_problem_category",
                        principalColumn: "psa_problem_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grievance_record_discussion",
                columns: table => new
                {
                    grievance_record_discussion_id = table.Column<Guid>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_by_name = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    grievance_record_id = table.Column<Guid>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    last_sync_source_id = table.Column<int>(nullable: true),
                    position = table.Column<string>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grievance_record_discussion", x => x.grievance_record_discussion_id);
                    table.ForeignKey(
                        name: "FK_grievance_record_discussion_grievance_record_grievance_record_id",
                        column: x => x.grievance_record_id,
                        principalTable: "grievance_record",
                        principalColumn: "grievance_record_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "muni_profile_fund_use",
                columns: table => new
                {
                    muni_profile_fund_use_id = table.Column<Guid>(nullable: false),
                    amount = table.Column<double>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    is_gad = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    muni_profile_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_muni_profile_fund_use", x => x.muni_profile_fund_use_id);
                    table.ForeignKey(
                        name: "FK_muni_profile_fund_use_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_muni_profile_fund_use_muni_profile_muni_profile_id",
                        column: x => x.muni_profile_id,
                        principalTable: "muni_profile",
                        principalColumn: "muni_profile_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "muni_profile_income",
                columns: table => new
                {
                    muni_profile_income_id = table.Column<Guid>(nullable: false),
                    amount = table.Column<double>(nullable: true),
                    approval_id = table.Column<int>(nullable: false),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    deleted_by = table.Column<int>(nullable: true),
                    deleted_date = table.Column<DateTime>(nullable: true),
                    families = table.Column<int>(nullable: false),
                    households = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: true),
                    last_modified_by = table.Column<int>(nullable: true),
                    last_modified_date = table.Column<DateTime>(nullable: true),
                    lib_source_incomesource_income_id = table.Column<int>(nullable: true),
                    muni_profile_id = table.Column<Guid>(nullable: false),
                    push_date = table.Column<DateTime>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_muni_profile_income", x => x.muni_profile_income_id);
                    table.ForeignKey(
                        name: "FK_muni_profile_income_lib_approval_approval_id",
                        column: x => x.approval_id,
                        principalTable: "lib_approval",
                        principalColumn: "approval_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_muni_profile_income_lib_source_income_lib_source_incomesource_income_id",
                        column: x => x.lib_source_incomesource_income_id,
                        principalTable: "lib_source_income",
                        principalColumn: "source_income_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_muni_profile_income_muni_profile_muni_profile_id",
                        column: x => x.muni_profile_id,
                        principalTable: "muni_profile",
                        principalColumn: "muni_profile_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_project_coverage",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Selected = table.Column<bool>(nullable: false),
                    brgy_code = table.Column<int>(nullable: false),
                    brgy_name = table.Column<string>(nullable: true),
                    push_status_id = table.Column<int>(nullable: false),
                    sub_project_id = table.Column<int>(nullable: false),
                    sub_project_unique_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_project_coverage", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_project_coverage_lib_brgy_brgy_code",
                        column: x => x.brgy_code,
                        principalTable: "lib_brgy",
                        principalColumn: "brgy_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sub_project_coverage_sub_project_sub_project_unique_id",
                        column: x => x.sub_project_unique_id,
                        principalTable: "sub_project",
                        principalColumn: "sub_project_unique_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_approval_id",
                table: "brgy_assembly",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_barangay_assembly_purpose_id",
                table: "brgy_assembly",
                column: "barangay_assembly_purpose_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_brgy_code",
                table: "brgy_assembly",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_city_code",
                table: "brgy_assembly",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_cycle_id",
                table: "brgy_assembly",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_enrollment_id",
                table: "brgy_assembly",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_fund_source_id",
                table: "brgy_assembly",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_prov_code",
                table: "brgy_assembly",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_push_status_id",
                table: "brgy_assembly",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_region_code",
                table: "brgy_assembly",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_ip_approval_id",
                table: "brgy_assembly_ip",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_ip_brgy_assembly_id",
                table: "brgy_assembly_ip",
                column: "brgy_assembly_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_assembly_ip_ip_group_id",
                table: "brgy_assembly_ip",
                column: "ip_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_eca_brgy_code",
                table: "brgy_eca",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_eca_city_code",
                table: "brgy_eca",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_eca_eca_type_id",
                table: "brgy_eca",
                column: "eca_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_eca_prov_code",
                table: "brgy_eca",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_eca_region_code",
                table: "brgy_eca",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_approval_id",
                table: "brgy_profile",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_brgy_code",
                table: "brgy_profile",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_city_code",
                table: "brgy_profile",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_cycle_id",
                table: "brgy_profile",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_fund_source_id",
                table: "brgy_profile",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_prov_code",
                table: "brgy_profile",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_region_code",
                table: "brgy_profile",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_ip_brgy_profile_id",
                table: "brgy_profile_ip",
                column: "brgy_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_brgy_profile_ip_ip_group_id",
                table: "brgy_profile_ip",
                column: "ip_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_approval_id",
                table: "ceac_list",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_brgy_code",
                table: "ceac_list",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_city_code",
                table: "ceac_list",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_cycle_id",
                table: "ceac_list",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_enrollment_id",
                table: "ceac_list",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_fund_source_id",
                table: "ceac_list",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_prov_code",
                table: "ceac_list",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_push_status_id",
                table: "ceac_list",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_list_region_code",
                table: "ceac_list",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_approval_id",
                table: "ceac_tracking",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_brgy_code",
                table: "ceac_tracking",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_ceac_list_id",
                table: "ceac_tracking",
                column: "ceac_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_city_code",
                table: "ceac_tracking",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_cycle_id",
                table: "ceac_tracking",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_enrollment_id",
                table: "ceac_tracking",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_fund_source_id",
                table: "ceac_tracking",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_implementation_status_id",
                table: "ceac_tracking",
                column: "implementation_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_lgu_level_id",
                table: "ceac_tracking",
                column: "lgu_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_prov_code",
                table: "ceac_tracking",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_push_status_id",
                table: "ceac_tracking",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_region_code",
                table: "ceac_tracking",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_ceac_tracking_training_category_id",
                table: "ceac_tracking",
                column: "training_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_approval_id",
                table: "community_training",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_brgy_code",
                table: "community_training",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_city_code",
                table: "community_training",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_cycle_id",
                table: "community_training",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_enrollment_id",
                table: "community_training",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_fund_source_id",
                table: "community_training",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_lgu_level_id",
                table: "community_training",
                column: "lgu_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_prov_code",
                table: "community_training",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_push_status_id",
                table: "community_training",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_region_code",
                table: "community_training",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_community_training_training_category_id",
                table: "community_training",
                column: "training_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_attached_document_mov_list_id",
                table: "attached_document",
                column: "mov_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_approval_id",
                table: "community_organization",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_brgy_code",
                table: "community_organization",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_city_code",
                table: "community_organization",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_cycle_id",
                table: "community_organization",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_enrollment_id",
                table: "community_organization",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_fund_source_id",
                table: "community_organization",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_lgu_level_id",
                table: "community_organization",
                column: "lgu_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_prov_code",
                table: "community_organization",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_community_organization_region_code",
                table: "community_organization",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_diaster_coverage_city_code",
                table: "diaster_coverage",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_diaster_coverage_disaster_id",
                table: "diaster_coverage",
                column: "disaster_id");

            migrationBuilder.CreateIndex(
                name: "IX_diaster_coverage_prov_code",
                table: "diaster_coverage",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_diaster_coverage_region_code",
                table: "diaster_coverage",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_disaster_disaster_type_id",
                table: "disaster",
                column: "disaster_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_file_attachment_approval_id",
                table: "file_attachment",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_file_attachment_means_of_verification_id",
                table: "file_attachment",
                column: "means_of_verification_id");

            migrationBuilder.CreateIndex(
                name: "IX_means_of_verification_table_name_id",
                table: "means_of_verification",
                column: "table_name_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_post_disaster_condition_id",
                table: "sub_project_post_disaster",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_post_disaster_disaster_id",
                table: "sub_project_post_disaster",
                column: "disaster_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_pre_disaster_condition_id",
                table: "sub_project_pre_disaster",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "IX_dof_blgf_financial_data_city_code",
                table: "dof_blgf_financial_data",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_dof_blgf_financial_data_prov_code",
                table: "dof_blgf_financial_data",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_dof_blgf_financial_data_region_code",
                table: "dof_blgf_financial_data",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_lgpms_data_city_code",
                table: "lgpms_data",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_lgpms_data_prov_code",
                table: "lgpms_data",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_lgpms_data_region_code",
                table: "lgpms_data",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_mlgu_financial_data_city_code",
                table: "mlgu_financial_data",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_mlgu_financial_data_cycle_id",
                table: "mlgu_financial_data",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_mlgu_financial_data_dof_blgf_financial_data_record_id",
                table: "mlgu_financial_data",
                column: "dof_blgf_financial_data_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_mlgu_financial_data_lgpms_data_id",
                table: "mlgu_financial_data",
                column: "lgpms_data_id");

            migrationBuilder.CreateIndex(
                name: "IX_mlgu_financial_data_prov_code",
                table: "mlgu_financial_data",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_mlgu_financial_data_region_code",
                table: "mlgu_financial_data",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_mlgu_financial_data_talakayan_yr_id",
                table: "mlgu_financial_data",
                column: "talakayan_yr_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_financial_profile_city_code",
                table: "municipal_financial_profile",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_financial_profile_cycle_id",
                table: "municipal_financial_profile",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_financial_profile_dof_blgf_financial_data_record_id",
                table: "municipal_financial_profile",
                column: "dof_blgf_financial_data_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_financial_profile_lgpms_data_id",
                table: "municipal_financial_profile",
                column: "lgpms_data_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_financial_profile_prov_code",
                table: "municipal_financial_profile",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_financial_profile_region_code",
                table: "municipal_financial_profile",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_financial_profile_talakayan_yr_id",
                table: "municipal_financial_profile",
                column: "talakayan_yr_id");

            migrationBuilder.CreateIndex(
                name: "IX_perception_survey_brgy_code",
                table: "perception_survey",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_perception_survey_city_code",
                table: "perception_survey",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_perception_survey_cycle_id",
                table: "perception_survey",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_perception_survey_lib_ip_groupip_group_id",
                table: "perception_survey",
                column: "lib_ip_groupip_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_perception_survey_prov_code",
                table: "perception_survey",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_perception_survey_region_code",
                table: "perception_survey",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_perception_survey_talakayan_yr_id",
                table: "perception_survey",
                column: "talakayan_yr_id");

            migrationBuilder.CreateIndex(
                name: "IX_talakayan_eval_brgy_code",
                table: "talakayan_eval",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_talakayan_eval_city_code",
                table: "talakayan_eval",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_talakayan_eval_prov_code",
                table: "talakayan_eval",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_talakayan_eval_region_code",
                table: "talakayan_eval",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_talakayan_eval_talakayan_yr_id",
                table: "talakayan_eval",
                column: "talakayan_yr_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_approval_id",
                table: "grievance_record",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_brgy_code",
                table: "grievance_record",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_city_code",
                table: "grievance_record",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_cycle_id",
                table: "grievance_record",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_enrollment_id",
                table: "grievance_record",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_fund_source_id",
                table: "grievance_record",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_category_id",
                table: "grievance_record",
                column: "grs_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_complaint_subject_id",
                table: "grievance_record",
                column: "grs_complaint_subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_feedback_id",
                table: "grievance_record",
                column: "grs_feedback_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_filling_mode_id",
                table: "grievance_record",
                column: "grs_filling_mode_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_form_id",
                table: "grievance_record",
                column: "grs_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_intake_level_id",
                table: "grievance_record",
                column: "grs_intake_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_intake_officer_id",
                table: "grievance_record",
                column: "grs_intake_officer_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_nature_id",
                table: "grievance_record",
                column: "grs_nature_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_pincos_actor_id",
                table: "grievance_record",
                column: "grs_pincos_actor_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_resolution_status_id",
                table: "grievance_record",
                column: "grs_resolution_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_grs_sender_designation_id",
                table: "grievance_record",
                column: "grs_sender_designation_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_ip_group_id",
                table: "grievance_record",
                column: "ip_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_prov_code",
                table: "grievance_record",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_region_code",
                table: "grievance_record",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_sex_id",
                table: "grievance_record",
                column: "sex_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_training_category_id",
                table: "grievance_record",
                column: "training_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_grievance_record_discussion_grievance_record_id",
                table: "grievance_record_discussion",
                column: "grievance_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_approval_id",
                table: "grs_installation",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_brgy_code",
                table: "grs_installation",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_city_code",
                table: "grs_installation",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_cycle_id",
                table: "grs_installation",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_fund_source_id",
                table: "grs_installation",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_lgu_level_id",
                table: "grs_installation",
                column: "lgu_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_prov_code",
                table: "grs_installation",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_grs_installation_region_code",
                table: "grs_installation",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_lib_cycle_fund_source_id",
                table: "lib_cycle",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_lib_project_type_major_classification_id",
                table: "lib_project_type",
                column: "major_classification_id");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_criteria_approval_id",
                table: "mibf_criteria",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_criteria_community_training_id",
                table: "mibf_criteria",
                column: "community_training_id");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_prioritization_approval_id",
                table: "mibf_prioritization",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_prioritization_brgy_code",
                table: "mibf_prioritization",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_prioritization_city_code",
                table: "mibf_prioritization",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_prioritization_community_training_id",
                table: "mibf_prioritization",
                column: "community_training_id");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_prioritization_project_type_id",
                table: "mibf_prioritization",
                column: "project_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_prioritization_prov_code",
                table: "mibf_prioritization",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_mibf_prioritization_region_code",
                table: "mibf_prioritization",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_approval_id",
                table: "muni_profile",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_city_code",
                table: "muni_profile",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_cycle_id",
                table: "muni_profile",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_fund_source_id",
                table: "muni_profile",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_prov_code",
                table: "muni_profile",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_region_code",
                table: "muni_profile",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_fund_use_approval_id",
                table: "muni_profile_fund_use",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_fund_use_muni_profile_id",
                table: "muni_profile_fund_use",
                column: "muni_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_income_approval_id",
                table: "muni_profile_income",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_income_lib_source_incomesource_income_id",
                table: "muni_profile_income",
                column: "lib_source_incomesource_income_id");

            migrationBuilder.CreateIndex(
                name: "IX_muni_profile_income_muni_profile_id",
                table: "muni_profile_income",
                column: "muni_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_approval_id",
                table: "municipal_lcc",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_city_code",
                table: "municipal_lcc",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_cycle_id",
                table: "municipal_lcc",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_enrollment_id",
                table: "municipal_lcc",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_fund_source_id",
                table: "municipal_lcc",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_prov_code",
                table: "municipal_lcc",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_push_status_id",
                table: "municipal_lcc",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_lcc_region_code",
                table: "municipal_lcc",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_approval_id",
                table: "municipal_pta",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_city_code",
                table: "municipal_pta",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_cycle_id",
                table: "municipal_pta",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_enrollment_id",
                table: "municipal_pta",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_fund_source_id",
                table: "municipal_pta",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_prov_code",
                table: "municipal_pta",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_push_status_id",
                table: "municipal_pta",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_pta_region_code",
                table: "municipal_pta",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_oversight_committee_approval_id",
                table: "oversight_committee",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_oversight_committee_city_code",
                table: "oversight_committee",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_oversight_committee_cycle_id",
                table: "oversight_committee",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_oversight_committee_enrollment_id",
                table: "oversight_committee",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_oversight_committee_fund_source_id",
                table: "oversight_committee",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_oversight_committee_prov_code",
                table: "oversight_committee",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_oversight_committee_region_code",
                table: "oversight_committee",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_person_ers_work_approval_id",
                table: "person_ers_work",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_ers_work_ers_current_work_id",
                table: "person_ers_work",
                column: "ers_current_work_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_ers_work_person_profile_id",
                table: "person_ers_work",
                column: "person_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_ers_work_sub_project_ers_id",
                table: "person_ers_work",
                column: "sub_project_ers_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_approval_id",
                table: "person_profile",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_brgy_code",
                table: "person_profile",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_city_code",
                table: "person_profile",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_civil_status_id",
                table: "person_profile",
                column: "civil_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_education_attainment_id",
                table: "person_profile",
                column: "education_attainment_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_ip_group_id",
                table: "person_profile",
                column: "ip_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_lgu_position_id",
                table: "person_profile",
                column: "lgu_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_occupation_id",
                table: "person_profile",
                column: "occupation_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_prov_code",
                table: "person_profile",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_push_status_id",
                table: "person_profile",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_profile_region_code",
                table: "person_profile",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_person_training_approval_id",
                table: "person_training",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_training_community_training_id",
                table: "person_training",
                column: "community_training_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_training_person_profile_id",
                table: "person_training",
                column: "person_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_training_push_status_id",
                table: "person_training",
                column: "push_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_volunteer_record_approval_id",
                table: "person_volunteer_record",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_volunteer_record_cycle_id",
                table: "person_volunteer_record",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_volunteer_record_enrollment_id",
                table: "person_volunteer_record",
                column: "enrollment_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_volunteer_record_fund_source_id",
                table: "person_volunteer_record",
                column: "fund_source_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_volunteer_record_person_profile_id",
                table: "person_volunteer_record",
                column: "person_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_volunteer_record_volunteer_committee_id",
                table: "person_volunteer_record",
                column: "volunteer_committee_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_volunteer_record_volunteer_committee_position_id",
                table: "person_volunteer_record",
                column: "volunteer_committee_position_id");

            migrationBuilder.CreateIndex(
                name: "IX_psa_problem_approval_id",
                table: "psa_problem",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_psa_problem_community_training_id",
                table: "psa_problem",
                column: "community_training_id");

            migrationBuilder.CreateIndex(
                name: "IX_psa_problem_psa_problem_category_id",
                table: "psa_problem",
                column: "psa_problem_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_psa_solution_approval_id",
                table: "psa_solution",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_psa_solution_psa_solution_category_id",
                table: "psa_solution",
                column: "psa_solution_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_brgy_code",
                table: "sub_project",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_city_code",
                table: "sub_project",
                column: "city_code");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_cycle_id",
                table: "sub_project",
                column: "cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_physical_status_category_id",
                table: "sub_project",
                column: "physical_status_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_physical_status_id",
                table: "sub_project",
                column: "physical_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_project_type_id",
                table: "sub_project",
                column: "project_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_prov_code",
                table: "sub_project",
                column: "prov_code");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_region_code",
                table: "sub_project",
                column: "region_code");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_coverage_brgy_code",
                table: "sub_project_coverage",
                column: "brgy_code");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_coverage_sub_project_unique_id",
                table: "sub_project_coverage",
                column: "sub_project_unique_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_ers_approval_id",
                table: "sub_project_ers",
                column: "approval_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_project_ers_ers_delivery_mode_id",
                table: "sub_project_ers",
                column: "ers_delivery_mode_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "brgy_assembly_ip");

            migrationBuilder.DropTable(
                name: "brgy_eca");

            migrationBuilder.DropTable(
                name: "brgy_profile_ip");

            migrationBuilder.DropTable(
                name: "ceac_tracking");

            migrationBuilder.DropTable(
                name: "attached_document");

            migrationBuilder.DropTable(
                name: "community_organization");

            migrationBuilder.DropTable(
                name: "diaster_coverage");

            migrationBuilder.DropTable(
                name: "file_attachment");

            migrationBuilder.DropTable(
                name: "report_list");

            migrationBuilder.DropTable(
                name: "sub_project_post_disaster");

            migrationBuilder.DropTable(
                name: "sub_project_pre_disaster");

            migrationBuilder.DropTable(
                name: "erfr_sub_project");

            migrationBuilder.DropTable(
                name: "mlgu_financial_data");

            migrationBuilder.DropTable(
                name: "municipal_financial_profile");

            migrationBuilder.DropTable(
                name: "perception_survey");

            migrationBuilder.DropTable(
                name: "talakayan_eval");

            migrationBuilder.DropTable(
                name: "grievance_record_discussion");

            migrationBuilder.DropTable(
                name: "grs_installation");

            migrationBuilder.DropTable(
                name: "lib_grs_complainant_position");

            migrationBuilder.DropTable(
                name: "lib_kalahi_committee");

            migrationBuilder.DropTable(
                name: "lib_organization");

            migrationBuilder.DropTable(
                name: "lib_sector");

            migrationBuilder.DropTable(
                name: "lib_transpo_mode");

            migrationBuilder.DropTable(
                name: "mibf_criteria");

            migrationBuilder.DropTable(
                name: "mibf_prioritization");

            migrationBuilder.DropTable(
                name: "muni_profile_fund_use");

            migrationBuilder.DropTable(
                name: "muni_profile_income");

            migrationBuilder.DropTable(
                name: "municipal_lcc");

            migrationBuilder.DropTable(
                name: "municipal_pta");

            migrationBuilder.DropTable(
                name: "oversight_committee");

            migrationBuilder.DropTable(
                name: "person_ers_work");

            migrationBuilder.DropTable(
                name: "person_training");

            migrationBuilder.DropTable(
                name: "person_volunteer_record");

            migrationBuilder.DropTable(
                name: "psa_problem");

            migrationBuilder.DropTable(
                name: "psa_solution");

            migrationBuilder.DropTable(
                name: "SPPhoto");

            migrationBuilder.DropTable(
                name: "sub_project_coverage");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "brgy_assembly");

            migrationBuilder.DropTable(
                name: "lib_eca_type");

            migrationBuilder.DropTable(
                name: "brgy_profile");

            migrationBuilder.DropTable(
                name: "ceac_list");

            migrationBuilder.DropTable(
                name: "lib_implementation_status");

            migrationBuilder.DropTable(
                name: "mov_list");

            migrationBuilder.DropTable(
                name: "means_of_verification");

            migrationBuilder.DropTable(
                name: "disaster");

            migrationBuilder.DropTable(
                name: "lib_condition");

            migrationBuilder.DropTable(
                name: "dof_blgf_financial_data");

            migrationBuilder.DropTable(
                name: "lgpms_data");

            migrationBuilder.DropTable(
                name: "talakayan_year");

            migrationBuilder.DropTable(
                name: "grievance_record");

            migrationBuilder.DropTable(
                name: "lib_source_income");

            migrationBuilder.DropTable(
                name: "muni_profile");

            migrationBuilder.DropTable(
                name: "lib_ers_current_work");

            migrationBuilder.DropTable(
                name: "sub_project_ers");

            migrationBuilder.DropTable(
                name: "person_profile");

            migrationBuilder.DropTable(
                name: "lib_volunteer_committee");

            migrationBuilder.DropTable(
                name: "lib_volunteer_committee_position");

            migrationBuilder.DropTable(
                name: "community_training");

            migrationBuilder.DropTable(
                name: "lib_psa_problem_category");

            migrationBuilder.DropTable(
                name: "lib_psa_solution_category");

            migrationBuilder.DropTable(
                name: "sub_project");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "lib_barangay_assembly_purpose");

            migrationBuilder.DropTable(
                name: "table_name");

            migrationBuilder.DropTable(
                name: "lib_disaster_type");

            migrationBuilder.DropTable(
                name: "lib_grs_category");

            migrationBuilder.DropTable(
                name: "lib_grs_complaint_subject");

            migrationBuilder.DropTable(
                name: "lib_grs_feedback");

            migrationBuilder.DropTable(
                name: "lib_grs_filling_mode");

            migrationBuilder.DropTable(
                name: "lib_grs_form");

            migrationBuilder.DropTable(
                name: "lib_grs_intake_level");

            migrationBuilder.DropTable(
                name: "lib_grs_intake_officer");

            migrationBuilder.DropTable(
                name: "lib_grs_nature");

            migrationBuilder.DropTable(
                name: "lib_grs_pincos_actor");

            migrationBuilder.DropTable(
                name: "lib_grs_resolution_status");

            migrationBuilder.DropTable(
                name: "lib_grs_sender_designation");

            migrationBuilder.DropTable(
                name: "lib_sex");

            migrationBuilder.DropTable(
                name: "lib_ers_delivery_mode");

            migrationBuilder.DropTable(
                name: "lib_civil_status");

            migrationBuilder.DropTable(
                name: "lib_education_attainment");

            migrationBuilder.DropTable(
                name: "lib_ip_group");

            migrationBuilder.DropTable(
                name: "lib_lgu_position");

            migrationBuilder.DropTable(
                name: "lib_occupation");

            migrationBuilder.DropTable(
                name: "lib_approval");

            migrationBuilder.DropTable(
                name: "lib_enrollment");

            migrationBuilder.DropTable(
                name: "lib_lgu_level");

            migrationBuilder.DropTable(
                name: "lib_push_status");

            migrationBuilder.DropTable(
                name: "lib_training_category");

            migrationBuilder.DropTable(
                name: "lib_brgy");

            migrationBuilder.DropTable(
                name: "lib_city");

            migrationBuilder.DropTable(
                name: "lib_cycle");

            migrationBuilder.DropTable(
                name: "lib_physical_status_category");

            migrationBuilder.DropTable(
                name: "lib_physical_status");

            migrationBuilder.DropTable(
                name: "lib_project_type");

            migrationBuilder.DropTable(
                name: "lib_province");

            migrationBuilder.DropTable(
                name: "lib_region");

            migrationBuilder.DropTable(
                name: "lib_fund_source");

            migrationBuilder.DropTable(
                name: "lib_major_classification");
        }
    }
}
