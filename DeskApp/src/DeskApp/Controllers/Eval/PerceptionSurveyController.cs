using DeskApp.Data;
using DeskApp.DataLayer;
using DeskApp.DataLayer.Eval;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.Controllers
{
    public class PerceptionSurveyController : Controller
    {
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public PerceptionSurveyController(ApplicationDbContext context)
        {
            db = context;

        }
        
        //api delete moved to DeleteController.cs 01-24-18

        public ActionResult Index()
        {
            return View();
        }
        
        public IActionResult Perception(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }


            ViewBag.id = id;

            return View();
        }


        private IQueryable<perception_survey> GetData(
                 DataLayer.AngularFilterModel item
           )
        {
            var model = db.perception_survey.Where(x => x.is_deleted != true).AsQueryable();

            if (!string.IsNullOrEmpty(item.name))
            {
                string converted_name = item.name.ToLower();
                model = model.Where(x => x.person_name.ToLower().Contains(converted_name));
            }
            if (item.age != null)
            {
                model = model.Where(m => m.age == item.age);
            }
            if (item.is_male != null)
            {
                model = model.Where(m => m.is_male == item.is_male);
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

            if (item.is_pantawid != null)
            {
                model = model.Where(m => m.is_pantawid == item.is_pantawid);
            }
            if (item.is_slp != null)
            {
                model = model.Where(m => m.is_slp == item.is_slp);
            }
            if (item.is_ip != null)
            {
                model = model.Where(m => m.is_ip == item.is_ip);
            }


            if (item.survey_date_from != null)
            {
                model = model.Where(m => m.survey_date_from == item.survey_date_from);
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
        [Route("api/offline/v1/perception_survey/get_dto")]
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
                //TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalSync = model.Count(x => x.is_deleted != true && (x.push_status_id == 2 || x.push_status_id == 3)),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                
                Items = model.OrderBy(x => x.talakayan_date_from)
                .Select(x => new
                {                    
                    perception_survey_id = x.perception_survey_id,
                    lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    age = x.age,
                    is_male = x.is_male,
                    person_name = x.person_name,
                    survey_date_from = x.survey_date_from,
                    year = x.year,   
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date                 

                }).Skip(currPages * size).Take(size).ToList(),
            };
        }

        //getting recently edited:
        [HttpPost]
        [Route("api/offline/v1/perception_survey/get_recently_edited")]
        public PagedCollection<dynamic> GetRecentlyEdited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();

            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            //retrieving records:
            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),

                //TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).OrderBy(x => x.talakayan_date_from)
                .Select(x => new
                {
                    perception_survey_id = x.perception_survey_id,
                    lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    age = x.age,
                    is_male = x.is_male,
                    person_name = x.person_name,
                    talakayan_date_from = x.talakayan_date_from,
                    talakayan_date_to = x.talakayan_date_to,
                    year = x.year,
                    activity_name = x.activity_name,
                    talakayan_yr_id = x.talakayan_yr_id,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date

                }).Skip(currPages * size).Take(size).ToList(),
            };
        }

        //getting recently added:
        [HttpPost]
        [Route("api/offline/v1/perception_survey/get_recently_added")]
        public PagedCollection<dynamic> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            //THIS PART IS FOR RETRIEVING RECORDS TO BE DISPLAYED ON THE LIST
            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).OrderBy(x => x.talakayan_date_from)
                .Select(x => new
                {
                    perception_survey_id = x.perception_survey_id,
                    lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    age = x.age,
                    is_male = x.is_male,
                    person_name = x.person_name,
                    talakayan_date_from = x.talakayan_date_from,
                    talakayan_date_to = x.talakayan_date_to,
                    year = x.year,
                    activity_name = x.activity_name,
                    talakayan_yr_id = x.talakayan_yr_id,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date

                }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/perception_survey/get_recently_edited_and_added")]
        public PagedCollection<dynamic> GetRecentlyEditedandAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            //THIS PART IS FOR RETRIEVING RECORDS TO BE DISPLAYED ON THE LIST
            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                    .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                    .OrderBy(x => x.talakayan_date_from)
                    .Select(x => new
                    {
                        perception_survey_id = x.perception_survey_id,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        age = x.age,
                        is_male = x.is_male,
                        person_name = x.person_name,
                        talakayan_date_from = x.talakayan_date_from,
                        talakayan_date_to = x.talakayan_date_to,
                        year = x.year,
                        activity_name = x.activity_name,
                        talakayan_yr_id = x.talakayan_yr_id,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                        last_modified_date = x.last_modified_date

                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }

        //-----------------------------------GETTTING  DATA TO EXPORT------------------------------------------------//

        public class queriedTalakayanReport<T>
        {
            public DateTime? minDate { get; set; }
            public DateTime? maxDate { get; set; }
            public IEnumerable<T> Items { get; set; }
            public int total_male { get; set; }
            public int total_female { get; set; }
            public int total_respondents { get; set; }
            public int total_pantawid { get; set; }
            public int total_ip { get; set; }
            public int total_slp { get; set; }
        }

        public class report_1
        {
            public int year { get; set; }
            public string item { get; set; }
            public string category { get; set; }

            public int strongly_disagree_male { get; set; }
            public int strongly_disagree_female { get; set; }
            public int total_strongly_disagree { get; set; }

            public int disagree_male { get; set; }
            public int disagree_female { get; set; }
            public int total_disagree { get; set; }

            public int agree_male { get; set; }
            public int agree_female { get; set; }
            public int total_agree { get; set; }

            public int strongly_agree_male { get; set; }
            public int strongly_agree_female { get; set; }
            public int total_strongly_agree { get; set; }

            public int no_answer_male { get; set; }
            public int no_answer_female { get; set; }
            public int total_no_answer { get; set; }

            public string percent_strongly_disagree_male { get; set; }
            public string percent_strongly_disagree_female { get; set; }

            public string percent_disagree_male { get; set; }
            public string percent_disagree_female { get; set; }

            public string percent_agree_male { get; set; }
            public string percent_agree_female { get; set; }

            public string percent_strongly_agree_male { get; set; }
            public string percent_strongly_agree_female { get; set; }

            public string percent_no_answer_male { get; set; }
            public string percent_no_answer_female { get; set; }

            public DateTime earliest_talakayan_date { get; set; }
            public DateTime latest_talakayan_date { get; set; }

        }

        #region Report 1 year 2015
        [HttpPost]
        [Route("api/export/perception/report_1_2015")]
        public IActionResult export_report_1_2015(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_1>();

            model = model.Where(x => x.year == 2015);

            //var total_male = model.Count(c => c.is_male == true);
            //var total_female = model.Count(c => c.is_male != true);
            //var total_respondents = total_male + total_female;

            //var total = new report_1
            //{
            //    year = 2015,
            //    total_male = model.Count(c => c.is_male == true),
            //    total_female = model.Count(c => c.is_male != true),
            //    total_respondents = model.Count(c => c.is_male == true) + model.Count(c => c.is_male != true),
            //};

            var trust_2 = new report_1
            {
                item = "The community trusts the local officials in planning, implementation, monitoring and reporting of programs and projects for the community",
                year = 2015,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_2 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_2 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_2 == 1),

                disagree_male = model.Count(c => c.trust_2 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_2 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_2 == 2),

                agree_male = model.Count(c => c.trust_2 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_2 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_2 == 3),

                strongly_agree_male = model.Count(c => c.trust_2 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_2 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_2 == 4),

                no_answer_male = model.Count(c => c.trust_2 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_2 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_2 == 0),
            };
            var trust_4 = new report_1
            {
                item = "Local officials are fair in dealing with the people",
                year = 2015,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_4 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_4 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_4 == 1),

                disagree_male = model.Count(c => c.trust_4 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_4 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_4 == 2),

                agree_male = model.Count(c => c.trust_4 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_4 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_4 == 3),

                strongly_agree_male = model.Count(c => c.trust_4 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_4 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_4 == 4),

                no_answer_male = model.Count(c => c.trust_4 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_4 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_4 == 0),
            };

            var trust_5 = new report_1
            {
                item = "Barangay officials regularly meet with the community",
                year = 2015,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_5 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_5 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_5 == 1),

                disagree_male = model.Count(c => c.trust_5 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_5 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_5 == 2),

                agree_male = model.Count(c => c.trust_5 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_5 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_5 == 3),

                strongly_agree_male = model.Count(c => c.trust_5 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_5 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_5 == 4),

                no_answer_male = model.Count(c => c.trust_5 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_5 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_5 == 0),
            };

            var trust_6 = new report_1
            {
                item = "Municipal officials regularly meet with the community",
                year = 2015,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_6 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_6 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_6 == 1),

                disagree_male = model.Count(c => c.trust_6 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_6 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_6 == 2),

                agree_male = model.Count(c => c.trust_6 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_6 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_6 == 3),

                strongly_agree_male = model.Count(c => c.trust_6 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_6 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_6 == 4),

                no_answer_male = model.Count(c => c.trust_6 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_6 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_6 == 0),
            };
            var trust_7 = new report_1
            {
                item = "I trust the local officials in planning, implementation, monitoring and reporting of programs and projects for the barangay",
                year = 2015,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_7 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_7 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_7 == 1),

                disagree_male = model.Count(c => c.trust_7 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_7 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_7 == 2),

                agree_male = model.Count(c => c.trust_7 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_7 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_7 == 3),

                strongly_agree_male = model.Count(c => c.trust_7 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_7 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_7 == 4),

                no_answer_male = model.Count(c => c.trust_7 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_7 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_7 == 0),
            };

            var access_1 = new report_1
            {
                item = "There is an adequate number of health facilities in our barangay",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_1 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_1 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_1 == 1),

                disagree_male = model.Count(c => c.access_1 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_1 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_1 == 2),

                agree_male = model.Count(c => c.access_1 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_1 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_1 == 3),

                strongly_agree_male = model.Count(c => c.access_1 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_1 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_1 == 4),

                no_answer_male = model.Count(c => c.access_1 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_1 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_1 == 0),
            };

            var access_3 = new report_1
            {
                item = "There is adequate number of educational facilities",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_3 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_3 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_3 == 1),

                disagree_male = model.Count(c => c.access_3 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_3 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_3 == 2),

                agree_male = model.Count(c => c.access_3 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_3 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_3 == 3),

                strongly_agree_male = model.Count(c => c.access_3 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_3 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_3 == 4),

                no_answer_male = model.Count(c => c.access_3 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_3 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_3 == 0),
            };

            var access_5 = new report_1
            {
                item = "There is adequate number of roads accessed by public vehicles (e.g. transportation of public goods)",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_5 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_5 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_5 == 1),

                disagree_male = model.Count(c => c.access_5 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_5 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_5 == 2),

                agree_male = model.Count(c => c.access_5 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_5 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_5 == 3),

                strongly_agree_male = model.Count(c => c.access_5 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_5 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_5 == 4),

                no_answer_male = model.Count(c => c.access_5 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_5 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_5 == 0),
            };

            var access_7 = new report_1
            {
                item = "Potable water is accessible to the community",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_7 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_7 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_7 == 1),

                disagree_male = model.Count(c => c.access_7 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_7 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_7 == 2),

                agree_male = model.Count(c => c.access_7 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_7 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_7 == 3),

                strongly_agree_male = model.Count(c => c.access_7 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_7 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_7 == 4),

                no_answer_male = model.Count(c => c.access_7 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_7 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_7 == 0),
            };

            var access_8 = new report_1
            {
                item = "There is peace and order in the community",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_8 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_8 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_8 == 1),

                disagree_male = model.Count(c => c.access_8 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_8 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_8 == 2),

                agree_male = model.Count(c => c.access_8 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_8 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_8 == 3),

                strongly_agree_male = model.Count(c => c.access_8 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_8 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_8 == 4),

                no_answer_male = model.Count(c => c.access_8 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_8 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_8 == 0),
            };

            var access_9 = new report_1
            {
                item = "It is easy to go to a health facility",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_9 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_9 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_9 == 1),

                disagree_male = model.Count(c => c.access_9 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_9 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_9 == 2),

                agree_male = model.Count(c => c.access_9 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_9 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_9 == 3),

                strongly_agree_male = model.Count(c => c.access_9 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_9 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_9 == 4),

                no_answer_male = model.Count(c => c.access_9 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_9 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_9 == 0),
            };

            var access_11 = new report_1
            {
                item = "Children in our household were able to go to school in less time",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_11 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_11 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_11 == 1),

                disagree_male = model.Count(c => c.access_11 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_11 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_11 == 2),

                agree_male = model.Count(c => c.access_11 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_11 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_11 == 3),

                strongly_agree_male = model.Count(c => c.access_11 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_11 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_11 == 4),

                no_answer_male = model.Count(c => c.access_11 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_11 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_11 == 0),
            };

            var access_12 = new report_1
            {
                item = "Our household is able to purchase basic necessities in the market/nearby store",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_12 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_12 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_12 == 1),

                disagree_male = model.Count(c => c.access_12 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_12 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_12 == 2),

                agree_male = model.Count(c => c.access_12 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_12 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_12 == 3),

                strongly_agree_male = model.Count(c => c.access_12 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_12 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_12 == 4),

                no_answer_male = model.Count(c => c.access_12 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_12 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_12 == 0),
            };

            var access_14 = new report_1
            {
                item = "I feel secure in the community",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_14 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_14 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_14 == 1),

                disagree_male = model.Count(c => c.access_14 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_14 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_14 == 2),

                agree_male = model.Count(c => c.access_14 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_14 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_14 == 3),

                strongly_agree_male = model.Count(c => c.access_14 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_14 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_14 == 4),

                no_answer_male = model.Count(c => c.access_14 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_14 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_14 == 0),
            };

            var access_15 = new report_1
            {
                item = "Our household has access to potable water",
                year = 2015,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_15 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_15 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_15 == 1),

                disagree_male = model.Count(c => c.access_15 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_15 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_15 == 2),

                agree_male = model.Count(c => c.access_15 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_15 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_15 == 3),

                strongly_agree_male = model.Count(c => c.access_15 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_15 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_15 == 4),

                no_answer_male = model.Count(c => c.access_15 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_15 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_15 == 0),
            };

            var participation_2 = new report_1
            {
                item = "Women are engaged in the implementation of programs/projects in the community",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_2 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_2 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_2 == 1),

                disagree_male = model.Count(c => c.participation_2 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_2 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_2 == 2),

                agree_male = model.Count(c => c.participation_2 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_2 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_2 == 3),

                strongly_agree_male = model.Count(c => c.participation_2 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_2 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_2 == 4),

                no_answer_male = model.Count(c => c.participation_2 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_2 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_2 == 0),
            };
            var participation_3 = new report_1
            {
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_3 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_3 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_3 == 1),

                disagree_male = model.Count(c => c.participation_3 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_3 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_3 == 2),

                agree_male = model.Count(c => c.participation_3 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_3 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_3 == 3),

                strongly_agree_male = model.Count(c => c.participation_3 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_3 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_3 == 4),

                no_answer_male = model.Count(c => c.participation_3 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_3 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_3 == 0),
            };
            var participation_4 = new report_1
            {
                item = "The poorest are represented in the barangay assembly",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_4 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_4 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_4 == 1),

                disagree_male = model.Count(c => c.participation_4 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_4 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_4 == 2),

                agree_male = model.Count(c => c.participation_4 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_4 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_4 == 3),

                strongly_agree_male = model.Count(c => c.participation_4 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_4 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_4 == 4),

                no_answer_male = model.Count(c => c.participation_4 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_4 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_4 == 0),
            };
            var participation_6 = new report_1
            {
                item = "Members of the community are provided with various skills training (e.g. livelihood, financial literacy among others)",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_6 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_6 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_6 == 1),

                disagree_male = model.Count(c => c.participation_6 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_6 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_6 == 2),

                agree_male = model.Count(c => c.participation_6 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_6 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_6 == 3),

                strongly_agree_male = model.Count(c => c.participation_6 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_6 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_6 == 4),

                no_answer_male = model.Count(c => c.participation_6 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_6 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_6 == 0),
            };
            var participation_7 = new report_1
            {
                item = "I am able to participate in the implementation of programs/projects in the community",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_7 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_7 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_7 == 1),

                disagree_male = model.Count(c => c.participation_7 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_7 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_7 == 2),

                agree_male = model.Count(c => c.participation_7 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_7 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_7 == 3),

                strongly_agree_male = model.Count(c => c.participation_7 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_7 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_7 == 4),

                no_answer_male = model.Count(c => c.participation_7 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_7 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_7 == 0),
            };
            var participation_9 = new report_1
            {
                item = "I am not shy when participating in community development activities",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_9 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_9 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_9 == 1),

                disagree_male = model.Count(c => c.participation_9 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_9 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_9 == 2),

                agree_male = model.Count(c => c.participation_9 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_9 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_9 == 3),

                strongly_agree_male = model.Count(c => c.participation_9 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_9 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_9 == 4),

                no_answer_male = model.Count(c => c.participation_9 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_9 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_9 == 0),
            };
            var participation_11 = new report_1
            {
                item = "I have benefitted from the trainings/activities provided to the community",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_11 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_11 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_11 == 1),

                disagree_male = model.Count(c => c.participation_11 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_11 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_11 == 2),

                agree_male = model.Count(c => c.participation_11 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_11 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_11 == 3),

                strongly_agree_male = model.Count(c => c.participation_11 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_11 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_11 == 4),

                no_answer_male = model.Count(c => c.participation_11 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_11 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_11 == 0),
            };
            var participation_12 = new report_1
            {
                item = "(If answer to item above is 3 or 4 only) My skills have improved because of these trainings",
                year = 2015,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_12 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_12 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_12 == 1),

                disagree_male = model.Count(c => c.participation_12 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_12 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_12 == 2),

                agree_male = model.Count(c => c.participation_12 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_12 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_12 == 3),

                strongly_agree_male = model.Count(c => c.participation_12 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_12 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_12 == 4),

                no_answer_male = model.Count(c => c.participation_12 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_12 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_12 == 0),
            };

            var disaster_3 = new report_1
            {
                item = "People in the community are aware of the dangers brought by disasters",
                year = 2015,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_3 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_3 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_3 == 1),

                disagree_male = model.Count(c => c.disaster_3 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_3 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_3 == 2),

                agree_male = model.Count(c => c.disaster_3 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_3 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_3 == 3),

                strongly_agree_male = model.Count(c => c.disaster_3 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_3 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_3 == 4),

                no_answer_male = model.Count(c => c.disaster_3 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_3 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_3 == 0),
            };
            var disaster_4 = new report_1
            {
                item = "I am aware of the dangers brought by disasters",
                year = 2015,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_4 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_4 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_4 == 1),

                disagree_male = model.Count(c => c.disaster_4 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_4 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_4 == 2),

                agree_male = model.Count(c => c.disaster_4 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_4 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_4 == 3),

                strongly_agree_male = model.Count(c => c.disaster_4 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_4 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_4 == 4),

                no_answer_male = model.Count(c => c.disaster_4 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_4 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_4 == 0),
            };
            var disaster_5 = new report_1
            {
                item = "People are aware of how the community is affected by disasters",
                year = 2015,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_5 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_5 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_5 == 1),

                disagree_male = model.Count(c => c.disaster_5 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_5 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_5 == 2),

                agree_male = model.Count(c => c.disaster_5 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_5 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_5 == 3),

                strongly_agree_male = model.Count(c => c.disaster_5 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_5 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_5 == 4),

                no_answer_male = model.Count(c => c.disaster_5 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_5 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_5 == 0),
            };
            var disaster_6 = new report_1
            {
                item = "I am aware of how the community is affected by disasters",
                year = 2015,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_6 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_6 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_6 == 1),

                disagree_male = model.Count(c => c.disaster_6 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_6 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_6 == 2),

                agree_male = model.Count(c => c.disaster_6 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_6 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_6 == 3),

                strongly_agree_male = model.Count(c => c.disaster_6 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_6 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_6 == 4),

                no_answer_male = model.Count(c => c.disaster_6 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_6 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_6 == 0),
            };
            var disaster_7 = new report_1
            {
                item = "I am aware of the different measures to reduce dangers brought by disasters",
                year = 2015,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_7 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_7 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_7 == 1),

                disagree_male = model.Count(c => c.disaster_7 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_7 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_7 == 2),

                agree_male = model.Count(c => c.disaster_7 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_7 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_7 == 3),

                strongly_agree_male = model.Count(c => c.disaster_7 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_7 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_7 == 4),

                no_answer_male = model.Count(c => c.disaster_7 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_7 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_7 == 0),
            };
            var disaster_8 = new report_1
            {
                item = "People in the community are aware of the different measures to reduce dangers brought by disasters",
                year = 2015,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_8 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_8 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_8 == 1),

                disagree_male = model.Count(c => c.disaster_8 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_8 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_8 == 2),

                agree_male = model.Count(c => c.disaster_8 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_8 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_8 == 3),

                strongly_agree_male = model.Count(c => c.disaster_8 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_8 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_8 == 4),

                no_answer_male = model.Count(c => c.disaster_8 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_8 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_8 == 0),
            };

            //report_list.Add(total);

            report_list.Add(trust_2);
            report_list.Add(trust_4);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_7);
            report_list.Add(access_1);
            report_list.Add(access_3);
            report_list.Add(access_5);
            report_list.Add(access_7);
            report_list.Add(access_8);
            report_list.Add(access_9);
            report_list.Add(access_11);
            report_list.Add(access_12);
            report_list.Add(access_14);
            report_list.Add(access_15);
            report_list.Add(participation_2);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_6);
            report_list.Add(participation_7);
            report_list.Add(participation_9);
            report_list.Add(participation_11);
            report_list.Add(participation_12);
            report_list.Add(disaster_3);
            report_list.Add(disaster_4);
            report_list.Add(disaster_5);
            report_list.Add(disaster_6);
            report_list.Add(disaster_7);
            report_list.Add(disaster_8);

            foreach (var e in report_list)
            {
                //e.percent_strongly_disagree_male = Math.Round((100.0 * e.strongly_disagree_male / (e.total_strongly_disagree)), 2);

                e.percent_strongly_disagree_male = Math.Round((100.0 * e.strongly_disagree_male / (e.total_strongly_disagree)), 2).ToString("#.0\\%");
                e.percent_strongly_disagree_female = Math.Round((100.0 * e.strongly_disagree_female / (e.total_strongly_disagree)), 2).ToString("#.0\\%");

                e.percent_disagree_male = Math.Round((100.0 * e.disagree_male / (e.total_disagree)), 2).ToString("#.0\\%");
                e.percent_disagree_female = Math.Round((100.0 * e.disagree_female / (e.total_disagree)), 2).ToString("#.0\\%");

                e.percent_agree_male = Math.Round((100.0 * e.agree_male / (e.total_agree)), 2).ToString("#.0\\%");
                e.percent_agree_female = Math.Round((100.0 * e.agree_female / (e.total_agree)), 2).ToString("#.0\\%");

                e.percent_strongly_agree_male = Math.Round((100.0 * e.strongly_agree_male / (e.total_strongly_agree)), 2).ToString("#.0\\%");
                e.percent_strongly_agree_female = Math.Round((100.0 * e.strongly_agree_female / (e.total_strongly_agree)), 2).ToString("#.0\\%");

                e.percent_no_answer_male = Math.Round((100.0 * e.no_answer_male / (e.total_no_answer)), 2).ToString("#.0\\%");
                e.percent_no_answer_female = Math.Round((100.0 * e.no_answer_female / (e.total_no_answer)), 2).ToString("#.0\\%");

            }


            return Ok(new queriedTalakayanReport<report_1>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                total_male = model.Count(c => c.is_male == true),
                total_female = model.Count(c => c.is_male != true),
                total_respondents = model.Count(c => c.is_male == true) + model.Count(c => c.is_male != true),

            });

          //  return Ok(report_list);


        }

        #endregion

        #region Report 1 year 2016
        [HttpPost]
        [Route("api/export/perception/report_1_2016")]
        public IActionResult export_report_1_2016(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_1>();

            model = model.Where(x => x.year == 2016);

            var trust_1 = new report_1
            {
                item = "Barangay residents believe that our local officials are working for the benefit of our barangay",
                year = 2016,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_1 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_1 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_1 == 1),

                disagree_male = model.Count(c => c.trust_1 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_1 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_1 == 2),

                agree_male = model.Count(c => c.trust_1 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_1 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_1 == 3),

                strongly_agree_male = model.Count(c => c.trust_1 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_1 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_1 == 4),

                no_answer_male = model.Count(c => c.trust_1 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_1 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_1 == 0),
            };
            var trust_3 = new report_1
            {
                item = "Local officials are not fair in dealing with the people",
                year = 2016,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_3 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_3 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_3 == 1),

                disagree_male = model.Count(c => c.trust_3 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_3 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_3 == 2),

                agree_male = model.Count(c => c.trust_3 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_3 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_3 == 3),

                strongly_agree_male = model.Count(c => c.trust_3 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_3 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_3 == 4),

                no_answer_male = model.Count(c => c.trust_3 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_3 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_3 == 0),
            };

            var trust_5 = new report_1
            {
                item = "Barangay officials regularly meet with the community",
                year = 2016,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_5 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_5 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_5 == 1),

                disagree_male = model.Count(c => c.trust_5 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_5 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_5 == 2),

                agree_male = model.Count(c => c.trust_5 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_5 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_5 == 3),

                strongly_agree_male = model.Count(c => c.trust_5 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_5 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_5 == 4),

                no_answer_male = model.Count(c => c.trust_5 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_5 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_5 == 0),
            };

            var trust_6 = new report_1
            {
                item = "Municipal officials regularly meet with the community",
                year = 2016,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_6 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_6 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_6 == 1),

                disagree_male = model.Count(c => c.trust_6 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_6 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_6 == 2),

                agree_male = model.Count(c => c.trust_6 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_6 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_6 == 3),

                strongly_agree_male = model.Count(c => c.trust_6 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_6 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_6 == 4),

                no_answer_male = model.Count(c => c.trust_6 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_6 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_6 == 0),
            };
            var trust_8 = new report_1
            {
                item = "I don't believe that the local officials are working for the benefit of our barangay.",
                year = 2016,
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_male = model.Count(c => c.trust_8 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.trust_8 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.trust_8 == 1),

                disagree_male = model.Count(c => c.trust_8 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.trust_8 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.trust_8 == 2),

                agree_male = model.Count(c => c.trust_8 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.trust_8 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.trust_8 == 3),

                strongly_agree_male = model.Count(c => c.trust_8 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.trust_8 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.trust_8 == 4),

                no_answer_male = model.Count(c => c.trust_8 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.trust_8 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.trust_8 == 0),
            };


            var access_1 = new report_1
            {
                item = "There is an adequate number of health facilities in our barangay",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_1 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_1 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_1 == 1),

                disagree_male = model.Count(c => c.access_1 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_1 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_1 == 2),

                agree_male = model.Count(c => c.access_1 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_1 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_1 == 3),

                strongly_agree_male = model.Count(c => c.access_1 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_1 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_1 == 4),

                no_answer_male = model.Count(c => c.access_1 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_1 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_1 == 0),
            };

            var access_2 = new report_1
            {
                item = "The number of educational facilities in our barangay is not enough",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_2 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_2 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_2 == 1),

                disagree_male = model.Count(c => c.access_2 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_2 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_2 == 2),

                agree_male = model.Count(c => c.access_2 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_2 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_2 == 3),

                strongly_agree_male = model.Count(c => c.access_2 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_2 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_2 == 4),

                no_answer_male = model.Count(c => c.access_2 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_2 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_2 == 0),
            };

            var access_4 = new report_1
            {
                item = "Our barangay has adequate an number of roads accessed by public vehicles (e.g. transportation of public goods)",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_4 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_4 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_4 == 1),

                disagree_male = model.Count(c => c.access_4 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_4 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_4 == 2),

                agree_male = model.Count(c => c.access_4 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_4 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_4 == 3),

                strongly_agree_male = model.Count(c => c.access_4 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_4 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_4 == 4),

                no_answer_male = model.Count(c => c.access_4 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_4 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_4 == 0),
            };

            var access_6 = new report_1
            {
                item = "Potable water is not available to the community",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_6 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_6 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_6 == 1),

                disagree_male = model.Count(c => c.access_6 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_6 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_6 == 2),

                agree_male = model.Count(c => c.access_6 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_6 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_6 == 3),

                strongly_agree_male = model.Count(c => c.access_6 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_6 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_6 == 4),

                no_answer_male = model.Count(c => c.access_6 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_6 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_6 == 0),
            };

            var access_8 = new report_1
            {
                item = "There is peace and order in the community",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_8 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_8 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_8 == 1),

                disagree_male = model.Count(c => c.access_8 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_8 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_8 == 2),

                agree_male = model.Count(c => c.access_8 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_8 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_8 == 3),

                strongly_agree_male = model.Count(c => c.access_8 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_8 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_8 == 4),

                no_answer_male = model.Count(c => c.access_8 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_8 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_8 == 0),
            };

            var access_10 = new report_1
            {
                item = "School-aged children in our household are able to go to school",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_10 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_10 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_10 == 1),

                disagree_male = model.Count(c => c.access_10 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_10 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_10 == 2),

                agree_male = model.Count(c => c.access_10 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_10 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_10 == 3),

                strongly_agree_male = model.Count(c => c.access_10 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_10 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_10 == 4),

                no_answer_male = model.Count(c => c.access_10 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_10 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_10 == 0),
            };

            var access_12 = new report_1
            {
                item = "Our household is able to purchase basic necessities in the market/nearby store",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_12 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_12 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_12 == 1),

                disagree_male = model.Count(c => c.access_12 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_12 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_12 == 2),

                agree_male = model.Count(c => c.access_12 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_12 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_12 == 3),

                strongly_agree_male = model.Count(c => c.access_12 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_12 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_12 == 4),

                no_answer_male = model.Count(c => c.access_12 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_12 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_12 == 0),
            };

            var access_13 = new report_1
            {
                item = "I don’t feel secure from crimes in the community",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_13 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_13 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_13 == 1),

                disagree_male = model.Count(c => c.access_13 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_13 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_13 == 2),

                agree_male = model.Count(c => c.access_13 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_13 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_13 == 3),

                strongly_agree_male = model.Count(c => c.access_13 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_13 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_13 == 4),

                no_answer_male = model.Count(c => c.access_13 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_13 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_13 == 0),
            };

            var access_15 = new report_1
            {
                item = "Our household has access to potable water",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_15 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_15 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_15 == 1),

                disagree_male = model.Count(c => c.access_15 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_15 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_15 == 2),

                agree_male = model.Count(c => c.access_15 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_15 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_15 == 3),

                strongly_agree_male = model.Count(c => c.access_15 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_15 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_15 == 4),

                no_answer_male = model.Count(c => c.access_15 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_15 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_15 == 0),
            };

            var access_16 = new report_1
            {
                item = "If I or any of my household is sick, we can easily go to a health center",
                year = 2016,
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_male = model.Count(c => c.access_16 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.access_16 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.access_16 == 1),

                disagree_male = model.Count(c => c.access_16 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.access_16 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.access_16 == 2),

                agree_male = model.Count(c => c.access_16 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.access_16 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.access_16 == 3),

                strongly_agree_male = model.Count(c => c.access_16 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.access_16 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.access_16 == 4),

                no_answer_male = model.Count(c => c.access_16 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.access_16 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.access_16 == 0),
            };


            var participation_1 = new report_1
            {
                item = "Women are excluded from the implementation of programs/projects in the community",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_1 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_1 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_1 == 1),

                disagree_male = model.Count(c => c.participation_1 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_1 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_1 == 2),

                agree_male = model.Count(c => c.participation_1 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_1 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_1 == 3),

                strongly_agree_male = model.Count(c => c.participation_1 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_1 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_1 == 4),

                no_answer_male = model.Count(c => c.participation_1 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_1 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_1 == 0),
            };

            var participation_3 = new report_1
            {
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_3 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_3 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_3 == 1),

                disagree_male = model.Count(c => c.participation_3 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_3 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_3 == 2),

                agree_male = model.Count(c => c.participation_3 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_3 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_3 == 3),

                strongly_agree_male = model.Count(c => c.participation_3 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_3 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_3 == 4),

                no_answer_male = model.Count(c => c.participation_3 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_3 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_3 == 0),
            };
            var participation_4 = new report_1
            {
                item = "The poorest are represented in the barangay assembly",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_4 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_4 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_4 == 1),

                disagree_male = model.Count(c => c.participation_4 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_4 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_4 == 2),

                agree_male = model.Count(c => c.participation_4 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_4 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_4 == 3),

                strongly_agree_male = model.Count(c => c.participation_4 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_4 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_4 == 4),

                no_answer_male = model.Count(c => c.participation_4 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_4 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_4 == 0),
            };
            var participation_5 = new report_1
            {
                item = "Members of the community are not given various skills training (e.g. livelihood, financial literacy, etc.)",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_5 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_5 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_5 == 1),

                disagree_male = model.Count(c => c.participation_5 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_5 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_5 == 2),

                agree_male = model.Count(c => c.participation_5 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_5 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_5 == 3),

                strongly_agree_male = model.Count(c => c.participation_5 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_5 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_5 == 4),

                no_answer_male = model.Count(c => c.participation_5 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_5 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_5 == 0),
            };
            var participation_7 = new report_1
            {
                item = "I am able to participate in the implementation of programs/projects in the community",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_7 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_7 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_7 == 1),

                disagree_male = model.Count(c => c.participation_7 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_7 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_7 == 2),

                agree_male = model.Count(c => c.participation_7 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_7 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_7 == 3),

                strongly_agree_male = model.Count(c => c.participation_7 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_7 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_7 == 4),

                no_answer_male = model.Count(c => c.participation_7 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_7 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_7 == 0),
            };
            var participation_8 = new report_1
            {
                item = "I am shy when participating in community development activities",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_8 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_8 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_8 == 1),

                disagree_male = model.Count(c => c.participation_8 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_8 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_8 == 2),

                agree_male = model.Count(c => c.participation_8 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_8 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_8 == 3),

                strongly_agree_male = model.Count(c => c.participation_8 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_8 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_8 == 4),

                no_answer_male = model.Count(c => c.participation_8 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_8 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_8 == 0),
            };
            var participation_10 = new report_1
            {
                item = "I attend trainings/activities (e.g. livelihood, financial literacy, etc.) provided to the community",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_10 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_10 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_10 == 1),

                disagree_male = model.Count(c => c.participation_10 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_10 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_10 == 2),

                agree_male = model.Count(c => c.participation_10 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_10 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_10 == 3),

                strongly_agree_male = model.Count(c => c.participation_10 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_10 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_10 == 4),

                no_answer_male = model.Count(c => c.participation_10 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_10 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_10 == 0),
            };
            var participation_12 = new report_1
            {
                item = "(If answer to item above is 3 or 4 only) My skills have improved because of these trainings",
                year = 2016,
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_male = model.Count(c => c.participation_12 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.participation_12 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.participation_12 == 1),

                disagree_male = model.Count(c => c.participation_12 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.participation_12 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.participation_12 == 2),

                agree_male = model.Count(c => c.participation_12 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.participation_12 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.participation_12 == 3),

                strongly_agree_male = model.Count(c => c.participation_12 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.participation_12 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.participation_12 == 4),

                no_answer_male = model.Count(c => c.participation_12 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.participation_12 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.participation_12 == 0),
            };


            var disaster_1 = new report_1
            {
                item = "People in the community are aware of the dangers brought by disasters",
                year = 2016,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_1 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_1 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_1 == 1),

                disagree_male = model.Count(c => c.disaster_1 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_1 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_1 == 2),

                agree_male = model.Count(c => c.disaster_1 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_1 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_1 == 3),

                strongly_agree_male = model.Count(c => c.disaster_1 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_1 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_1 == 4),

                no_answer_male = model.Count(c => c.disaster_1 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_1 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_1 == 0),
            };
            var disaster_2 = new report_1
            {
                item = "People in the community are not aware of the different measures to reduce dangers brought by disasters",
                year = 2016,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_2 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_2 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_2 == 1),

                disagree_male = model.Count(c => c.disaster_2 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_2 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_2 == 2),

                agree_male = model.Count(c => c.disaster_2 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_2 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_2 == 3),

                strongly_agree_male = model.Count(c => c.disaster_2 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_2 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_2 == 4),

                no_answer_male = model.Count(c => c.disaster_2 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_2 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_2 == 0),
            };
            var disaster_4 = new report_1
            {
                item = "I am aware of the dangers brought by disasters",
                year = 2016,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_4 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_4 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_4 == 1),

                disagree_male = model.Count(c => c.disaster_4 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_4 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_4 == 2),

                agree_male = model.Count(c => c.disaster_4 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_4 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_4 == 3),

                strongly_agree_male = model.Count(c => c.disaster_4 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_4 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_4 == 4),

                no_answer_male = model.Count(c => c.disaster_4 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_4 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_4 == 0),
            };
            var disaster_7 = new report_1
            {
                item = "I am aware of the different measures to reduce dangers brought by disasters",
                year = 2016,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_7 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_7 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_7 == 1),

                disagree_male = model.Count(c => c.disaster_7 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_7 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_7 == 2),

                agree_male = model.Count(c => c.disaster_7 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_7 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_7 == 3),

                strongly_agree_male = model.Count(c => c.disaster_7 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_7 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_7 == 4),

                no_answer_male = model.Count(c => c.disaster_7 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_7 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_7 == 0),
            };
            var disaster_9 = new report_1
            {
                item = "I think my barangay is not ready for any possible disaster ",
                year = 2016,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_male = model.Count(c => c.disaster_9 == 1 && c.is_male == true),
                strongly_disagree_female = model.Count(c => c.disaster_9 == 1 && c.is_male != true),
                total_strongly_disagree = model.Count(c => c.disaster_9 == 1),

                disagree_male = model.Count(c => c.disaster_9 == 2 && c.is_male == true),
                disagree_female = model.Count(c => c.disaster_9 == 2 && c.is_male != true),
                total_disagree = model.Count(c => c.disaster_9 == 2),

                agree_male = model.Count(c => c.disaster_9 == 3 && c.is_male == true),
                agree_female = model.Count(c => c.disaster_9 == 3 && c.is_male != true),
                total_agree = model.Count(c => c.disaster_9 == 3),

                strongly_agree_male = model.Count(c => c.disaster_9 == 4 && c.is_male == true),
                strongly_agree_female = model.Count(c => c.disaster_9 == 4 && c.is_male != true),
                total_strongly_agree = model.Count(c => c.disaster_9 == 4),

                no_answer_male = model.Count(c => c.disaster_9 == 0 && c.is_male == true),
                no_answer_female = model.Count(c => c.disaster_9 == 0 && c.is_male != true),
                total_no_answer = model.Count(c => c.disaster_9 == 0),
            };

            report_list.Add(trust_1);
            report_list.Add(trust_3);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_8);
            report_list.Add(access_1);
            report_list.Add(access_2);
            report_list.Add(access_4);
            report_list.Add(access_6);
            report_list.Add(access_8);
            report_list.Add(access_10);
            report_list.Add(access_12);
            report_list.Add(access_13);
            report_list.Add(access_15);
            report_list.Add(access_16);
            report_list.Add(participation_1);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_5);
            report_list.Add(participation_7);
            report_list.Add(participation_8);
            report_list.Add(participation_10);
            report_list.Add(participation_12);
            report_list.Add(disaster_1);
            report_list.Add(disaster_2);
            report_list.Add(disaster_4);
            report_list.Add(disaster_7);
            report_list.Add(disaster_9);

            foreach (var e in report_list)
            {
                //e.percent_strongly_disagree_male = Math.Round((100.0 * e.strongly_disagree_male / (e.total_strongly_disagree)), 2);

                e.percent_strongly_disagree_male = Math.Round((100.0 * e.strongly_disagree_male / (e.total_strongly_disagree)), 2).ToString("#.0\\%");
                e.percent_strongly_disagree_female = Math.Round((100.0 * e.strongly_disagree_female / (e.total_strongly_disagree)), 2).ToString("#.0\\%");

                e.percent_disagree_male = Math.Round((100.0 * e.disagree_male / (e.total_disagree)), 2).ToString("#.0\\%");
                e.percent_disagree_female = Math.Round((100.0 * e.disagree_female / (e.total_disagree)), 2).ToString("#.0\\%");

                e.percent_agree_male = Math.Round((100.0 * e.agree_male / (e.total_agree)), 2).ToString("#.0\\%");
                e.percent_agree_female = Math.Round((100.0 * e.agree_female / (e.total_agree)), 2).ToString("#.0\\%");

                e.percent_strongly_agree_male = Math.Round((100.0 * e.strongly_agree_male / (e.total_strongly_agree)), 2).ToString("#.0\\%");
                e.percent_strongly_agree_female = Math.Round((100.0 * e.strongly_agree_female / (e.total_strongly_agree)), 2).ToString("#.0\\%");

                e.percent_no_answer_male = Math.Round((100.0 * e.no_answer_male / (e.total_no_answer)), 2).ToString("#.0\\%");
                e.percent_no_answer_female = Math.Round((100.0 * e.no_answer_female / (e.total_no_answer)), 2).ToString("#.0\\%");

            }

            return Ok(new queriedTalakayanReport<report_1>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                total_male = model.Count(c => c.is_male == true),
                total_female = model.Count(c => c.is_male != true),
                total_respondents = model.Count(c => c.is_male == true) + model.Count(c => c.is_male != true),

            });

            //return Ok(report_list);
        }

        #endregion

        public class report_2
        {
            public int year { get; set; }
            public string item { get; set; }
            public string category { get; set; }
            public int total_num_of_response { get; set; }

            public int strongly_disagree_pantawid { get; set; }
            public int strongly_disagree_slp { get; set; }
            public int strongly_disagree_ip { get; set; }

            public int disagree_pantawid { get; set; }
            public int disagree_slp { get; set; }
            public int disagree_ip { get; set; }

            public int agree_pantawid { get; set; }
            public int agree_slp { get; set; }
            public int agree_ip { get; set; }

            public int strongly_agree_pantawid { get; set; }
            public int strongly_agree_slp { get; set; }
            public int strongly_agree_ip { get; set; }

            public int no_answer_pantawid { get; set; }
            public int no_answer_slp { get; set; }
            public int no_answer_ip { get; set; }

            public string percent_strongly_disagree_pantawid { get; set; }
            public string percent_strongly_disagree_slp { get; set; }
            public string percent_strongly_disagree_ip { get; set; }

            public string percent_disagree_pantawid { get; set; }
            public string percent_disagree_slp { get; set; }
            public string percent_disagree_ip { get; set; }

            public string percent_agree_pantawid { get; set; }
            public string percent_agree_slp { get; set; }
            public string percent_agree_ip { get; set; }

            public string percent_strongly_agree_pantawid { get; set; }
            public string percent_strongly_agree_slp { get; set; }
            public string percent_strongly_agree_ip { get; set; }

            public string percent_no_answer_pantawid { get; set; }
            public string percent_no_answer_slp { get; set; }
            public string percent_no_answer_ip { get; set; }

        }

        #region Report 2 year 2015
        [HttpPost]
        [Route("api/export/perception/report_2_2015")]
        public IActionResult export_report_2_2015(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_2>();

            model = model.Where(x => x.year == 2015);

            var trust_2 = new report_2
            {
                item = "The community trusts the local officials in planning, implementation, monitoring and reporting of programs and projects for the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_2 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_2 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_2 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_2 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_2 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_2 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_2 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_2 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_2 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_2 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_2 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_2 == 4 && c.is_ip == true),
                strongly_agree_ip = model.Count(c => c.trust_2 == 4),

                no_answer_pantawid = model.Count(c => c.trust_2 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_2 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_2 == 0 && c.is_ip == true),
            };

            var trust_4 = new report_2
            {
                item = "Local officials are fair in dealing with the people",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_4 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_4 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_4 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_4 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_4 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_4 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_4 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_4 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_4 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_4 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_4 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_4 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_4 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_4 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_4 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_4 == 0 && c.is_ip == true),
            };

            var trust_5 = new report_2
            {
                item = "Barangay officials regularly meet with the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_5 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_5 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_5 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_5 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_5 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_5 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_5 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_5 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_5 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_5 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_5 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_5 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_5 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_5 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_5 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_5 == 0 && c.is_ip == true),
            };

            var trust_6 = new report_2
            {
                item = "Municipal officials regularly meet with the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_6 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_6 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_6 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_6 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_6 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_6 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_6 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_6 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_6 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_6 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_6 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_6 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_6 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_6 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_6 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_6 == 0 && c.is_ip == true),
            };
            var trust_7 = new report_2
            {
                item = "I trust the local officials in planning, implementation, monitoring and reporting of programs and projects for the barangay",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_7 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_7 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_7 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_7 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_7 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_7 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_7 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_7 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_7 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_7 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_7 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_7 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_7 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_7 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_7 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_7 == 0 && c.is_ip == true),
            };

            var access_1 = new report_2
            {
                item = "There is an adequate number of health facilities in our barangay",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_1 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_1 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_1 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_1 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_1 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_1 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_1 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_1 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_1 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_1 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_1 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_1 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_1 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_1 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_1 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_1 == 0 && c.is_ip == true),
            };

            var access_3 = new report_2
            {
                item = "There is adequate number of educational facilities",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_3 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_3 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_3 == 1 && c.is_ip == true),
                strongly_disagree_ip = model.Count(c => c.access_3 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_3 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_3 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_3 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_3 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_3 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_3 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_3 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_3 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_3 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_3 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_3 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_3 == 0 && c.is_ip == true),
            };

            var access_5 = new report_2
            {
                item = "There is adequate number of roads accessed by public vehicles (e.g. transportation of public goods)",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_5 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_5 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_5 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_5 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_5 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_5 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_5 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_5 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_5 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_5 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_5 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_5 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_5 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_5 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_5 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_5 == 0 && c.is_ip == true),
            };

            var access_7 = new report_2
            {
                item = "Potable water is accessible to the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_7 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_7 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_7 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_7 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_7 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_7 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_7 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_7 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_7 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_7 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_7 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_7 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_7 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_7 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_7 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_7 == 0 && c.is_ip == true),
            };

            var access_8 = new report_2
            {
                item = "There is peace and order in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_8 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_8 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_8 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_8 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_8 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_8 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_8 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_8 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_8 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_8 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_8 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_8 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_8 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_8 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_8 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_8 == 0 && c.is_ip == true),
            };

            var access_9 = new report_2
            {
                item = "It is easy to go to a health facility",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_9 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_9 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_9 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_9 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_9 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_9 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_9 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_9 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_9 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_9 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_9 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_9 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_9 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_9 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_9 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_9 == 0 && c.is_ip == true),
            };

            var access_11 = new report_2
            {
                item = "Children in our household were able to go to school in less time",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_11 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_11 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_11 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_11 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_11 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_11 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_11 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_11 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_11 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_11 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_11 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_11 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_11 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_11 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_11 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_11 == 0 && c.is_ip == true),
            };

            var access_12 = new report_2
            {
                item = "Our household is able to purchase basic necessities in the market/nearby store",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_12 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_12 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_12 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_12 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_12 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_12 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_12 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_12 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_12 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_12 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_12 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_12 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_12 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_12 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_12 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_12 == 0 && c.is_ip == true),
            };

            var access_14 = new report_2
            {
                item = "I feel secure in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_14 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_14 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_14 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_14 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_14 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_14 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_14 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_14 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_14 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_14 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_14 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_14 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_14 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_14 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_14 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_14 == 0 && c.is_ip == true),
            };

            var access_15 = new report_2
            {
                item = "Our household has access to potable water",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_15 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_15 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_15 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_15 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_15 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_15 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_15 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_15 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_15 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_15 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_15 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_15 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_15 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_15 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_15 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_15 == 0 && c.is_ip == true),
            };

            var participation_2 = new report_2
            {
                item = "Women are engaged in the implementation of programs/projects in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_2 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_2 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_2 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_2 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_2 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_2 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_2 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_2 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_2 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_2 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_2 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_2 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_2 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_2 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_2 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_2 == 0 && c.is_ip == true),
            };
            var participation_3 = new report_2
            {
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_3 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_3 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_3 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_3 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_3 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_3 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_3 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_3 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_3 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_3 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_3 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_3 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_3 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_3 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_3 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_3 == 0 && c.is_ip == true),
            };
            var participation_4 = new report_2
            {
                item = "The poorest are represented in the barangay assembly",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_4 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_4 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_4 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_4 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_4 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_4 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_4 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_4 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_4 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_4 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_4 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_4 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_4 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_4 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_4 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_4 == 0 && c.is_ip == true),
            };
            var participation_6 = new report_2
            {
                item = "Members of the community are provided with various skills training (e.g. livelihood, financial literacy among others)",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_6 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_6 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_6 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_6 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_6 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_6 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_6 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_6 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_6 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_6 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_6 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_6 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_6 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_6 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_6 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_6 == 0 && c.is_ip == true),
            };
            var participation_7 = new report_2
            {
                item = "I am able to participate in the implementation of programs/projects in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_7 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_7 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_7 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_7 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_7 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_7 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_7 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_7 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_7 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_7 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_7 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_7 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_7 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_7 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_7 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_7 == 0 && c.is_ip == true),
            };
            var participation_9 = new report_2
            {
                item = "I am not shy when participating in community development activities",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_9 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_9 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_9 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_9 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_9 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_9 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_9 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_9 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_9 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_9 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_9 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_9 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_9 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_9 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_9 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_9 == 0 && c.is_ip == true),
            };
            var participation_11 = new report_2
            {
                item = "I have benefitted from the trainings/activities provided to the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_11 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_11 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_11 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_11 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_11 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_11 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_11 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_11 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_11 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_11 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_11 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_11 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_11 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_11 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_11 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_11 == 0 && c.is_ip == true),
            };
            var participation_12 = new report_2
            {
                item = "(If answer to item above is 3 or 4 only) My skills have improved because of these trainings",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_12 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_12 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_12 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_12 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_12 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_12 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_12 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_12 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_12 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_12 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_12 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_12 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_12 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_12 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_12 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_12 == 0 && c.is_ip == true),
            };

            var disaster_3 = new report_2
            {
                item = "People in the community are aware of the dangers brought by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_3 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_3 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_3 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_3 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_3 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_3 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_3 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_3 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_3 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_3 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_3 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_3 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_3 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_3 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_3 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_3 == 0 && c.is_ip == true),
            };
            var disaster_4 = new report_2
            {
                item = "I am aware of the dangers brought by disasters",
                total_num_of_response = model.Count(c => c.disaster_4 != 0),
                year = 2015,
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_4 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_4 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_4 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_4 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_4 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_4 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_4 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_4 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_4 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_4 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_4 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_4 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_4 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_4 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_4 == 0 && c.is_ip == true),
            };
            var disaster_5 = new report_2
            {
                item = "People are aware of how the community is affected by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_5 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_5 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_5 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_5 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_5 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_5 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_5 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_5 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_5 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_5 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_5 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_5 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_5 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_5 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_5 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_5 == 0 && c.is_ip == true),
            };
            var disaster_6 = new report_2
            {
                item = "I am aware of how the community is affected by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_6 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_6 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_6 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_6 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_6 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_6 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_6 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_6 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_6 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_6 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_6 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_6 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_6 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_6 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_6 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_6 == 0 && c.is_ip == true),
            };
            var disaster_7 = new report_2
            {
                item = "I am aware of the different measures to reduce dangers brought by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_7 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_7 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_7 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_7 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_7 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_7 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_7 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_7 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_7 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_7 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_7 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_7 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_7 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_7 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_7 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_7 == 0 && c.is_ip == true),
            };
            var disaster_8 = new report_2
            {
                item = "People in the community are aware of the different measures to reduce dangers brought by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_8 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_8 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_8 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_8 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_8 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_8 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_8 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_8 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_8 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_8 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_8 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_8 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_8 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_8 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_8 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_8 == 0 && c.is_ip == true),
            };

            report_list.Add(trust_2);
            report_list.Add(trust_4);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_7);
            report_list.Add(access_1);
            report_list.Add(access_3);
            report_list.Add(access_5);
            report_list.Add(access_7);
            report_list.Add(access_8);
            report_list.Add(access_9);
            report_list.Add(access_11);
            report_list.Add(access_12);
            report_list.Add(access_14);
            report_list.Add(access_15);
            report_list.Add(participation_2);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_6);
            report_list.Add(participation_7);
            report_list.Add(participation_9);
            report_list.Add(participation_11);
            report_list.Add(participation_12);
            report_list.Add(disaster_3);
            report_list.Add(disaster_4);
            report_list.Add(disaster_5);
            report_list.Add(disaster_6);
            report_list.Add(disaster_7);
            report_list.Add(disaster_8);

            foreach (var e in report_list)
            {
                e.percent_strongly_disagree_pantawid = Math.Round((100.0 * e.strongly_disagree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_disagree_slp = Math.Round((100.0 * e.strongly_disagree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_disagree_ip = Math.Round((100.0 * e.strongly_disagree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_disagree_pantawid = Math.Round((100.0 * e.disagree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_disagree_slp = Math.Round((100.0 * e.disagree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_disagree_ip = Math.Round((100.0 * e.disagree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_agree_pantawid = Math.Round((100.0 * e.agree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_agree_slp = Math.Round((100.0 * e.agree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_agree_ip = Math.Round((100.0 * e.agree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_strongly_agree_pantawid = Math.Round((100.0 * e.strongly_agree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_agree_slp = Math.Round((100.0 * e.strongly_agree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_agree_ip = Math.Round((100.0 * e.strongly_agree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_no_answer_pantawid = Math.Round((100.0 * e.no_answer_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_no_answer_slp = Math.Round((100.0 * e.no_answer_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_no_answer_ip = Math.Round((100.0 * e.no_answer_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

            }

            return Ok(new queriedTalakayanReport<report_2>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),

                total_pantawid = model.Count(c => c.is_pantawid == true),
                total_slp = model.Count(c => c.is_slp == true),
                total_ip = model.Count(c => c.is_ip == true),
                total_respondents = model.Count()
            });

           // return Ok(report_list);
        }
        #endregion

        #region Report 2 year 2016        
        [HttpPost]
        [Route("api/export/perception/report_2_2016")]
        public IActionResult export_report_2_2016(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_2>();

            model = model.Where(x => x.year == 2016);

            var trust_1 = new report_2
            {
                item = "Barangay residents believe that our local officials are working for the benefit of our barangay",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_1 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_1 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_1 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_1 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_1 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_1 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_1 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_1 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_1 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_1 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_1 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_1 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_1 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_1 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_1 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_1 == 0 && c.is_ip == true),
            };
            var trust_3 = new report_2
            {
                item = "Local officials are not fair in dealing with the people",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_3 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_3 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_3 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_3 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_3 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_3 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_3 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_3 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_3 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_3 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_3 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_3 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_3 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_3 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_3 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_3 == 0 && c.is_ip == true),
            };

            var trust_5 = new report_2
            {
                item = "Barangay officials regularly meet with the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_5 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_5 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_5 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_5 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_5 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_5 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_5 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_5 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_5 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_5 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_5 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_5 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_5 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_5 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_5 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_5 == 0 && c.is_ip == true),
            };

            var trust_6 = new report_2
            {
                item = "Municipal officials regularly meet with the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_6 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_6 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_6 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_6 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_6 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_6 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_6 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_6 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_6 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_6 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_6 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_6 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_6 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_6 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_6 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_6 == 0 && c.is_ip == true),
            };
            var trust_8 = new report_2
            {
                item = "I don't believe that the local officials are working for the benefit of our barangay.",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_8 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree_pantawid = model.Count(c => c.trust_8 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.trust_8 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.trust_8 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.trust_8 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.trust_8 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.trust_8 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.trust_8 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.trust_8 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.trust_8 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.trust_8 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.trust_8 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.trust_8 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.trust_8 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.trust_8 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.trust_8 == 0 && c.is_ip == true),
            };

            var access_1 = new report_2
            {
                item = "There is an adequate number of health facilities in our barangay",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_1 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_1 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_1 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_1 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_1 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_1 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_1 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_1 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_1 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_1 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_1 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_1 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_1 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_1 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_1 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_1 == 0 && c.is_ip == true),
            };

            var access_2 = new report_2
            {
                item = "The number of educational facilities in our barangay is not enough",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_2 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_2 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_2 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_2 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_2 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_2 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_2 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_2 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_2 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_2 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_2 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_2 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_2 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_2 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_2 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_2 == 0 && c.is_ip == true),
            };

            var access_4 = new report_2
            {
                item = "Our barangay has adequate an number of roads accessed by public vehicles (e.g. transportation of public goods)",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_4 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_4 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_4 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_4 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_4 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_4 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_4 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_4 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_4 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_4 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_4 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_4 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_4 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_4 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_4 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_4 == 0 && c.is_ip == true),
            };

            var access_6 = new report_2
            {
                item = "Potable water is not available to the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_6 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_6 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_6 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_6 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_6 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_6 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_6 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_6 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_6 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_6 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_6 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_6 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_6 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_6 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_6 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_6 == 0 && c.is_ip == true),
            };

            var access_8 = new report_2
            {
                item = "There is peace and order in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_8 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_8 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_8 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_8 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_8 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_8 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_8 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_8 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_8 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_8 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_8 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_8 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_8 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_8 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_8 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_8 == 0 && c.is_ip == true),
            };

            var access_10 = new report_2
            {
                item = "School-aged children in our household are able to go to school",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_10 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_10 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_10 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_10 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_10 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_10 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_10 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_10 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_10 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_10 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_10 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_10 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_10 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_10 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_10 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_10 == 0 && c.is_ip == true),
            };

            var access_12 = new report_2
            {
                item = "Our household is able to purchase basic necessities in the market/nearby store",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_12 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_12 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_12 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_12 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_12 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_12 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_12 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_12 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_12 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_12 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_12 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_12 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_12 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_12 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_12 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_12 == 0 && c.is_ip == true),
            };

            var access_13 = new report_2
            {
                item = "I don’t feel secure from crimes in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_13 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_13 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_13 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_13 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_13 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_13 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_13 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_13 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_13 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_13 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_13 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_13 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_13 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_13 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_13 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_13 == 0 && c.is_ip == true),
            };

            var access_15 = new report_2
            {
                item = "Our household has access to potable water",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_15 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_15 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_15 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_15 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_15 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_15 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_15 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_15 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_15 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_15 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_15 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_15 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_15 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_15 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_15 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_15 == 0 && c.is_ip == true),
            };

            var access_16 = new report_2
            {
                item = "If I or any of my household is sick, we can easily go to a health center",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_16 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree_pantawid = model.Count(c => c.access_16 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.access_16 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.access_16 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.access_16 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.access_16 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.access_16 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.access_16 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.access_16 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.access_16 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.access_16 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.access_16 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.access_16 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.access_16 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.access_16 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.access_16 == 0 && c.is_ip == true),
            };

            var participation_1 = new report_2
            {
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_1 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_1 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_1 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_1 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_1 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_1 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_1 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_1 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_1 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_1 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_1 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_1 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_1 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_1 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_1 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_1 == 0 && c.is_ip == true),
            };
            var participation_3 = new report_2
            {
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_3 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_3 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_3 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_3 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_3 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_3 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_3 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_3 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_3 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_3 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_3 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_3 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_3 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_3 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_3 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_3 == 0 && c.is_ip == true),
            };
            var participation_4 = new report_2
            {
                item = "The poorest are represented in the barangay assembly",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_4 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_4 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_4 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_4 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_4 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_4 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_4 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_4 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_4 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_4 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_4 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_4 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_4 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_4 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_4 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_4 == 0 && c.is_ip == true),
            };
            var participation_5 = new report_2
            {
                item = "Members of the community are not given various skills training (e.g. livelihood, financial literacy, etc.)",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_5 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_5 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_5 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_5 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_5 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_5 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_5 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_5 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_5 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_5 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_5 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_5 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_5 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_5 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_5 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_5 == 0 && c.is_ip == true),
            };
            var participation_7 = new report_2
            {
                item = "I am able to participate in the implementation of programs/projects in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_7 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_7 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_7 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_7 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_7 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_7 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_7 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_7 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_7 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_7 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_7 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_7 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_7 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_7 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_7 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_7 == 0 && c.is_ip == true),
            };
            var participation_8 = new report_2
            {
                item = "I am shy when participating in community development activities",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_8 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_8 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_8 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_8 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_8 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_8 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_8 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_8 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_8 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_8 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_8 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_8 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_8 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_8 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_8 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_8 == 0 && c.is_ip == true),
            };
            var participation_10 = new report_2
            {
                item = "I attend trainings/activities (e.g. livelihood, financial literacy, etc.) provided to the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_10 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_10 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_10 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_10 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_10 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_10 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_10 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_10 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_10 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_10 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_10 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_10 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_10 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_10 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_10 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_10 == 0 && c.is_ip == true),
            };
            var participation_12 = new report_2
            {
                item = "(If answer to item above is 3 or 4 only) My skills have improved because of these trainings",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_12 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree_pantawid = model.Count(c => c.participation_12 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.participation_12 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.participation_12 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.participation_12 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.participation_12 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.participation_12 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.participation_12 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.participation_12 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.participation_12 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.participation_12 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.participation_12 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.participation_12 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.participation_12 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.participation_12 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.participation_12 == 0 && c.is_ip == true),
            };

            var disaster_1 = new report_2
            {
                item = "People in the community are aware of the dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_1 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_1 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_1 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_1 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_1 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_1 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_1 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_1 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_1 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_1 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_1 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_1 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_1 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_1 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_1 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_1 == 0 && c.is_ip == true),
            };
            var disaster_2 = new report_2
            {
                item = "People in the community are not aware of the different measures to reduce dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_2 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_2 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_2 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_2 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_2 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_2 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_2 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_2 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_2 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_2 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_2 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_2 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_2 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_2 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_2 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_2 == 0 && c.is_ip == true),
            };
            var disaster_4 = new report_2
            {
                item = "I am aware of the dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_4 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_4 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_4 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_4 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_4 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_4 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_4 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_4 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_4 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_4 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_4 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_4 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_4 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_4 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_4 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_4 == 0 && c.is_ip == true),
            };
            var disaster_7 = new report_2
            {
                item = "I am aware of the different measures to reduce dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_7 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_7 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_7 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_7 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_7 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_7 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_7 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_7 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_7 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_7 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_7 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_7 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_7 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_7 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_7 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_7 == 0 && c.is_ip == true),
            };
            var disaster_9 = new report_2
            {
                item = "I think my barangay is not ready for any possible disaster ",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_9 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree_pantawid = model.Count(c => c.disaster_9 == 1 && c.is_pantawid == true),
                strongly_disagree_slp = model.Count(c => c.disaster_9 == 1 && c.is_slp == true),
                strongly_disagree_ip = model.Count(c => c.disaster_9 == 1 && c.is_ip == true),

                disagree_pantawid = model.Count(c => c.disaster_9 == 2 && c.is_pantawid == true),
                disagree_slp = model.Count(c => c.disaster_9 == 2 && c.is_slp == true),
                disagree_ip = model.Count(c => c.disaster_9 == 2 && c.is_ip == true),

                agree_pantawid = model.Count(c => c.disaster_9 == 3 && c.is_pantawid == true),
                agree_slp = model.Count(c => c.disaster_9 == 3 && c.is_slp == true),
                agree_ip = model.Count(c => c.disaster_9 == 3 && c.is_ip == true),

                strongly_agree_pantawid = model.Count(c => c.disaster_9 == 4 && c.is_pantawid == true),
                strongly_agree_slp = model.Count(c => c.disaster_9 == 4 && c.is_slp == true),
                strongly_agree_ip = model.Count(c => c.disaster_9 == 4 && c.is_ip == true),

                no_answer_pantawid = model.Count(c => c.disaster_9 == 0 && c.is_pantawid == true),
                no_answer_slp = model.Count(c => c.disaster_9 == 0 && c.is_slp == true),
                no_answer_ip = model.Count(c => c.disaster_9 == 0 && c.is_ip == true),
            };

            report_list.Add(trust_1);
            report_list.Add(trust_3);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_8);
            report_list.Add(access_1);
            report_list.Add(access_2);
            report_list.Add(access_4);
            report_list.Add(access_6);
            report_list.Add(access_8);
            report_list.Add(access_10);
            report_list.Add(access_12);
            report_list.Add(access_13);
            report_list.Add(access_15);
            report_list.Add(access_16);
            report_list.Add(participation_1);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_5);
            report_list.Add(participation_7);
            report_list.Add(participation_8);
            report_list.Add(participation_10);
            report_list.Add(participation_12);
            report_list.Add(disaster_1);
            report_list.Add(disaster_2);
            report_list.Add(disaster_4);
            report_list.Add(disaster_7);
            report_list.Add(disaster_9);

            foreach (var e in report_list)
            {
                e.percent_strongly_disagree_pantawid = Math.Round((100.0 * e.strongly_disagree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_disagree_slp = Math.Round((100.0 * e.strongly_disagree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_disagree_ip = Math.Round((100.0 * e.strongly_disagree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_disagree_pantawid = Math.Round((100.0 * e.disagree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_disagree_slp = Math.Round((100.0 * e.disagree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_disagree_ip = Math.Round((100.0 * e.disagree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_agree_pantawid = Math.Round((100.0 * e.agree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_agree_slp = Math.Round((100.0 * e.agree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_agree_ip = Math.Round((100.0 * e.agree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_strongly_agree_pantawid = Math.Round((100.0 * e.strongly_agree_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_agree_slp = Math.Round((100.0 * e.strongly_agree_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_agree_ip = Math.Round((100.0 * e.strongly_agree_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.percent_no_answer_pantawid = Math.Round((100.0 * e.no_answer_pantawid / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_no_answer_slp = Math.Round((100.0 * e.no_answer_slp / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_no_answer_ip = Math.Round((100.0 * e.no_answer_ip / (e.total_num_of_response)), 2).ToString("#.0\\%");

            }

            return Ok(new queriedTalakayanReport<report_2>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),

                total_pantawid = model.Count(c => c.is_pantawid == true),
                total_slp = model.Count(c => c.is_slp == true),
                total_ip = model.Count(c => c.is_ip == true),
                total_respondents = model.Count()
            });

            //return Ok(report_list);
        }

        #endregion

        public class report_3
        {
            public int total_participants { get; set; }
            public int total_male { get; set; }
            public int total_female { get; set; }
            public int total_pantawid { get; set; }
            public int total_slp { get; set; }
            public int total_ip { get; set; }
            public int total_60_and_above { get; set; }
            public int total_40_to_59_yo { get; set; }
            public int total_21_to_39_yo { get; set; }
            public int total_20_and_below { get; set; }

            public string percent_male { get; set; }
            public string percent_female { get; set; }
            public string percent_pantawid { get; set; }
            public string percent_slp { get; set; }
            public string percent_ip { get; set; }
            public string percent_60_and_above { get; set; }
            public string percent_40_to_59_yo { get; set; }
            public string percent_21_to_39_yo { get; set; }
            public string percent_20_and_below { get; set; }

            public int year { get; set; }

        }

        #region Report 3, 2015
        [HttpPost]
        [Route("api/export/perception/report_3_2015")]
        public IActionResult export_report_3_2015(AngularFilterModel item)
        {
            var model = GetData(item);

            var report_list = new List<report_3>();

            model = model.Where(x => x.year == 2015);

            var report = new report_3
            {
                total_participants = model.Count(),
                year = 2015,

                total_male = model.Count(c => c.is_male == true),
                total_female = model.Count(c => c.is_male != true),

                total_pantawid = model.Count(c => c.is_pantawid == true),
                total_slp = model.Count(c => c.is_slp == true),
                total_ip = model.Count(c => c.is_ip == true),

                total_60_and_above = model.Count(c => c.age >= 60),
                total_40_to_59_yo = model.Count(c => c.age >= 40 && c.age <= 59),
                total_21_to_39_yo = model.Count(c => c.age >= 21 && c.age <= 39),
                total_20_and_below = model.Count(c => c.age <= 20),

            };

            report_list.Add(report);

            foreach (var e in report_list)
            {
                e.percent_male = Math.Round((100.0 * e.total_male / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_female = Math.Round((100.0 * e.total_female / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_pantawid = Math.Round((100.0 * e.total_pantawid / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_slp = Math.Round((100.0 * e.total_slp / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_ip = Math.Round((100.0 * e.total_ip / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_60_and_above = Math.Round((100.0 * e.total_60_and_above / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_40_to_59_yo = Math.Round((100.0 * e.total_40_to_59_yo / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_21_to_39_yo = Math.Round((100.0 * e.total_21_to_39_yo / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_20_and_below = Math.Round((100.0 * e.total_20_and_below / (e.total_participants)), 2).ToString("#.0\\%");

            }

            return Ok(new queriedTalakayanReport<report_3>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
            });

            //return Ok(report_list);
        }
        #endregion

        #region Report 3, 2016
        [HttpPost]
        [Route("api/export/perception/report_3_2016")]
        public IActionResult export_report_3_2016(AngularFilterModel item)
        {
            var model = GetData(item);
            
            var report_list = new List<report_3>();

            model = model.Where(x => x.year == 2016);

            var report = new report_3
            {
                total_participants = model.Count(),
                year = 2016,

                total_male = model.Count(c => c.is_male == true),
                total_female = model.Count(c => c.is_male != true),

                total_pantawid = model.Count(c => c.is_pantawid == true),
                total_slp = model.Count(c => c.is_slp == true),
                total_ip = model.Count(c => c.is_ip == true),

                total_60_and_above = model.Count(c => c.age >= 60),
                total_40_to_59_yo = model.Count(c => c.age >= 40 && c.age <= 59),
                total_21_to_39_yo = model.Count(c => c.age >= 21 && c.age <= 39),
                total_20_and_below = model.Count(c => c.age <= 20),


            };

            report_list.Add(report);

            foreach (var e in report_list)
            {
                e.percent_male = Math.Round((100.0 * e.total_male / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_female = Math.Round((100.0 * e.total_female / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_pantawid = Math.Round((100.0 * e.total_pantawid / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_slp = Math.Round((100.0 * e.total_slp / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_ip = Math.Round((100.0 * e.total_ip / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_60_and_above = Math.Round((100.0 * e.total_60_and_above / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_40_to_59_yo = Math.Round((100.0 * e.total_40_to_59_yo / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_21_to_39_yo = Math.Round((100.0 * e.total_21_to_39_yo / (e.total_participants)), 2).ToString("#.0\\%");
                e.percent_20_and_below = Math.Round((100.0 * e.total_20_and_below / (e.total_participants)), 2).ToString("#.0\\%");

            }

            return Ok(new queriedTalakayanReport<report_3>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
            });

            //return Ok(report_list);
        }
        #endregion

        private string verbal(double v)
        {

            if (v >= 3.51 && v <= 4.00)
            {
                return "Strongly Agree";
            }
            else if (v >= 2.51 && v <= 3.50)
            {
                return "Agree";
            }
            else if (v >= 1.51 && v <= 2.50)
            {
                return "Disagree";
            }
            else if (v >= 1.00 && v <= 1.50)
            {
                return "Strongly Disagree";
            }
            return "";
        }

        public class report_4
        {
            public int year { get; set; }
            public string item { get; set; }
            public string category { get; set; }
            public int total_num_of_response { get; set; }
            public int strongly_disagree { get; set; }
            public int disagree { get; set; }
            public int agree { get; set; }
            public int strongly_agree { get; set; }
            public double average { get; set; }
            public double net_agreement_disagreement { get; set; } //-----added 08/18/17 for v3.0 as additional requirement by Eval Team
            public string verbal_interpretation { get; set; }

            public string percent_strongly_disagree { get; set; }
            public string percent_disagree { get; set; }
            public string percent_agree { get; set; }
            public string percent_strongly_agree { get; set; }
        }

        #region Report 4 2015
        [HttpPost]
        [Route("api/export/perception/report_4_2015")]
        public IActionResult export_report_4_2015(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_4>();

            model = model.Where(x => x.year == 2015);

            var trust_2 = new report_4
            {
                item = "The community trusts the local officials in planning, implementation, monitoring and reporting of programs and projects for the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_2 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_2 == 1),
                disagree = model.Count(c => c.trust_2 == 2),
                agree = model.Count(c => c.trust_2 == 3),
                strongly_agree = model.Count(c => c.trust_2 == 4),

            };

            var trust_4 = new report_4
            {
                item = "Local officials are fair in dealing with the people",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_4 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_4 == 1),
                disagree = model.Count(c => c.trust_4 == 2),
                agree = model.Count(c => c.trust_4 == 3),
                strongly_agree = model.Count(c => c.trust_4 == 4),
            };
            var trust_5 = new report_4
            {
                item = "Barangay officials regularly meet with the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_5 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_5 == 1),
                disagree = model.Count(c => c.trust_5 == 2),
                agree = model.Count(c => c.trust_5 == 3),
                strongly_agree = model.Count(c => c.trust_5 == 4),

            };
            var trust_6 = new report_4
            {
                item = "Municipal officials regularly meet with the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_6 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_6 == 1),
                disagree = model.Count(c => c.trust_6 == 2),
                agree = model.Count(c => c.trust_6 == 3),
                strongly_agree = model.Count(c => c.trust_6 == 4),

            };
            var trust_7 = new report_4
            {
                item = "I trust the local officials in planning, implementation, monitoring and reporting of programs and projects for the barangay",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_7 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_7 == 1),
                disagree = model.Count(c => c.trust_7 == 2),
                agree = model.Count(c => c.trust_7 == 3),
                strongly_agree = model.Count(c => c.trust_7 == 4),

            };

            var access_1 = new report_4
            {
                item = "There is an adequate number of health facilities in our barangay",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_1 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_1 == 1),
                disagree = model.Count(c => c.access_1 == 2),
                agree = model.Count(c => c.access_1 == 3),
                strongly_agree = model.Count(c => c.access_1 == 4),

            };
            var access_3 = new report_4
            {
                item = "There is adequate number of educational facilities",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_3 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_3 == 1),
                disagree = model.Count(c => c.access_3 == 2),
                agree = model.Count(c => c.access_3 == 3),
                strongly_agree = model.Count(c => c.access_3 == 4),

            };
            var access_5 = new report_4
            {
                item = "There is adequate number of roads accessed by public vehicles (e.g. transportation of public goods)",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_5 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_5 == 1),
                disagree = model.Count(c => c.access_5 == 2),
                agree = model.Count(c => c.access_5 == 3),
                strongly_agree = model.Count(c => c.access_5 == 4),

            };
            var access_7 = new report_4
            {
                item = "Potable water is accessible to the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_7 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_7 == 1),
                disagree = model.Count(c => c.access_7 == 2),
                agree = model.Count(c => c.access_7 == 3),
                strongly_agree = model.Count(c => c.access_7 == 4),

            };
            var access_8 = new report_4
            {
                item = "There is peace and order in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_8 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_8 == 1),
                disagree = model.Count(c => c.access_8 == 2),
                agree = model.Count(c => c.access_8 == 3),
                strongly_agree = model.Count(c => c.access_8 == 4),

            };
            var access_9 = new report_4
            {
                item = "It is easy to go to a health facility",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_9 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_9 == 1),
                disagree = model.Count(c => c.access_9 == 2),
                agree = model.Count(c => c.access_9 == 3),
                strongly_agree = model.Count(c => c.access_9 == 4),

            };
            var access_11 = new report_4
            {
                item = "Children in our household were able to go to school in less time",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_11 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_11 == 1),
                disagree = model.Count(c => c.access_11 == 2),
                agree = model.Count(c => c.access_11 == 3),
                strongly_agree = model.Count(c => c.access_11 == 4),

            };
            var access_12 = new report_4
            {
                item = "Our household is able to purchase basic necessities in the market/nearby store",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_12 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_12 == 1),
                disagree = model.Count(c => c.access_12 == 2),
                agree = model.Count(c => c.access_12 == 3),
                strongly_agree = model.Count(c => c.access_12 == 4),

            };
            var access_14 = new report_4
            {
                item = "I feel secure in the community",
                total_num_of_response = model.Count(c => c.access_14 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",
                year = 2015,

                strongly_disagree = model.Count(c => c.access_14 == 1),
                disagree = model.Count(c => c.access_14 == 2),
                agree = model.Count(c => c.access_14 == 3),
                strongly_agree = model.Count(c => c.access_14 == 4),

            };
            var access_15 = new report_4
            {
                item = "Our household has access to potable water",
                year = 2015,
                total_num_of_response = model.Count(c => c.access_15 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_15 == 1),
                disagree = model.Count(c => c.access_15 == 2),
                agree = model.Count(c => c.access_15 == 3),
                strongly_agree = model.Count(c => c.access_15 == 4),

            };

            var participation_2 = new report_4
            {
                item = "Women are engaged in the implementation of programs/projects in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_2 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_2 == 1),
                disagree = model.Count(c => c.participation_2 == 2),
                agree = model.Count(c => c.participation_2 == 3),
                strongly_agree = model.Count(c => c.participation_2 == 4),

            };
            var participation_3 = new report_4
            {
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_3 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_3 == 1),
                disagree = model.Count(c => c.participation_3 == 2),
                agree = model.Count(c => c.participation_3 == 3),
                strongly_agree = model.Count(c => c.participation_3 == 4),

            };
            var participation_4 = new report_4
            {
                item = "The poorest are represented in the barangay assembly",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_4 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_4 == 1),
                disagree = model.Count(c => c.participation_4 == 2),
                agree = model.Count(c => c.participation_4 == 3),
                strongly_agree = model.Count(c => c.participation_4 == 4),

            };
            var participation_6 = new report_4
            {
                item = "Members of the community are provided with various skills training (e.g. livelihood, financial literacy among others)",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_6 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_6 == 1),
                disagree = model.Count(c => c.participation_6 == 2),
                agree = model.Count(c => c.participation_6 == 3),
                strongly_agree = model.Count(c => c.participation_6 == 4),

            };
            var participation_7 = new report_4
            {
                item = "I am able to participate in the implementation of programs/projects in the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_7 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_7 == 1),
                disagree = model.Count(c => c.participation_7 == 2),
                agree = model.Count(c => c.participation_7 == 3),
                strongly_agree = model.Count(c => c.participation_7 == 4),

            };
            var participation_9 = new report_4
            {
                item = "I am not shy when participating in community development activities",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_9 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_9 == 1),
                disagree = model.Count(c => c.participation_9 == 2),
                agree = model.Count(c => c.participation_9 == 3),
                strongly_agree = model.Count(c => c.participation_9 == 4),

            };
            var participation_11 = new report_4
            {
                item = "I have benefitted from the trainings/activities provided to the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_11 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_11 == 1),
                disagree = model.Count(c => c.participation_11 == 2),
                agree = model.Count(c => c.participation_11 == 3),
                strongly_agree = model.Count(c => c.participation_11 == 4),

            };
            var participation_12 = new report_4
            {
                item = "(If answer to item above is 3 or 4 only) My skills have improved because of these trainings",
                year = 2015,
                total_num_of_response = model.Count(c => c.participation_12 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_12 == 1),
                disagree = model.Count(c => c.participation_12 == 2),
                agree = model.Count(c => c.participation_12 == 3),
                strongly_agree = model.Count(c => c.participation_12 == 4),

            };

            var disaster_3 = new report_4
            {
                item = "People in the community are aware of the dangers brought by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_3 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_3 == 1),
                disagree = model.Count(c => c.disaster_3 == 2),
                agree = model.Count(c => c.disaster_3 == 3),
                strongly_agree = model.Count(c => c.disaster_3 == 4),

            };
            var disaster_4 = new report_4
            {
                item = "I am aware of the dangers brought by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_4 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_4 == 1),
                disagree = model.Count(c => c.disaster_4 == 2),
                agree = model.Count(c => c.disaster_4 == 3),
                strongly_agree = model.Count(c => c.disaster_4 == 4),

            };
            var disaster_5 = new report_4
            {
                item = "People are aware of how the community is affected by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_5 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_5 == 1),
                disagree = model.Count(c => c.disaster_5 == 2),
                agree = model.Count(c => c.disaster_5 == 3),
                strongly_agree = model.Count(c => c.disaster_5 == 4),

            };
            var disaster_6 = new report_4
            {
                item = "I am aware of how the community is affected by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_6 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_6 == 1),
                disagree = model.Count(c => c.disaster_6 == 2),
                agree = model.Count(c => c.disaster_6 == 3),
                strongly_agree = model.Count(c => c.disaster_6 == 4),

            };
            var disaster_7 = new report_4
            {
                item = "I am aware of the different measures to reduce dangers brought by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_7 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_7 == 1),
                disagree = model.Count(c => c.disaster_7 == 2),
                agree = model.Count(c => c.disaster_7 == 3),
                strongly_agree = model.Count(c => c.disaster_7 == 4),

            };
            var disaster_8 = new report_4
            {
                item = "People in the community are aware of the different measures to reduce dangers brought by disasters",
                year = 2015,
                total_num_of_response = model.Count(c => c.disaster_8 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_8 == 1),
                disagree = model.Count(c => c.disaster_8 == 2),
                agree = model.Count(c => c.disaster_8 == 3),
                strongly_agree = model.Count(c => c.disaster_8 == 4),
            };

            report_list.Add(trust_2);
            report_list.Add(trust_4);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_7);
            report_list.Add(access_1);
            report_list.Add(access_3);
            report_list.Add(access_5);
            report_list.Add(access_7);
            report_list.Add(access_8);
            report_list.Add(access_9);
            report_list.Add(access_11);
            report_list.Add(access_12);
            report_list.Add(access_14);
            report_list.Add(access_15);
            report_list.Add(participation_2);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_6);
            report_list.Add(participation_7);
            report_list.Add(participation_9);
            report_list.Add(participation_11);
            report_list.Add(participation_12);
            report_list.Add(disaster_3);
            report_list.Add(disaster_4);
            report_list.Add(disaster_5);
            report_list.Add(disaster_6);
            report_list.Add(disaster_7);
            report_list.Add(disaster_8);

            foreach (var e in report_list)
            {
                e.total_num_of_response = e.strongly_disagree + e.disagree + e.agree + e.strongly_agree;
                e.average = Math.Round((e.strongly_disagree * 1 + e.disagree * 2 + e.agree * 3 + e.strongly_agree * 4) /
                    (e.total_num_of_response + 0.00), 2);
                e.verbal_interpretation = verbal(e.average);

                e.percent_strongly_disagree = Math.Round((100.0 * e.strongly_disagree / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_disagree = Math.Round((100.0 * e.disagree / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_agree = Math.Round((100.0 * e.agree / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_agree = Math.Round((100.0 * e.strongly_agree / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.net_agreement_disagreement = ((e.strongly_agree + e.agree) - (e.strongly_disagree + e.disagree));

            }

            return Ok(new queriedTalakayanReport<report_4>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
            });

            //return Ok(report_list);
        }

        #endregion

        #region Report 4 2016
        [HttpPost]
        [Route("api/export/perception/report_4_2016")]
        public IActionResult export_report_4_2016(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_4>();

            model = model.Where(x => x.year == 2016);

            var trust_1 = new report_4
            {
                item = "Barangay residents believe that our local officials are working for the benefit of our barangay",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_1 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_1 == 1),
                disagree = model.Count(c => c.trust_1 == 2),
                agree = model.Count(c => c.trust_1 == 3),
                strongly_agree = model.Count(c => c.trust_1 == 4),

            };
            var trust_3 = new report_4
            {
                item = "Local officials are not fair in dealing with the people",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_3 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_3 == 1),
                disagree = model.Count(c => c.trust_3 == 2),
                agree = model.Count(c => c.trust_3 == 3),
                strongly_agree = model.Count(c => c.trust_3 == 4),

            };
            var trust_5 = new report_4
            {
                item = "Barangay officials regularly meet with the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.trust_5 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_5 == 1),
                disagree = model.Count(c => c.trust_5 == 2),
                agree = model.Count(c => c.trust_5 == 3),
                strongly_agree = model.Count(c => c.trust_5 == 4),

            };
            var trust_6 = new report_4
            {
                item = "Municipal officials regularly meet with the community",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_6 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_6 == 1),
                disagree = model.Count(c => c.trust_6 == 2),
                agree = model.Count(c => c.trust_6 == 3),
                strongly_agree = model.Count(c => c.trust_6 == 4),

            };
            var trust_8 = new report_4
            {
                item = "I don't believe that the local officials are working for the benefit of our barangay.",
                year = 2015,
                total_num_of_response = model.Count(c => c.trust_8 != 0),
                category = "TRUST AND CONFIDENCE ON OFFICIALS",

                strongly_disagree = model.Count(c => c.trust_8 == 1),
                disagree = model.Count(c => c.trust_8 == 2),
                agree = model.Count(c => c.trust_8 == 3),
                strongly_agree = model.Count(c => c.trust_8 == 4),

            };

            var access_1 = new report_4
            {
                item = "There is an adequate number of health facilities in our barangay",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_1 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_1 == 1),
                disagree = model.Count(c => c.access_1 == 2),
                agree = model.Count(c => c.access_1 == 3),
                strongly_agree = model.Count(c => c.access_1 == 4),

            };
            var access_2 = new report_4
            {
                item = "The number of educational facilities in our barangay is not enough",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_2 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_2 == 1),
                disagree = model.Count(c => c.access_2 == 2),
                agree = model.Count(c => c.access_2 == 3),
                strongly_agree = model.Count(c => c.access_2 == 4),

            };
            var access_4 = new report_4
            {
                item = "Our barangay has adequate an number of roads accessed by public vehicles (e.g. transportation of public goods)",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_4 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_4 == 1),
                disagree = model.Count(c => c.access_4 == 2),
                agree = model.Count(c => c.access_4 == 3),
                strongly_agree = model.Count(c => c.access_4 == 4),

            };
            var access_6 = new report_4
            {
                item = "Potable water is not available to the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_6 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_6 == 1),
                disagree = model.Count(c => c.access_6 == 2),
                agree = model.Count(c => c.access_6 == 3),
                strongly_agree = model.Count(c => c.access_6 == 4),

            };
            var access_8 = new report_4
            {
                item = "There is peace and order in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_8 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_8 == 1),
                disagree = model.Count(c => c.access_8 == 2),
                agree = model.Count(c => c.access_8 == 3),
                strongly_agree = model.Count(c => c.access_8 == 4),

            };
            var access_10 = new report_4
            {
                item = "School-aged children in our household are able to go to school",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_10 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_10 == 1),
                disagree = model.Count(c => c.access_10 == 2),
                agree = model.Count(c => c.access_10 == 3),
                strongly_agree = model.Count(c => c.access_10 == 4),

            };
            var access_12 = new report_4
            {
                item = "Our household is able to purchase basic necessities in the market/nearby store",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_12 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_12 == 1),
                disagree = model.Count(c => c.access_12 == 2),
                agree = model.Count(c => c.access_12 == 3),
                strongly_agree = model.Count(c => c.access_12 == 4),

            };
            var access_13 = new report_4
            {
                item = "I don’t feel secure from crimes in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_13 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_13 == 1),
                disagree = model.Count(c => c.access_13 == 2),
                agree = model.Count(c => c.access_13 == 3),
                strongly_agree = model.Count(c => c.access_13 == 4),

            };
            var access_15 = new report_4
            {
                item = "Our household has access to potable water",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_15 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_15 == 1),
                disagree = model.Count(c => c.access_15 == 2),
                agree = model.Count(c => c.access_15 == 3),
                strongly_agree = model.Count(c => c.access_15 == 4),

            };
            var access_16 = new report_4
            {
                item = "If I or any of my household is sick, we can easily go to a health center",
                year = 2016,
                total_num_of_response = model.Count(c => c.access_16 != 0),
                category = "ACCESS TO SERVICES AND GOVERNANCE",

                strongly_disagree = model.Count(c => c.access_16 == 1),
                disagree = model.Count(c => c.access_16 == 2),
                agree = model.Count(c => c.access_16 == 3),
                strongly_agree = model.Count(c => c.access_16 == 4),

            };

            var participation_1 = new report_4
            {
                item = "Women are excluded from the implementation of programs/projects in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_1 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_1 == 1),
                disagree = model.Count(c => c.participation_1 == 2),
                agree = model.Count(c => c.participation_1 == 3),
                strongly_agree = model.Count(c => c.participation_1 == 4),

            };
            var participation_3 = new report_4
            {
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_3 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_3 == 1),
                disagree = model.Count(c => c.participation_3 == 2),
                agree = model.Count(c => c.participation_3 == 3),
                strongly_agree = model.Count(c => c.participation_3 == 4),

            };
            var participation_4 = new report_4
            {
                item = "The poorest are represented in the barangay assembly",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_4 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_4 == 1),
                disagree = model.Count(c => c.participation_4 == 2),
                agree = model.Count(c => c.participation_4 == 3),
                strongly_agree = model.Count(c => c.participation_4 == 4),

            };
            var participation_5 = new report_4
            {
                item = "Members of the community are not given various skills training (e.g. livelihood, financial literacy, etc.)",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_5 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_5 == 1),
                disagree = model.Count(c => c.participation_5 == 2),
                agree = model.Count(c => c.participation_5 == 3),
                strongly_agree = model.Count(c => c.participation_5 == 4),

            };
            var participation_7 = new report_4
            {
                item = "I am able to participate in the implementation of programs/projects in the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_7 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_7 == 1),
                disagree = model.Count(c => c.participation_7 == 2),
                agree = model.Count(c => c.participation_7 == 3),
                strongly_agree = model.Count(c => c.participation_7 == 4),

            };
            var participation_8 = new report_4
            {
                item = "I am shy when participating in community development activities",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_8 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_8 == 1),
                disagree = model.Count(c => c.participation_8 == 2),
                agree = model.Count(c => c.participation_8 == 3),
                strongly_agree = model.Count(c => c.participation_8 == 4),

            };
            var participation_10 = new report_4
            {
                item = "I attend trainings/activities (e.g. livelihood, financial literacy, etc.) provided to the community",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_10 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_10 == 1),
                disagree = model.Count(c => c.participation_10 == 2),
                agree = model.Count(c => c.participation_10 == 3),
                strongly_agree = model.Count(c => c.participation_10 == 4),

            };
            var participation_12 = new report_4
            {
                item = "(If answer to item above is 3 or 4 only) My skills have improved because of these trainings",
                year = 2016,
                total_num_of_response = model.Count(c => c.participation_12 != 0),
                category = "PARTICIPATION AND EMPOWERMENT TO THE POOR AND MARGINALIZED",

                strongly_disagree = model.Count(c => c.participation_12 == 1),
                disagree = model.Count(c => c.participation_12 == 2),
                agree = model.Count(c => c.participation_12 == 3),
                strongly_agree = model.Count(c => c.participation_12 == 4),

            };

            var disaster_1 = new report_4
            {
                item = "People in the community are aware of the dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_1 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_1 == 1),
                disagree = model.Count(c => c.disaster_1 == 2),
                agree = model.Count(c => c.disaster_1 == 3),
                strongly_agree = model.Count(c => c.disaster_1 == 4),

            };
            var disaster_2 = new report_4
            {
                item = "People in the community are not aware of the different measures to reduce dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_2 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_2 == 1),
                disagree = model.Count(c => c.disaster_2 == 2),
                agree = model.Count(c => c.disaster_2 == 3),
                strongly_agree = model.Count(c => c.disaster_2 == 4),

            };
            var disaster_4 = new report_4
            {
                item = "I am aware of the dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_4 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_4 == 1),
                disagree = model.Count(c => c.disaster_4 == 2),
                agree = model.Count(c => c.disaster_4 == 3),
                strongly_agree = model.Count(c => c.disaster_4 == 4),

            };
            var disaster_7 = new report_4
            {
                item = "I am aware of the different measures to reduce dangers brought by disasters",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_7 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_7 == 1),
                disagree = model.Count(c => c.disaster_7 == 2),
                agree = model.Count(c => c.disaster_7 == 3),
                strongly_agree = model.Count(c => c.disaster_7 == 4),

            };
            var disaster_9 = new report_4
            {
                item = "I think my barangay is not ready for any possible disaster ",
                year = 2016,
                total_num_of_response = model.Count(c => c.disaster_9 != 0),
                category = "DISASTER-RISK RELATED ACTIVITIES AND PREPAREDNESS",

                strongly_disagree = model.Count(c => c.disaster_9 == 1),
                disagree = model.Count(c => c.disaster_9 == 2),
                agree = model.Count(c => c.disaster_9 == 3),
                strongly_agree = model.Count(c => c.disaster_9 == 4),

            };

            report_list.Add(trust_1);
            report_list.Add(trust_3);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_8);
            report_list.Add(access_1);
            report_list.Add(access_2);
            report_list.Add(access_4);
            report_list.Add(access_6);
            report_list.Add(access_8);
            report_list.Add(access_10);
            report_list.Add(access_12);
            report_list.Add(access_13);
            report_list.Add(access_15);
            report_list.Add(access_16);
            report_list.Add(participation_1);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_5);
            report_list.Add(participation_7);
            report_list.Add(participation_8);
            report_list.Add(participation_10);
            report_list.Add(participation_12);
            report_list.Add(disaster_1);
            report_list.Add(disaster_2);
            report_list.Add(disaster_4);
            report_list.Add(disaster_7);
            report_list.Add(disaster_9);


            foreach (var e in report_list)
            {
                e.total_num_of_response = e.strongly_disagree + e.disagree + e.agree + e.strongly_agree;
                e.average = Math.Round((e.strongly_disagree * 1 + e.disagree * 2 + e.agree * 3 + e.strongly_agree * 4) /
                    (e.total_num_of_response + 0.00), 2);
                e.verbal_interpretation = verbal(e.average);

                e.percent_strongly_disagree = Math.Round((100.0 * e.strongly_disagree / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_disagree = Math.Round((100.0 * e.disagree / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_agree = Math.Round((100.0 * e.agree / (e.total_num_of_response)), 2).ToString("#.0\\%");
                e.percent_strongly_agree = Math.Round((100.0 * e.strongly_agree / (e.total_num_of_response)), 2).ToString("#.0\\%");

                e.net_agreement_disagreement = ((e.strongly_agree + e.agree) - (e.strongly_disagree + e.disagree));

            }

            return Ok(new queriedTalakayanReport<report_4>()
            {
                Items = report_list,
                maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
            });

            //return Ok(report_list);
        }
        #endregion
        
        public class report_5
        {
            public string person_name { get; set; }
            public int? age { get; set; }
            public bool? sex { get; set; }
            public bool? is_pantawid { get; set; }
            public bool? is_slp { get; set; }
            public bool? is_ip { get; set; }
            public int year { get; set; }

            public string category { get; set; }
            public string item { get; set; }

            public int? trust_1 { get; set; }
            public int? trust_2 { get; set; }
            public int? trust_3 { get; set; }
            public int? trust_4 { get; set; }
            public int? trust_5 { get; set; }
            public int? trust_6 { get; set; }
            public int? trust_7 { get; set; }
            public int? trust_8 { get; set; }

            public int? access_1 { get; set; }
            public int? access_2 { get; set; }
            public int? access_3 { get; set; }
            public int? access_4 { get; set; }
            public int? access_5 { get; set; }
            public int? access_6 { get; set; }
            public int? access_7 { get; set; }
            public int? access_8 { get; set; }
            public int? access_9 { get; set; }
            public int? access_10 { get; set; }
            public int? access_11 { get; set; }
            public int? access_12 { get; set; }
            public int? access_13 { get; set; }
            public int? access_14 { get; set; }
            public int? access_15 { get; set; }
            public int? access_16 { get; set; }

            public int? participation_1 { get; set; }
            public int? participation_2 { get; set; }
            public int? participation_3 { get; set; }
            public int? participation_4 { get; set; }
            public int? participation_5 { get; set; }
            public int? participation_6 { get; set; }
            public int? participation_7 { get; set; }
            public int? participation_8 { get; set; }
            public int? participation_9 { get; set; }
            public int? participation_10 { get; set; }
            public int? participation_11 { get; set; }
            public int? participation_12 { get; set; }

            public int? disaster_1 { get; set; }
            public int? disaster_2 { get; set; }
            public int? disaster_3 { get; set; }
            public int? disaster_4 { get; set; }
            public int? disaster_5 { get; set; }
            public int? disaster_6 { get; set; }
            public int? disaster_7 { get; set; }
            public int? disaster_8 { get; set; }
            public int? disaster_9 { get; set; }

        }

        #region Report 5, year 2015
        [HttpPost]
        [Route("api/export/perception/report_5_2015")]
        public IActionResult export_report_5_2015(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_5>();

            model = model.Where(x => x.year == 2015);

            //2015 version:

            var trust_2 = new report_5
            {
                year = 2015,
                item = "The community trusts the local officials in planning, implementation, monitoring and reporting of programs and projects for the community",
            };
            var trust_4 = new report_5
            {
                year = 2015,
                item = "Local officials are fair in dealing with the people",
            };
            var trust_5 = new report_5
            {
                year = 2015,
                item = "Barangay officials regularly meet with the community",
            };
            var trust_6 = new report_5
            {
                year = 2015,
                item = "Municipal officials regularly meet with the community",
            };
            var trust_7 = new report_5
            {
                year = 2015,
                item = "I trust the local officials in planning, implementation, monitoring and reporting of programs and projects for the barangay",
            };
            var access_1 = new report_5
            {
                year = 2015,
                item = "There is an adequate number of health facilities in our barangay",
            };
            var access_3 = new report_5
            {
                year = 2015,
                item = "There is adequate number of educational facilities",
            };
            var access_5 = new report_5
            {
                year = 2015,
                item = "There is adequate number of roads accessed by public vehicles (e.g. transportation of public goods)",
            };
            var access_7 = new report_5
            {
                year = 2015,
                item = "Potable water is accessible to the community",
            };
            var access_8 = new report_5
            {
                year = 2015,
                item = "There is peace and order in the community",
            };
            var access_9 = new report_5
            {
                year = 2015,
                item = "It is easy to go to a health facility",
            };
            var access_11 = new report_5
            {
                year = 2015,
                item = "Children in our household were able to go to school in less time",
            };
            var access_12 = new report_5
            {
                year = 2015,
                item = "Our household is able to purchase basic necessities in the market/nearby store",
            };
            var access_14 = new report_5
            {
                year = 2015,
                item = "I feel secure in the community",
            };
            var access_15 = new report_5
            {
                year = 2015,
                item = "Our household has access to potable water",
            };
            var participation_2 = new report_5
            {
                year = 2015,
                item = "Women are engaged in the implementation of programs/projects in the community",
            };
            var participation_3 = new report_5
            {
                year = 2015,
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
            };
            var participation_4 = new report_5
            {
                year = 2015,
                item = "The poorest are represented in the barangay assembly",
            };
            var participation_6 = new report_5
            {
                year = 2015,
                item = "Members of the community are provided with various skills training (e.g. livelihood, financial literacy among others)",
            };
            var participation_7 = new report_5
            {
                year = 2015,
                item = "I am able to participate in the implementation of programs/projects in the community",
            };
            var participation_9 = new report_5
            {
                year = 2015,
                item = "I am not shy when participating in community development activities",
            };
            var participation_11 = new report_5
            {
                year = 2015,
                item = "I have benefitted from the trainings/activities provided to the community",
            };
            var participation_12 = new report_5
            {
                year = 2015,
                item = "(If answer to previous item is 3 or 4 only) My skills have improved because of these trainings",
            };
            var disaster_3 = new report_5
            {
                year = 2015,
                item = "People in the community are aware of the dangers brought by disasters",
            };
            var disaster_4 = new report_5
            {
                year = 2015,
                item = "I am aware of the dangers brought by disasters",
            };
            var disaster_5 = new report_5
            {
                year = 2015,
                item = "People are aware of how the community is affected by disasters",
            };
            var disaster_6 = new report_5
            {
                year = 2015,
                item = "I am aware of how the community is affected by disasters",
            };
            var disaster_7 = new report_5
            {
                year = 2015,
                item = "I am aware of the different measures to reduce dangers brought by disasters",
            };
            var disaster_8 = new report_5
            {
                year = 2015,
                item = "People in the community are aware of the different measures to reduce dangers brought by disasters",
            };
                        
            report_list.Add(trust_2);
            report_list.Add(trust_4);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_7);

            report_list.Add(access_1);
            report_list.Add(access_3);
            report_list.Add(access_5);
            report_list.Add(access_7);
            report_list.Add(access_8);
            report_list.Add(access_9);
            report_list.Add(access_11);
            report_list.Add(access_12);
            report_list.Add(access_14);
            report_list.Add(access_15);

            report_list.Add(participation_2);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_6);
            report_list.Add(participation_7);
            report_list.Add(participation_9);
            report_list.Add(participation_11);
            report_list.Add(participation_12);

            report_list.Add(disaster_3);
            report_list.Add(disaster_4);
            report_list.Add(disaster_5);
            report_list.Add(disaster_6);
            report_list.Add(disaster_7);
            report_list.Add(disaster_8);

            foreach (var e in report_list)
            {
                var result = model.Select(x => new
                {
                    year = x.year,
                    person_name = x.person_name,
                    age = x.age,
                    sex = x.is_male,
                    is_ip = x.is_ip,
                    is_pantawid = x.is_pantawid,
                    is_slp = x.is_slp,

                    talakayan_date_from = x.talakayan_date_from == null ? "" : x.talakayan_date_from.Value.ToString("dd/MM/yyyy"),
                    date_of_survey = x.survey_date_from == null ? "" : x.survey_date_from.Value.ToString("dd/MM/yyyy"),

                    //question = e.item,
                    
                    trust_2 = x.trust_2,
                    trust_4 = x.trust_4,
                    trust_5 = x.trust_5,
                    trust_6 = x.trust_6,
                    trust_7 = x.trust_7,
                    
                    access_1 = x.access_1,
                    access_3 = x.access_3,
                    access_5 = x.access_5,
                    access_7 = x.access_7,
                    access_8 = x.access_8,
                    access_9 = x.access_9,
                    access_11 = x.access_11,
                    access_12 = x.access_12,
                    access_14 = x.access_14,
                    access_15 = x.access_15,
                    
                    participation_2 = x.participation_2,
                    participation_3 = x.participation_3,
                    participation_4 = x.participation_4,
                    participation_6 = x.participation_6,
                    participation_7 = x.participation_7,
                    participation_9 = x.participation_9,
                    participation_11 = x.participation_11,
                    participation_12 = x.participation_12,
                    
                    disaster_3 = x.disaster_3,
                    disaster_4 = x.disaster_4,
                    disaster_5 = x.disaster_5,
                    disaster_6 = x.disaster_6,
                    disaster_7 = x.disaster_7,
                    disaster_8 = x.disaster_8,

                }).ToList();

                //return Ok(result);

                return Ok(new queriedTalakayanReport<dynamic>()
                {
                    Items = result,
                    maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                    minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                });

            }

            return Ok(report_list);

        }

        #endregion

        #region Report 5, year 2016
        [HttpPost]
        [Route("api/export/perception/report_5_2016")]
        public IActionResult export_report_5_2016(AngularFilterModel filt)
        {
            var model = GetData(filt);

            var report_list = new List<report_5>();

            model = model.Where(x => x.year == 2016);
            
            var trust_1 = new report_5
            {
                year = 2016,
                item = "Barangay residents believe that our local officials are working for the benefit of our barangay",
            };
            var trust_3 = new report_5
            {
                year = 2016,
                item = "Local officials are not fair in dealing with the people",
            };
            var trust_5 = new report_5
            {
                year = 2016,
                item = "Barangay officials regularly meet with the community",
            };
            var trust_6 = new report_5
            {
                year = 2016,
                item = "Municipal officials regularly meet with the community",
            };
            var trust_8 = new report_5
            {
                year = 2016,
                item = "I don't believe that the local officials are working for the benefit of our barangay.",
            };
            var access_1 = new report_5
            {
                year = 2016,
                item = "There is an adequate number of health facilities in our barangay",
            };
            var access_2 = new report_5
            {
                year = 2016,
                item = "The number of educational facilities in our barangay is not enough",
            };
            var access_4 = new report_5
            {
                year = 2016,
                item = "Our barangay has adequate an number of roads accessed by public vehicles (e.g. transportation of public goods)",
            };
            var access_6 = new report_5
            {
                year = 2016,
                item = "Potable water is not available to the community",
            };
            var access_8 = new report_5
            {
                year = 2016,
                item = "There is peace and order in the community",
            };
            var access_10 = new report_5
            {
                year = 2016,
                item = "School-aged children in our household are able to go to school",
            };
            var access_12 = new report_5
            {
                year = 2016,
                item = "Our household is able to purchase basic necessities in the market/nearby store",
            };
            var access_13 = new report_5
            {
                year = 2016,
                item = "I don’t feel secure from crimes in the community",
            };
            var access_15 = new report_5
            {
                year = 2016,
                item = "Our household has access to potable water",
            };
            var access_16 = new report_5
            {
                year = 2016,
                item = "If I or any of my household is sick, we can easily go to a health center",
            };
            var participation_1 = new report_5
            {
                year = 2016,
                item = "Women are excluded from the implementation of programs/projects in the community",
            };
            var participation_3 = new report_5
            {
                year = 2016,
                item = "(FOR IP MEMBERS ONLY) IPs are engaged in the implementation of programs/projects in the community",
            };
            var participation_4 = new report_5
            {
                year = 2016,
                item = "The poorest are represented in the barangay assembly",
            };
            var participation_5 = new report_5
            {
                year = 2016,
                item = "Members of the community are not given various skills training (e.g. livelihood, financial literacy, etc.)",
            };
            var participation_7 = new report_5
            {
                year = 2016,
                item = "I am able to participate in the implementation of programs/projects in the community",
            };
            var participation_8 = new report_5
            {
                year = 2016,
                item = "I am shy when participating in community development activities",
            };
            var participation_10 = new report_5
            {
                year = 2016,
                item = "I attend trainings/activities (e.g. livelihood, financial literacy, etc.) provided to the community",
            };
            var participation_12 = new report_5
            {
                year = 2016,
                item = "(If answer to previous item is 3 or 4 only) My skills have improved because of these trainings",
            };
            var disaster_1 = new report_5
            {
                year = 2016,
                item = "People in the community are aware of the dangers brought by disasters",
            };
            var disaster_2 = new report_5
            {
                year = 2016,
                item = "People in the community are not aware of the different measures to reduce dangers brought by disasters",
            };
            var disaster_4 = new report_5
            {
                year = 2016,
                item = "I am aware of the dangers brought by disasters",
            };
            var disaster_7 = new report_5
            {
                year = 2016,
                item = "I am aware of the different measures to reduce dangers brought by disasters",
            };
            var disaster_9 = new report_5
            {
                year = 2016,
                item = "I think my barangay is not ready for any possible disaster ",
            };

            //var result = model.Select(x => new
            //{
            //    year = x.year,
            //    person_name = x.person_name,
            //    age = x.age,
            //    sex = x.is_male,
            //    is_ip = x.is_ip,
            //    is_pantawid = x.is_pantawid,
            //    is_slp = x.is_slp,

            //    trust_1 = x.trust_1,
            //    trust_2 = x.trust_2,
            //    trust_3 = x.trust_3,
            //    trust_4 = x.trust_4,
            //    trust_5 = x.trust_5,
            //    trust_6 = x.trust_6,
            //    trust_7 = x.trust_7,
            //    trust_8 = x.trust_8,

            //    access_1 = x.access_1,
            //    access_2 = x.access_2,
            //    access_3 = x.access_3,
            //    access_4 = x.access_4,
            //    access_5 = x.access_5,
            //    access_6 = x.access_6,
            //    access_7 = x.access_7,
            //    access_8 = x.access_8,
            //    access_9 = x.access_9,
            //    access_10 = x.access_10,
            //    access_11 = x.access_11,
            //    access_12 = x.access_12,
            //    access_13 = x.access_13,
            //    access_14 = x.access_14,
            //    access_15 = x.access_15,
            //    access_16 = x.access_16,

            //    participation_1 = x.participation_1,
            //    participation_2 = x.participation_2,
            //    participation_3 = x.participation_3,
            //    participation_4 = x.participation_4,
            //    participation_5 = x.participation_5,
            //    participation_6 = x.participation_6,
            //    participation_7 = x.participation_7,
            //    participation_8 = x.participation_8,
            //    participation_9 = x.participation_9,
            //    participation_10 = x.participation_10,
            //    participation_11 = x.participation_11,
            //    participation_12 = x.participation_12,

            //    disaster_1 = x.disaster_1,
            //    disaster_2 = x.disaster_2,
            //    disaster_3 = x.disaster_3,
            //    disaster_4 = x.disaster_4,
            //    disaster_5 = x.disaster_5,
            //    disaster_6 = x.disaster_6,
            //    disaster_7 = x.disaster_7,
            //    disaster_8 = x.disaster_8,
            //    disaster_9 = x.disaster_9,

            //}).ToList();

            report_list.Add(trust_1);
            report_list.Add(trust_3);
            report_list.Add(trust_5);
            report_list.Add(trust_6);
            report_list.Add(trust_8);

            report_list.Add(access_1);
            report_list.Add(access_2);
            report_list.Add(access_4);
            report_list.Add(access_6);
            report_list.Add(access_8);
            report_list.Add(access_10);
            report_list.Add(access_12);
            report_list.Add(access_13);
            report_list.Add(access_15);
            report_list.Add(access_16);

            report_list.Add(participation_1);
            report_list.Add(participation_3);
            report_list.Add(participation_4);
            report_list.Add(participation_5);
            report_list.Add(participation_7);
            report_list.Add(participation_8);
            report_list.Add(participation_10);
            report_list.Add(participation_12);

            report_list.Add(disaster_1);
            report_list.Add(disaster_2);
            report_list.Add(disaster_4);
            report_list.Add(disaster_7);
            report_list.Add(disaster_9);

            foreach (var e in report_list)
            {
                var result = model.Select(x => new
                {
                    year = x.year,
                    person_name = x.person_name,
                    age = x.age,
                    sex = x.is_male,
                    is_ip = x.is_ip,
                    is_pantawid = x.is_pantawid,
                    is_slp = x.is_slp,

                    talakayan_date_from = x.talakayan_date_from == null ? "" : x.talakayan_date_from.Value.ToString("dd/MM/yyyy"),
                    date_of_survey = x.survey_date_from == null ? "" : x.survey_date_from.Value.ToString("dd/MM/yyyy"),

                    //question = e.item,

                    trust_1 = x.trust_1,
                    trust_3 = x.trust_3,
                    trust_5 = x.trust_5,
                    trust_6 = x.trust_6,
                    trust_8 = x.trust_8,

                    access_1 = x.access_1,
                    access_2 = x.access_2,
                    access_4 = x.access_4,
                    access_6 = x.access_6,
                    access_8 = x.access_8,
                    access_10 = x.access_10,
                    access_12 = x.access_12,
                    access_13 = x.access_13,
                    access_15 = x.access_15,
                    access_16 = x.access_16,

                    participation_1 = x.participation_1,
                    participation_3 = x.participation_3,
                    participation_4 = x.participation_4,
                    participation_5 = x.participation_5,
                    participation_7 = x.participation_7,
                    participation_8 = x.participation_8,
                    participation_10 = x.participation_10,
                    participation_12 = x.participation_12,

                    disaster_1 = x.disaster_1,
                    disaster_2 = x.disaster_2,
                    disaster_4 = x.disaster_4,
                    disaster_7 = x.disaster_7,
                    disaster_9 = x.disaster_9,

                }).ToList();

                //return Ok(result);

                return Ok(new queriedTalakayanReport<dynamic>()
                {
                    Items = result,
                    maxDate = model.OrderByDescending(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                    minDate = model.OrderBy(x => x.talakayan_date_from).Select(x => x.talakayan_date_from).FirstOrDefault(),
                });
            }

            return Ok(report_list);

        }

        #endregion
        
        //SAVE FUNCTION:
        [Route("api/offline/v1/perception_survey/save")]
        public async Task<IActionResult> Save(perception_survey model, bool? api)
        {
            var record = db.perception_survey.AsNoTracking().FirstOrDefault(x => x.perception_survey_id == model.perception_survey_id && x.is_deleted != true);

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

                db.perception_survey.Add(model);
                
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

        [Route("api/offline/v1/perception_survey/get")]
        public IActionResult Get(Guid id)
        {
            var model = db.perception_survey.FirstOrDefault(x => x.perception_survey_id == id && x.is_deleted != true);

            if (model == null)
            {
                var item = new perception_survey();

                item.perception_survey_id = id;
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
        [Route("api/offline/v1/perception_survey/get_details")]
        public IActionResult GetDetails(Guid id)
        {
            var model = db.perception_survey.FirstOrDefault(x => x.perception_survey_id == id);
            
            return Ok(model);
        }


        #region API GET / POST

        [HttpPost]
        [Route("Sync/Get/perception_survey")]
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/perception_survey/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<perception_survey>>(responseJson.Result);

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


        [Route("api/offline/v1/perception_survey/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid perception_survey_id, int push_status_id)
        {
            var perception = db.perception_survey.Find(perception_survey_id);

            if (perception == null)
            {
                return BadRequest("Error");
            }

            perception.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }


        //new:
        [HttpPost]
        [Route("Sync/Post/perception_survey")]
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

                var items_preselected = db.perception_survey.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.perception_survey.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/perception_survey/save", data).Result;
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
                    var items = db.perception_survey.Where(x => x.push_status_id == 5 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/perception_survey/save", data).Result;
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
