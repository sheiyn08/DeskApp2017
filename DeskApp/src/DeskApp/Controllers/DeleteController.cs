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

        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

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
            var main_record = db.community_training.FirstOrDefault(x => x.community_training_id == record.community_training_id);

            record.is_deleted = true;
            record.push_status_id = 3;

            //v3.1 if one PRA is deleted, the ranking should be adjusted
            var pra_onwards = db.mibf_prioritization.Where(x => x.rank > record.rank);
            foreach (var pra in pra_onwards.ToList())
            {
                pra.rank = pra.rank - 1;
            }

            //v3.1 if PRA is deleted, update main training record as well
            main_record.push_status_id = 3;
            main_record.last_modified_by = 0;
            main_record.last_modified_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("sub_project")]
        public async Task<IActionResult> SubProject(Guid id)
        {
            var record = db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == id);
            var spcf_record = db.sub_project_spcf.FirstOrDefault(x => x.sub_project_unique_id == id);

            record.IsActive = false;
            record.push_status_id = 3;
            record.deleted_date = DateTime.Now;
            record.deleted_reason = "Deleted through DeskApp";

            //update record in sub_project_spcf: 
            if (spcf_record != null) {
                spcf_record.is_deleted = true;
                spcf_record.push_status_id = 3;
                spcf_record.deleted_by = 0;
                spcf_record.deleted_date = DateTime.Now;
            }  

            //v3.1 update also ers to deleted state
            foreach (var ers in db.sub_project_ers.Where(x => x.sub_project_unique_id == id).ToList())
            {
                ers.push_status_id = 3;
                ers.is_deleted = true;
                ers.deleted_by = 0;
                ers.deleted_date = DateTime.Now;
                Guid ers_id = ers.sub_project_ers_id;

                //v3.1 update also ers worker to deleted state
                var person = db.person_ers_work.Where(x => x.sub_project_ers_id == ers_id).ToList();
                foreach (var p in person)
                {
                    p.push_status_id = 3;
                    p.is_deleted = true;
                    p.deleted_by = 0;
                    p.deleted_date = DateTime.Now;
                }
            }

            //v3.1 update also set to deleted state
            foreach (var set in db.sub_project_set.Where(x => x.sub_project_unique_id == id).ToList())
            {
                set.push_status_id = 3;
                set.is_deleted = true;
                set.deleted_by = 0;
                set.deleted_date = DateTime.Now;
            }
            
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("ceac_list")]
        public async Task<IActionResult> CEACTracking(Guid id)
        {
            var record = db.ceac_list.FirstOrDefault(x => x.ceac_list_id == id);
            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            //v3.1 update also ceac_tracking to deleted state
            foreach (var c in db.ceac_tracking.Where(x => x.ceac_list_id == id).ToList())
            {
                c.is_deleted = true;
                c.push_status_id = 3;
                c.deleted_by = 0;
                c.deleted_date = DateTime.Now;
            }

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
                record.deleted_by = 0;
                record.deleted_date = DateTime.Now;
            }

            //if training EXISTS on ceac tracking
            //Modified 07-24-2018 for v4.2
            else
            {
                record.is_deleted = true;
                record.push_status_id = 3;
                record.deleted_by = 0;
                record.deleted_date = DateTime.Now;

                //if record is included on the regular 15 items in CEAC (municipal level), and the record is deleted,
                //just clear the dates so it will still appear there                
                if ((ceac_record.training_category_id == 23 || ceac_record.training_category_id == 24 ||
                    ceac_record.training_category_id == 25 || ceac_record.training_category_id == 26 ||
                    ceac_record.training_category_id == 12 || ceac_record.training_category_id == 27 ||
                    ceac_record.training_category_id == 13 || ceac_record.training_category_id == 4 ||
                    ceac_record.training_category_id == 19 || ceac_record.training_category_id == 10 ||
                    ceac_record.training_category_id == 11 || ceac_record.training_category_id == 44 ||
                    ceac_record.training_category_id == 14 || ceac_record.training_category_id == 36 ||
                    ceac_record.training_category_id == 37) && ceac_record.lgu_level_id == 2)
                {
                    ceac_record.reference_id = null;
                    ceac_record.actual_start = null;
                    ceac_record.actual_end = null;
                    ceac_record.catch_start = null;
                    ceac_record.catch_end = null;
                    ceac_record.plan_start = null;
                    ceac_record.plan_end = null;
                    ceac_record.implementation_status_id = 2;
                    ceac_record.push_status_id = 3;
                }
                //if record is included on the regular 19 items in CEAC (barangay level), and the record is deleted,
                //just clear the dates so it will still appear there  
                else if ((ceac_record.training_category_id == 38 || ceac_record.training_category_id == 2 ||
                    ceac_record.training_category_id == 39 || ceac_record.training_category_id == 40 ||
                    ceac_record.training_category_id == 3 || ceac_record.training_category_id == 28 ||
                    ceac_record.training_category_id == 41 || ceac_record.training_category_id == 29 ||
                    ceac_record.training_category_id == 30 || ceac_record.training_category_id == 42 ||
                    ceac_record.training_category_id == 31 || ceac_record.training_category_id == 43 ||
                    ceac_record.training_category_id == 32 || ceac_record.training_category_id == 33 ||
                    ceac_record.training_category_id == 34 || ceac_record.training_category_id == 35 ||
                    ceac_record.training_category_id == 1 || ceac_record.training_category_id == 36 ||
                    ceac_record.training_category_id == 37) && ceac_record.lgu_level_id == 1)
                {
                    ceac_record.reference_id = null;
                    ceac_record.actual_start = null;
                    ceac_record.actual_end = null;
                    ceac_record.catch_start = null;
                    ceac_record.catch_end = null;
                    ceac_record.plan_start = null;
                    ceac_record.plan_end = null;
                    ceac_record.implementation_status_id = 2;
                    ceac_record.push_status_id = 3;
                }
                //else, set the whole record as deleted, so it will not appear on the list (barangay and municipal level)
                else
                {
                    ceac_record.is_deleted = true;
                    ceac_record.push_status_id = 3;
                }

                //OLD WAY OF DELETING/CLEARING DATES IN CEAC:
                //ceac_record.reference_id = null;
                //ceac_record.actual_start = null;
                //ceac_record.actual_end = null;
                //ceac_record.catch_start = null;
                //ceac_record.catch_end = null;
                //ceac_record.plan_start = null;
                //ceac_record.plan_end = null;
                //ceac_record.implementation_status_id = 2;
                //ceac_record.push_status_id = 3;                
            }

            //v3.1 update also training participant to deleted state
            foreach (var p in db.person_training.Where(x => x.community_training_id == id).ToList())
            {
                p.push_status_id = 3;
                p.is_deleted = true;
                p.deleted_by = 0;
                p.deleted_date = DateTime.Now;
            }
            //v3.1 update also psa problem to deleted state
            foreach (var prob in db.psa_problem.Where(x => x.community_training_id == id).ToList())
            {
                prob.push_status_id = 3;
                prob.is_deleted = true;
                prob.deleted_by = 0;
                prob.deleted_date = DateTime.Now;
                Guid prob_id = prob.psa_problem_id;

                //v3.1 update also psa solution to deleted state
                var sol = db.psa_solution.Where(x => x.psa_problem_id == prob_id).ToList();
                foreach (var s in sol)
                {
                    s.push_status_id = 3;
                    s.is_deleted = true;
                    s.deleted_by = 0;
                    s.deleted_date = DateTime.Now;
                }
            }
            //v3.1 update also mibf criteria to deleted state
            foreach (var mibf_criteria in db.mibf_criteria.Where(x => x.community_training_id == id).ToList())
            {
                mibf_criteria.push_status_id = 3;
                mibf_criteria.is_deleted = true;
                mibf_criteria.deleted_by = 0;
                mibf_criteria.deleted_date = DateTime.Now;
            }
            //v3.1 update also mibf prio to deleted state
            foreach (var mibf_prio in db.mibf_prioritization.Where(x => x.community_training_id == id).ToList())
            {
                mibf_prio.push_status_id = 3;
                mibf_prio.is_deleted = true;
                mibf_prio.deleted_by = 0;
                mibf_prio.deleted_date = DateTime.Now;
            }
            //v3.1 update also grievance to deleted state
            foreach (var g in db.grievance_record.Where(x => x.activity_source_id == id).ToList())
            {
                g.push_status_id = 3;
                g.is_deleted = true;
                g.deleted_by = 0;
                g.deleted_date = DateTime.Now;
                Guid grs_id = g.grievance_record_id;

                //v3.1 update also discussion to deleted state
                var grievance_record_discussion = db.grievance_record_discussion.Where(x => x.grievance_record_id == grs_id).ToList();
                foreach (var d in grievance_record_discussion)
                {
                    d.push_status_id = 3;
                    d.is_deleted = true;
                    d.deleted_by = 0;
                    d.deleted_date = DateTime.Now;
                }
            }

            await db.SaveChangesAsync();
            await updateTracking();
            return Ok();
            
        }

        [HttpPost]
        [Route("barangay_assembly")]
        public async Task<IActionResult> barangay_assembly(Guid id)
        {
            //OLD:
            //var record = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == id);
            //record.is_deleted = true;
            //record.push_status_id = 3;
            //await db.SaveChangesAsync();
            //return Ok();

            //NEW:RDR 072817
            var record = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == id);
            var ceac_record = db.ceac_tracking.FirstOrDefault(x => x.reference_id == id);

            //if BA DOES NOT EXISTS on ceac tracking
            if (ceac_record == null)
            {
                //go with the usual delete
                record.is_deleted = true;
                record.push_status_id = 3;
                record.deleted_by = 0;
                record.deleted_date = DateTime.Now;
            }

            //if BA EXISTS on ceac tracking
            //Modified 07-24-2018 for v4.2
            else
            {
                record.is_deleted = true;
                record.push_status_id = 3;
                record.deleted_by = 0;
                record.deleted_date = DateTime.Now;

                //if record is included on the regular 15 items in CEAC (municipal level), and the record is deleted,
                //just clear the dates so it will still appear there                
                if ((ceac_record.training_category_id == 23 || ceac_record.training_category_id == 24 ||
                    ceac_record.training_category_id == 25 || ceac_record.training_category_id == 26 ||
                    ceac_record.training_category_id == 12 || ceac_record.training_category_id == 27 ||
                    ceac_record.training_category_id == 13 || ceac_record.training_category_id == 4 ||
                    ceac_record.training_category_id == 19 || ceac_record.training_category_id == 10 ||
                    ceac_record.training_category_id == 11 || ceac_record.training_category_id == 44 ||
                    ceac_record.training_category_id == 14 || ceac_record.training_category_id == 36 ||
                    ceac_record.training_category_id == 37) && ceac_record.lgu_level_id == 2)
                {
                    ceac_record.reference_id = null;
                    ceac_record.actual_start = null;
                    ceac_record.actual_end = null;
                    ceac_record.catch_start = null;
                    ceac_record.catch_end = null;
                    ceac_record.plan_start = null;
                    ceac_record.plan_end = null;
                    ceac_record.implementation_status_id = 2;
                    ceac_record.push_status_id = 3;
                }
                //if record is included on the regular 19 items in CEAC (barangay level), and the record is deleted,
                //just clear the dates so it will still appear there  
                else if ((ceac_record.training_category_id == 38 || ceac_record.training_category_id == 2 ||
                    ceac_record.training_category_id == 39 || ceac_record.training_category_id == 40 ||
                    ceac_record.training_category_id == 3 || ceac_record.training_category_id == 28 ||
                    ceac_record.training_category_id == 41 || ceac_record.training_category_id == 29 ||
                    ceac_record.training_category_id == 30 || ceac_record.training_category_id == 42 ||
                    ceac_record.training_category_id == 31 || ceac_record.training_category_id == 43 ||
                    ceac_record.training_category_id == 32 || ceac_record.training_category_id == 33 ||
                    ceac_record.training_category_id == 34 || ceac_record.training_category_id == 35 ||
                    ceac_record.training_category_id == 1 || ceac_record.training_category_id == 36 ||
                    ceac_record.training_category_id == 37) && ceac_record.lgu_level_id == 1)
                {
                    ceac_record.reference_id = null;
                    ceac_record.actual_start = null;
                    ceac_record.actual_end = null;
                    ceac_record.catch_start = null;
                    ceac_record.catch_end = null;
                    ceac_record.plan_start = null;
                    ceac_record.plan_end = null;
                    ceac_record.implementation_status_id = 2;
                    ceac_record.push_status_id = 3;
                }
                //else, set the whole record as deleted, so it will not appear on the list (barangay and municipal level)
                else
                {
                    ceac_record.is_deleted = true;
                    ceac_record.push_status_id = 3;
                }

                //OLD WAY OF DELETING/CLEARING DATES IN CEAC:
                //ceac_record.reference_id = null;
                //ceac_record.actual_start = null;
                //ceac_record.actual_end = null;
                //ceac_record.catch_start = null;
                //ceac_record.catch_end = null;
                //ceac_record.plan_start = null;
                //ceac_record.plan_end = null;
                //ceac_record.implementation_status_id = 2;
                //ceac_record.push_status_id = 3;
                //ceac_record.brgy_code = null;

            }

            //v3.1 update also ip represented to deleted state
            foreach (var ip in db.brgy_assembly_ip.Where(x => x.brgy_assembly_id == id).ToList()) {
                ip.push_status_id = 3;
                ip.is_deleted = true;
                ip.deleted_by = 0;
                ip.deleted_date = DateTime.Now;
            }
            //v3.1 update also grievance to deleted state
            foreach (var g in db.grievance_record.Where(x => x.activity_source_id == id).ToList()) {
                g.push_status_id = 3;
                g.is_deleted = true;
                g.deleted_by = 0;
                g.deleted_date = DateTime.Now;
                Guid grs_id = g.grievance_record_id;
                
                //v3.1 update also discussion to deleted state
                var grievance_record_discussion = db.grievance_record_discussion.Where(x => x.grievance_record_id == grs_id).ToList();
                foreach (var d in grievance_record_discussion)
                {
                    d.push_status_id = 3;
                    d.is_deleted = true;
                    d.deleted_by = 0;
                    d.deleted_date = DateTime.Now;
                }
            }      

            await db.SaveChangesAsync();
            await updateTracking();
            return Ok();
        }

        //if dates are all empty, then delete the ceac_list
        public async Task<bool> updateTracking()
        {
            foreach (var c in db.ceac_list.Where(x => x.is_deleted != true).ToList())
            {
                int count_of_item_with_dates = 0;
                int count_of_item_without_dates = 0;

                int number_of_list = db.ceac_tracking.Select(x => x.ceac_list_id).Distinct().Count();

                foreach (var ceac_item in db.ceac_tracking.Where(x => x.ceac_list_id == c.ceac_list_id).ToList())
                {
                    if (ceac_item.plan_start == null && ceac_item.plan_end == null && ceac_item.catch_start == null && ceac_item.catch_end == null && ceac_item.actual_start == null && ceac_item.actual_end == null)
                    {
                        count_of_item_without_dates = count_of_item_without_dates + 1;
                    }
                    else {
                        count_of_item_with_dates = count_of_item_with_dates + 1;
                    }
                }

                if (count_of_item_with_dates == 0)
                {
                    c.is_deleted = true;
                    c.push_status_id = 3;
                    c.deleted_by = 0;
                    c.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();                    
                }
            }

            return true;
        }




        [HttpPost]
        [Route("grievance_installation")]
        public async Task<IActionResult> GRSInstallation(Guid id)
        {
            var record = db.grs_installation.FirstOrDefault(x => x.grs_installation_id == id);
            
            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("person_profile")]
        public async Task<IActionResult> Person(Guid id)
        {
            var record = db.person_profile.FirstOrDefault(x => x.person_profile_id == id);
            var volunteer_record = db.person_volunteer_record.Where(x => x.person_profile_id == id).ToList();

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            //v3.1 update also volunteer record to deleted state
            foreach (var v in db.person_volunteer_record.Where(x => x.person_profile_id == id).ToList())
            {
                v.push_status_id = 3;
                v.is_deleted = true;
                v.deleted_by = 0;
                v.deleted_date = DateTime.Now;
            }
            //v3.1 update also trainings attended record to deleted state
            foreach (var t in db.person_training.Where(x => x.person_profile_id == id).ToList())
            {
                t.push_status_id = 3;
                t.is_deleted = true;
                t.deleted_by = 0;
                t.deleted_date = DateTime.Now;
            }
            //v3.1 update also ers record to deleted state
            foreach (var e in db.person_ers_work.Where(x => x.person_profile_id == id).ToList())
            {
                e.push_status_id = 3;
                e.is_deleted = true;
                e.deleted_by = 0;
                e.deleted_date = DateTime.Now;
            }

            await db.SaveChangesAsync();
            return Ok();

        }


        [HttpPost]
        [Route("person_volunteer_record")]
        public async Task<IActionResult> PersonVolunteer(Guid id)
        {
            var record = db.person_volunteer_record.FirstOrDefault(x => x.person_volunteer_record_id == id);
            var main_record = db.person_profile.FirstOrDefault(x => x.person_profile_id == record.person_profile_id);
            var volunteer_count = db.person_volunteer_record.Where(x => x.person_profile_id == record.person_profile_id && x.is_deleted != true).Count();

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            //v3.1 if volunteer record is deleted/removed, main person profile will be updated
            main_record.push_status_id = 3;
            main_record.last_modified_by = 0;
            main_record.last_modified_date = DateTime.Now;
            
            //if there is only 1 record in person_volunteer_record and that person's volunteer record is removed from Volunteers tab, then he/she should be tagged as non-volunteer anymore || else, person is still tagged as volunteer because he might have other active volunteer details
            if (volunteer_count == 1)
            {
                db.person_profile.FirstOrDefault(x => x.person_profile_id == record.person_profile_id).is_volunteer = null;
            }

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
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

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
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("grievance_record")]
        public async Task<IActionResult> grievance_record(Guid id)
        {
            var record = db.grievance_record.FirstOrDefault(x => x.grievance_record_id == id);
            var training_record = db.community_training.FirstOrDefault(x => x.community_training_id == record.activity_source_id);
            var ba_record = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == record.activity_source_id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            //v3.1 if deleted in BA or Trainings module, main record should be updated as well:
            if (training_record != null) {
                training_record.push_status_id = 3;
            }

            if (ba_record != null) {
                ba_record.push_status_id = 3;
            }

            //v3.1 update also discussion to deleted state
            var grievance_record_discussion = db.grievance_record_discussion.Where(x => x.grievance_record_id == id).ToList();
            foreach (var d in grievance_record_discussion)
            {
                d.push_status_id = 3;
                d.is_deleted = true;
                d.deleted_by = 0;
                d.deleted_date = DateTime.Now;
            }

            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("sub_project_ers")]
        public async Task<IActionResult> sub_project_ers(Guid id)
        {
            var record = db.sub_project_ers.FirstOrDefault(x => x.sub_project_ers_id == id);
            var main_sp_record = db.sub_project.FirstOrDefault(x => x.sub_project_id == record.sub_project_id);
            
            record.is_deleted = true;
            record.deleted_date = DateTime.Now;
            record.push_status_id = 3;
            main_sp_record.push_status_id = 3;

            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("person_ers_work")]
        public async Task<IActionResult> person_ers_work(Guid id)
        {
            var record = db.person_ers_work.FirstOrDefault(x => x.person_ers_work_id == id);
            var ers_record = db.sub_project_ers.FirstOrDefault(x => x.sub_project_ers_id == record.sub_project_ers_id);
            var main_sp_record = db.sub_project.FirstOrDefault(x => x.sub_project_id == ers_record.sub_project_id);
            var person_profile_record = db.person_ers_work.Where(x => x.person_profile_id == record.person_profile_id && x.is_deleted != true).Count();
            
            record.is_deleted = true;
            record.push_status_id = 3;
            main_sp_record.push_status_id = 3;

            //if there is only 1 record in person_ers_work and that person is deleted from ERS, then he/she should be tagged as non-worker anymore || else, person is still tagged as worker because he might be added to other ERS
            if (person_profile_record == 1) {
                db.person_profile.FirstOrDefault(x => x.person_profile_id == record.person_profile_id).is_worker = null;
            }

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
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("oversight_committee")]
        public async Task<IActionResult> oversight(Guid id)
        {
            var record = db.oversight_committee.FirstOrDefault(x => x.oversight_committee_id == id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

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
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

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
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("perception_survey")]
        public async Task<IActionResult> perception_survey(Guid id)
        {
            var record = db.perception_survey.FirstOrDefault(x => x.perception_survey_id == id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("talakayan_eval")]
        public async Task<IActionResult> talakayan_eval(Guid id)
        {
            var record = db.talakayan_eval.FirstOrDefault(x => x.talakayan_evaluation_id == id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("muni_financial_profile")]
        public async Task<IActionResult> muni_financial_profile(Guid id)
        {
            var record = db.muni_financial_profile.FirstOrDefault(x => x.muni_financial_profile_id == id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }


    }
}