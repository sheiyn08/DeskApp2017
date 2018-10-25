using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace DeskApp.DataLayer
{
    public class person_volunteer_record
    {
        [Key]
        public Guid person_volunteer_record_id { get; set; }
        public Guid person_profile_id { get; set; }
        public int volunteer_committee_id { get; set; }
        public int volunteer_committee_position_id { get; set; }

        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int enrollment_id { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public virtual lib_volunteer_committee lib_volunteer_committee { get; set; }
        public virtual lib_volunteer_committee_position lib_volunteer_committee_position { get; set; }
        public virtual person_profile person_profile { get; set; }
        public virtual lib_fund_source lib_fund_source { get; set; }
        public virtual lib_cycle lib_cycle { get; set; }
        public virtual lib_enrollment lib_enrollment { get; set; }

  

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
        public virtual lib_approval lib_approval { get; set; }
        #endregion
    }

    public class person_volunteer_record_mapping
    {
        [Key]
        public Guid person_volunteer_record_id { get; set; }
        public Guid person_profile_id { get; set; }
        public int volunteer_committee_id { get; set; }
        public int volunteer_committee_position_id { get; set; }

        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int enrollment_id { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
 



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
