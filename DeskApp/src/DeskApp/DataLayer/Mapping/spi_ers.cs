using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class sub_project_ers_mapping
    {

        public Guid sub_project_ers_id { get; set; }
        public string old_id { get; set; }
        public int? sub_project_id { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public string brgy_code { get; set; }



        public int ers_delivery_mode_id { get; set; }

        public DateTime? date_reported { get; set; }
        public DateTime? date_started { get; set; }
        public DateTime? date_ended { get; set; }


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
