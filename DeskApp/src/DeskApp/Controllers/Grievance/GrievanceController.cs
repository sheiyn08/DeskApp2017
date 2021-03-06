﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DeskApp.Controllers.Grievance
//{
//    public class GrievanceController
//    {
//    }
//}

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
using Newtonsoft.Json;

namespace DeskApp.Controllers
{

    public class GrievanceController : Controller
    {

        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public GrievanceController(ApplicationDbContext context)
        {
            db = context;

        }

        #region DQA
        [Route("api/export/grievance_record/dqa")]
        public IActionResult export_dqa(AngularFilterModel item)
        {
            var model = GetData(item);

            DateTime today_A = (DateTime.Now).AddDays(-5);
            DateTime today_B = (DateTime.Now).AddDays(-10);
            var result = model.GroupBy(x => new
            {
                x.lib_region,
                x.lib_province,
                x.lib_city,
                x.brgy_code
            }).Select(x =>
               new
               {
                   region_name = x.Key.lib_region.region_name,

                   prov_name = x.Key.lib_province.prov_name,

                   city_name = x.Key.lib_city.city_name,
                   brgy_name = x.Key.brgy_code != null ? db.lib_brgy.FirstOrDefault(c => c.brgy_code == x.Key.brgy_code).brgy_name : "",

                   no_intake_date = x.Where(cx => cx.date_intake == null).Count(),
                   intake_date_advanced = x.Where(cx => cx.date_intake > DateTime.Now).Count(),

                   resolved_without_date = x.Where(c => c.grs_resolution_status_id == 1 && c.resolution_date == null).Count(),
                   resolved_without_resolution = x.Where(c => c.grs_resolution_status_id == 1 && c.actions == null).Count(),

                   resolved_in_advance = x.Where(c => c.resolution_date > DateTime.Now).Count(),

                   TYPE_A_Pending_Not_Resolved_after_5_days_intake = x.Where(c => c.date_intake != null && c.grs_resolution_status_id != 1 && c.grs_category_id == 1)
                  .Where(c => c.date_intake < today_A).Where(c => c.grs_form_id == 2).Count(),
                   TYPE_A_Pending_Not_Resolved_after_5_days_pincos = x.Where(c => c.date_intake != null && c.grs_resolution_status_id != 1 && c.grs_category_id == 1)
                  .Where(c => c.date_intake < today_A).Where(c => c.grs_form_id == 1).Count(),


                   TYPE_B_Pending_Not_Resolved_after_30_days_intake = x.Where(c => c.date_intake != null && c.grs_resolution_status_id != 1 && c.grs_category_id == 2)
                  .Where(c => c.date_intake < today_B).Where(c => c.grs_form_id == 2).Count(),

                   TYPE_B_Pending_Not_Resolved_after_30_days_pincos = x.Where(c => c.date_intake != null && c.grs_resolution_status_id != 1 && c.grs_category_id == 2)
                  .Where(c => c.date_intake < today_B).Where(c => c.grs_form_id == 1).Count(),

                   TYPE_C_Pending_Not_Resolved_after_60_days_intake = x.Where(c => c.date_intake != null && c.grs_resolution_status_id != 1 && c.grs_category_id == 3)
                  .Where(c => c.date_intake < today_B).Where(c => c.grs_form_id == 2).Count(),

                   TYPE_C_Pending_Not_Resolved_after_60_days_pincos = x.Where(c => c.date_intake != null && c.grs_resolution_status_id != 1 && c.grs_category_id == 3)
                  .Where(c => c.date_intake < today_B).Where(c => c.grs_form_id == 1).Count(),

               }
                ).ToList();

            var export = result.GroupBy(x => new { x.region_name, x.prov_name, x.city_name, x.brgy_name }).Select(x => new {


                region_name = x.Key.region_name,

                prov_name = x.Key.prov_name,

                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,

                no_intake_date = x.Sum(c => c.no_intake_date),

                intake_date_advanced = x.Sum(c => c.intake_date_advanced),

                resolved_without_date = x.Sum(c => c.resolved_without_date),
                resolved_without_resolution = x.Sum(c => c.resolved_without_resolution),

                resolved_in_advance = x.Sum(c => c.resolved_in_advance),

                TYPE_A_Pending_Not_Resolved_after_5_days_intake = x.Sum(c => c.TYPE_A_Pending_Not_Resolved_after_5_days_intake),
                TYPE_A_Pending_Not_Resolved_after_5_days_pincos = x.Sum(c => c.TYPE_A_Pending_Not_Resolved_after_5_days_pincos),

                TYPE_B_Pending_Not_Resolved_after_30_days_intake = x.Sum(c => c.TYPE_B_Pending_Not_Resolved_after_30_days_intake),

                TYPE_B_Pending_Not_Resolved_after_30_days_pincos = x.Sum(c => c.TYPE_B_Pending_Not_Resolved_after_30_days_pincos),

                TYPE_C_Pending_Not_Resolved_after_60_days_intake = x.Sum(c => c.TYPE_C_Pending_Not_Resolved_after_60_days_intake),

                TYPE_C_Pending_Not_Resolved_after_60_days_pincos = x.Sum(c => c.TYPE_C_Pending_Not_Resolved_after_60_days_pincos),


            });

            return Ok(export);

        }

