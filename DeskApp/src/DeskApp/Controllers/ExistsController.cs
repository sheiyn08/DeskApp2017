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
    [Route("api/exists")]
    public class ExistsController : Controller
    {


        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;

        public ExistsController(ApplicationDbContext context)
        {
            db = context;

        }

        [HttpGet]
        [Route("community_organization")]

        public bool community_organization(Guid id)
        {
            return db.community_organization.Count(e => e.community_organization_id == id) > 0;
        }



        [HttpGet]
        [Route("perception_survey")]

        public bool perception_survey(Guid id)
        {
            return db.perception_survey.Count(e => e.perception_survey_id == id) > 0;
        }

        [HttpGet]
        [Route("talakayan_eval")]

        public bool talakayan_eval(Guid id)
        {
            return db.talakayan_eval.Count(e => e.talakayan_evaluation_id == id) > 0;
        }

        [HttpGet]
        [Route("dof_blgf_financial_data")]

        public bool dof_blgf_financial_data(int id)
        {
            return db.dof_blgf_financial_data.Count(e => e.dof_blgf_financial_data_record_id == id) > 0;
        }

        [HttpGet]
        [Route("lgpms_data")]

        public bool lgpms_data(int id)
        {
            return db.lgpms_data.Count(e => e.lgpms_data_id == id) > 0;
        }

        [HttpGet]
        [Route("mlgu_financial_data")]

        public bool mlgu_financial_data(Guid id)
        {
            return db.mlgu_financial_data.Count(e => e.mlgu_financial_data_record_id == id) > 0;
        }
        

        [HttpGet]
        [Route("muni_financial_profile")]

        public bool muni_financial_profile(Guid id)
        {
            return db.muni_financial_profile.Count(e => e.muni_financial_profile_id == id) > 0;
        }


        [HttpGet]
        [Route("oversight_committee")]

        public bool oversight_committee(Guid id)
        {
            return db.oversight_committee.Count(e => e.oversight_committee_id == id) > 0;
        }

        [HttpGet]
        [Route("grs_installation")]

        public bool grs_installation(Guid id)
        {
            return db.grs_installation.Count(e => e.grs_installation_id == id) > 0;
        }



        [HttpGet]
        [Route("grievance_record")]

        public bool grievance_record(Guid id)
        {
            return db.grievance_record.Count(e => e.grievance_record_id == id) > 0;
        }


        [HttpGet]
        [Route("person_profile")]

        public bool person_profile(Guid id)
        {
            return db.person_profile.Count(e => e.person_profile_id == id) > 0;
        }


        [HttpGet]
        [Route("barangay_assembly")]

        public bool record_exists(Guid id)
        {
            return db.brgy_assembly.Count(e => e.brgy_assembly_id == id) > 0;
        }




        [HttpGet]
        [Route("barangay_profile")]

        public bool brgy_profile(Guid id)
        {
            return db.brgy_profile.Count(e => e.brgy_profile_id == id) > 0;
        }

        [HttpGet]
        [Route("municipal_profile")]

        public bool muni_profile(Guid id)
        {
            return db.muni_profile.Count(e => e.muni_profile_id == id) > 0;
        }


        [HttpGet]
        [Route("community_training")]

        public bool community_training(Guid id)
        {
            return db.community_training.Count(e => e.community_training_id == id) > 0;
        }

        [HttpGet]
        [Route("mlcc")]

        public bool mlcc(Guid id)
        {
            return db.municipal_lcc.Count(e => e.municipal_lcc_id == id) > 0;
        }

        [HttpGet]
        [Route("mpta")]

        public bool mpta(Guid id)
        {
            return db.municipal_pta.Count(e => e.municipal_pta_id == id) > 0;
        }
    }
}