using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class community_training_mapping
    {
     
        public Guid community_training_id { get; set; }
        public string old_id { get; set; }
    


        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }




        public string training_title { get; set; }
        public int training_category_id { get; set; }
        public string venue { get; set; }
        public DateTime? date_conducted { get; set; }
        public int duration { get; set; }
        public int lgu_level_id { get; set; }

        public string reported_by { get; set; }

        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public bool is_draft { get; set; }
        public int enrollment_id { get; set; }


   

        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public string brgy_code { get; set; }
 
        #endregion

        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        #endregion

        #region Sync
        //used for offline sync purposes
 

        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
   
        #endregion

        
    }


    public class person_ers_work_mapping
    {
 
        public Guid person_ers_work_id { get; set; }
        public string old_id { get; set; }


        public Guid sub_project_ers_id { get; set; }
        public Guid person_profile_id { get; set; }




        public decimal? rate_hour { get; set; }
        public decimal? rate_day { get; set; }
        public decimal? work_hours { get; set; }
        public decimal? work_days { get; set; }
        public decimal? plan_cash { get; set; }
        public decimal? plan_lcc { get; set; }
        public decimal? actual_cash { get; set; }
        public decimal? actual_lcc { get; set; }

        public int ers_current_work_id { get; set; }

 

        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        #endregion

        #region Sync
        //used for offline sync purposes
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
       
        #endregion

    }

}
