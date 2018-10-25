using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class sub_project_spcr_mapping
    {

        public Guid sub_project_spcr_id { get; set; }
        /// <summary>
        /// Old Sub Project Id, mus be matched with SPI Profile to get Fund Source and Cycle, Brgy, ETC
        /// </summary>
        public string old_id { get; set; }


        public int? sub_project_id { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }

        public int city_code { get; set; }

        public string brgy_code { get; set; }

        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        public int enrollment_id { get; set; }

        public DateTime? date_csw { get; set; }
        public DateTime? date_prioritization { get; set; }
        public DateTime? date_inaugurate { get; set; }
        public int? no_households { get; set; }
        public int? no_families { get; set; }
        public int? no_pantawid_hh { get; set; }
        public int? no_pantawid_fm { get; set; }
        public float? phys_actual { get; set; }
        public float? total_cost { get; set; }
        public float? total_direct { get; set; }
        public float? total_indirect { get; set; }
        public int? no_male_pop { get; set; }
        public int? no_female_pop { get; set; }
        public int? no_male_serve { get; set; }
        public int? no_female_serve { get; set; }
        public int? no_male_ip { get; set; }
        public int? no_female_ip { get; set; }
        public int? no_slp_hh { get; set; }
        public int? no_slp_family { get; set; }
        public int? no_ip_hh { get; set; }
        public int? no_ip_hh_families { get; set; }
        public int procurement_mode_id { get; set; }
        public float? total_counterpart { get; set; }
        public int? no_skilled_male { get; set; }
        public int? no_skilled_female { get; set; }
        public int? no_unskilled_male { get; set; }
        public int? no_unskilled_female { get; set; }
        public int? days_skilled_male { get; set; }
        public int? days_skilled_female { get; set; }
        public int? days_unskilled_male { get; set; }
        public int? days_unskilled_female { get; set; }
        public float? rate_skilled_male { get; set; }
        public float? rate_skilled_female { get; set; }
        public float? rate_unskilled_male { get; set; }
        public float? rate_unskilled_female { get; set; }
        public int completion_status_id { get; set; }


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
