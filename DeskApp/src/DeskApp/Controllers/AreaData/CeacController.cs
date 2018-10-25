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
    public class CeacController : Controller
    {
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public CeacController(ApplicationDbContext context)
        {
            db = context;
        }
        
        //ACT Monthly Report: --- AS OF
        [Route("api/export/report/ceac_conducted/as_of")]
        public IActionResult export_actreport_asof(AngularFilterModel item)
        {
            DateTime? as_of = item.as_of;
            int? selected_fund_source = item.fund_source_id;

            var items = db.ceac_tracking
                .Include(x => x.lib_city)
                .Include(x => x.lib_lgu_level)
                .Include(x => x.lib_training_category)
                .Include(x => x.lib_fund_source)

                .Select(x => new
                {
                    ceac_tracking_id = x.ceac_tracking_id,
                    ceac_list_id = x.ceac_list_id,
                    old_id = x.old_id,
                    ceac_activity_id = x.ceac_activity_id,

                    brgy_name = x.lib_brgy.brgy_name,

                    plan_start = x.plan_start,
                    plan_end = x.plan_end,
                    actual_start = x.actual_start,
                    actual_end = x.actual_end,
                    fund_source_id = x.fund_source_id,

                    max7days = x.plan_start.HasValue ? x.plan_start.Value.AddDays(7) : (DateTime?)null,

                    catch_start = x.catch_start,
                    catch_end = x.catch_end,
                    training_category_id = x.training_category_id,
                    lib_training_category_name = x.lib_training_category.name,
                    implementation_status_id = x.implementation_status_id,
                })
                .Where(x => x.actual_start <= as_of && x.fund_source_id == selected_fund_source)
                .OrderBy(x => x.actual_start);
            
            return Ok(items);
        }

        //ACT Monthly Report: --- FOR THE PERIOD OF
        [Route("api/export/report/ceac_conducted/for_the_period_of")]
        public IActionResult export_actreport_fortheperiodof(AngularFilterModel item)
        {
            DateTime? fortheperiodof_from = item.fortheperiodof_from;
            DateTime? fortheperiodof_to = item.fortheperiodof_to;
            int? selected_fund_source = item.fund_source_id;

            var items = db.ceac_tracking
                .Include(x => x.lib_implementation_status)
                .Include(x => x.lib_city)
                .Include(x => x.lib_lgu_level)
                .Include(x => x.lib_training_category)
                .Include(x => x.lib_fund_source)

                .Select(x => new
                {
                    ceac_tracking_id = x.ceac_tracking_id,
                    ceac_list_id = x.ceac_list_id,
                    old_id = x.old_id,
                    ceac_activity_id = x.ceac_activity_id,

                    plan_start = x.plan_start,
                    plan_end = x.plan_end,
                    actual_start = x.actual_start,
                    actual_end = x.actual_end,
                    fund_source_id = x.fund_source_id,

                    max7days = x.plan_start.HasValue ? x.plan_start.Value.AddDays(7) : (DateTime?)null,

                    catch_start = x.catch_start,
                    catch_end = x.catch_end,
                    training_category_id = x.training_category_id,
                    lib_implementation_status_name = x.lib_implementation_status.name,
                    lib_training_category_name = x.lib_training_category.name,
                    implementation_status_id = x.implementation_status_id,

                }).Where(x => x.fund_source_id == selected_fund_source && (x.actual_start >= fortheperiodof_from && x.actual_start <= fortheperiodof_to))
                .OrderBy(x => x.actual_start);

            return Ok(items);
        }

        [Route("api/export/report/ceac_tracking")]
        public IActionResult export_report(AngularFilterModel item)
        {

           
            var items = db.ceac_tracking
            .Include(x => x.lib_implementation_status)
           .Include(x => x.lib_city)
           .Include(x => x.lib_lgu_level)
            .Include(x => x.lib_training_category)
            .Include(x => x.lib_fund_source)

        .Select(x =>
               new
               {


                   ceac_tracking_id = x.ceac_tracking_id,
                   ceac_list_id = x.ceac_list_id,
                       //  ceac_list = x.ceac_list,
                       old_id = x.old_id,
                   ceac_activity_id = x.ceac_activity_id,
                   plan_start = x.plan_start,
                   plan_end = x.plan_end,
                   actual_start = x.actual_start,
                   actual_end = x.actual_end,
                   catch_start = x.catch_start,
                   catch_end = x.catch_end,
                   training_category_id = x.training_category_id,

                       // lib_training_category = x.lib_training_category,
                       ///lib_lgu_level = x.lib_lgu_level,
                       //lgu_level_id = x.lgu_level_id,
                       lib_implementation_status_name = x.lib_implementation_status.name,
                       //lib_city_city_name = x.lib_city.city_name,
                       lib_training_category_name = x.lib_training_category.name,
                   implementation_status_id = x.implementation_status_id,
                   lib_fund_source_name = x.lib_fund_source.name,

                   lgu_level_id = x.lgu_level_id,

                   region_code = x.region_code,
                   prov_code = x.prov_code,
                   city_code = x.city_code,
                   brgy_code = x.brgy_code,
                   fund_source_id = x.fund_source_id,
                   cycle_id = x.cycle_id,
                   enrollment_id = x.enrollment_id,
                       lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,

                   //   lib_province_prov_name = db.lib_province.First(c => c.prov_code == x.prov_code).prov_name,
                   //   lib_region_region_name = db.lib_region.First(c => c.region_code == x.region_code).region_nick,
                   //   lib_lgu_level_name = db.lib_lgu_level.First(c => c.lgu_level_id == x.lgu_level_id).name,


                   // lib_fund_source_name = db.lib_fund_source.First(c => c.fund_source_id == x.fund_source_id).name,
                   //  lib_cycle_name = db.lib_cycle.First(c => c.cycle_id == x.cycle_id).name,
                   //  lib_enrollment_name = db.lib_enrollment.First(c => c.enrollment_id == x.enrollment_id).name,

                   brgy_sort_order = x.lib_training_category.brgy_sort_order,

                   muni_sort_order = x.lib_training_category.muni_sort_order,

                   is_ceac_tracking_only = x.lib_training_category.is_ceac_tracking_only,


                   created_by = x.created_by,
                   created_date = x.created_date,
                   last_modified_by = x.last_modified_by,
                   last_modified_date = x.last_modified_date,
                   is_deleted = x.is_deleted,
                   deleted_by = x.deleted_by,
                   deleted_date = x.deleted_date,

                       // lib_push_status = x.lib_push_status,
                       push_status_id = x.push_status_id,
                   push_date = x.push_date,
                   approval_id = x.approval_id,
                       // lib_approval = x.lib_approval,


                   });

            return Ok(items);
        }

        [Route("api/export/ceac_tracking/municipal/list")]
        public IActionResult export_list(AngularFilterModel item)
        {
            #region query

            var deleted_list = db.ceac_list.Where(x => x.is_deleted == true).ToList();
            foreach (var d in deleted_list) {
                var tracking = db.ceac_tracking.Where(x => x.ceac_list_id == d.ceac_list_id);
                foreach (var t in tracking) {
                    t.is_deleted = true;
                    t.push_status_id = 3;
                    t.deleted_by = 0;
                    t.deleted_date = DateTime.Now;
                }
                db.SaveChangesAsync();               
            }

            var model = db.ceac_tracking.Where(x => x.is_deleted != true);
            
            if (item.record_id != null)
            {

                model = model.Where(x => x.ceac_list_id == item.record_id);

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




            if (item.fund_source_id != null)
            {
                model = model.Where(m => m.fund_source_id == item.fund_source_id);
            }
            if (item.cycle_id != null)
            {
                model = model.Where(m => m.cycle_id == item.cycle_id);
            }


            var result = model.Select(x => new
            {
                x.lib_region.region_name,
                x.lib_province.prov_name,
                x.lib_city.city_name,
                brgy_name = x.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(c => c.brgy_code == x.brgy_code).brgy_name,

                fund_source = x.lib_fund_source.name,
                cycle = x.lib_cycle.name,
                kc_mode = x.lib_enrollment.name,


                activity_level = x.lib_lgu_level.name,

                activity_type = x.lib_training_category.name,

                //x.plan_start,
                //x.actual_start,
                //x.plan_end,
                //x.actual_end,
                //x.catch_start,
                //x.catch_end,

                //4.2: format dates to dd/mm/yyyy
                plan_start = x.plan_start == null ? "" : x.plan_start.Value.ToString("dd/MM/yyyy"),
                actual_start = x.actual_start == null ? "" : x.actual_start.Value.ToString("dd/MM/yyyy"),
                plan_end = x.plan_end == null ? "" : x.plan_end.Value.ToString("dd/MM/yyyy"),
                actual_end = x.actual_end == null ? "" : x.actual_end.Value.ToString("dd/MM/yyyy"),
                catch_start = x.catch_start == null ? "" : x.catch_start.Value.ToString("dd/MM/yyyy"),
                catch_end = x.catch_end == null ? "" : x.catch_end.Value.ToString("dd/MM/yyyy"),

                status = x.lib_implementation_status.name


            });

            // var export = result.GroupBy(x => new { x.region_name, x.prov_name, x.city_name, x.brgy_name, x.fund_source, x.cycle, x.kc_mode, x.lgu})

            #endregion

            return Ok(result);
        }

        private IQueryable<ceac_list> GetData(AngularFilterModel item)
        {
            
            var model = db.ceac_list.Where(x => x.is_deleted != true).AsQueryable();

            #region query

            if (item.record_id != null)
            {
                model = model.Where(x => x.ceac_list_id == item.record_id);
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
            

            #endregion

            return model;
        }

        private IQueryable<ceac_tracking> GetCEACTrackingData(DataLayer.AngularFilterModel item)
        {
            var model = db.ceac_tracking
                .Include(x => x.lib_city)
                .Include(x => x.lib_lgu_level)
                .Include(x => x.lib_training_category)
                .Include(x => x.lib_fund_source)
                .Where(x => x.is_deleted != true).AsQueryable();

            #region query

            if (item.record_id != null)
            {
                model = model.Where(x => x.ceac_tracking_id == item.record_id);
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
            if (item.fund_source_id != null)
            {
                model = model.Where(m => m.fund_source_id == item.fund_source_id);
            }
            if (item.actual_start != null)
            {
                model = model.Where(x => x.actual_start == item.actual_start);
            }
            if (item.actual_end != null)
            {
                model = model.Where(m => m.actual_end == item.actual_end);
            }
            if (item.plan_start != null)
            {
                model = model.Where(m => m.plan_start == item.plan_start);
            }            
            if (item.plan_end != null)
            {
                model = model.Where(m => m.plan_end == item.plan_end);
            }
            if (item.implementation_status_id != null)
            {
                model = model.Where(m => m.implementation_status_id == item.implementation_status_id);
            }
            if (item.training_category_id != null)
            {
                model = model.Where(m => m.training_category_id == item.training_category_id);
            }
            if (item.cycle_id != null)
            {
                model = model.Where(m => m.cycle_id == item.cycle_id);
            }

            #endregion

            return model;
        }


        [Route("api/offline/v1/main/ceac")]
        public IActionResult getmain(Guid id)
        {
            var model = db.ceac_list.FirstOrDefault(x => x.ceac_list_id == id);

            if (model == null)
            {
                var item = new ceac_list();
                item.ceac_list_id = id;
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


            //return Ok(db.ceac_list.Find(id));
        }
        
        [HttpPost]
        [Route("api/offline/v1/ceac_list/get_dto")]
        public PagedCollection<dynamic> GetAll(AngularFilterModel item)
        {
            var model = GetData(item);
            
            var totalCount = model.Count();
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
                           .Include(x => x.lib_fund_source)
                           .Include(x => x.lib_cycle)
                            .Include(x => x.lib_enrollment)
                           .OrderBy(x => x.ceac_list_id)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_name,
                               lib_enrollment_name = x.lib_enrollment.name,
                               ceac_list_id = x.ceac_list_id,
                               push_status_id = x.push_status_id,
                               push_date = x.push_date,
                               last_modified_date = x.last_modified_date
                               //status = x.implementation_status_id == null ? ""
                               //: db.lib_implementation_status.Find(x.implementation_status_id).name
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/ceac_list/get_recently_edited")]
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
                           .Include(x => x.lib_fund_source)
                           .Include(x => x.lib_cycle)
                           .Include(x => x.lib_enrollment)
                           .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                           .OrderBy(x => x.ceac_list_id)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_nick,
                               lib_enrollment_name = x.lib_enrollment.name,
                               ceac_list_id = x.ceac_list_id,
                               //status = x.implementation_status_id == null ? ""
                               //: db.lib_implementation_status.Find(x.implementation_status_id).name
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/ceac_list/get_recently_added")]
        public PagedCollection<dynamic> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),

                Items = model
                           .Include(x => x.lib_fund_source)
                           .Include(x => x.lib_cycle)
                           .Include(x => x.lib_enrollment)
                           .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                           .OrderBy(x => x.ceac_list_id)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_nick,
                               lib_enrollment_name = x.lib_enrollment.name,

                               ceac_list_id = x.ceac_list_id,
                               //status = x.implementation_status_id == null ? ""
                               //: db.lib_implementation_status.Find(x.implementation_status_id).name
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/ceac_list/get_recently_edited_and_added")]
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
                           .Include(x => x.lib_fund_source)
                           .Include(x => x.lib_cycle)
                           .Include(x => x.lib_enrollment)
                           .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                           .OrderBy(x => x.ceac_list_id)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_nick,
                               lib_enrollment_name = x.lib_enrollment.name,
                               ceac_list_id = x.ceac_list_id,
                               //status = x.implementation_status_id == null ? ""
                               //: db.lib_implementation_status.Find(x.implementation_status_id).name
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Ceac List Id</param>
        /// <param name="lgu_level_id">1- Brgy, 2-City</param>
        /// <param name="city_code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/offline/v1/ceac_tracking/get")]
        public async Task<IActionResult> DisplayTracking(Guid id, int lgu_level_id, int? brgy_code)
        {
            var model = db.ceac_list.FirstOrDefault(x => x.ceac_list_id == id);

            if (lgu_level_id == 2) //municipal level
            {
                foreach (var item in db.lib_training_category.Where(x => x.muni_sort_order != null).ToList())
                {
                    if (db.ceac_tracking.Where(x => x.ceac_list_id == id && x.lgu_level_id == 2 && x.city_code == model.city_code && x.training_category_id == item.training_category_id && x.is_deleted != true).Count() == 0)
                    {
                        var o = new ceac_tracking
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
                            training_category_id = item.training_category_id,
                            push_status_id = 2,
                            created_by = 0,
                            created_date = DateTime.Now,
                            implementation_status_id = 2,
                            ceac_tracking_id = Guid.NewGuid(),
                            lgu_level_id = lgu_level_id
                        };

                        db.ceac_tracking.Add(o);
                        await db.SaveChangesAsync();
                    }
                }
            }


            if (lgu_level_id == 1)
            {
                foreach (var item in db.lib_training_category.Where(x => x.brgy_sort_order != null).ToList())
                {
                    if (db.ceac_tracking.Where(x => x.ceac_list_id == id && x.lgu_level_id == 1 && x.brgy_code == brgy_code && x.training_category_id == item.training_category_id && x.is_deleted != true).Count() == 0)
                    {
                        var o = new ceac_tracking
                        {
                            ceac_list_id = id,
                            region_code = model.region_code,
                            prov_code = model.prov_code,
                            city_code = model.city_code,
                            brgy_code = brgy_code,
                            approval_id = model.approval_id,
                            fund_source_id = model.fund_source_id,
                            cycle_id = model.cycle_id,
                            enrollment_id = model.enrollment_id,
                            training_category_id = item.training_category_id,
                            push_status_id = 2,
                            created_by = 0,
                            created_date = DateTime.Now,
                            implementation_status_id = 2,
                            ceac_tracking_id = Guid.NewGuid(),
                            lgu_level_id = lgu_level_id
                        };

                        db.ceac_tracking.Add(o);
                        await db.SaveChangesAsync();
                    }                    
                }
            }


            //OLD -- with brgy_code in condition, removed this dahil may mga CEAC na may brgy_code na dapat wala.
            //var items = db.ceac_tracking.Where(x => x.ceac_list_id == id &&
            //    x.lgu_level_id == lgu_level_id && x.brgy_code == brgy_code)
            //    .Include(x => x.lib_implementation_status)
            //   .Include(x => x.lib_city)
            //   .Include(x => x.lib_lgu_level)
            //    .Include(x => x.lib_training_category)
            //    .Where(x => x.is_deleted != true)



            var items = db.ceac_tracking.Where(x => x.ceac_list_id == id && x.lgu_level_id == lgu_level_id && x.brgy_code == brgy_code)
                                                    .Include(x => x.lib_implementation_status)
                                                    .Include(x => x.lib_city)
                                                    .Include(x => x.lib_lgu_level)
                                                    .Include(x => x.lib_training_category)
                                                    .Where(x => x.is_deleted != true)
                                                    .Select(x => new
                                                    {
                                                        ceac_tracking_id = x.ceac_tracking_id,
                                                        ceac_list_id = x.ceac_list_id,
                                                        old_id = x.old_id,
                                                        ceac_activity_id = x.ceac_activity_id,
                                                        plan_start = x.plan_start,
                                                        plan_end = x.plan_end,
                                                        actual_start = x.actual_start,
                                                        actual_end = x.actual_end,
                                                        catch_start = x.catch_start,
                                                        catch_end = x.catch_end,
                                                        training_category_id = x.training_category_id,
                                                        reference_id = x.reference_id,
                                                        lib_implementation_status_name = x.lib_implementation_status.name,
                                                        lib_training_category_name = x.lib_training_category.name,
                                                        implementation_status_id = x.implementation_status_id,
                                                        lgu_level_id = x.lgu_level_id,
                                                        region_code = x.region_code,
                                                        prov_code = x.prov_code,
                                                        city_code = x.city_code,
                                                        brgy_code = x.brgy_code,
                                                        fund_source_id = x.fund_source_id,
                                                        cycle_id = x.cycle_id,
                                                        enrollment_id = x.enrollment_id,
                                                        brgy_sort_order = x.lib_training_category.brgy_sort_order,
                                                        muni_sort_order = x.lib_training_category.muni_sort_order,
                                                        is_ceac_tracking_only = x.lib_training_category.is_ceac_tracking_only,
                                                        created_by = x.created_by,
                                                        created_date = x.created_date,
                                                        last_modified_by = x.last_modified_by,
                                                        last_modified_date = x.last_modified_date,
                                                        is_deleted = x.is_deleted,
                                                        deleted_by = x.deleted_by,
                                                        deleted_date = x.deleted_date,
                                                        push_status_id = x.push_status_id,
                                                        push_date = x.push_date,
                                                        approval_id = x.approval_id
                                                    });

            return Ok(items);





        }




        [Route("api/offline/v1/ceac_list/save")]
        public async Task<IActionResult> Save(ceac_list model, bool? is_ba, bool? api)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (model.implementation_status_id == null) {
                model.implementation_status_id = 2;
            } 
            
            var record = db.ceac_list.AsNoTracking().FirstOrDefault(x => x.city_code == model.city_code
                                                                        && x.brgy_code == model.brgy_code
                                                                        && x.fund_source_id == model.fund_source_id
                                                                        && x.cycle_id == model.cycle_id
                                                                        && x.enrollment_id == model.enrollment_id
                                                                        && x.is_deleted != true);

            if (record == null)
            {
                if (api != true) { //save using create button
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else { //save using sync download
                    model.push_status_id = 1;
                }

                db.ceac_list.Add(model);
                
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
                if (api != true) //save using edit
                {
                    //v3.1 turnaround for ceac_list not accepted if there is an existing record with same city_code, brgy_code, fund_source_id, cycle_id, enrollment_id
                    //putting this inside the condition
                    model.push_status_id = 3;
                    model.push_date = null;
                    model.ceac_list_id = record.ceac_list_id;
                    model.created_by = record.created_by;
                    model.created_date = record.created_date;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    db.Entry(model).State = EntityState.Modified;
                }

                //v3.1 turnaround for ceac_list not accepted if there is an existing record with same city_code, brgy_code, fund_source_id, cycle_id, enrollment_id
                //sync get, api is set to true
                else {
                    db.Entry(model).State = EntityState.Modified; //modified: 4-24-18
                }                

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



        [Route("api/offline/v1/ceac_list/tracking/save")]
        public async Task<IActionResult> SaveTracking(ceac_tracking model, bool? is_ba, bool? api)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (model.actual_end != null && model.actual_start != null) {
                model.implementation_status_id = 1;
            } 

            if (model.implementation_status_id == 1)
            {
                if (model.actual_start == null || model.actual_end == null)
                {
                    model.implementation_status_id = 2;
                }
            }

            //var record = db.ceac_tracking.AsNoTracking().FirstOrDefault(x => x.city_code == model.city_code
            //                                                                && x.brgy_code == model.brgy_code
            //                                                                && x.fund_source_id == model.fund_source_id
            //                                                                && x.cycle_id == model.cycle_id
            //                                                                && x.enrollment_id == model.enrollment_id
            //                                                                && x.training_category_id == model.training_category_id
            //                                                                && x.is_deleted != true);

            //var ceac_list = db.ceac_list.FirstOrDefault(x => x.city_code == model.city_code
            //                                                && x.cycle_id == model.cycle_id
            //                                                && x.enrollment_id == model.enrollment_id
            //                                                && x.is_deleted != true);

            //4.1 Fix for dates (plan, catch up, actual, status) being saved on different ceac_list
            var record = db.ceac_tracking.AsNoTracking().FirstOrDefault(x => x.ceac_tracking_id == model.ceac_tracking_id);
            var ceac_list = db.ceac_list.FirstOrDefault(x => x.ceac_list_id == model.ceac_list_id);

            if (record == null)
            {
                var training_cat = db.lib_training_category.FirstOrDefault(x => x.training_category_id == model.training_category_id);

                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.ceac_list_id = ceac_list.ceac_list_id;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.approval_id = 3;
                    model.is_deleted = false;
                }
                else {
                    model.push_status_id = 1;
                }                
                
                db.ceac_tracking.Add(model);
                
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
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    //v3.1 any changes on ceac_tracking, main ceac_list should be updated as well
                    ceac_list.push_status_id = 3;
                    ceac_list.push_date = null;
                    ceac_list.last_modified_by = 0;
                    ceac_list.last_modified_date = DateTime.Now;
                    db.Entry(ceac_list).State = EntityState.Modified;
                }

                //retaining old data whether edited or from sync download:
                model.ceac_tracking_id = record.ceac_tracking_id;
                model.ceac_list_id = record.ceac_list_id;
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


        [HttpPost]
        [Route("Sync/Get/ceac")]
        public async Task<ActionResult> GetOnline(string username, string password, string city_code = null, Guid? record_id = null)
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
                

                HttpResponseMessage response = client.GetAsync("api/offline/v1/ceac_list/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<ceac_list>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        await Save(item, false, true);
                        record_id = item.ceac_list_id;
                        GetTracking(username, password, city_code, record_id);
                    }                    

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }



        [HttpPost]
        [Route("Sync/Get/ceac_tracking")]
        public async Task<bool> GetTracking(string username, string password, string city_code = null, Guid? record_id = null)
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
                HttpResponseMessage response = client.GetAsync("api/offline/v1/ceac_list/tracking/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<ceac_tracking>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        await SaveTracking(item, false, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        [Route("api/offline/v1/ceac_tracking/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid ceac_list_id, int push_status_id)
        {
            var ceac = db.ceac_list.Find(ceac_list_id);

            if (ceac == null)
            {
                return BadRequest("Error");
            }

            ceac.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }



        [HttpPost]
        [Route("Sync/Post/ceac")]
        public async Task<ActionResult> PostOnline(string username, string password, Guid record_id)
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
                
                var items_preselected = db.ceac_list.Where(x => x.push_status_id == 5).ToList();
                
                if (!items_preselected.Any())
                { 
                    var items = db.ceac_list.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || x.is_deleted == true).ToList();

                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        //HttpResponseMessage response = client.PostAsync("api/offline/v1/ceac_list/save", data).Result;
                        HttpResponseMessage response = client.PostAsync("api/offline/v2/ceac_list/save", data).Result; //v4.3 changed api to v2 as per glenn's changes

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            record_id = item.ceac_list_id;
                            //deployed for 4.0
                            //commented on v4.3
                            //if (item.is_deleted != true) {
                            //    PostOnlineTracking(username, password, record_id);
                            //}                            
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
                    var items = db.ceac_list.Where(x => x.push_status_id == 5 || x.is_deleted == true).ToList();
                    
                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        //HttpResponseMessage response = client.PostAsync("api/offline/v1/ceac_list/save", data).Result;
                        HttpResponseMessage response = client.PostAsync("api/offline/v2/ceac_list/save", data).Result; //v4.3 changed api to v2 as per glenn's changes

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            record_id = item.ceac_list_id;
                            //deployed for 4.0
                            //commented on v4.3
                            //if (item.is_deleted != true)
                            //{
                            //    PostOnlineTracking(username, password, record_id);
                            //}
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

            //v4.3 put PostOnlineTracking outside:
            await PostOnlineTracking(username, password, record_id);

            return Ok();
        }

        public async Task<bool> PostOnlineTracking(string username, string password, Guid record_id)
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
                
                //var items = db.ceac_tracking.Where(x => x.ceac_list_id == record_id || x.is_deleted == true).ToList();
                var items = db.ceac_tracking.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1)).ToList(); //v4.3

                foreach (var item in items)
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    //HttpResponseMessage response = client.PostAsync("api/offline/v1/ceac_list/tracking/save", data).Result;
                    HttpResponseMessage response = client.PostAsync("api/offline/v2/ceac_list/tracking/save", data).Result; //v4.3 changed api to v2 as per glenn's changes
                    //response.EnsureSuccessStatusCode();
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


        ///-------------- ORIGINAL SYNC/POST ------------------///

        //[HttpPost]
        //[Route("Sync/Post/ceac")]
        //public async Task<ActionResult> PostOnline(string username, string password, Guid record_id)
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

        //        var items = db.ceac_tracking.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).ToList();

        //        foreach (var item in items)
        //        {
        //            StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        //            HttpResponseMessage response = client.PostAsync("api/offline/v1/ceac_list/save", data).Result;
        //            // response.EnsureSuccessStatusCode();

        //            if (response.IsSuccessStatusCode)
        //            {
        //                item.push_status_id = 1;
        //                item.push_date = DateTime.Now;
        //                await db.SaveChangesAsync();
        //            }
        //        }
        //    }
        //    return Ok();
        //}








    }
}