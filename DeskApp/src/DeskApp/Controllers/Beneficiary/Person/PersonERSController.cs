using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeskApp.DataLayer;
using DeskApp.Data;
using Microsoft.EntityFrameworkCore;


using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DeskApp.Controllers
{
    public class PersonERSController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

        private readonly ApplicationDbContext db;

        public PersonERSController(ApplicationDbContext context)
        {
            db = context;

        }



        #region ers

        [Route("api/offline/v1/profiles/ers/get")]
        public IActionResult GetPersonERS(Guid? id)
        {

            var model = db.person_ers_work
                .Include(x => x.lib_ers_current_work)
                .Include(x => x.sub_project_ers)
                .Where(x => x.person_profile_id == id && x.is_deleted != true).
                            Select(x => new
                            {
                                x.rate_hour,
                                x.rate_day,
                                x.work_hours,
                                x.work_days,
                                x.plan_cash,
                                x.plan_lcc,
                                x.actual_cash,
                                x.actual_lcc,

                                lib_ers_current_work_name = x.lib_ers_current_work.name,

                                
                                date_reported = x.sub_project_ers.date_reported,
                                date_ended = x.sub_project_ers.date_ended,
                                date_started = x.sub_project_ers.date_started
                            });


            return Ok(model);
        }







        /// <summary>
        /// Gets Basic Person Profile and Worker Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/offline/v1/spi/ers/worker/get")]
        public IActionResult GetSpiErsListWorkers(Guid id)
        {


            var model = db.person_ers_work
                .Include(x => x.lib_ers_current_work)
                .Include(x => x.person_profile)
                .Where(x => x.sub_project_ers_id == id && x.is_deleted != true)
                .OrderBy(x => x.created_date);

            var result = from s in model
                         select new
                         {
                             s.person_profile.first_name,
                             s.person_profile.last_name,
                             s.person_profile.middle_name,
                             s.person_profile.birthdate,
                             s.person_profile.person_profile_id,
                             s.ers_current_work_id,
                             ers_current_work_name = s.lib_ers_current_work.name,
                             
                             is_skilled = s.lib_ers_current_work.is_skilled,

                             s.rate_day,
                             s.rate_hour,
                             s.actual_cash,
                             s.actual_lcc,
                             s.plan_cash,
                             s.plan_lcc,
                             s.work_days,
                             s.work_hours,

                             s.unit_hauling,
                             s.percent,
                             s.rate_hauling,
                             s.work_hauling,

                             s.sub_project_ers_id,

                             s.person_ers_work_id,
                             s.person_profile.sex,
                             s.person_profile.contact_no,

                         };

            return Ok(result);
        }




        /// <summary>
        /// Gets Basic Person Profile and Worker Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/offline/v1/spi/ers/worker/get/summary")]
        public IActionResult GetSpiErsListWorkersSummary(Guid id)
        {


            var model = db.person_ers_work
                .Include(x => x.lib_ers_current_work)
                .Include(x => x.person_profile)
                .Where(x => x.sub_project_ers_id == id && x.is_deleted != true && x.person_profile.is_deleted != true).ToList();


            var result = model.GroupBy(x => new { x.person_profile.sex, x.lib_ers_current_work.is_skilled })
                            .Select(c => new
                            {
                                category = (c.Key.is_skilled == true ? "Skilled-" : "Unskilled-") + (c.Key.sex == true ? "Male" : "Female"),
                                number = c.Count(),
                                cash = c.Sum(x => x.actual_cash),
                                inkind = c.Sum(x => x.actual_lcc),
                                total = c.Sum(x => x.actual_cash) + c.Sum(x => x.actual_lcc)
                            });

            return Ok(result);
        }
        #endregion  
    }
}