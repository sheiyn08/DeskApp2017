using DeskApp.Data;
using DeskApp.DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.Controllers
{
    public class GraphsController : Controller
    {
        private readonly ApplicationDbContext db;

        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

        public GraphsController(ApplicationDbContext context)
        {
            db = context;

        }

        public ActionResult Index()
        {
            return View();
        }
        public IActionResult grievances()
        {
            var result = db.grievance_record.Where(x => x.is_deleted != true).GroupBy(x => x.lib_grs_resolution_status.name)
                .Select(x => new
                {
                    Name = x.Key,
                    Count = x.Count(),
                });

            return Ok(result);

        }

        public IActionResult trainings()
        {
            var result = db.community_training
           
               
                .Where(x => x.is_deleted != true)
                
                .GroupBy(x => x.lib_training_category.name)
                
                .Select(x => new
                {
                    Name = x.Key,
                    Count = x.Count(),
                });

            return Ok(result);
        }
        public IActionResult sub_project()
        {
            var result = db.sub_project.Where(x => x.IsActive == true).GroupBy(x => x.physical_status_id)
                .Select(x => new
                {
                    Name =  x.Key == 1 ? "Completed" : x.Key == 2 ? "Ongoing" : "Not Yet Started",
                    Count = x.Count(),
                });

            return Ok(result);
        }

        public IActionResult volunteers()
        {
            var model = (from s in db.person_profile
                         where
                             (from o in
                                 db.person_volunteer_record.Where(
                                     x =>

                                         x.is_deleted != true)
                              select o.person_profile_id)
                                 .Contains(s.person_profile_id)
                         select s).GroupBy(x => x.sex)
                    .Select(x => new
                    {
                        Name = x.Key == true ? "Male" : "Female",
                        Count = x.Count(),
                    });


            return Ok(model);


        }

        public IActionResult trained_volunteers()
        {
            var model = (from s in db.person_profile
                         where
                             (from o in
                                 db.person_volunteer_record.Where(
                                     x => x.is_deleted != true)
                                     join p in db.person_training.Where(x => x.is_deleted != true && x.is_participant == true) on o.person_profile_id equals p.person_profile_id
                              select o.person_profile_id)
                                 .Contains(s.person_profile_id)
                         select s)
                         .GroupBy(x => x.sex)
                  .Select(x => new
                  {
                      Name = x.Key == true ? "Male" : "Female",
                      Count = x.Count(),
                  });

            return Ok(model);
        }

        
    }
}
