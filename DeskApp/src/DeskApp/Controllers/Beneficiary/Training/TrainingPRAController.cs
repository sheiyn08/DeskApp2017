using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeskApp.Data;

using DeskApp.DataLayer;
using Microsoft.EntityFrameworkCore; 


namespace DeskApp.Controllers
{
 
    [Route("api/offline/v1/trainings/pra")]
    public class TrainingPRAController : Controller
    {

        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        [HttpPost]
        [Route("export/list")]
        public IActionResult export_pra_list(AngularFilterModel model)
        {


            var list = db.mibf_prioritization.Select(x => new
            {
                region = db.lib_region.FirstOrDefault(c => c.region_code == x.region_code).region_name,
                province = db.lib_province.FirstOrDefault(c => c.prov_code == x.prov_code).prov_name,
                city = db.lib_city.FirstOrDefault(c => c.city_code == x.city_code).city_name,

                project = db.community_training.FirstOrDefault(c => c.community_training_id == x.community_training_id).lib_fund_source.name,
                cycle = db.community_training.FirstOrDefault(c => c.community_training_id == x.community_training_id).lib_cycle.name,

                rank = x.rank,
                sub_project_title = x.project_name,
                lead_barangay = db.lib_brgy.FirstOrDefault(c => c.brgy_code == x.brgy_code).brgy_name,
                proposed_kc_grant_amount = x.kc_amount,
                proposed_lcc_amount = x.lcc_amount,
                is_priority = x.is_priority == true ? "Yes" : "No"

            });

            return Ok(list);
        }


        [HttpGet]
        [Route("selected")]
        public IActionResult GetSelected(Guid id)
        { 

            return Ok(db.mibf_prioritization.FirstOrDefault(x => x.mibf_prioritization_id == id));

        }
        [Route("activity_matching")]
        public IActionResult GetMatched(int fund_source_id, int cycle_id, int city_code)
        {
            var model = db.community_training.Where(x => x.fund_source_id == fund_source_id && x.cycle_id == cycle_id
                                                         && x.city_code == city_code && x.training_category_id == 11 &&
                                                         x.is_deleted != true);
           

            return Ok(model.Select(x => new { Id = x.community_training_id, Name = x.training_title }));

        }

        /// <summary>
        /// Get PRA List using community training id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("spi_matching")]
        public IActionResult GetSPIMatched(Guid id)
        {
            var model = db.mibf_prioritization.Where(x => x.community_training_id == id && x.is_deleted != true);


            return Ok(model.Select(x => new { Id = x.mibf_prioritization_id, Name = x.project_name }));

        }



        public TrainingPRAController(ApplicationDbContext context)
        {
            db = context;

        }


        [Route("get_mapped")]
        public IActionResult GetCSW(
                     Guid community_training_id
           )
        {

            var training = db.mibf_prioritization.Where(x => x.community_training_id == community_training_id && x.is_deleted != true);



           


            return Ok(training.ToList());
        }

        [Route("save")]
        public async Task<IActionResult> SaveCSW(mibf_prioritization model, bool? api)
        {


            var record = db.mibf_prioritization.AsNoTracking().FirstOrDefault(x => x.mibf_prioritization_id == model.mibf_prioritization_id);

            var training = db.community_training.Find(model.community_training_id);

            model.region_code = training.region_code;
            model.prov_code = training.prov_code;
            model.city_code = training.city_code;


            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;

                    int rank =
                     db.mibf_prioritization.Where(
                         x => x.community_training_id == model.community_training_id && x.is_deleted != true).Count();

                    model.rank = rank + 1;
                }


                model.created_by = 0;
                model.created_date = DateTime.Now;
                model.mibf_prioritization_id = Guid.NewGuid();

                model.is_deleted = false;
                db.mibf_prioritization.Add(model);


                try
                {
                    await db.SaveChangesAsync();
                    return Ok(model);
                }
                catch (DbUpdateConcurrencyException)
                {


                    return BadRequest();
                }
            }
            else
            {
                model.push_date = null;


                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

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



        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {


            var record = db.mibf_prioritization.FirstOrDefault(x => x.mibf_prioritization_id == id);

            if (db.sub_project.FirstOrDefault(x => x.mibf_prioritization_id == id) != null)
            {
                var proj = db.sub_project.FirstOrDefault(x => x.mibf_prioritization_id == id).sub_project_id;

                return
                    BadRequest(
                        "Project has already been selected as Result of Prioritization in the SPI Module, with sub project Id: " +
                        proj);
            }


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }
    }
}