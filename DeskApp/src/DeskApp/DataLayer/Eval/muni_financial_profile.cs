using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Eval
{
    public class muni_financial_profile : base_record_muni
    {
        [Key]
        public Guid muni_financial_profile_id { get; set; }

        public int talakayan_yr_id { get; set; }
        public DateTime? talakayan_date_start { get; set; }
        public DateTime? talakayan_date_end { get; set; }

        public int? population_2015 { get; set; }

        public decimal? mlgu2009_rev_ira_share { get; set; }
        public string   mlgu2009_rev_ira_share_source { get; set; }
        public decimal? mlgu2009_rev_locally_sourced { get; set; }
        public string   mlgu2009_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2009_rev_other_revenues_total { get; set; }
        public string   mlgu2009_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2009_rev_total_lgu_income { get; set; }
        public string   mlgu2009_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2009_rev_total_lgu_income_per_capita { get; set; }
        public string   mlgu2009_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2009_rev_devt_fund { get; set; }
        public string   mlgu2009_rev_devt_fund_source { get; set; }
        public decimal? mlgu2009_rev_devt_fund_per_capita { get; set; }
        public string   mlgu2009_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2009_ex_gen_public { get; set; }
        public string   mlgu2009_ex_gen_public_source { get; set; }
        public decimal? mlgu2009_ex_educ { get; set; }
        public string   mlgu2009_ex_educ_source { get; set; }
        public decimal? mlgu2009_ex_health { get; set; }
        public string   mlgu2009_ex_health_source { get; set; }
        public decimal? mlgu2009_ex_labor { get; set; }
        public string   mlgu2009_ex_labor_source { get; set; }
        public decimal? mlgu2009_ex_housing { get; set; }
        public string   mlgu2009_ex_housing_source { get; set; }
        public decimal? mlgu2009_ex_social_welfare { get; set; }
        public string   mlgu2009_ex_social_welfare_source { get; set; }
        public decimal? mlgu2009_ex_economic { get; set; }
        public string   mlgu2009_ex_economic_source { get; set; }
        public decimal? mlgu2009_ex_other_purposes { get; set; }
        public string   mlgu2009_ex_other_purposes_source { get; set; }

        public decimal? mlgu2010_rev_ira_share { get; set; }
        public string   mlgu2010_rev_ira_share_source { get; set; }
        public decimal? mlgu2010_rev_locally_sourced { get; set; }
        public string   mlgu2010_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2010_rev_other_revenues_total { get; set; }
        public string   mlgu2010_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2010_rev_total_lgu_income { get; set; }
        public string   mlgu2010_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2010_rev_total_lgu_income_per_capita { get; set; }
        public string   mlgu2010_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2010_rev_devt_fund { get; set; }
        public string   mlgu2010_rev_devt_fund_source { get; set; }
        public decimal? mlgu2010_rev_devt_fund_per_capita { get; set; }
        public string   mlgu2010_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2010_ex_gen_public { get; set; }
        public string   mlgu2010_ex_gen_public_source { get; set; }
        public decimal? mlgu2010_ex_educ { get; set; }
        public string   mlgu2010_ex_educ_source { get; set; }
        public decimal? mlgu2010_ex_health { get; set; }
        public string   mlgu2010_ex_health_source { get; set; }
        public decimal? mlgu2010_ex_labor { get; set; }
        public string   mlgu2010_ex_labor_source { get; set; }
        public decimal? mlgu2010_ex_housing { get; set; }
        public string   mlgu2010_ex_housing_source { get; set; }
        public decimal? mlgu2010_ex_social_welfare { get; set; }
        public string   mlgu2010_ex_social_welfare_source { get; set; }
        public decimal? mlgu2010_ex_economic { get; set; }
        public string   mlgu2010_ex_economic_source { get; set; }
        public decimal? mlgu2010_ex_other_purposes { get; set; }
        public string   mlgu2010_ex_other_purposes_source { get; set; }

        public decimal? mlgu2011_rev_ira_share { get; set; }
        public string mlgu2011_rev_ira_share_source { get; set; }
        public decimal? mlgu2011_rev_locally_sourced { get; set; }
        public string mlgu2011_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2011_rev_other_revenues_total { get; set; }
        public string mlgu2011_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2011_rev_total_lgu_income { get; set; }
        public string mlgu2011_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2011_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2011_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2011_rev_devt_fund { get; set; }
        public string mlgu2011_rev_devt_fund_source { get; set; }
        public decimal? mlgu2011_rev_devt_fund_per_capita { get; set; }
        public string mlgu2011_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2011_ex_gen_public { get; set; }
        public string mlgu2011_ex_gen_public_source { get; set; }
        public decimal? mlgu2011_ex_educ { get; set; }
        public string mlgu2011_ex_educ_source { get; set; }
        public decimal? mlgu2011_ex_health { get; set; }
        public string mlgu2011_ex_health_source { get; set; }
        public decimal? mlgu2011_ex_labor { get; set; }
        public string mlgu2011_ex_labor_source { get; set; }
        public decimal? mlgu2011_ex_housing { get; set; }
        public string mlgu2011_ex_housing_source { get; set; }
        public decimal? mlgu2011_ex_social_welfare { get; set; }
        public string mlgu2011_ex_social_welfare_source { get; set; }
        public decimal? mlgu2011_ex_economic { get; set; }
        public string mlgu2011_ex_economic_source { get; set; }
        public decimal? mlgu2011_ex_other_purposes { get; set; }
        public string mlgu2011_ex_other_purposes_source { get; set; }

        public decimal? mlgu2012_rev_ira_share { get; set; }
        public string mlgu2012_rev_ira_share_source { get; set; }
        public decimal? mlgu2012_rev_locally_sourced { get; set; }
        public string mlgu2012_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2012_rev_other_revenues_total { get; set; }
        public string mlgu2012_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2012_rev_total_lgu_income { get; set; }
        public string mlgu2012_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2012_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2012_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2012_rev_devt_fund { get; set; }
        public string mlgu2012_rev_devt_fund_source { get; set; }
        public decimal? mlgu2012_rev_devt_fund_per_capita { get; set; }
        public string mlgu2012_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2012_ex_gen_public { get; set; }
        public string mlgu2012_ex_gen_public_source { get; set; }
        public decimal? mlgu2012_ex_educ { get; set; }
        public string mlgu2012_ex_educ_source { get; set; }
        public decimal? mlgu2012_ex_health { get; set; }
        public string mlgu2012_ex_health_source { get; set; }
        public decimal? mlgu2012_ex_labor { get; set; }
        public string mlgu2012_ex_labor_source { get; set; }
        public decimal? mlgu2012_ex_housing { get; set; }
        public string mlgu2012_ex_housing_source { get; set; }
        public decimal? mlgu2012_ex_social_welfare { get; set; }
        public string mlgu2012_ex_social_welfare_source { get; set; }
        public decimal? mlgu2012_ex_economic { get; set; }
        public string mlgu2012_ex_economic_source { get; set; }
        public decimal? mlgu2012_ex_other_purposes { get; set; }
        public string mlgu2012_ex_other_purposes_source { get; set; }

        public decimal? mlgu2013_rev_ira_share { get; set; }
        public string mlgu2013_rev_ira_share_source { get; set; }
        public decimal? mlgu2013_rev_locally_sourced { get; set; }
        public string mlgu2013_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2013_rev_other_revenues_total { get; set; }
        public string mlgu2013_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2013_rev_total_lgu_income { get; set; }
        public string mlgu2013_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2013_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2013_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2013_rev_devt_fund { get; set; }
        public string mlgu2013_rev_devt_fund_source { get; set; }
        public decimal? mlgu2013_rev_devt_fund_per_capita { get; set; }
        public string mlgu2013_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2013_ex_gen_public { get; set; }
        public string mlgu2013_ex_gen_public_source { get; set; }
        public decimal? mlgu2013_ex_educ { get; set; }
        public string mlgu2013_ex_educ_source { get; set; }
        public decimal? mlgu2013_ex_health { get; set; }
        public string mlgu2013_ex_health_source { get; set; }
        public decimal? mlgu2013_ex_labor { get; set; }
        public string mlgu2013_ex_labor_source { get; set; }
        public decimal? mlgu2013_ex_housing { get; set; }
        public string mlgu2013_ex_housing_source { get; set; }
        public decimal? mlgu2013_ex_social_welfare { get; set; }
        public string mlgu2013_ex_social_welfare_source { get; set; }
        public decimal? mlgu2013_ex_economic { get; set; }
        public string mlgu2013_ex_economic_source { get; set; }
        public decimal? mlgu2013_ex_other_purposes { get; set; }
        public string mlgu2013_ex_other_purposes_source { get; set; }

        public decimal? mlgu2014_rev_ira_share { get; set; }
        public string mlgu2014_rev_ira_share_source { get; set; }
        public decimal? mlgu2014_rev_locally_sourced { get; set; }
        public string mlgu2014_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2014_rev_other_revenues_total { get; set; }
        public string mlgu2014_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2014_rev_total_lgu_income { get; set; }
        public string mlgu2014_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2014_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2014_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2014_rev_devt_fund { get; set; }
        public string mlgu2014_rev_devt_fund_source { get; set; }
        public decimal? mlgu2014_rev_devt_fund_per_capita { get; set; }
        public string mlgu2014_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2014_ex_gen_public { get; set; }
        public string mlgu2014_ex_gen_public_source { get; set; }
        public decimal? mlgu2014_ex_educ { get; set; }
        public string mlgu2014_ex_educ_source { get; set; }
        public decimal? mlgu2014_ex_health { get; set; }
        public string mlgu2014_ex_health_source { get; set; }
        public decimal? mlgu2014_ex_labor { get; set; }
        public string mlgu2014_ex_labor_source { get; set; }
        public decimal? mlgu2014_ex_housing { get; set; }
        public string mlgu2014_ex_housing_source { get; set; }
        public decimal? mlgu2014_ex_social_welfare { get; set; }
        public string mlgu2014_ex_social_welfare_source { get; set; }
        public decimal? mlgu2014_ex_economic { get; set; }
        public string mlgu2014_ex_economic_source { get; set; }
        public decimal? mlgu2014_ex_other_purposes { get; set; }
        public string mlgu2014_ex_other_purposes_source { get; set; }

        public decimal? mlgu2015_rev_ira_share { get; set; }
        public string mlgu2015_rev_ira_share_source { get; set; }
        public decimal? mlgu2015_rev_locally_sourced { get; set; }
        public string mlgu2015_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2015_rev_other_revenues_total { get; set; }
        public string mlgu2015_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2015_rev_total_lgu_income { get; set; }
        public string mlgu2015_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2015_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2015_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2015_rev_devt_fund { get; set; }
        public string mlgu2015_rev_devt_fund_source { get; set; }
        public decimal? mlgu2015_rev_devt_fund_per_capita { get; set; }
        public string mlgu2015_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2015_ex_gen_public { get; set; }
        public string mlgu2015_ex_gen_public_source { get; set; }
        public decimal? mlgu2015_ex_educ { get; set; }
        public string mlgu2015_ex_educ_source { get; set; }
        public decimal? mlgu2015_ex_health { get; set; }
        public string mlgu2015_ex_health_source { get; set; }
        public decimal? mlgu2015_ex_labor { get; set; }
        public string mlgu2015_ex_labor_source { get; set; }
        public decimal? mlgu2015_ex_housing { get; set; }
        public string mlgu2015_ex_housing_source { get; set; }
        public decimal? mlgu2015_ex_social_welfare { get; set; }
        public string mlgu2015_ex_social_welfare_source { get; set; }
        public decimal? mlgu2015_ex_economic { get; set; }
        public string mlgu2015_ex_economic_source { get; set; }
        public decimal? mlgu2015_ex_other_purposes { get; set; }
        public string mlgu2015_ex_other_purposes_source { get; set; }

        public decimal? mlgu2016_rev_ira_share { get; set; }
        public string mlgu2016_rev_ira_share_source { get; set; }
        public decimal? mlgu2016_rev_locally_sourced { get; set; }
        public string mlgu2016_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2016_rev_other_revenues_total { get; set; }
        public string mlgu2016_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2016_rev_total_lgu_income { get; set; }
        public string mlgu2016_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2016_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2016_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2016_rev_devt_fund { get; set; }
        public string mlgu2016_rev_devt_fund_source { get; set; }
        public decimal? mlgu2016_rev_devt_fund_per_capita { get; set; }
        public string mlgu2016_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2016_ex_gen_public { get; set; }
        public string mlgu2016_ex_gen_public_source { get; set; }
        public decimal? mlgu2016_ex_educ { get; set; }
        public string mlgu2016_ex_educ_source { get; set; }
        public decimal? mlgu2016_ex_health { get; set; }
        public string mlgu2016_ex_health_source { get; set; }
        public decimal? mlgu2016_ex_labor { get; set; }
        public string mlgu2016_ex_labor_source { get; set; }
        public decimal? mlgu2016_ex_housing { get; set; }
        public string mlgu2016_ex_housing_source { get; set; }
        public decimal? mlgu2016_ex_social_welfare { get; set; }
        public string mlgu2016_ex_social_welfare_source { get; set; }
        public decimal? mlgu2016_ex_economic { get; set; }
        public string mlgu2016_ex_economic_source { get; set; }
        public decimal? mlgu2016_ex_other_purposes { get; set; }
        public string mlgu2016_ex_other_purposes_source { get; set; }

        public decimal? mlgu2017_rev_ira_share { get; set; }
        public string mlgu2017_rev_ira_share_source { get; set; }
        public decimal? mlgu2017_rev_locally_sourced { get; set; }
        public string mlgu2017_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2017_rev_other_revenues_total { get; set; }
        public string mlgu2017_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2017_rev_total_lgu_income { get; set; }
        public string mlgu2017_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2017_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2017_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2017_rev_devt_fund { get; set; }
        public string mlgu2017_rev_devt_fund_source { get; set; }
        public decimal? mlgu2017_rev_devt_fund_per_capita { get; set; }
        public string mlgu2017_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2017_ex_gen_public { get; set; }
        public string mlgu2017_ex_gen_public_source { get; set; }
        public decimal? mlgu2017_ex_educ { get; set; }
        public string mlgu2017_ex_educ_source { get; set; }
        public decimal? mlgu2017_ex_health { get; set; }
        public string mlgu2017_ex_health_source { get; set; }
        public decimal? mlgu2017_ex_labor { get; set; }
        public string mlgu2017_ex_labor_source { get; set; }
        public decimal? mlgu2017_ex_housing { get; set; }
        public string mlgu2017_ex_housing_source { get; set; }
        public decimal? mlgu2017_ex_social_welfare { get; set; }
        public string mlgu2017_ex_social_welfare_source { get; set; }
        public decimal? mlgu2017_ex_economic { get; set; }
        public string mlgu2017_ex_economic_source { get; set; }
        public decimal? mlgu2017_ex_other_purposes { get; set; }
        public string mlgu2017_ex_other_purposes_source { get; set; }

        public decimal? mlgu2018_rev_ira_share { get; set; }
        public string mlgu2018_rev_ira_share_source { get; set; }
        public decimal? mlgu2018_rev_locally_sourced { get; set; }
        public string mlgu2018_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2018_rev_other_revenues_total { get; set; }
        public string mlgu2018_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2018_rev_total_lgu_income { get; set; }
        public string mlgu2018_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2018_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2018_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2018_rev_devt_fund { get; set; }
        public string mlgu2018_rev_devt_fund_source { get; set; }
        public decimal? mlgu2018_rev_devt_fund_per_capita { get; set; }
        public string mlgu2018_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2018_ex_gen_public { get; set; }
        public string mlgu2018_ex_gen_public_source { get; set; }
        public decimal? mlgu2018_ex_educ { get; set; }
        public string mlgu2018_ex_educ_source { get; set; }
        public decimal? mlgu2018_ex_health { get; set; }
        public string mlgu2018_ex_health_source { get; set; }
        public decimal? mlgu2018_ex_labor { get; set; }
        public string mlgu2018_ex_labor_source { get; set; }
        public decimal? mlgu2018_ex_housing { get; set; }
        public string mlgu2018_ex_housing_source { get; set; }
        public decimal? mlgu2018_ex_social_welfare { get; set; }
        public string mlgu2018_ex_social_welfare_source { get; set; }
        public decimal? mlgu2018_ex_economic { get; set; }
        public string mlgu2018_ex_economic_source { get; set; }
        public decimal? mlgu2018_ex_other_purposes { get; set; }
        public string mlgu2018_ex_other_purposes_source { get; set; }

        public decimal? mlgu2019_rev_ira_share { get; set; }
        public string mlgu2019_rev_ira_share_source { get; set; }
        public decimal? mlgu2019_rev_locally_sourced { get; set; }
        public string mlgu2019_rev_locally_sourced_source { get; set; }
        public decimal? mlgu2019_rev_other_revenues_total { get; set; }
        public string mlgu2019_rev_other_revenues_total_source { get; set; }
        public decimal? mlgu2019_rev_total_lgu_income { get; set; }
        public string mlgu2019_rev_total_lgu_income_source { get; set; }
        public decimal? mlgu2019_rev_total_lgu_income_per_capita { get; set; }
        public string mlgu2019_rev_total_lgu_income_per_capita_source { get; set; }
        public decimal? mlgu2019_rev_devt_fund { get; set; }
        public string mlgu2019_rev_devt_fund_source { get; set; }
        public decimal? mlgu2019_rev_devt_fund_per_capita { get; set; }
        public string mlgu2019_rev_devt_fund_per_capita_source { get; set; }
        public decimal? mlgu2019_ex_gen_public { get; set; }
        public string mlgu2019_ex_gen_public_source { get; set; }
        public decimal? mlgu2019_ex_educ { get; set; }
        public string mlgu2019_ex_educ_source { get; set; }
        public decimal? mlgu2019_ex_health { get; set; }
        public string mlgu2019_ex_health_source { get; set; }
        public decimal? mlgu2019_ex_labor { get; set; }
        public string mlgu2019_ex_labor_source { get; set; }
        public decimal? mlgu2019_ex_housing { get; set; }
        public string mlgu2019_ex_housing_source { get; set; }
        public decimal? mlgu2019_ex_social_welfare { get; set; }
        public string mlgu2019_ex_social_welfare_source { get; set; }
        public decimal? mlgu2019_ex_economic { get; set; }
        public string mlgu2019_ex_economic_source { get; set; }
        public decimal? mlgu2019_ex_other_purposes { get; set; }
        public string mlgu2019_ex_other_purposes_source { get; set; }
        
    }

    public class base_record_muni
    {
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? cycle_id { get; set; }
        


        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool is_deleted { get; set; }
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
