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

    public class GrievanceInstallController : Controller
    {

        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;


        public GrievanceInstallController(ApplicationDbContext context)
        {
            db = context;

        }
  
        #region Exports
        [Route("api/export/grievance_installation/list")]
        public IActionResult export_list(AngularFilterModel item)
        {


            var model = GetData(item);

            var result = from data in model
                         select new
                         {
                             data.grs_installation_id,

                             fund_source = data.lib_fund_source.name,
                             cycle = data.lib_cycle.name,
                             data.lib_region.region_name,
                             data.lib_province.prov_name,
                             data.lib_city.city_name,
                             data.lib_brgy.brgy_name,
                             //  kc_mode = data.lib_enrollment.name,
                             grs_installation_level_id = data.lib_lgu_level.name,

                             //no_brochures = data.no_brochures,
                             //no_manuals = data.no_manuals,
                             //no_posters = data.no_posters,
                             //no_tarpauline = data.no_tarpauline,

                             with_grievance_box = data.is_boxinstalled == true ? "Yes" : "No",

                          

                             date_ffcomm = data.date_ffcomm,
                             date_infodess = data.date_infodess,
                             date_inspect = data.date_inspect,
                             date_meansrept = data.date_meansrept,
                             date_training = data.date_training,
                             date_voliden = data.date_voliden,

                             date_orientation = data.date_orientation,
                             date_means_of_reporting = data.date_meansrept,

                             phone_no = data.phone_no,

                             remarks = data.remarks,
                             
                             brgy_count_with_date_orientation = db.grs_installation.Count(x => x.date_orientation != null && x.lgu_level_id == 1),
                             brgy_count_with_date_voliden = db.grs_installation.Count(x => x.date_voliden != null && x.lgu_level_id == 1),
                             brgy_count_with_date_ffcomm = db.grs_installation.Count(x => x.date_ffcomm != null && x.lgu_level_id == 1),
                             brgy_count_with_date_training = db.grs_installation.Count(x => x.date_training != null && x.lgu_level_id == 1),
                             brgy_count_with_date_inspect = db.grs_installation.Count(x => x.date_inspect != null && x.lgu_level_id == 1),
                             brgy_count_with_grievance_box = db.grs_installation.Count(x => x.is_boxinstalled == true && x.lgu_level_id == 1),

                             act_count_with_date_orientation = db.grs_installation.Count(x => x.date_orientation != null && x.lgu_level_id == 2),
                             act_count_with_date_voliden = db.grs_installation.Count(x => x.date_voliden != null && x.lgu_level_id == 2),
                             act_count_with_date_ffcomm = db.grs_installation.Count(x => x.date_ffcomm != null && x.lgu_level_id == 2),
                             act_count_with_date_training = db.grs_installation.Count(x => x.date_training != null && x.lgu_level_id == 2),
                             act_count_with_date_inspect = db.grs_installation.Count(x => x.date_inspect != null && x.lgu_level_id == 2),
                             act_count_with_grievance_box = db.grs_installation.Count(x => x.is_boxinstalled == true && x.lgu_level_id == 2),

                         };

            return Ok(result);
        }


        #endregion  

        private IQueryable<grs_installation> GetData(AngularFilterModel item
         )
        {
            var model = db.grs_installation

                   .Include(x => x.lib_fund_source)
                .Include(x => x.lib_cycle)
           
                 .Include(x => x.lib_region)
                .Include(x => x.lib_province)
                .Include(x => x.lib_city)
                .Include(x => x.lib_brgy)
                .Include(x => x.lib_lgu_level)

                .Where(x => x.is_deleted != true).AsQueryable();





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

            if (item.push_date != null)
            {
                model = model.Where(m => m.push_date == item.push_date);
            }
            if (item.approval_id != null)
            {
                model = model.Where(m => m.approval_id == item.approval_id);
            }

            if (item.lgu_level_id != null)
            {
                model = model.Where(x => x.lgu_level_id == item.lgu_level_id);
            }

            if (item.fund_source_id != null)
            {
                model = model.Where(m => m.fund_source_id == item.fund_source_id);
            }
            if (item.cycle_id != null)
            {
                model = model.Where(m => m.cycle_id == item.cycle_id);
            }
           


            return model;
        }









        [HttpPost]
        [Route("api/offline/v1/grs_installation/get_dto")]
        public PagedCollection<grs_installationDTO> GetAll(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grs_installationDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model.OrderBy(x => x.grs_installation_id)
                .Select(x => new grs_installationDTO
                {
                    grs_installation_id = x.grs_installation_id,
                    lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                    lib_city_city_name = x.lib_city.city_name,
                    lib_province_prov_name = x.lib_province.prov_name,
                    lib_region_region_name = x.lib_region.region_name,
                    lib_fund_source_name = x.lib_fund_source.name,
                    lib_lgu_level_name = x.lib_lgu_level.name,
                    lib_cycle_name = x.lib_cycle.name,
                    push_date = x.push_date,
                    push_status_id = x.push_status_id,
                    last_modified_date = x.last_modified_date

                }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/grs_installation/get_recently_edited")]
        public PagedCollection<grs_installationDTO> GetRecentlyEdited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grs_installationDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                    .OrderBy(x => x.grs_installation_id)
                    .Select(x => new grs_installationDTO
                    {
                        grs_installation_id = x.grs_installation_id,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_lgu_level_name = x.lib_lgu_level.name,
                        lib_cycle_name = x.lib_cycle.name,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/grs_installation/get_recently_added")]
        public PagedCollection<grs_installationDTO> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grs_installationDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                    .OrderBy(x => x.grs_installation_id)
                    .Select(x => new grs_installationDTO
                    {
                        grs_installation_id = x.grs_installation_id,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_lgu_level_name = x.lib_lgu_level.name,
                        lib_cycle_name = x.lib_cycle.name,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/grs_installation/get_recently_edited_and_added")]
        public PagedCollection<grs_installationDTO> GetRecentlyEditedandAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<grs_installationDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model
                    .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                    .OrderBy(x => x.grs_installation_id)
                    .Select(x => new grs_installationDTO
                    {
                        grs_installation_id = x.grs_installation_id,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_lgu_level_name = x.lib_lgu_level.name,
                        lib_cycle_name = x.lib_cycle.name,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }



        [Route("api/offline/v1/grs_installation/save")]
        public async Task<IActionResult> Save(grs_installation model, bool? api)
        {


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            var record = db.grs_installation.AsNoTracking()
            .FirstOrDefault(x => 
                x.grs_installation_id == model.grs_installation_id &&            
                x.brgy_code == model.brgy_code && 
                x.city_code == model.city_code && 
                x.cycle_id == model.cycle_id && 
                x.lgu_level_id == model.lgu_level_id &&
                x.fund_source_id == model.fund_source_id &&
                x.is_deleted != true            
            );

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

                //because api is set to TRUE in sync/get
                if (api == true)
                {
                    model.push_status_id = 1;
                    model.is_deleted = false;
                }

                db.grs_installation.Add(model);
 

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


                //model.grs_installation_id = record.grs_installation_id;
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

        [Route("api/offline/v1/grs_installation/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.grs_installation

                .FirstOrDefault(x => x.grs_installation_id == id && x.is_deleted != true);



            if (model == null)
            {
                var item = new grs_installation();

                item.grs_installation_id = id;
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







 


 

        [HttpPost]
        [Route("Sync/Get/grs_installation")]
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/grs_installation/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<grs_installation>>(responseJson.Result);

//                    var all = Mapper.DynamicMap<List<grs_installation_mapping>, List<grs_installation>>(model);

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


        [Route("api/offline/v1/grs_installation/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid grs_installation_id, int push_status_id)
        {
            var grs_installation = db.grs_installation.Find(grs_installation_id);

            if (grs_installation == null)
            {
                return BadRequest("Error");
            }

            grs_installation.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Sync/Post/grs_installation")]
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
                
                var items_preselected = db.grs_installation.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.grs_installation.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || (x.push_status_id == 3 && x.is_deleted == true));
                    if (record_id != null)
                    {
                        items = items.Where(x => x.grs_installation_id == record_id);
                    }
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/grs_installation/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            //item.push_status_id = 4;
                            //item.push_date = DateTime.Now;
                            //await db.SaveChangesAsync();
                        }
                    }
                }
                else {
                    var items = db.grs_installation.Where(x => x.push_status_id == 5 || (x.push_status_id == 3 && x.is_deleted == true));
                    if (record_id != null)
                    {
                        items = items.Where(x => x.grs_installation_id == record_id);
                    }
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/grs_installation/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            //item.push_status_id = 4;
                            //item.push_date = DateTime.Now;
                            //await db.SaveChangesAsync();
                        }
                    }

                }
            }
            return Ok();
        }

        public bool record_exists(Guid id)
        {
            return db.grs_installation.Count(e => e.grs_installation_id == id) > 0;
        }

        [Route("api/offline/v1/grs_installation/exists")]
        public IActionResult api_record_exists(Guid id)
        {
            return Ok(record_exists(id));
        }


    }
}