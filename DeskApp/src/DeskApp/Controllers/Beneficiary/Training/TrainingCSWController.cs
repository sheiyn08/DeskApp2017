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

        public static string url = @"http://ncddpdb.dswd.gov.ph";

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



        [Route("save")]
        public async Task<IActionResult> SaveCSW(mibf_criteria model, bool? api)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var record = db.mibf_criteria.AsNoTracking().FirstOrDefault(x => x.mibf_criteria_id == model.mibf_criteria_id);

            if (record == null)
            {


                double? rates =
                    db.mibf_criteria.Where(
                        x => x.community_training_id == model.community_training_id && x.is_deleted != true)
                        .Sum(x => x.rate);

                rates = rates + model.rate;

                if (rates > 100)
                {
                    return BadRequest("Total of Rates is > 100");
                }


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }


                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;
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

                double? rates =
                   db.mibf_criteria.Where(
                       x => x.community_training_id == model.community_training_id && x.is_deleted != true && x.mibf_criteria_id != model.mibf_criteria_id)
                       .Sum(x => x.rate);

                rates = rates + model.rate;

                if (rates > 100)
                {
                    return BadRequest("Total of Rates is > 100");
                }



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


            var record = db.mibf_criteria.FirstOrDefault(x => x.mibf_criteria_id == id);

            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }
    }
}