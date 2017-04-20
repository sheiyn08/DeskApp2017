using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class mibf_criteria_mapping
    {
      
        public Guid mibf_criteria_id { get; set; }

        public Guid community_training_id { get; set; }
     

        public string old_id { get; set; }


        public string criteria { get; set; }
        public double? rate { get; set; }



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

    public class mibf_prioritization_mapping
    {
       
        public Guid mibf_prioritization_id { get; set; }

        public Guid community_training_id { get; set; }
       

        public string old_id { get; set; }

        public int? rank { get; set; }
        public string project_name { get; set; }
        public int? priority { get; set; }
        public double? kc_amount { get; set; }
        public double? lcc_amount { get; set; }
        public double? pamana_amount { get; set; }
        public int? type { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public string brgy_code { get; set; }
        public bool added_to_spi { get; set; }

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
