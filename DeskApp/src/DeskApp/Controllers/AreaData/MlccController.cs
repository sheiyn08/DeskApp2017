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
using System.Threading;
using Newtonsoft.Json;


namespace DeskApp.Controllers.AreaData
{
    public class MlccController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;


        public MlccController(ApplicationDbContext context)
        {
            db = context;

        }
        

        private IQueryable<municipal_lcc> GetData(
               AngularFilterModel item
         )
        {
            var model = db.municipal_lcc.Where(x => x.is_deleted != true).AsQueryable();





            #region query

            if (item.record_id != null)
            {

                model = model.Where(x => x.municipal_lcc_id == item.record_id);

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


            #endregion

            return model;
        }


        [Route("api/export/municipal_lcc/list")]
        public IActionResult export_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var export = from s in model
                         select new
                         {
                             s.municipal_lcc_id,
                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             fund_source = s.lib_fund_source.name,
                             cycle = s.lib_cycle.name,
                             kc_mode = s.lib_enrollment.name,

                             //Barangay
                             s.me_blgu_planned,
                             s.me_blgu_actual,
                             me_blgu_balance = s.me_blgu_planned - s.me_blgu_actual,

                             s.cbis_blgu_planned,
                             s.cbis_blgu_actual,
                             cbis_blgu_balance = s.cbis_blgu_planned - s.cbis_blgu_actual,

                             s.spi_blgu_planned,
                             s.spi_blgu_actual,
                             spi_blgu_balance = s.spi_blgu_planned - s.spi_blgu_actual,

                             //Municipal
                             s.me_mlgu_planned,
                             s.me_mlgu_actual,
                             me_mlgu_balance = s.me_mlgu_planned - s.me_mlgu_actual,

                             s.cbis_mlgu_planned,
                             s.cbis_mlgu_actual,
                             cbis_mlgu_balance = s.cbis_mlgu_planned - s.cbis_mlgu_actual,

                             s.spi_mlgu_planned,
                             s.spi_mlgu_actual,
                             spi_mlgu_balance = s.spi_mlgu_planned - s.spi_mlgu_actual,

                             //Municipal
                             s.me_plgu_planned,
                             s.me_plgu_actual,
                             me_plgu_balance = s.me_plgu_planned - s.me_plgu_actual,

                             s.cbis_plgu_planned,
                             s.cbis_plgu_actual,
                             cbis_plgu_balance = s.cbis_plgu_planned - s.cbis_plgu_actual,

                             s.spi_plgu_planned,
                             s.spi_plgu_actual,
                             spi_plgu_balance = s.spi_plgu_planned - s.spi_plgu_actual,


                             s.me_others_planned,
                             s.me_others_actual,
                             me_others_balance = s.me_others_planned - s.me_others_actual,

                             s.cbis_others_planned,
                             s.cbis_others_actual,
                             cbis_others_balance = s.cbis_others_planned - s.cbis_others_actual,

                             s.spi_others_planned,
                             s.spi_others_actual,
                             spi_others_balance = s.spi_others_planned - s.spi_others_actual,


                             total_planned_me = s.me_blgu_planned + s.me_mlgu_planned + s.me_plgu_planned + s.me_others_planned,
                             total_actual_me = s.me_blgu_actual + s.me_mlgu_actual + s.me_plgu_actual + s.me_others_actual,
                             total_balance_me = (s.me_blgu_planned + s.me_mlgu_planned + s.me_plgu_planned + s.me_others_planned) - (s.me_blgu_actual + s.me_mlgu_actual + s.me_plgu_actual + s.me_others_actual),

                             total_planned_cbis = s.cbis_blgu_planned + s.cbis_mlgu_planned + s.cbis_plgu_planned + s.cbis_others_planned,
                             total_actual_cbis = s.cbis_blgu_actual + s.cbis_mlgu_actual + s.cbis_plgu_actual + s.cbis_others_actual,
                             total_balance_cbis = (s.cbis_blgu_planned + s.cbis_mlgu_planned + s.cbis_plgu_planned + s.cbis_others_planned) - (s.cbis_blgu_actual + s.cbis_mlgu_actual + s.cbis_plgu_actual + s.cbis_others_actual),

                             total_planned_spi = s.spi_blgu_planned + s.spi_mlgu_planned + s.spi_plgu_planned + s.spi_others_planned,
                             total_actual_spi = s.spi_blgu_actual + s.spi_mlgu_actual + s.spi_plgu_actual + s.spi_others_actual,

                             total_balance_spi = (s.spi_blgu_planned + s.spi_mlgu_planned + s.spi_plgu_planned + s.spi_others_planned) - (s.spi_blgu_actual + s.spi_mlgu_actual + s.spi_plgu_actual + s.spi_others_actual),


                             total_overall_planned = (s.me_blgu_planned + s.me_mlgu_planned + s.me_plgu_planned + s.me_others_planned) +
                                             (s.cbis_blgu_planned + s.cbis_mlgu_planned + s.cbis_plgu_planned + s.cbis_others_planned) +
                                             (s.spi_blgu_planned + s.spi_mlgu_planned + s.spi_plgu_planned + s.spi_others_planned),

                             total_overall_actual = (s.me_blgu_actual + s.me_mlgu_actual + s.me_plgu_actual + s.me_others_actual) +
                                             (s.cbis_blgu_actual + s.cbis_mlgu_actual + s.cbis_plgu_actual + s.cbis_others_actual) +
                                             (s.spi_blgu_actual + s.spi_mlgu_actual + s.spi_plgu_actual + s.spi_others_actual),

                             total_overall_balance = ((s.me_blgu_planned + s.me_mlgu_planned + s.me_plgu_planned + s.me_others_planned) +
                                             (s.cbis_blgu_planned + s.cbis_mlgu_planned + s.cbis_plgu_planned + s.cbis_others_planned) +
                                             (s.spi_blgu_planned + s.spi_mlgu_planned + s.spi_plgu_planned + s.spi_others_planned))

                                             -

                                             ((s.me_blgu_actual + s.me_mlgu_actual + s.me_plgu_actual + s.me_others_actual) +
                                             (s.cbis_blgu_actual + s.cbis_mlgu_actual + s.cbis_plgu_actual + s.cbis_others_actual) +
                                             (s.spi_blgu_actual + s.spi_mlgu_actual + s.spi_plgu_actual + s.spi_others_actual))

                         };

            return Ok(export);

        }




