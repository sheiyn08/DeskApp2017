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

namespace DeskApp.Controllers
{
    public class BarangayAssemblyAPIController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public BarangayAssemblyAPIController(ApplicationDbContext context)
        {
            db = context;

        }

        public async Task<bool> SaveTracking(brgy_assembly model, bool? api)
        {

            int training_category_id = 1;

            switch (model.barangay_assembly_purpose_id)
            {
                case 1:
                    training_category_id = 39;
                    break;
                case 2:
                    training_category_id = 40;
                    break;
                case 3:
                    training_category_id = 41;
                    break;
                case 4:
                    training_category_id = 42;
                    break;
                case 5:
                    training_category_id = 43;
                    break;
            }

            var record = db.ceac_tracking.FirstOrDefault(x => x.city_code == model.city_code
            && x.brgy_code == model.brgy_code
            && x.fund_source_id == model.fund_source_id
            && x.cycle_id == model.cycle_id
            && x.enrollment_id == model.enrollment_id
            && x.training_category_id == training_category_id

            );

            var ceac_list = db.ceac_list.AsNoTracking().FirstOrDefault(x =>
                                                x.city_code == model.city_code
                                                &&
                                                x.cycle_id == model.cycle_id &&
                                                x.enrollment_id == model.enrollment_id);

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
                var training_cat = db.lib_training_category.FirstOrDefault(x => x.training_category_id == training_category_id);

                if (training_cat.is_ceac != true)
                {
                    return true;
                }


                //if (api != true)
                //{
                //    model.push_status_id = 2;
                //    model.push_date = null;

                //}



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
                    training_category_id = training_category_id,

                    reference_id = model.brgy_assembly_id,

                    push_status_id = 2,
                    created_by = 0,
                    created_date = DateTime.Now,


                    actual_start = model.date_start,
                    actual_end = model.date_end,

                    plan_end = model.plan_date_end,
                    plan_start = model.plan_date_start,

                    implementation_status_id = 1,
                    ceac_tracking_id = Guid.NewGuid(),

                    lgu_level_id = 1

                };



                if (ceac.actual_end != null && ceac.actual_start != null) ceac.implementation_status_id = 1;


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
                //model.push_date = null;


                //if (api != true)
                //{
                //    model.push_status_id = 3;
                //}

                record.actual_start = model.date_start;
                record.actual_end = model.date_end;


                if (record.actual_end != null && record.actual_start != null) record.implementation_status_id = 1;


                if (record.implementation_status_id == 1)
                {
                    if (record.actual_start == null || record.actual_end == null)
                    {
                        record.implementation_status_id = 2;
                    }
                }








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



        private IQueryable<brgy_assembly> GetData(
                 AngularFilterModel item
           )
        {
            var model = db.brgy_assembly
                .Include(x => x.lib_fund_source)
                .Include(x => x.lib_cycle)
                .Include(x => x.lib_enrollment)
             .Include(x => x.lib_barangay_assembly_purpose)


                .Include(x => x.lib_region)
                .Include(x => x.lib_province)
                .Include(x => x.lib_city)
                .Include(x => x.lib_brgy)


                .Where(x => x.is_deleted != true).AsQueryable();





            #region query

            if (item.record_id != null)
            {

                model = model.Where(x => x.brgy_assembly_id == item.record_id);

            }

            if (item.barangay_assembly_purpose_id != null)
            {

                model = model.Where(x => x.barangay_assembly_purpose_id == item.barangay_assembly_purpose_id);

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
            if (item.enrollment_id != null)
            {
                model = model.Where(m => m.enrollment_id == item.enrollment_id);
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








        [HttpPost]
        [Route("api/offline/v1/barangay_assembly/get_dto")]
        public PagedCollection<brgy_assemblyDTO> GetAll(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<brgy_assemblyDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model.OrderBy(x => x.brgy_assembly_id).Skip(currPages * size).Select(brgy_assemblyDTO.SELECT).Take(size).ToList()
            };
        }

        [HttpPost]
        [Route("api/offline/v1/barangay_assembly/get_recently_edited")]
        public PagedCollection<brgy_assemblyDTO> GetRecentlyEdited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<brgy_assemblyDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model
                        .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                        .OrderBy(x => x.brgy_assembly_id).Skip(currPages * size).Select(brgy_assemblyDTO.SELECT).Take(size).ToList()
            };
        }

