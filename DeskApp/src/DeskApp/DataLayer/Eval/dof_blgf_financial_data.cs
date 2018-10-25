using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Eval
{
    public class dof_blgf_financial_data : base_record
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dof_blgf_financial_data_record_id { get; set; }

        public int year_id { get; set; }
        public int psgc_code { get; set; }
        public float? locally_shared_revenues { get; set; }
        public float? ira_share { get; set; }
        public float? other_revenues_total { get; set; }
        public float? other_shares_natl_tax { get; set; }
        public float? inter_local_transfers { get; set; }
        public float? extraordinary_receipts { get; set; }
        public float? total_lgu_income { get; set; }
        public float? expenditures_gen_public_services { get; set; }
        public float? expenditures_educ_culture_etc { get; set; }
        public float? expenditures_health_services { get; set; }
        public float? expenditures_labor_and_employment { get; set; }
        public float? expenditures_housing_comm_devt { get; set; }
        public float? expenditures_social_welfare_services { get; set; }
        public float? expenditures_economic_services { get; set; }
        public float? expenditures_other_purposes { get; set; }

    }

    public class base_record
    {
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        
    }
}
