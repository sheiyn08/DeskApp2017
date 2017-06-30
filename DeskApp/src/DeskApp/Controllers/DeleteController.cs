using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskApp.Controllers
{
    [Produces("application/json")]
    [Route("api/delete")]
    public class DeleteController : Controller
    {

        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;

        public DeleteController(ApplicationDbContext context)
        {
            db = context;

        }



        [HttpPost]
        [Route("mibf_prioritization")]
        public async Task<IActionResult> mibf_prioritization(Guid id)
        {


            var proj = db.sub_project.Where(x => x.mibf_prioritization_id == id);

            if(proj.Count() > 0)
            {
                return BadRequest("Prioritization Already used for Sub Projects. This can not be deleted unless the project is untagged from the Prio");
            }

            var record = db.mibf_prioritization.FirstOrDefault(x => x.mibf_prioritization_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }



        [HttpPost]
        [Route("training")]
        public async Task<IActionResult> Training(Guid id)
        {
            var record = db.community_training.FirstOrDefault(x => x.community_training_id == id);
            var ceac_record = db.ceac_tracking.FirstOrDefault(x => x.reference_id == id);

            //if training DOES NOT EXISTS on ceac tracking
            if (ceac_record == null)
            {
                //go with the usual delete
                record.is_deleted = true;
                record.push_status_id = 3;
            }

            //if training EXISTS on ceac tracking
            else {
                record.is_deleted = true;
                record.push_status_id = 3;

                ceac_record.reference_id = null; //reference id in ceac_tracking table allows null values
                ceac_record.actual_start = null;
                ceac_record.actual_end = null;
                ceac_record.catch_start = null;
                ceac_record.catch_end = null;
                ceac_record.actual_start = null;
                ceac_record.actual_end = null;
                ceac_record.implementation_status_id = 2;
                ceac_record.push_status_id = 3;

                ////check if existing record has plan dates or catch up dates, any of the four
                //if (ceac_record.plan_start != null || ceac_record.plan_end != null || ceac_record.catch_start != null || ceac_record.catch_end != null)
                //{
                //    record.is_deleted = true;
                //    record.push_status_id = 3;

                //    ceac_record.reference_id = null; //reference id in ceac_tracking table allows null values
                //    ceac_record.actual_start = null;
                //    ceac_record.actual_end = null;
                //    ceac_record.catch_start = null;
                //    ceac_record.catch_end = null;
                //    ceac_record.actual_start = null;
                //    ceac_record.actual_end = null;
                //    ceac_record.implementation_status_id = 2;
                //    ceac_record.push_status_id = 3;
                //}
                ////else, if andun lang naman si record at wala naman plan date or catch up, okay lang to delete
                //else
                //{
                //    record.is_deleted = true;
                //    record.push_status_id = 3;

                //    ceac_record.actual_start = null;
                //    ceac_record.actual_end = null;
                //    ceac_record.implementation_status_id = 2;
                //    ceac_record.push_status_id = 3;
                //}
            }

            //record.is_deleted = true;
            //record.push_status_id = 3;

            //ceac_record.actual_start = null;
            //ceac_record.actual_end = null;
            //ceac_record.implementation_status_id = 2;
            //ceac_record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }
        
        [HttpPost]
        [Route("grievance_installation")]
        public async Task<IActionResult> GRSInstallation(Guid id)
        {


            var record = db.grs_installation.FirstOrDefault(x => x.grs_installation_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }

        [HttpPost]
        [Route("person_profile")]
        public async Task<IActionResult> Person(Guid id)
        {


            var record = db.person_profile.FirstOrDefault(x => x.person_profile_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost]
        [Route("person_volunteer_record")]
        public async Task<IActionResult> PersonVolunteer(Guid id)
        {


            var record = db.person_volunteer_record.FirstOrDefault(x => x.person_volunteer_record_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }





        [HttpPost]
        [Route("barangay_assembly")]
        public async Task<IActionResult> barangay_assembly(Guid id)
        {


            var record = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost]
        [Route("barangay_profile")]
        public async Task<IActionResult> barangay_profile(Guid id)
        {


            var record = db.brgy_profile.FirstOrDefault(x => x.brgy_profile_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost]
        [Route("municipal_profile")]
        public async Task<IActionResult> municipal_profile(Guid id)
        {


            var record = db.muni_profile.FirstOrDefault(x => x.muni_profile_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost]
        [Route("grievance_record")]
        public async Task<IActionResult> grievance_record(Guid id)
        {


            var record = db.grievance_record.FirstOrDefault(x => x.grievance_record_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost]
        [Route("sub_project_ers")]
        public async Task<IActionResult> sub_project_ers(Guid id)
        {


            var record = db.sub_project_ers.FirstOrDefault(x => x.sub_project_ers_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost]
        [Route("person_ers_work")]
        public async Task<IActionResult> person_ers_work(Guid id)
        {


            var record = db.person_ers_work.FirstOrDefault(x => x.person_ers_work_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost]
        [Route("municipal_lcc")]
        public async Task<IActionResult> mlcc(Guid id)
        {


            var record = db.municipal_lcc.FirstOrDefault(x => x.municipal_lcc_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }

        [HttpPost]
        [Route("municipal_pta")]
        public async Task<IActionResult> mpta(Guid id)
        {


            var record = db.municipal_pta.FirstOrDefault(x => x.municipal_pta_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }

        [HttpPost]
        [Route("community_organization")]
        public async Task<IActionResult> community_organization(Guid id)
        {


            var record = db.community_organization.FirstOrDefault(x => x.community_organization_id == id);


            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

            return Ok();

        }


    }
}