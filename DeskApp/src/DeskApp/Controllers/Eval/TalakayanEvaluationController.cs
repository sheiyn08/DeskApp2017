using DeskApp.Data;
using DeskApp.DataLayer;
using DeskApp.DataLayer.Eval;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DeskApp.Controllers.Eval
{
    
    public class TalakayanEvaluationController : Controller
    {
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public TalakayanEvaluationController(ApplicationDbContext context)
        {
            db = context;
        }

        //api delete moved to DeleteController.cs 01-24-18

        public ActionResult Index()
        {
            return View();
        }

        //Talakayan.cshtml
        public IActionResult Talakayan(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }
            ViewBag.id = id;

            return View();
        }

        //GetData:
        private IQueryable<talakayan_eval> GetData(
                 DataLayer.AngularFilterModel item
           )
        {
            var model = db.talakayan_eval.Where(x => x.is_deleted != true).AsQueryable();


            if (!string.IsNullOrEmpty(item.name))
            {
                model = model.Where(x => x.person_name.Contains(item.name));
            }
            if (item.is_male != null)
            {
                model = model.Where(m => m.is_male == item.is_male);
            }
            if (item.participant_type != null)
            {
                model = model.Where(m => m.participant_type == item.participant_type);
            }

            if (item.evaluation_form_version != null)
            {
                model = model.Where(m => m.evaluation_form_version == item.evaluation_form_version);
            }
            if (item.talakayan_name != null)
            {
                model = model.Where(m => m.talakayan_name == item.talakayan_name);
            }
            if (item.talakayan_venue != null)
            {
                model = model.Where(m => m.talakayan_venue == item.talakayan_venue);
            }
            if (item.talakayan_date_start != null)
            {
                model = model.Where(m => m.talakayan_date_start == item.talakayan_date_start);
            }
            if (item.talakayan_date_end != null)
            {
                model = model.Where(m => m.talakayan_date_end == item.talakayan_date_end);
            }
            if (item.evaluation_date_start != null)
            {
                model = model.Where(m => m.evaluation_date_start == item.evaluation_date_start);
            }
            if (item.evaluation_date_end != null)
            {
                model = model.Where(m => m.evaluation_date_end == item.evaluation_date_end);
            }
            if (item.talakayan_yr_id != null)
            {
                model = model.Where(m => m.talakayan_yr_id == item.talakayan_yr_id);
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

            //v3.0 additional filter
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

        [HttpPost]
        [Route("api/offline/v1/talakayan_eval/get_dto")]
        public PagedCollection<dynamic> GetAll(AngularFilterModel item)
        {
            var model = GetData(item);            
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;
            
            //THIS PART IS FOR RETRIEVING RECORDS TO BE DISPLAYED ON THE LIST
            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                //TotalSync = model.Where(x => x.is_deleted != true && (x.push_status_id == 2 || x.push_status_id == 3)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model.OrderBy(x => x.talakayan_date_start)
                .Select(x => new
                {
                    talakayan_evaluation_id = x.talakayan_evaluation_id,
                    talakayan_date_start = x.talakayan_date_start,
                    evaluation_date_start = x.evaluation_date_start,
                    evaluation_form_version = x.evaluation_form_version,
                    talakayan_yr_id = x.talakayan_yr_id,
                    person_name = x.person_name,
                    is_male = x.is_male,
                    lib_brgy_brgy_name = x.lib_brgy.brgy_name,
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
        [Route("api/offline/v1/talakayan_eval/get_recently_edited")]
        public PagedCollection<dynamic> GetRecentlyEdited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;
            
            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                    .OrderBy(x => x.talakayan_date_start)
                    .Select(x => new
                    {
                        talakayan_evaluation_id = x.talakayan_evaluation_id,
                        talakayan_date_start = x.talakayan_date_start,
                        evaluation_date_start = x.evaluation_date_start,
                        evaluation_form_version = x.evaluation_form_version,
                        talakayan_yr_id = x.talakayan_yr_id,
                        person_name = x.person_name,
                        is_male = x.is_male,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
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
        [Route("api/offline/v1/talakayan_eval/get_recently_added")]
        public PagedCollection<dynamic> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                    .OrderBy(x => x.talakayan_date_start)
                    .Select(x => new
                    {
                        talakayan_evaluation_id = x.talakayan_evaluation_id,
                        talakayan_date_start = x.talakayan_date_start,
                        evaluation_date_start = x.evaluation_date_start,
                        evaluation_form_version = x.evaluation_form_version,
                        talakayan_yr_id = x.talakayan_yr_id,
                        person_name = x.person_name,
                        is_male = x.is_male,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
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
        [Route("api/offline/v1/talakayan_eval/get_recently_edited_and_added")]
        public PagedCollection<dynamic> GetRecentlyEditedandAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                    .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                    .OrderBy(x => x.talakayan_date_start)
                    .Select(x => new
                    {
                        talakayan_evaluation_id = x.talakayan_evaluation_id,
                        talakayan_date_start = x.talakayan_date_start,
                        evaluation_date_start = x.evaluation_date_start,
                        evaluation_form_version = x.evaluation_form_version,
                        talakayan_yr_id = x.talakayan_yr_id,
                        person_name = x.person_name,
                        is_male = x.is_male,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                        last_modified_date = x.last_modified_date

                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        #region For REPORT

        public class queriedEvaluationReport<T>
        {
            public DateTime? minDate { get; set; }
            public DateTime? maxDate { get; set; }
            public IEnumerable<T> Items { get; set; }
        }

        public class report_1
        {
            public int evaluation_form_version { get; set; }

            public string city_name { get; set; }

            public int total_respondents { get; set; }
            public int total_male { get; set; }
            public int total_female { get; set; }

            public int total_localofficial_elected { get; set; }
            public int total_localofficial_appointed { get; set; }
            public int total_volunteer { get; set; }
            public int total_nonvolunteer { get; set; }
            public int total_citizens_grp_a { get; set; }
            public int total_citizens_grp_b { get; set; }

            //2015 version - I. Objectives of the Municipal Talakayan (1.1 Were the objectives fuly explained?)
            public int v2015_obj_a_total_yes { get; set; }
            public int v2015_obj_a_total_no { get; set; }
            //2015 version - I. Objectives of the Municipal Talakayan (1.2 To what extent were the objectives attained?)
            public int v2015_obj_b_total_fully { get; set; }
            public int v2015_obj_b_total_partially { get; set; }
            public int v2015_obj_b_total_hardly { get; set; }

            //2015 version - II. Time Allotment (2.1 Was time alloted to the different activities sufficient?)
            public int v2015_time_a_part1_total_yes   { get; set; }
            public int v2015_time_a_part1_total_no    { get; set; }
            public int v2015_time_a_part2_total_yes { get; set; }
            public int v2015_time_a_part2_total_no { get; set; }
            public int v2015_time_a_part3_total_yes { get; set; }
            public int v2015_time_a_part3_total_no { get; set; }
            public int v2015_time_a_part4_total_yes { get; set; }
            public int v2015_time_a_part4_total_no { get; set; }
            public int v2015_time_a_part5_total_yes { get; set; }
            public int v2015_time_a_part5_total_no { get; set; }
            //2015 version - II. Time Allotment (2.2 Which part(s) would you have given more/less time?)
            public int v2015_time_b_part1_total_more { get; set; }
            public int v2015_time_b_part1_total_less { get; set; }
            public int v2015_time_b_part2_total_more { get; set; }
            public int v2015_time_b_part2_total_less { get; set; }
            public int v2015_time_b_part3_total_more { get; set; }
            public int v2015_time_b_part3_total_less { get; set; }
            public int v2015_time_b_part4_total_more { get; set; }
            public int v2015_time_b_part4_total_less { get; set; }
            public int v2015_time_b_part5_total_more { get; set; }
            public int v2015_time_b_part5_total_less { get; set; }

            //2015 version - III.Parts of the Municipal Talakayan (3.1 In your opinion, did the following parts contributed to meeting the objectives of the Municipal Talakayan?)
            public int v2015_partsoftalakayan_part1_total_yes { get; set; }
            public int v2015_partsoftalakayan_part1_total_no { get; set; }
            public int v2015_partsoftalakayan_part2_total_yes { get; set; }
            public int v2015_partsoftalakayan_part2_total_no { get; set; }
            public int v2015_partsoftalakayan_part3_total_yes { get; set; }
            public int v2015_partsoftalakayan_part3_total_no { get; set; }
            public int v2015_partsoftalakayan_part4_total_yes { get; set; }
            public int v2015_partsoftalakayan_part4_total_no { get; set; }
            public int v2015_partsoftalakayan_part5_total_yes { get; set; }
            public int v2015_partsoftalakayan_part5_total_no { get; set; }

            //2015 version - IV. Evaluation of the team that conducted the Municipal Talakayan (4.1 Please evaluate the whole teams' effectiveness in the conduct of the different parts of the Municipal Talakayan)
            public int v2015_evaluation_total_excellent { get; set; }
            public int v2015_evaluation_total_verygood { get; set; }
            public int v2015_evaluation_total_good { get; set; }
            public int v2015_evaluation_total_poor { get; set; }
            public int v2015_evaluation_total_verypoor { get; set; }

            //2015 version: - V. Venue and Visual Aids (5.1 Venue is…)
            public int v2015_venue_a_total_yes { get; set; }
            public int v2015_venue_a_total_no { get; set; }
            public int v2015_venue_b_total_yes { get; set; }
            public int v2015_venue_b_total_no { get; set; }
            public int v2015_venue_c_total_yes { get; set; }
            public int v2015_venue_c_total_no { get; set; }
            public int v2015_venue_d_total_yes { get; set; }
            public int v2015_venue_d_total_no { get; set; }
            public int v2015_venue_e_total_yes { get; set; }
            public int v2015_venue_e_total_no { get; set; }
            public int v2015_visual_a_total_yes { get; set; }
            public int v2015_visual_a_total_no { get; set; }
            public int v2015_visual_b_total_yes { get; set; }
            public int v2015_visual_b_total_no { get; set; }
            public int v2015_visual_c_total_yes { get; set; }
            public int v2015_visual_c_total_no { get; set; }
            public int v2015_visual_d_total_yes { get; set; }
            public int v2015_visual_d_total_no { get; set; }
            public int v2015_visual_e_total_yes { get; set; }
            public int v2015_visual_e_total_no { get; set; }

            //2015 version - VI. Level of satisfaction with the different aspects of the Municipal Talakayan 
            //(A. Knowledge-gained)
            public int v2015_satisfaction_a_total_verysatisfied { get; set; }
            public int v2015_satisfaction_a_total_satisfied { get; set; }
            public int v2015_satisfaction_a_total_dissatisfied { get; set; }
            public int v2015_satisfaction_a_total_verydissatisfied { get; set; }
            //(B. Overall content)
            public int v2015_satisfaction_b_total_verysatisfied { get; set; }
            public int v2015_satisfaction_b_total_satisfied { get; set; }
            public int v2015_satisfaction_b_total_dissatisfied { get; set; }
            public int v2015_satisfaction_b_total_verydissatisfied { get; set; }
            //(C. Competency of Facilitators)
            public int v2015_satisfaction_c_total_verysatisfied { get; set; }
            public int v2015_satisfaction_c_total_satisfied { get; set; }
            public int v2015_satisfaction_c_total_dissatisfied { get; set; }
            public int v2015_satisfaction_c_total_verydissatisfied { get; set; }
            //(D. Schedule of different parts of the Municipal Talakayan)
            public int v2015_satisfaction_d_total_verysatisfied { get; set; }
            public int v2015_satisfaction_d_total_satisfied { get; set; }
            public int v2015_satisfaction_d_total_dissatisfied { get; set; }
            public int v2015_satisfaction_d_total_verydissatisfied { get; set; }
            //(E. Part 1: Presentation of the Municipal Profile and Development Status of the Municipality)
            public int v2015_satisfaction_e_total_verysatisfied { get; set; }
            public int v2015_satisfaction_e_total_satisfied { get; set; }
            public int v2015_satisfaction_e_total_dissatisfied { get; set; }
            public int v2015_satisfaction_e_total_verydissatisfied { get; set; }
            //(F. Part 2: Presentation of Summary of Needs, Interventions (including KC), and Gaps)
            public int v2015_satisfaction_f_total_verysatisfied { get; set; }
            public int v2015_satisfaction_f_total_satisfied { get; set; }
            public int v2015_satisfaction_f_total_dissatisfied { get; set; }
            public int v2015_satisfaction_f_total_verydissatisfied { get; set; }
            //(G. Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year)
            public int v2015_satisfaction_g_total_verysatisfied { get; set; }
            public int v2015_satisfaction_g_total_satisfied { get; set; }
            public int v2015_satisfaction_g_total_dissatisfied { get; set; }
            public int v2015_satisfaction_g_total_verydissatisfied { get; set; }
            //(H. Part 4: Gallery Walk)
            public int v2015_satisfaction_h_total_verysatisfied { get; set; }
            public int v2015_satisfaction_h_total_satisfied { get; set; }
            public int v2015_satisfaction_h_total_dissatisfied { get; set; }
            public int v2015_satisfaction_h_total_verydissatisfied { get; set; }
            //(I. Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan)
            public int v2015_satisfaction_i_total_verysatisfied { get; set; }
            public int v2015_satisfaction_i_total_satisfied { get; set; }
            public int v2015_satisfaction_i_total_dissatisfied { get; set; }
            public int v2015_satisfaction_i_total_verydissatisfied { get; set; }
            //(J. Venue)
            public int v2015_satisfaction_j_total_verysatisfied { get; set; }
            public int v2015_satisfaction_j_total_satisfied { get; set; }
            public int v2015_satisfaction_j_total_dissatisfied { get; set; }
            public int v2015_satisfaction_j_total_verydissatisfied { get; set; }
            //(K. Overall satisfaction on the Municipal Talakayan)
            public int v2015_satisfaction_k_total_verysatisfied { get; set; }
            public int v2015_satisfaction_k_total_satisfied { get; set; }
            public int v2015_satisfaction_k_total_dissatisfied { get; set; }
            public int v2015_satisfaction_k_total_verydissatisfied { get; set; }

            //2016 version:

            //1. Were the objectives fully explained?
            public int v2016_obj_total_yes { get; set; }
            public int v2016_obj_total_no { get; set; }

            //2. Was time alloted to the different activities sufficient?
            public int v2016_time_alloted_total_yes { get; set; }
            public int v2016_time_alloted_total_no { get; set; }

            //3. Venue
            //a. Clean
            public int v2016_venue_a_total_yes { get; set; }
            public int v2016_venue_a_total_no { get; set; }
            //b. Well-ventilated
            public int v2016_venue_b_total_yes { get; set; }
            public int v2016_venue_b_total_no { get; set; }
            //c. Spacious
            public int v2016_venue_c_total_yes { get; set; }
            public int v2016_venue_c_total_no { get; set; }
            //d. Well-lighted
            public int v2016_venue_d_total_yes { get; set; }
            public int v2016_venue_d_total_no { get; set; }

            //4. Equipped with good sound system
            public int v2016_sound_system_total_yes { get; set; }
            public int v2016_sound_system_total_no { get; set; }

            //5. Visual Aids in the presentation are:
            //a. Easily read from where I am seated
            public int v2016_visual_a_total_yes { get; set; }
            public int v2016_visual_a_total_no { get; set; }
            //b. Attractive
            public int v2016_visual_b_total_yes { get; set; }
            public int v2016_visual_b_total_no { get; set; }
            //c. Easy to understand
            public int v2016_visual_c_total_yes { get; set; }
            public int v2016_visual_c_total_no { get; set; }

            //6. Meals
            //a. Delicious
            public int v2016_meals_a_total_yes { get; set; }
            public int v2016_meals_a_total_no { get; set; }
            //b. Sufficient
            public int v2016_meals_b_total_yes { get; set; }
            public int v2016_meals_b_total_no { get; set; }
            //c. Clean
            public int v2016_meals_c_total_yes { get; set; }
            public int v2016_meals_c_total_no { get; set; }

            //7. Did you like the gallery walk?
            public int v2016_gallery_walk_total_yes { get; set; }
            public int v2016_gallery_walk_total_no { get; set; }

            //8. Did you like the Focus Group Discussion?
            public int v2016_fgd_total_yes { get; set; }
            public int v2016_fgd_total_no { get; set; }

            //KNOWLEDGE-GAINED
            //Part 1: Presentation of the Municipal Profile and Development Status of the Municipality
            public int v2016_knowledge_part1_total_verysatisfied { get; set; }
            public int v2016_knowledge_part1_total_satisfied { get; set; }
            public int v2016_knowledge_part1_total_dissatisfied { get; set; }
            public int v2016_knowledge_part1_total_verydissatisfied { get; set; }
            //Part 2: Presentation of the Municipal Development Priorities, Interventions and Gaps
            public int v2016_knowledge_part2_total_verysatisfied { get; set; }
            public int v2016_knowledge_part2_total_satisfied { get; set; }
            public int v2016_knowledge_part2_total_dissatisfied { get; set; }
            public int v2016_knowledge_part2_total_verydissatisfied { get; set; }
            //Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year
            public int v2016_knowledge_part3_total_verysatisfied { get; set; }
            public int v2016_knowledge_part3_total_satisfied { get; set; }
            public int v2016_knowledge_part3_total_dissatisfied { get; set; }
            public int v2016_knowledge_part3_total_verydissatisfied { get; set; }
            //Part 4: Gallery Walk
            public int v2016_knowledge_part4_total_verysatisfied { get; set; }
            public int v2016_knowledge_part4_total_satisfied { get; set; }
            public int v2016_knowledge_part4_total_dissatisfied { get; set; }
            public int v2016_knowledge_part4_total_verydissatisfied { get; set; }
            //Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan
            public int v2016_knowledge_part5_total_verysatisfied { get; set; }
            public int v2016_knowledge_part5_total_satisfied { get; set; }
            public int v2016_knowledge_part5_total_dissatisfied { get; set; }
            public int v2016_knowledge_part5_total_verydissatisfied { get; set; }

            //OVERALL SATISFACTION ON THE CONDUCT OF MUNICIPAL TALAKAYAN
            public int v2016_overall_satisfaction_total_verysatisfied { get; set; }
            public int v2016_overall_satisfaction_total_satisfied { get; set; }
            public int v2016_overall_satisfaction_total_dissatisfied { get; set; }
            public int v2016_overall_satisfaction_total_verydissatisfied { get; set; }

            //PLEASE EVALUATE THE WHOLE TEAM'S EFFECTIVENESS IN THE CONDUCT OF THE DIFFERENT PARTS OF THE MUNICIPAL TALAKAYAN
            public int v2016_team_effectiveness_total_excellent { get; set; }
            public int v2016_team_effectiveness_total_verygood { get; set; }
            public int v2016_team_effectiveness_total_good { get; set; }
            public int v2016_team_effectiveness_total_poor { get; set; }
            public int v2016_team_effectiveness_total_verypoor { get; set; }


        }

        #region Report 1, 2015
        [HttpPost]
        [Route("api/export/evaluation/evaluation_report_1_2015")]
        public IActionResult export_report_1_2015(AngularFilterModel item)
        {
            var model = GetData(item);
            model = model.Where(x => x.evaluation_form_version == 2015);

            var result = model.GroupBy(x => new { city_name = x.lib_city.city_name }).
                Select(x => new report_1
                {
                    city_name = x.Key.city_name,
                    total_respondents = x.Count(),
                    total_male = x.Count(c => c.is_male == true),
                    total_female = x.Count(c => c.is_male != true),

                    total_localofficial_elected = x.Count(c => c.participant_type == 1),
                    total_localofficial_appointed = x.Count(c => c.participant_type == 2),
                    total_volunteer = x.Count(c => c.participant_type == 3),
                    total_nonvolunteer = x.Count(c => c.participant_type == 4),

                    v2015_obj_a_total_yes = x.Count(c => c.v2015_obj_a == 1),
                    v2015_obj_a_total_no = x.Count(c => c.v2015_obj_a == 2),
                    v2015_obj_b_total_fully = x.Count(c => c.v2015_obj_b == 1),
                    v2015_obj_b_total_partially = x.Count(c => c.v2015_obj_b == 2),
                    v2015_obj_b_total_hardly = x.Count(c => c.v2015_obj_b == 3),

                    v2015_time_a_part1_total_yes = x.Count(c => c.v2015_timeallotment_a_part1 == 1),
                    v2015_time_a_part1_total_no = x.Count(c => c.v2015_timeallotment_a_part1 == 2),                    
                    v2015_time_a_part2_total_yes = x.Count(c => c.v2015_timeallotment_a_part2 ==1),
                    v2015_time_a_part2_total_no = x.Count(c => c.v2015_timeallotment_a_part2 == 2),
                    v2015_time_a_part3_total_yes = x.Count(c => c.v2015_timeallotment_a_part3 == 1),
                    v2015_time_a_part3_total_no = x.Count(c => c.v2015_timeallotment_a_part3 == 2),
                    v2015_time_a_part4_total_yes = x.Count(c => c.v2015_timeallotment_a_part4 == 1),
                    v2015_time_a_part4_total_no = x.Count(c => c.v2015_timeallotment_a_part4 == 2),
                    v2015_time_a_part5_total_yes = x.Count(c => c.v2015_timeallotment_a_part5 == 1),
                    v2015_time_a_part5_total_no = x.Count(c => c.v2015_timeallotment_a_part5 == 2),
                    v2015_time_b_part1_total_more = x.Count(c => c.v2015_timeallotment_b_part1 == 1),
                    v2015_time_b_part1_total_less = x.Count(c => c.v2015_timeallotment_b_part1 == 2),
                    v2015_time_b_part2_total_more = x.Count(c => c.v2015_timeallotment_b_part2 == 1),
                    v2015_time_b_part2_total_less = x.Count(c => c.v2015_timeallotment_b_part2 == 2),
                    v2015_time_b_part3_total_more = x.Count(c => c.v2015_timeallotment_b_part3 == 1),
                    v2015_time_b_part3_total_less = x.Count(c => c.v2015_timeallotment_b_part3 == 2),
                    v2015_time_b_part4_total_more = x.Count(c => c.v2015_timeallotment_b_part4 == 1),
                    v2015_time_b_part4_total_less = x.Count(c => c.v2015_timeallotment_b_part4 == 2),
                    v2015_time_b_part5_total_more = x.Count(c => c.v2015_timeallotment_b_part5 == 1),
                    v2015_time_b_part5_total_less = x.Count(c => c.v2015_timeallotment_b_part5 == 2),

                    v2015_partsoftalakayan_part1_total_yes = x.Count(c => c.v2015_partsoftalakayan_part1 == 1),
                    v2015_partsoftalakayan_part1_total_no = x.Count(c => c.v2015_partsoftalakayan_part1 == 2),
                    v2015_partsoftalakayan_part2_total_yes = x.Count(c => c.v2015_partsoftalakayan_part2 == 1),
                    v2015_partsoftalakayan_part2_total_no = x.Count(c => c.v2015_partsoftalakayan_part2 == 2),
                    v2015_partsoftalakayan_part3_total_yes = x.Count(c => c.v2015_partsoftalakayan_part3 == 1),
                    v2015_partsoftalakayan_part3_total_no = x.Count(c => c.v2015_partsoftalakayan_part3 == 2),
                    v2015_partsoftalakayan_part4_total_yes = x.Count(c => c.v2015_partsoftalakayan_part4 == 1),
                    v2015_partsoftalakayan_part4_total_no = x.Count(c => c.v2015_partsoftalakayan_part4 == 2),
                    v2015_partsoftalakayan_part5_total_yes = x.Count(c => c.v2015_partsoftalakayan_part5 == 1),
                    v2015_partsoftalakayan_part5_total_no = x.Count(c => c.v2015_partsoftalakayan_part5 == 2),

                    v2015_evaluation_total_excellent = x.Count(c => c.v2015_evaluation_a == 5),
                    v2015_evaluation_total_verygood = x.Count(c => c.v2015_evaluation_a == 4),
                    v2015_evaluation_total_good = x.Count(c => c.v2015_evaluation_a == 3),
                    v2015_evaluation_total_poor = x.Count(c => c.v2015_evaluation_a == 2),
                    v2015_evaluation_total_verypoor = x.Count(c => c.v2015_evaluation_a == 1),

                    //2015 version: - V. Venue and Visual Aids (5.1 Venue is…)
                    v2015_venue_a_total_yes = x.Count(c => c.v2015_venue_a == 1),
                    v2015_venue_a_total_no = x.Count(c => c.v2015_venue_a == 2),
                    v2015_venue_b_total_yes = x.Count(c => c.v2015_venue_b == 1),
                    v2015_venue_b_total_no = x.Count(c => c.v2015_venue_b == 2),
                    v2015_venue_c_total_yes = x.Count(c => c.v2015_venue_c == 1),
                    v2015_venue_c_total_no = x.Count(c => c.v2015_venue_c == 2),
                    v2015_venue_d_total_yes = x.Count(c => c.v2015_venue_d == 1),
                    v2015_venue_d_total_no = x.Count(c => c.v2015_venue_d == 2),
                    v2015_venue_e_total_yes = x.Count(c => c.v2015_venue_e == 1),
                    v2015_venue_e_total_no = x.Count(c => c.v2015_venue_e == 2),
                    v2015_visual_a_total_yes = x.Count(c => c.v2015_visual_a == 1),
                    v2015_visual_a_total_no = x.Count(c => c.v2015_visual_a == 2),
                    v2015_visual_b_total_yes = x.Count(c => c.v2015_visual_b == 1),
                    v2015_visual_b_total_no = x.Count(c => c.v2015_visual_b == 2),
                    v2015_visual_c_total_yes = x.Count(c => c.v2015_visual_c == 1),
                    v2015_visual_c_total_no = x.Count(c => c.v2015_visual_c == 2),
                    v2015_visual_d_total_yes = x.Count(c => c.v2015_visual_d == 1),
                    v2015_visual_d_total_no = x.Count(c => c.v2015_visual_d == 2),
                    v2015_visual_e_total_yes = x.Count(c => c.v2015_visual_e == 1),
                    v2015_visual_e_total_no = x.Count(c => c.v2015_visual_e == 2),

                    //2015 version - VI. Level of satisfaction with the different aspects of the Municipal Talakayan 
                    //(A. Knowledge-gained)
                    v2015_satisfaction_a_total_verysatisfied = x.Count(c => c.v2015_satisfaction_a == 4),
                    v2015_satisfaction_a_total_satisfied = x.Count(c => c.v2015_satisfaction_a == 3),
                    v2015_satisfaction_a_total_dissatisfied = x.Count(c => c.v2015_satisfaction_a == 2),
                    v2015_satisfaction_a_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_a == 1),
                    //(B. Overall content)
                    v2015_satisfaction_b_total_verysatisfied = x.Count(c => c.v2015_satisfaction_b == 4),
                    v2015_satisfaction_b_total_satisfied = x.Count(c => c.v2015_satisfaction_b == 3),
                    v2015_satisfaction_b_total_dissatisfied = x.Count(c => c.v2015_satisfaction_b == 2),
                    v2015_satisfaction_b_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_b == 1),
                    //(C. Competency of Facilitators)
                    v2015_satisfaction_c_total_verysatisfied = x.Count(c => c.v2015_satisfaction_c == 4),
                    v2015_satisfaction_c_total_satisfied = x.Count(c => c.v2015_satisfaction_c == 3),
                    v2015_satisfaction_c_total_dissatisfied = x.Count(c => c.v2015_satisfaction_c == 2),
                    v2015_satisfaction_c_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_c == 1),
                    //(D. Schedule of different parts of the Municipal Talakayan)
                    v2015_satisfaction_d_total_verysatisfied = x.Count(c => c.v2015_satisfaction_d == 4),
                    v2015_satisfaction_d_total_satisfied = x.Count(c => c.v2015_satisfaction_d == 3),
                    v2015_satisfaction_d_total_dissatisfied = x.Count(c => c.v2015_satisfaction_d == 2),
                    v2015_satisfaction_d_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_d == 1),
                    //(E. Part 1: Presentation of the Municipal Profile and Development Status of the Municipality)
                    v2015_satisfaction_e_total_verysatisfied = x.Count(c => c.v2015_satisfaction_e == 4),
                    v2015_satisfaction_e_total_satisfied = x.Count(c => c.v2015_satisfaction_e == 3),
                    v2015_satisfaction_e_total_dissatisfied = x.Count(c => c.v2015_satisfaction_e == 2),
                    v2015_satisfaction_e_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_e == 1),
                    //(F. Part 2: Presentation of Summary of Needs, Interventions (including KC), and Gaps)
                    v2015_satisfaction_f_total_verysatisfied = x.Count(c => c.v2015_satisfaction_f == 4),
                    v2015_satisfaction_f_total_satisfied = x.Count(c => c.v2015_satisfaction_f == 3),
                    v2015_satisfaction_f_total_dissatisfied = x.Count(c => c.v2015_satisfaction_f == 2),
                    v2015_satisfaction_f_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_f == 1),
                    //(G. Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year)
                    v2015_satisfaction_g_total_verysatisfied = x.Count(c => c.v2015_satisfaction_g == 4),
                    v2015_satisfaction_g_total_satisfied = x.Count(c => c.v2015_satisfaction_g == 3),
                    v2015_satisfaction_g_total_dissatisfied = x.Count(c => c.v2015_satisfaction_g == 2),
                    v2015_satisfaction_g_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_g == 1),
                    //(H. Part 4: Gallery Walk)
                    v2015_satisfaction_h_total_verysatisfied = x.Count(c => c.v2015_satisfaction_h == 4),
                    v2015_satisfaction_h_total_satisfied = x.Count(c => c.v2015_satisfaction_h == 3),
                    v2015_satisfaction_h_total_dissatisfied = x.Count(c => c.v2015_satisfaction_h == 2),
                    v2015_satisfaction_h_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_h == 1),
                    //(I. Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan)
                    v2015_satisfaction_i_total_verysatisfied = x.Count(c => c.v2015_satisfaction_i == 4),
                    v2015_satisfaction_i_total_satisfied = x.Count(c => c.v2015_satisfaction_i == 3),
                    v2015_satisfaction_i_total_dissatisfied = x.Count(c => c.v2015_satisfaction_i == 2),
                    v2015_satisfaction_i_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_i == 1),
                    //(J. Venue)
                    v2015_satisfaction_j_total_verysatisfied = x.Count(c => c.v2015_satisfaction_j == 4),
                    v2015_satisfaction_j_total_satisfied = x.Count(c => c.v2015_satisfaction_j == 3),
                    v2015_satisfaction_j_total_dissatisfied = x.Count(c => c.v2015_satisfaction_j == 2),
                    v2015_satisfaction_j_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_j == 1),
                    //(K. Overall satisfaction on the Municipal Talakayan)
                    v2015_satisfaction_k_total_verysatisfied = x.Count(c => c.v2015_satisfaction_k == 4),
                    v2015_satisfaction_k_total_satisfied = x.Count(c => c.v2015_satisfaction_k == 3),
                    v2015_satisfaction_k_total_dissatisfied = x.Count(c => c.v2015_satisfaction_k == 2),
                    v2015_satisfaction_k_total_verydissatisfied = x.Count(c => c.v2015_satisfaction_k == 1),
                    
                }).ToList();

            return Ok(new queriedEvaluationReport<report_1>()
            {
                Items = result,
            //    maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
              //  minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
            });

            //return Ok(report_list);
        }
        #endregion


        #region Report 1, 2016
        [HttpPost]
        [Route("api/export/evaluation/evaluation_report_1_2016")]
        public IActionResult export_report_1_2016(AngularFilterModel item)
        {
            var model = GetData(item);
            model = model.Where(x => x.evaluation_form_version == 2016);

            var result = model.GroupBy(x => new { city_name = x.lib_city.city_name }).
                Select(x => new report_1
                {
                    city_name = x.Key.city_name,
                    total_respondents = x.Count(),
                    total_male = x.Count(c => c.is_male == true),
                    total_female = x.Count(c => c.is_male != true),

                    total_localofficial_elected = x.Count(c => c.participant_type == 1),
                    total_localofficial_appointed = x.Count(c => c.participant_type == 2),
                    total_citizens_grp_a = x.Count(c => c.participant_type == 3),
                    total_citizens_grp_b = x.Count(c => c.participant_type == 4),

                    //1. Were the objectives fully explained?
                    v2016_obj_total_yes = x.Count(c => c.v2016_obj == 1),
                    v2016_obj_total_no = x.Count(c => c.v2016_obj == 2),

                    //2. Was time alloted to the different activities sufficient?
                    v2016_time_alloted_total_yes = x.Count(c => c.v2016_time_alloted == 1),
                    v2016_time_alloted_total_no = x.Count(c => c.v2016_time_alloted == 2),

                    //3. Venue
                    //a. Clean
                    v2016_venue_a_total_yes = x.Count(c => c.v2016_venue_a == 1),
                    v2016_venue_a_total_no = x.Count(c => c.v2016_venue_a == 2),
                    //b. Well-ventilated
                    v2016_venue_b_total_yes = x.Count(c => c.v2016_venue_b == 1),
                    v2016_venue_b_total_no = x.Count(c => c.v2016_venue_b == 2),
                    //c. Spacious
                    v2016_venue_c_total_yes = x.Count(c => c.v2016_venue_c == 1),
                    v2016_venue_c_total_no = x.Count(c => c.v2016_venue_c == 2),
                    //d. Well-lighted
                    v2016_venue_d_total_yes = x.Count(c => c.v2016_venue_d == 1),
                    v2016_venue_d_total_no = x.Count(c => c.v2016_venue_d == 2),

                    //4. Equipped with good sound system
                    v2016_sound_system_total_yes = x.Count(c => c.v2016_sound_system == 1),
                    v2016_sound_system_total_no = x.Count(c => c.v2016_sound_system == 2),

                    //5. Visual Aids in the presentation are:
                    //a. Easily read from where I am seated
                    v2016_visual_a_total_yes = x.Count(c => c.v2016_visual_a == 1),
                    v2016_visual_a_total_no = x.Count(c => c.v2016_visual_a == 2),
                    //b. Attractive
                    v2016_visual_b_total_yes = x.Count(c => c.v2016_visual_b == 1),
                    v2016_visual_b_total_no = x.Count(c => c.v2016_visual_b == 2),
                    //c. Easy to understand
                    v2016_visual_c_total_yes = x.Count(c => c.v2016_visual_c == 1),
                    v2016_visual_c_total_no = x.Count(c => c.v2016_visual_c == 2),

                    //6. Meals
                    //a. Delicious
                    v2016_meals_a_total_yes = x.Count(c => c.v2016_meals_a == 1),
                    v2016_meals_a_total_no = x.Count(c => c.v2016_meals_a == 2),
                    //b. Sufficient
                    v2016_meals_b_total_yes = x.Count(c => c.v2016_meals_b == 1),
                    v2016_meals_b_total_no = x.Count(c => c.v2016_meals_b == 2),
                    //c. Clean
                    v2016_meals_c_total_yes = x.Count(c => c.v2016_meals_c == 1),
                    v2016_meals_c_total_no = x.Count(c => c.v2016_meals_c == 2),

                    //7. Did you like the gallery walk?
                    v2016_gallery_walk_total_yes = x.Count(c => c.v2016_gallery_walk == 1),
                    v2016_gallery_walk_total_no = x.Count(c => c.v2016_gallery_walk == 2),

                    //8. Did you like the Focus Group Discussion?
                    v2016_fgd_total_yes = x.Count(c => c.v2016_fgd == 1),
                    v2016_fgd_total_no = x.Count(c => c.v2016_fgd == 2),

                    //KNOWLEDGE-GAINED
                    //Part 1: Presentation of the Municipal Profile and Development Status of the Municipality
                    v2016_knowledge_part1_total_verysatisfied = x.Count(c => c.v2016_knowledge_part1 == 4),
                    v2016_knowledge_part1_total_satisfied = x.Count(c => c.v2016_knowledge_part1 == 3),
                    v2016_knowledge_part1_total_dissatisfied = x.Count(c => c.v2016_knowledge_part1 == 2),
                    v2016_knowledge_part1_total_verydissatisfied = x.Count(c => c.v2016_knowledge_part1 == 1),
                    //Part 2: Presentation of the Municipal Development Priorities, Interventions and Gaps
                    v2016_knowledge_part2_total_verysatisfied = x.Count(c => c.v2016_knowledge_part2 == 4),
                    v2016_knowledge_part2_total_satisfied = x.Count(c => c.v2016_knowledge_part2 == 3),
                    v2016_knowledge_part2_total_dissatisfied = x.Count(c => c.v2016_knowledge_part2 == 2),
                    v2016_knowledge_part2_total_verydissatisfied = x.Count(c => c.v2016_knowledge_part2 == 1),
                    //Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year
                    v2016_knowledge_part3_total_verysatisfied = x.Count(c => c.v2016_knowledge_part3 == 4),
                    v2016_knowledge_part3_total_satisfied = x.Count(c => c.v2016_knowledge_part3 == 3),
                    v2016_knowledge_part3_total_dissatisfied = x.Count(c => c.v2016_knowledge_part3 == 2),
                    v2016_knowledge_part3_total_verydissatisfied = x.Count(c => c.v2016_knowledge_part3 == 1),
                    //Part 4: Gallery Walk
                    v2016_knowledge_part4_total_verysatisfied = x.Count(c => c.v2016_knowledge_part4 == 4),
                    v2016_knowledge_part4_total_satisfied = x.Count(c => c.v2016_knowledge_part4 == 3),
                    v2016_knowledge_part4_total_dissatisfied = x.Count(c => c.v2016_knowledge_part4 == 2),
                    v2016_knowledge_part4_total_verydissatisfied = x.Count(c => c.v2016_knowledge_part4 == 1),
                    //Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan
                    v2016_knowledge_part5_total_verysatisfied = x.Count(c => c.v2016_knowledge_part5 == 4),
                    v2016_knowledge_part5_total_satisfied = x.Count(c => c.v2016_knowledge_part5 == 3),
                    v2016_knowledge_part5_total_dissatisfied = x.Count(c => c.v2016_knowledge_part5 == 2),
                    v2016_knowledge_part5_total_verydissatisfied = x.Count(c => c.v2016_knowledge_part5 == 1),

                    //OVERALL SATISFACTION ON THE CONDUCT OF MUNICIPAL TALAKAYAN
                    v2016_overall_satisfaction_total_verysatisfied = x.Count(c => c.v2016_overall_satisfaction == 4),
                    v2016_overall_satisfaction_total_satisfied = x.Count(c => c.v2016_overall_satisfaction == 3),
                    v2016_overall_satisfaction_total_dissatisfied = x.Count(c => c.v2016_overall_satisfaction == 2),
                    v2016_overall_satisfaction_total_verydissatisfied = x.Count(c => c.v2016_overall_satisfaction == 1),

                    //PLEASE EVALUATE THE WHOLE TEAM'S EFFECTIVENESS IN THE CONDUCT OF THE DIFFERENT PARTS OF THE MUNICIPAL TALAKAYAN
                    v2016_team_effectiveness_total_excellent = x.Count(c => c.v2016_team_effectiveness == 5),
                    v2016_team_effectiveness_total_verygood = x.Count(c => c.v2016_team_effectiveness == 4),
                    v2016_team_effectiveness_total_good = x.Count(c => c.v2016_team_effectiveness == 3),
                    v2016_team_effectiveness_total_poor = x.Count(c => c.v2016_team_effectiveness == 2),
                    v2016_team_effectiveness_total_verypoor = x.Count(c => c.v2016_team_effectiveness == 1),


                }).ToList();

            return Ok(new queriedEvaluationReport<report_1>()
            {
                Items = result,
                //    maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                //  minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
            });

            //return Ok(report_list);
        }
        #endregion

        public class report_2
        {
            public int evaluation_form_version { get; set; }

            public string region_name { get; set; }
            public string province_name { get; set; }
            public string city_name { get; set; }
            public int talakayan_year { get; set; }
            public DateTime? talakayan_date { get; set; }
            public string participant_name { get; set; }
            public bool? sex { get; set; }
            public int? participant_type { get; set; }
            public string comment { get; set; }
        }


        //09-08-2017
        //New Report for v3.0 : Comments encoded per participant
        [HttpPost]
        [Route("api/export/evaluation/evaluation_report_2_2016")]
        public IActionResult export_report_2_2016(AngularFilterModel item)
        {
            var model = GetData(item);
            model = model.Where(x => x.evaluation_form_version == 2016);

            var result = model.Select(x => new report_2
            {
                region_name = x.lib_region.region_name,
                province_name = x.lib_province.prov_name,
                city_name = x.lib_city.city_name,

                talakayan_year = x.talakayan_yr_id,
                talakayan_date = x.talakayan_date_start,
                participant_name = x.person_name == null ? "" : x.person_name,
                sex = x.is_male,
                participant_type = x.participant_type,

                comment = x.v2016_comments

                }).ToList();

            return Ok(new queriedEvaluationReport<report_2>()
            {
                Items = result
            });
        }



        #endregion










        //SAVE FUNCTION:
        [Route("api/offline/v1/talakayan_eval/save")]
        public async Task<IActionResult> Save(talakayan_eval model, bool? api)
        {
            var record = db.talakayan_eval.AsNoTracking().FirstOrDefault(x => x.talakayan_evaluation_id == model.talakayan_evaluation_id && x.is_deleted != true);

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

                db.talakayan_eval.Add(model);

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

        [Route("api/offline/v1/talakayan_eval/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.talakayan_eval.FirstOrDefault(x => x.talakayan_evaluation_id == id && x.is_deleted != true);



            if (model == null)
            {
                var item = new talakayan_eval();

                item.talakayan_evaluation_id = id;
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
        [Route("Sync/Get/talakayan_eval")]
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/talakayan_eval/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<talakayan_eval>>(responseJson.Result);
                    
                    foreach (var item in model.ToList())
                    {
                        await Save(item, true);
                    }
                    
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            
        }

        //[Route("api/offline/v1/talakayan_evaluation/post/undo_action")]
        //public async Task<IActionResult> RevertOldPushStatusID(Guid talakayan_evaluation_id, int push_status_id)
        //{
        //    var talakayan = db.talakayan_eval.Find(talakayan_evaluation_id);

        //    if (talakayan == null)
        //    {
        //        return BadRequest("Error");
        //    }

        //    talakayan.push_status_id = push_status_id;

        //    await db.SaveChangesAsync();
        //    return Ok();
        //}


        [Route("api/offline/v1/talakayan_evaluation/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid talakayan_evaluation_id, int push_status_id)
        {
            var talakayan = db.talakayan_eval.Find(talakayan_evaluation_id);

            if (talakayan == null)
            {
                return BadRequest("Error");
            }

            talakayan.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        //new:
        [HttpPost]
        [Route("Sync/Post/talakayan_eval")]
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

                var items_preselected = db.talakayan_eval.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.talakayan_eval.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/talakayan_eval/save", data).Result;
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
                            //return BadRequest(); 
                        }
                    }
                }
                else
                {
                    var items = db.talakayan_eval.Where(x => x.push_status_id == 5 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/talakayan_eval/save", data).Result;
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
                            //return BadRequest();
                        }
                    }
                }
            }
            return Ok();
        }
        

        #endregion

    }
}
