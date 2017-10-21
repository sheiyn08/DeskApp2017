using DeskApp.Data;
using DeskApp.DataLayer;
using DeskApp.DataLayer.Entities;
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
    public class CommunityOrganizationController : Controller
    {

        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public CommunityOrganizationController(ApplicationDbContext context)
        {
            db = context;

        }

        [Route("api/export/community_organization/list")]
        public IActionResult export_list(AngularFilterModel item)
        {
            #region query


            var model = GetData(item);


            var result = model.Select(x => new
            {
                x.lib_region.region_name,
                x.lib_province.prov_name,
                x.lib_city.city_name,
                brgy_name = x.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(c => c.brgy_code == x.brgy_code).brgy_name,


                fund_source = x.lib_fund_source.name,
                cycle = x.lib_cycle.name,
                kc_mode = x.lib_enrollment.name,

                name = x.name,
            
               x.no_male,
               x.no_female,
               x.no_ip_male,
               x.no_ip_female,
 
              

            });

            // var export = result.GroupBy(x => new { x.region_name, x.prov_name, x.city_name, x.brgy_name, x.fund_source, x.cycle, x.kc_mode, x.lgu})

            #endregion

            return Ok(result);
        }

        private IQueryable<community_organization> GetData(

                 AngularFilterModel item
             )
        {
            var model = db.community_organization.Where(x => x.is_deleted != true).AsQueryable();

            //if (item.record_id != null)
            //{

            //    model = model.Where(x => x.community_organization_id == item.record_id);

            //}


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


            return model;
        }






        [HttpPost]
        [Route("api/offline/v1/community_organization/get_dto")]
        public PagedCollection<dynamic> get_dto(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = model
                    .OrderBy(x => x.community_organization_id)
                    .Select(x => new
                    {
                        community_organization_id = x.community_organization_id,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_lgu_level_name = x.lib_lgu_level.name,
                        lib_cycle_name = x.lib_cycle.name,
                        name = x.name,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                        last_modified_date = x.last_modified_date
                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/community_organization/get_recently_edited")]
        public PagedCollection<dynamic> get_recently_edited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                    .OrderBy(x => x.community_organization_id)
                    .Select(x => new
                    {
                        community_organization_id = x.community_organization_id,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_lgu_level_name = x.lib_lgu_level.name,
                        lib_cycle_name = x.lib_cycle.name,
                        name = x.name,
                        last_modified_date = x.last_modified_date,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,

                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/community_organization/get_recently_added")]
        public PagedCollection<dynamic> get_recently_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                    .OrderBy(x => x.community_organization_id)
                    .Select(x => new
                    {
                        community_organization_id = x.community_organization_id,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_lgu_level_name = x.lib_lgu_level.name,
                        lib_cycle_name = x.lib_cycle.name,
                        name = x.name,
                        last_modified_date = x.last_modified_date,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,

                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/community_organization/get_recently_edited_and_added")]
        public PagedCollection<dynamic> get_recently_edited_and_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                Items = model
                    .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                    .OrderBy(x => x.community_organization_id)
                    .Select(x => new
                    {
                        community_organization_id = x.community_organization_id,
                        lib_brgy_brgy_name = x.brgy_code == null ? "" : db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
                        lib_city_city_name = x.lib_city.city_name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_lgu_level_name = x.lib_lgu_level.name,
                        lib_cycle_name = x.lib_cycle.name,
                        name = x.name,
                        last_modified_date = x.last_modified_date,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,

                    }).Skip(currPages * size).Take(size).ToList(),
            };
        }


        [Route("api/offline/v1/community_organization/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.community_organization

                .FirstOrDefault(x => x.community_organization_id == id && x.is_deleted != true);



            if (model == null)
            {
                var item = new community_organization();

                item.community_organization_id = id;

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


        

 

        [Route("api/offline/v1/community_organization/save")]
        public async Task<IActionResult> Save(community_organization model, bool? api)
        {



            var record = db.community_organization.AsNoTracking().FirstOrDefault(x => x.community_organization_id == model.community_organization_id);

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

                db.community_organization.Add(model);
                
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
        [Route("Sync/Get/community_organization")]
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/community_organization/get_mapped?city_code=" + city_code + "&record_id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<community_organization>>(responseJson.Result);

                    //     var all = Mapper.DynamicMap<List<community_organization_mapping>, List<community_organization>>(model);

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


        [Route("api/offline/v1/community_organizations/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid community_organization_id, int push_status_id)
        {
            var comm_org = db.community_organization.Find(community_organization_id);

            if (comm_org == null)
            {
                return BadRequest("Error");
            }

            comm_org.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        //new:
        [HttpPost]
        [Route("Sync/Post/community_organization")]
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

                var items_preselected = db.community_organization.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.community_organization.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));
                    if (record_id != null)
                    {
                        items = items.Where(x => x.community_organization_id == record_id);
                    }
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/community_organization/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                }
                else {
                    var items = db.community_organization.Where(x => x.push_status_id == 5 && x.is_deleted != true);
                    if (record_id != null)
                    {
                        items = items.Where(x => x.community_organization_id == record_id);
                    }
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/community_organization/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                }               

            }
            return Ok();
        }
        


        private bool record_exists(Guid id)
        {
            return db.community_organization.Count(e => e.community_organization_id == id) > 0;
        }


    }
}
