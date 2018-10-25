using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeskApp.DataLayer;
using DeskApp.Data;
using Microsoft.EntityFrameworkCore;


using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using DeskApp.Controllers.Repository;
using DeskApp.Services;
using Newtonsoft.Json;

namespace DeskApp.Controllers
{

    public class TrainingController : Controller
    {
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing


        private readonly ApplicationDbContext db;


        private readonly IPSARepository psa;


        public TrainingController(ApplicationDbContext context, IPSARepository _psa)
        {
            db = context;
            psa = _psa;
        }
    
        [Route("/api/export/training/report")]
        public IActionResult export_dqa_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = (from s in model
                          join p in db.person_training.Where(x => x.is_participant == true) on s.community_training_id equals p.community_training_id
                          join q in db.person_profile.Where(x => x.is_deleted != true) on p.person_profile_id equals q.person_profile_id


                          select new
                          {
                              s.community_training_id,
                              s.training_category_id,

                              s.lib_region.region_name,
                              s.lib_province.prov_name,
                              s.lib_city.city_name,
                              brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,

                              fund_source = s.lib_fund_source.name,
                              cycle = s.lib_cycle.name,
                              kc_mode = s.lib_enrollment.name,
                              activity_level = s.lib_lgu_level.name,

                              training_category = s.lib_training_category.name,


                              p.is_participant,
                              is_volunteer = db.person_volunteer_record.Where(x => x.person_profile_id == p.person_profile_id).Count() == 0 ? false : true,
                              is_male = q.sex,
                              is_ip = q.is_ip,
                              is_pp = q.is_pantawid,
                              is_slp = q.is_slp,
                              is_lgu = q.is_lguofficial,

                          }).ToList();


            var export = result.GroupBy(x => new { x.region_name, x.prov_name, x.city_name, x.brgy_name, x.fund_source, x.cycle, x.kc_mode, x.activity_level, x.training_category })
                .Select(x => new
                {
                    x.Key.region_name,
                    x.Key.prov_name,
                    x.Key.city_name,
                    x.Key.brgy_name,
                    x.Key.fund_source,
                    x.Key.cycle,

                    x.Key.kc_mode,
                    x.Key.activity_level,
                    x.Key.training_category,

                    male_volunteer_participants = x.Count(cx => cx.is_male == true && cx.is_volunteer == true),
                    female_volunteer_participants = x.Count(cx => cx.is_male != true && cx.is_volunteer == true),

                    male_non_volunteer_participants = x.Count(cx => cx.is_male == true && cx.is_volunteer != true),
                    female_non_volunteer_participants = x.Count(cx => cx.is_male != true && cx.is_volunteer != true),


                    male_ip_participants = x.Count(cx => cx.is_male == true && cx.is_ip == true),
                    male_pp_participants = x.Count(cx => cx.is_male == true && cx.is_pp == true),
                    male_slp_participants = x.Count(cx => cx.is_male == true && cx.is_slp == true),
                    male_lgu_participants = x.Count(cx => cx.is_male == true && cx.is_lgu == true),
                    //no_of_volunteer_participant_in_BAR = x.Where(cx => cx.training_category_id == 1 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_BPSA = x.Where(cx => cx.training_category_id == 2 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_CMVP = x.Where(cx => cx.training_category_id == 3 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_CSW = x.Where(cx => cx.training_category_id == 4 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_Finance = x.Where(cx => cx.training_category_id == 5 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_GAD = x.Where(cx => cx.training_category_id == 6 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_INFRA = x.Where(cx => cx.training_category_id == 7 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_LGU_training = x.Where(cx => cx.training_category_id == 8 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_MF = x.Where(cx => cx.training_category_id == 9 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_MIAC = x.Where(cx => cx.training_category_id == 10 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_MIBFPRA = x.Where(cx => cx.training_category_id == 11 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_MO = x.Where(cx => cx.training_category_id == 12 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_MPSA = x.Where(cx => cx.training_category_id == 13 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_MunAR = x.Where(cx => cx.training_category_id == 14 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_OM = x.Where(cx => cx.training_category_id == 15 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_ODM = x.Where(cx => cx.training_category_id == 16 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_Others = x.Where(cx => cx.training_category_id == 17 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_PCM = x.Where(cx => cx.training_category_id == 18 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_PPDW = x.Where(cx => cx.training_category_id == 19 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_Proc = x.Where(cx => cx.training_category_id == 20 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_SpecialMIBF = x.Where(cx => cx.training_category_id == 21 && cx.is_volunteer == true).Count(),
                    //no_of_volunteer_participant_in_SPW = x.Where(cx => cx.training_category_id == 22 && cx.is_volunteer == true).Count(),


                    //no_of_non_volunteer_participant_in_BAR = x.Where(cx => cx.training_category_id == 1 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_BPSA = x.Where(cx => cx.training_category_id == 2 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_CMVP = x.Where(cx => cx.training_category_id == 3 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_CSW = x.Where(cx => cx.training_category_id == 4 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_Finance = x.Where(cx => cx.training_category_id == 5 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_GAD = x.Where(cx => cx.training_category_id == 6 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_INFRA = x.Where(cx => cx.training_category_id == 7 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_LGU_training = x.Where(cx => cx.training_category_id == 8 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_MF = x.Where(cx => cx.training_category_id == 9 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_MIAC = x.Where(cx => cx.training_category_id == 10 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_MIBFPRA = x.Where(cx => cx.training_category_id == 11 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_MO = x.Where(cx => cx.training_category_id == 12 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_MPSA = x.Where(cx => cx.training_category_id == 13 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_MunAR = x.Where(cx => cx.training_category_id == 14 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_OM = x.Where(cx => cx.training_category_id == 15 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_ODM = x.Where(cx => cx.training_category_id == 16 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_Others = x.Where(cx => cx.training_category_id == 17 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_PCM = x.Where(cx => cx.training_category_id == 18 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_PPDW = x.Where(cx => cx.training_category_id == 19 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_Proc = x.Where(cx => cx.training_category_id == 20 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_SpecialMIBF = x.Where(cx => cx.training_category_id == 21 && cx.is_volunteer != true).Count(),
                    //no_of_non_volunteer_participant_in_SPW = x.Where(cx => cx.training_category_id == 22 && cx.is_volunteer != true).Count(),



                });




            return Ok(export);
        }


        #region DQA: Trainings

