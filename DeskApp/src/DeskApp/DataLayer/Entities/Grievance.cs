using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class table_name
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int table_name_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }


    }
    //public class pincos
    //{
    //    [Key]
    //    public int pincos_id { get; set; }
    //    public int? table_name_id { get; set; }
    //    public int? grievance_intake_id { get; set; }
    //    public int? pinco_reference_activity_id { get; set; }
    //    public bool? is_transferred_to_grievance_intake { get; set; }
    //    public string complainant_first_name { get; set; }
    //    public string complainant_last_name { get; set; }
    //    public string complainant_middle_name { get; set; }
    //    public string activity_id { get; set; }
    //    public int old_issue_no { get; set; }
    //    public string raised_by { get; set; }

    //    public string action { get; set; }
    //    public string issue { get; set; }

    //    #region Audit
    //    public int created_by { get; set; }
    //    public DateTime created_date { get; set; }
    //    public int? last_modified_by { get; set; }
    //    public DateTime? last_modified_date { get; set; }

    //    public bool? is_deleted { get; set; }
    //    public int? deleted_by { get; set; }
    //    public DateTime? deleted_date { get; set; }


    //    #endregion


    //    public int region_code { get; set; }
    //    public string prov_code { get; set; }
    //    public int city_code { get; set; }
    //    public string brgy_code { get; set; }
    //    public virtual table_name table_name { get; set; }
    //}

    public class lib_grs_nature
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_nature_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }

        public int? return_id { get; set; }
    }
    public class lib_grs_feedback
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_feedback_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }
    public class lib_grs_intake_level
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_intake_level_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
        public int? office_level_id { get; set; }
    }
    public class lib_grs_form
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_form_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }
    public class lib_grs_filling_mode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_filling_mode_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }

    public class lib_grs_resolution_status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_resolution_status_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }
    public class lib_grs_complainant_position
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_complainant_position_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }


    public class lib_grs_category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_category_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }

    public class lib_grs_complaint_subject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_complaint_subject_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }

    public class lib_ip_group
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ip_group_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }

    }



    public class lib_grs_sender_designation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_sender_designation_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int? return_id { get; set; }
    }


     


    public class lib_sex
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sex_id { get; set; }
        public string name { get; set; }
        public string desktop_value { get; set; }


    }

    public class lib_grs_intake_officer
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int grs_intake_officer_id { get; set; }
        public string name { get; set; }
        public bool? is_active { get; set; }
        public int? sort_order { get; set; }
        public int office_level_id { get; set; }

    }
}
