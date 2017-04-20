using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeskApp.Data;
using DeskApp.DataLayer;


using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace DeskApp.Controllers
{

    public class BarangayProfileAPIController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;


        public BarangayProfileAPIController(ApplicationDbContext context)
        {
            db = context;

        }
   

        private IQueryable<brgy_profile> GetData(

                 AngularFilterModel item
             )
        {
            var model = db.brgy_profile.Where(x => x.is_deleted != true).AsQueryable();

            //if (item.record_id != null)
            //{

            //    model = model.Where(x => x.brgy_profile_id == item.record_id);

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
            if (item.brgy_code != null)
            {
                model = model.Where(m => m.brgy_code == item.brgy_code);
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


            return model;
        }






        [HttpPost]
        [Route("api/offline/v1/barangay_profile/get_dto")]
        public PagedCollection<brgy_profileDTO> get_dto(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;

            return new PagedCollection<brgy_profileDTO>()
                       {
                           TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),

                           TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                           Page = currPage,
                           TotalCount = totalCount,
                           TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                           Items = model.OrderBy(x => x.brgy_profile_id).Skip(currPage * pageSize).Select(brgy_profileDTO.SELECT).Take(pageSize).ToList()
                       };
        }

        [HttpPost]
        [Route("api/offline/v1/barangay_profile/get_recently_edited")]
        public PagedCollection<brgy_profileDTO> get_recently_edited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;

            return new PagedCollection<brgy_profileDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPage,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                Items = model
                        .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                        .OrderBy(x => x.brgy_profile_id).Skip(currPage * pageSize).Select(brgy_profileDTO.SELECT).Take(pageSize).ToList()
            };
        }

        [HttpPost]
        [Route("api/offline/v1/barangay_profile/get_recently_added")]
        public PagedCollection<brgy_profileDTO> get_recently_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;

            return new PagedCollection<brgy_profileDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPage,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                Items = model
                        .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                        .OrderBy(x => x.brgy_profile_id).Skip(currPage * pageSize).Select(brgy_profileDTO.SELECT).Take(pageSize).ToList()
            };
        }

        [HttpPost]
        [Route("api/offline/v1/barangay_profile/get_recently_edited_and_added")]
        public PagedCollection<brgy_profileDTO> get_recently_edited_and_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;

            return new PagedCollection<brgy_profileDTO>()
            {
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Page = currPage,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                Items = model
                        .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                        .OrderBy(x => x.brgy_profile_id).Skip(currPage * pageSize).Select(brgy_profileDTO.SELECT).Take(pageSize).ToList()
            };
        }






        [Route("api/offline/v1/barangay_profile/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.brgy_profile

                .FirstOrDefault(x => x.brgy_profile_id == id);



            if (model == null)
            {
                var item = new brgy_profile();

                item.brgy_profile_id = id;
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
            //    return Ok(Mapper.DynamicMap<brgy_profile, brgy_profile_mapping>(model));
        }



        [Route("api/offline/v1/barangay_profile/save")]
        public async Task<IActionResult> Save(brgy_profile model, bool? api)
        {


          

             var record = db.brgy_profile.AsNoTracking().FirstOrDefault(x => x.brgy_profile_id == model.brgy_profile_id);


            //var record = db.brgy_profile.AsNoTracking().FirstOrDefault(x => x.brgy_code == model.brgy_code
            //&& x.cycle_id == model.cycle_id && x.is_deleted != true);
        
           


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

                db.brgy_profile.Add(model);


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

                //SET KEY
                model.brgy_profile_id = record.brgy_profile_id;


                //set old data
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


        [Route("api/offline/v1/barangay_profile/sync_push")]
        public async Task<IActionResult> Push(brgy_profile model, bool? api)
        {
            var record = db.brgy_profile.AsNoTracking().FirstOrDefault(x => x.brgy_profile_id == model.brgy_profile_id);
            
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

                db.brgy_profile.Add(model);

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

                model.brgy_profile_id = record.brgy_profile_id;                
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


        [Route("api/offline/v1/barangay_profile/eca/get")]
        public IActionResult GetEca(int id)
        {

            var model = db.brgy_eca

                .Where(x => x.brgy_code == id && x.is_deleted != true)
                .Select(
                    x =>
                        new
                        {
                            x.brgy_eca_id,
                            lib_eca_type_name = db.lib_eca_type.FirstOrDefault(c => c.eca_type_id == x.eca_type_id).name,
                            x.location,
                            x.description
                        });






            return Ok(model);
        }




        [Route("api/offline/v1/barangay_profile/eca/save")]
        public async Task<IActionResult> SaveEca(brgy_eca model, bool? api)
        {
            model.created_by = 0;
            model.created_date = DateTime.Now;
            model.approval_id = 3;
            model.is_deleted = false;
            model.push_status_id = 2;
            model.brgy_eca_id = Guid.NewGuid();


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var record = db.brgy_eca.AsNoTracking().FirstOrDefault(x => x.brgy_eca_id == model.brgy_eca_id);

            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;

                }

                model.brgy_eca_id = Guid.NewGuid();


                db.brgy_eca.Add(model);


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



        [Route("api/offline/v1/barangay_profile/eca/delete")]
        public async Task<IActionResult> DeleteEca(Guid id)
        {




            var record = db.brgy_eca.FirstOrDefault(x => x.brgy_eca_id == id);

            if (record == null)
            {
                return BadRequest();
            }
            else
            {

                record.is_deleted = true;
                record.deleted_by = 1;
                record.deleted_date = DateTime.Now;



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
        [Route("Sync/Get/barangay_profile")]
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/barangay_profile/get_mapped?city_code=" + city_code + "&record_id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var all = JsonConvert.DeserializeObject<List<brgy_profile>>(responseJson.Result);

                    //    var all = Mapper.DynamicMap<List<brgy_profile_mapping>, List<brgy_profile>>(model);

                    foreach (var item in all.ToList())
                    {
                        await Save(item, true);
                    }

                    GetOnlineEca(username, password, city_code, record_id);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }


        [Route("api/offline/v1/barangay_profile/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid brgy_profile_id, int push_status_id)
        {
            var brgy_prof = db.brgy_profile.Find(brgy_profile_id);

            if (brgy_prof == null)
            {
                return BadRequest("Error");
            }

            brgy_prof.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }



        [HttpPost]
        [Route("Sync/Post/barangay_profile")]
        public async Task<ActionResult> PostOnline(string username, string password, Guid? record_id = null)
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
                
                var items_preselected = db.brgy_profile.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();
                
                if (!items_preselected.Any())
                { //check if the list (items_preselected) is empty which means no items were selected
                    var items = db.brgy_profile.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

                    if (record_id != null)
                    {
                        items = items.Where(x => x.brgy_profile_id == record_id);
                    }
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_profile/sync_push", data).Result;

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
                    var items = db.brgy_profile.Where(x => x.push_status_id == 5 && x.is_deleted != true);

                    if (record_id != null)
                    {
                        items = items.Where(x => x.brgy_profile_id == record_id);
                    }

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_profile/sync_push", data).Result;

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
            await PostECA(username, password, record_id);
            return Ok();
        }



        public async Task<bool> PostECA(string username, string password, Guid? record_id = null)
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
                
                var items = db.brgy_eca.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));
                
                foreach (var item in items.ToList())
                {

                  //  var push = Mapper.DynamicMap<brgy_eca, brgy_eca_mapping>(item);

                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_profile/eca/save", data).Result;

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

        public async Task<bool> GetOnlineEca(string username, string password, string city_code = null, Guid? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/barangay_profile/eca/get_mapped?city_code=" + city_code + "&record_id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var all = JsonConvert.DeserializeObject<List<brgy_eca>>(responseJson.Result);

                    //    var all = Mapper.DynamicMap<List<brgy_profile_mapping>, List<brgy_profile>>(model);

                    foreach (var item in all.ToList())
                    {
                        await SaveEca(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }



        private bool record_exists(Guid id)
        {
            return db.brgy_profile.Count(e => e.brgy_profile_id == id) > 0;
        }




    }

}
