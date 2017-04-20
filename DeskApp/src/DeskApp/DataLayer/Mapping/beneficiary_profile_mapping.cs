//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace DeskApp.DataLayer
//{
//    public class person_profile_mapping
//    {

//        //   public int? person_identity_key { get; set; }
//        public int person_identity_key { get; set; }
      
//        public Guid person_profile_id { get; set; }
//        public string old_id { get; set; }
//        public string household_id { get; set; }
//        public int? entry_id { get; set; }
//        public int region_code { get; set; }
//        public int prov_code { get; set; }
//        public int city_code { get; set; }
//        public string brgy_code { get; set; }
//        public string old_address { get; set; }


//        public string last_name { get; set; }
//        public string first_name { get; set; }
//        public string middle_name { get; set; }
//        public DateTime? birthdate { get; set; }
//        public bool sex { get; set; }
//        public int? civil_status_id { get; set; }
//        public int? no_children { get; set; }

//        public string contact_no { get; set; }

//        public bool? is_volunteer { get; set; }
//        public bool? is_ip { get; set; }
//        public bool? is_ipleader { get; set; }
//        public bool? is_pantawid { get; set; }
//        public bool? is_slp { get; set; }
//        public bool? is_lguofficial { get; set; }
//        public bool? is_worker { get; set; }



//        public int? lgu_position_id { get; set; }
//        public int? education_attainment_id { get; set; }
//        public int? occupation_id { get; set; }
//        //  public int? sector { get; set; }
//        public string sector { get; set; }
     

//        #region Audit
//        public int created_by { get; set; }
//        public DateTime created_date { get; set; }
//        public int? last_modified_by { get; set; }
//        public DateTime? last_modified_date { get; set; }

//        public bool? is_deleted { get; set; }
//        public int? deleted_by { get; set; }
//        public DateTime? deleted_date { get; set; }


//        #endregion

//        #region Sync
//        //used for offline sync purposes
//        public int push_status_id { get; set; }
//        public DateTime? push_date { get; set; }
//        #endregion

//        #region Approval
//        public int approval_id { get; set; }
         
//        #endregion




//        public int? ip_group_id { get; set; }
        
//        public int? last_sync_source_id { get; set; }

//    }
//}