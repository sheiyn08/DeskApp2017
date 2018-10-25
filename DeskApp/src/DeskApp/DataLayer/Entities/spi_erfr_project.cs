using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class spi_erfr_detail
    {
        public Guid spi_erfr_detail_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int brgy_code { get; set; }
   
        public int SPID { get; set; }
        public DateTime? date_of_mibf { get; set; }
        public Nullable<decimal> Year { get; set; }
        public string title { get; set; }
        public string status { get; set; }
 
        public Nullable<decimal> grant_amount_direct_cost { get; set; }
        public Nullable<decimal> grant_amount_indirect_cost { get; set; }
        public Nullable<decimal> grant_amount_contingency_cost { get; set; }
        public Nullable<decimal> total_grant { get; set; }

        public string fund_source { get; set; }
        public Nullable<decimal> cycle { get; set; }
        public string grouping { get; set; }
        public Nullable<decimal> first_tranch_amount { get; set; }
        public DateTime? first_tranch_date_required { get; set; }
        public Nullable<decimal> second_tranch_amount { get; set; }
        public DateTime? second_tranch_date_required { get; set; }
        public Nullable<decimal> third_tranch_amount { get; set; }
        public DateTime? third_tranch_date_required { get; set; }
        public Nullable<decimal> lcc_blgu_direct_cost { get; set; }
        public Nullable<decimal> lcc_blgu_indirect_cost { get; set; }
        public Nullable<decimal> community_direct_cost { get; set; }
        public Nullable<decimal> community_indirect_cost { get; set; }
        public Nullable<decimal> mlgu_direct_cost { get; set; }
        public Nullable<decimal> mlgu_indirect_cost { get; set; }
        public Nullable<decimal> plgu_others_direct_cost { get; set; }
        public Nullable<decimal> plgu_others_indirect_cost { get; set; }
        public Nullable<decimal> total_lcc_cash_direct_cost { get; set; }
        public Nullable<decimal> total_lcc_cash_indirect_cost { get; set; }
        public Nullable<decimal> total_lcc_in_kind_direct_cost { get; set; }
        public Nullable<decimal> total_lcc_in_kind_indirect_cost { get; set; }
        public Nullable<decimal> total_lcc { get; set; }
        
    }


    public class erfr_sub_project
    {
        public string nscb_code { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Barangay { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SPID { get; set; }
        public DateTime? date_of_mibf { get; set; }
        public Nullable<decimal> Year { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        // public string category { get; set; }
        //  public string physical_target { get; set; }
        //    public string cost_parameter { get; set; }
        //   public string mode_of_implementation { get; set; }
        //   public string description { get; set; }
        public Nullable<decimal> grant_amount_direct_cost { get; set; }
        public Nullable<decimal> grant_amount_indirect_cost { get; set; }
        public Nullable<decimal> grant_amount_contingency_cost { get; set; }
        public Nullable<decimal> total_grant { get; set; }

        public string fund_source { get; set; }
        public Nullable<decimal> cycle { get; set; }
        public string grouping { get; set; }
        public Nullable<decimal> first_tranch_amount { get; set; }
        public DateTime? first_tranch_date_required { get; set; }
        public Nullable<decimal> second_tranch_amount { get; set; }
        public DateTime? second_tranch_date_required { get; set; }
        public Nullable<decimal> third_tranch_amount { get; set; }
        public DateTime? third_tranch_date_required { get; set; }
        public Nullable<decimal> lcc_blgu_direct_cost { get; set; }
        public Nullable<decimal> lcc_blgu_indirect_cost { get; set; }
        public Nullable<decimal> community_direct_cost { get; set; }
        public Nullable<decimal> community_indirect_cost { get; set; }
        public Nullable<decimal> mlgu_direct_cost { get; set; }
        public Nullable<decimal> mlgu_indirect_cost { get; set; }
        public Nullable<decimal> plgu_others_direct_cost { get; set; }
        public Nullable<decimal> plgu_others_indirect_cost { get; set; }
        public Nullable<decimal> total_lcc_cash_direct_cost { get; set; }
        public Nullable<decimal> total_lcc_cash_indirect_cost { get; set; }
        public Nullable<decimal> total_lcc_in_kind_direct_cost { get; set; }
        public Nullable<decimal> total_lcc_in_kind_indirect_cost { get; set; }
        public Nullable<decimal> total_lcc { get; set; }
        //public DateTime? created_at { get; set; }
        //public DateTime? updated_at { get; set; }
        //public DateTime? date_encoded { get; set; }
    }
}
