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
    public class LGPMSDataController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;

        public LGPMSDataController(ApplicationDbContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region getData
        private IQueryable<lgpms_data> GetData(
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
            //if (item.psgc_code != null)
            //{
            //    model = model.Where(m => m.psgc_code == item.psgc_code);
            //}
            
            
            return model;
        }
        #endregion

        [HttpPost]
        [Route("api/export/lgpms_data/all_data")]
        public IActionResult export_lgpms_data_all_data(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var result = model.Select(x => new
            {
                lib_region_region_name = x.lib_region.region_name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_city_city_name = x.lib_city.city_name,
                psgc_code = x.psgc_code,

                overall_performance_index_2009 = Math.Round((double)x.overall_performance_index_2009, 2),
                overall_performance_index_2010 = Math.Round((double)x.overall_performance_index_2010, 2),
                overall_performance_index_2011 = Math.Round((double)x.overall_performance_index_2011, 2),
                overall_performance_index_2012 = Math.Round((double)x.overall_performance_index_2012, 2),

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

                environmental_governance_2009 = Math.Round((double)x.environmental_governance_2009, 2),
                environmental_governance_2010 = Math.Round((double)x.environmental_governance_2010, 2),
                environmental_governance_2011 = Math.Round((double)x.environmental_governance_2011, 2),
                environmental_governance_2012 = Math.Round((double)x.environmental_governance_2012, 2),

                valuing_fundamentals_of_good_gov_2009 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2009, 2),
                valuing_fundamentals_of_good_gov_2010 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2010, 2),
                valuing_fundamentals_of_good_gov_2011 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2011, 2),
                valuing_fundamentals_of_good_gov_2012 = Math.Round((double)x.valuing_fundamentals_of_good_gov_2012, 2),


            }).ToList();

            return Ok(result);
        }
        
        
        //[Route("api/offline/v1/lgpms_data/get")]
        //public IActionResult Get(int id)
        //{

        //    var model = db.lgpms_data.FirstOrDefault(x => x.lgpms_data_id == id && x.is_deleted != true);

        //    return Ok(model);
        //}


    }
}
