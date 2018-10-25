using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeskApp.DataLayer;
using DeskApp.Data;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;


namespace DeskApp.Controllers
{
    public class APIGetController : Controller
    {
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public APIGetController(ApplicationDbContext context)
        {
            db = context;
        }

        #region CEAC
        [Route("api/deskapp/ceac_list/get")]
        public IActionResult GetCeacList(Guid id)
        {
            var model = db.ceac_list.FirstOrDefault(x => x.ceac_list_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/ceac_tracking_multiple/get")]
        public IActionResult GetCeacTrackingMultiple(Guid id)
        {
            var model = db.ceac_tracking.Where(x => x.ceac_list_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/ceac_tracking/get")]
        public IActionResult GetCeacTrackingRecord(Guid id)
        {
            var model = db.ceac_tracking.FirstOrDefault(x => x.ceac_tracking_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Barangay Profile
        [Route("api/deskapp/brgy_profile/get")]
        public IActionResult GetBrgyProfile(Guid id)
        {
            var model = db.brgy_profile.FirstOrDefault(x => x.brgy_profile_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Municipal Profile
        [Route("api/deskapp/muni_profile/get")]
        public IActionResult GetMuniProfile(Guid id)
        {
            var model = db.muni_profile.FirstOrDefault(x => x.muni_profile_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Barangay Assembly
        [Route("api/deskapp/brgy_assembly/get")]
        public IActionResult GetBA(Guid id)
        {
            var model = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/brgy_assembly_ip_multiple/get")]
        public IActionResult GetBAIPMultiple(Guid id)
        {
            var model = db.brgy_assembly_ip.Where(x => x.brgy_assembly_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/brgy_assembly_ip/get")]
        public IActionResult GetBAIP(Guid id)
        {
            var model = db.brgy_assembly_ip.FirstOrDefault(x => x.brgy_assembly_ip_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Person Profile
        [Route("api/deskapp/person_profile/get")]
        public IActionResult GetPersonProfile(Guid id)
        {
            var model = db.person_profile.FirstOrDefault(x => x.person_profile_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        //getting all volunteer records of one person (multiple)
        [Route("api/deskapp/person_volunteer_record/get")]
        public IActionResult GetVolunteerRecords(Guid id)
        {
            var model = db.person_volunteer_record.Where(x => x.person_profile_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        //getting details of one volunteer record (one record only)
        [Route("api/deskapp/person_volunteer_detail/get")]
        public IActionResult GetVolunteerDetail(Guid id)
        {
            var model = db.person_volunteer_record.FirstOrDefault(x => x.person_volunteer_record_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Trainings
        [Route("api/deskapp/community_training/get")]
        public IActionResult GetTraining(Guid id)
        {
            var model = db.community_training.FirstOrDefault(x => x.community_training_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/psa_problem/get")]
        public IActionResult GetPSAProblem(Guid id)
        {
            var model = db.psa_problem.Where(x => x.community_training_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/psa_solution/get")]
        public IActionResult GetPSASolution(Guid id)
        {
            var model = db.psa_solution.Where(x => x.psa_problem_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/mibf_criteria/get")]
        public IActionResult GetCriteria(Guid id)
        {
            var model = db.mibf_criteria.Where(x => x.community_training_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/mibf_prioritization/get")]
        public IActionResult GetPrioritization(Guid id)
        {
            var model = db.mibf_prioritization.Where(x => x.community_training_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/person_training/get")]
        public IActionResult GetPersonTraining(Guid id)
        {
            var model = db.person_training.Where(x => x.community_training_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Grievance
        [Route("api/deskapp/grievance_record/get")]
        public IActionResult GetGrievance(Guid id)
        {
            var model = db.grievance_record.FirstOrDefault(x => x.grievance_record_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/grievance_record_discussion_multiple/get")]
        public IActionResult GetGrievanceDiscussionMultiple(Guid id)
        {
            var model = db.grievance_record_discussion.Where(x => x.grievance_record_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/grievance_record_discussion/get")]
        public IActionResult GetGrievanceDiscussion(Guid id)
        {
            var model = db.grievance_record_discussion.FirstOrDefault(x => x.grievance_record_discussion_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region GRS Installation
        [Route("api/deskapp/grs_installation/get")]
        public IActionResult GetGRSInstallation(Guid id)
        {
            var model = db.grs_installation.FirstOrDefault(x => x.grs_installation_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region SubProject
        [Route("api/deskapp/sub_project/get")]
        public IActionResult GetSubProjectUsingID(int id)
        {
            var model = db.sub_project.FirstOrDefault(x => x.sub_project_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/sub_project_unique_id/get")]
        public IActionResult GetSubProjectUsingUniqueID(Guid id)
        {
            var model = db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/sub_project_ers/get")]
        public IActionResult GetERS(Guid id)
        {
            var model = db.sub_project_ers.Where(x => x.sub_project_unique_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/person_ers_work/get")]
        public IActionResult GetERSWorker(Guid id)
        {
            var model = db.person_ers_work.Where(x => x.sub_project_ers_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/sub_project_set/get")]
        public IActionResult GetSET(Guid id)
        {
            var model = db.sub_project_set.Where(x => x.sub_project_unique_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }

        [Route("api/deskapp/sub_project_spcf/get")]
        public IActionResult GetSPCF(Guid id)
        {
            var model = db.sub_project_spcf.Where(x => x.sub_project_unique_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region MPTA
        [Route("api/deskapp/municipal_pta/get")]
        public IActionResult GetPTA(Guid id)
        {
            var model = db.municipal_pta.FirstOrDefault(x => x.municipal_pta_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region MLCC
        [Route("api/deskapp/municipal_lcc/get")]
        public IActionResult GetMLCC(Guid id)
        {
            var model = db.municipal_lcc.FirstOrDefault(x => x.municipal_lcc_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Oversight
        [Route("api/deskapp/oversight_committee/get")]
        public IActionResult GetOversight(Guid id)
        {
            var model = db.oversight_committee.FirstOrDefault(x => x.oversight_committee_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Community Organization
        [Route("api/deskapp/community_organization/get")]
        public IActionResult GetCommOrg(Guid id)
        {
            var model = db.community_organization.FirstOrDefault(x => x.community_organization_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Perception Survey
        [Route("api/deskapp/perception_survey/get")]
        public IActionResult GetPerceptionSurvey(Guid id)
        {
            var model = db.perception_survey.FirstOrDefault(x => x.perception_survey_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Talakayan
        [Route("api/deskapp/talakayan_eval/get")]
        public IActionResult GetTalakayan(Guid id)
        {
            var model = db.talakayan_eval.FirstOrDefault(x => x.talakayan_evaluation_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region Muni Financial Profile
        [Route("api/deskapp/muni_financial_profile/get")]
        public IActionResult GetMuniFinancialProfile(Guid id)
        {
            var model = db.muni_financial_profile.FirstOrDefault(x => x.muni_financial_profile_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

        #region MOV
        [Route("api/deskapp/attached_document/get")]
        public IActionResult GetMOV(Guid id)
        {
            var model = db.attached_document.FirstOrDefault(x => x.attached_document_id == id);

            if (model == null)
            {
                return BadRequest("ID not found");
            }
            else
            {
                return Ok(model);
            }
        }
        #endregion

    }
}

