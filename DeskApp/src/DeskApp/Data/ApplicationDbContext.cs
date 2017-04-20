using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeskApp.Models;

using DeskApp.DataLayer;
using DeskApp.DataLayer.Entities;


using Microsoft.Extensions.PlatformAbstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DeskApp.DataLayer.Eval;

namespace DeskApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //private static string path01 = PlatformServices.Default.Application.ApplicationBasePath;

        //string source = @"Data Source = " + path01 + @"\" + "integerdatabase.sqlite";


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        #region builder
        /// <summary>
        /// Place inside BuilderRepository
        /// </summary>

            public DbSet<community_organization> community_organization { get; set; }

        #endregion

        #region Evaluation
        public DbSet<perception_survey> perception_survey { get; set; }
        public DbSet<talakayan_eval> talakayan_eval { get; set; }
        public DbSet<talakayan_year> talakayan_year { get; set; }

        public DbSet<dof_blgf_financial_data> dof_blgf_financial_data { get; set; }
        public DbSet<mlgu_financial_data> mlgu_financial_data { get; set; }
        public DbSet<lgpms_data> lgpms_data { get; set; }

        public DbSet<municipal_financial_profile> municipal_financial_profile { get; set; }

        public DbSet<muni_financial_profile> muni_financial_profile { get; set; }

        #endregion

        //public DbSet<DownloadLog> DownloadLog { get; set; }

        public DbSet<attached_document> attached_document { get; set; }


        public DbSet<oversight_committee> oversight_committee { get; set; }
        public DbSet<municipal_pta> municipal_pta { get; set; }
        public DbSet<municipal_lcc> municipal_lcc { get; set; }

        public DbSet<lib_condition> lib_condition { get; set; }
        public DbSet<report_list> report_list { get; set; }
        public DbSet<mov_list> mov_list { get; set; }
        public DbSet<disaster> disaster { get; set; }
        public DbSet<lib_disaster_type> lib_disaster_type { get; set; }
        public DbSet<diaster_coverage> diaster_coverage { get; set; }
        public DbSet<sub_project_post_disaster> sub_project_post_disaster { get; set; }
        public DbSet<sub_project_pre_disaster> sub_project_pre_disaster { get; set; }



        public DbSet<lib_source_income> lib_source_income { get; set; }

        //       public DbSet<ncddp_grouping> ncddp_grouping { get; set; }
        public DbSet<lib_transpo_mode> lib_transpo_mode { get; set; }

        public DbSet<grievance_record> grievance_record { get; set; }
        public DbSet<grievance_record_discussion> grievance_record_discussion { get; set; }

        #region Library
        public DbSet<lib_psa_problem_category> lib_psa_problem_category { get; set; }
        public DbSet<lib_psa_solution_category> lib_psa_solution_category { get; set; }
        public DbSet<lib_region> lib_region { get; set; }
        public DbSet<lib_province> lib_province { get; set; }
        public DbSet<lib_city> lib_city { get; set; }
        public DbSet<lib_brgy> lib_brgy { get; set; }
        public DbSet<lib_cycle> lib_cycle { get; set; }
        public DbSet<lib_enrollment> lib_enrollment { get; set; }
        public DbSet<lib_approval> lib_approval { get; set; }

        public DbSet<lib_fund_source> lib_fund_source { get; set; }

        public DbSet<lib_barangay_assembly_purpose> lib_barangay_assembly_purpose { get; set; }

        public DbSet<lib_push_status> lib_push_status { get; set; }
        #endregion

        #region Barangay Data
        public DbSet<brgy_profile> brgy_profile { get; set; }
        public DbSet<brgy_profile_ip> brgy_profile_ip { get; set; }
        public DbSet<brgy_eca> brgy_eca { get; set; }
        public DbSet<lib_eca_type> lib_eca_type { get; set; }



        public DbSet<brgy_assembly> brgy_assembly { get; set; }
        public DbSet<brgy_assembly_ip> brgy_assembly_ip { get; set; }

        //public DbSet<barangay_profile> barangay_profile { get; set; }

        //public DbSet<barangay_fund_source> barangay_fund_source { get; set; }
        //public DbSet<barangay_facility> barangay_facility { get; set; }
        //public DbSet<lib_barangay_fund_source_list> lib_barangay_fund_source_list { get; set; }
        //public DbSet<lib_barangay_facility_list> lib_barangay_facility_list { get; set; }
        #endregion

        #region Beneficiary
        public DbSet<lib_education_attainment> lib_education_attainment { get; set; }
        public DbSet<lib_lgu_position> lib_lgu_position { get; set; }
        public DbSet<lib_occupation> lib_occupation { get; set; }

        public DbSet<lib_civil_status> lib_civil_status { get; set; }
        public DbSet<lib_sector> lib_sector { get; set; }

        public DbSet<person_profile> person_profile { get; set; }
        public DbSet<person_training> person_training { get; set; }
        public DbSet<person_ers_work> person_ers_work { get; set; }
        public DbSet<person_volunteer_record> person_volunteer_record { get; set; }

        #endregion

        #region LGU Data
        public DbSet<lib_lgu_level> lib_lgu_level { get; set; }

        public DbSet<lib_kalahi_committee> lib_kalahi_committee { get; set; }
        public DbSet<lib_organization> lib_organization { get; set; }

        public DbSet<lib_ip_group> lib_ip_group { get; set; }
        #endregion

        #region Training
        public DbSet<lib_training_category> lib_training_category { get; set; }
        public DbSet<community_training> community_training { get; set; }

        public DbSet<psa_problem> psa_problem { get; set; }
        public DbSet<psa_solution> psa_solution { get; set; }

        public DbSet<mibf_criteria> mibf_criteria { get; set; }
        public DbSet<mibf_prioritization> mibf_prioritization { get; set; }
        //public DbSet<training> training { get; set; }
        //public DbSet<training_participant> training_participant { get; set; }

        //public DbSet<lib_training_instructor_designation> lib_training_instructor_designation { get; set; }
        //public DbSet<training_instructor> training_instructor { get; set; }
        #endregion


        #region Volunteer
        //public DbSet<volunteer_record> volunteer_record { get; set; }
        //public DbSet<volunteer> volunteer { get; set; }
        //public DbSet<volunteer_committee_membership> volunteer_committee_membership { get; set; }
        public DbSet<lib_volunteer_committee> lib_volunteer_committee { get; set; }
        public DbSet<lib_volunteer_committee_position> lib_volunteer_committee_position { get; set; }
        #endregion

        #region Grievance
        // public DbSet<pincos> pincos { get; set; }
        //      public DbSet<lgu_activity_monitoring> lgu_activity_monitoring { get; set; }
        public DbSet<lib_grs_feedback> lib_grs_feedback { get; set; }
        // public DbSet<grievance_intake> grievance_intake { get; set; }

        //    public DbSet<grievance_intake_discussion> grievance_intake_discussion { get; set; }
        public DbSet<lib_grs_intake_level> lib_grs_intake_level { get; set; }
        public DbSet<lib_grs_form> lib_grs_form { get; set; }
        public DbSet<lib_grs_filling_mode> lib_grs_filling_mode { get; set; }
        public DbSet<lib_grs_complainant_position> lib_grs_complainant_position { get; set; }
        public DbSet<lib_grs_resolution_status> lib_grs_resolution_status { get; set; }
        public DbSet<lib_grs_nature> lib_grs_nature { get; set; }
        public DbSet<lib_grs_complaint_subject> lib_grs_complaint_subject { get; set; }

        public DbSet<lib_grs_intake_officer> lib_grs_intake_officer { get; set; }
        public DbSet<lib_grs_category> lib_grs_category { get; set; }

        public DbSet<lib_sex> lib_sex { get; set; }
        //installation
        //public DbSet<grievance_installation> grievance_installation { get; set; }
        //public DbSet<lib_grs_installation_level> lib_grs_installation_level { get; set; }

        public DbSet<lib_grs_sender_designation> lib_grs_sender_designation { get; set; }


        public DbSet<grs_installation> grs_installation { get; set; }
        #endregion


        #region CEAC

        public DbSet<lib_implementation_status> lib_implementation_status { get; set; }
        public DbSet<ceac_tracking> ceac_tracking { get; set; }

        public DbSet<ceac_list> ceac_list { get; set; }

        public DbSet<act_report_other_activities> act_report_other_activities { get; set; }
        #endregion


        #region SPI
        //public DbSet<lib_spi_nature_work> lib_spi_nature_work { get; set; }
        //public DbSet<spi_worker> spi_worker { get; set; }
        //// public DbSet<spi_project_worker> spi_project_worker { get; set; }
        //public DbSet<spi_profile> spi_profile { get; set; }


        #endregion

        public DbSet<lib_ers_delivery_mode> lib_ers_delivery_mode { get; set; }
        public DbSet<lib_ers_current_work> lib_ers_current_work { get; set; }
        //public DbSet<spi_ers_record> spi_ers_record { get; set; }
        //public DbSet<spi_ers_list> spi_ers_list { get; set; }

        #region ERS

        #endregion

        #region Geotagging

        public DbSet<SPPhoto> SPPhoto { get; set; }
        public DbSet<sub_project> sub_project { get; set; }
        public DbSet<sub_project_ers> sub_project_ers { get; set; }
        public DbSet<sub_project_set> sub_project_set { get; set; }
        public DbSet<sub_project_coverage> sub_project_coverage { get; set; }
        public DbSet<lib_physical_status_category> lib_physical_status_category { get; set; }
        public DbSet<lib_project_type> lib_project_type { get; set; }
        public DbSet<lib_major_classification> lib_major_classification { get; set; }
        public DbSet<lib_physical_status> lib_physical_status { get; set; }
        //public DbSet<sub_project_physical_target> sub_project_physical_target { get; set; }
        //public DbSet<lib_project_type_measure> lib_project_type_measure { get; set; }
        //public DbSet<lib_physical_target_description> lib_physical_target_description { get; set; }
        //public DbSet<lib_measure> lib_measure { get; set; }
        #endregion


        #region Municipal Data

        //public DbSet<lib_municipal_forum_purpose> lib_municipal_forum_purpose { get; set; }
        //public DbSet<municipal_forum> municipal_forum { get; set; }

        //public DbSet<municipal_forum_criteria> municipal_forum_criteria { get; set; }
        //public DbSet<municipal_lgu> municipal_lgu { get; set; }

        public DbSet<muni_profile> muni_profile { get; set; }
        public DbSet<muni_profile_income> muni_profile_income { get; set; }
        public DbSet<muni_profile_fund_use> muni_profile_fund_use { get; set; }

        #endregion

        #region External Data

        //public DbSet<pppp_barangay_summary> pppp_barangay_summary { get; set; }
        public DbSet<erfr_sub_project> erfr_sub_project { get; set; }
        #endregion
        public DbSet<table_name> table_name { get; set; }

        public DbSet<file_attachment> file_attachment { get; set; }
        public DbSet<means_of_verification> means_of_verification { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}
        //protected override void OnModelCreating(DbModelBuilder builder)
        //{
        //    builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        //    builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


        //    builder.Entity<IdentityUserLogin>().HasKey(u => new
        //    {
        //        u.UserId

        //    });

        //    builder.Entity<IdentityUserRole>().HasKey(u => new
        //    {
        //        u.UserId,
        //        u.RoleId

        //    });

        //    //  base.OnModelCreating(builder);
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

         
        }
    }
}
