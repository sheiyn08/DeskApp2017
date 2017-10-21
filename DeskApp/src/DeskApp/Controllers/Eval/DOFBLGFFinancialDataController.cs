using DeskApp.Data;
using DeskApp.DataLayer;
using DeskApp.DataLayer.Eval;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.Controllers.Eval
{
    public class DOFBLGFFinancialDataController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

        private readonly ApplicationDbContext db;

        public DOFBLGFFinancialDataController(ApplicationDbContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            return View();
        }
        

        //getData:
        private IQueryable<dof_blgf_financial_data> GetData(
               DataLayer.AngularFilterModel item)
        {
            var model = db.dof_blgf_financial_data.AsQueryable();

            if (item.psgc_code != null)
            {
                model = model.Where(m => m.psgc_code == item.psgc_code);
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
            if (item.year_id != null)
            {
                model = model.Where(m => m.year_id == item.year_id);
            }


            //if (item.locally_shared_revenues != null)
            //{
            //    model = model.Where(m => m.locally_shared_revenues == item.locally_shared_revenues);
            //}
            //if (item.ira_share != null)
            //{
            //    model = model.Where(m => m.ira_share == item.ira_share);
            //}
            //if (item.other_revenues_total != null)
            //{
            //    model = model.Where(m => m.other_revenues_total == item.other_revenues_total);
            //}
            //if (item.other_shares_natl_tax != null)
            //{
            //    model = model.Where(m => m.other_shares_natl_tax == item.other_shares_natl_tax);
            //}
            //if (item.inter_local_transfers != null)
            //{
            //    model = model.Where(m => m.inter_local_transfers == item.inter_local_transfers);
            //}
            //if (item.extraordinary_receipts != null)
            //{
            //    model = model.Where(m => m.extraordinary_receipts == item.extraordinary_receipts);
            //}
            //if (item.total_lgu_income != null)
            //{
            //    model = model.Where(m => m.total_lgu_income == item.total_lgu_income);
            //}
            //if (item.expenditures_gen_public_services != null)
            //{
            //    model = model.Where(m => m.expenditures_gen_public_services == item.expenditures_gen_public_services);
            //}
            //if (item.expenditures_educ_culture_etc != null)
            //{
            //    model = model.Where(m => m.expenditures_educ_culture_etc == item.expenditures_educ_culture_etc);
            //}
            //if (item.expenditures_health_services != null)
            //{
            //    model = model.Where(m => m.expenditures_health_services == item.expenditures_health_services);
            //}
            //if (item.expenditures_labor_and_employment != null)
            //{
            //    model = model.Where(m => m.expenditures_labor_and_employment == item.expenditures_labor_and_employment);
            //}
            //if (item.expenditures_housing_comm_devt != null)
            //{
            //    model = model.Where(m => m.expenditures_housing_comm_devt == item.expenditures_housing_comm_devt);
            //}
            //if (item.expenditures_social_welfare_services != null)
            //{
            //    model = model.Where(m => m.expenditures_social_welfare_services == item.expenditures_social_welfare_services);
            //}
            //if (item.expenditures_economic_services != null)
            //{
            //    model = model.Where(m => m.expenditures_economic_services == item.expenditures_economic_services);
            //}
            //if (item.expenditures_other_purposes != null)
            //{
            //    model = model.Where(m => m.expenditures_other_purposes == item.expenditures_other_purposes);
            //}

            

            return model;
        }
        
        //#region

        //[HttpPost]
        //[Route("api/offline/v1/dof_blgf_financial_data/get_dto")]
        //public PagedCollection<dynamic> GetAll(AngularFilterModel item)
        //{

        //    var model = GetData(item);


        //    return new PagedCollection<dynamic>()
        //    {

        //        Items = model.OrderBy(x => x.year_id)
        //        .Select(x => new
        //        {
        //            mlgu_financial_data_record_id = x.dof_blgf_financial_data_record_id,
        //            lib_city_city_name = x.lib_city.city_name,
        //            lib_province_prov_name = x.lib_province.prov_name,
        //            lib_region_region_name = x.lib_region.region_name,
        //            year_id = x.year_id,

        //            psgc_code = x.psgc_code,
        //            locally_shared_revenues = x.locally_shared_revenues,
        //            ira_share = x.ira_share,
        //            other_revenues_total = x.other_revenues_total,
        //            other_shares_natl_tax = x.other_shares_natl_tax,
        //            inter_local_transfers = x.inter_local_transfers,
        //            extraordinary_receipts = x.extraordinary_receipts,
        //            total_lgu_income = x.total_lgu_income,

        //            expenditures_gen_public_services = x.expenditures_gen_public_services,
        //            expenditures_educ_culture_etc = x.expenditures_educ_culture_etc,
        //            expenditures_health_services = x.expenditures_health_services,
        //            expenditures_labor_and_employment = x.expenditures_labor_and_employment,
        //            expenditures_housing_comm_devt = x.expenditures_housing_comm_devt,
        //            expenditures_social_welfare_services = x.expenditures_social_welfare_services,
        //            expenditures_economic_services = x.expenditures_economic_services,
        //            expenditures_other_purposes = x.expenditures_other_purposes


        //        }).ToList(),

        //    };

        //}

        //#endregion

        #region For PREVIEW

        [HttpPost]
        [Route("api/export/dof_blgf_financial_data/financial_data_all_year")]
        public IActionResult export_financial_data_all_year(AngularFilterModel itemForExport)
        {
            var model = GetData(itemForExport);

            var result = model.Select(x => new
            {
                year_id = x.year_id,

                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                psgc_code = x.psgc_code,

                locally_shared_revenues = ((double)x.locally_shared_revenues).ToString("C2"),
                ira_share = ((double)x.ira_share).ToString("C2"),
                other_revenues_total = ((double)x.other_revenues_total).ToString("C2"),
                other_shares_natl_tax = ((double)x.other_shares_natl_tax).ToString("C2"),
                inter_local_transfers = ((double)x.inter_local_transfers).ToString("C2"),
                extraordinary_receipts = ((double)x.extraordinary_receipts).ToString("C2"),
                total_lgu_income = ((double)x.total_lgu_income).ToString("C2"),

                expenditures_gen_public_services = ((double)x.expenditures_gen_public_services).ToString("C2"),
                expenditures_educ_culture_etc = ((double)x.expenditures_educ_culture_etc).ToString("C2"),
                expenditures_health_services = ((double)x.expenditures_health_services).ToString("C2"),
                expenditures_labor_and_employment = ((double)x.expenditures_labor_and_employment).ToString("C2"),
                expenditures_housing_comm_devt = ((double)x.expenditures_housing_comm_devt).ToString("C2"),
                expenditures_social_welfare_services = ((double)x.expenditures_social_welfare_services).ToString("C2"),
                expenditures_economic_services = ((double)x.expenditures_economic_services).ToString("C2"),
                expenditures_other_purposes = ((double)x.expenditures_other_purposes).ToString("C2"),

                total_external_source = ((double)(x.ira_share + x.other_revenues_total)).ToString("C2"),
                total_lgu_expenditures = ((double)(x.expenditures_gen_public_services + x.expenditures_educ_culture_etc + x.expenditures_health_services + x.expenditures_labor_and_employment + x.expenditures_housing_comm_devt + x.expenditures_social_welfare_services + x.expenditures_economic_services + x.expenditures_other_purposes)).ToString("C2")

            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        [Route("api/export/dof_blgf_financial_data/financial_data_2011")]
        public IActionResult export_financial_data_2011(AngularFilterModel filt)
        {
            var model = GetData(filt);

            model = model.Where(x => x.year_id == 3); //2011

            var result = model.Select(x => new 
            {
                year_id = x.year_id,
                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                psgc_code = x.psgc_code,
                
                locally_shared_revenues = ((double)x.locally_shared_revenues).ToString("C2"),
                ira_share = ((double)x.ira_share).ToString("C2"),
                other_revenues_total = ((double)x.other_revenues_total).ToString("C2"),
                other_shares_natl_tax = ((double)x.other_shares_natl_tax).ToString("C2"),
                inter_local_transfers = ((double)x.inter_local_transfers).ToString("C2"),
                extraordinary_receipts = ((double)x.extraordinary_receipts).ToString("C2"),
                total_lgu_income = ((double)x.total_lgu_income).ToString("C2"),

                expenditures_gen_public_services = ((double)x.expenditures_gen_public_services).ToString("C2"),
                expenditures_educ_culture_etc = ((double)x.expenditures_educ_culture_etc).ToString("C2"),
                expenditures_health_services = ((double)x.expenditures_health_services).ToString("C2"),
                expenditures_labor_and_employment = ((double)x.expenditures_labor_and_employment).ToString("C2"),
                expenditures_housing_comm_devt = ((double)x.expenditures_housing_comm_devt).ToString("C2"),
                expenditures_social_welfare_services = ((double)x.expenditures_social_welfare_services).ToString("C2"),
                expenditures_economic_services = ((double)x.expenditures_economic_services).ToString("C2"),
                expenditures_other_purposes = ((double)x.expenditures_other_purposes).ToString("C2"),

                total_external_source = ((double)(x.ira_share + x.other_revenues_total)).ToString("C2"),
                total_lgu_expenditures = ((double)(x.expenditures_gen_public_services + x.expenditures_educ_culture_etc + x.expenditures_health_services + x.expenditures_labor_and_employment + x.expenditures_housing_comm_devt + x.expenditures_social_welfare_services + x.expenditures_economic_services + x.expenditures_other_purposes)).ToString("C2")

            }).ToList();

            return Ok(result);
        }


        #region MLGU financial data, 2012
        [HttpPost]
        [Route("api/export/dof_blgf_financial_data/financial_data_2012")]
        public IActionResult export_financial_data_2012(AngularFilterModel filt)
        {
            var model = GetData(filt);

            model = model.Where(x => x.year_id == 4); //2012

            var result = model.Select(x => new
            {
                year_id = x.year_id,
                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                psgc_code = x.psgc_code,

                locally_shared_revenues = ((double)x.locally_shared_revenues).ToString("C2"),
                ira_share = ((double)x.ira_share).ToString("C2"),
                other_revenues_total = ((double)x.other_revenues_total).ToString("C2"),
                other_shares_natl_tax = ((double)x.other_shares_natl_tax).ToString("C2"),
                inter_local_transfers = ((double)x.inter_local_transfers).ToString("C2"),
                extraordinary_receipts = ((double)x.extraordinary_receipts).ToString("C2"),
                total_lgu_income = ((double)x.total_lgu_income).ToString("C2"),

                expenditures_gen_public_services = ((double)x.expenditures_gen_public_services).ToString("C2"),
                expenditures_educ_culture_etc = ((double)x.expenditures_educ_culture_etc).ToString("C2"),
                expenditures_health_services = ((double)x.expenditures_health_services).ToString("C2"),
                expenditures_labor_and_employment = ((double)x.expenditures_labor_and_employment).ToString("C2"),
                expenditures_housing_comm_devt = ((double)x.expenditures_housing_comm_devt).ToString("C2"),
                expenditures_social_welfare_services = ((double)x.expenditures_social_welfare_services).ToString("C2"),
                expenditures_economic_services = ((double)x.expenditures_economic_services).ToString("C2"),
                expenditures_other_purposes = ((double)x.expenditures_other_purposes).ToString("C2"),

                total_external_source = ((double)(x.ira_share + x.other_revenues_total)).ToString("C2"),
                total_lgu_expenditures = ((double)(x.expenditures_gen_public_services + x.expenditures_educ_culture_etc + x.expenditures_health_services + x.expenditures_labor_and_employment + x.expenditures_housing_comm_devt + x.expenditures_social_welfare_services + x.expenditures_economic_services + x.expenditures_other_purposes)).ToString("C2")

            }).ToList();

            return Ok(result);

        }

        #endregion

        #region MLGU financial data, 2013
        [HttpPost]
        [Route("api/export/dof_blgf_financial_data/financial_data_2013")]
        public IActionResult export_financial_data_2013(AngularFilterModel filt)
        {
            var model = GetData(filt);

            model = model.Where(x => x.year_id == 5); //2013

            var result = model.Select(x => new
            {
                year_id = x.year_id,
                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                psgc_code = x.psgc_code,

                locally_shared_revenues = ((double)x.locally_shared_revenues).ToString("C2"),
                ira_share = ((double)x.ira_share).ToString("C2"),
                other_revenues_total = ((double)x.other_revenues_total).ToString("C2"),
                other_shares_natl_tax = ((double)x.other_shares_natl_tax).ToString("C2"),
                inter_local_transfers = ((double)x.inter_local_transfers).ToString("C2"),
                extraordinary_receipts = ((double)x.extraordinary_receipts).ToString("C2"),
                total_lgu_income = ((double)x.total_lgu_income).ToString("C2"),

                expenditures_gen_public_services = ((double)x.expenditures_gen_public_services).ToString("C2"),
                expenditures_educ_culture_etc = ((double)x.expenditures_educ_culture_etc).ToString("C2"),
                expenditures_health_services = ((double)x.expenditures_health_services).ToString("C2"),
                expenditures_labor_and_employment = ((double)x.expenditures_labor_and_employment).ToString("C2"),
                expenditures_housing_comm_devt = ((double)x.expenditures_housing_comm_devt).ToString("C2"),
                expenditures_social_welfare_services = ((double)x.expenditures_social_welfare_services).ToString("C2"),
                expenditures_economic_services = ((double)x.expenditures_economic_services).ToString("C2"),
                expenditures_other_purposes = ((double)x.expenditures_other_purposes).ToString("C2"),

                total_external_source = ((double)(x.ira_share + x.other_revenues_total)).ToString("C2"),
                total_lgu_expenditures = ((double)(x.expenditures_gen_public_services + x.expenditures_educ_culture_etc + x.expenditures_health_services + x.expenditures_labor_and_employment + x.expenditures_housing_comm_devt + x.expenditures_social_welfare_services + x.expenditures_economic_services + x.expenditures_other_purposes)).ToString("C2")

            }).ToList();

            return Ok(result);

        }

        #endregion


        //[Route("api/offline/v1/dof_blgf_financial_data/get")]
        //public IActionResult Get(int id)
        //{

        //    var model = db.dof_blgf_financial_data.FirstOrDefault(x => x.dof_blgf_financial_data_record_id == id && x.is_deleted != true);
            
        //    return Ok(model);
        //}


        #endregion








    }
}
