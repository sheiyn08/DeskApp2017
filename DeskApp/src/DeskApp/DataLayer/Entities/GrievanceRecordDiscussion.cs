using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskApp.DataLayer
{
    public class grievance_record_discussion
    {
        [Key]
        public Guid grievance_record_discussion_id { get; set; }
        public string remarks { get; set; }

        public string position { get; set; }
        public Guid grievance_record_id { get; set; }
        [JsonIgnore]
        public virtual grievance_record grievance_record { get; set; }


        #region Audit
        public string created_by_name { get; set; }
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
        public int? last_sync_source_id { get; set; }
    }
}