        //DQA: Shows the list of trainings provided to barangays and municipalities with less attendees than expected
        [Route("/api/export/training/dqa")]
        public IActionResult export_dqa_list_(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model
                .GroupBy(c => new
                {
                    c.lib_region,
                    c.lib_province,
                    c.lib_city,
                    c.brgy_code,
                    c.lib_fund_source,
                    c.lib_cycle,
                    c.lib_enrollment,
                    c.lib_lgu_level
                })
                .Select(x => new
                {
                    fund_source = x.Key.lib_fund_source.name,
                    cycle = x.Key.lib_cycle.name,
                    region_name = x.Key.lib_region.region_name,
                    prov_name = x.Key.lib_province.prov_name,
                    city_name = x.Key.lib_city.city_name,
                    brgy_name = x.Key.brgy_code != null ? db.lib_brgy.FirstOrDefault(c => c.brgy_code == x.Key.brgy_code).brgy_name : "",
                    kc_mode = x.Key.lib_enrollment.name,
                    activity_level = x.Key.lib_lgu_level.name,
                    no_of_conducted_in_BAR = x.Where(cx => cx.training_category_id == 1).Count(),
                    no_of_conducted_in_BPSA = x.Where(cx => cx.training_category_id == 2).Count(),
                    no_of_conducted_in_CMVP = x.Where(cx => cx.training_category_id == 3).Count(),
                    no_of_conducted_in_CSW = x.Where(cx => cx.training_category_id == 4).Count(),
                    no_of_conducted_in_Finance = x.Where(cx => cx.training_category_id == 5).Count(),
                    no_of_conducted_in_GAD = x.Where(cx => cx.training_category_id == 6).Count(),
                    no_of_conducted_in_INFRA = x.Where(cx => cx.training_category_id == 7).Count(),
                    no_of_conducted_in_LGU_training = x.Where(cx => cx.training_category_id == 8).Count(),
                    no_of_conducted_in_MF = x.Where(cx => cx.training_category_id == 9).Count(),
                    no_of_conducted_in_MIAC = x.Where(cx => cx.training_category_id == 10).Count(),
                    no_of_conducted_in_MIBFPRA = x.Where(cx => cx.training_category_id == 11).Count(),
                    no_of_conducted_in_MO = x.Where(cx => cx.training_category_id == 12).Count(),
                    no_of_conducted_in_MPSA = x.Where(cx => cx.training_category_id == 13).Count(),
                    no_of_conducted_in_MunAR = x.Where(cx => cx.training_category_id == 14).Count(),
                    no_of_conducted_in_OM = x.Where(cx => cx.training_category_id == 15).Count(),
                    no_of_conducted_in_ODM = x.Where(cx => cx.training_category_id == 16).Count(),
                    no_of_conducted_in_Others = x.Where(cx => cx.training_category_id == 17).Count(),
                    no_of_conducted_in_PCM = x.Where(cx => cx.training_category_id == 18).Count(),
                    no_of_conducted_in_PPDW = x.Where(cx => cx.training_category_id == 19).Count(),
                    no_of_conducted_in_Proc = x.Where(cx => cx.training_category_id == 20).Count(),
                    no_of_conducted_in_SpecialMIBF = x.Where(cx => cx.training_category_id == 21).Count(),
                    no_of_conducted_in_SPW = x.Where(cx => cx.training_category_id == 22).Count(),
                }).ToList();


            var export = result
                .GroupBy(x => new
                {
                    x.region_name,
                    x.prov_name,
                    x.city_name,
                    x.brgy_name,
                    x.fund_source,
                    x.cycle,
                    x.kc_mode,
                    x.activity_level
                })
                .Select(x => new
                {
                    fund_source = x.Key.fund_source,
                    cycle = x.Key.cycle,
                    region_name = x.Key.region_name,
                    prov_name = x.Key.prov_name,
                    city_name = x.Key.city_name,
                    brgy_name = x.Key.brgy_name,
                    kc_mode = x.Key.kc_mode,
                    activity_level = x.Key.activity_level,
                    no_of_conducted_in_BAR = x.Sum(cx => cx.no_of_conducted_in_BAR),
                    no_of_conducted_in_BPSA = x.Sum(cx => cx.no_of_conducted_in_BPSA),
                    no_of_conducted_in_CMVP = x.Sum(cx => cx.no_of_conducted_in_CMVP),
                    no_of_conducted_in_CSW = x.Sum(cx => cx.no_of_conducted_in_CSW),
                    no_of_conducted_in_Finance = x.Sum(cx => cx.no_of_conducted_in_Finance),
                    no_of_conducted_in_GAD = x.Sum(cx => cx.no_of_conducted_in_GAD),
                    no_of_conducted_in_INFRA = x.Sum(cx => cx.no_of_conducted_in_INFRA),
                    no_of_conducted_in_LGU_training = x.Sum(cx => cx.no_of_conducted_in_LGU_training),
                    no_of_conducted_in_MF = x.Sum(cx => cx.no_of_conducted_in_MF),
                    no_of_conducted_in_MIAC = x.Sum(cx => cx.no_of_conducted_in_MIAC),
                    no_of_conducted_in_MIBFPRA = x.Sum(cx => cx.no_of_conducted_in_MIBFPRA),
                    no_of_conducted_in_MO = x.Sum(cx => cx.no_of_conducted_in_MO),
                    no_of_conducted_in_MPSA = x.Sum(cx => cx.no_of_conducted_in_MPSA),
                    no_of_conducted_in_MunAR = x.Sum(cx => cx.no_of_conducted_in_MunAR),
                    no_of_conducted_in_OM = x.Sum(cx => cx.no_of_conducted_in_OM),
                    no_of_conducted_in_ODM = x.Sum(cx => cx.no_of_conducted_in_ODM),
                    no_of_conducted_in_Others = x.Sum(cx => cx.no_of_conducted_in_Others),
                    no_of_conducted_in_PCM = x.Sum(cx => cx.no_of_conducted_in_PCM),
                    no_of_conducted_in_PPDW = x.Sum(cx => cx.no_of_conducted_in_PPDW),
                    no_of_conducted_in_Proc = x.Sum(cx => cx.no_of_conducted_in_Proc),
                    no_of_conducted_in_SpecialMIBF = x.Sum(cx => cx.no_of_conducted_in_SpecialMIBF),
                    no_of_conducted_in_SPW = x.Sum(cx => cx.no_of_conducted_in_SPW)
                });

            return Ok(export);
        }

        //DQA: Displays Trainings with Others as category
        [Route("/api/export/training/dqa/trainings_with_others_category")]
        public IActionResult export_dqa_trainings_with_others_category(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model
                .Select(x => new
                {
                    Training_Unique_Id = x.community_training_id,
                    Region = x.lib_region.region_name,
                    Province = x.lib_province.prov_name,
                    Municipality = x.lib_city.city_name,
                    Barangay = x.lib_brgy.brgy_name,
                    Project = x.lib_fund_source.name,
                    Cycle = x.lib_cycle.name,
                    Training_Category = x.lib_training_category.name,
                    Training_Category_Id = x.training_category_id,
                    Training_Title = x.training_title,
                    Date_Conducted = x.start_date.Value.ToString("dd/MM/yyyy") + " - " + x.end_date.Value.ToString("dd/MM/yyyy")
                })
                .Where(x => x.Training_Category_Id == 17)
                .ToList();


            var export = result
                .Select(x => new
                {
                    Training_Unique_Id = x.Training_Unique_Id,
                    Region = x.Region,
                    Province = x.Province,
                    Municipality = x.Municipality,
                    Barangay = x.Barangay,
                    Project = x.Project,
                    Cycle = x.Cycle,
                    Training_Category = x.Training_Category,
                    Training_Title = x.Training_Title,
                    Date_Conducted = x.Date_Conducted
                });

            return Ok(export);
        }

        //DQA: Displays Trainings without criteria or total rate is less than 100%
        [Route("/api/export/training/dqa/trainings_without_criteria")]
        public IActionResult export_dqa_trainings_without_criteria(AngularFilterModel item)
        {
            var model = GetData(item);
            var mibf_criteria = db.mibf_criteria.Where(x => x.is_deleted != true);

            var criteria_list = from mc in mibf_criteria
                                group mc.rate by new { mc.community_training_id } into grp
                                select new { grp.Key.community_training_id, Total_Rate = grp.Sum() };
            
            var csw_without_criteria = from c in model
                                       where c.training_category_id == 4 && !(from o in mibf_criteria
                                                                              select o.community_training_id).Contains(c.community_training_id)
                                       select new
                                       {
                                           Training_Unique_Id = c.community_training_id,
                                           Region = c.lib_region.region_name == null ? null : c.lib_region.region_name,
                                           Province = c.lib_province.prov_name == null ? null : c.lib_province.prov_name,
                                           Municipality = c.lib_city.city_name == null ? null : c.lib_city.city_name,
                                           Barangay = c.lib_brgy.brgy_name == null ? null : c.lib_brgy.brgy_name,
                                           Project = c.lib_fund_source.name == null ? null : c.lib_fund_source.name,
                                           Cycle = c.lib_cycle.name == null ? null : c.lib_cycle.name,
                                           Training_Category = c.lib_training_category.name == null ? null : c.lib_training_category.name,
                                           Training_Title = c.training_title == null ? null : c.lib_region.region_name
                                       };

            var result = from m in model
                         join c in criteria_list on m.community_training_id equals c.community_training_id
                         where c.Total_Rate < 100
                         select new
                         {
                             Training_Unique_Id = m.community_training_id,
                             Region = m.lib_region.region_name == null ? null : m.lib_region.region_name,
                             Province = m.lib_province.prov_name == null ? null : m.lib_province.prov_name,
                             Municipality = m.lib_city.city_name == null ? null : m.lib_city.city_name,
                             Barangay = m.lib_brgy.brgy_name == null ? null : m.lib_brgy.brgy_name,
                             Project = m.lib_fund_source.name == null ? null : m.lib_fund_source.name,
                             Cycle = m.lib_cycle.name == null ? null : m.lib_cycle.name,
                             Training_Category = m.lib_training_category.name == null ? null : m.lib_training_category.name,
                             Training_Title = m.training_title == null ? null : m.lib_region.region_name,
                             Total_Rate = c.Total_Rate == null ? null : c.Total_Rate
                         };

            //var export = from list1 in result
            //             join list2 in csw_without_criteria
            //             on list1.Training_Unique_Id equals list2.Training_Unique_Id
            //             select new
            //             {
            //                 Training_Unique_Id = list1.Training_Unique_Id,
            //                 Region = list1.Region == null ? null : list1.Region,
            //                 Province = list1.Province == null ? null : list1.Province,
            //                 Municipality = list1.Municipality == null ? null : list1.Municipality,
            //                 Barangay = list1.Barangay == null ? null : list1.Barangay,
            //                 Project = list1.Project == null ? null : list1.Project,
            //                 Cycle = list1.Cycle == null ? null : list1.Cycle,
            //                 Training_Category = list1.Training_Category == null ? null : list1.Training_Category,
            //                 Training_Title = list1.Training_Title == null ? null : list1.Training_Title,
            //                 Total_Rate = list1.Total_Rate == null ? null : list1.Total_Rate + "%"
            //             };

            var export = result
                .Select(x => new
                {
                    Training_Unique_Id = x.Training_Unique_Id,
                    Region = x.Region,
                    Province = x.Province,
                    Municipality = x.Municipality,
                    Barangay = x.Barangay,
                    Project = x.Project,
                    Cycle = x.Cycle,
                    Training_Category = x.Training_Category,
                    Training_Title = x.Training_Title,
                    Total_Rate = x.Total_Rate == null ? null : x.Total_Rate + "%"
                }).ToList();

            return Ok(export);
            
        }

