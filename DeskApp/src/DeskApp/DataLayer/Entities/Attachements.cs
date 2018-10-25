using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Entities
{
    public class file_attachment
    {
        [Key]
        public Guid file_attachment_id { get; set; }
        public Guid record_id { get; set; }
        public int means_of_verification_id { get; set; }
        public int count { get; set; }

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
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion


        [JsonIgnore]
        public means_of_verification means_of_verification { get; set; }
    }

    public class means_of_verification
    {
        [Key]
        public int means_of_verification_id { get; set; }
        public string name { get; set; }
        public int table_name_id { get; set; }
        public int max { get; set; }

        [JsonIgnore]
        public virtual table_name table_name { get; set; }
    }
}


