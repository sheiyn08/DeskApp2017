using DeskApp.Data;
using DeskApp.DataLayer;
using DeskApp.DataLayer.Eval;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.Controllers.Eval
{
    public class MunicipalFinancialProfileController : Controller
    {
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;

        public MunicipalFinancialProfileController(ApplicationDbContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            return View();
        }


        //api delete moved to DeleteController.cs 01-24-18

        public IActionResult FinancialProfile(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }
            ViewBag.id = id;

            return View();
        }

        #region
        //for filter:

        private IQueryable<muni_financial_profile> GetData(
                 DataLayer.AngularFilterModel item
           )
        {
            var model = db.muni_financial_profile.Where(x => x.is_deleted != true).AsQueryable();
            
            if (item.talakayan_yr_id != null)
            {
                model = model.Where(m => m.talakayan_yr_id == item.talakayan_yr_id);
            }
            if (item.talakayan_date_start != null)
            {
                model = model.Where(m => m.talakayan_date_start == item.talakayan_date_start);
            }
            if (item.talakayan_date_end != null)
            {
                model = model.Where(m => m.talakayan_date_end == item.talakayan_date_end);
            }
            if (item.region_code != null)
            {
                model = model.Where(m => m.region_code == item.region_code);
            }
            if (item.prov_code != null)
            {
                model = model.Where(m => m.prov_code == item.prov_code);
            }
            if (item.city_code != null)
            {
                model = model.Where(m => m.city_code == item.city_code);
            }
            if (item.cycle_id != null)
            {
                model = model.Where(m => m.cycle_id == item.cycle_id);
            }

            return model;
        }

        #endregion

        [HttpPost]
        [Route("api/offline/v1/muni_financial_profile/get_dto")]
        public PagedCollection<dynamic> GetAll(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            //THIS PART IS FOR RETRIEVING RECORDS TO BE DISPLAYED ON THE LIST
            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model.OrderBy(x => x.created_date)
                .Select(x => new
                {
                    muni_financial_profile_id = x.muni_financial_profile_id,
                    talakayan_date_start = x.talakayan_date_start,
                    talakayan_yr_id = x.talakayan_yr_id,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    lib_cycle_name = x.lib_cycle.name,
                    push_status_id = x.push_status_id,
                    push_date = x.push_date

                }).Skip(currPages * size).Take(size).ToList(),
            };

        }


        #region
        //get entered data (MLGU and source) to be used for report generation
                
        [HttpPost]
        [Route("api/export/muni_financial_profile/mlgu_data_entered")]
        public IActionResult export_report_mlgu(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var result = model.Select(x => new
            {
                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_cycle_name = x.lib_cycle.name,

                talakayan_yr_id = x.talakayan_yr_id,
               
                population_2015 = x.population_2015,

                mlgu2009_rev_ira_share = x.mlgu2009_rev_ira_share,
                mlgu2009_rev_ira_share_source = x.mlgu2009_rev_ira_share_source,
                mlgu2009_rev_locally_sourced = x.mlgu2009_rev_locally_sourced,
                mlgu2009_rev_locally_sourced_source = x.mlgu2009_rev_locally_sourced_source,
                mlgu2009_rev_other_revenues_total = x.mlgu2009_rev_other_revenues_total,
                mlgu2009_rev_other_revenues_total_source = x.mlgu2009_rev_other_revenues_total_source,
                mlgu2009_rev_total_lgu_income = x.mlgu2009_rev_total_lgu_income,
                mlgu2009_rev_total_lgu_income_source = x.mlgu2009_rev_total_lgu_income_source,
                mlgu2009_rev_total_lgu_income_per_capita = x.mlgu2009_rev_total_lgu_income_per_capita,
                mlgu2009_rev_total_lgu_income_per_capita_source = x.mlgu2009_rev_total_lgu_income_per_capita_source,
                mlgu2009_rev_devt_fund = x.mlgu2009_rev_devt_fund,
                mlgu2009_rev_devt_fund_source = x.mlgu2009_rev_devt_fund_source,
                mlgu2009_rev_devt_fund_per_capita = x.mlgu2009_rev_devt_fund_per_capita,
                mlgu2009_rev_devt_fund_per_capita_source = x.mlgu2009_rev_devt_fund_per_capita_source,
                mlgu2009_ex_gen_public = x.mlgu2009_ex_gen_public,
                mlgu2009_ex_gen_public_source = x.mlgu2009_ex_gen_public_source,
                mlgu2009_ex_educ = x.mlgu2009_ex_educ,
                mlgu2009_ex_educ_source = x.mlgu2009_ex_educ_source,
                mlgu2009_ex_health = x.mlgu2009_ex_health,
                mlgu2009_ex_health_source = x.mlgu2009_ex_health_source,
                mlgu2009_ex_labor = x.mlgu2009_ex_labor,
                mlgu2009_ex_labor_source = x.mlgu2009_ex_labor_source,
                mlgu2009_ex_housing = x.mlgu2009_ex_housing,
                mlgu2009_ex_housing_source = x.mlgu2009_ex_housing_source,
                mlgu2009_ex_social_welfare = x.mlgu2009_ex_social_welfare,
                mlgu2009_ex_social_welfare_source = x.mlgu2009_ex_social_welfare_source,
                mlgu2009_ex_economic = x.mlgu2009_ex_economic,
                mlgu2009_ex_economic_source = x.mlgu2009_ex_economic_source,
                mlgu2009_ex_other_purposes = x.mlgu2009_ex_other_purposes,
                mlgu2009_ex_other_purposes_source = x.mlgu2009_ex_other_purposes_source,

                mlgu2010_rev_ira_share = x.mlgu2010_rev_ira_share,
                mlgu2010_rev_ira_share_source = x.mlgu2010_rev_ira_share_source,
                mlgu2010_rev_locally_sourced = x.mlgu2010_rev_locally_sourced,
                mlgu2010_rev_locally_sourced_source = x.mlgu2010_rev_locally_sourced_source,
                mlgu2010_rev_other_revenues_total = x.mlgu2010_rev_other_revenues_total,
                mlgu2010_rev_other_revenues_total_source = x.mlgu2010_rev_other_revenues_total_source,
                mlgu2010_rev_total_lgu_income = x.mlgu2010_rev_total_lgu_income,
                mlgu2010_rev_total_lgu_income_source = x.mlgu2010_rev_total_lgu_income_source,
                mlgu2010_rev_total_lgu_income_per_capita = x.mlgu2010_rev_total_lgu_income_per_capita,
                mlgu2010_rev_total_lgu_income_per_capita_source = x.mlgu2010_rev_total_lgu_income_per_capita_source,
                mlgu2010_rev_devt_fund = x.mlgu2010_rev_devt_fund,
                mlgu2010_rev_devt_fund_source = x.mlgu2010_rev_devt_fund_source,
                mlgu2010_rev_devt_fund_per_capita = x.mlgu2010_rev_devt_fund_per_capita,
                mlgu2010_rev_devt_fund_per_capita_source = x.mlgu2010_rev_devt_fund_per_capita_source,
                mlgu2010_ex_gen_public = x.mlgu2010_ex_gen_public,
                mlgu2010_ex_gen_public_source = x.mlgu2010_ex_gen_public_source,
                mlgu2010_ex_educ = x.mlgu2010_ex_educ,
                mlgu2010_ex_educ_source = x.mlgu2010_ex_educ_source,
                mlgu2010_ex_health = x.mlgu2010_ex_health,
                mlgu2010_ex_health_source = x.mlgu2010_ex_health_source,
                mlgu2010_ex_labor = x.mlgu2010_ex_labor,
                mlgu2010_ex_labor_source = x.mlgu2010_ex_labor_source,
                mlgu2010_ex_housing = x.mlgu2010_ex_housing,
                mlgu2010_ex_housing_source = x.mlgu2010_ex_housing_source,
                mlgu2010_ex_social_welfare = x.mlgu2010_ex_social_welfare,
                mlgu2010_ex_social_welfare_source = x.mlgu2010_ex_social_welfare_source,
                mlgu2010_ex_economic = x.mlgu2010_ex_economic,
                mlgu2010_ex_economic_source = x.mlgu2010_ex_economic_source,
                mlgu2010_ex_other_purposes = x.mlgu2010_ex_other_purposes,
                mlgu2010_ex_other_purposes_source = x.mlgu2010_ex_other_purposes_source,

                mlgu2011_rev_ira_share = x.mlgu2011_rev_ira_share,
                mlgu2011_rev_ira_share_source = x.mlgu2011_rev_ira_share_source,
                mlgu2011_rev_locally_sourced = x.mlgu2011_rev_locally_sourced,
                mlgu2011_rev_locally_sourced_source = x.mlgu2011_rev_locally_sourced_source,
                mlgu2011_rev_other_revenues_total = x.mlgu2011_rev_other_revenues_total,
                mlgu2011_rev_other_revenues_total_source = x.mlgu2011_rev_other_revenues_total_source,
                mlgu2011_rev_total_lgu_income = x.mlgu2011_rev_total_lgu_income,
                mlgu2011_rev_total_lgu_income_source = x.mlgu2011_rev_total_lgu_income_source,
                mlgu2011_rev_total_lgu_income_per_capita = x.mlgu2011_rev_total_lgu_income_per_capita,
                mlgu2011_rev_total_lgu_income_per_capita_source = x.mlgu2011_rev_total_lgu_income_per_capita_source,
                mlgu2011_rev_devt_fund = x.mlgu2011_rev_devt_fund,
                mlgu2011_rev_devt_fund_source = x.mlgu2011_rev_devt_fund_source,
                mlgu2011_rev_devt_fund_per_capita = x.mlgu2011_rev_devt_fund_per_capita,
                mlgu2011_rev_devt_fund_per_capita_source = x.mlgu2011_rev_devt_fund_per_capita_source,
                mlgu2011_ex_gen_public = x.mlgu2011_ex_gen_public,
                mlgu2011_ex_gen_public_source = x.mlgu2011_ex_gen_public_source,
                mlgu2011_ex_educ = x.mlgu2011_ex_educ,
                mlgu2011_ex_educ_source = x.mlgu2011_ex_educ_source,
                mlgu2011_ex_health = x.mlgu2011_ex_health,
                mlgu2011_ex_health_source = x.mlgu2011_ex_health_source,
                mlgu2011_ex_labor = x.mlgu2011_ex_labor,
                mlgu2011_ex_labor_source = x.mlgu2011_ex_labor_source,
                mlgu2011_ex_housing = x.mlgu2011_ex_housing,
                mlgu2011_ex_housing_source = x.mlgu2011_ex_housing_source,
                mlgu2011_ex_social_welfare = x.mlgu2011_ex_social_welfare,
                mlgu2011_ex_social_welfare_source = x.mlgu2011_ex_social_welfare_source,
                mlgu2011_ex_economic = x.mlgu2011_ex_economic,
                mlgu2011_ex_economic_source = x.mlgu2011_ex_economic_source,
                mlgu2011_ex_other_purposes = x.mlgu2011_ex_other_purposes,
                mlgu2011_ex_other_purposes_source = x.mlgu2011_ex_other_purposes_source,

                mlgu2012_rev_ira_share = x.mlgu2012_rev_ira_share,
                mlgu2012_rev_ira_share_source = x.mlgu2012_rev_ira_share_source,
                mlgu2012_rev_locally_sourced = x.mlgu2012_rev_locally_sourced,
                mlgu2012_rev_locally_sourced_source = x.mlgu2012_rev_locally_sourced_source,
                mlgu2012_rev_other_revenues_total = x.mlgu2012_rev_other_revenues_total,
                mlgu2012_rev_other_revenues_total_source = x.mlgu2012_rev_other_revenues_total_source,
                mlgu2012_rev_total_lgu_income = x.mlgu2012_rev_total_lgu_income,
                mlgu2012_rev_total_lgu_income_source = x.mlgu2012_rev_total_lgu_income_source,
                mlgu2012_rev_total_lgu_income_per_capita = x.mlgu2012_rev_total_lgu_income_per_capita,
                mlgu2012_rev_total_lgu_income_per_capita_source = x.mlgu2012_rev_total_lgu_income_per_capita_source,
                mlgu2012_rev_devt_fund = x.mlgu2012_rev_devt_fund,
                mlgu2012_rev_devt_fund_source = x.mlgu2012_rev_devt_fund_source,
                mlgu2012_rev_devt_fund_per_capita = x.mlgu2012_rev_devt_fund_per_capita,
                mlgu2012_rev_devt_fund_per_capita_source = x.mlgu2012_rev_devt_fund_per_capita_source,
                mlgu2012_ex_gen_public = x.mlgu2012_ex_gen_public,
                mlgu2012_ex_gen_public_source = x.mlgu2012_ex_gen_public_source,
                mlgu2012_ex_educ = x.mlgu2012_ex_educ,
                mlgu2012_ex_educ_source = x.mlgu2012_ex_educ_source,
                mlgu2012_ex_health = x.mlgu2012_ex_health,
                mlgu2012_ex_health_source = x.mlgu2012_ex_health_source,
                mlgu2012_ex_labor = x.mlgu2012_ex_labor,
                mlgu2012_ex_labor_source = x.mlgu2012_ex_labor_source,
                mlgu2012_ex_housing = x.mlgu2012_ex_housing,
                mlgu2012_ex_housing_source = x.mlgu2012_ex_housing_source,
                mlgu2012_ex_social_welfare = x.mlgu2012_ex_social_welfare,
                mlgu2012_ex_social_welfare_source = x.mlgu2012_ex_social_welfare_source,
                mlgu2012_ex_economic = x.mlgu2012_ex_economic,
                mlgu2012_ex_economic_source = x.mlgu2012_ex_economic_source,
                mlgu2012_ex_other_purposes = x.mlgu2012_ex_other_purposes,
                mlgu2012_ex_other_purposes_source = x.mlgu2012_ex_other_purposes_source,

                mlgu2013_rev_ira_share = x.mlgu2013_rev_ira_share,
                mlgu2013_rev_ira_share_source = x.mlgu2013_rev_ira_share_source,
                mlgu2013_rev_locally_sourced = x.mlgu2013_rev_locally_sourced,
                mlgu2013_rev_locally_sourced_source = x.mlgu2013_rev_locally_sourced_source,
                mlgu2013_rev_other_revenues_total = x.mlgu2013_rev_other_revenues_total,
                mlgu2013_rev_other_revenues_total_source = x.mlgu2013_rev_other_revenues_total_source,
                mlgu2013_rev_total_lgu_income = x.mlgu2013_rev_total_lgu_income,
                mlgu2013_rev_total_lgu_income_source = x.mlgu2013_rev_total_lgu_income_source,
                mlgu2013_rev_total_lgu_income_per_capita = x.mlgu2013_rev_total_lgu_income_per_capita,
                mlgu2013_rev_total_lgu_income_per_capita_source = x.mlgu2013_rev_total_lgu_income_per_capita_source,
                mlgu2013_rev_devt_fund = x.mlgu2013_rev_devt_fund,
                mlgu2013_rev_devt_fund_source = x.mlgu2013_rev_devt_fund_source,
                mlgu2013_rev_devt_fund_per_capita = x.mlgu2013_rev_devt_fund_per_capita,
                mlgu2013_rev_devt_fund_per_capita_source = x.mlgu2013_rev_devt_fund_per_capita_source,
                mlgu2013_ex_gen_public = x.mlgu2013_ex_gen_public,
                mlgu2013_ex_gen_public_source = x.mlgu2013_ex_gen_public_source,
                mlgu2013_ex_educ = x.mlgu2013_ex_educ,
                mlgu2013_ex_educ_source = x.mlgu2013_ex_educ_source,
                mlgu2013_ex_health = x.mlgu2013_ex_health,
                mlgu2013_ex_health_source = x.mlgu2013_ex_health_source,
                mlgu2013_ex_labor = x.mlgu2013_ex_labor,
                mlgu2013_ex_labor_source = x.mlgu2013_ex_labor_source,
                mlgu2013_ex_housing = x.mlgu2013_ex_housing,
                mlgu2013_ex_housing_source = x.mlgu2013_ex_housing_source,
                mlgu2013_ex_social_welfare = x.mlgu2013_ex_social_welfare,
                mlgu2013_ex_social_welfare_source = x.mlgu2013_ex_social_welfare_source,
                mlgu2013_ex_economic = x.mlgu2013_ex_economic,
                mlgu2013_ex_economic_source = x.mlgu2013_ex_economic_source,
                mlgu2013_ex_other_purposes = x.mlgu2013_ex_other_purposes,
                mlgu2013_ex_other_purposes_source = x.mlgu2013_ex_other_purposes_source,

                mlgu2014_rev_ira_share = x.mlgu2014_rev_ira_share,
                mlgu2014_rev_ira_share_source = x.mlgu2014_rev_ira_share_source,
                mlgu2014_rev_locally_sourced = x.mlgu2014_rev_locally_sourced,
                mlgu2014_rev_locally_sourced_source = x.mlgu2014_rev_locally_sourced_source,
                mlgu2014_rev_other_revenues_total = x.mlgu2014_rev_other_revenues_total,
                mlgu2014_rev_other_revenues_total_source = x.mlgu2014_rev_other_revenues_total_source,
                mlgu2014_rev_total_lgu_income = x.mlgu2014_rev_total_lgu_income,
                mlgu2014_rev_total_lgu_income_source = x.mlgu2014_rev_total_lgu_income_source,
                mlgu2014_rev_total_lgu_income_per_capita = x.mlgu2014_rev_total_lgu_income_per_capita,
                mlgu2014_rev_total_lgu_income_per_capita_source = x.mlgu2014_rev_total_lgu_income_per_capita_source,
                mlgu2014_rev_devt_fund = x.mlgu2014_rev_devt_fund,
                mlgu2014_rev_devt_fund_source = x.mlgu2014_rev_devt_fund_source,
                mlgu2014_rev_devt_fund_per_capita = x.mlgu2014_rev_devt_fund_per_capita,
                mlgu2014_rev_devt_fund_per_capita_source = x.mlgu2014_rev_devt_fund_per_capita_source,
                mlgu2014_ex_gen_public = x.mlgu2014_ex_gen_public,
                mlgu2014_ex_gen_public_source = x.mlgu2014_ex_gen_public_source,
                mlgu2014_ex_educ = x.mlgu2014_ex_educ,
                mlgu2014_ex_educ_source = x.mlgu2014_ex_educ_source,
                mlgu2014_ex_health = x.mlgu2014_ex_health,
                mlgu2014_ex_health_source = x.mlgu2014_ex_health_source,
                mlgu2014_ex_labor = x.mlgu2014_ex_labor,
                mlgu2014_ex_labor_source = x.mlgu2014_ex_labor_source,
                mlgu2014_ex_housing = x.mlgu2014_ex_housing,
                mlgu2014_ex_housing_source = x.mlgu2014_ex_housing_source,
                mlgu2014_ex_social_welfare = x.mlgu2014_ex_social_welfare,
                mlgu2014_ex_social_welfare_source = x.mlgu2014_ex_social_welfare_source,
                mlgu2014_ex_economic = x.mlgu2014_ex_economic,
                mlgu2014_ex_economic_source = x.mlgu2014_ex_economic_source,
                mlgu2014_ex_other_purposes = x.mlgu2014_ex_other_purposes,
                mlgu2014_ex_other_purposes_source = x.mlgu2014_ex_other_purposes_source,

                mlgu2015_rev_ira_share = x.mlgu2015_rev_ira_share,
                mlgu2015_rev_ira_share_source = x.mlgu2015_rev_ira_share_source,
                mlgu2015_rev_locally_sourced = x.mlgu2015_rev_locally_sourced,
                mlgu2015_rev_locally_sourced_source = x.mlgu2015_rev_locally_sourced_source,
                mlgu2015_rev_other_revenues_total = x.mlgu2015_rev_other_revenues_total,
                mlgu2015_rev_other_revenues_total_source = x.mlgu2015_rev_other_revenues_total_source,
                mlgu2015_rev_total_lgu_income = x.mlgu2015_rev_total_lgu_income,
                mlgu2015_rev_total_lgu_income_source = x.mlgu2015_rev_total_lgu_income_source,
                mlgu2015_rev_total_lgu_income_per_capita = x.mlgu2015_rev_total_lgu_income_per_capita,
                mlgu2015_rev_total_lgu_income_per_capita_source = x.mlgu2015_rev_total_lgu_income_per_capita_source,
                mlgu2015_rev_devt_fund = x.mlgu2015_rev_devt_fund,
                mlgu2015_rev_devt_fund_source = x.mlgu2015_rev_devt_fund_source,
                mlgu2015_rev_devt_fund_per_capita = x.mlgu2015_rev_devt_fund_per_capita,
                mlgu2015_rev_devt_fund_per_capita_source = x.mlgu2015_rev_devt_fund_per_capita_source,
                mlgu2015_ex_gen_public = x.mlgu2015_ex_gen_public,
                mlgu2015_ex_gen_public_source = x.mlgu2015_ex_gen_public_source,
                mlgu2015_ex_educ = x.mlgu2015_ex_educ,
                mlgu2015_ex_educ_source = x.mlgu2015_ex_educ_source,
                mlgu2015_ex_health = x.mlgu2015_ex_health,
                mlgu2015_ex_health_source = x.mlgu2015_ex_health_source,
                mlgu2015_ex_labor = x.mlgu2015_ex_labor,
                mlgu2015_ex_labor_source = x.mlgu2015_ex_labor_source,
                mlgu2015_ex_housing = x.mlgu2015_ex_housing,
                mlgu2015_ex_housing_source = x.mlgu2015_ex_housing_source,
                mlgu2015_ex_social_welfare = x.mlgu2015_ex_social_welfare,
                mlgu2015_ex_social_welfare_source = x.mlgu2015_ex_social_welfare_source,
                mlgu2015_ex_economic = x.mlgu2015_ex_economic,
                mlgu2015_ex_economic_source = x.mlgu2015_ex_economic_source,
                mlgu2015_ex_other_purposes = x.mlgu2015_ex_other_purposes,
                mlgu2015_ex_other_purposes_source = x.mlgu2015_ex_other_purposes_source,

                mlgu2016_rev_ira_share = x.mlgu2016_rev_ira_share,
                mlgu2016_rev_ira_share_source = x.mlgu2016_rev_ira_share_source,
                mlgu2016_rev_locally_sourced = x.mlgu2016_rev_locally_sourced,
                mlgu2016_rev_locally_sourced_source = x.mlgu2016_rev_locally_sourced_source,
                mlgu2016_rev_other_revenues_total = x.mlgu2016_rev_other_revenues_total,
                mlgu2016_rev_other_revenues_total_source = x.mlgu2016_rev_other_revenues_total_source,
                mlgu2016_rev_total_lgu_income = x.mlgu2016_rev_total_lgu_income,
                mlgu2016_rev_total_lgu_income_source = x.mlgu2016_rev_total_lgu_income_source,
                mlgu2016_rev_total_lgu_income_per_capita = x.mlgu2016_rev_total_lgu_income_per_capita,
                mlgu2016_rev_total_lgu_income_per_capita_source = x.mlgu2016_rev_total_lgu_income_per_capita_source,
                mlgu2016_rev_devt_fund = x.mlgu2016_rev_devt_fund,
                mlgu2016_rev_devt_fund_source = x.mlgu2016_rev_devt_fund_source,
                mlgu2016_rev_devt_fund_per_capita = x.mlgu2016_rev_devt_fund_per_capita,
                mlgu2016_rev_devt_fund_per_capita_source = x.mlgu2016_rev_devt_fund_per_capita_source,
                mlgu2016_ex_gen_public = x.mlgu2016_ex_gen_public,
                mlgu2016_ex_gen_public_source = x.mlgu2016_ex_gen_public_source,
                mlgu2016_ex_educ = x.mlgu2016_ex_educ,
                mlgu2016_ex_educ_source = x.mlgu2016_ex_educ_source,
                mlgu2016_ex_health = x.mlgu2016_ex_health,
                mlgu2016_ex_health_source = x.mlgu2016_ex_health_source,
                mlgu2016_ex_labor = x.mlgu2016_ex_labor,
                mlgu2016_ex_labor_source = x.mlgu2016_ex_labor_source,
                mlgu2016_ex_housing = x.mlgu2016_ex_housing,
                mlgu2016_ex_housing_source = x.mlgu2016_ex_housing_source,
                mlgu2016_ex_social_welfare = x.mlgu2016_ex_social_welfare,
                mlgu2016_ex_social_welfare_source = x.mlgu2016_ex_social_welfare_source,
                mlgu2016_ex_economic = x.mlgu2016_ex_economic,
                mlgu2016_ex_economic_source = x.mlgu2016_ex_economic_source,
                mlgu2016_ex_other_purposes = x.mlgu2016_ex_other_purposes,
                mlgu2016_ex_other_purposes_source = x.mlgu2016_ex_other_purposes_source,

                mlgu2017_rev_ira_share = x.mlgu2017_rev_ira_share,
                mlgu2017_rev_ira_share_source = x.mlgu2017_rev_ira_share_source,
                mlgu2017_rev_locally_sourced = x.mlgu2017_rev_locally_sourced,
                mlgu2017_rev_locally_sourced_source = x.mlgu2017_rev_locally_sourced_source,
                mlgu2017_rev_other_revenues_total = x.mlgu2017_rev_other_revenues_total,
                mlgu2017_rev_other_revenues_total_source = x.mlgu2017_rev_other_revenues_total_source,
                mlgu2017_rev_total_lgu_income = x.mlgu2017_rev_total_lgu_income,
                mlgu2017_rev_total_lgu_income_source = x.mlgu2017_rev_total_lgu_income_source,
                mlgu2017_rev_total_lgu_income_per_capita = x.mlgu2017_rev_total_lgu_income_per_capita,
                mlgu2017_rev_total_lgu_income_per_capita_source = x.mlgu2017_rev_total_lgu_income_per_capita_source,
                mlgu2017_rev_devt_fund = x.mlgu2017_rev_devt_fund,
                mlgu2017_rev_devt_fund_source = x.mlgu2017_rev_devt_fund_source,
                mlgu2017_rev_devt_fund_per_capita = x.mlgu2017_rev_devt_fund_per_capita,
                mlgu2017_rev_devt_fund_per_capita_source = x.mlgu2017_rev_devt_fund_per_capita_source,
                mlgu2017_ex_gen_public = x.mlgu2017_ex_gen_public,
                mlgu2017_ex_gen_public_source = x.mlgu2017_ex_gen_public_source,
                mlgu2017_ex_educ = x.mlgu2017_ex_educ,
                mlgu2017_ex_educ_source = x.mlgu2017_ex_educ_source,
                mlgu2017_ex_health = x.mlgu2017_ex_health,
                mlgu2017_ex_health_source = x.mlgu2017_ex_health_source,
                mlgu2017_ex_labor = x.mlgu2017_ex_labor,
                mlgu2017_ex_labor_source = x.mlgu2017_ex_labor_source,
                mlgu2017_ex_housing = x.mlgu2017_ex_housing,
                mlgu2017_ex_housing_source = x.mlgu2017_ex_housing_source,
                mlgu2017_ex_social_welfare = x.mlgu2017_ex_social_welfare,
                mlgu2017_ex_social_welfare_source = x.mlgu2017_ex_social_welfare_source,
                mlgu2017_ex_economic = x.mlgu2017_ex_economic,
                mlgu2017_ex_economic_source = x.mlgu2017_ex_economic_source,
                mlgu2017_ex_other_purposes = x.mlgu2017_ex_other_purposes,
                mlgu2017_ex_other_purposes_source = x.mlgu2017_ex_other_purposes_source,

                mlgu2018_rev_ira_share = x.mlgu2018_rev_ira_share,
                mlgu2018_rev_ira_share_source = x.mlgu2018_rev_ira_share_source,
                mlgu2018_rev_locally_sourced = x.mlgu2018_rev_locally_sourced,
                mlgu2018_rev_locally_sourced_source = x.mlgu2018_rev_locally_sourced_source,
                mlgu2018_rev_other_revenues_total = x.mlgu2018_rev_other_revenues_total,
                mlgu2018_rev_other_revenues_total_source = x.mlgu2018_rev_other_revenues_total_source,
                mlgu2018_rev_total_lgu_income = x.mlgu2018_rev_total_lgu_income,
                mlgu2018_rev_total_lgu_income_source = x.mlgu2018_rev_total_lgu_income_source,
                mlgu2018_rev_total_lgu_income_per_capita = x.mlgu2018_rev_total_lgu_income_per_capita,
                mlgu2018_rev_total_lgu_income_per_capita_source = x.mlgu2018_rev_total_lgu_income_per_capita_source,
                mlgu2018_rev_devt_fund = x.mlgu2018_rev_devt_fund,
                mlgu2018_rev_devt_fund_source = x.mlgu2018_rev_devt_fund_source,
                mlgu2018_rev_devt_fund_per_capita = x.mlgu2018_rev_devt_fund_per_capita,
                mlgu2018_rev_devt_fund_per_capita_source = x.mlgu2018_rev_devt_fund_per_capita_source,
                mlgu2018_ex_gen_public = x.mlgu2018_ex_gen_public,
                mlgu2018_ex_gen_public_source = x.mlgu2018_ex_gen_public_source,
                mlgu2018_ex_educ = x.mlgu2018_ex_educ,
                mlgu2018_ex_educ_source = x.mlgu2018_ex_educ_source,
                mlgu2018_ex_health = x.mlgu2018_ex_health,
                mlgu2018_ex_health_source = x.mlgu2018_ex_health_source,
                mlgu2018_ex_labor = x.mlgu2018_ex_labor,
                mlgu2018_ex_labor_source = x.mlgu2018_ex_labor_source,
                mlgu2018_ex_housing = x.mlgu2018_ex_housing,
                mlgu2018_ex_housing_source = x.mlgu2018_ex_housing_source,
                mlgu2018_ex_social_welfare = x.mlgu2018_ex_social_welfare,
                mlgu2018_ex_social_welfare_source = x.mlgu2018_ex_social_welfare_source,
                mlgu2018_ex_economic = x.mlgu2018_ex_economic,
                mlgu2018_ex_economic_source = x.mlgu2018_ex_economic_source,
                mlgu2018_ex_other_purposes = x.mlgu2018_ex_other_purposes,
                mlgu2018_ex_other_purposes_source = x.mlgu2018_ex_other_purposes_source,

                mlgu2019_rev_ira_share = x.mlgu2019_rev_ira_share,
                mlgu2019_rev_ira_share_source = x.mlgu2019_rev_ira_share_source,
                mlgu2019_rev_locally_sourced = x.mlgu2019_rev_locally_sourced,
                mlgu2019_rev_locally_sourced_source = x.mlgu2019_rev_locally_sourced_source,
                mlgu2019_rev_other_revenues_total = x.mlgu2019_rev_other_revenues_total,
                mlgu2019_rev_other_revenues_total_source = x.mlgu2019_rev_other_revenues_total_source,
                mlgu2019_rev_total_lgu_income = x.mlgu2019_rev_total_lgu_income,
                mlgu2019_rev_total_lgu_income_source = x.mlgu2019_rev_total_lgu_income_source,
                mlgu2019_rev_total_lgu_income_per_capita = x.mlgu2019_rev_total_lgu_income_per_capita,
                mlgu2019_rev_total_lgu_income_per_capita_source = x.mlgu2019_rev_total_lgu_income_per_capita_source,
                mlgu2019_rev_devt_fund = x.mlgu2019_rev_devt_fund,
                mlgu2019_rev_devt_fund_source = x.mlgu2019_rev_devt_fund_source,
                mlgu2019_rev_devt_fund_per_capita = x.mlgu2019_rev_devt_fund_per_capita,
                mlgu2019_rev_devt_fund_per_capita_source = x.mlgu2019_rev_devt_fund_per_capita_source,
                mlgu2019_ex_gen_public = x.mlgu2019_ex_gen_public,
                mlgu2019_ex_gen_public_source = x.mlgu2019_ex_gen_public_source,
                mlgu2019_ex_educ = x.mlgu2019_ex_educ,
                mlgu2019_ex_educ_source = x.mlgu2019_ex_educ_source,
                mlgu2019_ex_health = x.mlgu2019_ex_health,
                mlgu2019_ex_health_source = x.mlgu2019_ex_health_source,
                mlgu2019_ex_labor = x.mlgu2019_ex_labor,
                mlgu2019_ex_labor_source = x.mlgu2019_ex_labor_source,
                mlgu2019_ex_housing = x.mlgu2019_ex_housing,
                mlgu2019_ex_housing_source = x.mlgu2019_ex_housing_source,
                mlgu2019_ex_social_welfare = x.mlgu2019_ex_social_welfare,
                mlgu2019_ex_social_welfare_source = x.mlgu2019_ex_social_welfare_source,
                mlgu2019_ex_economic = x.mlgu2019_ex_economic,
                mlgu2019_ex_economic_source = x.mlgu2019_ex_economic_source,
                mlgu2019_ex_other_purposes = x.mlgu2019_ex_other_purposes,
                mlgu2019_ex_other_purposes_source = x.mlgu2019_ex_other_purposes_source,

                //mlgu2009_rev_ira_share = ((double)x.mlgu2009_rev_ira_share).ToString("C2"),
                //mlgu2009_rev_ira_share_source = x.mlgu2009_rev_ira_share_source,
                //mlgu2009_rev_locally_sourced = ((double)x.mlgu2009_rev_locally_sourced).ToString("C2"),
                //mlgu2009_rev_locally_sourced_source = x.mlgu2009_rev_locally_sourced_source,
                //mlgu2009_rev_other_revenues_total = ((double)x.mlgu2009_rev_other_revenues_total).ToString("C2"),
                //mlgu2009_rev_other_revenues_total_source = x.mlgu2009_rev_other_revenues_total_source,
                //mlgu2009_rev_total_lgu_income = ((double)x.mlgu2009_rev_total_lgu_income).ToString("C2"),
                //mlgu2009_rev_total_lgu_income_source = x.mlgu2009_rev_total_lgu_income_source,
                //mlgu2009_rev_total_lgu_income_per_capita = ((double)x.mlgu2009_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2009_rev_total_lgu_income_per_capita_source = x.mlgu2009_rev_total_lgu_income_per_capita_source,
                //mlgu2009_rev_devt_fund = ((double)x.mlgu2009_rev_devt_fund).ToString("C2"),
                //mlgu2009_rev_devt_fund_source = x.mlgu2009_rev_devt_fund_source,
                //mlgu2009_rev_devt_fund_per_capita = ((double)x.mlgu2009_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2009_rev_devt_fund_per_capita_source = x.mlgu2009_rev_devt_fund_per_capita_source,
                //mlgu2009_ex_gen_public = ((double)x.mlgu2009_ex_gen_public).ToString("C2"),
                //mlgu2009_ex_gen_public_source = x.mlgu2009_ex_gen_public_source,
                //mlgu2009_ex_educ = ((double)x.mlgu2009_ex_educ).ToString("C2"),
                //mlgu2009_ex_educ_source = x.mlgu2009_ex_educ_source,
                //mlgu2009_ex_health = ((double)x.mlgu2009_ex_health).ToString("C2"),
                //mlgu2009_ex_health_source = x.mlgu2009_ex_health_source,
                //mlgu2009_ex_labor = ((double)x.mlgu2009_ex_labor).ToString("C2"),
                //mlgu2009_ex_labor_source = x.mlgu2009_ex_labor_source,
                //mlgu2009_ex_housing = ((double)x.mlgu2009_ex_housing).ToString("C2"),
                //mlgu2009_ex_housing_source = x.mlgu2009_ex_housing_source,
                //mlgu2009_ex_social_welfare = ((double)x.mlgu2009_ex_social_welfare).ToString("C2"),
                //mlgu2009_ex_social_welfare_source = x.mlgu2009_ex_social_welfare_source,
                //mlgu2009_ex_economic = ((double)x.mlgu2009_ex_economic).ToString("C2"),
                //mlgu2009_ex_economic_source = x.mlgu2009_ex_economic_source,
                //mlgu2009_ex_other_purposes = ((double)x.mlgu2009_ex_other_purposes).ToString("C2"),
                //mlgu2009_ex_other_purposes_source = x.mlgu2009_ex_other_purposes_source,

                //mlgu2010_rev_ira_share = ((double)x.mlgu2010_rev_ira_share).ToString("C2"),
                //mlgu2010_rev_ira_share_source = x.mlgu2010_rev_ira_share_source,
                //mlgu2010_rev_locally_sourced = ((double)x.mlgu2010_rev_locally_sourced).ToString("C2"),
                //mlgu2010_rev_locally_sourced_source = x.mlgu2010_rev_locally_sourced_source,
                //mlgu2010_rev_other_revenues_total = ((double)x.mlgu2010_rev_other_revenues_total).ToString("C2"),
                //mlgu2010_rev_other_revenues_total_source = x.mlgu2010_rev_other_revenues_total_source,
                //mlgu2010_rev_total_lgu_income = ((double)x.mlgu2010_rev_total_lgu_income).ToString("C2"),
                //mlgu2010_rev_total_lgu_income_source = x.mlgu2010_rev_total_lgu_income_source,
                //mlgu2010_rev_total_lgu_income_per_capita = ((double)x.mlgu2010_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2010_rev_total_lgu_income_per_capita_source = x.mlgu2010_rev_total_lgu_income_per_capita_source,
                //mlgu2010_rev_devt_fund = ((double)x.mlgu2010_rev_devt_fund).ToString("C2"),
                //mlgu2010_rev_devt_fund_source = x.mlgu2010_rev_devt_fund_source,
                //mlgu2010_rev_devt_fund_per_capita = ((double)x.mlgu2010_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2010_rev_devt_fund_per_capita_source = x.mlgu2010_rev_devt_fund_per_capita_source,
                //mlgu2010_ex_gen_public = ((double)x.mlgu2010_ex_gen_public).ToString("C2"),
                //mlgu2010_ex_gen_public_source = x.mlgu2010_ex_gen_public_source,
                //mlgu2010_ex_educ = ((double)x.mlgu2010_ex_educ).ToString("C2"),
                //mlgu2010_ex_educ_source = x.mlgu2010_ex_educ_source,
                //mlgu2010_ex_health = ((double)x.mlgu2010_ex_health).ToString("C2"),
                //mlgu2010_ex_health_source = x.mlgu2010_ex_health_source,
                //mlgu2010_ex_labor = ((double)x.mlgu2010_ex_labor).ToString("C2"),
                //mlgu2010_ex_labor_source = x.mlgu2010_ex_labor_source,
                //mlgu2010_ex_housing = ((double)x.mlgu2010_ex_housing).ToString("C2"),
                //mlgu2010_ex_housing_source = x.mlgu2010_ex_housing_source,
                //mlgu2010_ex_social_welfare = ((double)x.mlgu2010_ex_social_welfare).ToString("C2"),
                //mlgu2010_ex_social_welfare_source = x.mlgu2010_ex_social_welfare_source,
                //mlgu2010_ex_economic = ((double)x.mlgu2010_ex_economic).ToString("C2"),
                //mlgu2010_ex_economic_source = x.mlgu2010_ex_economic_source,
                //mlgu2010_ex_other_purposes = ((double)x.mlgu2010_ex_other_purposes).ToString("C2"),
                //mlgu2010_ex_other_purposes_source = x.mlgu2010_ex_other_purposes_source,

                //mlgu2011_rev_ira_share = ((double)x.mlgu2011_rev_ira_share).ToString("C2"),
                //mlgu2011_rev_ira_share_source = x.mlgu2011_rev_ira_share_source,
                //mlgu2011_rev_locally_sourced = ((double)x.mlgu2011_rev_locally_sourced).ToString("C2"),
                //mlgu2011_rev_locally_sourced_source = x.mlgu2011_rev_locally_sourced_source,
                //mlgu2011_rev_other_revenues_total = ((double)x.mlgu2011_rev_other_revenues_total).ToString("C2"),
                //mlgu2011_rev_other_revenues_total_source = x.mlgu2011_rev_other_revenues_total_source,
                //mlgu2011_rev_total_lgu_income = ((double)x.mlgu2011_rev_total_lgu_income).ToString("C2"),
                //mlgu2011_rev_total_lgu_income_source = x.mlgu2011_rev_total_lgu_income_source,
                //mlgu2011_rev_total_lgu_income_per_capita = ((double)x.mlgu2011_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2011_rev_total_lgu_income_per_capita_source = x.mlgu2011_rev_total_lgu_income_per_capita_source,
                //mlgu2011_rev_devt_fund = ((double)x.mlgu2011_rev_devt_fund).ToString("C2"),
                //mlgu2011_rev_devt_fund_source = x.mlgu2011_rev_devt_fund_source,
                //mlgu2011_rev_devt_fund_per_capita = ((double)x.mlgu2011_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2011_rev_devt_fund_per_capita_source = x.mlgu2011_rev_devt_fund_per_capita_source,
                //mlgu2011_ex_gen_public = ((double)x.mlgu2011_ex_gen_public).ToString("C2"),
                //mlgu2011_ex_gen_public_source = x.mlgu2011_ex_gen_public_source,
                //mlgu2011_ex_educ = ((double)x.mlgu2011_ex_educ).ToString("C2"),
                //mlgu2011_ex_educ_source = x.mlgu2011_ex_educ_source,
                //mlgu2011_ex_health = ((double)x.mlgu2011_ex_health).ToString("C2"),
                //mlgu2011_ex_health_source = x.mlgu2011_ex_health_source,
                //mlgu2011_ex_labor = ((double)x.mlgu2011_ex_labor).ToString("C2"),
                //mlgu2011_ex_labor_source = x.mlgu2011_ex_labor_source,
                //mlgu2011_ex_housing = ((double)x.mlgu2011_ex_housing).ToString("C2"),
                //mlgu2011_ex_housing_source = x.mlgu2011_ex_housing_source,
                //mlgu2011_ex_social_welfare = ((double)x.mlgu2011_ex_social_welfare).ToString("C2"),
                //mlgu2011_ex_social_welfare_source = x.mlgu2011_ex_social_welfare_source,
                //mlgu2011_ex_economic = ((double)x.mlgu2011_ex_economic).ToString("C2"),
                //mlgu2011_ex_economic_source = x.mlgu2011_ex_economic_source,
                //mlgu2011_ex_other_purposes = ((double)x.mlgu2011_ex_other_purposes).ToString("C2"),
                //mlgu2011_ex_other_purposes_source = x.mlgu2011_ex_other_purposes_source,

                //mlgu2012_rev_ira_share = ((double)x.mlgu2012_rev_ira_share).ToString("C2"),
                //mlgu2012_rev_ira_share_source = x.mlgu2012_rev_ira_share_source,
                //mlgu2012_rev_locally_sourced = ((double)x.mlgu2012_rev_locally_sourced).ToString("C2"),
                //mlgu2012_rev_locally_sourced_source = x.mlgu2012_rev_locally_sourced_source,
                //mlgu2012_rev_other_revenues_total = ((double)x.mlgu2012_rev_other_revenues_total).ToString("C2"),
                //mlgu2012_rev_other_revenues_total_source = x.mlgu2012_rev_other_revenues_total_source,
                //mlgu2012_rev_total_lgu_income = ((double)x.mlgu2012_rev_total_lgu_income).ToString("C2"),
                //mlgu2012_rev_total_lgu_income_source = x.mlgu2012_rev_total_lgu_income_source,
                //mlgu2012_rev_total_lgu_income_per_capita = ((double)x.mlgu2012_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2012_rev_total_lgu_income_per_capita_source = x.mlgu2012_rev_total_lgu_income_per_capita_source,
                //mlgu2012_rev_devt_fund = ((double)x.mlgu2012_rev_devt_fund).ToString("C2"),
                //mlgu2012_rev_devt_fund_source = x.mlgu2012_rev_devt_fund_source,
                //mlgu2012_rev_devt_fund_per_capita = ((double)x.mlgu2012_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2012_rev_devt_fund_per_capita_source = x.mlgu2012_rev_devt_fund_per_capita_source,
                //mlgu2012_ex_gen_public = ((double)x.mlgu2012_ex_gen_public).ToString("C2"),
                //mlgu2012_ex_gen_public_source = x.mlgu2012_ex_gen_public_source,
                //mlgu2012_ex_educ = ((double)x.mlgu2012_ex_educ).ToString("C2"),
                //mlgu2012_ex_educ_source = x.mlgu2012_ex_educ_source,
                //mlgu2012_ex_health = ((double)x.mlgu2012_ex_health).ToString("C2"),
                //mlgu2012_ex_health_source = x.mlgu2012_ex_health_source,
                //mlgu2012_ex_labor = ((double)x.mlgu2012_ex_labor).ToString("C2"),
                //mlgu2012_ex_labor_source = x.mlgu2012_ex_labor_source,
                //mlgu2012_ex_housing = ((double)x.mlgu2012_ex_housing).ToString("C2"),
                //mlgu2012_ex_housing_source = x.mlgu2012_ex_housing_source,
                //mlgu2012_ex_social_welfare = ((double)x.mlgu2012_ex_social_welfare).ToString("C2"),
                //mlgu2012_ex_social_welfare_source = x.mlgu2012_ex_social_welfare_source,
                //mlgu2012_ex_economic = ((double)x.mlgu2012_ex_economic).ToString("C2"),
                //mlgu2012_ex_economic_source = x.mlgu2012_ex_economic_source,
                //mlgu2012_ex_other_purposes = ((double)x.mlgu2012_ex_other_purposes).ToString("C2"),
                //mlgu2012_ex_other_purposes_source = x.mlgu2012_ex_other_purposes_source,

                //mlgu2013_rev_ira_share = ((double)x.mlgu2013_rev_ira_share).ToString("C2"),
                //mlgu2013_rev_ira_share_source = x.mlgu2013_rev_ira_share_source,
                //mlgu2013_rev_locally_sourced = ((double)x.mlgu2013_rev_locally_sourced).ToString("C2"),
                //mlgu2013_rev_locally_sourced_source = x.mlgu2013_rev_locally_sourced_source,
                //mlgu2013_rev_other_revenues_total = ((double)x.mlgu2013_rev_other_revenues_total).ToString("C2"),
                //mlgu2013_rev_other_revenues_total_source = x.mlgu2013_rev_other_revenues_total_source,
                //mlgu2013_rev_total_lgu_income = ((double)x.mlgu2013_rev_total_lgu_income).ToString("C2"),
                //mlgu2013_rev_total_lgu_income_source = x.mlgu2013_rev_total_lgu_income_source,
                //mlgu2013_rev_total_lgu_income_per_capita = ((double)x.mlgu2013_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2013_rev_total_lgu_income_per_capita_source = x.mlgu2013_rev_total_lgu_income_per_capita_source,
                //mlgu2013_rev_devt_fund = ((double)x.mlgu2013_rev_devt_fund).ToString("C2"),
                //mlgu2013_rev_devt_fund_source = x.mlgu2013_rev_devt_fund_source,
                //mlgu2013_rev_devt_fund_per_capita = ((double)x.mlgu2013_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2013_rev_devt_fund_per_capita_source = x.mlgu2013_rev_devt_fund_per_capita_source,
                //mlgu2013_ex_gen_public = ((double)x.mlgu2013_ex_gen_public).ToString("C2"),
                //mlgu2013_ex_gen_public_source = x.mlgu2013_ex_gen_public_source,
                //mlgu2013_ex_educ = ((double)x.mlgu2013_ex_educ).ToString("C2"),
                //mlgu2013_ex_educ_source = x.mlgu2013_ex_educ_source,
                //mlgu2013_ex_health = ((double)x.mlgu2013_ex_health).ToString("C2"),
                //mlgu2013_ex_health_source = x.mlgu2013_ex_health_source,
                //mlgu2013_ex_labor = ((double)x.mlgu2013_ex_labor).ToString("C2"),
                //mlgu2013_ex_labor_source = x.mlgu2013_ex_labor_source,
                //mlgu2013_ex_housing = ((double)x.mlgu2013_ex_housing).ToString("C2"),
                //mlgu2013_ex_housing_source = x.mlgu2013_ex_housing_source,
                //mlgu2013_ex_social_welfare = ((double)x.mlgu2013_ex_social_welfare).ToString("C2"),
                //mlgu2013_ex_social_welfare_source = x.mlgu2013_ex_social_welfare_source,
                //mlgu2013_ex_economic = ((double)x.mlgu2013_ex_economic).ToString("C2"),
                //mlgu2013_ex_economic_source = x.mlgu2013_ex_economic_source,
                //mlgu2013_ex_other_purposes = ((double)x.mlgu2013_ex_other_purposes).ToString("C2"),
                //mlgu2013_ex_other_purposes_source = x.mlgu2013_ex_other_purposes_source,

                //mlgu2014_rev_ira_share = ((double)x.mlgu2014_rev_ira_share).ToString("C2"),
                //mlgu2014_rev_ira_share_source = x.mlgu2014_rev_ira_share_source,
                //mlgu2014_rev_locally_sourced = ((double)x.mlgu2014_rev_locally_sourced).ToString("C2"),
                //mlgu2014_rev_locally_sourced_source = x.mlgu2014_rev_locally_sourced_source,
                //mlgu2014_rev_other_revenues_total = ((double)x.mlgu2014_rev_other_revenues_total).ToString("C2"),
                //mlgu2014_rev_other_revenues_total_source = x.mlgu2014_rev_other_revenues_total_source,
                //mlgu2014_rev_total_lgu_income = ((double)x.mlgu2014_rev_total_lgu_income).ToString("C2"),
                //mlgu2014_rev_total_lgu_income_source = x.mlgu2014_rev_total_lgu_income_source,
                //mlgu2014_rev_total_lgu_income_per_capita = ((double)x.mlgu2014_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2014_rev_total_lgu_income_per_capita_source = x.mlgu2014_rev_total_lgu_income_per_capita_source,
                //mlgu2014_rev_devt_fund = ((double)x.mlgu2014_rev_devt_fund).ToString("C2"),
                //mlgu2014_rev_devt_fund_source = x.mlgu2014_rev_devt_fund_source,
                //mlgu2014_rev_devt_fund_per_capita = ((double)x.mlgu2014_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2014_rev_devt_fund_per_capita_source = x.mlgu2014_rev_devt_fund_per_capita_source,
                //mlgu2014_ex_gen_public = ((double)x.mlgu2014_ex_gen_public).ToString("C2"),
                //mlgu2014_ex_gen_public_source = x.mlgu2014_ex_gen_public_source,
                //mlgu2014_ex_educ = ((double)x.mlgu2014_ex_educ).ToString("C2"),
                //mlgu2014_ex_educ_source = x.mlgu2014_ex_educ_source,
                //mlgu2014_ex_health = ((double)x.mlgu2014_ex_health).ToString("C2"),
                //mlgu2014_ex_health_source = x.mlgu2014_ex_health_source,
                //mlgu2014_ex_labor = ((double)x.mlgu2014_ex_labor).ToString("C2"),
                //mlgu2014_ex_labor_source = x.mlgu2014_ex_labor_source,
                //mlgu2014_ex_housing = ((double)x.mlgu2014_ex_housing).ToString("C2"),
                //mlgu2014_ex_housing_source = x.mlgu2014_ex_housing_source,
                //mlgu2014_ex_social_welfare = ((double)x.mlgu2014_ex_social_welfare).ToString("C2"),
                //mlgu2014_ex_social_welfare_source = x.mlgu2014_ex_social_welfare_source,
                //mlgu2014_ex_economic = ((double)x.mlgu2014_ex_economic).ToString("C2"),
                //mlgu2014_ex_economic_source = x.mlgu2014_ex_economic_source,
                //mlgu2014_ex_other_purposes = ((double)x.mlgu2014_ex_other_purposes).ToString("C2"),
                //mlgu2014_ex_other_purposes_source = x.mlgu2014_ex_other_purposes_source,

                //mlgu2015_rev_ira_share = ((double)x.mlgu2015_rev_ira_share).ToString("C2"),
                //mlgu2015_rev_ira_share_source = x.mlgu2015_rev_ira_share_source,
                //mlgu2015_rev_locally_sourced = ((double)x.mlgu2015_rev_locally_sourced).ToString("C2"),
                //mlgu2015_rev_locally_sourced_source = x.mlgu2015_rev_locally_sourced_source,
                //mlgu2015_rev_other_revenues_total = ((double)x.mlgu2015_rev_other_revenues_total).ToString("C2"),
                //mlgu2015_rev_other_revenues_total_source = x.mlgu2015_rev_other_revenues_total_source,
                //mlgu2015_rev_total_lgu_income = ((double)x.mlgu2015_rev_total_lgu_income).ToString("C2"),
                //mlgu2015_rev_total_lgu_income_source = x.mlgu2015_rev_total_lgu_income_source,
                //mlgu2015_rev_total_lgu_income_per_capita = ((double)x.mlgu2015_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2015_rev_total_lgu_income_per_capita_source = x.mlgu2015_rev_total_lgu_income_per_capita_source,
                //mlgu2015_rev_devt_fund = ((double)x.mlgu2015_rev_devt_fund).ToString("C2"),
                //mlgu2015_rev_devt_fund_source = x.mlgu2015_rev_devt_fund_source,
                //mlgu2015_rev_devt_fund_per_capita = ((double)x.mlgu2015_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2015_rev_devt_fund_per_capita_source = x.mlgu2015_rev_devt_fund_per_capita_source,
                //mlgu2015_ex_gen_public = ((double)x.mlgu2015_ex_gen_public).ToString("C2"),
                //mlgu2015_ex_gen_public_source = x.mlgu2015_ex_gen_public_source,
                //mlgu2015_ex_educ = ((double)x.mlgu2015_ex_educ).ToString("C2"),
                //mlgu2015_ex_educ_source = x.mlgu2015_ex_educ_source,
                //mlgu2015_ex_health = ((double)x.mlgu2015_ex_health).ToString("C2"),
                //mlgu2015_ex_health_source = x.mlgu2015_ex_health_source,
                //mlgu2015_ex_labor = ((double)x.mlgu2015_ex_labor).ToString("C2"),
                //mlgu2015_ex_labor_source = x.mlgu2015_ex_labor_source,
                //mlgu2015_ex_housing = ((double)x.mlgu2015_ex_housing).ToString("C2"),
                //mlgu2015_ex_housing_source = x.mlgu2015_ex_housing_source,
                //mlgu2015_ex_social_welfare = ((double)x.mlgu2015_ex_social_welfare).ToString("C2"),
                //mlgu2015_ex_social_welfare_source = x.mlgu2015_ex_social_welfare_source,
                //mlgu2015_ex_economic = ((double)x.mlgu2015_ex_economic).ToString("C2"),
                //mlgu2015_ex_economic_source = x.mlgu2015_ex_economic_source,
                //mlgu2015_ex_other_purposes = ((double)x.mlgu2015_ex_other_purposes).ToString("C2"),
                //mlgu2015_ex_other_purposes_source = x.mlgu2015_ex_other_purposes_source,

                //mlgu2016_rev_ira_share = ((double)x.mlgu2016_rev_ira_share).ToString("C2"),
                //mlgu2016_rev_ira_share_source = x.mlgu2016_rev_ira_share_source,
                //mlgu2016_rev_locally_sourced = ((double)x.mlgu2016_rev_locally_sourced).ToString("C2"),
                //mlgu2016_rev_locally_sourced_source = x.mlgu2016_rev_locally_sourced_source,
                //mlgu2016_rev_other_revenues_total = ((double)x.mlgu2016_rev_other_revenues_total).ToString("C2"),
                //mlgu2016_rev_other_revenues_total_source = x.mlgu2016_rev_other_revenues_total_source,
                //mlgu2016_rev_total_lgu_income = ((double)x.mlgu2016_rev_total_lgu_income).ToString("C2"),
                //mlgu2016_rev_total_lgu_income_source = x.mlgu2016_rev_total_lgu_income_source,
                //mlgu2016_rev_total_lgu_income_per_capita = ((double)x.mlgu2016_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2016_rev_total_lgu_income_per_capita_source = x.mlgu2016_rev_total_lgu_income_per_capita_source,
                //mlgu2016_rev_devt_fund = ((double)x.mlgu2016_rev_devt_fund).ToString("C2"),
                //mlgu2016_rev_devt_fund_source = x.mlgu2016_rev_devt_fund_source,
                //mlgu2016_rev_devt_fund_per_capita = ((double)x.mlgu2016_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2016_rev_devt_fund_per_capita_source = x.mlgu2016_rev_devt_fund_per_capita_source,
                //mlgu2016_ex_gen_public = ((double)x.mlgu2016_ex_gen_public).ToString("C2"),
                //mlgu2016_ex_gen_public_source = x.mlgu2016_ex_gen_public_source,
                //mlgu2016_ex_educ = ((double)x.mlgu2016_ex_educ).ToString("C2"),
                //mlgu2016_ex_educ_source = x.mlgu2016_ex_educ_source,
                //mlgu2016_ex_health = ((double)x.mlgu2016_ex_health).ToString("C2"),
                //mlgu2016_ex_health_source = x.mlgu2016_ex_health_source,
                //mlgu2016_ex_labor = ((double)x.mlgu2016_ex_labor).ToString("C2"),
                //mlgu2016_ex_labor_source = x.mlgu2016_ex_labor_source,
                //mlgu2016_ex_housing = ((double)x.mlgu2016_ex_housing).ToString("C2"),
                //mlgu2016_ex_housing_source = x.mlgu2016_ex_housing_source,
                //mlgu2016_ex_social_welfare = ((double)x.mlgu2016_ex_social_welfare).ToString("C2"),
                //mlgu2016_ex_social_welfare_source = x.mlgu2016_ex_social_welfare_source,
                //mlgu2016_ex_economic = ((double)x.mlgu2016_ex_economic).ToString("C2"),
                //mlgu2016_ex_economic_source = x.mlgu2016_ex_economic_source,
                //mlgu2016_ex_other_purposes = ((double)x.mlgu2016_ex_other_purposes).ToString("C2"),
                //mlgu2016_ex_other_purposes_source = x.mlgu2016_ex_other_purposes_source,

                //mlgu2017_rev_ira_share = ((double)x.mlgu2017_rev_ira_share).ToString("C2"),
                //mlgu2017_rev_ira_share_source = x.mlgu2017_rev_ira_share_source,
                //mlgu2017_rev_locally_sourced = ((double)x.mlgu2017_rev_locally_sourced).ToString("C2"),
                //mlgu2017_rev_locally_sourced_source = x.mlgu2017_rev_locally_sourced_source,
                //mlgu2017_rev_other_revenues_total = ((double)x.mlgu2017_rev_other_revenues_total).ToString("C2"),
                //mlgu2017_rev_other_revenues_total_source = x.mlgu2017_rev_other_revenues_total_source,
                //mlgu2017_rev_total_lgu_income = ((double)x.mlgu2017_rev_total_lgu_income).ToString("C2"),
                //mlgu2017_rev_total_lgu_income_source = x.mlgu2017_rev_total_lgu_income_source,
                //mlgu2017_rev_total_lgu_income_per_capita = ((double)x.mlgu2017_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2017_rev_total_lgu_income_per_capita_source = x.mlgu2017_rev_total_lgu_income_per_capita_source,
                //mlgu2017_rev_devt_fund = ((double)x.mlgu2017_rev_devt_fund).ToString("C2"),
                //mlgu2017_rev_devt_fund_source = x.mlgu2017_rev_devt_fund_source,
                //mlgu2017_rev_devt_fund_per_capita = ((double)x.mlgu2017_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2017_rev_devt_fund_per_capita_source = x.mlgu2017_rev_devt_fund_per_capita_source,
                //mlgu2017_ex_gen_public = ((double)x.mlgu2017_ex_gen_public).ToString("C2"),
                //mlgu2017_ex_gen_public_source = x.mlgu2017_ex_gen_public_source,
                //mlgu2017_ex_educ = ((double)x.mlgu2017_ex_educ).ToString("C2"),
                //mlgu2017_ex_educ_source = x.mlgu2017_ex_educ_source,
                //mlgu2017_ex_health = ((double)x.mlgu2017_ex_health).ToString("C2"),
                //mlgu2017_ex_health_source = x.mlgu2017_ex_health_source,
                //mlgu2017_ex_labor = ((double)x.mlgu2017_ex_labor).ToString("C2"),
                //mlgu2017_ex_labor_source = x.mlgu2017_ex_labor_source,
                //mlgu2017_ex_housing = ((double)x.mlgu2017_ex_housing).ToString("C2"),
                //mlgu2017_ex_housing_source = x.mlgu2017_ex_housing_source,
                //mlgu2017_ex_social_welfare = ((double)x.mlgu2017_ex_social_welfare).ToString("C2"),
                //mlgu2017_ex_social_welfare_source = x.mlgu2017_ex_social_welfare_source,
                //mlgu2017_ex_economic = ((double)x.mlgu2017_ex_economic).ToString("C2"),
                //mlgu2017_ex_economic_source = x.mlgu2017_ex_economic_source,
                //mlgu2017_ex_other_purposes = ((double)x.mlgu2017_ex_other_purposes).ToString("C2"),
                //mlgu2017_ex_other_purposes_source = x.mlgu2017_ex_other_purposes_source,

                //mlgu2018_rev_ira_share = ((double)x.mlgu2018_rev_ira_share).ToString("C2"),
                //mlgu2018_rev_ira_share_source = x.mlgu2018_rev_ira_share_source,
                //mlgu2018_rev_locally_sourced = ((double)x.mlgu2018_rev_locally_sourced).ToString("C2"),
                //mlgu2018_rev_locally_sourced_source = x.mlgu2018_rev_locally_sourced_source,
                //mlgu2018_rev_other_revenues_total = ((double)x.mlgu2018_rev_other_revenues_total).ToString("C2"),
                //mlgu2018_rev_other_revenues_total_source = x.mlgu2018_rev_other_revenues_total_source,
                //mlgu2018_rev_total_lgu_income = ((double)x.mlgu2018_rev_total_lgu_income).ToString("C2"),
                //mlgu2018_rev_total_lgu_income_source = x.mlgu2018_rev_total_lgu_income_source,
                //mlgu2018_rev_total_lgu_income_per_capita = ((double)x.mlgu2018_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2018_rev_total_lgu_income_per_capita_source = x.mlgu2018_rev_total_lgu_income_per_capita_source,
                //mlgu2018_rev_devt_fund = ((double)x.mlgu2018_rev_devt_fund).ToString("C2"),
                //mlgu2018_rev_devt_fund_source = x.mlgu2018_rev_devt_fund_source,
                //mlgu2018_rev_devt_fund_per_capita = ((double)x.mlgu2018_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2018_rev_devt_fund_per_capita_source = x.mlgu2018_rev_devt_fund_per_capita_source,
                //mlgu2018_ex_gen_public = ((double)x.mlgu2018_ex_gen_public).ToString("C2"),
                //mlgu2018_ex_gen_public_source = x.mlgu2018_ex_gen_public_source,
                //mlgu2018_ex_educ = ((double)x.mlgu2018_ex_educ).ToString("C2"),
                //mlgu2018_ex_educ_source = x.mlgu2018_ex_educ_source,
                //mlgu2018_ex_health = ((double)x.mlgu2018_ex_health).ToString("C2"),
                //mlgu2018_ex_health_source = x.mlgu2018_ex_health_source,
                //mlgu2018_ex_labor = ((double)x.mlgu2018_ex_labor).ToString("C2"),
                //mlgu2018_ex_labor_source = x.mlgu2018_ex_labor_source,
                //mlgu2018_ex_housing = ((double)x.mlgu2018_ex_housing).ToString("C2"),
                //mlgu2018_ex_housing_source = x.mlgu2018_ex_housing_source,
                //mlgu2018_ex_social_welfare = ((double)x.mlgu2018_ex_social_welfare).ToString("C2"),
                //mlgu2018_ex_social_welfare_source = x.mlgu2018_ex_social_welfare_source,
                //mlgu2018_ex_economic = ((double)x.mlgu2018_ex_economic).ToString("C2"),
                //mlgu2018_ex_economic_source = x.mlgu2018_ex_economic_source,
                //mlgu2018_ex_other_purposes = ((double)x.mlgu2018_ex_other_purposes).ToString("C2"),
                //mlgu2018_ex_other_purposes_source = x.mlgu2018_ex_other_purposes_source,

                //mlgu2019_rev_ira_share = ((double)x.mlgu2019_rev_ira_share).ToString("C2"),
                //mlgu2019_rev_ira_share_source = x.mlgu2019_rev_ira_share_source,
                //mlgu2019_rev_locally_sourced = ((double)x.mlgu2019_rev_locally_sourced).ToString("C2"),
                //mlgu2019_rev_locally_sourced_source = x.mlgu2019_rev_locally_sourced_source,
                //mlgu2019_rev_other_revenues_total = ((double)x.mlgu2019_rev_other_revenues_total).ToString("C2"),
                //mlgu2019_rev_other_revenues_total_source = x.mlgu2019_rev_other_revenues_total_source,
                //mlgu2019_rev_total_lgu_income = ((double)x.mlgu2019_rev_total_lgu_income).ToString("C2"),
                //mlgu2019_rev_total_lgu_income_source = x.mlgu2019_rev_total_lgu_income_source,
                //mlgu2019_rev_total_lgu_income_per_capita = ((double)x.mlgu2019_rev_total_lgu_income_per_capita).ToString("C2"),
                //mlgu2019_rev_total_lgu_income_per_capita_source = x.mlgu2019_rev_total_lgu_income_per_capita_source,
                //mlgu2019_rev_devt_fund = ((double)x.mlgu2019_rev_devt_fund).ToString("C2"),
                //mlgu2019_rev_devt_fund_source = x.mlgu2019_rev_devt_fund_source,
                //mlgu2019_rev_devt_fund_per_capita = ((double)x.mlgu2019_rev_devt_fund_per_capita).ToString("C2"),
                //mlgu2019_rev_devt_fund_per_capita_source = x.mlgu2019_rev_devt_fund_per_capita_source,
                //mlgu2019_ex_gen_public = ((double)x.mlgu2019_ex_gen_public).ToString("C2"),
                //mlgu2019_ex_gen_public_source = x.mlgu2019_ex_gen_public_source,
                //mlgu2019_ex_educ = ((double)x.mlgu2019_ex_educ).ToString("C2"),
                //mlgu2019_ex_educ_source = x.mlgu2019_ex_educ_source,
                //mlgu2019_ex_health = ((double)x.mlgu2019_ex_health).ToString("C2"),
                //mlgu2019_ex_health_source = x.mlgu2019_ex_health_source,
                //mlgu2019_ex_labor = ((double)x.mlgu2019_ex_labor).ToString("C2"),
                //mlgu2019_ex_labor_source = x.mlgu2019_ex_labor_source,
                //mlgu2019_ex_housing = ((double)x.mlgu2019_ex_housing).ToString("C2"),
                //mlgu2019_ex_housing_source = x.mlgu2019_ex_housing_source,
                //mlgu2019_ex_social_welfare = ((double)x.mlgu2019_ex_social_welfare).ToString("C2"),
                //mlgu2019_ex_social_welfare_source = x.mlgu2019_ex_social_welfare_source,
                //mlgu2019_ex_economic = ((double)x.mlgu2019_ex_economic).ToString("C2"),
                //mlgu2019_ex_economic_source = x.mlgu2019_ex_economic_source,
                //mlgu2019_ex_other_purposes = ((double)x.mlgu2019_ex_other_purposes).ToString("C2"),
                //mlgu2019_ex_other_purposes_source = x.mlgu2019_ex_other_purposes_source,


            }).FirstOrDefault();

            return Ok(result);

        }
        

        #endregion


        #region 
        //get data from dof_blgf_financial_data table that will populate the pre-filled items in DATA ENTRY

        private IQueryable<dof_blgf_financial_data> GetDOFBLGFData(
               DataLayer.AngularFilterModel item)
        {
            var model = db.dof_blgf_financial_data.AsQueryable();
            
            if (item.region_code != null)
            {
                model = model.Where(m => m.region_code == item.region_code);
            }
            if (item.prov_code != null)
            {
                model = model.Where(m => m.prov_code == item.prov_code);
            }
            if (item.city_code != null)
            {
                model = model.Where(m => m.city_code == item.city_code);
            }

            return model;
        }

        #region  getting DOF-BLGF financial data, per year
        [HttpPost]
        [Route("api/export/muni_financial_profile/blgf_financial_data")]
        public IActionResult export_financial_data_2011(int id, AngularFilterModel item)
        {
            var model = GetDOFBLGFData(item);
            var current_model = GetData(item);

            model = model.Where(x => x.year_id == id);
                                    
            var result = model.Select(x => new
            {
                year_id = x.year_id,
                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                psgc_code = x.psgc_code,

                ira_share = x.ira_share,
                locally_shared_revenues = x.locally_shared_revenues,
                other_revenues_total = x.other_revenues_total,
                total_lgu_income = x.total_lgu_income,

                result_development_fund = x.ira_share * 0.2,
                
                expenditures_gen_public_services = x.expenditures_gen_public_services,
                expenditures_educ_culture_etc = x.expenditures_educ_culture_etc,
                expenditures_health_services = x.expenditures_health_services,
                expenditures_labor_and_employment = x.expenditures_labor_and_employment,
                expenditures_housing_comm_devt = x.expenditures_housing_comm_devt,
                expenditures_social_welfare_services = x.expenditures_social_welfare_services,
                expenditures_economic_services = x.expenditures_economic_services,
                expenditures_other_purposes = x.expenditures_other_purposes,

                total_lgu_income_raw = Math.Round((double)x.total_lgu_income, 1, MidpointRounding.ToEven),
                development_fund_raw = Math.Round((double)x.ira_share * 0.2, 1, MidpointRounding.ToEven),


                //ira_share = ((double)x.ira_share).ToString("C2"),
                //locally_shared_revenues = ((double)x.locally_shared_revenues).ToString("C2"),
                //other_revenues_total = ((double)x.other_revenues_total).ToString("C2"),
                //total_lgu_income = ((double)x.total_lgu_income).ToString("C2"),

                //result_development_fund = ((double)x.ira_share * 0.2).ToString("C2"),

                //total_lgu_income_raw = Math.Round((double) x.total_lgu_income, 1, MidpointRounding.ToEven),
                //development_fund_raw = Math.Round((double) x.ira_share * 0.2, 1, MidpointRounding.ToEven),

                //expenditures_gen_public_services = ((double)x.expenditures_gen_public_services).ToString("C2"),
                //expenditures_educ_culture_etc = ((double)x.expenditures_educ_culture_etc).ToString("C2"),
                //expenditures_health_services = ((double)x.expenditures_health_services).ToString("C2"),
                //expenditures_labor_and_employment = ((double)x.expenditures_labor_and_employment).ToString("C2"),
                //expenditures_housing_comm_devt = ((double)x.expenditures_housing_comm_devt).ToString("C2"),
                //expenditures_social_welfare_services = ((double)x.expenditures_social_welfare_services).ToString("C2"),
                //expenditures_economic_services = ((double)x.expenditures_economic_services).ToString("C2"),
                //expenditures_other_purposes = ((double)x.expenditures_other_purposes).ToString("C2"),

            }).FirstOrDefault();
            

            return Ok(result);
        }

        #endregion

        #endregion

        #region 
        //get data from lgpms_data table that will populate the pre-filled items in DATA ENTRY

        private IQueryable<lgpms_data> GetLGPMSData(
               DataLayer.AngularFilterModel item)
        {
            var model = db.lgpms_data.AsQueryable();

            if (item.region_code != null)
            {
                model = model.Where(m => m.region_code == item.region_code);
            }
            if (item.prov_code != null)
            {
                model = model.Where(m => m.prov_code == item.prov_code);
            }
            if (item.city_code != null)
            {
                model = model.Where(m => m.city_code == item.city_code);
            }

            return model;
        }

        [HttpPost]
        [Route("api/export/muni_financial_profile/lgpms_all_data")]
        public IActionResult export_lgpms_data_all_data(AngularFilterModel filt)
        {
            var model = GetLGPMSData(filt);

            var result = model.Select(x => new
            {
                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                psgc_code = x.psgc_code,
                
                administrative_governance_2009 = Math.Round((double)x.administrative_governance_2009, 2),
                administrative_governance_2010 = Math.Round((double)x.administrative_governance_2010, 2),
                administrative_governance_2011 = Math.Round((double)x.administrative_governance_2011, 2),
                administrative_governance_2012 = Math.Round((double)x.administrative_governance_2012, 2),

                social_governance_2009 = Math.Round((double)x.social_governance_2009, 2),
                social_governance_2010 = Math.Round((double)x.social_governance_2010, 2),
                social_governance_2011 = Math.Round((double)x.social_governance_2011, 2),
                social_governance_2012 = Math.Round((double)x.social_governance_2012, 2),

                economic_governance_2009 = Math.Round((double)x.economic_governance_2009, 2),
                economic_governance_2010 = Math.Round((double)x.economic_governance_2010, 2),
                economic_governance_2011 = Math.Round((double)x.economic_governance_2011, 2),
                economic_governance_2012 = Math.Round((double)x.economic_governance_2012, 2),

                environmental_governance_2009 = Math.Round((double)x.environmental_governance_2009 + 0.00, 2),
                environmental_governance_2010 = Math.Round((double)x.environmental_governance_2010, 2),
                environmental_governance_2011 = Math.Round((double)x.environmental_governance_2011, 2),
                environmental_governance_2012 = Math.Round((double)x.environmental_governance_2012, 2),

                valuing_fundamentals_of_good_gov_2009 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2009, 2),
                valuing_fundamentals_of_good_gov_2010 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2010, 2),
                valuing_fundamentals_of_good_gov_2011 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2011, 2),
                valuing_fundamentals_of_good_gov_2012 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2012, 2),
                
                overall_performance_index_2009 = Math.Round((double)x.overall_performance_index_2009, 2),
                overall_performance_index_2010 = Math.Round((double)x.overall_performance_index_2010, 2),
                overall_performance_index_2011 = Math.Round((double)x.overall_performance_index_2011, 2),
                overall_performance_index_2012 = Math.Round((double)x.overall_performance_index_2012, 2),


            }).FirstOrDefault();

            return Ok(result);
        }

        #endregion
        
        #region SAVE FUNCTION
        [Route("api/offline/v1/muni_financial_profile/save")]
        public async Task<IActionResult> Save (muni_financial_profile model, bool? api)
        {            
            var record = db.muni_financial_profile.AsNoTracking().FirstOrDefault(x => x.muni_financial_profile_id == model.muni_financial_profile_id && x.is_deleted != true);
            
            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                    model.approval_id = 3;
                }
                else
                {
                    model.push_status_id = 1;
                }

                db.muni_financial_profile.Add(model);                

                try
                {
                    await db.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }

            else
            {
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;               

                db.Entry(model).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
        }
        #endregion
        
        #region
        [Route("api/offline/v1/muni_financial_profile/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.muni_financial_profile.FirstOrDefault(x => x.muni_financial_profile_id == id && x.is_deleted != true);

            if (model == null)
            {
                var item = new muni_financial_profile();

                item.muni_financial_profile_id = id;
                item.push_status_id = 3;
                item.created_by = 0;

                item.created_date = DateTime.Now;
                item.approval_id = 3;
                item.is_deleted = false;

                model = item;
            }
            else
            {
                model.push_status_id = 3;
                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;
                model.approval_id = 3;
            }

            return Ok(model);
        }
        #endregion

        #region API get and post

        [HttpPost]
        [Route("Sync/Get/muni_financial_profile")]
        public async Task<ActionResult> GetOnline(string username, string password, string city_code = null, Guid? record_id = null)
        {
            string token = username + ":" + password;
            byte[] toBytes = Encoding.ASCII.GetBytes(token);
            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/municipal_financial_profile/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<muni_financial_profile>>(responseJson.Result);
                    
                    foreach (var item in model.ToList())
                    {
                        await Save(item, true);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

        }

        [Route("api/offline/v1/muni_financial_profile/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid muni_financial_profile_id, int push_status_id)
        {
            var muni_financial_profile = db.muni_financial_profile.Find(muni_financial_profile_id);

            if (muni_financial_profile == null)
            {
                return BadRequest("Error");
            }

            muni_financial_profile.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Sync/Post/muni_financial_profile")]
        public async Task<ActionResult> PostOnline(string username, string password, Guid? record_id = null)
        {
            string token = username + ":" + password;
            byte[] toBytes = Encoding.ASCII.GetBytes(token);
            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                var items_preselected = db.muni_financial_profile.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.muni_financial_profile.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/muni_financial_profile/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            await db.SaveChangesAsync();
                            //return BadRequest();
                        }
                    }
                }
                else
                {
                    var items = db.muni_financial_profile.Where(x => x.push_status_id == 5 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/muni_financial_profile/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            await db.SaveChangesAsync();
                            //return BadRequest();
                        }
                    }
                }
            }
            return Ok();
        }
        
        #endregion


    }
}