        //DQA: Training null values -- Display Trainings encoded that contains null values on the minimum required fields
        [HttpPost]
        [Route("/api/export/training/dqa/null_required_fields")]
        public IActionResult export_dqa_null_required_fields(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model
                .Select(x => new
                {
                    Training_Unique_Id = x.community_training_id,
                    Region = x.region_code == null ? null : x.lib_region.region_name,
                    Province = x.prov_code == null ? null : x.lib_province.prov_name,
                    Municipality = x.city_code == null ? null : x.lib_city.city_name,
                    Barangay = x.brgy_code == null ? null : x.lib_brgy.brgy_name,
                    Project = x.fund_source_id == null ? null : x.lib_fund_source.name,
                    Cycle = x.cycle_id == null ? null : x.lib_cycle.name,
                    KC_Mode = x.enrollment_id == null ? null : x.lib_enrollment.name,                    
                    Training_Level = x.lib_lgu_level.name == null ? null : x.lib_lgu_level.name,
                    Training_Title = x.training_title == null ? null : x.training_title,
                    Training_Category = x.lib_training_category.name == null ? null : x.lib_training_category.name,
                    Reported_By = x.reported_by == null ? null : x.reported_by,
                    Venue = x.venue == null ? null : x.venue,
                    Start_Date = x.start_date == null ? null : x.start_date.Value.ToString("dd/MM/yyyy"),
                    End_Date = x.end_date == null ? null : x.end_date.Value.ToString("dd/MM/yyyy"),
                    Is_Incentives = x.is_incentive == null || x.is_incentive == false ? null : "Yes",
                    From_Savings = x.is_savings == null || x.is_savings == false ? null : "Yes",
                    Is_LGU_Led = x.is_lgu_led == null || x.is_lgu_led == false ? null : "Yes",
                    Participants = db.person_training.Any(t => t.community_training_id == x.community_training_id && t.is_participant == true) == true ? "With Participants encoded" : null,
                    Sector_Represented = x.is_sector_academe == null && x.is_sector_business == null && x.is_sector_pwd == null && x.is_sector_farmer == null && x.is_sector_fisherfolks == null && x.is_sector_government == null && x.is_sector_ip == null && x.is_sector_ngo == null && x.is_sector_po == null && x.is_sector_religios == null && x.is_sector_senior == null && x.is_sector_women == null && x.is_sector_youth == null ? null : "With sector represented",
                    tr_level = x.lgu_level_id,
                })
                .Where(x => (x.tr_level == 1 && (x.Region == null || x.Province == null || x.Municipality == null || x.Barangay == null || x.Project == null || x.Cycle == null || x.KC_Mode == null || x.Training_Title == null || x.Training_Category == null || x.Reported_By == null || x.Venue == null || x.Start_Date == null || x.End_Date == null || x.Is_Incentives == null || x.From_Savings == null || x.Is_LGU_Led == null || x.Participants == null || x.Sector_Represented == null)) ||
                            (x.tr_level == 2 && (x.Region == null || x.Province == null || x.Municipality == null || x.Project == null || x.Cycle == null || x.KC_Mode == null || x.Training_Title == null || x.Training_Category == null || x.Reported_By == null || x.Venue == null || x.Start_Date == null || x.End_Date == null || x.Is_Incentives == null || x.From_Savings == null || x.Is_LGU_Led == null || x.Participants == null || x.Sector_Represented == null))
                      )
                .ToList();

            return Ok(result);
        }       
        
        #endregion



        //09-08-17: Addded new export for v3.0
        [Route("api/export/training/pra")]
        public IActionResult export_pra_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = (from s in model
                          join m in db.mibf_prioritization.Where(x => x.is_deleted != true) on s.community_training_id equals m.community_training_id

                          select new
                          {
                              s.lib_region.region_name,
                              s.lib_province.prov_name,
                              s.lib_city.city_name,

                              fund_source = s.lib_fund_source.name,
                              cycle = s.lib_cycle.name,                              

                              rank = m.rank,
                              sub_project_title = m.project_name,
                              lead_barangay = db.lib_brgy.FirstOrDefault(c => c.brgy_code == m.brgy_code).brgy_name,
                              proposed_kc_grant_amount = m.kc_amount,
                              proposed_lcc_amount = m.lcc_amount,
                              is_priority = m.is_priority == true ? "Yes" : "No"
                              

                          }).ToList();
            

