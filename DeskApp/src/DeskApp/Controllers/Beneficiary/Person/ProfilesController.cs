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
        public static string url = @"http://ncddpdb.dswd.gov.ph";


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
                brgy_name = x.Key.brgy_name,
                x.Key.city_name,
                x.Key.prov_name,
                x.Key.region_name,
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
                x.person_profile.person_profile_id,
                x.person_profile.lib_region.region_name,
                x.person_profile.lib_province.prov_name,
                x.person_profile.lib_city.city_name,
                brgy_name = x.person_profile.lib_brgy.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(cx => cx.brgy_code == x.person_profile.brgy_code).brgy_name,
                   
            }).
            Select(x => new
            {
               brgy_name = x.Key.brgy_name,
               x.Key.city_name,
               x.Key.prov_name,
               x.Key.region_name,

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

                             s.first_name,
                             s.middle_name,
                             s.last_name,

                             sex = s.sex == true ? "Male" : "Female",
                             s.birthdate,

                             age = s.birthdate == null ? "" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),

                             marital_status = s.lib_civil_status.name,

                             occupation = s.lib_occupation.name,
                             s.no_children,

                             educational_attainment = s.lib_education_attainment.name,

                             is_volunteer = db.person_volunteer_record.Count(x => x.person_profile_id == s.person_profile_id && x.is_deleted != true) > 0 ? "Yes" : "",
                             is_worker = db.person_ers_work.Count(x => x.person_profile_id == s.person_profile_id && x.is_deleted != true) > 0 ? "Yes" : "",
                             is_ip = s.is_ip != null ? "YES" : "",

                             is_ip_leader = s.is_ip != null ? "YES" : "",
                             is_pantawid = s.is_pantawid != null ? "YES" : "",
                             is_slp = s.is_slp != null ? "YES" : "",


                             has_attended_in_BAR = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 1) > 0 ? "YES" : "",
                             has_attended_in_BPSA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 2) > 0 ? "YES" : "",
                             has_attended_in_CMVP = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 3) > 0 ? "YES" : "",

                             has_attended_in_CSW = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 4) > 0 ? "YES" : "",

                             has_attended_in_Finance = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 5) > 0 ? "YES" : "",

                             has_attended_in_GAD = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 6) > 0 ? "YES" : "",
                             has_attended_in_INFRA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 7) > 0 ? "YES" : "",
                             has_attended_in_LGU_training = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 8) > 0 ? "YES" : "",
                             has_attended_in_MF = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 9) > 0 ? "YES" : "",
                             has_attended_in_MIAC = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 11) > 0 ? "YES" : "",

                             has_attended_in_MIBFPRA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 11) > 0 ? "YES" : "",

                             has_attended_in_MO = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 12) > 0 ? "YES" : "",
                             has_attended_in_MPSA = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 13) > 0 ? "YES" : "",
                             has_attended_in_MunAR = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 14) > 0 ? "YES" : "",
                             has_attended_in_OM = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 15) > 0 ? "YES" : "",
                             has_attended_in_ODM = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 16) > 0 ? "YES" : "",
                             has_attended_in_Others = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 17) > 0 ? "YES" : "",
                             has_attended_in_PCM = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 18) > 0 ? "YES" : "",
                             has_attended_in_PPDW = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 19) > 0 ? "YES" : "",
                             has_attended_in_Proc = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 20) > 0 ? "YES" : "",
                             has_attended_in_SpecialMIBF = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 21) > 0 ? "YES" : "",

                             has_attended_in_SPW = db.person_training.Count(x => x.person_profile_id == s.person_profile_id && x.community_training.training_category_id == 22) > 0 ? "YES" : "",


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

                             s.first_name,
                             s.middle_name,
                             s.last_name,

                             sex = s.sex == true ? "Male" : "Female",
                             s.birthdate,

                             age = s.birthdate == null ? "" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),

                             committee = p.lib_volunteer_committee.name,
                             position = p .lib_volunteer_committee_position.name,

                             brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,

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

                             s.first_name,
                             s.middle_name,
                             s.last_name,
                             sex = s.sex == true ? "Male" : "Female",
                             s.birthdate,
                             age = s.birthdate == null ? "" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),
                             committee = p.lib_volunteer_committee.name,
                             position = p.lib_volunteer_committee_position.name,
                             brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,
                             cycle_id = p.lib_cycle.cycle_id,
                             fund_source_id = p.fund_source_id
                         };

            var items = result
                .Select(x => new
                {
                    cycle_id = x.cycle_id,
                    fund_source_id = x.fund_source_id,
                    first_name = x.first_name,
                    middle_name = x.middle_name,
                    last_name = x.last_name,
                    sex = x.sex,
                    age = x.age,
                    committee = x.committee,
                    position = x.position,
                    brgy_name = x.brgy_name
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
                x.person_profile.lib_region,
                x.person_profile.lib_province,
                x.person_profile.lib_city,
                x.person_profile.lib_brgy,
                x.lib_cycle,
                x.lib_fund_source,
                x.lib_enrollment,

            }).Select(x => new
            {

                fund_source_name = x.Key.lib_fund_source.name,
                cycle_name = x.Key.lib_cycle.name,
                kc_mode = x.Key.lib_enrollment.name,
                region_name = x.Key.lib_region.region_name,

                prov_name = x.Key.lib_province.prov_name,

                city_name = x.Key.lib_city.city_name,
                brgy_name = x.Key.lib_brgy.brgy_name,


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



        [HttpPost]
        [Route("/api/export/person_profile/volunteer/dqa/list")]
        public IActionResult export_dqa_volunteer(AngularFilterModel item)
        {

            item.is_volunteer = true;

            var model = GetData(item);

            //var result = from s in model
            //             join p in db.person_volunteer_record
            //             on s.person_profile_id equals p.person_profile_id
            //             select p;



            //var output = result.GroupBy(x => x.person_profile_id)

            var export = from s in model


                         select new
                         {
                             s.person_profile_id,

                             s.first_name,
                             s.middle_name,
                             s.last_name,

                             //  no_birthday = s.birthdate == null ? "No Birthday" : "",

                             age = s.birthdate == null ? "" : (DateTime.Now.Year - s.birthdate.Value.Year).ToString(),

                             age_remarks = s.birthdate == null ? "No Birthday" : (DateTime.Now.Year - s.birthdate.Value.Year) < 15 ? "Less Than 15 Years Old" : (DateTime.Now.Year - s.birthdate.Value.Year) > 100 ? "More than 100 Years Old" : "",

                             zero_committee = db.person_volunteer_record.Where(x => x.person_profile_id == s.person_profile_id && x.is_deleted != true).Count() > 0 ? "No Committee" : "",
                             more_than_one_head = db.person_volunteer_record.Where(x => x.person_profile_id == s.person_profile_id && x.volunteer_committee_position_id == 2 && x.is_deleted != true).Count() > 1 ? "More than 1" : "",
                             no_of_committes_as_head = db.person_volunteer_record.Where(x => x.person_profile_id == s.person_profile_id && x.volunteer_committee_position_id == 2 && x.is_deleted != true).Count(),
                             no_of_committes_as_member = db.person_volunteer_record.Where(x => x.person_profile_id == s.person_profile_id && x.volunteer_committee_position_id == 1 && x.is_deleted != true).Count()
                         };



            return Ok(export);
        }







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

            //for single sync
            if (!string.IsNullOrEmpty(item.name))
            {
                model = model.Where(x => x.first_name.Contains(item.name) || x.last_name.Contains(item.name)
           //     || (x.first_name + " " + x.last_name).Contains(item.name)
                );

            }
            if (item.record_id != null)
            {
                model = model.Where(x => x.person_profile_id == item.record_id);
            }
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
            if (item.civil_status_id != null)
            {
                model = model.Where(m => m.civil_status_id == item.civil_status_id);
            }
            if (item.lgu_position_id != null)
            {
                model = model.Where(m => m.lgu_position_id == item.lgu_position_id);
            }
            if (item.education_attainment_id != null)
            {
                model = model.Where(m => m.education_attainment_id == item.education_attainment_id);
            }
            if (item.occupation_id != null)
            {
                model = model.Where(m => m.occupation_id == item.occupation_id);
            }
            if (item.push_status_id != null)
            {
                model = model.Where(m => m.push_status_id == item.push_status_id);
            }
            if (item.approval_id != null)
            {
                model = model.Where(m => m.approval_id == item.approval_id);
            }

            //try:
            if (item.fund_source_id != null)
            {
                if (item.cycle_id != null)
                {
                    model = from s in model
                            where
                                (from o in
                                    db.person_volunteer_record.Where(x => x.fund_source_id == item.fund_source_id && x.cycle_id == item.cycle_id && x.is_deleted != true)
                                 select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                            select s;
                }
            }            
            //end try



            if (item.is_ip != null)
            {
                if (item.is_male == true)
                {
                    model = model.Where(x => x.is_ip == true);
                }
                else
                {
                    model = model.Where(x => x.is_ip != true);
                }
            }


            if (item.ip_group_id != null)
            {
                model = model.Where(m => m.ip_group_id == item.ip_group_id);
            }

            if (item.is_volunteer != null)
            {
                if (item.is_volunteer == true)
                {
                    //  model = model.Where(x => x.is_volunteer != null);

                    model = from s in model
                            where
                                (from o in
                                    db.person_volunteer_record.Where(
                                        x =>

                                            x.is_deleted != true)
                                 select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                            select s;



                    if (item.volunteer_committee_id != null)
                    {
                        model = from s in model
                                where
                                    (from o in
                                        db.person_volunteer_record.Where(
                                            x =>
                                                x.volunteer_committee_id == item.volunteer_committee_id &&
                                                x.is_deleted != true)
                                     select o.person_profile_id)
                                        .Contains(s.person_profile_id)
                                select s;

                    }
                    //if (item.volunteer_committee_position_id != null)
                    //{
                    //model = from s in model
                    //        where
                    //            (from o in
                    //                db.person_volunteer_record.Where(
                    //                    x =>
                    //                        x.volunteer_committee_position_id == item.volunteer_committee_position_id
                    //                        &&
                    //                        x.volunteer_committee_id == item.volunteer_committee_id
                    //                        &&
                    //                        x.fund_source_id == item.fund_source_id
                    //                        &&
                    //                        x.cycle_id == item.cycle_id
                    //                        &&
                    //                        x.enrollment_id == item.enrollment_id
                    //                        &&
                    //                        x.is_deleted != true)
                    //             select o.person_profile_id)
                    //                .Contains(s.person_profile_id)
                    //        select s;
                    //  }

                    if (item.fund_source_id != null)
                    {
                        model = from s in model
                                where
                                    (from o in
                                        db.person_volunteer_record.Where(
                                            x => x.fund_source_id == item.fund_source_id && x.is_deleted != true)
                                     select o.person_profile_id)
                                        .Contains(s.person_profile_id)
                                select s;
                    }

                    if (item.cycle_id != null)
                    {
                        model = from s in model
                                where
                                    (from o in
                                        db.person_volunteer_record.Where(
                                            x => x.cycle_id == item.cycle_id && x.is_deleted != true)
                                     select o.person_profile_id)
                                        .Contains(s.person_profile_id)
                                select s;
                    }

                    if (item.enrollment_id != null)
                    {
                        model = from s in model
                                where
                                    (from o in
                                        db.person_volunteer_record.Where(
                                            x => x.enrollment_id == item.enrollment_id && x.is_deleted != true)
                                     select o.person_profile_id)
                                        .Contains(s.person_profile_id)
                                select s;
                    }

                }
                else
                {
                    model = from s in model
                            where
                                !(from o in
                                    db.person_volunteer_record.Where(
                                        x =>

                                            x.is_deleted != true)
                                  select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                            select s;

                    //   model = model.Where(x => x.is_volunteer =! true);
                }
            }
            if (item.is_worker != null)
            {
                if (item.is_worker == true)
                {
                    model = from s in model
                            where
                                (from o in
                                    db.person_ers_work.Where(
                                        x =>

                                            x.is_deleted != true)
                                 select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                            select s;
                }
                else
                {
                    model = from s in model
                            where
                                !(from o in
                                    db.person_ers_work.Where(
                                        x =>

                                            x.is_deleted != true)
                                  select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                            select s;
                }
            }

            if (item.sub_project_ers_id != null)
            {
                //this gets the Non ERS workers for this LIST
                model = from s in model
                        where
                            !(from o in
                                db.person_ers_work.Where(
                                    x => x.sub_project_ers_id == item.sub_project_ers_id && x.is_deleted != true)
                              select o.person_profile_id)
                                .Contains(s.person_profile_id)
                        select s;
            }

            if (item.is_lguofficial != null)
            {

                if (item.is_lguofficial == true)
                {
                    model = model.Where(x => x.is_lguofficial == true);
                }
                else
                {
                    model = model.Where(x => x.is_lguofficial != true);
                }

            }
            if (item.is_trained != null)
            {


                //for training module
                if (item.is_trained == true)
                {

                    model = from s in model
                            where
                                (from o in
                                    db.person_training.Where(
                                        x =>
                                          
                                            x.is_participant == true)
                                 select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                            select s;


                    //checks if person is member of a conducted training
                    if (item.community_training_id != null)
                    {
                        model = from s in model
                                where
                                    (from o in
                                        db.person_training.Where(
                                            x =>
                                                x.community_training_id == item.community_training_id &&
                                                x.is_participant == true)
                                     select o.person_profile_id)
                                        .Contains(s.person_profile_id)
                                select s;
                    }

                    if (item.training_category_id != null)
                    {
                        model = from s in model
                                where (from o in db.person_training.Where(x => x.community_training.training_category_id == item.training_category_id)
                                    .Include(x => x.community_training)
                                    .Where(
                                        x =>
                                            x.community_training_id == item.community_training_id &&
                                            x.is_participant == true &&
                                            x.community_training.training_category_id == item.training_category_id)
                                       select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                                select s;
                    }

                    // model = model.GroupBy(x => x.person_profile_id).Select(x => x.FirstOrDefault());
                }
                if (item.is_trained == false)
                {
                    if (item.community_training_id != null)
                    {
                        model = from s in model
                                where
                                    !(from o in
                                        db.person_training.Where(
                                            x =>
                                                x.community_training_id == item.community_training_id &&
                                                x.is_participant == true)
                                      select o.person_profile_id)
                                        .Contains(s.person_profile_id)
                                select s;
                    }



                    if (item.training_category_id != null)
                    {


                        model = from s in model
                                where !(from o in db.person_training
                              //      .Include(x => x.community_training)
                                    .Where(
                                        x =>
                                        //    x.community_training_id == item.community_training_id &&
                                            x.is_participant == true
                                            //&&
                                            //x.community_training.training_category_id == item.training_category_id
                                            )
                                        select o.person_profile_id)
                                    .Contains(s.person_profile_id)
                                select s;
                    }

                }




            }
            if (item.is_pantawid != null)
            {
                if (item.is_pantawid == true)
                {
                    model = model.Where(x => x.is_pantawid == true);
                }
                else
                {
                    model = model.Where(x => x.is_pantawid != true);
                }
            }
            if (item.is_slp != null)
            {
                if (item.is_slp == true)
                {
                    model = model.Where(x => x.is_slp == true);
                }
                else
                {
                    model = model.Where(x => x.is_slp != true);
                }
            }
            if (item.is_male != null)
            {
                if (item.is_male == true)
                {
                    model = model.Where(x => x.sex == true);
                }
                else
                {
                    model = model.Where(x => x.sex != true);
                }
            }
            return model;
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







        [HttpPost]
        [Route("api/offline/v1/profiles/get_dto")]
        public PagedCollection<person_profileDTO> GetDTO(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<person_profileDTO>()
            {
                //TotalSync = model.Count(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted != true)),
                TotalSync = model.Count(x => x.is_deleted !=true && (x.push_status_id == 2 || x.push_status_id == 3)),
                TotalUnAuthorized = model.Count(x => x.push_status_id == 4 && x.is_deleted != true),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
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
                            lib_region_region_name = x.lib_region.region_name

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
                            lib_region_region_name = x.lib_region.region_name

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
                            lib_region_region_name = x.lib_region.region_name

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



            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var record =
                db.person_profile.AsNoTracking().FirstOrDefault(x => x.person_profile_id == model.person_profile_id);

            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }


                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;
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
                model.push_date = null;


                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

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
        public async Task<ActionResult> GetOnline(string username, string password, string city_code = null,
            Guid? record_id = null)
        {





            string token = username + ":" + password;

            byte[] toBytes = Encoding.ASCII.GetBytes(token);


            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                // var model = new auth_messages();

                HttpResponseMessage response =
                    client.GetAsync("api/offline/v1/profiles/get_mapped?city_code=" + city_code + "&id=" + record_id)
                        .Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<person_profile>>(responseJson.Result);

                    //                    var all = Mapper.DynamicMap<List<person_profile_mapping>, List<person_profile>>(model);

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
        public async Task<bool> GetVolunteer(string username, string password, string city_code = null,
            Guid? record_id = null)
        {
            string token = username + ":" + password;

            byte[] toBytes = Encoding.ASCII.GetBytes(token);


            string key = Convert.ToBase64String(toBytes);

            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                HttpResponseMessage response =
                    client.GetAsync("api/online/v1/person/volunteer_record/get?city_code=" + city_code + "&id=" +
                                    record_id).Result;

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

                var items_preselected = db.person_profile.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.person_profile.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));
                   
                    if (record_id != null)
                    {
                        items = items.Where(x => x.person_profile_id == record_id);
                    }

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/profiles/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                    }
                }
                else {
                    var items = db.person_profile.Where(x => x.push_status_id == 5 && x.is_deleted != true);
                    if (record_id != null)
                    {
                        items = items.Where(x => x.person_profile_id == record_id);
                    }
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/profiles/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
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
                //setup client
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                // var model = new auth_messages();

                var items = db.person_volunteer_record.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted != true));

                if (record_id != null)
                {
                    items = items.Where(x => x.person_profile_id == record_id);
                }

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/person/volunteer_record/save", data).Result;

                    // response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.push_status_id = 4;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
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
            var record =
                db.person_volunteer_record.AsNoTracking()
                    .FirstOrDefault(x => x.person_volunteer_record_id == model.person_volunteer_record_id);


            if (record == null)
            {

                if (api != true)
                {
                    if (model.volunteer_committee_position_id == 2)
                    {
                        if (db.person_volunteer_record.Count(x => x.person_profile_id == model.person_profile_id &&
                                                                  x.cycle_id == model.cycle_id &&
                                                                  x.volunteer_committee_position_id == 2 &&
                                                                  x.enrollment_id == model.enrollment_id &&
                                                                  x.is_deleted != true) > 0)
                        {

                            return BadRequest("Cannot be head for more than 1 committee");
                        }
                    }

                    else
                    {
                        if (db.person_volunteer_record.Count(x => x.person_profile_id == model.person_profile_id &&
                                                                  x.cycle_id == model.cycle_id &&
                                                                  x.volunteer_committee_position_id == 1 &&
                                                                  model.volunteer_committee_position_id == 1 &&
                                                                  x.volunteer_committee_id == model.volunteer_committee_id &&
                                                                  x.enrollment_id == model.enrollment_id &&
                                                                  x.is_deleted != true) > 0)
                        {

                            return BadRequest("Cannot be member  for more same  committee twice");
                        }
                    }
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }
                else
                {
                    model.push_status_id = 1;
                }



                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;
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
                model.push_date = null;


                if (api == null)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

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

        public bool record_exists_volunteer(Guid id)
        {
            return db.person_volunteer_record.Count(e => e.person_volunteer_record_id == id) > 0;
        }

        #endregion



        #region trainings

        [Route("api/offline/v1/profiles/trainings/get")]
        public IActionResult GetPersonTrainings(Guid? id)
        {


            var model = from s in db.person_training.Where(x => x.person_profile_id == id)
                        join p in db.community_training on s.community_training_id equals p.community_training_id
                        where s.is_participant == true
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
    }
}