        [HttpPost]
        [Route("api/offline/v1/mlcc/get_dto")]
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
                            .Include(x => x.lib_region)
                            .Include(x => x.lib_province)
                            .Include(x => x.lib_city)
                            .Include(x => x.lib_fund_source)
                            .Include(x => x.lib_cycle)
                            .Include(x => x.lib_enrollment)
                            .OrderByDescending(x => x.history)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_nick,
                               lib_enrollment_name = x.lib_enrollment.name,
                               municipal_lcc_id = x.municipal_lcc_id,
                               push_date = x.push_date,
                               push_status_id = x.push_status_id,
                               history = x.history,
                               last_modified_date = x.last_modified_date
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/mlcc/get_recently_edited")]
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
                            .Include(x => x.lib_region)
                            .Include(x => x.lib_province)
                            .Include(x => x.lib_city)
                            .Include(x => x.lib_fund_source)
                            .Include(x => x.lib_cycle)
                            .Include(x => x.lib_enrollment)
                           .OrderBy(x => x.municipal_lcc_id)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_nick,
                               lib_enrollment_name = x.lib_enrollment.name,
                               municipal_lcc_id = x.municipal_lcc_id,
                               push_date = x.push_date,
                               push_status_id = x.push_status_id,
                               last_modified_date = x.last_modified_date
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/mlcc/get_recently_added")]
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
                            .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                            .Include(x => x.lib_region)
                            .Include(x => x.lib_province)
                            .Include(x => x.lib_city)
                            .Include(x => x.lib_fund_source)
                            .Include(x => x.lib_cycle)
                            .Include(x => x.lib_enrollment)
                           .OrderBy(x => x.municipal_lcc_id)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_nick,
                               lib_enrollment_name = x.lib_enrollment.name,
                               municipal_lcc_id = x.municipal_lcc_id,
                               push_date = x.push_date,
                               push_status_id = x.push_status_id,
                               last_modified_date = x.last_modified_date
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/mlcc/get_recently_edited_and_added")]
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
                            .Include(x => x.lib_region)
                            .Include(x => x.lib_province)
                            .Include(x => x.lib_city)
                            .Include(x => x.lib_fund_source)
                            .Include(x => x.lib_cycle)
                            .Include(x => x.lib_enrollment)
                           .OrderBy(x => x.municipal_lcc_id)
                           .Select(x => new
                           {
                               lib_city_city_name = x.lib_city.city_name,
                               lib_cycle_name = x.lib_cycle.name,
                               lib_fund_source_name = x.lib_fund_source.name,
                               lib_province_prov_name = x.lib_province.prov_name,
                               lib_region_region_name = x.lib_region.region_nick,
                               lib_enrollment_name = x.lib_enrollment.name,
                               municipal_lcc_id = x.municipal_lcc_id,
                               push_date = x.push_date,
                               push_status_id = x.push_status_id,
                               last_modified_date = x.last_modified_date
                           })
                           .Skip(currPages * size).Take(size).ToList(),
            };
        }


        [Route("api/offline/v1/mlcc/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.municipal_lcc

                .FirstOrDefault(x => x.municipal_lcc_id == id);// && (x.is_deleted.HasValue && x.is_deleted == true));



            if (model == null)
            {
                var item = new municipal_lcc();

                item.municipal_lcc_id = id;
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






        //OLD:
        //[Route("api/offline/v1/mlcc/save")]
        //public async Task<IActionResult> Save(municipal_lcc model, bool? is_ba, bool? api)
        //{


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }




        //    var record = db.municipal_lcc.AsNoTracking().FirstOrDefault(x => x.city_code == model.city_code

        //    && x.fund_source_id == model.fund_source_id
        //    && x.cycle_id == model.cycle_id
        //    && x.enrollment_id == model.enrollment_id


        //    );

        //    model.no_of_barangays = db.lib_brgy.Count(x => x.city_code == model.city_code);

        //    if (record == null)
        //    {



        //        model.created_by = 0;
        //        model.created_date = DateTime.Now;
        //        model.approval_id = 3;
        //        model.is_deleted = false;


        //        if (api == true)
        //        {
        //            model.push_status_id = 1;
        //        }

        //        db.municipal_lcc.Add(model);


        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            return Ok();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        model.push_date = null;

        //        model.push_status_id = 3;

        //        if (api == true)
        //        {
        //            model.push_status_id = 1;
        //        }

        //        model.municipal_lcc_id = record.municipal_lcc_id;



        //        model.created_by = record.created_by;
        //        model.created_date = record.created_date;


        //        model.last_modified_by = 0;
        //        model.last_modified_date = DateTime.Now;

        //        db.Entry(model).State = EntityState.Modified;

        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            return Ok();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}

        
            
            
        ////NEW: await Save(item, false, true); in sync/get
        [Route("api/offline/v1/mlcc/save")]
        public async Task<IActionResult> Save(municipal_lcc model, bool? is_ba, bool? api)
        {
            var record = db.municipal_lcc.AsNoTracking().FirstOrDefault(x => x.city_code == model.city_code && x.history == model.history);
            
            model.no_of_barangays = db.lib_brgy.Count(x => x.city_code == model.city_code);

            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;

                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.approval_id = 3;
                    model.is_deleted = false;
                }

                //because api is set to TRUE in sync/get
                if (api == true)
                {
                    model.push_status_id = 1;
                    model.is_deleted = false;
                }

                db.municipal_lcc.Add(model);


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
                }
                
                model.municipal_lcc_id = record.municipal_lcc_id;
                
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



        [HttpPost]
        [Route("Sync/Get/mlcc")]
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

                // var model = new auth_messages();

                HttpResponseMessage response = client.GetAsync("api/offline/v1/mlcc/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<municipal_lcc>>(responseJson.Result);


                    foreach (var item in model.ToList())
                    {
                        await Save(item, false, true);
                    }



                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }


        [Route("api/offline/v1/municipal_lcc/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid municipal_lcc_id, int push_status_id)
        {
            var mlcc = db.municipal_lcc.Find(municipal_lcc_id);

            if (mlcc == null)
            {
                return BadRequest("Error");
            }

            mlcc.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        //new:
        [HttpPost]
        [Route("Sync/Post/mlcc")]
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

                var items_preselected = db.municipal_lcc.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.municipal_lcc.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).ToList();
                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/mlcc/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                    }
                }
                else {
                    var items = db.municipal_lcc.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();
                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/mlcc/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
            return Ok();
        }
        
    }
}