            return Ok(result);
        }

        //09-08-17: Addded new export for v3.0
        [Route("api/export/training/csw")]
        public IActionResult export_csw_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = (from s in model
                          join m in db.mibf_criteria.Where(x => x.is_deleted != true) on s.community_training_id equals m.community_training_id

                          select new
                          {
                              s.lib_region.region_name,
                              s.lib_province.prov_name,
                              s.lib_city.city_name,

                              fund_source = s.lib_fund_source.name,
                              cycle = s.lib_cycle.name,

                              criteria = m.criteria,
                              rate = m.rate

                          }).ToList();

            return Ok(result);
        }


        [Route("api/export/community_training/list")]
        public IActionResult export_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from s in model
                         select new
                         {
                             s.community_training_id,

                             fund_source = s.lib_fund_source.name,
                             cycle = s.lib_cycle.name,
                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,
                             kc_mode = s.lib_enrollment.name,

                             s.training_title,
                             training_category = s.lib_training_category.name,
                             lgu_level_id = s.lgu_level_id,
                             lgu_name = s.lib_lgu_level.name,
                             start_date = s.start_date == null ? null : s.start_date.Value.ToString("dd/MM/yyyy"),
                             end_date = s.end_date == null ? null : s.end_date.Value.ToString("dd/MM/yyyy"),

                             s.venue,
                             no_atn_female = db.person_training.Count(x => x.person_profile.sex != true && x.is_participant == true && x.community_training_id == s.community_training_id),
                             no_atn_male = db.person_training.Count(x => x.person_profile.sex == true && x.is_participant == true && x.community_training_id == s.community_training_id),
                             no_ip_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == s.community_training_id),
                             no_ip_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == s.community_training_id),

                             no_pantawid_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == s.community_training_id),
                             no_pantawid_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == s.community_training_id),

                             no_slp_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == s.community_training_id),
                             no_slp_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == s.community_training_id),

                             no_lgu_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_lguofficial == true && x.is_participant == true && x.community_training_id == s.community_training_id),
                             no_lgu_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_lguofficial == true && x.is_participant == true && x.community_training_id == s.community_training_id),

                             //male_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == s.community_training_id && x.is_participant == true && x.is_deleted != true)
                             //                               where
                             //                                    (from o in
                             //                                        db.person_volunteer_record.Where(x => x.is_deleted != true
                             //                                        && x.person_profile.is_deleted != true
                             //                                        && x.person_profile.sex == true)
                             //                                     select o.person_profile_id)
                             //                                        .Contains(i.person_profile_id)

                             //                               select i).Count(),

                             male_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == s.community_training_id && x.is_participant == true && x.person_profile.sex == true && x.is_deleted != true)
                                                            where
                                                                 (from o in
                                                                     db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                     && x.person_profile.person_profile_id == x.person_profile_id)
                                                                  select o.person_profile_id)
                                                                     .Contains(i.person_profile_id)

                                                            select i).Count(),

                             male_non_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == s.community_training_id && x.is_participant == true && x.person_profile.sex == true && x.is_deleted != true)
                                                                  where
                                                                       !(from o in
                                                                             db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                             && x.person_profile.person_profile_id == x.person_profile_id)
                                                                         select o.person_profile_id)
                                                                           .Contains(i.person_profile_id)

                                                                  select i).Count(),

                             female_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == s.community_training_id && x.is_participant == true && x.person_profile.sex != true && x.is_deleted != true)
                                                            where
                                                                 (from o in
                                                                     db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                     && x.person_profile.person_profile_id == x.person_profile_id)
                                                                  select o.person_profile_id)
                                                                     .Contains(i.person_profile_id)

                                                            select i).Count(),

                             female_non_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == s.community_training_id && x.is_participant == true && x.person_profile.sex != true && x.is_deleted != true)
                                                                where
                                                                     !(from o in
                                                                           db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                           && x.person_profile.person_profile_id == x.person_profile_id)
                                                                       select o.person_profile_id)
                                                                         .Contains(i.person_profile_id)

                                                                select i).Count(),


                             //female_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == s.community_training_id && x.is_deleted != true)
                             //                                 where
                             //                                      (from o in
                             //                                          db.person_volunteer_record.Where(x => x.is_deleted != true
                             //                                          && x.person_profile.is_deleted != true
                             //                                          && x.person_profile.sex != true)
                             //                                       select o.person_profile_id)
                             //                                          .Contains(i.person_profile_id)

                             //                                 select i).Count(),

                             //female_non_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == s.community_training_id && x.is_deleted != true)
                             //                                   where
                             //                                        !(from o in
                             //                                                    db.person_volunteer_record.Where(x => x.is_deleted != true
                             //                                              && x.person_profile.is_deleted != true
                             //                                              && x.person_profile.sex != true)
                             //                                          select o.person_profile_id)
                             //                                            .Contains(i.person_profile_id)

                             //                                   select i).Count(),

                             //male_non_volunteer_participants

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


        [Route("api/export/muniandbrgy_training_participants/list")]
        public IActionResult subreport4_training_participants_list(AngularFilterModel item)
        {
            var ct_model = GetData(item);
            var pt_model = db.person_training.Where(pt => pt.is_participant == true && pt.is_deleted != true);

            var result = from ct in ct_model                         
                         group ct by new { ct.lgu_level_id, ct.lib_training_category.name } into grp
                         join pt in pt_model on grp.FirstOrDefault().community_training_id equals pt.community_training_id
                         orderby grp.Key.name, grp.Key.lgu_level_id
                         select new
                         {
                             training_category = grp.Key.name,
                             lgu_level_id = grp.Key.lgu_level_id,
                             num_of_trainings = grp.Count(),

                             male_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex == true && x.is_deleted != true)
                                                            where
                                                                 (from o in
                                                                       db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                       && x.person_profile.person_profile_id == x.person_profile_id)
                                                                  select o.person_profile_id)
                                                                     .Contains(i.person_profile_id)
                                                            select i).Count(),

                             male_non_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex == true && x.is_deleted != true)
                                                                where
                                                                     !(from o in
                                                                           db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                           && x.person_profile.person_profile_id == x.person_profile_id)
                                                                       select o.person_profile_id)
                                                                         .Contains(i.person_profile_id)
                                                                select i).Count(),

                             female_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex != true && x.is_deleted != true)
                                                              where
                                                                   (from o in
                                                                       db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                       && x.person_profile.person_profile_id == x.person_profile_id)
                                                                    select o.person_profile_id)
                                                                       .Contains(i.person_profile_id)
                                                              select i).Count(),

                             female_non_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex != true && x.is_deleted != true)
                                                                  where
                                                                       !(from o in
                                                                             db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                             && x.person_profile.person_profile_id == x.person_profile_id)
                                                                         select o.person_profile_id)
                                                                           .Contains(i.person_profile_id)
                                                                  select i).Count(),

                             no_ip_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_pantawid_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_slp_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_lgu_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_lguofficial == true && x.is_participant == true && x.community_training_id == pt.community_training_id),

                             no_ip_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_pantawid_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_slp_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_lgu_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_lguofficial == true && x.is_participant == true && x.community_training_id == pt.community_training_id),

                         };


            return Ok(result);
        }


        [Route("api/export/muniandbrgy_trainings_provided_to_brgy/list")]
        public IActionResult subreport5_training_provided_to_brgy_list(AngularFilterModel item)
        {
            var ct_model = GetData(item);
            var pt_model = db.person_training.Where(pt => pt.is_participant == true && pt.is_deleted != true);

            var result = from ct in ct_model
                         group ct by new { ct.lib_brgy.brgy_name, ct.lib_training_category.name } into grp
                         join pt in pt_model on grp.FirstOrDefault().community_training_id equals pt.community_training_id
                         orderby grp.Key.name
                         select new
                         {
                             training_category = grp.Key.name,
                             brgy_name = grp.Key.brgy_name,
                             num_of_trainings = grp.Count(),

                             male_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex == true && x.is_deleted != true)
                                                            where
                                                                 (from o in
                                                                     db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                     && x.person_profile.person_profile_id == x.person_profile_id)
                                                                  select o.person_profile_id)
                                                                     .Contains(i.person_profile_id)
                                                            select i).Count(),

                             male_non_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex == true && x.is_deleted != true)
                                                                where
                                                                     !(from o in
                                                                           db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                           && x.person_profile.person_profile_id == x.person_profile_id)
                                                                       select o.person_profile_id)
                                                                         .Contains(i.person_profile_id)
                                                                select i).Count(),

                             female_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex != true && x.is_deleted != true)
                                                              where
                                                                   (from o in
                                                                       db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                       && x.person_profile.person_profile_id == x.person_profile_id)
                                                                    select o.person_profile_id)
                                                                       .Contains(i.person_profile_id)
                                                              select i).Count(),

                             female_non_volunteer_participants = (from i in db.person_training.Where(x => x.community_training_id == pt.community_training_id && x.is_participant == true && x.person_profile.sex != true && x.is_deleted != true)
                                                                  where
                                                                       !(from o in
                                                                             db.person_volunteer_record.Where(x => x.is_deleted != true
                                                                             && x.person_profile.person_profile_id == x.person_profile_id)
                                                                         select o.person_profile_id)
                                                                           .Contains(i.person_profile_id)
                                                                  select i).Count(),

                             no_ip_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_pantawid_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_slp_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_lgu_male = db.person_training.Count(x => x.person_profile.sex == true && x.person_profile.is_lguofficial == true && x.is_participant == true && x.community_training_id == pt.community_training_id),

                             no_ip_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_pantawid_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_slp_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == pt.community_training_id),
                             no_lgu_female = db.person_training.Count(x => x.person_profile.sex != true && x.person_profile.is_lguofficial == true && x.is_participant == true && x.community_training_id == pt.community_training_id),

                         };

            return Ok(result.Distinct());
        }

        private IQueryable<community_training> GetData(AngularFilterModel item)

        {
            var model = db.community_training
                .Include(x => x.lib_fund_source)
                .Include(x => x.lib_cycle)
                .Include(x => x.lib_enrollment)
                .Include(x => x.lib_training_category)
                 .Include(x => x.lib_region)
                .Include(x => x.lib_province)
                .Include(x => x.lib_city)
                .Include(x => x.lib_brgy)
                .Include(x => x.lib_lgu_level)
                .Where(x => x.is_deleted != true).AsQueryable();

            //for single sync

            if (item.record_id != null)
            {

                model = model.Where(x => x.community_training_id == item.record_id);

            }

            if (item.lgu_level_id != null)
            {
                model = model.Where(x => x.lgu_level_id == item.lgu_level_id);
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
            if (item.enrollment_id != null)
            {
                model = model.Where(x => x.enrollment_id == item.enrollment_id);
            }
            if (item.push_status_id != null)
            {
                model = model.Where(m => m.push_status_id == item.push_status_id);
            }
            if (item.approval_id != null)
            {
                model = model.Where(m => m.approval_id == item.approval_id);
            }

            if (item.training_category_id != null)
            {
                model = model.Where(m => m.training_category_id == item.training_category_id.Value);
            }
            if (item.lgu_level_id != null)
            {
                model = model.Where(m => m.lgu_level_id == item.lgu_level_id);
            }


            if (item.name != null)
            {
                model = model.Where(m => m.training_title.Contains(item.name));
            }


            if (item.fund_source_id != null)
            {
                model = model.Where(m => m.fund_source_id == item.fund_source_id);
            }
            if (item.cycle_id != null)
            {
                model = model.Where(m => m.cycle_id == item.cycle_id);
            }

            //v3.0 additional filters:
            if (item.is_incentive != null)
            {
                if (item.is_incentive == true)
                {
                    model = model.Where(m => m.is_incentive == true);
                }
                else
                {
                    model = model.Where(m => m.is_incentive != true);
                }
            }
            if (item.is_savings != null)
            {
                if (item.is_savings == true)
                {
                    model = model.Where(m => m.is_savings == true);
                }
                else
                {
                    model = model.Where(m => m.is_savings != true);
                }
            }
            if (item.is_lgu_led != null)
            {
                if (item.is_lgu_led == true)
                {
                    model = model.Where(m => m.is_lgu_led == true);
                }
                else
                {
                    model = model.Where(m => m.is_lgu_led != true);
                }
            }
            if (item.is_unauthorized != null)
            {
                if (item.is_unauthorized == true)
                {
                    model = model.Where(m => m.push_status_id == 4);
                }
                else
                {
                    model = model.Where(m => m.push_status_id != 4);
                }
            }


            return model;
        }


        //[HttpPost]
        //[Route("export/trainings/list")]
        //public IActionResult export_trainings(AngularFilterModel item)
        //{



        //    var model = GetData(item);




        //    var result = from s in model
        //                 select new
        //                 {
        //                     s.community_training_id,

        //                     fund_source = s.lib_fund_source.name,
        //                     cycle = s.lib_cycle.name,







        //                     lib_region_region_name = db.lib_region.First(c => c.region_code == s.region_code).region_nick,
        //                     lib_province_prov_name = db.lib_province.First(c => c.prov_code == s.prov_code).prov_name,
        //                     lib_city_city_name = db.lib_city.First(c => c.city_code == s.city_code).city_name,
        //                     lib_brgy_brgy_name = s.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == s.brgy_code).brgy_name,


        //                     lib_lgu_level_name = db.lib_lgu_level.First(c => c.lgu_level_id == s.lgu_level_id).name,



        //                     kc_mode = s.lib_enrollment.name,

        //                     s.training_title,
        //                     training_category = s.lib_training_category.name,
        //                     lgu_level = s.lib_lgu_level.name,
        //                     s.start_date,
        //                     s.end_date,

        //                     s.venue,
        //                     //     no_atn_female = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex != true && x.is_participant == true && x.community_training_id == s.community_training_id) == 0 ? 0 : db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex != true && x.is_participant == true && x.community_training_id == s.community_training_id),
        //                     //no_atn_male = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex == true && x.is_participant == true && x.community_training_id == s.community_training_id),
        //                     //no_ip_female = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex != true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == s.community_training_id),
        //                     //no_ip_male = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex == true && x.person_profile.is_ip == true && x.is_participant == true && x.community_training_id == s.community_training_id),

        //                     //no_pantawid_female = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex != true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == s.community_training_id),
        //                     //no_pantawid_male = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex == true && x.person_profile.is_pantawid == true && x.is_participant == true && x.community_training_id == s.community_training_id),

        //                     //no_slp_female = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex != true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == s.community_training_id),
        //                     //no_slp_male = db.person_training.Include(x => x.person_profile).Count(x => x.person_profile.sex == true && x.person_profile.is_slp == true && x.is_participant == true && x.community_training_id == s.community_training_id)
        //                 };


        //    return new ExcelFileResult(result);
        //}


        [HttpPost]
        [Route("api/offline/v1/trainings/get_dto")]
        public PagedCollection<community_trainingDTO> GetDTO(AngularFilterModel item)
        {
            var model = GetData(item);

            var totalCount = model.Count();

            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<community_trainingDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                
                Items = model.OrderBy(x => x.community_training_id)
                .Select(x => new community_trainingDTO
                {
                    community_training_id = x.community_training_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    lib_lgu_level_name = x.lib_lgu_level.name,
                    lib_training_category_name = x.lib_training_category.name,
                    training_title = x.training_title,
                    lib_fund_source_name = x.lib_fund_source.name,
                    lib_cycle_name = x.lib_cycle.name,
                    lib_enrollment_name = x.lib_enrollment.name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date
                }).Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/trainings/get_recently_edited")]
        public PagedCollection<community_trainingDTO> get_recently_edited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<community_trainingDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),

                Items = model.OrderBy(x => x.community_training_id)
                .Select(x => new community_trainingDTO
                {
                    community_training_id = x.community_training_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    lib_lgu_level_name = x.lib_lgu_level.name,
                    lib_training_category_name = x.lib_training_category.name,
                    training_title = x.training_title,
                    lib_fund_source_name = x.lib_fund_source.name,
                    lib_cycle_name = x.lib_cycle.name,
                    lib_enrollment_name = x.lib_enrollment.name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date
                })
                .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                .Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/trainings/get_recently_added")]
        public PagedCollection<community_trainingDTO> get_recently_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<community_trainingDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),

                Items = model.OrderBy(x => x.community_training_id)
                .Select(x => new community_trainingDTO
                {
                    community_training_id = x.community_training_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    lib_lgu_level_name = x.lib_lgu_level.name,
                    lib_training_category_name = x.lib_training_category.name,
                    training_title = x.training_title,
                    lib_fund_source_name = x.lib_fund_source.name,
                    lib_cycle_name = x.lib_cycle.name,
                    lib_enrollment_name = x.lib_enrollment.name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date
                })
                .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                .Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/trainings/get_recently_edited_and_added")]
        public PagedCollection<community_trainingDTO> get_recently_edited_and_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<community_trainingDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),

                Items = model.OrderBy(x => x.community_training_id)
                .Select(x => new community_trainingDTO
                {
                    community_training_id = x.community_training_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    lib_lgu_level_name = x.lib_lgu_level.name,
                    lib_training_category_name = x.lib_training_category.name,
                    training_title = x.training_title,
                    lib_fund_source_name = x.lib_fund_source.name,
                    lib_cycle_name = x.lib_cycle.name,
                    lib_enrollment_name = x.lib_enrollment.name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date
                })
                .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                .Skip(currPages * size).Take(size).ToList(),
            };
        }


        #region ACT Report: Part C #3:
        [HttpPost]
        [Route("api/export/report/trainings_facilitated/for_the_period_of")]
        public IActionResult export_actreport_fortheperiodof(AngularFilterModel item)
        {
            DateTime? fortheperiodof_from = item.fortheperiodof_from;
            DateTime? fortheperiodof_to = item.fortheperiodof_to;
            int? selected_fund_source = item.fund_source_id;

            var community_training = db.community_training.Where(x => x.fund_source_id == selected_fund_source && (x.start_date >= fortheperiodof_from && x.start_date <= fortheperiodof_to));
            var person_profile = db.person_profile;
            var person_training = db.person_training.Where(x => x.is_participant == true);

            //var joined_result = from ct in community_training
            //                    join pt in person_training on ct.community_training_id equals pt.community_training_id
            //                    join pp in person_profile on pt.person_profile_id equals pp.person_profile_id
            //                    select ct;

            var joined_result = from pp in person_profile
                                join pt in person_training on pp.person_profile_id equals pt.person_profile_id
                                join ct in community_training on pt.community_training_id equals ct.community_training_id
                                select ct;

            //var items = joined_result
            //    .Select(x => new
            //    {
            //        cycle_name = x.lib_cycle.name,
            //        training_category = x.lib_training_category.description,
            //        training_title = x.training_title,
            //        start_date = x.start_date,
            //        end_date = x.end_date,

            //        count_leader_male = x.Where(xc => xc.person_profile.sex == true && xc.volunteer_committee_position_id == 2).Count(),
            //        count_lgu_male = joined_result.Where(c => c)
            //    });

                                //group ct by new
                                //{
                                //    ct.lib_cycle.name,
                                //    ct.fund_source_id,
                                //    ct.community_training_id,
                                //    ct.lib_training_category.description
                                //} into g
                                ////select g;
                                //select new
                                //{
                                //    cycle_name = g.Key.name,
                                //    training_title = 
                                //    //training_title =  
                                //    start_date = g.Select(s => s.start_date),
                                //    end_date = g.Select(s => s.end_date),
                                //    count_lgu_male = g.Where(c => c.pp.is_male == true)
                                //};

            //        var items = community_training
            //            .Join(person_training, ct => ct.community_training_id, pt => pt.community_training_id, (ct, pt) => new { ct, pt })
            //            .Join(person_profile, ppc => ppc.pt.person_profile_id, pp => pp.person_profile_id, (ppc, pp) => new { ppc, pp })
            //            .GroupBy(x => new
            //            {
            //                fund_source = x.ppc.ct.lib_fund_source.fund_source_id,
            //                cycle_name = x.ppc.ct.lib_cycle.name,
            //                training_id = x.ppc.ct.community_training_id
            //            })
            //            .Select(m => new
            //            {
            //                cycle_name = m.Key.cycle_name,
            //                training_category = m.lib_training_category.description,
            //                training_title = m.ppc.ct.training_title,

            //            });

            //        var categorizedProducts = product
            //.Join(productcategory, p => p.Id, pc => pc.ProdId, (p, pc) => new { p, pc })
            //.Join(category, ppc => ppc.pc.CatId, c => c.Id, (ppc, c) => new { ppc, c })
            //.Select(m => new {
            //    ProdId = m.ppc.p.Id, // or m.ppc.pc.ProdId
            //    CatId = m.c.CatId
            //    // other assignments
            //});

            //var items = joined_result
            //    .Select(x => new
            //    {
            //        cycle_name = x.Key.name,
            //        training_category = x.Key.description,
            //        training_title = community_training.training_title,

            //    });

            return Ok(joined_result);
        }




        //[HttpPost]
        //[Route("api/export/report/trainings_facilitated/for_the_period_of")]
        //public IActionResult export_actreport_fortheperiodof(AngularFilterModel item)
        //{
        //    DateTime? fortheperiodof_from = item.fortheperiodof_from;
        //    DateTime? fortheperiodof_to = item.fortheperiodof_to;
        //    int? selected_fund_source = item.fund_source_id;

        //    var training_details = db.community_training.Where(x => x.fund_source_id == selected_fund_source && (x.start_date >= fortheperiodof_from && x.start_date <= fortheperiodof_to));
        //    var participants_details = db.person_profile;
        //    var training_participants = db.person_training.Where(x => x.is_participant == true);

        //    var joined_result = from pt in training_participants
        //                        join 


        //    //    [Route("api/offline/v1/trainings/CheckParticipation")]
        //    //public IActionResult CheckParticipation(Guid person_profile_id, Guid community_training_id)
        //    //{
        //    //    bool model = db.person_training.Any(
        //    //                x =>
        //    //                    x.person_profile_id == person_profile_id && x.community_training_id == community_training_id
        //    //                    && x.is_participant == true);


        //    //    return Ok(model);

        //    //}

            
        //    var result = itemsCovered.GroupBy(x => new
        //    {
        //        fund_source = x.lib_fund_source.fund_source_id,
        //        cycle_name = x.lib_cycle.name,
        //        region_name = x.lib_region.region_name,
        //        prov_name = x.lib_province.prov_name,
        //        city_name = x.lib_city.city_name,
        //        brgy_name = x.lib_brgy.brgy_name,
        //    }).
        //    Select(x => new
        //    {
        //        fund_source_id = x.Key.fund_source,
        //        cycle_name = x.Key.cycle_name,
        //        region_name = x.Key.region_name,
        //        prov_name = x.Key.prov_name,
        //        city_name = x.Key.city_name,
        //        brgy_name = x.Key.brgy_name,

        //        //household:
        //        first_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_household),
        //        first_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_in_barangay),
        //        second_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_household),
        //        second_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_in_barangay),
        //        third_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_household),
        //        third_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_in_barangay),
        //        fourth_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_household),
        //        fourth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_in_barangay),
        //        fifth_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_household),
        //        fifth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_in_barangay),

        //        //IP household:
        //        first_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_ip_household),
        //        first_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_ip_in_barangay),
        //        second_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_ip_household),
        //        second_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_ip_in_barangay),
        //        third_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_ip_household),
        //        third_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_ip_in_barangay),
        //        fourth_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_ip_household),
        //        fourth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_ip_in_barangay),
        //        fifth_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_ip_household),
        //        fifth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_ip_in_barangay),

        //        //male:
        //        first_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_male),
        //        second_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_male),
        //        third_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_male),
        //        fourth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_male),
        //        fifth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_male),

        //        //female:
        //        first_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_female),
        //        second_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_female),
        //        third_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_female),
        //        fourth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_female),
        //        fifth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_female),

        //        first_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_male + c.no_atn_female),
        //        second_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_male + c.no_atn_female),
        //        third_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_male + c.no_atn_female),
        //        fourh_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_male + c.no_atn_female),
        //        fifth_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_male + c.no_atn_female),

        //    }).ToList();

        //    return Ok(result);

        //}


        //public PagedCollection<community_trainingDTO> GetDTO(AngularFilterModel item)
        //{


        //    var model = GetData(item);



        //    var totalCount = model.Count();

        //    int currPages = item.currPage ?? 0;
        //    int size = item.pageSize ?? 10;

        //    return new PagedCollection<community_trainingDTO>()
        //    {


        //        TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),

        //        TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),


        //        Page = currPages,
        //        TotalCount = totalCount,
        //        TotalPages = (int)Math.Ceiling((decimal)totalCount / size),


        //        //   Items = model.OrderBy(x => x.training_title).Skip(currPages * size).Select(community_trainingDTO.SELECT).Take(size).ToList()

        //        Items = model.OrderBy(x => x.community_training_id)
        //        .Select(x => new community_trainingDTO
        //        {
        //            community_training_id = x.community_training_id,
        //            lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
        //            lib_city_city_name = x.lib_city.city_name,
        //            lib_province_prov_name = x.lib_province.prov_name,
        //            lib_region_region_name = x.lib_region.region_name,
        //            lib_lgu_level_name = x.lib_lgu_level.name,
        //            lib_training_category_name = x.lib_training_category.name,
        //            training_title = x.training_title,
        //            lib_fund_source_name = x.lib_fund_source.name,
        //            lib_cycle_name = x.lib_cycle.name,
        //            lib_enrollment_name = x.lib_enrollment.name,
        //            push_status_id = x.push_status_id
        //        }).Skip(currPages * size).Take(size).ToList(),


        //    };
        //}


        #endregion


        [Route("api/offline/v1/trainings/save")]
        public async Task<IActionResult> Save(community_training model, bool? api)
        {
            var record = db.community_training.AsNoTracking().FirstOrDefault(x => x.community_training_id == model.community_training_id && x.is_deleted != true);

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
                else
                {
                    model.push_status_id = 1;
                }

                db.community_training.Add(model);
                
                try
                {
                    await db.SaveChangesAsync();
                    await SaveTracking(model, false);
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
                }

                model.push_date = null;
                model.created_by = record.created_by;
                model.created_date = record.created_date;
                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

                //v3.1 temporarily open cycle for editing. So when cycle is edited, grievance should also be updated
                var grievance_record = db.grievance_record.Where(x => x.activity_source_id == model.community_training_id && x.is_deleted != true);
                if (grievance_record != null) {
                    foreach (var g in grievance_record.ToList())
                    {
                        g.cycle_id = model.cycle_id;
                        g.push_status_id = 3;                        
                    }
                }                

                db.Entry(model).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                    await SaveTracking(model, false);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }

        }






        public async Task<bool> SaveTracking(community_training model, bool? api)
        {
            var record = db.ceac_tracking.FirstOrDefault(x => x.city_code == model.city_code
                                                            && x.brgy_code == model.brgy_code
                                                            && x.fund_source_id == model.fund_source_id
                                                            && x.cycle_id == model.cycle_id
                                                            && x.enrollment_id == model.enrollment_id
                                                            && x.training_category_id == model.training_category_id
                                                            && x.is_deleted != true);

            var ceac_list = db.ceac_list.AsNoTracking().FirstOrDefault(x => x.city_code == model.city_code 
                                                                            && x.cycle_id == model.cycle_id 
                                                                            && x.enrollment_id == model.enrollment_id 
                                                                            && x.is_deleted != true);

            //var record = db.ceac_tracking.AsNoTracking().FirstOrDefault(x => x.ceac_list_id == ceac_list.ceac_list_id && x.training_category_id == model.training_category_id && x.is_deleted != true);

            Guid id = Guid.NewGuid();
                        
            if (ceac_list == null)
            {
                var ceac = new ceac_list
                {
                    ceac_list_id = id,
                    region_code = model.region_code,
                    prov_code = model.prov_code,
                    city_code = model.city_code,
                    approval_id = model.approval_id,
                    fund_source_id = model.fund_source_id,
                    cycle_id = model.cycle_id,
                    enrollment_id = model.enrollment_id,
                    push_status_id = 2,
                    created_by = 0,
                    created_date = DateTime.Now,
                };

                db.ceac_list.Add(ceac);
                await db.SaveChangesAsync();
            }

            else
            {
                id = ceac_list.ceac_list_id;
            }

            if (record == null)
            {
                var training_cat = db.lib_training_category.FirstOrDefault(x => x.training_category_id == model.training_category_id);

                if (training_cat.is_ceac != true)
                {
                    return true;
                }

                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                }

                var ceac = new ceac_tracking()
                {
                    ceac_list_id = id,
                    region_code = model.region_code,
                    prov_code = model.prov_code,
                    city_code = model.city_code,
                    brgy_code = model.brgy_code,
                    approval_id = model.approval_id,
                    fund_source_id = model.fund_source_id,
                    cycle_id = model.cycle_id,
                    enrollment_id = model.enrollment_id,
                    training_category_id = model.training_category_id,
                    reference_id = model.community_training_id,
                    push_status_id = 2,
                    created_by = 0,
                    created_date = DateTime.Now,
                    actual_start = model.start_date,
                    actual_end = model.end_date,
                    plan_end = model.plan_date_end,
                    plan_start = model.plan_date_start,
                    implementation_status_id = 1, //conducted or not yet conducted
                    ceac_tracking_id = Guid.NewGuid(),
                    lgu_level_id = model.lgu_level_id
                };

                if (ceac.actual_end != null && ceac.actual_start != null)
                {
                    ceac.implementation_status_id = 1;
                } 

                if (ceac.implementation_status_id == 1)
                {
                    if (ceac.actual_start == null || ceac.actual_end == null)
                    {
                        ceac.implementation_status_id = 2;
                    }
                }

                db.ceac_tracking.Add(ceac);
                
                try
                {
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }
            }

            else
            {
                

                if (api != true)
                {
                    model.push_status_id = 3;
                    model.push_date = null;
                }

                record.actual_start = model.start_date;
                record.actual_end = model.end_date;
                //test: RDR 06/21/17 to test if deleted training's record will be cleared in CEAC tracking
                record.reference_id = model.community_training_id;

                if (record.actual_end != null && record.actual_start != null) {
                    record.implementation_status_id = 1;
                }                

                if (record.implementation_status_id == 1)
                {
                    if (record.actual_start == null || record.actual_end == null)
                    {
                        record.implementation_status_id = 2;
                    }
                }

                db.Entry(record).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }
            }
        }



        #region 4.0 Fix for Filter of Participants added on training
        private IQueryable<person_profile> GetParticipantData(AngularFilterModel item)

        {
            var model = db.person_profile
                .Where(x => x.is_deleted != true)
                .AsQueryable();
            
            //////FILTER FIELDS:
           
            //Name
            if (!string.IsNullOrEmpty(item.name))
            {
                string converted_name = item.name.ToLower();
                model = model.Where(x => x.first_name.ToLower().Contains(converted_name) || x.last_name.ToLower().Contains(converted_name));
                //model = model.Where(x => x.first_name.Contains(item.name) || x.last_name.Contains(item.name));
            }

            //Brgy
            if (item.brgy_code != null)
            {
                model = model.Where(m => m.brgy_code == item.brgy_code);
            }

            //Trained?
            if (item.is_trained != null)
            {
                if (item.is_trained == true)
                {
                    model = from s in model
                            where (from o in db.person_training.Where(x => x.is_participant == true && x.is_deleted != true && x.community_training_id == item.community_training_id)
                                   select o.person_profile_id).Contains(s.person_profile_id)
                            select s;                    
                }

                if (item.is_trained == false)
                {
                    model = from s in model
                            where !(from o in db.person_training.Where(x => x.is_participant == true && x.is_deleted != true && x.community_training_id == item.community_training_id)
                                    select o.person_profile_id).Contains(s.person_profile_id)
                            select s;
                }
            }


            return model;
        }

        [HttpPost]
        [Route("api/offline/v1/participants/get_dto")]
        public PagedCollection<person_profileDTO> GetParticipants(AngularFilterModel item)
        {
            var model = GetParticipantData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;
            int person_training_count = db.person_training.Count(x => x.is_participant == true && x.community_training_id == item.community_training_id && x.is_deleted != true);

            return new PagedCollection<person_profileDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalCountParticipants = person_training_count,
                Items = model
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
                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }
        #endregion


















        [Route("api/offline/v1/trainings/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.community_training

                .FirstOrDefault(x => x.community_training_id == id);



            if (model == null)
            {
                var item = new community_training();

                item.community_training_id = id;
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


        //For MIBF Prio:
        [Route("api/offline/v1/mibf_prio/get")]
        public IActionResult GetPrioDetails(Guid id)
        {
            var model = db.mibf_prioritization.FirstOrDefault(x => x.mibf_prioritization_id == id);  
            return Ok(model);
        }





        [HttpPost]
        [Route("Sync/Get/trainings")]
        public async Task<ActionResult> GetOnline(string username, string password, string city_code = null, Guid? record_id = null, bool? getPax = null)
        {
            var all = await ApiTrainingOutput.GetTrainings(username, password, city_code, record_id, getPax);

            foreach (var item in all.ToList())
            {
                await Save(item, true);
            }

            foreach (var t in db.community_training.Where(x => x.is_deleted != true).ToList())
            {
                var participants = await ApiTrainingOutput.GetOnlineParticipants(username, password, t.community_training_id);

                foreach (var p in participants.ToList())
                {
                    await SavePersonTraining(p);
                }
            }

            foreach (var t in db.community_training.Where(x => x.is_deleted != true && x.training_category_id == 2).ToList())
            {
                await GetOnlineProblem(username, password, t.community_training_id);
            }

            foreach (var t in db.community_training.Where(x => x.is_deleted != true && x.training_category_id == 2).ToList())
            {
                await GetOnlineSolutions(username, password, t.community_training_id);
            }

            foreach (var t in db.community_training.Where(x => x.is_deleted != true && x.training_category_id == 4).ToList())
            {
                await GetOnlineCriteria(username, password, city_code, t.community_training_id);
            }

            foreach (var t in db.community_training.Where(x => x.is_deleted != true && x.training_category_id == 11 || x.training_category_id == 21 || x.training_category_id == 9).ToList())
            {
                await GetOnlinePriority(username, password, city_code, t.community_training_id);
            }

            return Ok();   

        }



        [Route("api/offline/v1/trainins/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid community_training_id, int push_status_id)
        {
            var training = db.community_training.Find(community_training_id);

            if (training == null)
            {
                return BadRequest("Error");
            }

            training.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        //NEW:
        [HttpPost]
        [Route("Sync/Post/trainings")]
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

                var items_preselected = db.community_training.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any()) {
                    var items = db.community_training.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/trainings/save", data).Result;
                        
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            record_id = item.community_training_id;
                            //PostProblem(username, password, record_id);
                            //PostCriteria(username, password, record_id);
                            //PostPrio(username, password, record_id);
                            //PostParticipants(username, password, record_id);
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
                else {
                    //var items = db.community_training.Where(x => x.push_status_id == 5);
                    var items = db.community_training.Where(x => x.push_status_id == 5 || x.is_deleted == true);

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/trainings/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            record_id = item.community_training_id;
                            //PostProblem(username, password, record_id);
                            //PostCriteria(username, password, record_id);
                            //PostPrio(username, password, record_id);
                            //PostParticipants(username, password, record_id);
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
            //v3.1 ALL AWAITS ARE MOVED INSIDE FOREACH
            //v4.3 put back awaits outside for each
            await PostProblem(username, password, record_id);
            await PostSolution(username, password, record_id);
            await PostCriteria(username, password, record_id);
            await PostPrio(username, password, record_id);
            await PostParticipants(username, password, record_id);
            return Ok();
        }
        

        //private async Task<bool> GetOnlineParticipants(string username, string password, Guid? community_training_id)
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

        //        HttpResponseMessage response = client.GetAsync("api/offline/v1/person_training/get_mapped?community_training_id=" + community_training_id).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseJson = response.Content.ReadAsStringAsync();

        //            var model = JsonConvert.DeserializeObject<List<person_training_mapping>>(responseJson.Result);

        //            var all = Mapper.DynamicMap<List<person_training_mapping>, List<person_training>>(model);

        //            foreach (var item in all.ToList())
        //            {
        //                await SavePersonTraining(item);
        //            }

        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }


        //}

        private async Task<ActionResult> SavePersonTraining(person_training model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pt = db.person_training.AsNoTracking().FirstOrDefault(x => x.person_training_id == model.person_training_id);
            
            if (pt == null)
            {
                db.person_training.Add(model);
                
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string e = ex.ToString();
                    var person_training = model;
                }
            }

            else
            {
                // model.person_training_id = pt.person_training_id;
                db.Entry(model).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string e = ex.ToString();
                    var person_training = model;
                }
            }
            return Ok(new { url = "/View/profiles", id = model.person_profile_id });
        }


        [HttpGet]
        [Route("api/training_record_exists")]
        public bool record_exists(Guid id)
        {
            return db.community_training.Count(e => e.community_training_id == id) > 0;
        }

        [Route("api/offline/v1/trainings/CheckParticipation")]
        public IActionResult CheckParticipation(Guid person_profile_id, Guid community_training_id)
        {
            bool model = db.person_training.Any(
                        x =>
                            x.person_profile_id == person_profile_id && x.community_training_id == community_training_id
                            && x.is_participant == true);


            return Ok(model);

        }

        [Route("api/offline/v1/trainings/SaveBeneficiaryTraining")]
        public ActionResult SaveBeneficiaryTraining(Guid person_profile_id, Guid community_training_id, bool is_participant, bool? api)
        {
            var training = db.community_training.FirstOrDefault(x => x.community_training_id == community_training_id);

            if (training == null)
            {
                return BadRequest("Save Training First Then Proceed in adding Participants");
            }

            var model = db.person_training.FirstOrDefault(x => x.person_profile_id == person_profile_id && x.community_training_id == community_training_id);

            if (model != null)
            {
                if (is_participant == true)
                {
                    model.is_participant = true;
                }
                else
                {
                    model.is_participant = false;
                }
                
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }

                //v3.1 any changes on participant, main training record should be updated as well
                training.push_status_id = 3;
                training.last_modified_by = 0;
                training.last_modified_date = DateTime.Now;
                db.Entry(training).State = EntityState.Modified;

                db.SaveChanges();
            }

            else
            {
                var item = new person_training
                {
                    created_by = 1,
                    created_date = DateTime.Now,
                    approval_id = 3,
                    push_status_id = 3,

                    community_training_id = community_training_id,
                    is_participant = is_participant,
                    person_profile_id = person_profile_id,

                    person_training_id = Guid.NewGuid(),
                };

                if (api != true)
                {
                    item.push_status_id = 2;
                    item.push_date = null;
                    item.approval_id = 3;
                }

                //v3.1 any changes on participant, main training record should be updated as well
                training.push_status_id = 3;
                training.last_modified_by = 0;
                training.last_modified_date = DateTime.Now;
                
                db.person_training.Add(item);
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();

            }



            return Ok();
        }






        #region GET METHOS
        [HttpPost]
        [Route("Sync/Get/problem")]
        public async Task<bool> GetOnlineProblem(string username, string password, Guid? community_training_id = null)
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/psa_problem/get_mapped?community_training_id=" + community_training_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<psa_problem>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        await psa.SaveProblem(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }


        [HttpPost]
        [Route("Sync/Get/solution")]
        public async Task<bool> GetOnlineSolutions(string username, string password, Guid? community_training_id = null)
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/psa_solution/get_mapped?community_training_id=" + community_training_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<psa_solution>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        await psa.SaveSolution(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        [HttpPost]
        [Route("Sync/Get/Criteria")]
        public async Task<bool> GetOnlineCriteria(string username, string password, string city_code = null, Guid? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/trainings/mibf/csw/get?community_training_id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<mibf_criteria>>(responseJson.Result);
                    
                    foreach (var item in model.ToList())
                    {
                        await psa.SaveCriteria(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        [HttpPost]
        [Route("Sync/Get/Priority")]
        public async Task<bool> GetOnlinePriority(string username, string password, string city_code = null, Guid? record_id = null)
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/trainings/mibf/pra/get_mapped?community_training_id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<mibf_prioritization>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        await psa.SavePriority(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion


        public async Task<ActionResult> PostParticipants(string username, string password, Guid? record_id = null)
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
                
                //var items = db.person_training.Where(x => x.community_training_id == record_id || x.is_deleted == true);
                var items = db.person_training.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1)); //v4.3 all pending items will be uploaded

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/person/trainings/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.push_status_id = 4; //added 10-08-2018
                        await db.SaveChangesAsync();
                        //return BadRequest();
                    }
                }
            }
            return Ok();
        }

        public async Task<ActionResult> PostProblem(string username, string password, Guid? record_id = null)
        //old: public async Task<ActionResult>
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

                //var items = db.psa_problem.Where(x => x.community_training_id == record_id || x.is_deleted == true);
                var items = db.psa_problem.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1)); //v4.3 all pending items will be uploaded

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/trainings/psa/problems/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        record_id = item.psa_problem_id;
                        //PostSolution(username, password, record_id);
                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        item.push_status_id = 4; //added 10-08-2018
                        await db.SaveChangesAsync();
                        //return BadRequest();
                        //old: return BadRequest();
                    }
                }
            }
            //old: return Ok();
            return Ok();
        }

        public async Task<ActionResult> PostSolution(string username, string password, Guid? record_id = null)
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
                
                //var items = db.psa_solution.Where(x => x.psa_problem_id == record_id || x.is_deleted == true);
                var items = db.psa_solution.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1)); //v4.3 all pending items will be uploaded
                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/trainings/psa/solutions/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.push_status_id = 4; //added 10-08-2018
                        await db.SaveChangesAsync();
                        //return BadRequest();
                    }
                }
            }
            return Ok();
        }


        public async Task<ActionResult> PostCriteria(string username, string password, Guid? record_id = null)
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
                
                //var items = db.mibf_criteria.Where(x => x.community_training_id == record_id || x.is_deleted == true);
                var items = db.mibf_criteria.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1)); //v4.3 all pending items will be uploaded

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/trainings/mibf/csw/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.push_status_id = 4; //added 10-08-2018
                        await db.SaveChangesAsync();
                        //return BadRequest();
                    }
                }
            }
            return Ok();
        }

        public async Task<ActionResult> PostPrio(string username, string password, Guid? record_id = null)
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
                
                //var items = db.mibf_prioritization.Where(x => x.community_training_id == record_id);
                var items = db.mibf_prioritization.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1)); //v4.3 all pending items will be uploaded

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/trainings/mibf/pra/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.push_status_id = 4; //added 10-08-2018
                        await db.SaveChangesAsync();
                        //return BadRequest();
                    }
                }
            }
            return Ok();
        }

    }
}