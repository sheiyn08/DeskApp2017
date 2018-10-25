using System;
using System.Collections.Generic;
using System.Linq;
 

namespace DeskApp.DataLayer
{
    public class brgy_assembly_mapping
    {

        public Guid brgy_assembly_id { get; set; }
    
        public string old_id { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public string brgy_code { get; set; }

        public string highlights { get; set; }
        public int barangay_assembly_purpose_id { get; set; }

        public int enrollment_id { get; set; }

        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }

        public int? no_families { get; set; }
        public int? no_household { get; set; }

        public int? no_atn_male { get; set; }
        public int? no_atn_female { get; set; }
        public int? no_ip_male { get; set; }
        public int? no_ip_female { get; set; }
        public int? no_old_male { get; set; }
        public int? no_old_female { get; set; }
        public int? no_pantawid_household { get; set; }
        public int? no_pantawid_family { get; set; }

        public int? no_slp_household { get; set; }
        public int? no_slp_family { get; set; }

        public int? no_ip_household { get; set; }
        public int? no_ip_family { get; set; }

        public int? total_household_in_barangay { get; set; }
        public int? total_families_in_barangay { get; set; }


        public int? no_lgu_male { get; set; }
        public int? no_lgu_female { get; set; }


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