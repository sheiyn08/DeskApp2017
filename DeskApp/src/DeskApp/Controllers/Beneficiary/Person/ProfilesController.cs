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
//using DocumentFormat.OpenXml.Office2010.Word;
using Newtonsoft.Json;
using System.Data;

namespace DeskApp.Controllers
{
    public class ProfilesController : Controller
    {
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing


        private readonly ApplicationDbContext db;



        /// <summary>
        /// B. Male and Female Community Volunteers Committee Membership
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/export/person_profile/volunteer/commitee/list")]
        public IActionResult export_volunteer_summary_committee_male_female(AngularFilterModel item)
        {
            item.is_volunteer = true;

            var model = GetData(item);


            var result = from s in model
                         join p in db.person_volunteer_record.Where(x => x.is_deleted != true) on s.person_profile_id equals p.person_profile_id
                         select p;

            var export = result.GroupBy(x => new
            {
                x.person_profile.lib_region.region_name,
                x.person_profile.lib_province.prov_name,
                x.person_profile.lib_city.city_name,
                x.lib_volunteer_committee.name,

                brgy_name = x.person_profile.lib_brgy.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(cx => cx.brgy_code == x.person_profile.brgy_code).brgy_name,
                
            }).Select(x => new
            {
                region_name = x.Key.region_name,
                province_name = x.Key.prov_name,
                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,
                committee_name = x.Key.name,

                male_chair = x.Where(xc => xc.person_profile.sex == true && xc.volunteer_committee_position_id == 2).Count(),
                female_chair = x.Where(t => t.person_profile.sex != true && t.volunteer_committee_position_id == 2).Count(),

                male_member = x.Where(xc => xc.person_profile.sex == true && xc.volunteer_committee_position_id == 1).Count(),
                female_member = x.Where(t => t.person_profile.sex != true && t.volunteer_committee_position_id == 1).Count(),
            });


            return Ok(export);
        }

        #region ACT Monthly Report - Total number of volunteers per committee

        [HttpPost]
        [Route("/api/export/report/number_of_volunteers_per_comm/as_of")]
        public IActionResult export_actreport_asof(AngularFilterModel item)
        {
            DateTime? as_of = item.as_of;
            int? selected_fund_source = item.fund_source_id;
            item.is_volunteer = true;

            var volunteer_record = db.person_volunteer_record.Where(x => x.start_date <= as_of && x.fund_source_id == selected_fund_source);
            var person_record = db.person_profile;

            int date_today = DateTime.Now.Year;

            var joined_result = from p in person_record
                                join v in volunteer_record.Where(x => x.is_deleted != true) on p.person_profile_id equals v.person_profile_id
                                select v;
            
            var export = joined_result
                .GroupBy(x => new
                {
                    x.lib_volunteer_committee.volunteer_committee_id,
                    x.lib_fund_source.fund_source_id,
                    x.lib_cycle.name
                        
                }).Select(x => new
                {
                    cycle_name = x.Key.name,
                    committee_name = x.Key.volunteer_committee_id,
                    
                    count_leader_male = x.Where(xc => xc.person_profile.sex == true && xc.volunteer_committee_position_id == 2).Count(),
                    count_leader_female = x.Where(xc => xc.person_profile.sex != true && xc.volunteer_committee_position_id == 2).Count(),
                    count_member_male = x.Where(xc => xc.person_profile.sex == true && xc.volunteer_committee_position_id == 1).Count(),
                    count_member_female = x.Where(xc => xc.person_profile.sex != true && xc.volunteer_committee_position_id == 1).Count(),

                    count_pantawid_male = x.Where(xc => xc.person_profile.sex == true && xc.person_profile.is_pantawid == true).Count(),
                    count_pantawid_female = x.Where(xc => xc.person_profile.sex != true && xc.person_profile.is_pantawid == true).Count(),
                    count_ip_male = x.Where(xc => xc.person_profile.sex == true && xc.person_profile.is_ip == true).Count(),
                    count_ip_female = x.Where(xc => xc.person_profile.sex != true && xc.person_profile.is_ip == true).Count(),
                    count_slp_male = x.Where(xc => xc.person_profile.sex == true && xc.person_profile.is_slp == true).Count(),
                    count_slp_female = x.Where(xc => xc.person_profile.sex != true && xc.person_profile.is_slp == true).Count(),
                    count_senior_male = x.Where(xc => xc.person_profile.sex == true && ((xc.person_profile.birthdate.HasValue ? (date_today - xc.person_profile.birthdate.Value.Year) : (int?)0) >= 60)).Count(),
                    count_senior_female = x.Where(xc => xc.person_profile.sex != true && ((xc.person_profile.birthdate.HasValue ? (date_today - xc.person_profile.birthdate.Value.Year) : (int?)0) >= 60)).Count(),
                });
            return Ok(export);
        }



        [HttpPost]
        [Route("/api/export/report/number_of_volunteers_per_comm/for_the_period_of")]
        public IActionResult export_actreport_fortheperiodof(AngularFilterModel item)
        {
            DateTime? fortheperiodof_from = item.fortheperiodof_from;
            DateTime? fortheperiodof_to = item.fortheperiodof_to;
            int? selected_fund_source = item.fund_source_id;
            item.is_volunteer = true;

            var volunteer_record = db.person_volunteer_record.Where(x => x.fund_source_id == selected_fund_source && (x.start_date >= fortheperiodof_from && x.start_date <= fortheperiodof_to));
            var person_record = db.person_profile;

            int date_today = DateTime.Now.Year;

            var joined_result = from p in person_record
                                join v in volunteer_record.Where(x => x.is_deleted != true) on p.person_profile_id equals v.person_profile_id
                                select v;

            var export = joined_result
                .GroupBy(x => new
                {
                    x.lib_volunteer_committee.volunteer_committee_id,
                    x.lib_fund_source.fund_source_id,
                    x.lib_cycle.name

                }).Select(x => new
                {
                    cycle_name = x.Key.name,
                    committee_name = x.Key.volunteer_committee_id,

                    count_leader_male = x.Where(xc => xc.person_profile.sex == true && xc.volunteer_committee_position_id == 2).Count(),
                    count_leader_female = x.Where(xc => xc.person_profile.sex != true && xc.volunteer_committee_position_id == 2).Count(),
                    count_member_male = x.Where(xc => xc.person_profile.sex == true && xc.volunteer_committee_position_id == 1).Count(),
                    count_member_female = x.Where(xc => xc.person_profile.sex != true && xc.volunteer_committee_position_id == 1).Count(),

                    count_pantawid_male = x.Where(xc => xc.person_profile.sex == true && xc.person_profile.is_pantawid == true).Count(),
                    count_pantawid_female = x.Where(xc => xc.person_profile.sex != true && xc.person_profile.is_pantawid == true).Count(),
                    count_ip_male = x.Where(xc => xc.person_profile.sex == true && xc.person_profile.is_ip == true).Count(),
                    count_ip_female = x.Where(xc => xc.person_profile.sex != true && xc.person_profile.is_ip == true).Count(),
                    count_slp_male = x.Where(xc => xc.person_profile.sex == true && xc.person_profile.is_slp == true).Count(),
                    count_slp_female = x.Where(xc => xc.person_profile.sex != true && xc.person_profile.is_slp == true).Count(),
                    count_senior_male = x.Where(xc => xc.person_profile.sex == true && ((xc.person_profile.birthdate.HasValue ? (date_today - xc.person_profile.birthdate.Value.Year) : (int?)0) >= 60)).Count(),
                    count_senior_female = x.Where(xc => xc.person_profile.sex != true && ((xc.person_profile.birthdate.HasValue ? (date_today - xc.person_profile.birthdate.Value.Year) : (int?)0) >= 60)).Count(),
                });
            return Ok(export);
        }

        #endregion



        /// <summary>
        /// Summary of Male and Female Cvs 1
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/export/person_profile/volunteer/barangay/list")]
        public IActionResult export_volunteer_summary_male_female(AngularFilterModel item)
        {

            item.is_volunteer = true;

            var model = GetData(item);


            var result = from s in model
                         join p in db.person_volunteer_record.Where(x => x.is_deleted != true) on s.person_profile_id equals p.person_profile_id
                         select p;

            var export = result.GroupBy(x => new
            {
                x.person_profile.lib_region.region_name,
                x.person_profile.lib_province.prov_name,
                x.person_profile.lib_city.city_name,
                brgy_name = x.person_profile.lib_brgy.brgy_code == null ? "(No barangay stated)" : db.lib_brgy.FirstOrDefault(cx => cx.brgy_code == x.person_profile.brgy_code).brgy_name,
                   
            }).
            Select(x => new
            {
                region_name = x.Key.region_name,
                province_name = x.Key.prov_name,
                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,

                //male = x.Where(xc => xc.person_profile.sex == true).Count(),
                //female = x.Where(t => t.person_profile.sex != true).Count(),
               male = x.Where(t => t.person_profile.sex == true).Select(t => t.person_profile_id).Distinct().Count(),
               female = x.Where(t => t.person_profile.sex != true).Select(t => t.person_profile_id).Distinct().Count(),
               
                male_ip = x.Where(t => t.person_profile.sex == true && t.person_profile.is_ip == true).Count(),
                male_pantawid = x.Where(t => t.person_profile.sex == true && t.person_profile.is_pantawid == true).Count(),
                male_slp = x.Where(t => t.person_profile.sex == true && t.person_profile.is_slp == true).Count(),
                male_lgu = x.Where(t => t.person_profile.sex == true && t.person_profile.is_lguofficial == true).Count(),

                female_ip = x.Where(t => t.person_profile.sex != true && t.person_profile.is_ip == true).Count(),
                female_pantawid = x.Where(t => t.person_profile.sex != true && t.person_profile.is_pantawid == true).Count(),
                female_slp = x.Where(t => t.person_profile.sex != true && t.person_profile.is_slp == true).Count(),
                female_lgu = x.Where(t => t.person_profile.sex != true && t.person_profile.is_lguofficial == true).Count(),

            });


            return Ok(export);
        }


