//using DeskApp.Data;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DeskApp.Controllers
//{
  

//        public class LibraryAPIController : Controller
//        {


//            private readonly ApplicationDbContext db;


//            public LibraryAPIController(ApplicationDbContext context)
//            {
//                db = context;

//            }



//        [Route("api/lib_region")]
//        public IActionResult GetRegions()
//        {


//            var source = db.lib_region.AsQueryable();

        
//            return Json(source.Select(x => new { Id = x.region_code, Name = x.region_nick }));
//        }


//        [Route("api/lib_province")]
//        public IActionResult GetProvince(string id)
//        {
//            return Json(db.lib_province.Where(x => x.region_code == id).Select(x => new { Id = x.prov_code, Name = x.prov_name }));
//        }

//        [Route("api/lib_province")]
//        public IActionResult GetProvinces()
//        {
//            return Json(db.lib_province.Select(x => new { Id = x.prov_code, Name = x.prov_name }));
//        }

//        [Route("api/lib_city")]
//        public IActionResult GetCity(string id)
//        {
//            return Json(db.lib_city.Where(x => x.prov_code == id).Select(x => new { Id = x.city_code, Name = x.city_name }));
//        }

//        [Route("api/lib_city")]
//        public IActionResult GetCities()
//        {
//            return Json(db.lib_city.Select(x => new { Id = x.city_code, Name = x.city_name }));
//        }

//        [Route("api/lib_brgy")]
//        public IActionResult GetBarangay(string id)
//        {
//            return Json(db.lib_brgy.Where(x => x.city_code == id).Select(x => new { Id = x.brgy_code, Name = x.brgy_name }));
//        }

//        [Route("api/lib_brgy")]
//        public IActionResult GetBarangays()
//        {
//            return Json(db.lib_brgy.Select(x => new { Id = x.brgy_code, Name = x.brgy_name }));
//        }



//        [HttpGet]
//            [Route("api/lib_fund_source")]
//            public IActionResult GetFundSources()
//            {
//                return Json(db.lib_fund_source.Select(x => new { Id = x.fund_source_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_lgu_level")]
//            public IActionResult GetLGULevel()
//            {
//                return Json(db.lib_lgu_level.Select(x => new { Id = x.lgu_level_id, Name = x.name }));

//            }
//            [HttpGet]
//            [Route("api/lib_enrollment")]
//            public IActionResult GetEnrollment()
//            {
//                return Json(db.lib_enrollment.Select(x => new { Id = x.enrollment_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_cycle")]
//            public IActionResult GetCycle(int id)
//            {
//                return Json(db.lib_cycle.Where(x => x.is_active == true && x.fund_source_id == id).Select(x => new { Id = x.cycle_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_cycle")]
//            public IActionResult GetCycles()
//            {
//                return Json(db.lib_cycle.Select(x => new { Id = x.cycle_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_training_category")]
//            public IActionResult GetTrainingCategory()
//            {
//                return Json(db.lib_training_category.Select(x => new { Id = x.training_category_id, Name = x.description }));
//            }
//            [HttpGet]
//            [Route("api/lib_civil_status")]
//            public IActionResult GetCivilStatus()
//            {
//                return Json(db.lib_civil_status.Select(x => new { Id = x.civil_status_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_education_attainment")]
//            public IActionResult GetEducationalAttainment()
//            {
//                return Json(db.lib_education_attainment.Select(x => new { Id = x.education_attainment_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_lgu_position")]
//            public IActionResult GetLguPosition()
//            {
//                return Json(db.lib_lgu_position.Select(x => new { Id = x.lgu_position_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_indigenous_groups")]
//            public IActionResult GetIPGroups()
//            {
//                return Json(db.lib_ip_group.Select(x => new { Id = x.ip_group_id, Name = x.name }));
//            }

//            [HttpGet]
//            [Route("api/lib_barangay_assembly_purpose")]
//            public IActionResult GetBAPurpose()
//            {
//                return Json(db.lib_barangay_assembly_purpose.Select(x => new { Id = x.barangay_assembly_purpose_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_occupations")]
//            public IActionResult GetOccupations()
//            {
//                return Json(db.lib_occupation.Select(x => new { Id = x.occupation_id, Name = x.name }));
//            }
//            [HttpGet]
//            [Route("api/lib_psa_problem_category")]
//            public IActionResult lib_psa_problem_category()
//            {
//                return Json(db.lib_psa_problem_category.Select(x => new { Id = x.psa_problem_category_id, Name = x.name }));

//            }

//            [HttpGet]
//            [Route("api/lib_psa_solution_category")]
//            public IActionResult lib_psa_solution_category()
//            {
//                return Json(db.lib_psa_solution_category.Select(x => new { Id = x.psa_solution_category_id, Name = x.name }));

//            }

//            [HttpGet]
//            [Route("api/lib_volunteer_committee_position")]
//            public IActionResult lib_volunteer_committee_position()
//            {
//                return Json(db.lib_volunteer_committee_position.Select(x => new { Id = x.volunteer_committee_position_id, Name = x.name }));

//            }
//            [HttpGet]
//            [Route("api/lib_volunteer_committee")]
//            public IActionResult lib_volunteer_committee()
//            {
//                return Json(db.lib_volunteer_committee.Select(x => new { Id = x.volunteer_committee_id, Name = x.name }));

//            }
//            [HttpGet]
//            [Route("api/lib_occupation")]
//            public IActionResult GetOccupation()
//            {
//                return Json(db.lib_occupation.Select(x => new { Id = x.occupation_id, Name = x.name }));

//            }
//        }
    
//}
