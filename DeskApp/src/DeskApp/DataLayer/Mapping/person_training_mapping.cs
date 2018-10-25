using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
     
     public class person_training_mapping
    {
        
        public Guid person_training_id { get; set; }
        public Guid person_profile_id { get; set; }
    
        public Guid community_training_id { get; set; }
     
        public bool? is_participant { get; set; }



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
