using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DeskApp.Data;

namespace DeskApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170619005730_initial_june19")]
    partial class initial_june19
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("DeskApp.DataLayer.act_report_other_activities", b =>
                {
                    b.Property<Guid>("act_report_other_activity_id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("act_other_activity_enddate");

                    b.Property<string>("act_other_activity_name");

                    b.Property<string>("act_other_activity_remarks");

                    b.Property<DateTime?>("act_other_activity_startdate");

                    b.Property<Guid>("act_report_id");

                    b.Property<int>("approval_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.HasKey("act_report_other_activity_id");

                    b.ToTable("act_report_other_activities");
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_assembly", b =>
                {
                    b.Property<Guid>("brgy_assembly_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("barangay_assembly_purpose_id");

                    b.Property<int>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<DateTime?>("date_end");

                    b.Property<DateTime?>("date_start");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int>("fund_source_id");

                    b.Property<string>("highlights");

                    b.Property<string>("ip_leader");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_sector_academe");

                    b.Property<bool?>("is_sector_business");

                    b.Property<bool?>("is_sector_farmer");

                    b.Property<bool?>("is_sector_fisherfolks");

                    b.Property<bool?>("is_sector_government");

                    b.Property<bool?>("is_sector_ip");

                    b.Property<bool?>("is_sector_ngo");

                    b.Property<bool?>("is_sector_po");

                    b.Property<bool?>("is_sector_pwd");

                    b.Property<bool?>("is_sector_religios");

                    b.Property<bool?>("is_sector_senior");

                    b.Property<bool?>("is_sector_women");

                    b.Property<bool?>("is_sector_youth");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("no_atn_female");

                    b.Property<int?>("no_atn_male");

                    b.Property<int?>("no_families");

                    b.Property<int?>("no_household");

                    b.Property<int?>("no_ip_family");

                    b.Property<int?>("no_ip_family_in_barangay");

                    b.Property<int?>("no_ip_female");

                    b.Property<int?>("no_ip_household");

                    b.Property<int?>("no_ip_male");

                    b.Property<int?>("no_lgu_female");

                    b.Property<int?>("no_lgu_male");

                    b.Property<int?>("no_old_female");

                    b.Property<int?>("no_old_male");

                    b.Property<int?>("no_pantawid_family");

                    b.Property<int?>("no_pantawid_family_in_barangay");

                    b.Property<int?>("no_pantawid_household");

                    b.Property<int?>("no_slp_family");

                    b.Property<int?>("no_slp_family_in_barangay");

                    b.Property<int?>("no_slp_household");

                    b.Property<string>("old_id");

                    b.Property<DateTime?>("plan_date_end");

                    b.Property<DateTime?>("plan_date_start");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<bool?>("requirement_1");

                    b.Property<bool?>("requirement_2");

                    b.Property<bool?>("requirement_3");

                    b.Property<bool?>("requirement_4");

                    b.Property<bool?>("requirement_5");

                    b.Property<bool?>("requirement_6");

                    b.Property<int?>("total_families_in_barangay");

                    b.Property<int?>("total_household_in_barangay");

                    b.Property<int?>("total_household_ip_in_barangay");

                    b.Property<int?>("total_household_pantawid_in_barangay");

                    b.Property<int?>("total_household_slp_in_barangay");

                    b.Property<string>("venue");

                    b.HasKey("brgy_assembly_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("barangay_assembly_purpose_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("push_status_id");

                    b.HasIndex("region_code");

                    b.ToTable("brgy_assembly");
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_assembly_ip", b =>
                {
                    b.Property<Guid>("brgy_assembly_ip_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Selected");

                    b.Property<int>("approval_id");

                    b.Property<Guid>("brgy_assembly_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("ip_group_id");

                    b.Property<string>("ip_group_name");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.HasKey("brgy_assembly_ip_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_assembly_id");

                    b.HasIndex("ip_group_id");

                    b.ToTable("brgy_assembly_ip");
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_eca", b =>
                {
                    b.Property<Guid>("brgy_eca_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<string>("description");

                    b.Property<int>("eca_type_id");

                    b.Property<string>("effects");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("location");

                    b.Property<string>("old_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.HasKey("brgy_eca_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("eca_type_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("brgy_eca");
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_profile", b =>
                {
                    b.Property<Guid>("brgy_profile_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("access_addressed");

                    b.Property<string>("access_details");

                    b.Property<string>("access_remarks");

                    b.Property<bool?>("agriculture_addressed");

                    b.Property<string>("agriculture_details");

                    b.Property<string>("agriculture_remarks");

                    b.Property<double?>("alloc_basic");

                    b.Property<double?>("alloc_drrm");

                    b.Property<double?>("alloc_econ");

                    b.Property<double?>("alloc_educ");

                    b.Property<double?>("alloc_env");

                    b.Property<double?>("alloc_gender");

                    b.Property<double?>("alloc_infra");

                    b.Property<double?>("alloc_inst");

                    b.Property<double?>("alloc_others");

                    b.Property<double?>("alloc_peace");

                    b.Property<int>("approval_id");

                    b.Property<double?>("avg_fhdincome");

                    b.Property<double?>("avg_hhincome");

                    b.Property<double?>("avg_iphdincome");

                    b.Property<double?>("avg_mhdincome");

                    b.Property<int?>("baragay_additiondetails");

                    b.Property<int>("brgy_code");

                    b.Property<string>("brgy_projects");

                    b.Property<string>("child_13_16_secondary_ref");

                    b.Property<string>("children_0_5_reference");

                    b.Property<int?>("children_0_5_value");

                    b.Property<int?>("children_13_16_secondary_value");

                    b.Property<string>("children_6_12_elem_reference");

                    b.Property<int?>("children_6_12_elem_value");

                    b.Property<int>("city_code");

                    b.Property<bool?>("communication_addressed");

                    b.Property<string>("communication_details");

                    b.Property<string>("communication_remarks");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<string>("drrm_activity");

                    b.Property<double?>("drrm_utilized");

                    b.Property<string>("eca_list");

                    b.Property<bool?>("employment_addressed");

                    b.Property<string>("employment_details");

                    b.Property<string>("employment_remarks");

                    b.Property<bool?>("environment_addressed");

                    b.Property<string>("environment_details");

                    b.Property<string>("environment_remarks");

                    b.Property<int?>("fem_hdblwfood");

                    b.Property<int?>("fem_hdfoodshrt");

                    b.Property<int?>("fem_hdmkshouse");

                    b.Property<int?>("fem_hdnotoilet");

                    b.Property<int?>("fem_hdpoverty");

                    b.Property<int?>("fem_hdsfwater");

                    b.Property<int?>("fem_hdsquatters");

                    b.Property<int>("fund_source_id");

                    b.Property<string>("gad_activity");

                    b.Property<double?>("gad_utilized");

                    b.Property<bool?>("has_bank");

                    b.Property<bool?>("has_barangay_hall");

                    b.Property<bool?>("has_cap_agri");

                    b.Property<bool?>("has_cap_health");

                    b.Property<bool?>("has_cap_org_dev");

                    b.Property<bool?>("has_cap_others");

                    b.Property<bool?>("has_cementery");

                    b.Property<bool?>("has_college");

                    b.Property<bool?>("has_credit");

                    b.Property<bool?>("has_daycare");

                    b.Property<bool?>("has_drainage");

                    b.Property<bool?>("has_electricity");

                    b.Property<bool?>("has_elementary");

                    b.Property<bool?>("has_emergency_service");

                    b.Property<bool?>("has_evac_center");

                    b.Property<bool?>("has_harvest");

                    b.Property<bool?>("has_health");

                    b.Property<bool?>("has_hospital");

                    b.Property<bool?>("has_housing");

                    b.Property<bool?>("has_ip_presence");

                    b.Property<bool?>("has_irrigation");

                    b.Property<bool?>("has_market");

                    b.Property<bool?>("has_miniport");

                    b.Property<bool?>("has_multipurpose");

                    b.Property<bool?>("has_secondary");

                    b.Property<bool?>("has_stores");

                    b.Property<bool?>("has_tanod");

                    b.Property<bool?>("has_telecom");

                    b.Property<bool?>("has_tribal");

                    b.Property<bool?>("has_waste");

                    b.Property<bool?>("has_water_supply_system");

                    b.Property<bool?>("health_address");

                    b.Property<string>("health_details");

                    b.Property<string>("health_number_0_5_reference");

                    b.Property<int?>("health_number_0_5_value");

                    b.Property<string>("health_remarks");

                    b.Property<double?>("hrs_totown");

                    b.Property<string>("incomeless_reference");

                    b.Property<int?>("incomeless_value");

                    b.Property<string>("ip_group_in_area");

                    b.Property<double?>("ira_amount");

                    b.Property<bool?>("is_armedconflict");

                    b.Property<bool?>("is_bounddispute");

                    b.Property<bool?>("is_brgyaffected");

                    b.Property<bool?>("is_coastal");

                    b.Property<bool?>("is_crime");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_genderviolence");

                    b.Property<bool?>("is_hilly");

                    b.Property<bool?>("is_island");

                    b.Property<bool?>("is_isolated");

                    b.Property<bool?>("is_lowland");

                    b.Property<bool?>("is_poblacion");

                    b.Property<bool?>("is_poldispute");

                    b.Property<bool?>("is_rido_war");

                    b.Property<bool?>("is_transaccess");

                    b.Property<bool?>("is_upland");

                    b.Property<double?>("km_frmtown");

                    b.Property<string>("laborforce_reference");

                    b.Property<int?>("laborforce_value");

                    b.Property<bool?>("landownership_addressed");

                    b.Property<string>("landownership_details");

                    b.Property<string>("landownership_remarks");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("lessthan_3_meals_reference");

                    b.Property<int?>("lessthan_3_meals_value");

                    b.Property<bool?>("literacy_addressed");

                    b.Property<string>("literacy_details");

                    b.Property<string>("literacy_remarks");

                    b.Property<string>("makeshift_reference");

                    b.Property<int?>("makeshift_value");

                    b.Property<int?>("male_hdblwfood");

                    b.Property<int?>("male_hdfoodshrt");

                    b.Property<int?>("male_hdmkshhouse");

                    b.Property<int?>("male_hdnotoilet");

                    b.Property<int?>("male_hdpoverty");

                    b.Property<int?>("male_hdsfwater");

                    b.Property<int?>("male_hdsquatters");

                    b.Property<string>("malnourished_0_5reference");

                    b.Property<int?>("malnourished_0_5value");

                    b.Property<double?>("mins_totown");

                    b.Property<double?>("nearest_bank");

                    b.Property<double?>("nearest_barangay_hall");

                    b.Property<double?>("nearest_cap_agri");

                    b.Property<double?>("nearest_cap_health");

                    b.Property<double?>("nearest_cap_org_dev");

                    b.Property<double?>("nearest_cap_others");

                    b.Property<double?>("nearest_cementery");

                    b.Property<double?>("nearest_college");

                    b.Property<double?>("nearest_credit");

                    b.Property<double?>("nearest_daycare");

                    b.Property<double?>("nearest_drainage");

                    b.Property<double?>("nearest_electricity");

                    b.Property<double?>("nearest_elementary");

                    b.Property<double?>("nearest_emergency_service");

                    b.Property<double?>("nearest_evac_center");

                    b.Property<double?>("nearest_harvest");

                    b.Property<double?>("nearest_health");

                    b.Property<double?>("nearest_hospital");

                    b.Property<double?>("nearest_housing");

                    b.Property<double?>("nearest_irrigation");

                    b.Property<double?>("nearest_market");

                    b.Property<double?>("nearest_miniport");

                    b.Property<double?>("nearest_multipurpose");

                    b.Property<double?>("nearest_secondary");

                    b.Property<double?>("nearest_stores");

                    b.Property<double?>("nearest_tanod");

                    b.Property<double?>("nearest_telecom");

                    b.Property<double?>("nearest_tribal");

                    b.Property<double?>("nearest_waste");

                    b.Property<double?>("nearest_water_supply_system");

                    b.Property<int?>("no_diedpreg");

                    b.Property<int?>("no_drmm_activities");

                    b.Property<int?>("no_families");

                    b.Property<int?>("no_fdied5below");

                    b.Property<int?>("no_female");

                    b.Property<int?>("no_female0_5");

                    b.Property<int?>("no_female13_16");

                    b.Property<int?>("no_female17_59");

                    b.Property<int?>("no_female60up");

                    b.Property<int?>("no_female6_12");

                    b.Property<int?>("no_femcrimev");

                    b.Property<int?>("no_femnowork");

                    b.Property<int?>("no_fkidmaln");

                    b.Property<int?>("no_fkidnosch13_16");

                    b.Property<int?>("no_fkidnosch6_12");

                    b.Property<int?>("no_fmheadedhh");

                    b.Property<int?>("no_gad_activities");

                    b.Property<int?>("no_households");

                    b.Property<int?>("no_indisplaced");

                    b.Property<int?>("no_ipfamily");

                    b.Property<int?>("no_iphouseholds");

                    b.Property<int?>("no_labor_female");

                    b.Property<int?>("no_labor_male");

                    b.Property<int?>("no_male");

                    b.Property<int?>("no_male0_5");

                    b.Property<int?>("no_male13_16");

                    b.Property<int?>("no_male17_59");

                    b.Property<int?>("no_male60up");

                    b.Property<int?>("no_male6_12");

                    b.Property<int?>("no_malecrimev");

                    b.Property<int?>("no_malenowork");

                    b.Property<int?>("no_mdied5below");

                    b.Property<int?>("no_mkidmaln");

                    b.Property<int?>("no_mkidnosch13_16");

                    b.Property<int?>("no_mkidnosch6_12");

                    b.Property<int?>("no_pantawid_family");

                    b.Property<int?>("no_pantawid_household");

                    b.Property<int?>("no_sitios");

                    b.Property<int?>("no_slp_family");

                    b.Property<int?>("no_slp_household");

                    b.Property<int?>("no_voting_female");

                    b.Property<int?>("no_voting_male");

                    b.Property<int?>("no_withdisability");

                    b.Property<string>("old_id");

                    b.Property<string>("other_source");

                    b.Property<bool?>("others_addressed");

                    b.Property<double?>("others_amount");

                    b.Property<string>("others_details");

                    b.Property<string>("others_remarks");

                    b.Property<bool?>("peace_addressed");

                    b.Property<string>("peace_details");

                    b.Property<string>("peace_remarks");

                    b.Property<bool?>("powersupply_addressed");

                    b.Property<string>("powersupply_details");

                    b.Property<string>("powersupply_remarks");

                    b.Property<string>("pregnant_died_reference");

                    b.Property<int?>("pregnant_died_value");

                    b.Property<string>("pregnant_total_reference");

                    b.Property<int?>("pregnant_total_value");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<string>("safewater_reference");

                    b.Property<int?>("safewater_value");

                    b.Property<string>("sanity_reference");

                    b.Property<int?>("sanity_value");

                    b.Property<string>("socio_econ_activity_1");

                    b.Property<string>("socio_econ_activity_2");

                    b.Property<string>("socio_econ_activity_3");

                    b.Property<double?>("socio_econ_amount_1");

                    b.Property<double?>("socio_econ_amount_2");

                    b.Property<double?>("socio_econ_amount_3");

                    b.Property<int?>("socio_econ_hhs_1");

                    b.Property<int?>("socio_econ_hhs_2");

                    b.Property<int?>("socio_econ_hhs_3");

                    b.Property<string>("source_5dead");

                    b.Property<string>("source_blwfood");

                    b.Property<string>("source_crimevic");

                    b.Property<string>("source_foodshort");

                    b.Property<string>("source_kidmaln");

                    b.Property<string>("source_mkshouse");

                    b.Property<string>("source_notoilet");

                    b.Property<string>("source_nowork");

                    b.Property<string>("source_poverty");

                    b.Property<string>("source_safewater");

                    b.Property<string>("source_squatters");

                    b.Property<string>("sourse_diedpreg");

                    b.Property<string>("squatting_reference");

                    b.Property<int?>("squatting_value");

                    b.Property<string>("srce_kidnosch13_16");

                    b.Property<string>("srce_kidnosch6_12");

                    b.Property<string>("threshold_reference");

                    b.Property<int?>("threshold_value");

                    b.Property<string>("tot_lessthan_3_meals_ref");

                    b.Property<string>("tot_malnourished_0_5_ref");

                    b.Property<int?>("total_malnourished_0_5value");

                    b.Property<int?>("totalchildren_6_12_elem_value");

                    b.Property<string>("totalincomeless_reference");

                    b.Property<int?>("totalincomeless_value");

                    b.Property<string>("totallaborforce_reference");

                    b.Property<int?>("totallaborforce_value");

                    b.Property<int?>("totallessthan_3_meals_value");

                    b.Property<string>("totalmakeshift_reference");

                    b.Property<int?>("totalmakeshift_value");

                    b.Property<string>("totalsafewater_reference");

                    b.Property<int?>("totalsafewater_value");

                    b.Property<string>("totalsanity_reference");

                    b.Property<int?>("totalsanity_value");

                    b.Property<string>("totalsquatting_reference");

                    b.Property<int?>("totalsquatting_value");

                    b.Property<string>("totalthreshold_reference");

                    b.Property<int?>("totalthreshold_value");

                    b.Property<string>("totalvictimized_reference");

                    b.Property<int?>("totalvictimized_value");

                    b.Property<string>("totchild_13_16_secondary_ref");

                    b.Property<int?>("totchild_13_16_secondary_val");

                    b.Property<string>("totchild_6_12_elem_ref");

                    b.Property<int?>("transpo_bank");

                    b.Property<int?>("transpo_barangay_hall");

                    b.Property<int?>("transpo_cap_agri");

                    b.Property<int?>("transpo_cap_health");

                    b.Property<int?>("transpo_cap_org_dev");

                    b.Property<int?>("transpo_cap_others");

                    b.Property<int?>("transpo_cementery");

                    b.Property<int?>("transpo_college");

                    b.Property<int?>("transpo_credit");

                    b.Property<int?>("transpo_daycare");

                    b.Property<int?>("transpo_drainage");

                    b.Property<int?>("transpo_electricity");

                    b.Property<int?>("transpo_elementary");

                    b.Property<int?>("transpo_emergency_service");

                    b.Property<int?>("transpo_evac_center");

                    b.Property<int?>("transpo_harvest");

                    b.Property<int?>("transpo_health");

                    b.Property<int?>("transpo_hospital");

                    b.Property<int?>("transpo_housing");

                    b.Property<int?>("transpo_irrigation");

                    b.Property<int?>("transpo_market");

                    b.Property<int?>("transpo_miniport");

                    b.Property<int?>("transpo_multipurpose");

                    b.Property<int?>("transpo_secondary");

                    b.Property<int?>("transpo_stores");

                    b.Property<int?>("transpo_tanod");

                    b.Property<int?>("transpo_telecom");

                    b.Property<int?>("transpo_tribal");

                    b.Property<int?>("transpo_waste");

                    b.Property<int?>("transpo_water_supply_system");

                    b.Property<string>("victimized_reference");

                    b.Property<int?>("victimized_value");

                    b.Property<bool?>("water_address");

                    b.Property<string>("water_details");

                    b.Property<string>("water_remarks");

                    b.Property<int?>("year_source");

                    b.HasKey("brgy_profile_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("brgy_profile");
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_profile_ip", b =>
                {
                    b.Property<Guid>("brgy_profile_ip_id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("brgy_profile_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("ip_group_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("location");

                    b.Property<int>("no_families");

                    b.Property<int>("no_female");

                    b.Property<int>("no_household");

                    b.Property<int>("no_male");

                    b.Property<string>("old_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.HasKey("brgy_profile_ip_id");

                    b.HasIndex("brgy_profile_id");

                    b.HasIndex("ip_group_id");

                    b.ToTable("brgy_profile_ip");
                });

            modelBuilder.Entity("DeskApp.DataLayer.ceac_list", b =>
                {
                    b.Property<Guid>("ceac_list_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int?>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int>("fund_source_id");

                    b.Property<int?>("implementation_status_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("old_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.HasKey("ceac_list_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("push_status_id");

                    b.HasIndex("region_code");

                    b.ToTable("ceac_list");
                });

            modelBuilder.Entity("DeskApp.DataLayer.ceac_tracking", b =>
                {
                    b.Property<Guid>("ceac_tracking_id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("actual_end");

                    b.Property<DateTime?>("actual_start");

                    b.Property<int>("approval_id");

                    b.Property<int?>("brgy_code");

                    b.Property<DateTime?>("catch_end");

                    b.Property<DateTime?>("catch_start");

                    b.Property<int>("ceac_activity_id");

                    b.Property<Guid>("ceac_list_id");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int>("fund_source_id");

                    b.Property<int>("implementation_status_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int>("lgu_level_id");

                    b.Property<string>("old_id");

                    b.Property<DateTime?>("plan_end");

                    b.Property<DateTime?>("plan_start");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<Guid?>("reference_id");

                    b.Property<int>("region_code");

                    b.Property<int>("training_category_id");

                    b.HasKey("ceac_tracking_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("ceac_list_id");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("implementation_status_id");

                    b.HasIndex("lgu_level_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("push_status_id");

                    b.HasIndex("region_code");

                    b.HasIndex("training_category_id");

                    b.ToTable("ceac_tracking");
                });

            modelBuilder.Entity("DeskApp.DataLayer.community_training", b =>
                {
                    b.Property<Guid>("community_training_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int?>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<DateTime?>("date_conducted");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("duration");

                    b.Property<DateTime?>("end_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int>("fund_source_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool>("is_draft");

                    b.Property<bool?>("is_sector_academe");

                    b.Property<bool?>("is_sector_business");

                    b.Property<bool?>("is_sector_farmer");

                    b.Property<bool?>("is_sector_fisherfolks");

                    b.Property<bool?>("is_sector_government");

                    b.Property<bool?>("is_sector_ip");

                    b.Property<bool?>("is_sector_ngo");

                    b.Property<bool?>("is_sector_po");

                    b.Property<bool?>("is_sector_pwd");

                    b.Property<bool?>("is_sector_religios");

                    b.Property<bool?>("is_sector_senior");

                    b.Property<bool?>("is_sector_women");

                    b.Property<bool?>("is_sector_youth");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("last_sync_source_id");

                    b.Property<int>("lgu_level_id");

                    b.Property<int?>("no_atn_female");

                    b.Property<int?>("no_atn_male");

                    b.Property<int?>("no_atn_pantawid");

                    b.Property<int?>("no_atn_slp");

                    b.Property<int?>("no_brgy_rep");

                    b.Property<int?>("no_ip_female");

                    b.Property<int?>("no_ip_male");

                    b.Property<string>("old_id");

                    b.Property<DateTime?>("plan_date_end");

                    b.Property<DateTime?>("plan_date_start");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<string>("reported_by");

                    b.Property<bool?>("requirement_1");

                    b.Property<bool?>("requirement_2");

                    b.Property<bool?>("requirement_3");

                    b.Property<bool?>("requirement_4");

                    b.Property<bool?>("requirement_5");

                    b.Property<bool?>("requirement_6");

                    b.Property<string>("speaker");

                    b.Property<DateTime?>("start_date");

                    b.Property<int>("training_category_id");

                    b.Property<string>("training_title");

                    b.Property<string>("venue");

                    b.HasKey("community_training_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("lgu_level_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("push_status_id");

                    b.HasIndex("region_code");

                    b.HasIndex("training_category_id");

                    b.ToTable("community_training");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.attached_document", b =>
                {
                    b.Property<Guid>("attached_document_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int?>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("count");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int?>("enrollment_id");

                    b.Property<int?>("fund_source_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int>("mov_list_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<Guid>("record_id");

                    b.Property<int>("region_code");

                    b.HasKey("attached_document_id");

                    b.HasIndex("mov_list_id");

                    b.ToTable("attached_document");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.community_organization", b =>
                {
                    b.Property<Guid>("community_organization_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int?>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int>("fund_source_id");

                    b.Property<bool?>("is_active");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_formal");

                    b.Property<bool?>("is_lgu_accredited");

                    b.Property<bool?>("is_onm");

                    b.Property<bool?>("is_sector_academe");

                    b.Property<bool?>("is_sector_business");

                    b.Property<bool?>("is_sector_farmer");

                    b.Property<bool?>("is_sector_fisherfolks");

                    b.Property<bool?>("is_sector_government");

                    b.Property<bool?>("is_sector_ip");

                    b.Property<bool?>("is_sector_ngo");

                    b.Property<bool?>("is_sector_po");

                    b.Property<bool?>("is_sector_pwd");

                    b.Property<bool?>("is_sector_religios");

                    b.Property<bool?>("is_sector_senior");

                    b.Property<bool?>("is_sector_women");

                    b.Property<bool?>("is_sector_youth");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int>("lgu_level_id");

                    b.Property<string>("list_activities");

                    b.Property<string>("list_advocacy");

                    b.Property<string>("list_area_operation");

                    b.Property<string>("name");

                    b.Property<int?>("no_female");

                    b.Property<int?>("no_ip_female");

                    b.Property<int?>("no_ip_male");

                    b.Property<int?>("no_male");

                    b.Property<string>("old_id");

                    b.Property<int>("organization_type_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<int?>("years_operating");

                    b.HasKey("community_organization_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("lgu_level_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("community_organization");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.diaster_coverage", b =>
                {
                    b.Property<int>("disaster_coverage_id");

                    b.Property<bool?>("affected");

                    b.Property<int>("city_code");

                    b.Property<Guid>("disaster_id");

                    b.Property<int>("prov_code");

                    b.Property<int>("region_code");

                    b.HasKey("disaster_coverage_id");

                    b.HasIndex("city_code");

                    b.HasIndex("disaster_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("diaster_coverage");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.disaster", b =>
                {
                    b.Property<Guid>("disaster_id");

                    b.Property<DateTime?>("date_end");

                    b.Property<DateTime?>("date_start");

                    b.Property<string>("description");

                    b.Property<int>("disaster_type_id");

                    b.Property<string>("name");

                    b.HasKey("disaster_id");

                    b.HasIndex("disaster_type_id");

                    b.ToTable("disaster");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.file_attachment", b =>
                {
                    b.Property<Guid>("file_attachment_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("count");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int>("means_of_verification_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<Guid>("record_id");

                    b.HasKey("file_attachment_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("means_of_verification_id");

                    b.ToTable("file_attachment");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.lib_condition", b =>
                {
                    b.Property<int>("condition_id");

                    b.Property<string>("name");

                    b.HasKey("condition_id");

                    b.ToTable("lib_condition");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.lib_disaster_type", b =>
                {
                    b.Property<int>("disaster_type_id");

                    b.Property<string>("name");

                    b.HasKey("disaster_type_id");

                    b.ToTable("lib_disaster_type");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.means_of_verification", b =>
                {
                    b.Property<int>("means_of_verification_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("max");

                    b.Property<string>("name");

                    b.Property<int>("table_name_id");

                    b.HasKey("means_of_verification_id");

                    b.HasIndex("table_name_id");

                    b.ToTable("means_of_verification");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.mov_list", b =>
                {
                    b.Property<int>("mov_list_id");

                    b.Property<int>("max");

                    b.Property<string>("name");

                    b.Property<int>("table_name_id");

                    b.HasKey("mov_list_id");

                    b.ToTable("mov_list");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.report_list", b =>
                {
                    b.Property<int>("report_list_id");

                    b.Property<string>("description");

                    b.Property<bool?>("is_deleted");

                    b.Property<string>("name");

                    b.Property<int>("table_name_id");

                    b.Property<string>("url");

                    b.HasKey("report_list_id");

                    b.ToTable("report_list");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.sub_project_post_disaster", b =>
                {
                    b.Property<Guid>("sub_project_post_disaster_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("affected");

                    b.Property<int>("condition_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<Guid>("disaster_id");

                    b.Property<double?>("estimated_damage");

                    b.Property<double?>("estimated_repair");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("no_served");

                    b.Property<string>("remarks");

                    b.Property<int>("sub_project_id");

                    b.Property<bool?>("utilized");

                    b.HasKey("sub_project_post_disaster_id");

                    b.HasIndex("condition_id");

                    b.HasIndex("disaster_id");

                    b.ToTable("sub_project_post_disaster");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.sub_project_pre_disaster", b =>
                {
                    b.Property<Guid>("sub_project_pre_disaster_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("can_be_used_as_evac_site");

                    b.Property<int>("capacity");

                    b.Property<int>("condition_id");

                    b.Property<string>("contact_no");

                    b.Property<string>("contact_person");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<DateTime>("date_assessed");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int>("sub_project_id");

                    b.Property<bool?>("with_fist_aid_kit");

                    b.HasKey("sub_project_pre_disaster_id");

                    b.HasIndex("condition_id");

                    b.ToTable("sub_project_pre_disaster");
                });

            modelBuilder.Entity("DeskApp.DataLayer.erfr_sub_project", b =>
                {
                    b.Property<int>("SPID");

                    b.Property<string>("Barangay");

                    b.Property<string>("Municipality");

                    b.Property<string>("Province");

                    b.Property<string>("Region");

                    b.Property<decimal?>("Year");

                    b.Property<decimal?>("community_direct_cost");

                    b.Property<decimal?>("community_indirect_cost");

                    b.Property<decimal?>("cycle");

                    b.Property<DateTime?>("date_of_mibf");

                    b.Property<decimal?>("first_tranch_amount");

                    b.Property<DateTime?>("first_tranch_date_required");

                    b.Property<string>("fund_source");

                    b.Property<decimal?>("grant_amount_contingency_cost");

                    b.Property<decimal?>("grant_amount_direct_cost");

                    b.Property<decimal?>("grant_amount_indirect_cost");

                    b.Property<string>("grouping");

                    b.Property<decimal?>("lcc_blgu_direct_cost");

                    b.Property<decimal?>("lcc_blgu_indirect_cost");

                    b.Property<decimal?>("mlgu_direct_cost");

                    b.Property<decimal?>("mlgu_indirect_cost");

                    b.Property<string>("nscb_code");

                    b.Property<decimal?>("plgu_others_direct_cost");

                    b.Property<decimal?>("plgu_others_indirect_cost");

                    b.Property<decimal?>("second_tranch_amount");

                    b.Property<DateTime?>("second_tranch_date_required");

                    b.Property<string>("status");

                    b.Property<decimal?>("third_tranch_amount");

                    b.Property<DateTime?>("third_tranch_date_required");

                    b.Property<string>("title");

                    b.Property<decimal?>("total_grant");

                    b.Property<decimal?>("total_lcc");

                    b.Property<decimal?>("total_lcc_cash_direct_cost");

                    b.Property<decimal?>("total_lcc_cash_indirect_cost");

                    b.Property<decimal?>("total_lcc_in_kind_direct_cost");

                    b.Property<decimal?>("total_lcc_in_kind_indirect_cost");

                    b.HasKey("SPID");

                    b.ToTable("erfr_sub_project");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.dof_blgf_financial_data", b =>
                {
                    b.Property<int>("dof_blgf_financial_data_record_id");

                    b.Property<int>("city_code");

                    b.Property<float?>("expenditures_economic_services");

                    b.Property<float?>("expenditures_educ_culture_etc");

                    b.Property<float?>("expenditures_gen_public_services");

                    b.Property<float?>("expenditures_health_services");

                    b.Property<float?>("expenditures_housing_comm_devt");

                    b.Property<float?>("expenditures_labor_and_employment");

                    b.Property<float?>("expenditures_other_purposes");

                    b.Property<float?>("expenditures_social_welfare_services");

                    b.Property<float?>("extraordinary_receipts");

                    b.Property<float?>("inter_local_transfers");

                    b.Property<float?>("ira_share");

                    b.Property<float?>("locally_shared_revenues");

                    b.Property<float?>("other_revenues_total");

                    b.Property<float?>("other_shares_natl_tax");

                    b.Property<int>("prov_code");

                    b.Property<int>("psgc_code");

                    b.Property<int>("region_code");

                    b.Property<float?>("total_lgu_income");

                    b.Property<int>("year_id");

                    b.HasKey("dof_blgf_financial_data_record_id");

                    b.HasIndex("city_code");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("dof_blgf_financial_data");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.lgpms_data", b =>
                {
                    b.Property<int>("lgpms_data_id");

                    b.Property<float?>("administrative_governance_2009");

                    b.Property<float?>("administrative_governance_2010");

                    b.Property<float?>("administrative_governance_2011");

                    b.Property<float?>("administrative_governance_2012");

                    b.Property<int>("city_code");

                    b.Property<float?>("economic_governance_2009");

                    b.Property<float?>("economic_governance_2010");

                    b.Property<float?>("economic_governance_2011");

                    b.Property<float?>("economic_governance_2012");

                    b.Property<float?>("environmental_governance_2009");

                    b.Property<float?>("environmental_governance_2010");

                    b.Property<float?>("environmental_governance_2011");

                    b.Property<float?>("environmental_governance_2012");

                    b.Property<float?>("overall_performance_index_2009");

                    b.Property<float?>("overall_performance_index_2010");

                    b.Property<float?>("overall_performance_index_2011");

                    b.Property<float?>("overall_performance_index_2012");

                    b.Property<int>("prov_code");

                    b.Property<int>("psgc_code");

                    b.Property<int>("region_code");

                    b.Property<float?>("social_governance_2009");

                    b.Property<float?>("social_governance_2010");

                    b.Property<float?>("social_governance_2011");

                    b.Property<float?>("social_governance_2012");

                    b.Property<float?>("valuing_fundamentals_of_good_gov_2009");

                    b.Property<float?>("valuing_fundamentals_of_good_gov_2010");

                    b.Property<float?>("valuing_fundamentals_of_good_gov_2011");

                    b.Property<float?>("valuing_fundamentals_of_good_gov_2012");

                    b.HasKey("lgpms_data_id");

                    b.HasIndex("city_code");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("lgpms_data");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.mlgu_financial_data", b =>
                {
                    b.Property<Guid>("mlgu_financial_data_record_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("administrative_governance_2009");

                    b.Property<int?>("administrative_governance_2010");

                    b.Property<int?>("administrative_governance_2011");

                    b.Property<int?>("administrative_governance_2012");

                    b.Property<int>("approval_id");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int?>("dof_blgf_financial_data_record_id");

                    b.Property<int?>("economic_governance_2009");

                    b.Property<int?>("economic_governance_2010");

                    b.Property<int?>("economic_governance_2011");

                    b.Property<int?>("economic_governance_2012");

                    b.Property<int?>("environmental_governance_2009");

                    b.Property<int?>("environmental_governance_2010");

                    b.Property<int?>("environmental_governance_2011");

                    b.Property<int?>("environmental_governance_2012");

                    b.Property<int?>("expenditures_economic_services");

                    b.Property<string>("expenditures_economic_services_source");

                    b.Property<int?>("expenditures_educ_culture_etc");

                    b.Property<string>("expenditures_educ_culture_etc_source");

                    b.Property<int?>("expenditures_gen_public_services");

                    b.Property<string>("expenditures_gen_public_services_source");

                    b.Property<int?>("expenditures_health_services");

                    b.Property<string>("expenditures_health_services_source");

                    b.Property<int?>("expenditures_housing_comm_devt");

                    b.Property<string>("expenditures_housing_comm_devt_source");

                    b.Property<int?>("expenditures_labor_and_employment");

                    b.Property<string>("expenditures_labor_and_employment_source");

                    b.Property<int?>("expenditures_other_purposes");

                    b.Property<string>("expenditures_other_purposes_source");

                    b.Property<int?>("expenditures_social_welfare_services");

                    b.Property<string>("expenditures_social_welfare_services_source");

                    b.Property<int?>("extraordinary_receipts");

                    b.Property<string>("extraordinary_receipts_source");

                    b.Property<int?>("inter_local_transfers");

                    b.Property<string>("inter_local_transfers_source");

                    b.Property<int?>("ira_share");

                    b.Property<string>("ira_share_source");

                    b.Property<bool>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("lgpms_data_id");

                    b.Property<int?>("locally_shared_revenues");

                    b.Property<int?>("locally_sourced_revenues");

                    b.Property<string>("locally_sourced_revenues_source");

                    b.Property<int?>("other_revenues_total");

                    b.Property<string>("other_revenues_total_source");

                    b.Property<int?>("other_shares_natl_tax");

                    b.Property<string>("other_shares_natl_tax_source");

                    b.Property<int?>("overall_performance_index_2009");

                    b.Property<int?>("overall_performance_index_2010");

                    b.Property<int?>("overall_performance_index_2011");

                    b.Property<int?>("overall_performance_index_2012");

                    b.Property<int>("prov_code");

                    b.Property<int>("psgc_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<int?>("social_governance_2009");

                    b.Property<int?>("social_governance_2010");

                    b.Property<int?>("social_governance_2011");

                    b.Property<int?>("social_governance_2012");

                    b.Property<DateTime?>("talakayan_date_end");

                    b.Property<DateTime?>("talakayan_date_start");

                    b.Property<int>("talakayan_yr_id");

                    b.Property<int?>("total_lgu_income");

                    b.Property<string>("total_lgu_income_source");

                    b.Property<int?>("valuing_fundamentals_of_good_gov_2009");

                    b.Property<int?>("valuing_fundamentals_of_good_gov_2010");

                    b.Property<int?>("valuing_fundamentals_of_good_gov_2011");

                    b.Property<int?>("valuing_fundamentals_of_good_gov_2012");

                    b.Property<int>("year_id");

                    b.HasKey("mlgu_financial_data_record_id");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("dof_blgf_financial_data_record_id");

                    b.HasIndex("lgpms_data_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.HasIndex("talakayan_yr_id");

                    b.ToTable("mlgu_financial_data");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.muni_financial_profile", b =>
                {
                    b.Property<Guid>("muni_financial_profile_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<decimal?>("mlgu2009_ex_economic");

                    b.Property<string>("mlgu2009_ex_economic_source");

                    b.Property<decimal?>("mlgu2009_ex_educ");

                    b.Property<string>("mlgu2009_ex_educ_source");

                    b.Property<decimal?>("mlgu2009_ex_gen_public");

                    b.Property<string>("mlgu2009_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2009_ex_health");

                    b.Property<string>("mlgu2009_ex_health_source");

                    b.Property<decimal?>("mlgu2009_ex_housing");

                    b.Property<string>("mlgu2009_ex_housing_source");

                    b.Property<decimal?>("mlgu2009_ex_labor");

                    b.Property<string>("mlgu2009_ex_labor_source");

                    b.Property<decimal?>("mlgu2009_ex_other_purposes");

                    b.Property<string>("mlgu2009_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2009_ex_social_welfare");

                    b.Property<string>("mlgu2009_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2009_rev_devt_fund");

                    b.Property<decimal?>("mlgu2009_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2009_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2009_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2009_rev_ira_share");

                    b.Property<string>("mlgu2009_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2009_rev_locally_sourced");

                    b.Property<string>("mlgu2009_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2009_rev_other_revenues_total");

                    b.Property<string>("mlgu2009_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2009_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2009_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2009_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2009_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2010_ex_economic");

                    b.Property<string>("mlgu2010_ex_economic_source");

                    b.Property<decimal?>("mlgu2010_ex_educ");

                    b.Property<string>("mlgu2010_ex_educ_source");

                    b.Property<decimal?>("mlgu2010_ex_gen_public");

                    b.Property<string>("mlgu2010_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2010_ex_health");

                    b.Property<string>("mlgu2010_ex_health_source");

                    b.Property<decimal?>("mlgu2010_ex_housing");

                    b.Property<string>("mlgu2010_ex_housing_source");

                    b.Property<decimal?>("mlgu2010_ex_labor");

                    b.Property<string>("mlgu2010_ex_labor_source");

                    b.Property<decimal?>("mlgu2010_ex_other_purposes");

                    b.Property<string>("mlgu2010_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2010_ex_social_welfare");

                    b.Property<string>("mlgu2010_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2010_rev_devt_fund");

                    b.Property<decimal?>("mlgu2010_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2010_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2010_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2010_rev_ira_share");

                    b.Property<string>("mlgu2010_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2010_rev_locally_sourced");

                    b.Property<string>("mlgu2010_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2010_rev_other_revenues_total");

                    b.Property<string>("mlgu2010_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2010_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2010_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2010_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2010_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2011_ex_economic");

                    b.Property<string>("mlgu2011_ex_economic_source");

                    b.Property<decimal?>("mlgu2011_ex_educ");

                    b.Property<string>("mlgu2011_ex_educ_source");

                    b.Property<decimal?>("mlgu2011_ex_gen_public");

                    b.Property<string>("mlgu2011_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2011_ex_health");

                    b.Property<string>("mlgu2011_ex_health_source");

                    b.Property<decimal?>("mlgu2011_ex_housing");

                    b.Property<string>("mlgu2011_ex_housing_source");

                    b.Property<decimal?>("mlgu2011_ex_labor");

                    b.Property<string>("mlgu2011_ex_labor_source");

                    b.Property<decimal?>("mlgu2011_ex_other_purposes");

                    b.Property<string>("mlgu2011_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2011_ex_social_welfare");

                    b.Property<string>("mlgu2011_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2011_rev_devt_fund");

                    b.Property<decimal?>("mlgu2011_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2011_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2011_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2011_rev_ira_share");

                    b.Property<string>("mlgu2011_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2011_rev_locally_sourced");

                    b.Property<string>("mlgu2011_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2011_rev_other_revenues_total");

                    b.Property<string>("mlgu2011_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2011_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2011_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2011_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2011_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2012_ex_economic");

                    b.Property<string>("mlgu2012_ex_economic_source");

                    b.Property<decimal?>("mlgu2012_ex_educ");

                    b.Property<string>("mlgu2012_ex_educ_source");

                    b.Property<decimal?>("mlgu2012_ex_gen_public");

                    b.Property<string>("mlgu2012_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2012_ex_health");

                    b.Property<string>("mlgu2012_ex_health_source");

                    b.Property<decimal?>("mlgu2012_ex_housing");

                    b.Property<string>("mlgu2012_ex_housing_source");

                    b.Property<decimal?>("mlgu2012_ex_labor");

                    b.Property<string>("mlgu2012_ex_labor_source");

                    b.Property<decimal?>("mlgu2012_ex_other_purposes");

                    b.Property<string>("mlgu2012_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2012_ex_social_welfare");

                    b.Property<string>("mlgu2012_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2012_rev_devt_fund");

                    b.Property<decimal?>("mlgu2012_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2012_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2012_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2012_rev_ira_share");

                    b.Property<string>("mlgu2012_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2012_rev_locally_sourced");

                    b.Property<string>("mlgu2012_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2012_rev_other_revenues_total");

                    b.Property<string>("mlgu2012_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2012_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2012_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2012_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2012_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2013_ex_economic");

                    b.Property<string>("mlgu2013_ex_economic_source");

                    b.Property<decimal?>("mlgu2013_ex_educ");

                    b.Property<string>("mlgu2013_ex_educ_source");

                    b.Property<decimal?>("mlgu2013_ex_gen_public");

                    b.Property<string>("mlgu2013_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2013_ex_health");

                    b.Property<string>("mlgu2013_ex_health_source");

                    b.Property<decimal?>("mlgu2013_ex_housing");

                    b.Property<string>("mlgu2013_ex_housing_source");

                    b.Property<decimal?>("mlgu2013_ex_labor");

                    b.Property<string>("mlgu2013_ex_labor_source");

                    b.Property<decimal?>("mlgu2013_ex_other_purposes");

                    b.Property<string>("mlgu2013_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2013_ex_social_welfare");

                    b.Property<string>("mlgu2013_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2013_rev_devt_fund");

                    b.Property<decimal?>("mlgu2013_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2013_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2013_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2013_rev_ira_share");

                    b.Property<string>("mlgu2013_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2013_rev_locally_sourced");

                    b.Property<string>("mlgu2013_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2013_rev_other_revenues_total");

                    b.Property<string>("mlgu2013_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2013_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2013_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2013_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2013_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2014_ex_economic");

                    b.Property<string>("mlgu2014_ex_economic_source");

                    b.Property<decimal?>("mlgu2014_ex_educ");

                    b.Property<string>("mlgu2014_ex_educ_source");

                    b.Property<decimal?>("mlgu2014_ex_gen_public");

                    b.Property<string>("mlgu2014_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2014_ex_health");

                    b.Property<string>("mlgu2014_ex_health_source");

                    b.Property<decimal?>("mlgu2014_ex_housing");

                    b.Property<string>("mlgu2014_ex_housing_source");

                    b.Property<decimal?>("mlgu2014_ex_labor");

                    b.Property<string>("mlgu2014_ex_labor_source");

                    b.Property<decimal?>("mlgu2014_ex_other_purposes");

                    b.Property<string>("mlgu2014_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2014_ex_social_welfare");

                    b.Property<string>("mlgu2014_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2014_rev_devt_fund");

                    b.Property<decimal?>("mlgu2014_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2014_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2014_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2014_rev_ira_share");

                    b.Property<string>("mlgu2014_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2014_rev_locally_sourced");

                    b.Property<string>("mlgu2014_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2014_rev_other_revenues_total");

                    b.Property<string>("mlgu2014_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2014_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2014_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2014_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2014_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2015_ex_economic");

                    b.Property<string>("mlgu2015_ex_economic_source");

                    b.Property<decimal?>("mlgu2015_ex_educ");

                    b.Property<string>("mlgu2015_ex_educ_source");

                    b.Property<decimal?>("mlgu2015_ex_gen_public");

                    b.Property<string>("mlgu2015_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2015_ex_health");

                    b.Property<string>("mlgu2015_ex_health_source");

                    b.Property<decimal?>("mlgu2015_ex_housing");

                    b.Property<string>("mlgu2015_ex_housing_source");

                    b.Property<decimal?>("mlgu2015_ex_labor");

                    b.Property<string>("mlgu2015_ex_labor_source");

                    b.Property<decimal?>("mlgu2015_ex_other_purposes");

                    b.Property<string>("mlgu2015_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2015_ex_social_welfare");

                    b.Property<string>("mlgu2015_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2015_rev_devt_fund");

                    b.Property<decimal?>("mlgu2015_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2015_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2015_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2015_rev_ira_share");

                    b.Property<string>("mlgu2015_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2015_rev_locally_sourced");

                    b.Property<string>("mlgu2015_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2015_rev_other_revenues_total");

                    b.Property<string>("mlgu2015_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2015_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2015_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2015_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2015_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2016_ex_economic");

                    b.Property<string>("mlgu2016_ex_economic_source");

                    b.Property<decimal?>("mlgu2016_ex_educ");

                    b.Property<string>("mlgu2016_ex_educ_source");

                    b.Property<decimal?>("mlgu2016_ex_gen_public");

                    b.Property<string>("mlgu2016_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2016_ex_health");

                    b.Property<string>("mlgu2016_ex_health_source");

                    b.Property<decimal?>("mlgu2016_ex_housing");

                    b.Property<string>("mlgu2016_ex_housing_source");

                    b.Property<decimal?>("mlgu2016_ex_labor");

                    b.Property<string>("mlgu2016_ex_labor_source");

                    b.Property<decimal?>("mlgu2016_ex_other_purposes");

                    b.Property<string>("mlgu2016_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2016_ex_social_welfare");

                    b.Property<string>("mlgu2016_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2016_rev_devt_fund");

                    b.Property<decimal?>("mlgu2016_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2016_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2016_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2016_rev_ira_share");

                    b.Property<string>("mlgu2016_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2016_rev_locally_sourced");

                    b.Property<string>("mlgu2016_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2016_rev_other_revenues_total");

                    b.Property<string>("mlgu2016_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2016_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2016_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2016_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2016_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2017_ex_economic");

                    b.Property<string>("mlgu2017_ex_economic_source");

                    b.Property<decimal?>("mlgu2017_ex_educ");

                    b.Property<string>("mlgu2017_ex_educ_source");

                    b.Property<decimal?>("mlgu2017_ex_gen_public");

                    b.Property<string>("mlgu2017_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2017_ex_health");

                    b.Property<string>("mlgu2017_ex_health_source");

                    b.Property<decimal?>("mlgu2017_ex_housing");

                    b.Property<string>("mlgu2017_ex_housing_source");

                    b.Property<decimal?>("mlgu2017_ex_labor");

                    b.Property<string>("mlgu2017_ex_labor_source");

                    b.Property<decimal?>("mlgu2017_ex_other_purposes");

                    b.Property<string>("mlgu2017_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2017_ex_social_welfare");

                    b.Property<string>("mlgu2017_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2017_rev_devt_fund");

                    b.Property<decimal?>("mlgu2017_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2017_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2017_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2017_rev_ira_share");

                    b.Property<string>("mlgu2017_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2017_rev_locally_sourced");

                    b.Property<string>("mlgu2017_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2017_rev_other_revenues_total");

                    b.Property<string>("mlgu2017_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2017_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2017_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2017_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2017_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2018_ex_economic");

                    b.Property<string>("mlgu2018_ex_economic_source");

                    b.Property<decimal?>("mlgu2018_ex_educ");

                    b.Property<string>("mlgu2018_ex_educ_source");

                    b.Property<decimal?>("mlgu2018_ex_gen_public");

                    b.Property<string>("mlgu2018_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2018_ex_health");

                    b.Property<string>("mlgu2018_ex_health_source");

                    b.Property<decimal?>("mlgu2018_ex_housing");

                    b.Property<string>("mlgu2018_ex_housing_source");

                    b.Property<decimal?>("mlgu2018_ex_labor");

                    b.Property<string>("mlgu2018_ex_labor_source");

                    b.Property<decimal?>("mlgu2018_ex_other_purposes");

                    b.Property<string>("mlgu2018_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2018_ex_social_welfare");

                    b.Property<string>("mlgu2018_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2018_rev_devt_fund");

                    b.Property<decimal?>("mlgu2018_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2018_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2018_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2018_rev_ira_share");

                    b.Property<string>("mlgu2018_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2018_rev_locally_sourced");

                    b.Property<string>("mlgu2018_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2018_rev_other_revenues_total");

                    b.Property<string>("mlgu2018_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2018_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2018_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2018_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2018_rev_total_lgu_income_source");

                    b.Property<decimal?>("mlgu2019_ex_economic");

                    b.Property<string>("mlgu2019_ex_economic_source");

                    b.Property<decimal?>("mlgu2019_ex_educ");

                    b.Property<string>("mlgu2019_ex_educ_source");

                    b.Property<decimal?>("mlgu2019_ex_gen_public");

                    b.Property<string>("mlgu2019_ex_gen_public_source");

                    b.Property<decimal?>("mlgu2019_ex_health");

                    b.Property<string>("mlgu2019_ex_health_source");

                    b.Property<decimal?>("mlgu2019_ex_housing");

                    b.Property<string>("mlgu2019_ex_housing_source");

                    b.Property<decimal?>("mlgu2019_ex_labor");

                    b.Property<string>("mlgu2019_ex_labor_source");

                    b.Property<decimal?>("mlgu2019_ex_other_purposes");

                    b.Property<string>("mlgu2019_ex_other_purposes_source");

                    b.Property<decimal?>("mlgu2019_ex_social_welfare");

                    b.Property<string>("mlgu2019_ex_social_welfare_source");

                    b.Property<decimal?>("mlgu2019_rev_devt_fund");

                    b.Property<decimal?>("mlgu2019_rev_devt_fund_per_capita");

                    b.Property<string>("mlgu2019_rev_devt_fund_per_capita_source");

                    b.Property<string>("mlgu2019_rev_devt_fund_source");

                    b.Property<decimal?>("mlgu2019_rev_ira_share");

                    b.Property<string>("mlgu2019_rev_ira_share_source");

                    b.Property<decimal?>("mlgu2019_rev_locally_sourced");

                    b.Property<string>("mlgu2019_rev_locally_sourced_source");

                    b.Property<decimal?>("mlgu2019_rev_other_revenues_total");

                    b.Property<string>("mlgu2019_rev_other_revenues_total_source");

                    b.Property<decimal?>("mlgu2019_rev_total_lgu_income");

                    b.Property<decimal?>("mlgu2019_rev_total_lgu_income_per_capita");

                    b.Property<string>("mlgu2019_rev_total_lgu_income_per_capita_source");

                    b.Property<string>("mlgu2019_rev_total_lgu_income_source");

                    b.Property<int?>("population_2015");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<DateTime?>("talakayan_date_end");

                    b.Property<DateTime?>("talakayan_date_start");

                    b.Property<int>("talakayan_yr_id");

                    b.HasKey("muni_financial_profile_id");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("muni_financial_profile");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.perception_survey", b =>
                {
                    b.Property<Guid>("perception_survey_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("access_1");

                    b.Property<int?>("access_10");

                    b.Property<int?>("access_11");

                    b.Property<int?>("access_12");

                    b.Property<int?>("access_13");

                    b.Property<int?>("access_14");

                    b.Property<int?>("access_15");

                    b.Property<int?>("access_16");

                    b.Property<int?>("access_2");

                    b.Property<int?>("access_3");

                    b.Property<int?>("access_4");

                    b.Property<int?>("access_5");

                    b.Property<int?>("access_6");

                    b.Property<int?>("access_7");

                    b.Property<int?>("access_8");

                    b.Property<int?>("access_9");

                    b.Property<string>("activity_name");

                    b.Property<int?>("age");

                    b.Property<int>("approval_id");

                    b.Property<int>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int?>("disaster_1");

                    b.Property<int?>("disaster_2");

                    b.Property<int?>("disaster_3");

                    b.Property<int?>("disaster_4");

                    b.Property<int?>("disaster_5");

                    b.Property<int?>("disaster_6");

                    b.Property<int?>("disaster_7");

                    b.Property<int?>("disaster_8");

                    b.Property<int?>("disaster_9");

                    b.Property<bool>("is_deleted");

                    b.Property<bool?>("is_ip");

                    b.Property<bool?>("is_male");

                    b.Property<bool?>("is_pantawid");

                    b.Property<bool?>("is_slp");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("participation_1");

                    b.Property<int?>("participation_10");

                    b.Property<int?>("participation_11");

                    b.Property<int?>("participation_12");

                    b.Property<int?>("participation_2");

                    b.Property<int?>("participation_3");

                    b.Property<int?>("participation_4");

                    b.Property<int?>("participation_5");

                    b.Property<int?>("participation_6");

                    b.Property<int?>("participation_7");

                    b.Property<int?>("participation_8");

                    b.Property<int?>("participation_9");

                    b.Property<string>("person_name");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<DateTime?>("survey_date_from");

                    b.Property<DateTime?>("survey_date_to");

                    b.Property<DateTime?>("talakayan_date_from");

                    b.Property<DateTime?>("talakayan_date_to");

                    b.Property<int>("talakayan_yr_id");

                    b.Property<int?>("trust_1");

                    b.Property<int?>("trust_2");

                    b.Property<int?>("trust_3");

                    b.Property<int?>("trust_4");

                    b.Property<int?>("trust_5");

                    b.Property<int?>("trust_6");

                    b.Property<int?>("trust_7");

                    b.Property<int?>("trust_8");

                    b.Property<int>("year");

                    b.HasKey("perception_survey_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("perception_survey");
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.talakayan_eval", b =>
                {
                    b.Property<Guid>("talakayan_evaluation_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<DateTime?>("evaluation_date_end");

                    b.Property<DateTime?>("evaluation_date_start");

                    b.Property<int?>("evaluation_form_version");

                    b.Property<bool>("is_deleted");

                    b.Property<bool?>("is_male");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("participant_type");

                    b.Property<string>("person_name");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<DateTime?>("talakayan_date_end");

                    b.Property<DateTime?>("talakayan_date_start");

                    b.Property<string>("talakayan_name");

                    b.Property<string>("talakayan_venue");

                    b.Property<int>("talakayan_yr_id");

                    b.Property<int?>("v2015_evaluation_a");

                    b.Property<string>("v2015_evaluation_b");

                    b.Property<int?>("v2015_obj_a");

                    b.Property<int?>("v2015_obj_b");

                    b.Property<int?>("v2015_partsoftalakayan_part1");

                    b.Property<int?>("v2015_partsoftalakayan_part2");

                    b.Property<int?>("v2015_partsoftalakayan_part3");

                    b.Property<int?>("v2015_partsoftalakayan_part4");

                    b.Property<int?>("v2015_partsoftalakayan_part5");

                    b.Property<int?>("v2015_satisfaction_a");

                    b.Property<string>("v2015_satisfaction_a_remarks");

                    b.Property<int?>("v2015_satisfaction_b");

                    b.Property<string>("v2015_satisfaction_b_remarks");

                    b.Property<int?>("v2015_satisfaction_c");

                    b.Property<string>("v2015_satisfaction_c_remarks");

                    b.Property<int?>("v2015_satisfaction_d");

                    b.Property<string>("v2015_satisfaction_d_remarks");

                    b.Property<int?>("v2015_satisfaction_e");

                    b.Property<string>("v2015_satisfaction_e_remarks");

                    b.Property<int?>("v2015_satisfaction_f");

                    b.Property<string>("v2015_satisfaction_f_remarks");

                    b.Property<int?>("v2015_satisfaction_g");

                    b.Property<string>("v2015_satisfaction_g_remarks");

                    b.Property<int?>("v2015_satisfaction_h");

                    b.Property<string>("v2015_satisfaction_h_remarks");

                    b.Property<int?>("v2015_satisfaction_i");

                    b.Property<string>("v2015_satisfaction_i_remarks");

                    b.Property<int?>("v2015_satisfaction_j");

                    b.Property<string>("v2015_satisfaction_j_remarks");

                    b.Property<int?>("v2015_satisfaction_k");

                    b.Property<string>("v2015_satisfaction_k_remarks");

                    b.Property<int?>("v2015_timeallotment_a_part1");

                    b.Property<int?>("v2015_timeallotment_a_part2");

                    b.Property<int?>("v2015_timeallotment_a_part3");

                    b.Property<int?>("v2015_timeallotment_a_part4");

                    b.Property<int?>("v2015_timeallotment_a_part5");

                    b.Property<int?>("v2015_timeallotment_b_part1");

                    b.Property<int?>("v2015_timeallotment_b_part2");

                    b.Property<int?>("v2015_timeallotment_b_part3");

                    b.Property<int?>("v2015_timeallotment_b_part4");

                    b.Property<int?>("v2015_timeallotment_b_part5");

                    b.Property<int?>("v2015_venue_a");

                    b.Property<int?>("v2015_venue_b");

                    b.Property<int?>("v2015_venue_c");

                    b.Property<int?>("v2015_venue_d");

                    b.Property<int?>("v2015_venue_e");

                    b.Property<int?>("v2015_visual_a");

                    b.Property<int?>("v2015_visual_b");

                    b.Property<int?>("v2015_visual_c");

                    b.Property<int?>("v2015_visual_d");

                    b.Property<int?>("v2015_visual_e");

                    b.Property<string>("v2016_comments");

                    b.Property<int?>("v2016_fgd");

                    b.Property<int?>("v2016_gallery_walk");

                    b.Property<int?>("v2016_knowledge_part1");

                    b.Property<int?>("v2016_knowledge_part2");

                    b.Property<int?>("v2016_knowledge_part3");

                    b.Property<int?>("v2016_knowledge_part4");

                    b.Property<int?>("v2016_knowledge_part5");

                    b.Property<int?>("v2016_meals_a");

                    b.Property<int?>("v2016_meals_b");

                    b.Property<int?>("v2016_meals_c");

                    b.Property<int?>("v2016_obj");

                    b.Property<int?>("v2016_overall_satisfaction");

                    b.Property<int?>("v2016_sound_system");

                    b.Property<int?>("v2016_team_effectiveness");

                    b.Property<int?>("v2016_time_alloted");

                    b.Property<int?>("v2016_venue_a");

                    b.Property<int?>("v2016_venue_b");

                    b.Property<int?>("v2016_venue_c");

                    b.Property<int?>("v2016_venue_d");

                    b.Property<int?>("v2016_visual_a");

                    b.Property<int?>("v2016_visual_b");

                    b.Property<int?>("v2016_visual_c");

                    b.HasKey("talakayan_evaluation_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("talakayan_eval");
                });

            modelBuilder.Entity("DeskApp.DataLayer.grievance_record", b =>
                {
                    b.Property<Guid>("grievance_record_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("actions");

                    b.Property<Guid?>("activity_source_id");

                    b.Property<int>("approval_id");

                    b.Property<int?>("brgy_code");

                    b.Property<string>("cellphone");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("cycle_id");

                    b.Property<DateTime?>("date_intake");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<string>("details");

                    b.Property<string>("email");

                    b.Property<int?>("enrollment_id");

                    b.Property<string>("final_resolution");

                    b.Property<int?>("fund_source_id");

                    b.Property<int>("grs_category_id");

                    b.Property<int>("grs_complaint_subject_id");

                    b.Property<string>("grs_complaint_subject_others");

                    b.Property<int?>("grs_feedback_id");

                    b.Property<int>("grs_filling_mode_id");

                    b.Property<string>("grs_filling_mode_others");

                    b.Property<int>("grs_form_id");

                    b.Property<int>("grs_intake_level_id");

                    b.Property<int?>("grs_intake_officer_id");

                    b.Property<int>("grs_nature_id");

                    b.Property<int?>("grs_pincos_actor_id");

                    b.Property<int>("grs_resolution_status_id");

                    b.Property<int?>("grs_sender_designation_id");

                    b.Property<string>("intake_officer");

                    b.Property<string>("intake_officer_designation");

                    b.Property<int?>("ip_group_id");

                    b.Property<string>("ip_group_other");

                    b.Property<bool?>("is_anonymous");

                    b.Property<bool?>("is_central_office_level_only");

                    b.Property<bool>("is_deleted");

                    b.Property<bool?>("is_field_office_level_only");

                    b.Property<bool?>("is_fund_source_applicable");

                    b.Property<bool?>("is_individual");

                    b.Property<bool?>("is_ip");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("last_sync_source_id");

                    b.Property<string>("old_id");

                    b.Property<int?>("pincos_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<string>("recommendations");

                    b.Property<int>("region_code");

                    b.Property<bool?>("req_brgy");

                    b.Property<bool?>("req_city");

                    b.Property<bool?>("req_province");

                    b.Property<DateTime?>("resolution_date");

                    b.Property<string>("sender_contact_info");

                    b.Property<string>("sender_designation");

                    b.Property<string>("sender_designation_other");

                    b.Property<string>("sender_name");

                    b.Property<string>("sender_organization");

                    b.Property<bool>("sender_sex");

                    b.Property<int>("sex_id");

                    b.Property<int?>("training_category_id");

                    b.HasKey("grievance_record_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("grs_category_id");

                    b.HasIndex("grs_complaint_subject_id");

                    b.HasIndex("grs_feedback_id");

                    b.HasIndex("grs_filling_mode_id");

                    b.HasIndex("grs_form_id");

                    b.HasIndex("grs_intake_level_id");

                    b.HasIndex("grs_intake_officer_id");

                    b.HasIndex("grs_nature_id");

                    b.HasIndex("grs_pincos_actor_id");

                    b.HasIndex("grs_resolution_status_id");

                    b.HasIndex("grs_sender_designation_id");

                    b.HasIndex("ip_group_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.HasIndex("sex_id");

                    b.HasIndex("training_category_id");

                    b.ToTable("grievance_record");
                });

            modelBuilder.Entity("DeskApp.DataLayer.grievance_record_discussion", b =>
                {
                    b.Property<Guid>("grievance_record_discussion_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("created_by");

                    b.Property<string>("created_by_name");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<Guid>("grievance_record_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("last_sync_source_id");

                    b.Property<string>("position");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<string>("remarks");

                    b.HasKey("grievance_record_discussion_id");

                    b.HasIndex("grievance_record_id");

                    b.ToTable("grievance_record_discussion");
                });

            modelBuilder.Entity("DeskApp.DataLayer.grs_installation", b =>
                {
                    b.Property<Guid>("grs_installation_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<int>("approval_id");

                    b.Property<int?>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<DateTime?>("date_ffcomm");

                    b.Property<DateTime?>("date_infodess");

                    b.Property<DateTime?>("date_inspect");

                    b.Property<DateTime?>("date_means");

                    b.Property<DateTime?>("date_meansrept");

                    b.Property<DateTime?>("date_orientation");

                    b.Property<DateTime?>("date_training");

                    b.Property<DateTime?>("date_voliden");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("fund_source_id");

                    b.Property<bool?>("is_boxinstalled");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int>("lgu_level_id");

                    b.Property<int?>("no_brochures");

                    b.Property<int?>("no_manuals");

                    b.Property<int?>("no_posters");

                    b.Property<int?>("no_tarpauline");

                    b.Property<string>("old_id");

                    b.Property<int?>("other_mat");

                    b.Property<string>("phone_no");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<string>("remarks");

                    b.HasKey("grs_installation_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("lgu_level_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("grs_installation");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_approval", b =>
                {
                    b.Property<int>("approval_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("approval_id");

                    b.ToTable("lib_approval");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_barangay_assembly_purpose", b =>
                {
                    b.Property<int>("barangay_assembly_purpose_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("barangay_assembly_purpose_id");

                    b.ToTable("lib_barangay_assembly_purpose");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_brgy", b =>
                {
                    b.Property<int>("brgy_code");

                    b.Property<byte?>("brgy_mode");

                    b.Property<string>("brgy_name");

                    b.Property<int>("city_code");

                    b.Property<string>("district");

                    b.Property<string>("psgc");

                    b.HasKey("brgy_code");

                    b.ToTable("lib_brgy");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_city", b =>
                {
                    b.Property<int>("city_code");

                    b.Property<string>("city_name");

                    b.Property<int>("prov_code");

                    b.Property<string>("psgc");

                    b.HasKey("city_code");

                    b.ToTable("lib_city");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_civil_status", b =>
                {
                    b.Property<int>("civil_status_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("civil_status_id");

                    b.ToTable("lib_civil_status");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_cycle", b =>
                {
                    b.Property<int>("cycle_id");

                    b.Property<int>("fund_source_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("cycle_id");

                    b.HasIndex("fund_source_id");

                    b.ToTable("lib_cycle");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_eca_type", b =>
                {
                    b.Property<int>("eca_type_id");

                    b.Property<string>("name");

                    b.HasKey("eca_type_id");

                    b.ToTable("lib_eca_type");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_education_attainment", b =>
                {
                    b.Property<int>("education_attainment_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("education_attainment_id");

                    b.ToTable("lib_education_attainment");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_enrollment", b =>
                {
                    b.Property<int>("enrollment_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("enrollment_id");

                    b.ToTable("lib_enrollment");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_ers_current_work", b =>
                {
                    b.Property<int>("ers_current_work_id");

                    b.Property<bool?>("is_skilled");

                    b.Property<string>("name");

                    b.HasKey("ers_current_work_id");

                    b.ToTable("lib_ers_current_work");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_ers_delivery_mode", b =>
                {
                    b.Property<int>("ers_delivery_mode_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.HasKey("ers_delivery_mode_id");

                    b.ToTable("lib_ers_delivery_mode");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_fund_source", b =>
                {
                    b.Property<int>("fund_source_id");

                    b.Property<int?>("geotagging_fund_source_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("fund_source_id");

                    b.ToTable("lib_fund_source");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_category", b =>
                {
                    b.Property<int>("grs_category_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_category_id");

                    b.ToTable("lib_grs_category");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_complainant_position", b =>
                {
                    b.Property<int>("grs_complainant_position_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_complainant_position_id");

                    b.ToTable("lib_grs_complainant_position");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_complaint_subject", b =>
                {
                    b.Property<int>("grs_complaint_subject_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_complaint_subject_id");

                    b.ToTable("lib_grs_complaint_subject");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_feedback", b =>
                {
                    b.Property<int>("grs_feedback_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_feedback_id");

                    b.ToTable("lib_grs_feedback");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_filling_mode", b =>
                {
                    b.Property<int>("grs_filling_mode_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_filling_mode_id");

                    b.ToTable("lib_grs_filling_mode");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_form", b =>
                {
                    b.Property<int>("grs_form_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_form_id");

                    b.ToTable("lib_grs_form");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_intake_level", b =>
                {
                    b.Property<int>("grs_intake_level_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("office_level_id");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_intake_level_id");

                    b.ToTable("lib_grs_intake_level");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_intake_officer", b =>
                {
                    b.Property<int>("grs_intake_officer_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int>("office_level_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_intake_officer_id");

                    b.ToTable("lib_grs_intake_officer");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_nature", b =>
                {
                    b.Property<int>("grs_nature_id");

                    b.Property<string>("description");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_nature_id");

                    b.ToTable("lib_grs_nature");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_pincos_actor", b =>
                {
                    b.Property<int>("grs_pincos_actor_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("grs_pincos_actor_id");

                    b.ToTable("lib_grs_pincos_actor");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_resolution_status", b =>
                {
                    b.Property<int>("grs_resolution_status_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_resolution_status_id");

                    b.ToTable("lib_grs_resolution_status");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_grs_sender_designation", b =>
                {
                    b.Property<int>("grs_sender_designation_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<int?>("sort_order");

                    b.HasKey("grs_sender_designation_id");

                    b.ToTable("lib_grs_sender_designation");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_implementation_status", b =>
                {
                    b.Property<int>("implementation_status_id");

                    b.Property<string>("name");

                    b.HasKey("implementation_status_id");

                    b.ToTable("lib_implementation_status");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_ip_group", b =>
                {
                    b.Property<int>("ip_group_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("ip_group_id");

                    b.ToTable("lib_ip_group");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_kalahi_committee", b =>
                {
                    b.Property<int>("kalahi_committee_id");

                    b.Property<string>("name");

                    b.HasKey("kalahi_committee_id");

                    b.ToTable("lib_kalahi_committee");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_lgu_level", b =>
                {
                    b.Property<int>("lgu_level_id");

                    b.Property<string>("name");

                    b.HasKey("lgu_level_id");

                    b.ToTable("lib_lgu_level");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_lgu_position", b =>
                {
                    b.Property<int>("lgu_position_id");

                    b.Property<bool?>("is_active");

                    b.Property<int>("lgu_level_id");

                    b.Property<string>("name");

                    b.HasKey("lgu_position_id");

                    b.ToTable("lib_lgu_position");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_major_classification", b =>
                {
                    b.Property<int>("major_classification_id");

                    b.Property<string>("name");

                    b.HasKey("major_classification_id");

                    b.ToTable("lib_major_classification");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_occupation", b =>
                {
                    b.Property<int>("occupation_id");

                    b.Property<string>("description");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.Property<string>("sub_groups");

                    b.HasKey("occupation_id");

                    b.ToTable("lib_occupation");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_organization", b =>
                {
                    b.Property<int>("organization_id");

                    b.Property<string>("name");

                    b.HasKey("organization_id");

                    b.ToTable("lib_organization");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_physical_status", b =>
                {
                    b.Property<int>("physical_status_id");

                    b.Property<string>("name");

                    b.HasKey("physical_status_id");

                    b.ToTable("lib_physical_status");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_physical_status_category", b =>
                {
                    b.Property<int>("physical_status_category_id");

                    b.Property<string>("name");

                    b.HasKey("physical_status_category_id");

                    b.ToTable("lib_physical_status_category");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_project_type", b =>
                {
                    b.Property<int>("project_type_id");

                    b.Property<int?>("deleted");

                    b.Property<int?>("geo_category_id");

                    b.Property<int?>("major_classification_id");

                    b.Property<string>("name");

                    b.Property<string>("phy_construction_unit");

                    b.Property<string>("phy_construction_unit_secondary");

                    b.Property<bool?>("phy_has_construction_target");

                    b.Property<bool?>("phy_has_construction_target_secondary");

                    b.Property<bool?>("phy_has_improvement_target");

                    b.Property<bool?>("phy_has_improvement_target_secondary");

                    b.Property<bool?>("phy_has_purchase_target");

                    b.Property<bool?>("phy_has_purchase_target_secondary");

                    b.Property<bool?>("phy_has_rehabilitation_target");

                    b.Property<bool?>("phy_has_rehabilitation_target_secondary");

                    b.Property<string>("phy_improvement_unit");

                    b.Property<string>("phy_improvement_unit_secondary");

                    b.Property<string>("phy_purchase_unit");

                    b.Property<string>("phy_purchase_unit_secondary");

                    b.Property<string>("phy_rehabilitation_unit");

                    b.Property<string>("phy_rehabilitation_unit_secondary");

                    b.Property<string>("project_type_id_old");

                    b.Property<int?>("sub_class_id");

                    b.Property<string>("tsunit");

                    b.HasKey("project_type_id");

                    b.HasIndex("major_classification_id");

                    b.ToTable("lib_project_type");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_province", b =>
                {
                    b.Property<int>("prov_code");

                    b.Property<string>("prov_name");

                    b.Property<string>("psgc");

                    b.Property<int>("region_code");

                    b.HasKey("prov_code");

                    b.ToTable("lib_province");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_psa_problem_category", b =>
                {
                    b.Property<int>("psa_problem_category_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("psa_problem_category_id");

                    b.ToTable("lib_psa_problem_category");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_psa_solution_category", b =>
                {
                    b.Property<int>("psa_solution_category_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("psa_solution_category_id");

                    b.ToTable("lib_psa_solution_category");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_push_status", b =>
                {
                    b.Property<int>("push_status_id");

                    b.Property<string>("name");

                    b.HasKey("push_status_id");

                    b.ToTable("lib_push_status");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_region", b =>
                {
                    b.Property<int>("region_code");

                    b.Property<string>("psgc");

                    b.Property<string>("region_name");

                    b.Property<string>("region_nick");

                    b.HasKey("region_code");

                    b.ToTable("lib_region");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_sector", b =>
                {
                    b.Property<int>("sector_id");

                    b.Property<string>("name");

                    b.HasKey("sector_id");

                    b.ToTable("lib_sector");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_sex", b =>
                {
                    b.Property<int>("sex_id");

                    b.Property<string>("desktop_value");

                    b.Property<string>("name");

                    b.HasKey("sex_id");

                    b.ToTable("lib_sex");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_source_income", b =>
                {
                    b.Property<int>("source_income_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.HasKey("source_income_id");

                    b.ToTable("lib_source_income");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_training_category", b =>
                {
                    b.Property<int>("training_category_id");

                    b.Property<int?>("brgy_sort_order");

                    b.Property<string>("description");

                    b.Property<bool?>("is_accelerated");

                    b.Property<bool?>("is_active");

                    b.Property<bool?>("is_barangay");

                    b.Property<bool?>("is_ceac");

                    b.Property<bool?>("is_ceac_tracking_only");

                    b.Property<bool?>("is_municipality");

                    b.Property<bool?>("is_regular");

                    b.Property<int?>("muni_sort_order");

                    b.Property<string>("name");

                    b.Property<int?>("return_id");

                    b.HasKey("training_category_id");

                    b.ToTable("lib_training_category");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_transpo_mode", b =>
                {
                    b.Property<int>("transpo_mode_id");

                    b.Property<string>("name");

                    b.HasKey("transpo_mode_id");

                    b.ToTable("lib_transpo_mode");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_volunteer_committee", b =>
                {
                    b.Property<int>("volunteer_committee_id");

                    b.Property<string>("name");

                    b.HasKey("volunteer_committee_id");

                    b.ToTable("lib_volunteer_committee");
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_volunteer_committee_position", b =>
                {
                    b.Property<int>("volunteer_committee_position_id");

                    b.Property<bool?>("is_active");

                    b.Property<string>("name");

                    b.HasKey("volunteer_committee_position_id");

                    b.ToTable("lib_volunteer_committee_position");
                });

            modelBuilder.Entity("DeskApp.DataLayer.mibf_criteria", b =>
                {
                    b.Property<Guid>("mibf_criteria_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<Guid>("community_training_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<string>("criteria");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("old_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<double?>("rate");

                    b.HasKey("mibf_criteria_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("community_training_id");

                    b.ToTable("mibf_criteria");
                });

            modelBuilder.Entity("DeskApp.DataLayer.mibf_prioritization", b =>
                {
                    b.Property<Guid>("mibf_prioritization_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("added_to_spi");

                    b.Property<int>("approval_id");

                    b.Property<int>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<Guid>("community_training_id");

                    b.Property<string>("coverage");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_priority");

                    b.Property<double?>("kc_amount");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<double?>("lcc_amount");

                    b.Property<string>("old_id");

                    b.Property<double?>("pamana_amount");

                    b.Property<int?>("priority");

                    b.Property<string>("project_name");

                    b.Property<int?>("project_type_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int?>("rank");

                    b.Property<int>("region_code");

                    b.Property<int?>("type");

                    b.HasKey("mibf_prioritization_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("community_training_id");

                    b.HasIndex("project_type_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("mibf_prioritization");
                });

            modelBuilder.Entity("DeskApp.DataLayer.muni_profile", b =>
                {
                    b.Property<Guid>("muni_profile_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("alloc_basic");

                    b.Property<int?>("alloc_drrm");

                    b.Property<int?>("alloc_econ");

                    b.Property<int?>("alloc_educ");

                    b.Property<int?>("alloc_env");

                    b.Property<int?>("alloc_gender");

                    b.Property<int?>("alloc_infra");

                    b.Property<int?>("alloc_inst");

                    b.Property<int?>("alloc_others");

                    b.Property<int?>("alloc_peace");

                    b.Property<int>("approval_id");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<string>("drrm_activity");

                    b.Property<double?>("drrm_utilized");

                    b.Property<int?>("femaleheaded_hhs");

                    b.Property<int>("fund_source_id");

                    b.Property<string>("gad_activity");

                    b.Property<double?>("gad_utilized");

                    b.Property<int?>("headfemale_renting");

                    b.Property<int?>("headfemale_squatting");

                    b.Property<int?>("headfemale_tenant");

                    b.Property<int?>("headffemale_owner");

                    b.Property<int?>("headmale_owner");

                    b.Property<int?>("headmale_renting");

                    b.Property<int?>("headmale_squatting");

                    b.Property<int?>("headmale_tenant");

                    b.Property<int?>("hhs_owner");

                    b.Property<int?>("hhs_renting");

                    b.Property<int?>("hhs_squatting");

                    b.Property<int?>("hhs_tenant");

                    b.Property<double?>("householdincome_average");

                    b.Property<int?>("ipheaded_hhs");

                    b.Property<double?>("ira_amount");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("maleheaded_hhs");

                    b.Property<int?>("no_drmm_activities");

                    b.Property<int?>("no_gad_activities");

                    b.Property<int?>("no_of_brgys");

                    b.Property<string>("old_activity_id");

                    b.Property<string>("old_id");

                    b.Property<string>("other_source");

                    b.Property<double?>("others_amount");

                    b.Property<double?>("pop_index");

                    b.Property<int?>("population");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<string>("socio_econ_activity_1");

                    b.Property<string>("socio_econ_activity_2");

                    b.Property<string>("socio_econ_activity_3");

                    b.Property<double?>("socio_econ_amount_1");

                    b.Property<double?>("socio_econ_amount_2");

                    b.Property<double?>("socio_econ_amount_3");

                    b.Property<int?>("socio_econ_hhs_1");

                    b.Property<int?>("socio_econ_hhs_2");

                    b.Property<int?>("socio_econ_hhs_3");

                    b.Property<string>("socio_econ_seasonality_from_1");

                    b.Property<string>("socio_econ_seasonality_from_2");

                    b.Property<string>("socio_econ_seasonality_from_3");

                    b.Property<string>("socio_econ_seasonality_to_1");

                    b.Property<string>("socio_econ_seasonality_to_2");

                    b.Property<string>("socio_econ_seasonality_to_3");

                    b.Property<string>("transportation");

                    b.Property<int?>("year_source");

                    b.HasKey("muni_profile_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("muni_profile");
                });

            modelBuilder.Entity("DeskApp.DataLayer.muni_profile_fund_use", b =>
                {
                    b.Property<Guid>("muni_profile_fund_use_id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("amount");

                    b.Property<int>("approval_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_gad");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<Guid>("muni_profile_id");

                    b.Property<string>("name");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.HasKey("muni_profile_fund_use_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("muni_profile_id");

                    b.ToTable("muni_profile_fund_use");
                });

            modelBuilder.Entity("DeskApp.DataLayer.muni_profile_income", b =>
                {
                    b.Property<Guid>("muni_profile_income_id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("amount");

                    b.Property<int>("approval_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("families");

                    b.Property<int>("households");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<int?>("lib_source_incomesource_income_id");

                    b.Property<Guid>("muni_profile_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.HasKey("muni_profile_income_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("lib_source_incomesource_income_id");

                    b.HasIndex("muni_profile_id");

                    b.ToTable("muni_profile_income");
                });

            modelBuilder.Entity("DeskApp.DataLayer.municipal_lcc", b =>
                {
                    b.Property<Guid>("municipal_lcc_id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("actual_cash");

                    b.Property<double>("actual_inkind");

                    b.Property<int>("approval_id");

                    b.Property<double>("cbis_blgu_actual");

                    b.Property<double>("cbis_blgu_planned");

                    b.Property<double>("cbis_mlgu_actual");

                    b.Property<double>("cbis_mlgu_planned");

                    b.Property<double>("cbis_others_actual");

                    b.Property<double>("cbis_others_planned");

                    b.Property<double>("cbis_plgu_actual");

                    b.Property<double>("cbis_plgu_planned");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int>("fund_source_id");

                    b.Property<string>("history");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<double>("me_blgu_actual");

                    b.Property<double>("me_blgu_planned");

                    b.Property<double>("me_mlgu_actual");

                    b.Property<double>("me_mlgu_planned");

                    b.Property<double>("me_others_actual");

                    b.Property<double>("me_others_planned");

                    b.Property<double>("me_plgu_actual");

                    b.Property<double>("me_plgu_planned");

                    b.Property<int>("no_of_barangays");

                    b.Property<string>("old_id");

                    b.Property<double>("planned_cash");

                    b.Property<double>("planned_inkind");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<double>("spi_blgu_actual");

                    b.Property<double>("spi_blgu_planned");

                    b.Property<double>("spi_mlgu_actual");

                    b.Property<double>("spi_mlgu_planned");

                    b.Property<double>("spi_others_actual");

                    b.Property<double>("spi_others_planned");

                    b.Property<double>("spi_plgu_actual");

                    b.Property<double>("spi_plgu_planned");

                    b.HasKey("municipal_lcc_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("push_status_id");

                    b.HasIndex("region_code");

                    b.ToTable("municipal_lcc");
                });

            modelBuilder.Entity("DeskApp.DataLayer.municipal_pta", b =>
                {
                    b.Property<Guid>("municipal_pta_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<string>("budget_location_post");

                    b.Property<DateTime?>("budget_post_date");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<string>("encoder");

                    b.Property<int>("enrollment_id");

                    b.Property<string>("focal_person");

                    b.Property<int>("fund_source_id");

                    b.Property<DateTime?>("gad_approval_date");

                    b.Property<string>("gad_resolution_no");

                    b.Property<string>("incexp_location_post");

                    b.Property<DateTime?>("incexp_post_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<string>("kc_equipment");

                    b.Property<string>("kc_equipment_list");

                    b.Property<string>("kc_office");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("latitude");

                    b.Property<DateTime?>("lcc_approval_date");

                    b.Property<string>("lcc_resolution_no");

                    b.Property<string>("longitude");

                    b.Property<int?>("lsb_represented_female");

                    b.Property<int?>("lsb_represented_male");

                    b.Property<DateTime?>("mct_approval_date");

                    b.Property<DateTime?>("mct_eo_date");

                    b.Property<string>("mct_eo_no");

                    b.Property<string>("mct_resolution_no");

                    b.Property<string>("mdc_date");

                    b.Property<string>("mdc_resolution_no");

                    b.Property<DateTime?>("mdcmem_approval_date");

                    b.Property<int?>("mdcmem_female_no");

                    b.Property<int?>("mdcmem_male_no");

                    b.Property<string>("mdcmem_resolution_no");

                    b.Property<DateTime?>("miac_approval_date");

                    b.Property<DateTime?>("miac_eo_date");

                    b.Property<string>("miac_eo_no");

                    b.Property<string>("miac_resolution_no");

                    b.Property<DateTime?>("miacmct_approval_date");

                    b.Property<string>("miacmct_resolution_no");

                    b.Property<string>("mlgu_logistics");

                    b.Property<DateTime?>("moa_signed_date");

                    b.Property<DateTime?>("nga_approval_date");

                    b.Property<string>("nga_resolution_no");

                    b.Property<int?>("ngo_total");

                    b.Property<int?>("ngopo_accredited");

                    b.Property<DateTime?>("ngopo_approval_date");

                    b.Property<string>("ngopo_resolution_no");

                    b.Property<int?>("no_4p_female");

                    b.Property<int?>("no_4p_male");

                    b.Property<int?>("no_cdd_female");

                    b.Property<int?>("no_cdd_male");

                    b.Property<int?>("no_elderly_female");

                    b.Property<int?>("no_elderly_male");

                    b.Property<int?>("no_gad_plan_assessment");

                    b.Property<int?>("no_ip_female");

                    b.Property<int?>("no_ip_male");

                    b.Property<int?>("no_lgu_cso_meeting");

                    b.Property<int?>("no_lgu_female");

                    b.Property<int?>("no_lgu_male");

                    b.Property<int?>("no_ngo_pim");

                    b.Property<int?>("no_ngopo_accredited");

                    b.Property<int?>("no_ngopo_female");

                    b.Property<int?>("no_ngopo_male");

                    b.Property<int?>("no_ngopo_represented");

                    b.Property<int?>("no_ngopo_represented_female");

                    b.Property<int?>("no_ngopo_represented_male");

                    b.Property<int?>("no_pwd_female");

                    b.Property<int?>("no_pwd_male");

                    b.Property<int?>("no_staff");

                    b.Property<int?>("no_tas");

                    b.Property<int?>("no_women_female");

                    b.Property<int?>("no_women_male");

                    b.Property<int?>("no_youth_female");

                    b.Property<int?>("no_youth_male");

                    b.Property<string>("office_address");

                    b.Property<string>("old_id");

                    b.Property<string>("plan_location_post");

                    b.Property<DateTime?>("plan_post_date");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("pta_approval_date");

                    b.Property<string>("pta_resolution_no");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<string>("trust_account_no");

                    b.Property<DateTime?>("trust_opened_date");

                    b.Property<bool?>("with_equipment");

                    b.HasKey("municipal_pta_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("push_status_id");

                    b.HasIndex("region_code");

                    b.ToTable("municipal_pta");
                });

            modelBuilder.Entity("DeskApp.DataLayer.oversight_committee", b =>
                {
                    b.Property<Guid>("oversight_committee_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<DateTime?>("date_organized");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int?>("frequency");

                    b.Property<int>("fund_source_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("name");

                    b.Property<int?>("no_female");

                    b.Property<int?>("no_male");

                    b.Property<string>("old_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.HasKey("oversight_committee_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("oversight_committee");
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_ers_work", b =>
                {
                    b.Property<Guid>("person_ers_work_id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("actual_cash");

                    b.Property<decimal?>("actual_lcc");

                    b.Property<int>("approval_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("ers_current_work_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("old_id");

                    b.Property<decimal?>("percent");

                    b.Property<Guid>("person_profile_id");

                    b.Property<decimal?>("plan_cash");

                    b.Property<decimal?>("plan_lcc");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<decimal?>("rate_day");

                    b.Property<decimal?>("rate_hauling");

                    b.Property<decimal?>("rate_hour");

                    b.Property<Guid>("sub_project_ers_id");

                    b.Property<string>("unit_hauling");

                    b.Property<decimal?>("work_days");

                    b.Property<decimal?>("work_hauling");

                    b.Property<decimal?>("work_hours");

                    b.HasKey("person_ers_work_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("ers_current_work_id");

                    b.HasIndex("person_profile_id");

                    b.HasIndex("sub_project_ers_id");

                    b.ToTable("person_ers_work");
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_profile", b =>
                {
                    b.Property<Guid>("person_profile_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<DateTime?>("birthdate");

                    b.Property<int?>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int?>("civil_status_id");

                    b.Property<string>("contact_no");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<DateTime?>("date_appointment");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int?>("education_attainment_id");

                    b.Property<int?>("entry_id");

                    b.Property<string>("first_name");

                    b.Property<string>("household_id");

                    b.Property<int>("idenity");

                    b.Property<int?>("ip_group_id");

                    b.Property<bool?>("is_bdc");

                    b.Property<bool?>("is_bspmc");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_ip");

                    b.Property<bool?>("is_ipleader");

                    b.Property<bool?>("is_lguofficial");

                    b.Property<bool?>("is_mdc");

                    b.Property<bool?>("is_pantawid");

                    b.Property<bool?>("is_pantawid_leader");

                    b.Property<bool?>("is_sector_academe");

                    b.Property<bool?>("is_sector_business");

                    b.Property<bool?>("is_sector_farmer");

                    b.Property<bool?>("is_sector_fisherfolks");

                    b.Property<bool?>("is_sector_government");

                    b.Property<bool?>("is_sector_ip");

                    b.Property<bool?>("is_sector_ngo");

                    b.Property<bool?>("is_sector_po");

                    b.Property<bool?>("is_sector_pwd");

                    b.Property<bool?>("is_sector_religios");

                    b.Property<bool?>("is_sector_senior");

                    b.Property<bool?>("is_sector_women");

                    b.Property<bool?>("is_sector_youth");

                    b.Property<bool?>("is_slp");

                    b.Property<bool?>("is_slp_leader");

                    b.Property<bool?>("is_volunteer");

                    b.Property<bool?>("is_worker");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("last_name");

                    b.Property<int?>("last_sync_source_id");

                    b.Property<int?>("lgu_position_id");

                    b.Property<string>("middle_name");

                    b.Property<int?>("no_children");

                    b.Property<int?>("occupation_id");

                    b.Property<string>("old_address");

                    b.Property<string>("old_id");

                    b.Property<string>("other_membership");

                    b.Property<string>("other_training");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<string>("sector");

                    b.Property<bool?>("sex");

                    b.Property<string>("sitio");

                    b.HasKey("person_profile_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("civil_status_id");

                    b.HasIndex("education_attainment_id");

                    b.HasIndex("ip_group_id");

                    b.HasIndex("lgu_position_id");

                    b.HasIndex("occupation_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("push_status_id");

                    b.HasIndex("region_code");

                    b.ToTable("person_profile");
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_training", b =>
                {
                    b.Property<Guid>("person_training_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<Guid>("community_training_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("ig_bdc");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_mdc");

                    b.Property<bool?>("is_participant");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<Guid>("person_profile_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.HasKey("person_training_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("community_training_id");

                    b.HasIndex("person_profile_id");

                    b.HasIndex("push_status_id");

                    b.ToTable("person_training");
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_volunteer_record", b =>
                {
                    b.Property<Guid>("person_volunteer_record_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<DateTime?>("end_date");

                    b.Property<int>("enrollment_id");

                    b.Property<int>("fund_source_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<Guid>("person_profile_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<DateTime?>("start_date");

                    b.Property<int>("volunteer_committee_id");

                    b.Property<int>("volunteer_committee_position_id");

                    b.HasKey("person_volunteer_record_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("cycle_id");

                    b.HasIndex("enrollment_id");

                    b.HasIndex("fund_source_id");

                    b.HasIndex("person_profile_id");

                    b.HasIndex("volunteer_committee_id");

                    b.HasIndex("volunteer_committee_position_id");

                    b.ToTable("person_volunteer_record");
                });

            modelBuilder.Entity("DeskApp.DataLayer.psa_problem", b =>
                {
                    b.Property<Guid>("psa_problem_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<Guid>("community_training_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("old_id");

                    b.Property<string>("problem");

                    b.Property<int>("psa_problem_category_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int?>("rank");

                    b.HasKey("psa_problem_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("community_training_id");

                    b.HasIndex("psa_problem_category_id");

                    b.ToTable("psa_problem");
                });

            modelBuilder.Entity("DeskApp.DataLayer.psa_solution", b =>
                {
                    b.Property<Guid>("psa_solution_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("old_id");

                    b.Property<Guid>("psa_problem_id");

                    b.Property<int?>("psa_solution_category_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<string>("solution");

                    b.HasKey("psa_solution_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("psa_solution_category_id");

                    b.ToTable("psa_solution");
                });

            modelBuilder.Entity("DeskApp.DataLayer.SPPhoto", b =>
                {
                    b.Property<int>("Id");

                    b.Property<double?>("Altitude");

                    b.Property<string>("Comment");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("Day");

                    b.Property<string>("FileName");

                    b.Property<string>("GetDateTaken");

                    b.Property<string>("GpsDateTimeStamp");

                    b.Property<bool>("IsOtherTypeOfProject");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedDate");

                    b.Property<double?>("Latitude")
                        .IsRequired();

                    b.Property<double?>("Longitude")
                        .IsRequired();

                    b.Property<string>("Month");

                    b.Property<string>("Type");

                    b.Property<Guid>("UniqueName");

                    b.Property<int?>("Year");

                    b.Property<string>("act_approved");

                    b.Property<DateTime?>("act_last_approved");

                    b.Property<int?>("album_id");

                    b.Property<int>("approval_id");

                    b.Property<string>("co_approved");

                    b.Property<DateTime?>("co_last_approved");

                    b.Property<string>("deleted_by");

                    b.Property<int?>("geo_category_id");

                    b.Property<bool?>("has_backup");

                    b.Property<bool?>("is_deleted");

                    b.Property<bool?>("is_missing_photo");

                    b.Property<int>("lib_functionality_id");

                    b.Property<int?>("other_project_type_id");

                    b.Property<int?>("photo_description_id");

                    b.Property<int?>("photo_description_id_new");

                    b.Property<int?>("photo_position_id");

                    b.Property<int?>("reject_id");

                    b.Property<string>("rme_approved");

                    b.Property<DateTime?>("rme_last_approved");

                    b.Property<int?>("sequence_id");

                    b.Property<string>("srpmo_approved");

                    b.Property<DateTime?>("srpmo_last_approved");

                    b.Property<int>("sub_project_id");

                    b.Property<Guid?>("sub_project_unique_id");

                    b.HasKey("Id");

                    b.ToTable("SPPhoto");
                });

            modelBuilder.Entity("DeskApp.DataLayer.sub_project", b =>
                {
                    b.Property<Guid>("sub_project_unique_id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("Add_Ind");

                    b.Property<double?>("Balance");

                    b.Property<long?>("BalanceNotReq");

                    b.Property<DateTime?>("DateExtracted");

                    b.Property<DateTime?>("DateUpdated");

                    b.Property<DateTime?>("Date_Started");

                    b.Property<DateTime?>("Date_of_Completion");

                    b.Property<long?>("Delete_Ind");

                    b.Property<long?>("Edit_Ind");

                    b.Property<int>("EngineeringMigrationId");

                    b.Property<double?>("ExcessFund");

                    b.Property<long?>("ID");

                    b.Property<bool?>("IsActive");

                    b.Property<double?>("KC_Adj");

                    b.Property<double?>("KC_Fund_Released");

                    b.Property<double?>("KalahiAmt_Old");

                    b.Property<double?>("Kalahi_Amount");

                    b.Property<double?>("LCCAmt_Old");

                    b.Property<double?>("LCC_Adj");

                    b.Property<double?>("LCC_Amount");

                    b.Property<long?>("LandOwnership");

                    b.Property<double?>("Latitude");

                    b.Property<double?>("Longitude");

                    b.Property<long?>("Month");

                    b.Property<long?>("NoOfBgys");

                    b.Property<long?>("NoOfClassroom");

                    b.Property<long?>("NoOfHH");

                    b.Property<long?>("NoOfHHActual");

                    b.Property<long?>("NoOfHPumps");

                    b.Property<long?>("NoOfTapstands");

                    b.Property<long?>("OldSP_ID");

                    b.Property<double?>("PamanaGrant");

                    b.Property<double?>("Phy_Perc_Previous");

                    b.Property<double?>("Phy_Perc_This_Month");

                    b.Property<double?>("Phy_Perc_To_Date");

                    b.Property<string>("Phy_Target_Unit");

                    b.Property<double?>("Physical_Target");

                    b.Property<DateTime?>("PlannedDate_Completion");

                    b.Property<double?>("ProjectDuration");

                    b.Property<long?>("RFR_Ref_No");

                    b.Property<long?>("RFR_Update");

                    b.Property<string>("Remarks");

                    b.Property<long?>("ReportID");

                    b.Property<string>("SchoolType");

                    b.Property<double?>("SkilledLaborCost");

                    b.Property<double?>("TPC_Adj");

                    b.Property<double?>("Total_Amount");

                    b.Property<double?>("Totaltrancheamount");

                    b.Property<double?>("Totaltrancheamount_utilized");

                    b.Property<long?>("Tranch1Flag");

                    b.Property<string>("Tranche");

                    b.Property<double?>("Tranche1Amt_Old");

                    b.Property<double?>("Tranche1Perc");

                    b.Property<double?>("Tranche1Phy");

                    b.Property<double?>("Tranche1amount");

                    b.Property<double?>("Tranche1amount_utilized");

                    b.Property<DateTime?>("Tranche1date");

                    b.Property<double?>("Tranche2Amt_Old");

                    b.Property<double?>("Tranche2Perc");

                    b.Property<double?>("Tranche2Phy");

                    b.Property<double?>("Tranche2amount");

                    b.Property<double?>("Tranche2amount_utilized");

                    b.Property<DateTime?>("Tranche2date");

                    b.Property<double?>("Tranche3Amt_Old");

                    b.Property<double?>("Tranche3Perc");

                    b.Property<double?>("Tranche3Phy");

                    b.Property<double?>("Tranche3amount");

                    b.Property<double?>("Tranche3amount_utilized");

                    b.Property<DateTime?>("Tranche3date");

                    b.Property<double?>("UnskilledLaborCost");

                    b.Property<string>("UpdatedBy");

                    b.Property<long?>("Vdr_Update");

                    b.Property<long?>("WS_wd_Sanitation");

                    b.Property<long?>("WaterSystemType");

                    b.Property<long?>("Year");

                    b.Property<decimal?>("_dep_ed_amount");

                    b.Property<double?>("actual_breakdown");

                    b.Property<int?>("actual_female");

                    b.Property<int?>("actual_male");

                    b.Property<int?>("ancestral_domain_status_id");

                    b.Property<int?>("approval_id");

                    b.Property<int>("brgy_code");

                    b.Property<string>("bub_unique_id");

                    b.Property<int?>("cadt_no_ips");

                    b.Property<bool?>("cadteable");

                    b.Property<int>("city_code");

                    b.Property<string>("community_formation_list");

                    b.Property<Guid?>("community_training_id");

                    b.Property<string>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<string>("damage_list");

                    b.Property<DateTime?>("date_start_financial_actual");

                    b.Property<DateTime?>("date_start_financial_planned");

                    b.Property<DateTime?>("date_start_physical_actual");

                    b.Property<DateTime?>("date_start_physical_planned");

                    b.Property<string>("dbm_project_name");

                    b.Property<string>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<string>("deleted_reason");

                    b.Property<string>("dep_ed_amount");

                    b.Property<int?>("enrollment_type_id");

                    b.Property<int?>("erfr_id_t1");

                    b.Property<int?>("erfr_id_t2");

                    b.Property<int?>("erfr_id_t3");

                    b.Property<DateTime?>("erfr_mibf_date");

                    b.Property<double?>("erfr_mibf_grant_cost");

                    b.Property<double?>("erfr_mibf_lcc_cost");

                    b.Property<string>("erfr_mibf_project_name");

                    b.Property<int?>("erfr_project_id");

                    b.Property<decimal?>("erfr_t1");

                    b.Property<decimal?>("erfr_t2");

                    b.Property<decimal?>("erfr_t3");

                    b.Property<int?>("fund_source_id");

                    b.Property<int?>("geotagged_act_uploaded_7");

                    b.Property<int?>("geotagged_co_approved_1");

                    b.Property<int?>("geotagged_co_disapproved_5");

                    b.Property<int?>("geotagged_fo_approved_2");

                    b.Property<int?>("geotagged_fo_disapproved_4");

                    b.Property<int?>("geotagged_srpmo_approved_3");

                    b.Property<int?>("geotagged_srpmo_disapproved_6");

                    b.Property<bool?>("has_after_photo");

                    b.Property<bool?>("has_before_photo");

                    b.Property<bool?>("has_cadt");

                    b.Property<bool?>("has_cadteable");

                    b.Property<bool?>("has_closed_account");

                    b.Property<bool?>("has_ip_presence");

                    b.Property<bool?>("has_local_counterpart");

                    b.Property<bool?>("has_marker");

                    b.Property<bool?>("has_ncip");

                    b.Property<bool?>("has_on_process");

                    b.Property<bool?>("has_onm_group");

                    b.Property<bool?>("has_sc_cnc");

                    b.Property<bool?>("has_sc_cno");

                    b.Property<bool?>("has_sc_cp");

                    b.Property<bool?>("has_sc_ecc");

                    b.Property<bool?>("has_sc_esmp");

                    b.Property<bool?>("has_sc_essc");

                    b.Property<bool?>("has_scanned_spcr");

                    b.Property<bool?>("has_set");

                    b.Property<double?>("has_set_score");

                    b.Property<bool?>("has_t1");

                    b.Property<bool?>("has_t2");

                    b.Property<bool?>("has_t3");

                    b.Property<bool?>("has_turnover_certificate");

                    b.Property<bool?>("has_validation_conducted");

                    b.Property<bool?>("has_variation");

                    b.Property<string>("ip_groups");

                    b.Property<bool?>("is_correct_sp_id");

                    b.Property<bool?>("is_created_via_migration");

                    b.Property<bool?>("is_deped_funded");

                    b.Property<bool?>("is_duplicate");

                    b.Property<bool?>("is_enhancement_functionality");

                    b.Property<bool?>("is_incentive");

                    b.Property<bool?>("is_kalahi_funded");

                    b.Property<bool?>("is_lgu_led");

                    b.Property<bool?>("is_multiple_sps");

                    b.Property<bool?>("is_public_school_for_ip");

                    b.Property<bool?>("is_pulled_from_pra");

                    b.Property<bool?>("is_savings");

                    b.Property<bool?>("is_sp_functional");

                    b.Property<bool?>("is_updated");

                    b.Property<int?>("land_acquisition_id");

                    b.Property<bool?>("land_aq_blgu_resolution");

                    b.Property<bool?>("land_aq_deed_of_donation");

                    b.Property<bool?>("land_aq_deped_certification");

                    b.Property<bool?>("land_aq_other");

                    b.Property<bool?>("land_aq_permit");

                    b.Property<string>("land_aq_permit_issued_by");

                    b.Property<bool?>("land_aq_private_permit_to_construct");

                    b.Property<bool?>("land_aq_private_quit_claim");

                    b.Property<bool?>("land_aq_private_right_of_way_agreement");

                    b.Property<bool?>("land_aq_unsfruct");

                    b.Property<bool?>("land_ownership_dod");

                    b.Property<bool?>("land_ownership_gov");

                    b.Property<bool?>("land_ownership_private_titled");

                    b.Property<bool?>("land_ownership_private_untitled");

                    b.Property<bool?>("land_ownership_public_lands");

                    b.Property<int?>("last_modified_by");

                    b.Property<string>("last_updated_by");

                    b.Property<DateTime?>("last_updated_date");

                    b.Property<string>("lgu_engagement");

                    b.Property<string>("maintainance_list");

                    b.Property<Guid>("mibf_prioritization_id");

                    b.Property<int?>("modality_category_id");

                    b.Property<int>("modality_id");

                    b.Property<int?>("mode_id");

                    b.Property<string>("mode_of_implementation");

                    b.Property<int?>("movement_id");

                    b.Property<DateTime?>("ncip_date");

                    b.Property<bool?>("ncip_submitted");

                    b.Property<DateTime?>("ncip_submitted_date");

                    b.Property<bool?>("ncip_validated");

                    b.Property<DateTime?>("ncip_validated_date");

                    b.Property<int?>("no_actual_families");

                    b.Property<int?>("no_actual_female");

                    b.Property<int?>("no_actual_households");

                    b.Property<int?>("no_actual_ip_families");

                    b.Property<int?>("no_actual_ip_households");

                    b.Property<int?>("no_actual_male");

                    b.Property<int?>("no_actual_pantawid_families");

                    b.Property<int?>("no_actual_pantawid_households");

                    b.Property<int?>("no_actual_pwd_families");

                    b.Property<int?>("no_actual_pwd_households");

                    b.Property<int?>("no_actual_senior_families");

                    b.Property<int?>("no_actual_senior_households");

                    b.Property<int?>("no_actual_slp_families");

                    b.Property<int?>("no_actual_slp_households");

                    b.Property<int?>("no_non_pantawid_families");

                    b.Property<int?>("no_pantawid_families");

                    b.Property<int?>("no_target_families");

                    b.Property<int?>("no_target_female");

                    b.Property<int?>("no_target_households");

                    b.Property<int?>("no_target_ip_families");

                    b.Property<int?>("no_target_ip_households");

                    b.Property<int?>("no_target_male");

                    b.Property<int?>("no_target_pantawid_families");

                    b.Property<int?>("no_target_pantawid_households");

                    b.Property<int?>("no_target_pwd_families");

                    b.Property<int?>("no_target_pwd_households");

                    b.Property<int?>("no_target_senior_families");

                    b.Property<int?>("no_target_senior_households");

                    b.Property<int?>("no_target_slp_families");

                    b.Property<int?>("no_target_slp_households");

                    b.Property<bool?>("on_process_cadt");

                    b.Property<double?>("operation_maintainance_cost");

                    b.Property<string>("other_land_acquisition");

                    b.Property<string>("path");

                    b.Property<bool?>("permit_to_construct_enter");

                    b.Property<int?>("photos");

                    b.Property<decimal?>("phy_construction_actual");

                    b.Property<decimal?>("phy_construction_actual_secondary");

                    b.Property<decimal?>("phy_construction_target");

                    b.Property<decimal?>("phy_construction_target_secondary");

                    b.Property<bool?>("phy_has_construction_target");

                    b.Property<bool?>("phy_has_construction_target_secondary");

                    b.Property<bool?>("phy_has_improvement_target");

                    b.Property<bool?>("phy_has_improvement_target_secondary");

                    b.Property<bool?>("phy_has_purchase_target");

                    b.Property<bool?>("phy_has_purchase_target_secondary");

                    b.Property<bool?>("phy_has_rehabilitation_target");

                    b.Property<bool?>("phy_has_rehabilitation_target_secondary");

                    b.Property<decimal?>("phy_improvement_actual");

                    b.Property<decimal?>("phy_improvement_actual_secondary");

                    b.Property<decimal?>("phy_improvement_target");

                    b.Property<decimal?>("phy_improvement_target_secondary");

                    b.Property<decimal?>("phy_purchase_actual");

                    b.Property<decimal?>("phy_purchase_actual_secondary");

                    b.Property<decimal?>("phy_purchase_target");

                    b.Property<decimal?>("phy_purchase_target_secondary");

                    b.Property<decimal?>("phy_rehabilitation_actual");

                    b.Property<decimal?>("phy_rehabilitation_actual_secondary");

                    b.Property<decimal?>("phy_rehabilitation_target");

                    b.Property<decimal?>("phy_rehabilitation_target_secondary");

                    b.Property<int?>("physical_status_category_id");

                    b.Property<int>("physical_status_id");

                    b.Property<DateTime?>("pims_mibf_date");

                    b.Property<double?>("pims_mibf_grant_cost");

                    b.Property<double?>("pims_mibf_lcc_cost");

                    b.Property<string>("pims_mibf_project_mame");

                    b.Property<string>("pow_list");

                    b.Property<string>("project_sequence");

                    b.Property<int>("project_type_id");

                    b.Property<int>("prov_code");

                    b.Property<int?>("push_status_id");

                    b.Property<bool?>("quit_claim");

                    b.Property<int>("region_code");

                    b.Property<int?>("replaced_sub_project_id");

                    b.Property<string>("resume_order_list");

                    b.Property<string>("rfr_status");

                    b.Property<bool?>("right_of_way_agreement");

                    b.Property<string>("s_phy_construction_actual");

                    b.Property<string>("s_phy_construction_actual_secondary");

                    b.Property<string>("s_phy_construction_target");

                    b.Property<string>("s_phy_construction_target_secondary");

                    b.Property<string>("s_phy_improvement_actual");

                    b.Property<string>("s_phy_improvement_actual_secondary");

                    b.Property<string>("s_phy_improvement_target");

                    b.Property<string>("s_phy_improvement_target_secondary");

                    b.Property<string>("s_phy_purchase_actual");

                    b.Property<string>("s_phy_purchase_actual_secondary");

                    b.Property<string>("s_phy_purchase_target");

                    b.Property<string>("s_phy_purchase_target_secondary");

                    b.Property<string>("s_phy_rehabilitation_actual");

                    b.Property<string>("s_phy_rehabilitation_actual_secondary");

                    b.Property<string>("s_phy_rehabilitation_target");

                    b.Property<string>("s_phy_rehabilitation_target_secondary");

                    b.Property<DateTime?>("sc_cno_date");

                    b.Property<DateTime?>("sc_cp_date");

                    b.Property<DateTime?>("set_date_eval");

                    b.Property<double?>("set_financial");

                    b.Property<double?>("set_institutional_linkage");

                    b.Property<int?>("set_mode");

                    b.Property<double?>("set_onm_group");

                    b.Property<double?>("set_organization");

                    b.Property<double?>("set_physical");

                    b.Property<string>("set_physical_description");

                    b.Property<double?>("set_sp_utilization");

                    b.Property<double?>("set_total_score");

                    b.Property<int>("sub_project_id");

                    b.Property<string>("sub_project_name")
                        .IsRequired();

                    b.Property<string>("suspension_order_list");

                    b.Property<int?>("target_female");

                    b.Property<int?>("target_male");

                    b.Property<decimal?>("target_tranching_first");

                    b.Property<decimal?>("target_tranching_second");

                    b.Property<decimal?>("target_tranching_third");

                    b.Property<int?>("training_category_id");

                    b.Property<DateTime?>("validation_conducted_date");

                    b.Property<bool?>("validation_status_others");

                    b.Property<string>("validation_status_others_input");

                    b.Property<DateTime?>("variation_actual_date_completion");

                    b.Property<DateTime?>("variation_date_started");

                    b.Property<string>("variation_order_list");

                    b.Property<double?>("variation_phy_perc_to_date");

                    b.Property<int?>("variation_physical_status_id");

                    b.Property<DateTime?>("variation_target_date_completion");

                    b.Property<bool?>("with_cadt");

                    b.Property<bool?>("with_cert_non_overlap");

                    b.Property<bool?>("with_cert_precondition");

                    b.Property<bool?>("with_tariff");

                    b.Property<bool?>("within_env_crit_area");

                    b.Property<int?>("year_of_implementation");

                    b.HasKey("sub_project_unique_id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("city_code");

                    b.HasIndex("cycle_id");

                    b.HasIndex("physical_status_category_id");

                    b.HasIndex("physical_status_id");

                    b.HasIndex("project_type_id");

                    b.HasIndex("prov_code");

                    b.HasIndex("region_code");

                    b.ToTable("sub_project");
                });

            modelBuilder.Entity("DeskApp.DataLayer.sub_project_coverage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Selected");

                    b.Property<int>("brgy_code");

                    b.Property<string>("brgy_name");

                    b.Property<int>("push_status_id");

                    b.Property<int>("sub_project_id");

                    b.Property<Guid?>("sub_project_unique_id");

                    b.HasKey("id");

                    b.HasIndex("brgy_code");

                    b.HasIndex("sub_project_unique_id");

                    b.ToTable("sub_project_coverage");
                });

            modelBuilder.Entity("DeskApp.DataLayer.sub_project_ers", b =>
                {
                    b.Property<Guid>("sub_project_ers_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<string>("brgy_code");

                    b.Property<int>("city_code");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int>("cycle_id");

                    b.Property<DateTime?>("date_ended");

                    b.Property<DateTime?>("date_reported");

                    b.Property<DateTime?>("date_started");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<int>("ers_delivery_mode_id");

                    b.Property<int>("fund_source_id");

                    b.Property<bool?>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("old_id");

                    b.Property<int>("prov_code");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<int>("region_code");

                    b.Property<int?>("sub_project_id");

                    b.Property<Guid?>("sub_project_unique_id");

                    b.HasKey("sub_project_ers_id");

                    b.HasIndex("approval_id");

                    b.HasIndex("ers_delivery_mode_id");

                    b.ToTable("sub_project_ers");
                });

            modelBuilder.Entity("DeskApp.DataLayer.sub_project_set", b =>
                {
                    b.Property<Guid>("sub_project_set_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("approval_id");

                    b.Property<int>("created_by");

                    b.Property<DateTime>("created_date");

                    b.Property<int?>("deleted_by");

                    b.Property<DateTime?>("deleted_date");

                    b.Property<bool>("is_deleted");

                    b.Property<int?>("last_modified_by");

                    b.Property<DateTime?>("last_modified_date");

                    b.Property<string>("old_id");

                    b.Property<DateTime?>("push_date");

                    b.Property<int>("push_status_id");

                    b.Property<DateTime?>("set_date_eval");

                    b.Property<double?>("set_financial");

                    b.Property<double?>("set_institutional_linkage");

                    b.Property<double?>("set_organization");

                    b.Property<double?>("set_physical");

                    b.Property<string>("set_physical_description");

                    b.Property<double?>("set_sp_utilization");

                    b.Property<double?>("set_total_score");

                    b.Property<int?>("sub_project_id");

                    b.Property<Guid?>("sub_project_unique_id");

                    b.HasKey("sub_project_set_id");

                    b.ToTable("sub_project_set");
                });

            modelBuilder.Entity("DeskApp.DataLayer.table_name", b =>
                {
                    b.Property<int>("table_name_id");

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.HasKey("table_name_id");

                    b.ToTable("table_name");
                });

            modelBuilder.Entity("DeskApp.DataLayer.talakayan_year", b =>
                {
                    b.Property<int>("talakayan_yr_id");

                    b.Property<string>("name");

                    b.HasKey("talakayan_yr_id");

                    b.ToTable("talakayan_year");
                });

            modelBuilder.Entity("DeskApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_assembly", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_barangay_assembly_purpose", "lib_barangay_assembly_purpose")
                        .WithMany()
                        .HasForeignKey("barangay_assembly_purpose_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_assembly_ip", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.brgy_assembly", "brgy_assembly")
                        .WithMany()
                        .HasForeignKey("brgy_assembly_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_ip_group", "lib_ip_group")
                        .WithMany()
                        .HasForeignKey("ip_group_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_eca", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_eca_type", "lib_eca_type")
                        .WithMany()
                        .HasForeignKey("eca_type_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_profile", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.brgy_profile_ip", b =>
                {
                    b.HasOne("DeskApp.DataLayer.brgy_profile", "brgy_profile")
                        .WithMany()
                        .HasForeignKey("brgy_profile_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_ip_group", "lib_ip_group")
                        .WithMany()
                        .HasForeignKey("ip_group_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.ceac_list", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code");

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.ceac_tracking", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code");

                    b.HasOne("DeskApp.DataLayer.ceac_list", "ceac_list")
                        .WithMany()
                        .HasForeignKey("ceac_list_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_implementation_status", "lib_implementation_status")
                        .WithMany()
                        .HasForeignKey("implementation_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_lgu_level", "lib_lgu_level")
                        .WithMany()
                        .HasForeignKey("lgu_level_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_training_category", "lib_training_category")
                        .WithMany()
                        .HasForeignKey("training_category_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.community_training", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code");

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_lgu_level", "lib_lgu_level")
                        .WithMany()
                        .HasForeignKey("lgu_level_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_training_category", "lib_training_category")
                        .WithMany()
                        .HasForeignKey("training_category_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.attached_document", b =>
                {
                    b.HasOne("DeskApp.DataLayer.Entities.mov_list", "mov_list")
                        .WithMany()
                        .HasForeignKey("mov_list_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.community_organization", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code");

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_lgu_level", "lib_lgu_level")
                        .WithMany()
                        .HasForeignKey("lgu_level_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.diaster_coverage", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.Entities.disaster", "disaster")
                        .WithMany()
                        .HasForeignKey("disaster_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.disaster", b =>
                {
                    b.HasOne("DeskApp.DataLayer.Entities.lib_disaster_type", "lib_disaster_type")
                        .WithMany()
                        .HasForeignKey("disaster_type_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.file_attachment", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.Entities.means_of_verification", "means_of_verification")
                        .WithMany()
                        .HasForeignKey("means_of_verification_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.means_of_verification", b =>
                {
                    b.HasOne("DeskApp.DataLayer.table_name", "table_name")
                        .WithMany()
                        .HasForeignKey("table_name_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.sub_project_post_disaster", b =>
                {
                    b.HasOne("DeskApp.DataLayer.Entities.lib_condition", "lib_condition")
                        .WithMany()
                        .HasForeignKey("condition_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.Entities.disaster", "disaster")
                        .WithMany()
                        .HasForeignKey("disaster_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Entities.sub_project_pre_disaster", b =>
                {
                    b.HasOne("DeskApp.DataLayer.Entities.lib_condition", "lib_condition")
                        .WithMany()
                        .HasForeignKey("condition_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.dof_blgf_financial_data", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.lgpms_data", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.mlgu_financial_data", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id");

                    b.HasOne("DeskApp.DataLayer.Eval.dof_blgf_financial_data", "dof_blgf_financial_data")
                        .WithMany()
                        .HasForeignKey("dof_blgf_financial_data_record_id");

                    b.HasOne("DeskApp.DataLayer.Eval.lgpms_data", "lgpms_data")
                        .WithMany()
                        .HasForeignKey("lgpms_data_id");

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.talakayan_year", "talakayan_year")
                        .WithMany()
                        .HasForeignKey("talakayan_yr_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.muni_financial_profile", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id");

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.perception_survey", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id");

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.Eval.talakayan_eval", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.grievance_record", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code");

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id");

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id");

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id");

                    b.HasOne("DeskApp.DataLayer.lib_grs_category", "lib_grs_category")
                        .WithMany()
                        .HasForeignKey("grs_category_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_grs_complaint_subject", "lib_grs_complaint_subject")
                        .WithMany()
                        .HasForeignKey("grs_complaint_subject_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_grs_feedback", "lib_grs_feedback")
                        .WithMany()
                        .HasForeignKey("grs_feedback_id");

                    b.HasOne("DeskApp.DataLayer.lib_grs_filling_mode", "lib_grs_filling_mode")
                        .WithMany()
                        .HasForeignKey("grs_filling_mode_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_grs_form", "lib_grs_form")
                        .WithMany()
                        .HasForeignKey("grs_form_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_grs_intake_level", "lib_grs_intake_level")
                        .WithMany()
                        .HasForeignKey("grs_intake_level_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_grs_intake_officer", "lib_grs_intake_officer")
                        .WithMany()
                        .HasForeignKey("grs_intake_officer_id");

                    b.HasOne("DeskApp.DataLayer.lib_grs_nature", "lib_grs_nature")
                        .WithMany()
                        .HasForeignKey("grs_nature_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_grs_pincos_actor", "lib_grs_pincos_actor")
                        .WithMany()
                        .HasForeignKey("grs_pincos_actor_id");

                    b.HasOne("DeskApp.DataLayer.lib_grs_resolution_status", "lib_grs_resolution_status")
                        .WithMany()
                        .HasForeignKey("grs_resolution_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_grs_sender_designation", "lib_grs_sender_designation")
                        .WithMany()
                        .HasForeignKey("grs_sender_designation_id");

                    b.HasOne("DeskApp.DataLayer.lib_ip_group", "lib_ip_group")
                        .WithMany()
                        .HasForeignKey("ip_group_id");

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_sex", "lib_sex")
                        .WithMany()
                        .HasForeignKey("sex_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_training_category", "lib_training_category")
                        .WithMany()
                        .HasForeignKey("training_category_id");
                });

            modelBuilder.Entity("DeskApp.DataLayer.grievance_record_discussion", b =>
                {
                    b.HasOne("DeskApp.DataLayer.grievance_record", "grievance_record")
                        .WithMany("grievance_record_discussions")
                        .HasForeignKey("grievance_record_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.grs_installation", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code");

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_lgu_level", "lib_lgu_level")
                        .WithMany()
                        .HasForeignKey("lgu_level_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_cycle", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.lib_project_type", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_major_classification", "lib_major_classification")
                        .WithMany()
                        .HasForeignKey("major_classification_id");
                });

            modelBuilder.Entity("DeskApp.DataLayer.mibf_criteria", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.community_training", "community_training")
                        .WithMany()
                        .HasForeignKey("community_training_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.mibf_prioritization", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.community_training", "community_training")
                        .WithMany()
                        .HasForeignKey("community_training_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_project_type", "lib_project_type")
                        .WithMany()
                        .HasForeignKey("project_type_id");

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.muni_profile", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.muni_profile_fund_use", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.muni_profile", "muni_profile")
                        .WithMany()
                        .HasForeignKey("muni_profile_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.muni_profile_income", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_source_income", "lib_source_income")
                        .WithMany()
                        .HasForeignKey("lib_source_incomesource_income_id");

                    b.HasOne("DeskApp.DataLayer.muni_profile", "muni_profile")
                        .WithMany()
                        .HasForeignKey("muni_profile_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.municipal_lcc", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.municipal_pta", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.oversight_committee", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_ers_work", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_ers_current_work", "lib_ers_current_work")
                        .WithMany()
                        .HasForeignKey("ers_current_work_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.person_profile", "person_profile")
                        .WithMany()
                        .HasForeignKey("person_profile_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.sub_project_ers", "sub_project_ers")
                        .WithMany()
                        .HasForeignKey("sub_project_ers_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_profile", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code");

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_city")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_civil_status", "lib_civil_status")
                        .WithMany()
                        .HasForeignKey("civil_status_id");

                    b.HasOne("DeskApp.DataLayer.lib_education_attainment", "lib_education_attainment")
                        .WithMany()
                        .HasForeignKey("education_attainment_id");

                    b.HasOne("DeskApp.DataLayer.lib_ip_group", "lib_ip_group")
                        .WithMany()
                        .HasForeignKey("ip_group_id");

                    b.HasOne("DeskApp.DataLayer.lib_lgu_position", "lib_blgu_position")
                        .WithMany()
                        .HasForeignKey("lgu_position_id");

                    b.HasOne("DeskApp.DataLayer.lib_occupation", "lib_occupation")
                        .WithMany()
                        .HasForeignKey("occupation_id");

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_province")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_region")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_training", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.community_training", "community_training")
                        .WithMany()
                        .HasForeignKey("community_training_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.person_profile", "person_profile")
                        .WithMany()
                        .HasForeignKey("person_profile_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_push_status", "lib_push_status")
                        .WithMany()
                        .HasForeignKey("push_status_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.person_volunteer_record", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_enrollment", "lib_enrollment")
                        .WithMany()
                        .HasForeignKey("enrollment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_fund_source", "lib_fund_source")
                        .WithMany()
                        .HasForeignKey("fund_source_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.person_profile", "person_profile")
                        .WithMany()
                        .HasForeignKey("person_profile_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_volunteer_committee", "lib_volunteer_committee")
                        .WithMany()
                        .HasForeignKey("volunteer_committee_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_volunteer_committee_position", "lib_volunteer_committee_position")
                        .WithMany()
                        .HasForeignKey("volunteer_committee_position_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.psa_problem", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.community_training", "community_training")
                        .WithMany()
                        .HasForeignKey("community_training_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_psa_problem_category", "lib_psa_problem_category")
                        .WithMany()
                        .HasForeignKey("psa_problem_category_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.psa_solution", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_psa_solution_category", "lib_psa_solution_category")
                        .WithMany()
                        .HasForeignKey("psa_solution_category_id");
                });

            modelBuilder.Entity("DeskApp.DataLayer.sub_project", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_city", "lib_cities")
                        .WithMany()
                        .HasForeignKey("city_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_cycle", "lib_cycle")
                        .WithMany()
                        .HasForeignKey("cycle_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_physical_status_category", "lib_physical_status_category")
                        .WithMany()
                        .HasForeignKey("physical_status_category_id");

                    b.HasOne("DeskApp.DataLayer.lib_physical_status", "lib_physical_status")
                        .WithMany()
                        .HasForeignKey("physical_status_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_project_type", "lib_project_type")
                        .WithMany()
                        .HasForeignKey("project_type_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_province", "lib_provinces")
                        .WithMany()
                        .HasForeignKey("prov_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_region", "lib_regions")
                        .WithMany()
                        .HasForeignKey("region_code")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeskApp.DataLayer.sub_project_coverage", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_brgy", "lib_brgy")
                        .WithMany()
                        .HasForeignKey("brgy_code")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.sub_project", "sub_project")
                        .WithMany()
                        .HasForeignKey("sub_project_unique_id");
                });

            modelBuilder.Entity("DeskApp.DataLayer.sub_project_ers", b =>
                {
                    b.HasOne("DeskApp.DataLayer.lib_approval", "lib_approval")
                        .WithMany()
                        .HasForeignKey("approval_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.DataLayer.lib_ers_delivery_mode", "lib_ers_delivery_mode")
                        .WithMany()
                        .HasForeignKey("ers_delivery_mode_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DeskApp.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DeskApp.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeskApp.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
