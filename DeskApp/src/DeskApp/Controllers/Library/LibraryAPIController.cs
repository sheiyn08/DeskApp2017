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

        private readonly ApplicationDbContext db;


        public LibraryAPIController(ApplicationDbContext context)
        {
            db = context;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Table Name</param>
        /// <returns></returns>
        [Route("api/attachment_list")]
        public ActionResult attachment_list(int id)
        {

            var source = db.mov_list.Where(x=> x.table_name_id == id);
            return Json(source.Select(x => new { Id = x.mov_list_id, Name = x.name }));


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Table Name</param>
        /// <returns></returns>
        [Route("api/report_list")]
        public ActionResult report_list(int id)
        {

            var source = db.report_list.Where(x => x.is_deleted != true && x.table_name_id == id);
            return Json(source.Select(x => new { Id = x.report_list_id, name = x.name, description = x.description, url = x.url }));


        }

        [Route("api/mov_list")]
        public ActionResult mov_list(int id)
        {

            var source = db.mov_list.Where(x => x.table_name_id == id);
            return Json(source.Select(x => new { Id = x.mov_list_id, Name = x.name}));


        }



        [Route("api/lib_ers_delivery_mode")]
        public ActionResult lib_ers_delivery_mode()
        {
            var source = db.lib_ers_delivery_mode;




            return Json(source.Select(x => new { Id = x.ers_delivery_mode_id, Name = x.name }));


        }



        [Route("api/all/cities")]
        public ActionResult all_cities()
        {
            var source = db.lib_city.AsQueryable();



            return Json(source.Select(x => new { Id = x.city_code, Name = x.city_name }));


        }

        [Route("api/online/lib_region")]
        public ActionResult GetRegions()
        {
            var source = db.lib_region.AsQueryable();



            return Json(source.Select(x => new { Id = x.region_code, Name = x.region_nick }));


        }


        [Route("api/lib_ers_current_work")]
        public ActionResult lib_ers_current_work()
        {
            var source = db.lib_ers_current_work.AsQueryable();



            return Json(source.Select(x => new { Id = x.ers_current_work_id, Name = x.name }));


        }


        [Route("api/online/lib_province")]
        public ActionResult GetProvinces(int id)
        {
            return Json(db.lib_province.Where(x => x.region_code == id).Select(x => new { Id = x.prov_code, Name = x.prov_name }));

        }
        [Route("api/online/lib_city")]
        public ActionResult GetCities(int id)
        {
            return Json(db.lib_city.Where(x => x.prov_code == id).Select(x => new { Id = x.city_code, Name = x.city_name }));

        }
        [Route("api/online/lib_brgy")]
        public ActionResult GetBarangay(int id)
        {
            return Json(db.lib_brgy.Where(x => x.city_code == id).Select(x => new { Id = x.brgy_code, Name = x.brgy_name }));

        }

        //talakayan:
        [Route("api/talakayan_year")]
        public ActionResult GetTalakayanYear(int id)
        {
            var source = db.talakayan_year.AsQueryable();
            return Json(source.Select(x => new { Id = x.talakayan_yr_id, Name = x.name }));
        }
        //        [HttpGet]

        [Route("api/lib_fund_source")]
        public ActionResult GetFundSource()
        {
            return Json(db.lib_fund_source.Select(x => new { Id = x.fund_source_id, Name = x.name }));

        }

        //[Route("api/lib_lgu_level")]
        //public ActionResult GetLGULevel()
        //{
        //    return Json(db.lib_lgu_level.Select(x => new { Id = x.lgu_level_id, Name = x.name }));

        //}



        [Route("api/all_lib_enrollment")]
        public ActionResult all_lib_enrollment(int id)
        {
            return Json(db.lib_enrollment.Select(x => new { Id = x.enrollment_id, Name = x.name }));

        }



        [Route("api/lib_enrollment")]
        public ActionResult GetEnrollment()
        {

            return Json(db.lib_enrollment.Select(x => new { Id = x.enrollment_id, Name = x.name }));
        }

        [Route("api/lib_cycle")]
        public ActionResult GetCycle(int id)
        {
            return Json(db.lib_cycle.Where(x =>  x.fund_source_id == id).Select(x => new { Id = x.cycle_id, Name = x.name }));

        }


        [Route("api/lib_barangay_assembly_purpose")]
        public ActionResult lib_barangay_assembly_purpose()
        {
            return Json(db.lib_barangay_assembly_purpose.Select(x => new { Id = x.barangay_assembly_purpose_id, Name = x.name }));

        }

        [Route("api/lib_lgu_level")]
        public ActionResult lib_lgu_level()
        {
            return Json(db.lib_lgu_level.Select(x => new { Id = x.lgu_level_id, Name = x.name }));

        }
        [Route("api/lib_training_category")]
        public ActionResult lib_training_category(int lgu_level_id)
        {
            //BLGU
            if (lgu_level_id == 1)
            {
                return Json(db.lib_training_category.Where(x => x.is_active != null && x.is_barangay != null && x.is_ceac_tracking_only != true).OrderBy(x => x.name).Select(x => new { Id = x.training_category_id, Name = x.description }));
                
            }
            else
            {
                return Json(db.lib_training_category.Where(x => x.is_active != null && x.is_municipality != null && x.is_ceac_tracking_only != true).OrderBy(x => x.name).Select(x => new { Id = x.training_category_id, Name = x.description }));
                
            }

        }


        [Route("api/lib_occupation")]
        public ActionResult GetOccupation()
        {
            return Json(db.lib_occupation.Where(x => x.is_active != null).Select(x => new { Id = x.occupation_id, Name = x.name }));

        }

        [Route("api/lib_civil_status")]
        public ActionResult GetCivilStatus()
        {
            return Json(db.lib_civil_status.Select(x => new { Id = x.civil_status_id, Name = x.name }));

        }
        [Route("api/lib_education_attainment")]
        public ActionResult GetEducationalAttainment()
        {
            return Json(db.lib_education_attainment.Select(x => new { Id = x.education_attainment_id, Name = x.name }));

        }
        [Route("api/lib_lgu_position")]
        public ActionResult GetLguPosition()
        {
            return Json(db.lib_lgu_position.Select(x => new { Id = x.lgu_position_id, Name = x.name }));

        }
        [Route("api/lib_psa_problem_category")]
        public ActionResult lib_psa_problem_category()
        {
            return Json(db.lib_psa_problem_category.Where(x => x.is_active != null).Select(x => new { Id = x.psa_problem_category_id, Name = x.name }));

        }

        [Route("api/lib_psa_solution_category")]
        public ActionResult lib_psa_solution_category()
        {
            return Json(db.lib_psa_solution_category.Where(x => x.is_active != null).Select(x => new { Id = x.psa_solution_category_id, Name = x.name }));

        }


        [Route("api/lib_volunteer_committee_position")]
        public ActionResult lib_volunteer_committee_position()
        {
            return Json(db.lib_volunteer_committee_position.Where(x => x.is_active != null).Select(x => new { Id = x.volunteer_committee_position_id, Name = x.name }));

        }
        [Route("api/lib_volunteer_committee")]
        public ActionResult lib_volunteer_committee()
        {
            return Json(db.lib_volunteer_committee.Select(x => new { Id = x.volunteer_committee_id, Name = x.name }));

        }

        [Route("api/lib_eca_type")]
        public ActionResult lib_eca_type()
        {
            return Json(db.lib_eca_type.Select(x => new { Id = x.eca_type_id, Name = x.name }));

        }


        [Route("api/lib_transpo_mode")]
        public ActionResult lib_transpo_mode()
        {
            return Json(db.lib_transpo_mode.Select(x => new { Id = x.transpo_mode_id, Name = x.name }));

        }



        [Route("api/brgy_ceac")]
        public ActionResult brgy_ceac()
        {
            return Json(db.lib_training_category.Where(x => x.brgy_sort_order != null).OrderBy(x => x.brgy_sort_order).Select(x => new { Id = x.training_category_id, Name = x.name }));

        }

        //[Route("api/lib_indigenous_groups")]
        //public ActionResult GetIPGroups()
        //{
        //    return Json(db.lib_ip_group.Where(x => x.is_active != null).Select(x => new { Id = x.ip_group_id, Name = x.name }));

        //}


        //[Route("api/lib_barangay_assembly_purpose")]
        //public ActionResult GetBAPurpose()
        //{
        //    return Json(db.lib_barangay_assembly_purpose.Where(x => x.is_active != null).Select(x => new { Id = x.barangay_assembly_purpose_id, Name = x.name }));

        //}


    }
}