        #endregion
        #region Report
        [Route("api/export/grievance_record/barangay_summary")]
        public IActionResult export_brgy_summary(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model.GroupBy(x =>
                           new
                           {
                               brgy_name = x.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(c => c.brgy_code == x.brgy_code).brgy_name

                           }).Select(x => new
                           {
                               x.Key.brgy_name,
                               resolved = x.Count(c => c.grs_resolution_status_id == 1),
                               on_going = x.Count(c => c.grs_resolution_status_id == 2),
                               pending = x.Count(c => c.grs_resolution_status_id == 3),
                               total = x.Count()
                           });


            return Ok(result);

        }

        [Route("api/export/grievance_record/category_summary")]
        public IActionResult export_category_summary(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model.GroupBy(x =>
                           new
                           {
                               x.lib_grs_category.name

                           }).Select(x => new
                           {
                               name = x.Key.name,
                               type_a = x.Count(c => c.grs_nature_id == 1),
                               type_b = x.Count(c => c.grs_nature_id == 2),
                               type_c = x.Count(c => c.grs_nature_id == 3),
                               total = x.Count()
                           });


            return Ok(result);

        }
        [Route("api/export/grievance_record/nature_summary")]
        public IActionResult export_nature_summary(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model.GroupBy(x =>
                           new
                           {
                               x.lib_grs_nature.name

                           }).Select(x => new
                           {
                               name = x.Key.name,
                               resolved = x.Count(c => c.grs_resolution_status_id == 1),
                               on_going = x.Count(c => c.grs_resolution_status_id == 2),
                               pending = x.Count(c => c.grs_resolution_status_id == 3),
                               total = x.Count(),
                           });


            return Ok(result);

        }



        #endregion


        #region Exports
        [Route("api/export/grievance_record/list")]
        public IActionResult export_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from s in model
                         select new
                         {
                             s.grievance_record_id,
                             //s.date_intake,
                             date_intake = s.date_intake == null ? "" : s.date_intake.Value.ToString("dd/MM/yyyy"), /*System.Globalization.CultureInfo.InvariantCulture),*/
                             resolution_status = s.lib_grs_resolution_status.name,
                             //s.resolution_date,
                             resolution_date = s.resolution_date == null ? "" : s.resolution_date.Value.ToString("dd/MM/yyyy"), /*System.Globalization.CultureInfo.InvariantCulture),*/
                             feedback = s.lib_grs_feedback.name,
                             grs_form = s.lib_grs_form.name,
                             intake_level = s.lib_grs_intake_level.name,
                             fund_source = s.lib_fund_source.name,
                             //      cycle = s.lib_cycle.name,
                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,
                             //kc_mode = s.lib_enrollment.name,
                             grs_filling_mode = s.lib_grs_filling_mode.name,
                             s.grs_filling_mode_others,
                             is_individual = s.is_individual == true ? "Individual" : "Group",
                             is_anonymous = s.is_anonymous == true ? "Anonymous" : "",
                             s.sender_name,
                             sender_organization = s.sender_organization,
                             is_ip = s.is_ip == true ? "IP" : "Non-IP",
                             ip_group = s.is_ip == true ? db.lib_ip_group.FirstOrDefault(x => x.ip_group_id == s.ip_group_id).name : "",
                             sender_sex = s.lib_sex.name,
                             sender_designation = s.lib_grs_sender_designation.name,
                             sender_designation_others = s.sender_designation_other,
                             s.cellphone,
                             s.email,
                             old_sender_contact_from_desktop = s.sender_contact_info,
                             nature = s.lib_grs_nature.name,
                             complaint_subject = s.lib_grs_complaint_subject.name,
                             complaint_subject_others = s.grs_complaint_subject_others,
                             category = s.lib_grs_category.name,
                             details = @s.details,
                             actions = @s.actions,
                             recommendations = @s.recommendations,
                             s.intake_officer,
                             intake_officer_designation = (s.grs_intake_officer_id == null ? db.lib_grs_intake_officer.FirstOrDefault(x => x.grs_intake_officer_id == s.grs_intake_officer_id).name : ""),
                             old_intake_officer_designation_from_desktop = s.intake_officer_designation,
                             //date_intake_formatted = s.date_intake != null ? s.date_intake.ToString() : "",
                             //date_resolved_formatted = s.resolution_date != null ? s.resolution_date.ToString() : "",

                             

                         };