        [HttpPost]
        [Route("api/offline/v1/barangay_assembly/get_recently_added")]
        public PagedCollection<brgy_assemblyDTO> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<brgy_assemblyDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model
                        .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                        .OrderBy(x => x.brgy_assembly_id).Skip(currPages * size).Select(brgy_assemblyDTO.SELECT).Take(size).ToList()
            };
        }

        [HttpPost]
        [Route("api/offline/v1/barangay_assembly/get_recently_edited_and_added")]
        public PagedCollection<brgy_assemblyDTO> GetRecentlyEditedandAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<brgy_assemblyDTO>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model
                        .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                        .OrderBy(x => x.brgy_assembly_id).Skip(currPages * size).Select(brgy_assemblyDTO.SELECT).Take(size).ToList()
            };
        }




        [Route("api/offline/v1/barangay_assembly/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.brgy_assembly.FirstOrDefault(x => x.brgy_assembly_id == id);
            
            if (model == null)
            {
                var item = new brgy_assembly();

                item.brgy_assembly_id = id;
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




        [Route("api/offline/v1/barangay_assembly/save")]
        public async Task<IActionResult> Save(brgy_assembly model, bool? api)
        {
            var record = db.brgy_assembly.AsNoTracking().FirstOrDefault(x => x.brgy_assembly_id == model.brgy_assembly_id);

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


                db.brgy_assembly.Add(model);


                try
                {
                    await db.SaveChangesAsync();
                    await SaveTracking(model, false);
                    return Ok(model);
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
                    await SaveTracking(model, false);
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
        }


        public async Task<IActionResult> SaveRepresentedIP(brgy_assembly_ip model, bool? api)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var record = db.brgy_assembly_ip.AsNoTracking().FirstOrDefault(x => x.brgy_assembly_ip_id == model.brgy_assembly_ip_id);

            if (record == null)
            {




                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;

                }


                model.created_by = 0;
                model.created_date = DateTime.Now;
                model.approval_id = 3;
                model.is_deleted = false;

                db.brgy_assembly_ip.Add(model);


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
        [Route("Sync/Get/barangay_assembly")]
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/barangay_assembly/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<brgy_assembly>>(responseJson.Result);



                    foreach (var item in model.ToList())
                    {
                        await Save(item, true);
                    }

                    await GetOnlineRepresentedIP(username, password, city_code, record_id);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }


        public async Task<ActionResult> GetOnlineRepresentedIP(string username, string password, string city_code = null, Guid? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/barangay_assembly/represented_ip/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<brgy_assembly_ip>>(responseJson.Result);



                    foreach (var item in model.ToList())
                    {
                        await SaveRepresentedIP(item, true);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }

        [Route("api/offline/v1/barangay_assembly/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid brgy_assembly_id, int push_status_id)
        {
            var ba = db.brgy_assembly.Find(brgy_assembly_id);

            if (ba == null)
            {
                return BadRequest("Error");
            }

            ba.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Sync/Post/barangay_assembly")]
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

                
                var items_preselected = db.brgy_assembly.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.brgy_assembly.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || (x.push_status_id == 3 && x.is_deleted == true)).ToList();
                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_assembly/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                        else {
                            return BadRequest();
                        }
                    }
                }
                else {
                    var items = db.brgy_assembly.Where(x => x.push_status_id == 5 || (x.push_status_id == 3 && x.is_deleted == true)).ToList();
                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_assembly/save", data).Result;
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
            await PostRepresentedIp(username, password, record_id);
            return Ok();
        }


        
        public async Task<bool> PostRepresentedIp(string username, string password, Guid record_id)
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

                var items = db.brgy_assembly_ip.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || (x.push_status_id == 3 && x.is_deleted == true)).ToList();
                foreach (var item in items)
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_assembly/represented_ip/save", data).Result;
                    // response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        [HttpPost]
        [Route("/api/export/barangay_assembly/list")]
        public IActionResult export_list(AngularFilterModel item)
        {

            var list = GetData(item);

            var result = from s in list
                         select new
                         {
                             s.brgy_assembly_id,

                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             s.lib_brgy.brgy_name,

                             s.brgy_code,

                             fund_source = s.lib_fund_source.name,
                             cycle = s.lib_cycle.name,
                             kc_mode = s.lib_enrollment.name,
                             ba_purpose = s.lib_barangay_assembly_purpose.name,

                             ba_start = s.date_start,
                             ba_end = s.date_end,
                             ba_venue = s.venue,

                             highlights = @s.highlights,



                             //s.no_families,
                             s.no_household,

                             s.no_atn_male,
                             s.no_atn_female,
                             s.no_ip_male,
                             s.no_ip_female,
                             s.no_old_male,
                             s.no_old_female,
                             s.no_pantawid_household,
                             s.no_pantawid_family,

                             s.no_slp_household,
                             s.no_slp_family,

                             s.no_ip_household,
                             s.no_ip_family,

                             s.total_household_in_barangay,
                             //s.total_families_in_barangay,

                             is_sector_academe = s.is_sector_academe != null ? "Yes" : "",
                             is_sector_business = s.is_sector_business != null ? "Yes" : "",
                             is_sector_pwd = s.is_sector_pwd != null ? "Yes" : "",
                             is_sector_farmer = s.is_sector_farmer != null ? "Yes" : "",
                             is_sector_fisherfolks = s.is_sector_fisherfolks != null ? "Yes" : "",
                             is_sector_government = s.is_sector_government != null ? "Yes" : "",
                             is_sector_ip = s.is_sector_ip != null ? "Yes" : "",
                             is_sector_ngo = s.is_sector_ngo != null ? "Yes" : "",
                             is_sector_po = s.is_sector_po != null ? "Yes" : "",
                             is_sector_religios = s.is_sector_religios != null ? "Yes" : "",
                             is_sector_senior = s.is_sector_senior != null ? "Yes" : "",
                             is_sector_women = s.is_sector_women != null ? "Yes" : "",
                             is_sector_youth = s.is_sector_youth != null ? "Yes" : "",
                         };

            return Ok(result);

        }

        [HttpPost]
        [Route("/api/export/barangay_assembly/household_representation")]
        public IActionResult export_summary_representation(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model.GroupBy(x => new
            {
                x.lib_brgy.brgy_name,
                fund_source = x.lib_fund_source.name,
                cycle = x.lib_cycle.name,
                kc_mode = x.lib_enrollment.name,
                region_name = x.lib_region.region_name,
                prov_name = x.lib_province.prov_name,
                city_name = x.lib_city.city_name
            }).Select(x => new
            {
                fund_source_name = x.Key.fund_source,
                cycle_name = x.Key.cycle,
                kc_mode = x.Key.kc_mode,
                region_name = x.Key.region_name,
                prov_name = x.Key.prov_name,
                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,

                //first_ba_no_household = x.Sum(c => c.no_household),
                //total_household_in_barangay = x.Sum(c => c.total_household_in_barangay),

                //no_pantawid_household = x.Sum(c => c.no_pantawid_household),
                //total_household_pantawid_in_barangay = x.Sum(c => c.total_household_pantawid_in_barangay),

                //no_ip_household = x.Sum(c => c.no_ip_household),
                //total_household_ip_in_barangay = x.Sum(c => c.total_household_ip_in_barangay),

                //   no_ip_household = x.Sum(c => c.no_ip_household),
                //   total_household_ip_in_barangay = x.Sum(c => c.total_household_ip_in_barangay)




            }).ToList();




            return Ok(result);
        }


        //ACT Monthly Report: --- AS OF
        [HttpPost]
        [Route("/api/export/report/participation_rates_per_BAs/as_of")]
        public IActionResult export_actreport_asof(AngularFilterModel item)
        {
            DateTime? as_of = item.as_of;
            int? selected_fund_source = item.fund_source_id;

            var model = GetData(item);

            var itemsCovered = model.Where(x => x.date_start <= as_of && x.fund_source_id == selected_fund_source);

            var result = itemsCovered.GroupBy(x => new
            {
                fund_source = x.lib_fund_source.fund_source_id,
                cycle_name = x.lib_cycle.name,
                region_name = x.lib_region.region_name,
                prov_name = x.lib_province.prov_name,
                city_name = x.lib_city.city_name,
                brgy_name = x.lib_brgy.brgy_name,
            }).
            Select(x => new
            {
                fund_source_id = x.Key.fund_source,
                cycle_name = x.Key.cycle_name,
                region_name = x.Key.region_name,
                prov_name = x.Key.prov_name,
                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,

                //household:
                first_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_household),
                first_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_in_barangay),
                second_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_household),
                second_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_in_barangay),
                third_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_household),
                third_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_in_barangay),
                fourth_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_household),
                fourth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_in_barangay),
                fifth_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_household),
                fifth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_in_barangay),

                //IP household:
                first_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_ip_household),
                first_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_ip_in_barangay),
                second_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_ip_household),
                second_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_ip_in_barangay),
                third_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_ip_household),
                third_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_ip_in_barangay),
                fourth_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_ip_household),
                fourth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_ip_in_barangay),
                fifth_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_ip_household),
                fifth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_ip_in_barangay),

                //male:
                first_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_male),
                second_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_male),
                third_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_male),
                fourth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_male),
                fifth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_male),

                //female:
                first_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_female),
                second_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_female),
                third_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_female),
                fourth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_female),
                fifth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_female),

                first_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_male + c.no_atn_female),
                second_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_male + c.no_atn_female),
                third_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_male + c.no_atn_female),
                fourh_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_male + c.no_atn_female),
                fifth_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_male + c.no_atn_female),
                                
            }).ToList();

            return Ok(result);            

        }

        //ACT Monthly Report: --- FOR THE PERIOD OF
        [HttpPost]
        [Route("/api/export/report/participation_rates_per_BAs/for_the_period_of")]
        public IActionResult export_actreport_fortheperiodof(AngularFilterModel item)
        {
            DateTime? fortheperiodof_from = item.fortheperiodof_from;
            DateTime? fortheperiodof_to = item.fortheperiodof_to;
            int? selected_fund_source = item.fund_source_id;

            var model = GetData(item);

            var itemsCovered = model.Where(x => x.fund_source_id == selected_fund_source && (x.date_start >= fortheperiodof_from && x.date_start <= fortheperiodof_to));

            var result = itemsCovered.GroupBy(x => new
            {
                fund_source = x.lib_fund_source.fund_source_id,
                cycle_name = x.lib_cycle.name,
                region_name = x.lib_region.region_name,
                prov_name = x.lib_province.prov_name,
                city_name = x.lib_city.city_name,
                brgy_name = x.lib_brgy.brgy_name,
            }).
            Select(x => new
            {
                fund_source_id = x.Key.fund_source,
                cycle_name = x.Key.cycle_name,
                region_name = x.Key.region_name,
                prov_name = x.Key.prov_name,
                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,

                //household:
                first_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_household),
                first_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_in_barangay),
                second_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_household),
                second_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_in_barangay),
                third_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_household),
                third_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_in_barangay),
                fourth_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_household),
                fourth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_in_barangay),
                fifth_ba_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_household),
                fifth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_in_barangay),

                //IP household:
                first_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_ip_household),
                first_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_ip_in_barangay),
                second_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_ip_household),
                second_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_ip_in_barangay),
                third_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_ip_household),
                third_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_ip_in_barangay),
                fourth_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_ip_household),
                fourth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_ip_in_barangay),
                fifth_ba_ip_hh_represented = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_ip_household),
                fifth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_ip_in_barangay),

                //male:
                first_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_male),
                second_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_male),
                third_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_male),
                fourth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_male),
                fifth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_male),

                //female:
                first_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_female),
                second_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_female),
                third_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_female),
                fourth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_female),
                fifth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_female),

                first_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_male + c.no_atn_female),
                second_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_male + c.no_atn_female),
                third_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_male + c.no_atn_female),
                fourh_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_male + c.no_atn_female),
                fifth_ba_total_male_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_male + c.no_atn_female),

            }).ToList();

            return Ok(result);

        }




        [HttpPost]
        [Route("/api/export/barangay_assembly/summary")]
        public IActionResult export_summary(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model.GroupBy(x => new
            {
                x.lib_brgy.brgy_name,
                fund_source_name = x.lib_fund_source.name,
                cycle_name = x.lib_cycle.name,
                kc_mode = x.lib_enrollment.name,
                region_name = x.lib_region.region_name,
                prov_name = x.lib_province.prov_name,
                city_name = x.lib_city.city_name
            }).Select(x => new
            {
                fund_source_name = x.Key.fund_source_name,
                cycle_name = x.Key.cycle_name,
                kc_mode = x.Key.kc_mode,
                region_name = x.Key.region_name,

                prov_name = x.Key.prov_name,

                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,



                first_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_male),
                first_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_atn_female),
                first_ba_ip_male = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_ip_male),
                first_ba_ip_female = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_ip_female),

                second_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_male),
                second_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_atn_female),
                second_ba_ip_male = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_ip_male),
                second_ba_ip_female = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_ip_female),

                third_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_male),
                third_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_atn_female),
                third_ba_ip_male = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_ip_male),
                third_ba_ip_female = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_ip_female),

                fourth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_male),
                fourth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_atn_female),
                fourth_ba_ip_male = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_ip_male),
                fourth_ba_ip_female = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_ip_female),

                fifth_ba_male = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_male),
                fifth_ba_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_atn_female),
                fifth_ba_ip_male = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_ip_male),
                fifth_ba_ip_female = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_ip_female),

                //hhs
                first_ba_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_household),
                first_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_in_barangay),

                second_ba_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_household),
                second_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_in_barangay),

                third_ba_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_household),
                third_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_in_barangay),

                fourth_ba_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_household),
                fourth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_in_barangay),

                fifth_ba_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_household),
                fifth_ba_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_in_barangay),

                //IP HHs
                first_ba_ip_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_ip_household),
                first_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_ip_in_barangay),

                second_ba_ip_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_ip_household),
                second_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_ip_in_barangay),

                third_ba_ip_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_ip_household),
                third_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_ip_in_barangay),

                fourth_ba_ip_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_ip_household),
                fourth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_ip_in_barangay),

                fifth_ba_ip_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_ip_household),
                fifth_ba_ip_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_ip_in_barangay),

                //Pantawid
                first_ba_pp_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_pantawid_household),
                first_ba_pp_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.total_household_pantawid_in_barangay),

                second_ba_pp_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_pantawid_household),
                second_ba_pp_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.total_household_pantawid_in_barangay),

                third_ba_pp_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_pantawid_household),
                third_ba_pp_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.total_household_pantawid_in_barangay),

                fourth_ba_pp_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_pantawid_household),
                fourth_ba_pp_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.total_household_pantawid_in_barangay),

                fifth_ba_pp_hh_atn = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_pantawid_household),
                fifth_ba_pp_hh_total = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.total_household_pantawid_in_barangay),


                //Pantawid & IP families with atleast one representative:
                first_ba_pantawid_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_pantawid_family),
                second_ba_pantawid_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_pantawid_family),
                third_ba_pantawid_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_pantawid_family),
                fourth_ba_pantawid_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_pantawid_family),
                fifth_ba_pantawid_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_pantawid_family),

                first_ba_ip_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 1).Sum(c => c.no_ip_family),
                second_ba_ip_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 2).Sum(c => c.no_ip_family),
                third_ba_ip_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 3).Sum(c => c.no_ip_family),
                fourth_ba_ip_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 4).Sum(c => c.no_ip_family),
                fifth_ba_ip_fam_rep = x.Where(c => c.barangay_assembly_purpose_id == 5).Sum(c => c.no_ip_family)


            }).ToList();

            //var export = result.GroupBy(x => new
            //{
            //    x.region_name,
            //    x.prov_name,
            //    x.city_name,
            //    x.brgy_name,

            //    x.fund_source_name,
            //    x.cycle_name,
            //    x.kc_mode,

            //}).Select(x => new
            //{
            //    fund_source_name = x.Key.fund_source_name,
            //    cycle_name = x.Key.cycle_name,
            //    kc_mode = x.Key.kc_mode,
            //    region_name = x.Key.region_name,

            //    prov_name = x.Key.prov_name,

            //    city_name = x.Key.city_name,
            //    brgy_name = x.Key.brgy_name,





            //    first_ba_male = x.Sum(c => c.first_ba_male),
            //    first_ba_female = x.Sum(c => c.first_ba_female),
            //    first_ba_ip_male = x.Sum(c => c.first_ba_ip_male),
            //    first_ba_ip_female = x.Sum(c => c.first_ba_ip_female),

            //    second_ba_male = x.Sum(c => c.second_ba_male),
            //    second_ba_female = x.Sum(c => c.second_ba_female),
            //    second_ba_ip_male = x.Sum(c => c.second_ba_ip_male),
            //    second_ba_ip_female = x.Sum(c => c.second_ba_ip_female),

            //    third_ba_male = x.Sum(c => c.third_ba_male),
            //    third_ba_female = x.Sum(c => c.third_ba_female),
            //    third_ba_ip_male = x.Sum(c => c.third_ba_ip_male),
            //    third_ba_ip_female = x.Sum(c => c.third_ba_ip_female),

            //    fourth_ba_male = x.Sum(c => c.fourth_ba_male),
            //    fourth_ba_female = x.Sum(c => c.fourth_ba_female),
            //    fourth_ba_ip_male = x.Sum(c => c.fourth_ba_ip_male),
            //    fourth_ba_ip_female = x.Sum(c => c.fourth_ba_ip_female),

            //    fifth_ba_male = x.Sum(c => c.fifth_ba_male),
            //    fifth_ba_female = x.Sum(c => c.fifth_ba_female),
            //    fifth_ba_ip_male = x.Sum(c => c.fifth_ba_ip_male),
            //    fifth_ba_ip_female = x.Sum(c => c.fifth_ba_ip_female),
            //    //total_hh_male = x.Sum(c => c.total_hh_male),

            //    //total_hh_female = x.Sum(c => c.total_hh_female),
            //    //total_hh_ip_male = x.Sum(c => c.total_hh_ip_male),
            //    //total_hh_ip_female = x.Sum(c => c.total_hh_ip_female),
            //    //total_ip_male = x.Sum(c => c.total_ip_male),
            //    //total_ip_female = x.Sum(c => c.total_ip_female),
            //    //total_ip_hh_male = x.Sum(c => c.total_ip_hh_male),
            //    //total_ip_hh_female = x.Sum(c => c.total_ip_hh_female),
            //    //total_4P_male = x.Sum(c => c.total_4P_male),
            //    //total_4P_female = x.Sum(c => c.total_4P_female),
            //    //total_4P_ip_male = x.Sum(c => c.total_4P_ip_male),
            //    //total_4P_ip_female = x.Sum(c => c.total_4P_ip_female),
            //    //total_4Prep_male = x.Sum(c => c.total_4Prep_male),
            //    //total_4Prep_female = x.Sum(c => c.total_4Prep_female),
            //    //total_4Prep_ip_male = x.Sum(c => c.total_4Prep_ip_male),
            //    //total_4Prep_ip_female = x.Sum(c => c.total_4Prep_ip_female),
            //    //total_IPrep_male = x.Sum(c => c.total_IPrep_male),
            //    //total_IPrep_female = x.Sum(c => c.total_IPrep_female),
            //    //total_IPrep_ip_male = x.Sum(c => c.total_IPrep_ip_male),
            //    //total_IPrep_ip_female = x.Sum(c => c.total_IPrep_ip_female),


            //});


            return Ok(result);
        }

        #region DQA : Barangay Assembly

        //DQA: BA Count -- Display BAs that does not meet or exceeds the target number of BAs per cycle
        //For regular : target is 5 BAs per cycle
        //For accelerated : target is 3 BAs per cycle
        [HttpPost]
        [Route("/api/export/barangay_assembly/dqa/count")]
        public IActionResult export_dqa_count(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model
                .GroupBy(x => new
                {                    
                    x.lib_region,
                    x.lib_province,
                    x.lib_city,
                    x.lib_brgy,
                    x.lib_fund_source,
                    x.lib_cycle,                    
                    x.lib_enrollment,
                    x.brgy_code,
                    x.enrollment_id,
                    x.cycle_id
                })
                .Select(x => new
                {
                    Region = x.Key.lib_region.region_name,
                    Province = x.Key.lib_province.prov_name,
                    Municipality = x.Key.lib_city.city_name,
                    Barangay = x.Key.lib_brgy.brgy_name,
                    Project = x.Key.lib_fund_source.name,
                    Cycle = x.Key.lib_cycle.name,
                    KC_Mode = x.Key.lib_enrollment.name,
                    BA_Target_Count = x.Key.lib_enrollment.enrollment_id == 1 ? 3 : 5,
                    BA_Actual_Count = x.Count(),
                    no_of_first_ba = x.Count(c => c.barangay_assembly_purpose_id == 1),
                    no_of_second_ba = x.Count(c => c.barangay_assembly_purpose_id == 2),
                    no_of_third_ba = x.Count(c => c.barangay_assembly_purpose_id == 3),
                    no_of_fourth_ba = x.Count(c => c.barangay_assembly_purpose_id == 4),
                    no_of_fifth_ba = x.Count(c => c.barangay_assembly_purpose_id == 5),
                }).ToList();

            var export = result
                .GroupBy(x => new
                {
                    x.Region,
                    x.Province,
                    x.Municipality,
                    x.Barangay,
                    x.Project,
                    x.Cycle,
                    x.KC_Mode
                }).Select(x => new
                {
                    Region = x.Key.Region,
                    Province = x.Key.Province,
                    Municipality = x.Key.Municipality,
                    Barangay = x.Key.Barangay,
                    Project = x.Key.Project,
                    Cycle = x.Key.Cycle,
                    KC_Mode = x.Key.KC_Mode,
                    BA_Target_Count = x.Key.KC_Mode == "DROP" ? 3 : 5,
                    BA_Actual_Count = x.Sum(c => c.no_of_first_ba) + x.Sum(c => c.no_of_second_ba) + x.Sum(c => c.no_of_third_ba) + x.Sum(c => c.no_of_fourth_ba) + x.Sum(c => c.no_of_fifth_ba),
                    //no_of_first_ba = x.Sum(c => c.no_of_first_ba),
                    //no_of_second_ba = x.Sum(c => c.no_of_second_ba),
                    //no_of_third_ba = x.Sum(c => c.no_of_third_ba),
                    //no_of_fourth_ba = x.Sum(c => c.no_of_fourth_ba),
                    //no_of_fifth_ba = x.Sum(c => c.no_of_fifth_ba),
                })
                .Where(x => x.BA_Target_Count != x.BA_Actual_Count);
            return Ok(export);
        }

        //DQA: BA null values -- Display BAs encoded that contains null values on the minimum required fields
        [HttpPost]
        [Route("/api/export/barangay_assembly/dqa/null_required_fields")]
        public IActionResult export_dqa_null_required_fields(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model
                .Select(x => new
                {
                    BA_Unique_Id = x.brgy_assembly_id,
                    Region = x.region_code == null ? null : x.lib_region.region_name,
                    Province = x.prov_code == null ? null : x.lib_province.prov_name,
                    Municipality = x.city_code == null ? null : x.lib_city.city_name,
                    Barangay = x.brgy_code == null ? null : x.lib_brgy.brgy_name,
                    Project = x.fund_source_id == null ? null : x.lib_fund_source.name,
                    Cycle = x.cycle_id == null ? null : x.lib_cycle.name,
                    KC_Mode = x.enrollment_id == null ? null : x.lib_enrollment.name,
                    Purpose = x.barangay_assembly_purpose_id == null ? null : x.lib_barangay_assembly_purpose.name,
                    Start_Date = x.date_start == null ? null : x.date_start,
                    End_Date = x.date_end == null ? null : x.date_end,
                    Venue = x.venue == null ? null : x.venue,
                    Is_Incentives = x.is_incentive == null || x.is_incentive == false ? null : "Yes",
                    From_Savings = x.is_savings == null || x.is_savings == false ? null : "Yes",
                    Is_LGU_Led = x.is_lgu_led == null || x.is_lgu_led == false ? null : "Yes",
                    Attendance_Total_Male = x.no_atn_male == null ? null : x.no_atn_male,
                    Attendance_Total_Female = x.no_atn_female == null ? null : x.no_atn_female,
                    Attendance_IP_Male = x.no_ip_male == null ? null : x.no_ip_male,
                    Attendance_IP_Female = x.no_ip_female == null ? null : x.no_ip_female,
                    Attendance_Senior_Male = x.no_old_male == null ? null : x.no_old_male,
                    Attendance_Senior_Female = x.no_old_female == null ? null : x.no_old_female,
                    Attendance_BLGU_Male = x.no_lgu_male == null ? null : x.no_lgu_male,
                    Attendance_BLGU_Female = x.no_lgu_female == null ? null : x.no_lgu_female,
                    HH_Represented = x.no_household == null ? null : x.no_household,
                    HH_Total = x.total_household_in_barangay == null ? null : x.total_household_in_barangay,
                    Pantawid_Fam_Represented = x.no_pantawid_family == null ? null : x.no_pantawid_family,
                    Pantawid_Fam_Total = x.no_pantawid_family_in_barangay == null ? null : x.no_pantawid_family_in_barangay,
                    Pantawid_HH_Represented = x.no_pantawid_household == null ? null : x.no_pantawid_household,
                    Pantawid_HH_Total = x.total_household_pantawid_in_barangay == null ? null : x.total_household_pantawid_in_barangay,
                    SLP_Fam_Represented = x.no_slp_family == null ? null : x.no_slp_family,
                    SLP_Fam_Total = x.no_slp_family_in_barangay == null ? null : x.no_slp_family_in_barangay,
                    SLP_HH_Represented = x.no_slp_household == null ? null : x.no_slp_household,
                    SLP_HH_Total = x.total_household_slp_in_barangay == null ? null : x.total_household_slp_in_barangay,
                    IP_Fam_Represented = x.no_ip_family == null ? null : x.no_ip_family,
                    IP_Fam_Total = x.no_ip_family_in_barangay == null ? null : x.no_ip_family_in_barangay,
                    IP_HH_Represented = x.no_ip_household == null ? null : x.no_ip_household,
                    IP_HH_Total = x.total_household_ip_in_barangay == null ? null : x.total_household_ip_in_barangay,
                    Highlights = x.highlights == null ? null : x.highlights,
                    Sector_Represented = x.is_sector_academe == null && x.is_sector_business == null && x.is_sector_pwd == null && x.is_sector_farmer == null && x.is_sector_fisherfolks == null && x.is_sector_government == null && x.is_sector_ip == null && x.is_sector_ngo == null && x.is_sector_po == null && x.is_sector_religios == null && x.is_sector_senior == null && x.is_sector_women == null && x.is_sector_youth == null ? null : "With sector represented",
                    Represented_IP_Group = db.brgy_assembly_ip.Any(ip => ip.brgy_assembly_id == x.brgy_assembly_id && ip.is_deleted != true) == true ? "With IP Group represented" : null,
                    IP_Leader = x.ip_leader == null ? null : x.ip_leader
                })
                .Where(x => x.Region == null || x.Province == null || x.Municipality == null || x.Barangay == null ||
                            x.Project == null || x.Cycle == null || x.KC_Mode == null || x.Purpose == null ||
                            x.Start_Date == null || x.End_Date == null || x.Venue == null ||
                            x.Attendance_Total_Male == null || x.Attendance_Total_Female == null || 
                            x.Attendance_IP_Male == null || x.Attendance_IP_Female == null ||
                            x.Attendance_Senior_Male == null || x.Attendance_Senior_Female == null ||
                            x.Attendance_BLGU_Male == null || x.Attendance_BLGU_Female == null ||
                            x.HH_Represented == null || x.HH_Total == null ||
                            x.Pantawid_Fam_Represented == null || x.Pantawid_Fam_Total == null ||
                            x.Pantawid_HH_Represented == null || x.Pantawid_HH_Total == null ||
                            x.SLP_Fam_Represented == null ||x.SLP_Fam_Total == null ||
                            x.SLP_HH_Represented == null || x.SLP_HH_Total == null ||
                            x.IP_Fam_Represented == null || x.IP_Fam_Total == null ||
                            x.IP_HH_Represented == null || x.IP_HH_Total == null ||
                            x.Highlights == null ||
                            x.Sector_Represented == null ||
                            x.Represented_IP_Group == null ||
                            x.IP_Leader == null)
                .ToList();
            
            return Ok(result);
        }

        //DQA: BA Conducted Purpose for Verification
        //Same RPMB, cycle and BA purpose
        [HttpPost]
        [Route("/api/export/barangay_assembly/dqa/purpose_for_verification")]
        public IActionResult export_dqa_purpose_for_verification(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model
                .GroupBy(x => new
                {
                    x.lib_region,
                    x.lib_province,
                    x.lib_city,
                    x.lib_brgy,
                    x.lib_fund_source,
                    x.lib_cycle,
                    x.lib_barangay_assembly_purpose,
                    x.brgy_code,
                    x.barangay_assembly_purpose_id,
                    x.cycle_id
                })
                .Select(x => new
                {
                    Region = x.Key.lib_region.region_name,
                    Province = x.Key.lib_province.prov_name,
                    Municipality = x.Key.lib_city.city_name,
                    Barangay = x.Key.lib_brgy.brgy_name,
                    Project = x.Key.lib_fund_source.name,
                    Cycle = x.Key.lib_cycle.name,
                    Ba_Purpose = x.Key.lib_barangay_assembly_purpose.name,
                    BA_Purpose_Count = x.Count()
                })
                .Where(x => x.BA_Purpose_Count >= 2) //if there are two or more records having the same brgy and cycle, and same BA purpose
                .ToList();

            return Ok(result);
        }

        //DQA: BA Household Participation
        //BA with more than 100% participation rate, less than 50% participation rate, or null
        [HttpPost]
        [Route("/api/export/barangay_assembly/dqa/hh_participation")]
        public IActionResult export_dqa_hh_participation(AngularFilterModel item)
        {
            var model = GetData(item);
            
            var result = model
                .Select(x => new
                {
                    BA_Unique_Id = x.brgy_assembly_id,
                    Region = x.lib_region.region_name,
                    Province = x.lib_province.prov_name,
                    Municipality = x.lib_city.city_name,
                    Barangay = x.lib_brgy.brgy_name,
                    Project = x.lib_fund_source.name,
                    Cycle = x.lib_cycle.name,
                    Ba_Purpose = x.lib_barangay_assembly_purpose.name,
                    hh_rep = (double)x.no_household,
                    hh_total = (double)x.total_household_in_barangay,
                    Household_Participation_Rate = (x.no_household * 100.0f) / x.total_household_in_barangay,
                })
                .Where(x => x.Household_Participation_Rate > 100 || x.Household_Participation_Rate < 50 || x.Household_Participation_Rate == null) 
                .ToList();

            var convert_decimal = result
                .Select(x => new
                {
                    BA_Unique_Id = x.BA_Unique_Id,
                    Region = x.Region,
                    Province = x.Province,
                    Municipality = x.Municipality,
                    Barangay = x.Barangay,
                    Project = x.Project,
                    Cycle = x.Cycle,
                    Ba_Purpose = x.Ba_Purpose,
                    hh_rep = x.hh_rep,
                    hh_total = x.hh_total,
                    Household_Participation_Rate = x.Household_Participation_Rate.Value.ToString("n2")
                })
                .ToList();

            var export = convert_decimal
                .Select(x => new
                {
                    BA_Unique_Id = x.BA_Unique_Id,
                    Region = x.Region,
                    Province = x.Province,
                    Municipality = x.Municipality,
                    Barangay = x.Barangay,
                    Project = x.Project,
                    Cycle = x.Cycle,
                    Ba_Purpose = x.Ba_Purpose,
                    hh_rep = x.hh_rep,
                    hh_total = x.hh_total,
                    Household_Participation_Rate = x.Household_Participation_Rate + "%"
                });

            return Ok(export);
        }

        //DQA: BA attendees with null values or when the ratio reaches 70-30 or higher
        [HttpPost]
        [Route("/api/export/barangay_assembly/dqa/ba_attendance_ratio")]
        public IActionResult export_dqa_ba_attendance_ratio(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model
                .Select(x => new
                {
                    BA_Unique_Id = x.brgy_assembly_id,
                    Region = x.lib_region.region_name,
                    Province = x.lib_province.prov_name,
                    Municipality = x.lib_city.city_name,
                    Barangay = x.lib_brgy.brgy_name,
                    Project = x.lib_fund_source.name,
                    Cycle = x.lib_cycle.name,
                    Ba_Purpose = x.lib_barangay_assembly_purpose.name,
                    Male_Count = x.no_atn_male,
                    Female_Count = x.no_atn_female,
                    Male_Percentage = (x.no_atn_male * 100.0f) / (x.no_atn_male + x.no_atn_female),
                    Female_Percentage = (x.no_atn_female * 100.0f) / (x.no_atn_male + x.no_atn_female)
                })
                .Where(x => (x.Male_Percentage <= 30 || x.Male_Percentage >= 70) || (x.Female_Percentage <= 30 || x.Female_Percentage >= 70) || x.Male_Count == null || x.Female_Count == null)
                .ToList();

            var convert_decimal = result
                .Select(x => new
                {
                    BA_Unique_Id = x.BA_Unique_Id,
                    Region = x.Region,
                    Province = x.Province,
                    Municipality = x.Municipality,
                    Barangay = x.Barangay,
                    Project = x.Project,
                    Cycle = x.Cycle,
                    Ba_Purpose = x.Ba_Purpose,
                    Male_Count = x.Male_Count,
                    Female_Count = x.Female_Count,
                    Male_Percentage = x.Male_Percentage.Value.ToString("n2"),
                    Female_Percentage = x.Female_Percentage.Value.ToString("n2")
                })
                .ToList();

            var export = convert_decimal
                .Select(x => new
                {
                    BA_Unique_Id = x.BA_Unique_Id,
                    Region = x.Region,
                    Province = x.Province,
                    Municipality = x.Municipality,
                    Barangay = x.Barangay,
                    Project = x.Project,
                    Cycle = x.Cycle,
                    Ba_Purpose = x.Ba_Purpose,
                    Male_Count = x.Male_Count,
                    Female_Count = x.Female_Count,
                    Male_Percentage = x.Male_Percentage + "%",
                    Female_Percentage = x.Female_Percentage + "%"
                });

            return Ok(export);
        }


        #endregion


        //[HttpPost]
        //[Route("/api/export/barangay_assembly/dqa/count")]
        //public IActionResult export_dqa_count(AngularFilterModel item)
        //{
        //    var model = GetData(item);

        //    var result = model.GroupBy(x => new
        //    {
        //        x.lib_brgy,
        //        x.lib_region,
        //        x.lib_province,
        //        x.lib_city,

        //        x.lib_cycle,
        //        x.lib_fund_source,
        //        x.lib_enrollment,

        //        x.brgy_code,
        //        x.enrollment_id,
        //        x.cycle_id

        //    }).Select(x => new
        //    {


        //        fund_source_name = x.Key.lib_fund_source.name,
        //        cycle_name = x.Key.lib_cycle.name,
        //        kc_mode = x.Key.lib_enrollment.name,
        //        region_name = x.Key.lib_region.region_name,

        //        prov_name = x.Key.lib_province.prov_name,

        //        city_name = x.Key.lib_city.city_name,
        //        brgy_name = x.Key.lib_brgy.brgy_name,

        //        target_ba = x.Key.lib_enrollment.enrollment_id == 1 ? 3 : 5,
        //        total_conducted = x.Count(),

        //        no_of_first_ba = x.Count(c => c.barangay_assembly_purpose_id == 1),

        //        no_of_second_ba = x.Count(c => c.barangay_assembly_purpose_id == 2),


        //        no_of_third_ba = x.Count(c => c.barangay_assembly_purpose_id == 3),


        //        no_of_fourth_ba = x.Count(c => c.barangay_assembly_purpose_id == 4),

        //        no_of_fifth_ba = x.Count(c => c.barangay_assembly_purpose_id == 5),

        //    }).ToList();

        //    var export = result.GroupBy(x => new
        //    {
        //        x.region_name,
        //        x.prov_name,
        //        x.city_name,
        //        x.brgy_name,

        //        x.fund_source_name,
        //        x.cycle_name,
        //        x.kc_mode,

        //    }).Select(x => new
        //    {
        //        fund_source_name = x.Key.fund_source_name,
        //        cycle_name = x.Key.cycle_name,
        //        kc_mode = x.Key.kc_mode,
        //        region_name = x.Key.region_name,

        //        prov_name = x.Key.prov_name,

        //        city_name = x.Key.city_name,
        //        brgy_name = x.Key.brgy_name,

        //        target_ba = x.Key.kc_mode.Contains("ac") ? "3" : "5",

        //        total_conducted =
        //        x.Sum(c => c.no_of_first_ba)
        //        + x.Sum(c => c.no_of_second_ba)
        //        + x.Sum(c => c.no_of_third_ba)
        //        + x.Sum(c => c.no_of_fourth_ba)
        //        + x.Sum(c => c.no_of_fifth_ba),

        //        no_of_first_ba = x.Sum(c => c.no_of_first_ba),

        //        no_of_second_ba = x.Sum(c => c.no_of_second_ba),


        //        no_of_third_ba = x.Sum(c => c.no_of_third_ba),


        //        no_of_fourth_ba = x.Sum(c => c.no_of_fourth_ba),

        //        no_of_fifth_ba = x.Sum(c => c.no_of_fifth_ba),

        //    });

        //    return Ok(export);
        //}

        #region Represented IP
        [Route("api/offline/v1/barangay_assembly/post/represented_ip")]
        public bool post_location_coverage(int ip_group_id, Guid id)
        {

            string Area = null;


            //if (brgy_code.Length != 9)
            //{
            //    brgy_code = "0" + brgy_code;
            //}

            Area = db.lib_ip_group.SingleOrDefault(x => x.ip_group_id == ip_group_id).name;

            var access = db.brgy_assembly_ip.FirstOrDefault(x => x.brgy_assembly_id == id && x.ip_group_id == ip_group_id);

            if (access != null)
            {


                access.Selected = false;


                db.SaveChanges();



            }

            else
            {


                var item = new brgy_assembly_ip
                {
                    brgy_assembly_id = id,

                    ip_group_id = ip_group_id,

                    Selected = true,

                    ip_group_name = Area,

                    created_by = 0,
                    created_date = DateTime.Now,
                    approval_id = 3,
                    push_status_id = 2,
                    brgy_assembly_ip_id = Guid.NewGuid(),

                };
                db.brgy_assembly_ip.Add(item);
                db.SaveChanges();


            }






            return true;
        }



        [Route("api/offline/v1/barangay_assembly/represented_ip/get")]
        public List<brgy_assembly_ip> brgy_assembly_ip_represnted(Guid id)
        {


            //var model = db..FirstOrDefault(x => x.sub_project_id == id);

            var area = db.lib_ip_group.ToList();



            var list = new List<brgy_assembly_ip>();

            foreach (var item in area)
            {
                var coverage = db.brgy_assembly_ip.FirstOrDefault(x => x.brgy_assembly_id == id && x.ip_group_id == item.ip_group_id);

                if (coverage == null)
                {
                    var x = new brgy_assembly_ip
                    {
                        ip_group_name = item.name,
                        ip_group_id = item.ip_group_id,
                        brgy_assembly_id = id,
                        Selected = false,

                    };
                    list.Add(x);

                }

                else
                {
                    list.Add(coverage);
                }
            }


            return list;
        }


        #endregion

    }
}