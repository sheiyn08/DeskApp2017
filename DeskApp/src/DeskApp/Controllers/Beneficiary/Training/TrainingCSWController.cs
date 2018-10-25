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

    [Route("api/offline/v1/trainings/csw")]
    public class TrainingCSWController : Controller
    {

        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public TrainingCSWController(ApplicationDbContext context)
        {
            db = context;

        }


        [Route("get_mapped")]
        public IActionResult GetCSW(
                     Guid community_training_id
           )
        {

            var training = db.mibf_criteria.Where(x => x.community_training_id == community_training_id && x.is_deleted != true);



      


            return Ok(training.ToList());
        }

        //checking if Rate is 100:
        [HttpGet]
        [Route("check_total_rate")]
        public bool check_total_rate(Guid id)
        {
            double? total_rate = db.mibf_criteria
                .Where(x => x.community_training_id == id && x.is_deleted != true)
                .Sum(x => x.rate);
            
            //total_rate = total_rate + model.rate;

            return total_rate >= 100;
        }
        
        [Route("save")]
        public async Task<IActionResult> SaveCriteria(mibf_criteria model, bool? api)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var record = db.mibf_criteria.AsNoTracking().FirstOrDefault(x => x.mibf_criteria_id == model.mibf_criteria_id);
            var main_record = db.community_training.FirstOrDefault(x => x.community_training_id == model.community_training_id);

            if (record == null)
            {
                double? rates = db.mibf_criteria.Where(x => x.community_training_id == model.community_training_id && x.is_deleted != true).Sum(x => x.rate);
                rates = rates + model.rate;
                
                if (rates > 100)
                {
                    return BadRequest("Total Rate reached 100% already. Please check.");
                }

                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;

                    //v3.1 if new rate and criteria is added, update main training record as well
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;
                    db.Entry(main_record).State = EntityState.Modified;
                }
                else
                {
                    model.push_status_id = 1;
                }
                                
                db.mibf_criteria.Add(model);                

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
                double? rates = db.mibf_criteria.Where(x => x.community_training_id == model.community_training_id && x.is_deleted != true && x.mibf_criteria_id != model.mibf_criteria_id).Sum(x => x.rate);
                rates = rates + model.rate;

                if (rates > 100)
                {
                    return BadRequest("Total Rate reached 100% already. Please check.");
                }  

                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    //v3.1 if rate and criteria is edited, update main training record as well
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;
                    db.Entry(main_record).State = EntityState.Modified;
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


        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var record = db.mibf_criteria.FirstOrDefault(x => x.mibf_criteria_id == id);
            var main_record = db.community_training.FirstOrDefault(x => x.community_training_id == record.community_training_id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            //v3.1 if new rate and criteria is deleted, update main training record as well
            main_record.push_status_id = 3;
            main_record.last_modified_by = 0;
            main_record.last_modified_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();

        }
    }
}