            return Ok(result);

        }
        #endregion

        private IQueryable<grievance_record> GetData(
                 AngularFilterModel model
           )
        {
            var list = db.grievance_record.Where(x => x.is_deleted != true)

                .Include(x => x.lib_fund_source)
                .Include(x => x.lib_cycle)
                .Include(x => x.lib_enrollment)
                .Include(x => x.lib_training_category)
                 .Include(x => x.lib_region)
                .Include(x => x.lib_province)
                .Include(x => x.lib_city)
                .Include(x => x.lib_brgy)
                .Include(x => x.lib_grs_category)
                .Include(x => x.lib_grs_complaint_subject)
                .Include(x => x.lib_grs_feedback)
                .Include(x => x.lib_grs_filling_mode)
                .Include(x => x.lib_grs_form)
                .Include(x => x.lib_grs_intake_level)
                .Include(x => x.lib_grs_intake_officer)
                .Include(x => x.lib_grs_nature)
                .Include(x => x.lib_grs_pincos_actor)
                .Include(x => x.lib_grs_resolution_status)
                .Include(x => x.lib_grs_sender_designation)





                .AsQueryable();



            if (model.training_category_id != null)
            {
                list = list.Where(x => x.training_category_id == model.training_category_id);
            }
            if (model.activity_source_id != null)
            {
                list = list.Where(x => x.activity_source_id == model.activity_source_id);
            }

            #region query

            if (model.record_id != null)
            {
                list = list.Where(x => x.grievance_record_id == model.record_id);
            }


            if (!string.IsNullOrEmpty(model.name))
            {
                list = from s in list
                       where s.sender_name.Contains(model.name)
                       ||
                       s.details.Contains(model.name)
                       ||
                       s.actions.Contains(model.name)
                       select s;
            }




            if (model.brgy_code != null)
            {
                list = (from s in list
                        where (s.brgy_code == model.brgy_code)
                        select s);
            };

            if (model.city_code != null)
            {

                list = (from s in list
                        where (s.city_code == model.city_code)
                        select s);
            };

            if (model.prov_code != null)
            {
                list = (from s in list

                        where (s.prov_code == model.prov_code)
                        select s);
            };

            if (model.region_code != null)
            {
                list = (from s in list
                        where (s.region_code == model.region_code)
                        select s);
            };

            if (model.fund_source_id != null)
            {
                list = list.Where(x => x.fund_source_id == model.fund_source_id);
            }



            if (model.enrollment_id != null)
            {
                list = list.Where(x => x.enrollment_id == model.enrollment_id);
            }



            if (model.grs_intake_level_id != null)
            {
                list = list.Where(x => x.grs_intake_level_id == model.grs_intake_level_id);
            }
            if (model.grs_form_id != null)
            {
                list = list.Where(x => x.grs_form_id == model.grs_form_id);
            }

            if (model.grs_filling_mode_id != null)
            {
                list = list.Where(x => x.grs_filling_mode_id == model.grs_filling_mode_id);
            }



            if (model.grs_resolution_status_id == 2 || model.grs_resolution_status_id == 3)
            {
                list = list.Where(x => x.grs_resolution_status_id == model.grs_resolution_status_id);
            }



            if (model.grs_feedback_id != null)
            {
                list = list.Where(x => x.grs_feedback_id == model.grs_feedback_id);
            }
            if (model.grs_category_id != null)
            {
                list = list.Where(x => x.grs_category_id == model.grs_category_id);
            }


            if (model.grs_complaint_subject_id != null)
            {
                list = list.Where(x => x.grs_complaint_subject_id == model.grs_complaint_subject_id);
            }

            if (model.grs_nature_id != null)
            {
                list = list.Where(x => x.grs_nature_id == model.grs_nature_id);
            }


            if (model.ip_group_id != null)
            {
                list = list.Where(x => x.ip_group_id == model.ip_group_id);
            }

            //original filter:
            //if (model.intake_date != null)
            //{
            //    list = list.Where(x => x.date_intake >= model.intake_date);

            //    if (model.resolved_date != null)
            //    {
            //        list = list.Where(x => x.resolution_date <= model.resolved_date);
            //    }
            //}

            //4.2.1 range of dates filter
            if (model.intake_date_from != null)
            {
                list = list.Where(x => x.date_intake >= model.intake_date_from);
            }
            if (model.intake_date_to != null)
            {
                list = list.Where(x => x.date_intake <= model.intake_date_to);
            }

            //4.2.1
            if (model.grs_resolution_status_id == 1 && model.resolved_date != null)
            {
                list = list.Where(x => x.resolution_date == model.resolved_date);
            }
            if (model.grs_resolution_status_id == 1 && model.resolved_date == null)
            {
                list = list.Where(x => x.grs_resolution_status_id == 1);
            }

            //v3.0 additional filters:
            if (model.is_incentive != null)
            {
                if (model.is_incentive == true)
                {
                    list = list.Where(x => x.is_incentive == true);
                }
                else
                {
                    list = list.Where(x => x.is_incentive != true);
                }
            }
            if (model.is_savings != null)
            {
                if (model.is_savings == true)
                {
                    list = list.Where(x => x.is_savings == true);
                }
                else
                {
                    list = list.Where(x => x.is_savings != true);
                }
            }
            if (model.is_lgu_led != null)
            {
                if (model.is_lgu_led == true)
                {
                    list = list.Where(x => x.is_lgu_led == true);
                }
                else
                {
                    list = list.Where(x => x.is_lgu_led != true);
                }
            }
            if (model.is_unauthorized != null)
            {
                if (model.is_unauthorized == true)
                {
                    list = list.Where(m => m.push_status_id == 4);
                }
                else
                {
                    list = list.Where(m => m.push_status_id != 4);
                }
            }



            #endregion

            return list;
        }