        [HttpPost]
        [Route("/api/export/person_profile/list")]
        public IActionResult person_profile(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from s in model
                         select new
                         {
                             s.person_profile_id,
                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             s.lib_brgy.brgy_name,
                             s.first_name,
                             s.middle_name,
                             s.last_name,
                             sex = s.sex == true ? "Male" : s.sex == false? "Female" : "",
                             //s.birthdate,

                             //4.2: format dates to dd/mm/yyyy
                             birthdate = s.birthdate == null ? "" : s.birthdate.Value.ToString("dd/MM/yyyy"),

                             age = s.birthdate == null ? "" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),
                             marital_status = s.lib_civil_status.name,
                             occupation = s.lib_occupation.name,
                             s.no_children,
                             educational_attainment = s.lib_education_attainment.name,
                             //is_volunteer = db.person_volunteer_record.Count(x => x.person_profile_id == s.person_profile_id && x.is_deleted != true) > 0 ? "Yes" : "",
                             is_volunteer = s.is_volunteer == true ? "Yes" : "",
                             is_worker = db.person_ers_work.Count(x => x.person_profile_id == s.person_profile_id && x.is_deleted != true) > 0 ? "Yes" : "",
                             is_ip = s.is_ip == true ? "Yes" : s.is_ip == false ? "No" : "",
                             is_ip_leader = s.is_ipleader == true ? "Yes" : s.is_ipleader == false ? "No" :"",
                             is_pantawid = s.is_pantawid == true ? "Yes" : s.is_pantawid == false ? "No" : "",
                             is_pantawid_leader = s.is_pantawid_leader == true ? "Yes" : s.is_pantawid_leader == false ? "No" : "",
                             is_slp = s.is_slp == true ? "Yes" : s.is_slp == false ? "No" : "",
                             is_slp_leader = s.is_slp_leader == true ? "Yes" : s.is_slp_leader == false ? "No" : "",
                             
                             has_attended_in_BAR = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 1) > 0 ? "Yes" : "",
                             has_attended_in_BPSA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 2) > 0 ? "Yes" : "",
                             has_attended_in_CMVP = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 3) > 0 ? "Yes" : "",
                             has_attended_in_CSW = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 4) > 0 ? "Yes" : "",
                             has_attended_in_Finance = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 5) > 0 ? "Yes" : "",
                             has_attended_in_GAD = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 6) > 0 ? "Yes" : "",
                             has_attended_in_INFRA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 7) > 0 ? "Yes" : "",
                             has_attended_in_LGU_training = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 8) > 0 ? "Yes" : "",
                             has_attended_in_MF = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 9) > 0 ? "Yes" : "",
                             has_attended_in_MIAC = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 11) > 0 ? "Yes" : "",
                             has_attended_in_MIBFPRA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 11) > 0 ? "Yes" : "",
                             has_attended_in_MO = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 12) > 0 ? "Yes" : "",
                             has_attended_in_MPSA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 13) > 0 ? "Yes" : "",
                             has_attended_in_MunAR = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 14) > 0 ? "Yes" : "",
                             has_attended_in_OM = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 15) > 0 ? "Yes" : "",
                             has_attended_in_ODM = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 16) > 0 ? "Yes" : "",
                             has_attended_in_Others = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 17) > 0 ? "Yes" : "",
                             has_attended_in_PCM = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 18) > 0 ? "Yes" : "",
                             has_attended_in_PPDW = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 19) > 0 ? "Yes" : "",
                             has_attended_in_Proc = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 20) > 0 ? "Yes" : "",
                             has_attended_in_SpecialMIBF = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 21) > 0 ? "Yes" : "",
                             has_attended_in_SPW = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 22) > 0 ? "Yes" : "",
                             
                             is_sector_academe = s.is_sector_academe == true ? "Yes" : "",
                             is_sector_business = s.is_sector_business == true ? "Yes" : "",
                             is_sector_pwd = s.is_sector_pwd == true ? "Yes" : "",
                             is_sector_farmer = s.is_sector_farmer == true ? "Yes" : "",
                             is_sector_fisherfolks = s.is_sector_fisherfolks == true ? "Yes" : "",
                             is_sector_government = s.is_sector_government == true ? "Yes" : "",
                             is_sector_ip = s.is_sector_ip == true ? "Yes" : "",
                             is_sector_ngo = s.is_sector_ngo == true ? "Yes" : "",
                             is_sector_po = s.is_sector_po == true ? "Yes" : "",
                             is_sector_religios = s.is_sector_religios == true ? "Yes" : "",
                             is_sector_senior = s.is_sector_senior == true ? "Yes" : "",
                             is_sector_women = s.is_sector_women == true ? "Yes" : "",
                             is_sector_youth = s.is_sector_youth == true ? "Yes" : "",


                         };

            return Ok(result);
        }

        [HttpPost]
        [Route("/api/export/person_profile/simple/list")]
        public IActionResult person_profile_simple(AngularFilterModel item)
        {
            var model = GetData(item);
            
            var result = from s in model
                         join p in db.person_volunteer_record
                         on s.person_profile_id equals p.person_profile_id
                         where p.is_deleted != true
                         select new
                         {
                             s.person_profile_id,
                             p.volunteer_committee_id,

                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,

                             s.first_name,
                             s.middle_name,
                             s.last_name,

                             sex = s.sex == true ? "Male" : "Female",
                             s.birthdate,

                             age = s.birthdate == null ? "" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),

                             committee = p.lib_volunteer_committee.name,
                             position = p .lib_volunteer_committee_position.name,

                             

                             marital_status = s.lib_civil_status.name,

                             occupation = s.lib_occupation.name,
                             s.no_children,

                             educational_attainment = s.lib_education_attainment.name,

                          
                             is_ip = s.is_ip != null ? "YES" : "",

                             is_ip_leader = s.is_ip != null ? "YES" : "",
                             is_pantawid = s.is_pantawid != null ? "YES" : "",
                             is_slp = s.is_slp != null ? "YES" : "",


                             
                          
                             is_sector_academe = s.is_sector_academe == true ? "Yes" : "",
                             is_sector_business = s.is_sector_business == true ? "Yes" : "",
                             is_sector_pwd = s.is_sector_pwd == true ? "Yes" : "",
                             is_sector_farmer = s.is_sector_farmer == true ? "Yes" : "",
                             is_sector_fisherfolks = s.is_sector_fisherfolks == true ? "Yes" : "",
                             is_sector_government = s.is_sector_government == true ? "Yes" : "",
                             is_sector_ip = s.is_sector_ip == true ? "Yes" : "",
                             is_sector_ngo = s.is_sector_ngo == true ? "Yes" : "",
                             is_sector_po = s.is_sector_po == true ? "Yes" : "",
                             is_sector_religios = s.is_sector_religios == true ? "Yes" : "",
                             is_sector_senior = s.is_sector_senior == true ? "Yes" : "",
                             is_sector_women = s.is_sector_women == true ? "Yes" : "",
                             is_sector_youth = s.is_sector_youth == true ? "Yes" : "",


                         };

            return Ok(result);
        }
        
        [Route("api/export/person_profile/volunteer_per_committee")]
        public IActionResult volunteer_per_committee(AngularFilterModel item)
        {
            var model = GetData(item);

            int? selected_cycle = item.cycle_id;
            int? selected_fund_source = item.fund_source_id;

            var result = from s in model
                         join p in db.person_volunteer_record
                         on s.person_profile_id equals p.person_profile_id
                         where p.is_deleted != true
                         select new
                         {
                             s.person_profile_id,

                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,


                             s.first_name,
                             s.middle_name,
                             s.last_name,
                             sex = s.sex == true ? "Male" : "Female",
                             s.birthdate,
                             age = s.birthdate == null ? "" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),
                             committee = p.lib_volunteer_committee.name,
                             position = p.lib_volunteer_committee_position.name,
                             
                             cycle_id = p.lib_cycle.cycle_id,
                             fund_source_id = p.fund_source_id
                         };

            var items = result
                .Select(x => new
                {
                    region_name = x.region_name,
                    prov_name = x.prov_name,
                    city_name = x.city_name,
                    brgy_name = x.brgy_name,

                    cycle_id = x.cycle_id,
                    fund_source_id = x.fund_source_id,
                    first_name = x.first_name,
                    middle_name = x.middle_name,
                    last_name = x.last_name,
                    sex = x.sex,
                    age = x.age,
                    committee = x.committee,
                    position = x.position,                    
                })
                .Where(x => x.fund_source_id == selected_fund_source && x.cycle_id == item.cycle_id);

            return Ok(items);
        }


        [HttpPost]
        [Route("/api/export/person_profile/volunteer/list")]
        public IActionResult export_volunteer(AngularFilterModel item)
        {

            item.is_volunteer = true;

            var model = GetData(item);


            var result = from s in model
                         join p in db.person_volunteer_record.Where(x => x.is_deleted != true) on s.person_profile_id equals p.person_profile_id
                         select p;

            var export = result.GroupBy(x => new
            {
                x.person_profile.lib_region.region_name,
                x.person_profile.lib_province.prov_name,
                x.person_profile.lib_city.city_name,
                brgy_name = x.person_profile.lib_brgy.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(cx => cx.brgy_code == x.person_profile.brgy_code).brgy_name,
                
                //x.person_profile.lib_region,
                //x.person_profile.lib_province,
                //x.person_profile.lib_city,                
                //x.person_profile.lib_brgy,
                x.lib_cycle,
                x.lib_fund_source,
                x.lib_enrollment,

            }).Select(x => new
            {
                region_name = x.Key.region_name,
                prov_name = x.Key.prov_name,
                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,

                fund_source_name = x.Key.lib_fund_source.name,
                cycle_name = x.Key.lib_cycle.name,
                kc_mode = x.Key.lib_enrollment.name,
                
                no_of_ait_head = x.Where(c => c.volunteer_committee_id == 1 && c.volunteer_committee_position_id == 2).Count(),
                no_of_ait_member = x.Where(c => c.volunteer_committee_id == 1 && c.volunteer_committee_position_id == 1).Count(),
                no_of_bac_head = x.Where(c => c.volunteer_committee_id == 2 && c.volunteer_committee_position_id == 2).Count(),
                no_of_bac_member = x.Where(c => c.volunteer_committee_id == 2 && c.volunteer_committee_position_id == 1).Count(),
                no_of_bgd_head = x.Where(c => c.volunteer_committee_id == 3 && c.volunteer_committee_position_id == 2).Count(),
                no_of_bgd_member = x.Where(c => c.volunteer_committee_id == 3 && c.volunteer_committee_position_id == 1).Count(),
                no_of_brt_head = x.Where(c => c.volunteer_committee_id == 4 && c.volunteer_committee_position_id == 2).Count(),
                no_of_brt_member = x.Where(c => c.volunteer_committee_id == 4 && c.volunteer_committee_position_id == 1).Count(),
                no_of_cmt_head = x.Where(c => c.volunteer_committee_id == 5 && c.volunteer_committee_position_id == 2).Count(),
                no_of_cmt_member = x.Where(c => c.volunteer_committee_id == 5 && c.volunteer_committee_position_id == 1).Count(),
                no_of_grs_head = x.Where(c => c.volunteer_committee_id == 6 && c.volunteer_committee_position_id == 2).Count(),
                no_of_grs_member = x.Where(c => c.volunteer_committee_id == 6 && c.volunteer_committee_position_id == 1).Count(),
                no_of_mit_head = x.Where(c => c.volunteer_committee_id == 7 && c.volunteer_committee_position_id == 2).Count(),
                no_of_mit_member = x.Where(c => c.volunteer_committee_id == 7 && c.volunteer_committee_position_id == 1).Count(),
                no_of_oam_head = x.Where(c => c.volunteer_committee_id == 8 && c.volunteer_committee_position_id == 2).Count(),
                no_of_oam_member = x.Where(c => c.volunteer_committee_id == 8 && c.volunteer_committee_position_id == 1).Count(),
                no_of_pit_head = x.Where(c => c.volunteer_committee_id == 9 && c.volunteer_committee_position_id == 2).Count(),
                no_of_pit_member = x.Where(c => c.volunteer_committee_id == 9 && c.volunteer_committee_position_id == 1).Count(),
                no_of_ppt_head = x.Where(c => c.volunteer_committee_id == 10 && c.volunteer_committee_position_id == 2).Count(),
                no_of_ppt_member = x.Where(c => c.volunteer_committee_id == 10 && c.volunteer_committee_position_id == 1).Count(),
                no_of_psa_head = x.Where(c => c.volunteer_committee_id == 11 && c.volunteer_committee_position_id == 2).Count(),
                no_of_psa_member = x.Where(c => c.volunteer_committee_id == 11 && c.volunteer_committee_position_id == 1).Count(),
                no_of_pt_head = x.Where(c => c.volunteer_committee_id == 12 && c.volunteer_committee_position_id == 2).Count(),
                no_of_pt_member = x.Where(c => c.volunteer_committee_id == 12 && c.volunteer_committee_position_id == 1).Count(),


            });


            return Ok(export);
        }

        //05-11-18: New Export 4.0: Volunteer Profile List
        [HttpPost]
        [Route("/api/export/person_profile/volunteer/detailed_list")]
        public IActionResult export_volunteer_list(AngularFilterModel item)
        {
            item.is_volunteer = true;
            var model = GetData(item);

            var result = from s in model
                         join p in db.person_volunteer_record.Where(x => x.is_deleted != true) on s.person_profile_id equals p.person_profile_id
                         select p;

            var export = result.Select(x => new
            {
                x.person_profile_id,
                x.person_profile.first_name,
                x.person_profile.middle_name,
                x.person_profile.last_name,
                x.person_profile.lib_region.region_name,
                x.person_profile.lib_province.prov_name,
                x.person_profile.lib_city.city_name,
                x.person_profile.lib_brgy.brgy_name,
                //4.2: format dates to dd/mm/yyyy
                date_appointment = x.person_profile.date_appointment == null ? "" : x.person_profile.date_appointment.Value.ToString("dd/MM/yyyy"),
                fund_source = x.lib_fund_source.name,
                cycle = x.lib_cycle.name,
                mode = x.lib_enrollment.name,
                committee = x.lib_volunteer_committee.name,
                position = x.lib_volunteer_committee_position.name,
                //4.2: format dates to dd/mm/yyyy
                date_start = x.start_date == null ? "" : x.start_date.Value.ToString("dd/MM/yyyy"),
                date_end = x.end_date == null ? "" : x.end_date.Value.ToString("dd/MM/yyyy")
            }).OrderBy(s => s.person_profile_id);

            return Ok(export);
        }


        [HttpPost]
        [Route("/api/export/person_profile/trained/list")]
        public IActionResult export_volunteer_trained(AngularFilterModel item)
        {

            var model = GetData(item);


            var result = from s in model
                         join p in db.person_training
                         .Include(x => x.community_training)
                         .Include(x => x.community_training.lib_region)
                            .Include(x => x.community_training.lib_province)
                               .Include(x => x.community_training.lib_city)
                                  .Include(x => x.community_training.lib_brgy)
                                     .Include(x => x.community_training.lib_cycle)
                                        .Include(x => x.community_training.lib_fund_source)
                                           .Include(x => x.community_training.lib_enrollment)
                         .Where(x => x.is_deleted != true && x.community_training.is_deleted != true) on s.person_profile_id equals p.person_profile_id
                         select p;

            var export = result.GroupBy(x => new
            {
                x.community_training.lib_region,
                //x.community_training.lib_province,
                //x.community_training.lib_city,
                //x.community_training.lib_brgy,
                x.community_training.lib_cycle,
                x.community_training.lib_fund_source,
                x.community_training.lib_enrollment,

            })

            .Select(x => new
            {

                fund_source_name = x.Key.lib_fund_source.name,
                cycle_name = x.Key.lib_cycle.name,
                kc_mode = x.Key.lib_enrollment.name,
                region_name = x.Key.lib_region.region_name,

                //prov_name = x.Key.lib_province.prov_name,

                //city_name = x.Key.lib_city.city_name,
                //brgy_name = x.Key.lib_brgy.brgy_name,


                has_attended_in_BAR = x.Count(c => c.community_training.training_category_id == 1),
                has_attended_in_BPSA = x.Count(c => c.community_training.training_category_id == 2),
                has_attended_in_CMVP = x.Count(c => c.community_training.training_category_id == 3),

                has_attended_in_CSW = x.Count(c => c.community_training.training_category_id == 4),

                has_attended_in_Finance = x.Count(c => c.community_training.training_category_id == 5),

                has_attended_in_GAD = x.Count(c => c.community_training.training_category_id == 6),
                has_attended_in_INFRA = x.Count(c => c.community_training.training_category_id == 7),
                has_attended_in_LGU_training = x.Count(c => c.community_training.training_category_id == 8),
                has_attended_in_MF = x.Count(c => c.community_training.training_category_id == 9),
                has_attended_in_MIAC = x.Count(c => c.community_training.training_category_id == 11),

                has_attended_in_MIBFPRA = x.Count(c => c.community_training.training_category_id == 11),

                has_attended_in_MO = x.Count(c => c.community_training.training_category_id == 12),
                has_attended_in_MPSA = x.Count(c => c.community_training.training_category_id == 13),
                has_attended_in_MunAR = x.Count(c => c.community_training.training_category_id == 14),
                has_attended_in_OM = x.Count(c => c.community_training.training_category_id == 15),
                has_attended_in_ODM = x.Count(c => c.community_training.training_category_id == 16),
                has_attended_in_Others = x.Count(c => c.community_training.training_category_id == 17),
                has_attended_in_PCM = x.Count(c => c.community_training.training_category_id == 18),
                has_attended_in_PPDW = x.Count(c => c.community_training.training_category_id == 19),
                has_attended_in_Proc = x.Count(c => c.community_training.training_category_id == 20),
                has_attended_in_SpecialMIBF = x.Count(c => c.community_training.training_category_id == 21),

                has_attended_in_SPW = x.Count(c => c.community_training.training_category_id == 22),

            });


            return Ok(export);
        }

        #region DQA for Person Profile

        //DQA: Created by Jessy
        [HttpPost]
        [Route("/api/export/person_profile/volunteer/dqa/list")]
        public IActionResult export_dqa_volunteer(AngularFilterModel item)
        {
            item.is_volunteer = true;
            var model = GetData(item);

            var export = from s1 in model
                         join s2 in db.person_volunteer_record on s1.person_profile_id equals s2.person_profile_id where s2.is_deleted != true
                         select new
                         {
                             s1.person_profile_id,
                             s1.lib_region.region_name,
                             s1.lib_province.prov_name,
                             s1.lib_city.city_name,
                             s1.lib_brgy.brgy_name,
                             s1.first_name,
                             s1.middle_name,
                             s1.last_name,
                             age = s1.birthdate == null ? "" : (DateTime.Now.Year - s1.birthdate.Value.Year).ToString(),
                             age_remarks = s1.birthdate == null ? "No Birthday (no age) " : (DateTime.Now.Year - s1.birthdate.Value.Year) < 15 ? "Less Than 15 Years Old" : (DateTime.Now.Year - s1.birthdate.Value.Year) > 100 ? "More than 100 Years Old" : "",
                             committee_remarks = s2.lib_volunteer_committee.name == null ? "No Committee" : "With Committee (" + s2.lib_volunteer_committee.name + ")",
                             more_than_one_head = db.person_volunteer_record.Where(x => x.person_profile_id == s1.person_profile_id && x.volunteer_committee_position_id == 2 && x.is_deleted != true).Count() > 1 ? "More than 1" : "",
                             no_of_committes_as_head = db.person_volunteer_record.Where(x => x.person_profile_id == s1.person_profile_id && x.volunteer_committee_position_id == 2 && x.is_deleted != true).Count(),
                             no_of_committes_as_member = db.person_volunteer_record.Where(x => x.person_profile_id == s1.person_profile_id && x.volunteer_committee_position_id == 1 && x.is_deleted != true).Count()
                         };

            return Ok(export);
        }

        //DQA: Displays records with blank sex and/or same name with different sex
        [HttpPost]
        [Route("/api/export/person_profile/dqa/same_name_different_sex")]
        public IActionResult export_dqa_same_name_different_sex(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from p1 in model
                         join p2 in model on
                            new {
                                join1 = p1.region_code,
                                join2 = p1.prov_code,
                                join3 = p1.city_code,
                                join4 = p1.brgy_code,
                                join5 = p1.first_name.ToLower(),
                                join6 = p1.last_name.ToLower(),
                                join7 = p1.birthdate
                            } equals
                            new {
                                join1 = p2.region_code,
                                join2 = p2.prov_code,
                                join3 = p2.city_code,
                                join4 = p2.brgy_code,
                                join5 = p2.first_name.ToLower(),
                                join6 = p2.last_name.ToLower(),
                                join7 = p2.birthdate
                            }
                         where p2.sex != p1.sex || p1.sex == null
                         orderby p1.brgy_code
                         select new
                         {
                             Person_Unique_Id = p1.person_profile_id,
                             Region = p1.lib_region.region_name == null ? null : p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name == null ? null : p1.lib_province.prov_name,
                             Municipality = p1.lib_city.city_name == null ? null : p1.lib_city.city_name,
                             Barangay = p1.lib_brgy.brgy_name == null ? null : p1.lib_brgy.brgy_name,
                             Last_Name = p1.last_name,
                             First_Name = p1.first_name,
                             Birthday = p1.birthdate == null ? "" : p1.birthdate.Value.ToString("dd/MM/yyyy"),
                             Sex = p1.sex == null ?  null : p1.sex == true ? "Male" : "Female"
                         };

            return Ok(result);
        }

        //DQA: Displays all CVs without committee
        [HttpPost]
        [Route("/api/export/person_profile/dqa/cv_without_committee")]
        public IActionResult export_dqa_cv_without_committee(AngularFilterModel item)
        {
            item.is_volunteer = true;
            var model = GetData(item);

            var result = from p1 in model
                         join p2 in db.person_volunteer_record on p1.person_profile_id equals p2.person_profile_id
                         where p2.volunteer_committee_id == null
                         select new
                         {
                             Person_Unique_Id = p1.person_profile_id,
                             Region = p1.lib_region.region_name == null ? null : p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name == null ? null : p1.lib_province.prov_name,
                             Municipality = p1.lib_city.city_name == null ? null : p1.lib_city.city_name,
                             Barangay = p1.lib_brgy.brgy_name == null ? null : p1.lib_brgy.brgy_name,
                             Last_Name = p1.last_name,
                             First_Name = p1.first_name,
                             Birthday = p1.birthdate == null ? "" : p1.birthdate.Value.ToString("dd/MM/yyyy"),
                             Sex = p1.sex == null ? null : p1.sex == true ? "Male" : "Female",
                             Cycle = p2.lib_cycle.name,
                             Committee = p2.lib_volunteer_committee.name == null ? null : p2.lib_volunteer_committee.name,
                             Position = p2.lib_volunteer_committee_position.name == null ? null : p2.lib_volunteer_committee_position.name
                         };

            return Ok(result);
        }

        //DQA: Displays all CVs with committee but without position
        [HttpPost]
        [Route("/api/export/person_profile/dqa/cv_without_position")]
        public IActionResult export_dqa_cv_without_position(AngularFilterModel item)
        {
            item.is_volunteer = true;
            var model = GetData(item);

            var result = from p1 in model
                         join p2 in db.person_volunteer_record on p1.person_profile_id equals p2.person_profile_id
                         where (p2.volunteer_committee_id != null && p2.volunteer_committee_position_id == null) || p2.volunteer_committee_position_id == null
                         select new
                         {
                             Person_Unique_Id = p1.person_profile_id,
                             Region = p1.lib_region.region_name == null ? null : p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name == null ? null : p1.lib_province.prov_name,
                             Municipality = p1.lib_city.city_name == null ? null : p1.lib_city.city_name,
                             Barangay = p1.lib_brgy.brgy_name == null ? null : p1.lib_brgy.brgy_name,
                             Last_Name = p1.last_name,
                             First_Name = p1.first_name,
                             Birthday = p1.birthdate == null ? "" : p1.birthdate.Value.ToString("dd/MM/yyyy"),
                             Sex = p1.sex == null ? null : p1.sex == true ? "Male" : "Female",
                             Cycle = p2.lib_cycle.name,
                             Committee = p2.lib_volunteer_committee.name == null ? null : p2.lib_volunteer_committee.name,
                             Position = p2.lib_volunteer_committee_position.name == null ? null : p2.lib_volunteer_committee_position.name
                         };

            return Ok(result);
        }

        //DQA: Displays records with same birthday, same location, and cycle and same committee -- WITH ERROR! (not yet in report_list)
        [HttpPost]
        [Route("/api/export/person_profile/dqa/possible_duplicates")]
        public IActionResult export_dqa_possible_duplicates(AngularFilterModel item)
        {
            item.is_volunteer = true;
            var model = GetData(item);
            var volunteer_record = db.person_volunteer_record.Where(x => x.is_deleted != true);
            
            var query1 = from pv1 in volunteer_record
                         join pv2 in volunteer_record on
                         new
                         {
                             join1 = pv1.cycle_id,
                             join2 = pv1.volunteer_committee_id
                         } equals
                         new
                         {
                             join1 = pv2.cycle_id,
                             join2 = pv2.volunteer_committee_id
                         }
                         where pv2.person_profile_id != pv1.person_profile_id
                         select new
                         {
                             pv1.person_profile_id,
                             cycle = pv1.lib_cycle.name,
                             committee = pv1.lib_volunteer_committee.name,
                             pv1.cycle_id,
                             pv1.volunteer_committee_id,
                         };

            var query2 = from p1 in model
                         join p2 in model on
                         new
                         {
                             join1 = p1.birthdate,
                             join2 = p1.region_code,
                             join3 = p1.prov_code,
                             join4 = p1.city_code,
                             join5 = p1.brgy_code
                         } equals
                         new
                         {
                             join1 = p2.birthdate,
                             join2 = p2.region_code,
                             join3 = p2.prov_code,
                             join4 = p2.city_code,
                             join5 = p2.brgy_code
                         }
                         select new
                         {
                             p1.person_profile_id,
                             p1.birthdate,
                             Region = p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name,
                             City = p1.lib_city.city_name,
                             Brgy = p1.lib_brgy.brgy_name,
                             p1.region_code,
                             p1.prov_code,
                             p1.city_code,
                             p1.brgy_code,
                             p1.last_name,
                             p1.first_name
                         };
                        
            var result = from q1 in query1
                         join q2 in query2 on q1.person_profile_id equals q2.person_profile_id
                         select new
                         {
                             q1,
                             q2,
                             //Person_Unique_Id = q2.person_profile_id,
                             //Region = q2.Region == null ? null : q2.Region,
                             //Province = q2.Province == null ? null : q2.Province,
                             //Municipality = q2.City == null ? null : q2.City,
                             //Barangay = q2.Brgy == null ? null : q2.Brgy,
                             //Cycle = q1.cycle,
                             //Last_Name = q2.last_name,
                             //First_Name = q2.first_name,
                             //Birthday = q2.birthdate.Value.ToString("MM/dd/yyyy"),
                             //Committee = q1.committee
                         };         
            
            return Ok(result);
        }

        //DQA: Displays all CVs with missing birthdates
        [HttpPost]
        [Route("/api/export/person_profile/dqa/missing_birthdates")]
        public IActionResult export_dqa_missing_birthdates(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from p1 in model
                         where p1.birthdate == null
                         select new
                         {
                             Person_Unique_Id = p1.person_profile_id,
                             Region = p1.lib_region.region_name == null ? null : p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name == null ? null : p1.lib_province.prov_name,
                             Municipality = p1.lib_city.city_name == null ? null : p1.lib_city.city_name,
                             Barangay = p1.lib_brgy.brgy_name == null ? null : p1.lib_brgy.brgy_name,
                             Last_Name = p1.last_name,
                             First_Name = p1.first_name,                             
                             Sex = p1.sex == null ? null : p1.sex == true ? "Male" : "Female",
                             Birthdate = p1.birthdate == null ? "" : p1.birthdate.Value.ToString("dd/MM/yyyy")
                         };

            return Ok(result);
        }

        //DQA: Displays records with same name and location, but differs in birthdate.
        [HttpPost]
        [Route("/api/export/person_profile/dqa/same_name_different_bdate")]
        public IActionResult export_dqa_same_name_different_bdate(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from p1 in model
                         join p2 in model on
                            new
                            {
                                join1 = p1.region_code,
                                join2 = p1.prov_code,
                                join3 = p1.city_code,
                                join4 = p1.brgy_code,
                                join5 = p1.first_name.ToLower(),
                                join6 = p1.last_name.ToLower()
                            } equals
                            new
                            {
                                join1 = p2.region_code,
                                join2 = p2.prov_code,
                                join3 = p2.city_code,
                                join4 = p2.brgy_code,
                                join5 = p2.first_name.ToLower(),
                                join6 = p2.last_name.ToLower()
                            }
                         where p2.birthdate != p1.birthdate
                         orderby p1.brgy_code
                         select new
                         {
                             Person_Unique_Id = p1.person_profile_id,
                             Region = p1.lib_region.region_name == null ? null : p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name == null ? null : p1.lib_province.prov_name,
                             Municipality = p1.lib_city.city_name == null ? null : p1.lib_city.city_name,
                             Barangay = p1.lib_brgy.brgy_name == null ? null : p1.lib_brgy.brgy_name,
                             Last_Name = p1.last_name,
                             First_Name = p1.first_name,
                             Sex = p1.sex == null ? null : p1.sex == true ? "Male" : "Female",
                             Birthday = p1.birthdate == null ? "" : p1.birthdate.Value.ToString("dd/MM/yyyy")                             
                         };

            return Ok(result);
        }

        //DQA: Displays records with same last_name, same location, same birthdate, but differs in first_name
        [HttpPost]
        [Route("/api/export/person_profile/dqa/same_lname_different_fname")]
        public IActionResult export_dqa_same_lname_different_fname(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from p1 in model
                         join p2 in model on
                            new
                            {
                                join1 = p1.region_code,
                                join2 = p1.prov_code,
                                join3 = p1.city_code,
                                join4 = p1.brgy_code,
                                join5 = p1.last_name.ToLower(),
                                join6 = p1.birthdate
                            } equals
                            new
                            {
                                join1 = p2.region_code,
                                join2 = p2.prov_code,
                                join3 = p2.city_code,
                                join4 = p2.brgy_code,
                                join5 = p2.last_name.ToLower(),
                                join6 = p2.birthdate
                            }
                         where p2.first_name != p1.first_name
                         orderby new { p1.last_name, p1.first_name }
                         select new
                         {
                             Person_Unique_Id = p1.person_profile_id,
                             Region = p1.lib_region.region_name == null ? null : p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name == null ? null : p1.lib_province.prov_name,
                             Municipality = p1.lib_city.city_name == null ? null : p1.lib_city.city_name,
                             Barangay = p1.lib_brgy.brgy_name == null ? null : p1.lib_brgy.brgy_name,
                             Last_Name = p1.last_name,
                             First_Name = p1.first_name,
                             Sex = p1.sex == null ? null : p1.sex == true ? "Male" : "Female",
                             Birthday = p1.birthdate == null ? "" : p1.birthdate.Value.ToString("dd/MM/yyyy")
                         };

            return Ok(result);
        }

        //DQA: Displays records with same first_name, same location, same birthdate, but differs in last_name
        [HttpPost]
        [Route("/api/export/person_profile/dqa/same_fname_different_lname")]
        public IActionResult export_dqa_same_lname_different_lname(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from p1 in model
                         join p2 in model on
                            new
                            {
                                join1 = p1.region_code,
                                join2 = p1.prov_code,
                                join3 = p1.city_code,
                                join4 = p1.brgy_code,
                                join5 = p1.first_name.ToLower(),
                                join6 = p1.birthdate
                            } equals
                            new
                            {
                                join1 = p2.region_code,
                                join2 = p2.prov_code,
                                join3 = p2.city_code,
                                join4 = p2.brgy_code,
                                join5 = p2.first_name.ToLower(),
                                join6 = p2.birthdate
                            }
                         where p2.last_name != p1.last_name
                         orderby new { p1.last_name, p1.first_name }
                         select new
                         {
                             Person_Unique_Id = p1.person_profile_id,
                             Region = p1.lib_region.region_name == null ? null : p1.lib_region.region_name,
                             Province = p1.lib_province.prov_name == null ? null : p1.lib_province.prov_name,
                             Municipality = p1.lib_city.city_name == null ? null : p1.lib_city.city_name,
                             Barangay = p1.lib_brgy.brgy_name == null ? null : p1.lib_brgy.brgy_name,
                             Last_Name = p1.last_name,
                             First_Name = p1.first_name,
                             Sex = p1.sex == null ? null : p1.sex == true ? "Male" : "Female",
                             Birthday = p1.birthdate == null ? "" : p1.birthdate.Value.ToString("dd/MM/yyyy")
                         };

            return Ok(result);
        }

        //DQA: Person Profile null values -- Display Person Profile encoded that contains null values on the minimum required fields
        [HttpPost]
        [Route("/api/export/person_profile/dqa/null_required_fields")]
        public IActionResult export_dqa_null_required_fields(AngularFilterModel item)
        {
            var model = GetData(item);
            var list = model
                .Select(x => new
                {
                    x.person_profile_id, x.first_name, x.middle_name, x.last_name, x.sex, status = x.lib_civil_status.name, x.birthdate, x.no_children, education = x.lib_education_attainment.name, x.is_ip, x.is_pantawid, x.is_slp,
                    region = x.lib_region.region_name, province = x.lib_province.prov_name, city = x.lib_city.city_name, brgy = x.lib_brgy.brgy_name, x.sitio, x.contact_no,
                    occupation = x.lib_occupation.name, x.is_lguofficial, x.is_mdc, x.is_bdc,
                    x.is_sector_academe, x.is_sector_business, x.is_sector_pwd, x.is_sector_farmer, x.is_sector_fisherfolks, x.is_sector_government, x.is_sector_ip, x.is_sector_ngo, x.is_sector_po, x.is_sector_religios, x.is_sector_senior, x.is_sector_women, x.is_sector_youth,
                    x.other_training, x.other_membership
                })
                .Where(x => 
                    x.first_name == null || x.middle_name == null || x.last_name == null || x.sex == null || x.status == null || x.birthdate == null || x.no_children == null || x.education == null || x.is_ip == null || x.is_pantawid == null || x.is_slp == null ||
                    x.region == null || x.province == null || x.city == null || x.brgy == null || x.sitio == null || x.contact_no == null ||
                    x.occupation == null || x.is_lguofficial == null || x.is_mdc == null || x.is_bdc == null ||
                    x.is_sector_academe == null || x.is_sector_business == null || x.is_sector_pwd == null || x.is_sector_farmer == null || x.is_sector_fisherfolks == null || x.is_sector_government == null || x.is_sector_ip == null || x.is_sector_ngo == null || x.is_sector_po == null || x.is_sector_religios == null || x.is_sector_senior == null || x.is_sector_women == null || x.is_sector_youth == null ||
                    x.other_training == null || x.other_membership == null
                      )
                .ToList();

            var result = list
                .Select(x => new
                {
                    Person_Unique_Id = x.person_profile_id,
                    First_Name = x.first_name == null ? null : x.first_name,
                    Middle_Name = x.middle_name == null ? null : x.middle_name,
                    Last_Name = x.last_name == null ? null : x.last_name,
                    Sex = x.sex == null ? null : x.sex == true ? "Male" : "Female",
                    Civil_Status = x.status == null ? null : x.status,
                    Birthdate = x.birthdate == null ? null : x.birthdate.Value.ToString("dd/MM/yyyy"),
                    No_of_children = x.no_children,
                    Educational_Attainment = x.education,
                    Is_IP = x.is_ip == null ? null : x.is_ip == true ? "Yes" : "No",
                    Is_Pantawid = x.is_pantawid == null ? null : x.is_pantawid == true ? "Yes" : "No",
                    Is_SLP = x.is_slp == null ? null : x.is_slp == true ? "Yes" : "No",
                    Region = x.region == null ? null : x.region,
                    Province = x.province == null ? null : x.province,
                    Municipality = x.city == null ? null : x.city,
                    Barangay = x.brgy == null ? null : x.brgy,
                    Sitio = x.sitio,
                    Contact_no = x.contact_no,  
                    Current_Occupation = x.occupation == null ? null : x.occupation,
                    Is_LGU_Official = x.is_lguofficial == null ? null : x.is_lguofficial == true ? "Yes" : "No",
                    Is_MDC = x.is_mdc == null ? null : x.is_mdc == true ? "Yes" : "No",
                    Is_BDC = x.is_bdc == null ? null : x.is_bdc == true ? "Yes" : "No",
                    Sector_Represented = x.is_sector_academe == null && x.is_sector_business == null && x.is_sector_pwd == null && x.is_sector_farmer == null && x.is_sector_fisherfolks == null && x.is_sector_government == null && x.is_sector_ip == null && x.is_sector_ngo == null && x.is_sector_po == null && x.is_sector_religios == null && x.is_sector_senior == null && x.is_sector_women == null && x.is_sector_youth == null  ? null : "With sector represented",
                    Previous_Trainings_Attended = x.other_training == null ? null : x.other_training,
                    Previous_Organizations = x.other_membership == null ? null : x.other_membership
                })
                .ToList();

            return Ok(result);
        }

        #endregion










        public ProfilesController(ApplicationDbContext context)
        {
            db = context;

        }





        //public IActionResult GetVolunteerSummary(AngularFilterModel model)
        //{
        //    model.is_volunteer = true;

        //    var item = GetData(model);

        //    var result = item.GroupBy(x => new {x.lib_brgy}).Select(
        //                new {
        //                brgy_name = x.Key.brgy_name,
        //                male = x.Count(c => c.is_male != null),
        //                female = x.Count(c => c.is_male == false)


        //                male_ip = x.Count(c => c.is_male != null && x.is_ip != null),
        //                female_ip = x.Count(c => c.is_male == false && x.is_ip != null),

        //                male_pantawid = x.Count(c => c.is_male != null && x.is_pantawid != null),
        //                female_pantawid = x.Count(c => c.is_male == false && x.is_pantawid != null),

        //                male_lgu= x.Count(c => c.is_male != null && x.is_lguofficial != null),
        //                female_lgu= x.Count(c => c.is_male == false && x.is_lguofficial != null),


        //                male_slp = x.Count(c => c.is_male != null && x.is_slp != null),
        //                female_slp = x.Count(c => c.is_male == false && x.is_slp != null),
        //                };

        //     return Ok(result);

        //}



        //        public IActionResult GetVolunteerCommMemSummary(AngularFilterModel model)
        //        {
        //            model.is_volunteer != null;

        //            var item =  GetData(model);


        //            var item2 = from s in item
        //                    join     p in db.person_volunteer_record on s.person_profile_id equals p.person_profile_id
        //                    into AsQueryable
        //                    group by new { p.}

        //            var result = item.GroupBy(x => new {x.person_profile.lib_brgy, x.lib_volunteer_committee)}).Select(
        //                    new {
        //                        brgy_name = x.Key.brgy_name,
        //                        male = x.Count(c => c.is_male != null),
        //                        female = x.Count(c => c.is_male == false)
        //                        };

        //            return Ok(result);

        //        }





        private IQueryable<person_profile> GetData(AngularFilterModel item)

        {
            var model = db.person_profile
                .Include(x => x.lib_approval)
                .Include(x => x.lib_blgu_position)
                .Include(x => x.lib_brgy)
                .Include(x => x.lib_city)
                .Include(x => x.lib_civil_status)
                .Include(x => x.lib_education_attainment)
                .Include(x => x.lib_ip_group)
                .Include(x => x.lib_occupation)
                .Include(x => x.lib_province)
                .Include(x => x.lib_push_status)
                .Include(x => x.lib_region)
                .Where(x => x.is_deleted != true)
                .AsQueryable();

            //////FILTER FIELDS:

            //Recently Edited
            if (item.is_recently_edited != null)
            {
                if (item.is_recently_edited == true)
                {
                    model = model.Where(m => m.push_status_id == 3);
                }
            }

            //Recently Added
            if (item.is_recently_added != null)
            {
                if (item.is_recently_added == true)
                {
                    model = model.Where(m => m.push_status_id == 2);
                }
            }

            //Unauthorized
            if (item.is_unauthorized != null)
            {
                if (item.is_unauthorized == true)
                {
                    model = model.Where(m => m.push_status_id == 4);
                }
            }
            
            //Name
            if (!string.IsNullOrEmpty(item.name))
            {
               string converted_name = item.name.ToLower();
                model = model.Where(x => x.first_name.ToLower().Contains(converted_name) || x.last_name.ToLower().Contains(converted_name));                
                //model = model.Where(x => x.first_name.Contains(item.name) || x.last_name.Contains(item.name));
            }

            //Pantawid?
            if (item.is_pantawid != null)
            {
                if (item.is_pantawid == true)
                {
                    model = model.Where(x => x.is_pantawid == true);
                }
                else
                {
                    model = model.Where(x => x.is_pantawid == false);
                }
            }

            //is SLP?
            if (item.is_slp != null)
            {
                if (item.is_slp == true)
                {
                    model = model.Where(x => x.is_slp == true);
                }
                else
                {
                    model = model.Where(x => x.is_slp == false);
                }
            }

            //Region, Province, Municipality, Brgy
            if (item.region_code != null)
            {
                model = model.Where(m => m.region_code == item.region_code);
            }
            if (item.prov_code != null)
            {
                model = model.Where(m => m.prov_code == item.prov_code);
            }
            if (item.city_code != null)
            {
                model = model.Where(m => m.city_code == item.city_code);
            }
            if (item.brgy_code != null)
            {
                model = model.Where(m => m.brgy_code == item.brgy_code);
            }


            //IP?
            if (item.is_ip != null)
            {
                if (item.is_ip == true)
                {
                    model = model.Where(x => x.is_ip == true);

                    if (item.ip_group_id != null)
                    {
                        model = model.Where(m => m.ip_group_id == item.ip_group_id);
                    }
                }
                if (item.is_ip == false)
                {
                    model = model.Where(x => x.is_ip == false);
                }
            }

            //Trained?
            if (item.is_trained != null)
            {
                if (item.is_trained == true)
                {
                    model = from s in model
                            where (from o in db.person_training.Where(x => x.is_participant == true && x.is_deleted != true)
                                    select o.person_profile_id).Contains(s.person_profile_id)
                            select s;
                    
                    //if is_trained is yes, additional field will be present: Training Category 
                    if (item.training_category_id != null)
                    {
                        model = from s in model
                                where (from o in db.person_training.Where(x => x.community_training.training_category_id == item.training_category_id).Include(x => x.community_training).Where(x => x.community_training_id == item.community_training_id && x.is_participant == true && x.community_training.training_category_id == item.training_category_id)
                                        select o.person_profile_id).Contains(s.person_profile_id)
                                select s;
                    }                    
                }

                if (item.is_trained == false)
                {
                    model = from s in model
                            where !(from o in db.person_training.Where(x => x.is_participant == true && x.is_deleted != true)
                                   select o.person_profile_id).Contains(s.person_profile_id)
                            select s;
                }
            }

            //Volunteer?
            if (item.is_volunteer != null)
            {
                if (item.is_volunteer == true)
                {
                    model = from s in model
                            where (from o in db.person_volunteer_record.Where( x => x.is_deleted != true)
                                    select o.person_profile_id).Contains(s.person_profile_id)
                            select s;

                    //if volunteer is yes, ask for project
                    if (item.fund_source_id != null)
                    {
                        model = from s in model
                                where (from o in db.person_volunteer_record.Where(x => x.fund_source_id == item.fund_source_id && x.is_deleted != true)
                                        select o.person_profile_id).Contains(s.person_profile_id)
                                select s;
                    }

                    //if volunteer is yes, ask for cycle
                    if (item.cycle_id != null)
                    {
                        model = from s in model
                                where (from o in db.person_volunteer_record.Where(x => x.cycle_id == item.cycle_id && x.is_deleted != true)
                                        select o.person_profile_id).Contains(s.person_profile_id)
                                select s;
                    }

                    //if volunteer, ask for mode
                    if (item.enrollment_id != null)
                    {
                        model = from s in model
                                where (from o in db.person_volunteer_record.Where(x => x.enrollment_id == item.enrollment_id && x.is_deleted != true)
                                        select o.person_profile_id).Contains(s.person_profile_id)
                                select s;
                    }

                    //Member volunteer
                    if (item.volunteer_committee_id != null && item.volunteer_committee_position_id == 1)
                    {
                        model = from s in model
                                where (from o in db.person_volunteer_record.Where(x => x.volunteer_committee_id == item.volunteer_committee_id && x.volunteer_committee_position_id == 1 && x.is_deleted != true)
                                        select o.person_profile_id).Contains(s.person_profile_id)
                                select s;

                    }

                    //Chair member
                    if (item.volunteer_committee_id != null && item.volunteer_committee_position_id == 2)
                    {
                        model = from s in model
                                where (from o in db.person_volunteer_record.Where(x => x.volunteer_committee_id == item.volunteer_committee_id && x.volunteer_committee_position_id == 2 && x.is_deleted != true)
                                       select o.person_profile_id).Contains(s.person_profile_id)
                                select s;

                    }

                    //Volunteer regardless of position
                    if (item.volunteer_committee_id != null && item.volunteer_committee_position_id == null)
                    {
                        model = from s in model
                                where (from o in db.person_volunteer_record.Where(x => x.volunteer_committee_id == item.volunteer_committee_id && x.is_deleted != true)
                                       select o.person_profile_id).Contains(s.person_profile_id)
                                select s;

                    }

                    //Position (member/chair) regardless of committee
                    if (item.volunteer_committee_position_id != null && item.volunteer_committee_id == null)
                    {
                        model = from s in model
                                where (from o in db.person_volunteer_record.Where(x => x.volunteer_committee_position_id == item.volunteer_committee_position_id && x.is_deleted != true)
                                        select o.person_profile_id).Contains(s.person_profile_id)
                                select s;
                    }
                }

                if (item.is_volunteer == false)
                {
                    model = from s in model
                            where !(from o in db.person_volunteer_record.Where(x => x.is_deleted != true)
                                        select o.person_profile_id).Contains(s.person_profile_id)
                            select s;
                }
            }

            //Worker?
            if (item.is_worker != null)
            {
                if (item.is_worker == true)
                {
                    model = from s in model
                            where (from o in db.person_ers_work.Where(x => x.is_deleted != true)
                                    select o.person_profile_id).Contains(s.person_profile_id)
                            select s;
                }
                if (item.is_worker == false)
                {
                    model = from s in model
                            where !(from o in db.person_ers_work.Where(x => x.is_deleted != true)
                                   select o.person_profile_id).Contains(s.person_profile_id)
                            select s;
                }
            }

            //Sex
            if (item.is_male != null)
            {
                if (item.is_male == true)
                {
                    model = model.Where(x => x.sex == true);
                }
                if (item.is_male == false)
                {
                    model = model.Where(x => x.sex == false);
                }
            }

            //Educational Attainment
            if (item.education_attainment_id != null)
            {
                model = model.Where(m => m.education_attainment_id == item.education_attainment_id);
            }

            //Occupation
            if (item.occupation_id != null)
            {
                model = model.Where(m => m.occupation_id == item.occupation_id);
            }

            
            return model;
        }


        //v3.2 fix for Person not tagged as is_worker = true and is_volunteer = true though existing in person_ers_work and person_volunteer_record
        [Route("api/offline/v1/profiles/update_person_status")]
        public async Task<IActionResult> UpdatePersonStatus()
        {
            var model = db.person_profile.Where(p => p.is_deleted != true);

            foreach (var p in model) {
                var active_worker = db.person_ers_work.Where(x => x.person_profile_id == p.person_profile_id && p.is_deleted != true).Count();
                var deleted_worker = db.person_ers_work.Where(x => x.person_profile_id == p.person_profile_id && p.is_deleted == true).Count();
                var total_count = db.person_ers_work.Where(x => x.person_profile_id == p.person_profile_id).Count();

                if (total_count == 0 && p.is_worker == true)
                {
                    p.is_worker = null;
                    p.push_status_id = 3;
                }
                else if (total_count == 0 && p.is_worker != true)
                {
                    //do nothing
                }
                else if (active_worker >= 1)
                {
                    p.is_worker = true;
                    p.push_status_id = 3;
                }
                else if ((deleted_worker == total_count) == (deleted_worker > 0 && total_count > 0))
                {
                    p.is_worker = null;
                    p.push_status_id = 3;
                }
                else if ((deleted_worker == total_count) == (deleted_worker == 0 && total_count == 0)) {
                    //do nothing
                }

                var active_volunteer = db.person_volunteer_record.Where(x => x.person_profile_id == p.person_profile_id && p.is_deleted != true).Count();
                var deleted_volunteer = db.person_volunteer_record.Where(x => x.person_profile_id == p.person_profile_id && p.is_deleted == true).Count();
                var total_count_vol = db.person_volunteer_record.Where(x => x.person_profile_id == p.person_profile_id).Count();

                if (total_count_vol == 0 && p.is_volunteer == true)
                {
                    p.is_volunteer = null;
                    p.push_status_id = 3;
                }
                else if (total_count_vol == 0 && p.is_volunteer != true)
                {
                    //do nothing
                }
                else if (active_volunteer >= 1)
                {
                    p.is_volunteer = true;
                    p.push_status_id = 3;
                }
                else if ((deleted_volunteer == total_count_vol) == (deleted_volunteer > 0 && total_count_vol > 0))
                {
                    p.is_volunteer = null;
                    p.push_status_id = 3;
                }
                else if ((deleted_volunteer == total_count_vol) == (deleted_volunteer == 0 && total_count_vol == 0))
                {
                    //do nothing
                }
            }

            await db.SaveChangesAsync();
            return Ok();
        }



        [HttpGet]
        [Route("api/dashboard/person/volunteers")]
        public IActionResult dash_volunteer()
        {

            var item = new AngularFilterModel();

            var model = GetData(item);

            var result = (from s in model
                          join p in db.person_volunteer_record.Where(x => x.is_deleted != true) on s.person_profile_id equals p.person_profile_id
                          select s).GroupBy(x => x.lib_brgy).Select(x => new
                          {
                              Name = x.Key.brgy_name,
                              Count = x.Count()
                          });

            return Ok(result);


        }

        //[HttpPost]
        //[Route("api/offline/v1/get/potential_ers_workers")]
        //public PagedCollection<person_profileDTO> GetPotentialERSWorkers(AngularFilterModel item)
        //{
        //    var pp = db.person_profile.Where(x => x.is_deleted != true);
        //    var totalCount = pp.Count();
        //    int currPages = item.currPage ?? 0;
        //    int size = item.pageSize ?? 10;

        //    return new PagedCollection<person_profileDTO>()
        //    {
        //        Page = currPages,
        //        TotalCount = totalCount,
        //        TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
        //        Items = pp.OrderBy(x => x.last_name)
        //        .Select(
        //            x => new person_profileDTO
        //            {
        //                person_profile_id = x.person_profile_id,
        //                first_name = x.first_name,
        //                middle_name = x.middle_name,
        //                last_name = x.last_name,
        //                sex = x.sex.Value,
        //                birthdate = x.birthdate,
        //                lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
        //                lib_city_city_name = x.lib_city.city_name,
        //                lib_province_prov_name = x.lib_province.prov_name,
        //                lib_region_region_name = x.lib_region.region_name

        //            }).Skip(currPages * size).Take(size).ToList(),
        //    };
        //}

        [HttpPost]
        [Route("api/offline/v1/get/potential_ers_workers")]
        public PagedCollection<dynamic> GetPotentialERSWorkers(AngularFilterModel item)
        {
            var pp = GetData(item);
            var totalCount = pp.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = pp.OrderBy(x => x.last_name)
                .Select(
                    x => new 
                    {
                        person_profile_id = x.person_profile_id,
                        first_name = x.first_name,
                        middle_name = x.middle_name,
                        last_name = x.last_name,
                        sex = x.sex.Value,
                        birthdate = x.birthdate,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name

                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }







        [HttpPost]
        [Route("api/offline/v1/profiles/get_dto")]
        public PagedCollection<person_profileDTO> GetDTO(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;
            int person_training_count = db.person_training.Count(x => x.is_participant == true && x.community_training_id == item.community_training_id && x.is_deleted != true);

            return new PagedCollection<person_profileDTO>()
            {
                //TotalSync = model.Count(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted != true)),
                TotalSync = model.Count(x => x.is_deleted !=true && (x.push_status_id == 2 || x.push_status_id == 3)),
                TotalUnAuthorized = model.Count(x => x.push_status_id == 4 && x.is_deleted != true),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalCountParticipants = person_training_count,
                Items = model.OrderBy(x => x.created_date)
                .Select(
                    x => new person_profileDTO
                    {
                        person_profile_id = x.person_profile_id,
                        first_name = x.first_name,
                        middle_name = x.middle_name,
                        last_name = x.last_name,
                        sex = x.sex.Value,
                        birthdate = x.birthdate,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                        last_modified_date = x.last_modified_date

                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/profiles/get_recently_edited")]
        public PagedCollection<person_profileDTO> GetRecentlyEdited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<person_profileDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Count(x => x.push_status_id == 4 && x.is_deleted != true),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                    .OrderBy(x => x.first_name)
                    .Select(
                        x => new person_profileDTO
                        {
                            person_profile_id = x.person_profile_id,
                            first_name = x.first_name,
                            middle_name = x.middle_name,
                            last_name = x.last_name,
                            sex = x.sex.Value,
                            birthdate = x.birthdate,
                            lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                            lib_city_city_name = x.lib_city.city_name,
                            lib_province_prov_name = x.lib_province.prov_name,
                            lib_region_region_name = x.lib_region.region_name,
                            push_date = x.push_date,
                            push_status_id = x.push_status_id,

                        }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/profiles/get_recently_added")]
        public PagedCollection<person_profileDTO> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<person_profileDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Count(x => x.push_status_id == 4 && x.is_deleted != true),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                    .OrderBy(x => x.first_name)
                    .Select(
                        x => new person_profileDTO
                        {
                            person_profile_id = x.person_profile_id,
                            first_name = x.first_name,
                            middle_name = x.middle_name,
                            last_name = x.last_name,
                            sex = x.sex.Value,
                            birthdate = x.birthdate,
                            lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                            lib_city_city_name = x.lib_city.city_name,
                            lib_province_prov_name = x.lib_province.prov_name,
                            lib_region_region_name = x.lib_region.region_name,
                            push_date = x.push_date,
                            push_status_id = x.push_status_id,

                        }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/profiles/get_recently_edited_and_added")]
        public PagedCollection<person_profileDTO> GetRecentlyEditedandAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<person_profileDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Count(x => x.push_status_id == 4 && x.is_deleted != true),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = model
                    .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                    .OrderBy(x => x.first_name)
                    .Select(
                        x => new person_profileDTO
                        {
                            person_profile_id = x.person_profile_id,
                            first_name = x.first_name,
                            middle_name = x.middle_name,
                            last_name = x.last_name,
                            sex = x.sex.Value,
                            birthdate = x.birthdate,
                            lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                            lib_city_city_name = x.lib_city.city_name,
                            lib_province_prov_name = x.lib_province.prov_name,
                            lib_region_region_name = x.lib_region.region_name,
                            push_date = x.push_date,
                            push_status_id = x.push_status_id,

                        }).Skip(currPages * size).Take(size).ToList(),
            };
        }



        public bool record_exists(Guid id)
        {
            return db.person_profile.Count(e => e.person_profile_id == id) > 0;
        }



        [Route("api/offline/v1/profiles/save")]
        public async Task<IActionResult> Save(person_profile model, bool? api)
        {
            var record = db.person_profile.AsNoTracking().FirstOrDefault(x => x.person_profile_id == model.person_profile_id);

            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                
                if (api == true)
                {
                    model.push_status_id = 1;
                }

                db.person_profile.Add(model);
                
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

            else
            {
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;                

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

        [Route("api/offline/v1/profiles/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.person_profile

                .FirstOrDefault(x => x.person_profile_id == id);// && (x.is_deleted.HasValue && x.is_deleted == true));



            if (model == null)
            {
                var item = new person_profile();

                item.person_profile_id = id;
                item.push_status_id = 3;
                item.created_by = 0;


                item.created_date = DateTime.Now;
                item.approval_id = 3;
                item.is_deleted = false;


                model = item;
            }
            else
            {

                model.push_status_id = 3;
                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;
                model.approval_id = 3;


            }

            return Ok(model);
        }



        #region API GET / POST

        [HttpPost]
        [Route("Sync/Get/profiles")]
        public async Task<ActionResult> GetOnline(string username, string password, string city_code = null, Guid? record_id = null)
        {
            string token = username + ":" + password;
            byte[] toBytes = Encoding.ASCII.GetBytes(token);
            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/profiles/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<person_profile>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        await Save(item, true);
                    }

                    await GetVolunteer(username, password, city_code, record_id);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [HttpPost]
        [Route("Sync/Get/volunteer")]
        public async Task<bool> GetVolunteer(string username, string password, string city_code = null, Guid? record_id = null)
        {
            string token = username + ":" + password;
            byte[] toBytes = Encoding.ASCII.GetBytes(token);
            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                HttpResponseMessage response = client.GetAsync("api/online/v1/person/volunteer_record/get?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<person_volunteer_record>>(responseJson.Result);

                    foreach (var v in model.ToList())
                    {
                        await SavePersonVolunteer(v, true);
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }


        [Route("api/offline/v1/person_profile/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid person_profile_id, int push_status_id)
        {
            var person = db.person_profile.Find(person_profile_id);

            if (person == null)
            {
                return BadRequest("Error");
            }

            person.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Sync/Post/profiles")]
        public async Task<ActionResult> PostOnline(string username, string password, Guid? record_id = null)
        {
            string token = username + ":" + password;
            byte[] toBytes = Encoding.ASCII.GetBytes(token);
            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                var items_preselected = db.person_profile.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.person_profile.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || x.is_deleted == true);
                   
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/profiles/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            //record_id = item.person_profile_id;
                            //PostOnlineVolunteer(username, password, record_id); // put this back outside v4.3
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            await db.SaveChangesAsync();
                        }
                    }
                }
                else {
                    var items = db.person_profile.Where(x => x.push_status_id == 5 || x.is_deleted == true);
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/profiles/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            //record_id = item.person_profile_id;
                            //PostOnlineVolunteer(username, password, record_id); // put this back outside v4.3
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            await db.SaveChangesAsync();
                            //return BadRequest();
                        }
                    }
                }  
            }
            await PostOnlineVolunteer(username, password, record_id);
            return Ok();
        }


        public async Task<bool> PostOnlineVolunteer(string username, string password, Guid? record_id = null)
        {
            string token = username + ":" + password;
            byte[] toBytes = Encoding.ASCII.GetBytes(token);
            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                //var items = db.person_volunteer_record.Where(x => x.person_profile_id == record_id || x.is_deleted == true);
                var items = db.person_volunteer_record.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1)); //v4.3

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/person/volunteer_record/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.push_status_id = 4;
                        await db.SaveChangesAsync();
                        return false;
                    }
                }
            }
            return true;
        }



        //[HttpPost]
        //[Route("Sync/Post/profiles")]
        //public async Task<ActionResult> PostOnline(string username, string password, Guid? record_id = null)
        //{
        //    string token = username + ":" + password;
        //    byte[] toBytes = Encoding.ASCII.GetBytes(token);
        //    string key = Convert.ToBase64String(toBytes);

        //    using (var client = new HttpClient())
        //    {
        //        //setup client
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

        //        // var model = new auth_messages();
        //        var items =
        //            db.person_profile.Where(
        //                x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted != true));

        //        if (record_id != null)
        //        {
        //            items = items.Where(x => x.person_profile_id == record_id);
        //        }

        //        foreach (var item in items.ToList())
        //        {
        //            StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8,
        //                "application/json");
        //            HttpResponseMessage response = client.PostAsync("api/offline/v1/profiles/save", data).Result;

        //            // response.EnsureSuccessStatusCode();

        //            if (response.IsSuccessStatusCode)
        //            {
        //                item.push_status_id = 1;
        //                item.push_date = DateTime.Now;
        //                await db.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                item.push_status_id = 4;
        //                item.push_date = DateTime.Now;
        //                await db.SaveChangesAsync();
        //            }
        //        }

        //    }
        //    await PostOnlineVolunteer(username, password, record_id);
        //    return Ok();
        //}

        //public async Task<bool> PostOnlineVolunteer(string username, string password, Guid? record_id = null)
        //{
        //    string token = username + ":" + password;
        //    byte[] toBytes = Encoding.ASCII.GetBytes(token);
        //    string key = Convert.ToBase64String(toBytes);

        //    using (var client = new HttpClient())
        //    {
        //        //setup client
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

        //        // var model = new auth_messages();

        //        var items =
        //            db.person_volunteer_record.Where(
        //                x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted != true));

        //        if (record_id != null)
        //        {
        //            items = items.Where(x => x.person_profile_id == record_id);
        //        }

        //        foreach (var item in items.ToList())
        //        {
        //            StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8,
        //                "application/json");
        //            HttpResponseMessage response =
        //                client.PostAsync("api/offline/v1/person/volunteer_record/save", data).Result;

        //            // response.EnsureSuccessStatusCode();

        //            if (response.IsSuccessStatusCode)
        //            {
        //                item.push_status_id = 1;
        //                item.push_date = DateTime.Now;
        //                await db.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                item.push_status_id = 4;
        //                item.push_date = DateTime.Now;
        //                await db.SaveChangesAsync();
        //            }
        //        }
        //    }
        //    return true;
        //}
        #endregion



        #region volunteer data

        [HttpGet]

        [Route("api/offline/v1/profiles/volunteer_record/get")]
        public IActionResult GetPersonVolunteerRecord(Guid? id)
        {


            var model = from s in db.person_volunteer_record.Where(x => x.person_profile_id == id)

                        where s.is_deleted != true
                        select s;


            return Ok(model.Select(person_volunteer_recordDTO.SELECT));
        }

        [Route("api/offline/v1/profiles/volunteer_record/save")]
        public async Task<ActionResult> SavePersonVolunteer(person_volunteer_record model, bool? api)
        {
            var record = db.person_volunteer_record.AsNoTracking().FirstOrDefault(x => x.person_volunteer_record_id == model.person_volunteer_record_id && x.is_deleted != true);
            var main_record = db.person_profile.FirstOrDefault(x => x.person_profile_id == model.person_profile_id);

            if (record == null)
            {
                if (api != true)
                {
                    if (db.person_volunteer_record.Count(x => x.person_profile_id == model.person_profile_id &&
                                                                  x.cycle_id == model.cycle_id &&
                                                                  x.volunteer_committee_id == model.volunteer_committee_id &&
                                                                  x.is_deleted != true) > 0)
                    {
                        return BadRequest("Volunteer cannot be a CHAIR or MEMBER on the same committee and cycle.");
                    }

                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;

                    //v3.1
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;
                    main_record.is_volunteer = true; //if person has volunteer record/s, set is_volunteer to true
                    db.Entry(main_record).State = EntityState.Modified;
                }
                else
                {
                    model.push_status_id = 1;                  
                }                            

                db.person_volunteer_record.Add(model);
                                
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

            else
            {
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    //v3.1 any changes on volunteer record, main person profile should be updated as well
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;
                    main_record.is_volunteer = true; //if person has volunteer record/s, set is_volunteer to true
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;

                if (main_record != null) {
                    main_record.is_volunteer = true; //if person has volunteer record/s, set is_volunteer to true
                } 

                db.Entry(model).State = EntityState.Modified;
                db.Entry(main_record).State = EntityState.Modified;

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

        public bool record_exists_volunteer(Guid id)
        {
            return db.person_volunteer_record.Count(e => e.person_volunteer_record_id == id) > 0;
        }

        #endregion



        #region trainings

        [Route("api/offline/v1/profiles/trainings/get")]
        public IActionResult GetPersonTrainings(Guid? id)
        {


            var model = from s in db.person_training.Where(x => x.person_profile_id == id && x.is_deleted != true)
                        join p in db.community_training on s.community_training_id equals p.community_training_id
                        where s.is_participant == true && p.is_deleted != true
                        select p;


            return Ok(model.ToList());
        }

        #endregion

        #region export

        //[Route("export/profiles/list")]
        //public IActionResult export_list_profiles(AngularFilterModel item)
        //{
        //    var model = GetData(item);

        //    var result = from s in model

        //        select new
        //        {
        //            //     s.person_profile_id,
        //            s.lib_region.region_name,
        //            s.lib_province.prov_name,
        //            s.lib_city.city_name,
        //            s.lib_brgy.brgy_name,

        //            s.first_name,
        //            s.middle_name,
        //            s.last_name,

        //            sex = s.sex != null ? "Male" : "Female",
        //            s.birthdate,
        //            age = s.birthdate == null ? "No Birthdate" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),

        //        //     marital_status = s.lib_civil_status.name,

        //           //   occupation =   s.lib_occupation.name,
        //            s.no_children,
        //            s.sector,

        //         //   educational_attainment =   s.lib_education_attainment.name,

        //            is_volunteer = s.is_volunteer != null ? "YES" : "",
        //            is_worker = s.is_worker != null ? "YES" : "",
        //            is_ip = s.is_ip != null ? "YES" : "",

        //            is_ip_leader = s.is_ip != null ? "YES" : "",
        //            is_pantawid = s.is_pantawid != null ? "YES" : "",
        //            is_slp = s.is_slp != null ? "YES" : "",

        //            is_bspms = s.is_bspmc != null ? "YES" : "",
        //            is_bdc = s.is_bdc != null ? "YES" : "",
        //            is_mdb = s.is_mdc != null ? "YES" : "",

        //            has_attended_in_BAR =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 1) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_BPSA =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 2) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_CMVP =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 3) > 0
        //                    ? "YES"
        //                    : "",

        //            has_attended_in_CSW =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 4) > 0
        //                    ? "YES"
        //                    : "",

        //            has_attended_in_Finance =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 5) > 0
        //                    ? "YES"
        //                    : "",

        //            has_attended_in_GAD =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 6) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_INFRA =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 7) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_LGU_training =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 8) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_MF =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 9) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_MIAC =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 11) > 0
        //                    ? "YES"
        //                    : "",

        //            has_attended_in_MIBFPRA =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 11) > 0
        //                    ? "YES"
        //                    : "",

        //            has_attended_in_MO =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 12) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_MPSA =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 13) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_MunAR =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 14) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_OM =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 15) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_ODM =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 16) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_Others =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 17) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_PCM =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 18) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_PPDW =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 19) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_Proc =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 20) > 0
        //                    ? "YES"
        //                    : "",
        //            has_attended_in_SpecialMIBF =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 21) > 0
        //                    ? "YES"
        //                    : "",

        //            has_attended_in_SPW =
        //                db.person_training.Count(
        //                    x =>
        //                        x.person_profile_id == s.person_profile_id &&
        //                        x.community_training.training_category_id == 22) > 0
        //                    ? "YES"
        //                    : "",


        //        };

        //    return new ExcelFileResult(result);
        //}

        #endregion


        //------------------------------------------------------------------ ORIGINAL CODE FOR SAVING ERS WORKER --------------------------------------------//  
        //[Route("api/offline/v1/sub_projects/ers/worker/save")] 
        //public async Task<IActionResult> SaveERSWorker(person_ers_work model, bool? api)
        //{
        //    var record = db.person_ers_work.AsNoTracking().FirstOrDefault(x => x.person_profile_id == model.person_profile_id && x.sub_project_ers_id == model.sub_project_ers_id && x.is_deleted != true);

        //    if (record == null)
        //    {
        //        if (api != true)
        //        {
        //            //   model.push_status_id = 2;
        //            //  model.push_date = null;
        //            model.approval_id = 3;
        //        }
        //        model.created_by = 0;
        //        model.created_date = DateTime.Now;
        //        model.is_deleted = false;
        //        db.person_ers_work.Add(model);

        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            if (api == true)
        //            {
        //                return Ok();
        //            }

        //            var person =
        //                db.person_ers_work
        //                .Include(x => x.lib_ers_current_work)
        //                .Include(x => x.person_profile)
        //                .Where(x => x.person_ers_work_id == model.person_ers_work_id).Select(s => new
        //                {
        //                    s.person_profile.first_name,
        //                    s.person_profile.last_name,
        //                    s.person_profile.person_profile_id,
        //                    s.ers_current_work_id,
        //                    ers_current_work_name = s.lib_ers_current_work.name,
        //                    s.rate_day,
        //                    s.rate_hour,
        //                    s.actual_cash,
        //                    s.actual_lcc,
        //                    s.plan_cash,
        //                    s.plan_lcc,
        //                    s.work_days,
        //                    s.work_hours,
        //                    s.work_hauling,
        //                    s.rate_hauling,
        //                    s.unit_hauling,
        //                    s.percent,
        //                    s.sub_project_ers_id,
        //                    s.person_ers_work_id,
        //                    s.person_profile.sex,
        //                    s.person_profile.contact_no,
        //                });
        //            return Ok(person.FirstOrDefault());
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }

        //    else
        //    {
        //        // model.push_date = null;
        //        if (api != true)
        //        {
        //            //   model.push_status_id = 3;
        //            model.approval_id = 3;
        //        }
        //        model.person_ers_work_id = record.person_ers_work_id;
        //        model.created_by = record.created_by;
        //        model.created_date = record.created_date;
        //        model.last_modified_by = 0;
        //        model.last_modified_date = DateTime.Now;
        //        db.Entry(model).State = EntityState.Modified;

        //        try
        //        {
        //            await db.SaveChangesAsync();

        //            if (api == true)
        //            {
        //                return Ok();
        //            }

        //            var person =
        //              db.person_ers_work
        //              .Include(x => x.lib_ers_current_work)
        //            .Include(x => x.person_profile)
        //              .Where(x => x.person_ers_work_id == model.person_ers_work_id).Select(s => new
        //              {
        //                  s.person_profile.first_name,
        //                  s.person_profile.last_name,
        //                  s.person_profile.person_profile_id,
        //                  s.ers_current_work_id,
        //                  ers_current_work_name = s.lib_ers_current_work.name,
        //                  s.rate_day,
        //                  s.rate_hour,
        //                  s.actual_cash,
        //                  s.actual_lcc,
        //                  s.plan_cash,
        //                  s.plan_lcc,
        //                  s.work_days,
        //                  s.work_hours,
        //                  s.work_hauling,
        //                  s.rate_hauling,
        //                  s.unit_hauling,
        //                  s.percent,
        //                  s.sub_project_ers_id,
        //                  s.person_ers_work_id,
        //                  s.person_profile.sex,
        //                  s.person_profile.contact_no,
        //              });

        //            return Ok(person);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}
        //------------------------------------------------------------------- END ORIGINAL CODE ------------------------------------------------------------//






    }
}


