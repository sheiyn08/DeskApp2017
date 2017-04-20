using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;
using DeskApp.Data;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DeskApp.Controllers
{
    public partial class LibraryAPIController : Controller
    {

        [HttpGet]
        [Route("api/erfr_projects")]
        public IActionResult erfr_projects(string brgy_code, int cycle_id)
        {
            var cycle = db.lib_cycle.Find(cycle_id);
            
            int cycle_;

            if (cycle != null)
            {
                if (cycle.name.Contains("1")) cycle_ = 1;
                if (cycle.name.Contains("2")) cycle_ = 2;
                if (cycle.name.Contains("3")) cycle_ = 3;
            }


            if(brgy_code.Length == 8)
            {
                brgy_code = "0" + brgy_code;

            }

            var model = db.erfr_sub_project.Where(x => x.nscb_code == brgy_code);



            return Json(model.Select(x => new { Id = x.SPID, Name = "SPID: " + x.SPID + " | Title: " + x.title }));


        }



        [HttpGet]
        [Route("api/get_project_type")]
        public IActionResult get_project_type(int id)
        {
            return Json(db.lib_project_type.FirstOrDefault(x => x.project_type_id == id));

        }


        [HttpGet]
        [Route("api/lib_project_type")]
        public IActionResult lib_project_type()
        {
            return Json(db.lib_project_type.Where(x => x.deleted != 1).Select(x => new { Id = x.project_type_id, Name = x.name }));

        }
        [HttpGet]
        [Route("api/lib_physical_status")]
        public IActionResult lib_physical_status()
        {
            return Json(db.lib_physical_status.Select(x => new { Id = x.physical_status_id, Name = x.name }));

        }

        [HttpGet]
        [Route("api/lib_physical_status_category")]
        public IActionResult lib_physical_status_category(double percent_accomplished, double tranche)
        {
            return Json(db.lib_physical_status_category.Select(x => new { Id = x.physical_status_category_id, Name = x.name }));

        }

        //[HttpGet]
        //[Route("api/ncddp_grouping")]
        //public IActionResult ncddp_grouping(string id)
        //{
        //    var set = db.ncddp_grouping.FirstOrDefault(x => x.city_code == id);

        //    if (set != null)
        //    {
        //        return Json(db.lib_modality_category.Where(x => x.name == set.grouping_id.ToString()).Select(x => new { Id = x.modality_category_id, Name = x.name }));

        //    }

        //    return Json(db.lib_modality_category.Where(x => x.name == "None"));
        //}

    }
}