        //v3.1 fix for Grievance records not able to get the start date from BA or Trainings
        [Route("api/offline/v1/grievances/update_date_intake")]
        public async Task<IActionResult> UpdateIntake()
        {
            var records = db.grievance_record.Where(g => (g.grs_filling_mode_id == 8 && g.date_intake == null && g.activity_source_id != null) || (g.grs_filling_mode_id == 9 && g.date_intake == null && g.activity_source_id != null));

            foreach (var r in records) {                
                if (r.grs_filling_mode_id == 8) {
                    var ba = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == r.activity_source_id);
                    if (ba != null)
                    {
                        r.date_intake = ba.date_start;
                        r.push_status_id = 3;
                    }
                    else {
                        r.date_intake = null;
                        r.push_status_id = 3;
                    }                    
                }
                else {
                    var ct = db.community_training.FirstOrDefault(x => x.community_training_id == r.activity_source_id);
                    if (ct != null)
                    {
                        r.date_intake = ct.start_date;
                        r.push_status_id = 3;
                    }
                    else
                    {
                        r.date_intake = null;
                        r.push_status_id = 3;
                    }
                }                                
            }
            
            await db.SaveChangesAsync();
            return Ok();
        }




        [HttpPost]
        [Route("api/offline/v1/grievances/get_dto")]
        public PagedCollection<grievance_recordDTO> GetAll(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grievance_recordDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model.OrderBy(x => x.grievance_record_id)
                .Select(x => new grievance_recordDTO
                {
                    grievance_record_id = x.grievance_record_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    lib_fund_source_name = x.lib_fund_source.name,

                    sender_name = x.sender_name,
                    details = x.details,
                    //lib_enrollment_name = x.lib_enrollment.name,
                    lib_grs_resolution_status_name = x.lib_grs_resolution_status.name,
                    lib_grs_category_name = x.lib_grs_category.name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date

                }).Skip(currPages * size).Take(size).ToList(),
                
            };

        }

        [HttpPost]
        [Route("api/offline/v1/grievances/get_recently_edited")]
        public PagedCollection<grievance_recordDTO> GetRecentlyEdited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grievance_recordDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                .OrderBy(x => x.grievance_record_id)
                .Select(x => new grievance_recordDTO
                {
                    grievance_record_id = x.grievance_record_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    lib_fund_source_name = x.lib_fund_source.name,

                    sender_name = x.sender_name,
                    details = x.details,
                    //lib_enrollment_name = x.lib_enrollment.name,
                    lib_grs_resolution_status_name = x.lib_grs_resolution_status.name,
                    lib_grs_category_name = x.lib_grs_category.name,

                }).Skip(currPages * size).Take(size).ToList(),

            };

        }

        [HttpPost]
        [Route("api/offline/v1/grievances/get_recently_added")]
        public PagedCollection<grievance_recordDTO> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grievance_recordDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                .OrderBy(x => x.grievance_record_id)
                .Select(x => new grievance_recordDTO
                {
                    grievance_record_id = x.grievance_record_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    lib_fund_source_name = x.lib_fund_source.name,

                    sender_name = x.sender_name,
                    details = x.details,
                    //lib_enrollment_name = x.lib_enrollment.name,
                    lib_grs_resolution_status_name = x.lib_grs_resolution_status.name,
                    lib_grs_category_name = x.lib_grs_category.name,

                }).Skip(currPages * size).Take(size).ToList(),

            };

        }

        [HttpPost]
        [Route("api/offline/v1/grievances/get_recently_edited_and_added")]
        public PagedCollection<grievance_recordDTO> GetRecentlyEditedandAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grievance_recordDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                .OrderBy(x => x.grievance_record_id)
                .Select(x => new grievance_recordDTO
                {
                    grievance_record_id = x.grievance_record_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    lib_fund_source_name = x.lib_fund_source.name,

                    sender_name = x.sender_name,
                    details = x.details,
                    //lib_enrollment_name = x.lib_enrollment.name,
                    lib_grs_resolution_status_name = x.lib_grs_resolution_status.name,
                    lib_grs_category_name = x.lib_grs_category.name,

                }).Skip(currPages * size).Take(size).ToList(),

            };

        }


        [Route("api/offline/v1/grievances/save")]
        public async Task<IActionResult> Save(grievance_record model, bool? api)
        {
            var record = db.grievance_record.AsNoTracking().FirstOrDefault(x => x.grievance_record_id == model.grievance_record_id);
            var training_record = db.community_training.FirstOrDefault(x => x.community_training_id == model.activity_source_id);
            var ba_record = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == model.activity_source_id);

            if (record == null)
            {
                if (model.grievance_record_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    model.grievance_record_id = Guid.NewGuid();
                }

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

                if (training_record != null) {
                    //v3.1 if new PINCOs/Grievance is added in Trainings, update training record as well
                    training_record.push_status_id = 3;
                    training_record.last_modified_by = 0;
                    training_record.last_modified_date = DateTime.Now;
                    db.Entry(training_record).State = EntityState.Modified;
                }

                if (ba_record != null) {
                    //v3.1 if new PINCOs/Grievance is added in BA, update training record as well
                    ba_record.push_status_id = 3;
                    ba_record.last_modified_by = 0;
                    ba_record.last_modified_date = DateTime.Now;
                    db.Entry(ba_record).State = EntityState.Modified;
                }               

                //pincos work around
                model.grs_intake_officer_id = 5;
                model.grs_intake_level_id = 5;
               
                db.grievance_record.Add(model);
                
                try
                {
                    await db.SaveChangesAsync();
                    return Ok(model);
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
                    return Ok(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
        }

        [Route("api/offline/v1/grievances/get")]
        public IActionResult Get(Guid? id = null)
        {

            var model = db.grievance_record.Include(xi => xi.grievance_record_discussions)

                .FirstOrDefault(x => x.grievance_record_id == id && x.is_deleted != true);



            if (model == null)
            {
                var item = new grievance_record();

                item.grievance_record_id = Guid.NewGuid();
                item.push_status_id = 3;
                item.created_by = 0;
                //item.grs_intake_officer_id = 5;
                //item.grs_intake_level_id = 5;


                item.created_date = DateTime.Now;
                item.approval_id = 3;
                item.is_deleted = false;
                //item.date_intake = DateTime.Now; --


                model = item;
            }
            else
            {

                if (model.grs_filling_mode_id == 8 && model.date_intake == null) {
                    model.date_intake = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == model.activity_source_id && x.is_deleted != true).date_start;
                    UpdateDateIntake(model);
                }

                if (model.grs_filling_mode_id == 9 && model.date_intake == null)
                {
                    model.date_intake = db.community_training.FirstOrDefault(x => x.community_training_id == model.activity_source_id && x.is_deleted != true).start_date;
                    UpdateDateIntake(model);
                }

                model.push_status_id = 3;
                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;
                model.approval_id = 3;                

            }

            return Ok(model);
        }


        public void UpdateDateIntake(grievance_record model) {

            var record_to_be_updated = db.grievance_record.FirstOrDefault(x => x.grievance_record_id == model.grievance_record_id);

            

            try
            {
                db.Entry(record_to_be_updated).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception e) {
                throw e;
            }
            
        }





        [Route("api/offline/v1/grievances/discussion/get")]
        public IActionResult GetDiscussion(Guid? id = null)
        {


            var model = db.grievance_record_discussion.Where(x => x.grievance_record_id == id && x.is_deleted != true);


            return Ok(model);
        }





        [Route("api/offline/v1/grievances/save_discussion")]
        [HttpPost]
        public async Task<IActionResult> SaveDiscussion([FromBody]grievance_record_discussion model)
        {
            var main_record = db.grievance_record.FirstOrDefault(x => x.grievance_record_id == model.grievance_record_id);

            if (!ModelState.IsValid)
            {
                model.created_date = DateTime.Now;
                model.grievance_record_discussion_id = Guid.NewGuid();
            }

            if (!record_exists(model.grievance_record_id))
            {
                return BadRequest("Incomplete");
            }

            if (db.grievance_record_discussion.Count(x => x.grievance_record_discussion_id == model.grievance_record_discussion_id) == 0)
            {
                //if discussion is added, update grievance record
                main_record.push_status_id = 3;
                db.Entry(main_record).State = EntityState.Modified;

                db.grievance_record_discussion.Add(model);
                await db.SaveChangesAsync();
            }
            
            return Json(new { success = true, message = "Saved Details" });
        }




        [HttpPost]
        [Route("Sync/Get/grievances")]
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/grievances/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<grievance_record>>(responseJson.Result);
                    
                    foreach (var item in model.ToList())
                    {
                        await Save(item, true);
                    }

                    await GetOnlineDiscussion(username, password, city_code, record_id);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }

        public async Task<ActionResult> GetOnlineDiscussion(string username, string password, string city_code = null, Guid? record_id = null)
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/grievances/discussion/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<grievance_record_discussion>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        await SaveDiscussion(item);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [Route("api/offline/v1/grievance/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid grievance_record_id, int push_status_id)
        {
            var grievance = db.grievance_record.Find(grievance_record_id);

            if (grievance == null)
            {
                return BadRequest("Error");
            }

            grievance.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Sync/Post/grievances")]
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

                var items_preselected = db.grievance_record.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.grievance_record.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/grievances/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            record_id = item.grievance_record_id;
                            //PostOnlineDiscussion(username, password, record_id);
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
                    var items = db.grievance_record.Where(x => x.push_status_id == 5 || x.is_deleted == true);
                    
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/grievances/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            record_id = item.grievance_record_id;
                            //PostOnlineDiscussion(username, password, record_id);
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
            await PostOnlineDiscussion(username, password, record_id); //-- v4.3 put back outside foreach
            return Ok();
        }





        //OLD:
        //[HttpPost]
        //[Route("Sync/Post/grievances")]
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


        //        var items = db.grievance_record.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

        //        if (record_id != null)
        //        {
        //            items = items.Where(x => x.grievance_record_id == record_id);
        //        }

        //        foreach (var item in items.ToList())
        //        {

        //            //        var push = Mapper.DynamicMap<grievance_record, grievance_record_mapping>(item);

        //            StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

        //            HttpResponseMessage response = client.PostAsync("api/offline/v1/grievances/save", data).Result;

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

        //    await PostOnlineDiscussion(username, password, record_id);

        //    return Ok();
        //}


        public async Task<ActionResult> PostOnlineDiscussion(string username, string password, Guid? record_id = null)
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
                
                //var items = db.grievance_record_discussion.Where(x => x.grievance_record_id == record_id || x.is_deleted == true);
                var items = db.grievance_record_discussion.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1));

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/v1/offline/grievances/save_discussion", data).Result;
                    
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
            return Ok();
        }


        public bool record_exists(Guid id)
        {
            return db.grievance_record.Count(e => e.grievance_record_id == id) > 0;
        }

        [Route("api/offline/v1/grievances/exists")]
        public IActionResult api_record_exists(Guid id)
        {
            return Ok(record_exists(id));
        }


    }
}
