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
                    training_category_id = 42;
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
                model.push_date = null;


                if (api != true)
                {
                    model.push_status_id = 3;
                }

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

                //var exists = db.brgy_assembly.FirstOrDefault(x =>
                //x.brgy_assembly_id == model.brgy_assembly_id
                //    );

                //if (exists != null)
                //{
                //    //  if (api != true)
                //    ////  {
                //    return
                //        BadRequest(
                //            new
                //            {
                //                message = "Record Already Exist for this Activity, Edit this instead?",
                //                id = exists.brgy_assembly_id
                //            });
                //    // }
                //}


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;

                }


                model.created_by = 0;
                model.created_date = DateTime.Now;
                model.approval_id = 3;
                model.is_deleted = false;

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

                
                var items_preselected = db.brgy_assembly.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.brgy_assembly.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).ToList();
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
                    }
                }
                else {
                    var items = db.brgy_assembly.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();
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

                var items = db.brgy_assembly_ip.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).ToList();
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

                             highlights = @s.highlights,



                             s.no_families,
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
                             s.total_families_in_barangay,

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


        [HttpPost]
        [Route("/api/export/barangay_assembly/dqa/count")]
        public IActionResult export_dqa_count(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = model.GroupBy(x => new
            {
                x.lib_brgy,
                x.lib_region,
                x.lib_province,
                x.lib_city,

                x.lib_cycle,
                x.lib_fund_source,
                x.lib_enrollment,

                x.brgy_code,
                x.enrollment_id,
                x.cycle_id

            }).Select(x => new
            {


                fund_source_name = x.Key.lib_fund_source.name,
                cycle_name = x.Key.lib_cycle.name,
                kc_mode = x.Key.lib_enrollment.name,
                region_name = x.Key.lib_region.region_name,

                prov_name = x.Key.lib_province.prov_name,

                city_name = x.Key.lib_city.city_name,
                brgy_name = x.Key.lib_brgy.brgy_name,

                target_ba = x.Key.lib_enrollment.enrollment_id == 1 ? 3 : 5,
                total_conducted = x.Count(),

                no_of_first_ba = x.Count(c => c.barangay_assembly_purpose_id == 1),

                no_of_second_ba = x.Count(c => c.barangay_assembly_purpose_id == 2),


                no_of_third_ba = x.Count(c => c.barangay_assembly_purpose_id == 3),


                no_of_fourth_ba = x.Count(c => c.barangay_assembly_purpose_id == 4),

                no_of_fifth_ba = x.Count(c => c.barangay_assembly_purpose_id == 5),

            }).ToList();

            var export = result.GroupBy(x => new
            {
                x.region_name,
                x.prov_name,
                x.city_name,
                x.brgy_name,

                x.fund_source_name,
                x.cycle_name,
                x.kc_mode,

            }).Select(x => new
            {
                fund_source_name = x.Key.fund_source_name,
                cycle_name = x.Key.cycle_name,
                kc_mode = x.Key.kc_mode,
                region_name = x.Key.region_name,

                prov_name = x.Key.prov_name,

                city_name = x.Key.city_name,
                brgy_name = x.Key.brgy_name,

                target_ba = x.Key.kc_mode.Contains("ac") ? "3" : "5",

                total_conducted =
                x.Sum(c => c.no_of_first_ba)
                + x.Sum(c => c.no_of_second_ba)
                + x.Sum(c => c.no_of_third_ba)
                + x.Sum(c => c.no_of_fourth_ba)
                + x.Sum(c => c.no_of_fifth_ba),

                no_of_first_ba = x.Sum(c => c.no_of_first_ba),

                no_of_second_ba = x.Sum(c => c.no_of_second_ba),


                no_of_third_ba = x.Sum(c => c.no_of_third_ba),


                no_of_fourth_ba = x.Sum(c => c.no_of_fourth_ba),

                no_of_fifth_ba = x.Sum(c => c.no_of_fifth_ba),

            });

            return Ok(export);
        }

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