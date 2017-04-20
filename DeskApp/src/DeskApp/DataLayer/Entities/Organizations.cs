using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Entities
{
    public class community_organization
    {
        [Key]
         
        public Guid community_organization_id { get; set; }
        public string old_id { get; set; }


        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
            public int enrollment_id { get; set; }
        public int lgu_level_id { get; set; }

        public string name { get; set; }
        public bool? is_onm { get; set; }

        public int organization_type_id { get; set; }
        public bool? is_formal { get; set; }
        public bool? is_lgu_accredited { get; set; }
        public string list_advocacy { get; set; }
        public string list_area_operation { get; set; }

        public int? years_operating { get; set; }
        public bool? is_active { get; set; }
        public string list_activities { get; set; }
        public int? no_male { get; set; }
        public int? no_female { get; set; }
        public int? no_ip_male { get; set; }
        public int? no_ip_female { get; set; }

        public bool? is_sector_academe { get; set; }
        public bool? is_sector_business { get; set; }
        public bool? is_sector_pwd { get; set; }
        public bool? is_sector_farmer { get; set; }
        public bool? is_sector_fisherfolks { get; set; }
        public bool? is_sector_government { get; set; }
        public bool? is_sector_ip { get; set; }
        public bool? is_sector_ngo { get; set; }
        public bool? is_sector_po { get; set; }
        public bool? is_sector_religios { get; set; }
        public bool? is_sector_senior { get; set; }
        public bool? is_sector_women { get; set; }
        public bool? is_sector_youth { get; set; }









        [JsonIgnore]
        public virtual lib_lgu_level lib_lgu_level { get; set; }
        //[JsonIgnore]
        //public virtual lib_enrollment lib_enrollment { get; set; }

        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }
        [JsonIgnore]

        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }

        [JsonIgnore]
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
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion
    }
}
