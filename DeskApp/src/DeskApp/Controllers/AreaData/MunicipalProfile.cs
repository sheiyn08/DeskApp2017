using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeskApp.Data;
using DeskApp.DataLayer;

using Microsoft.EntityFrameworkCore; 
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DeskApp.Controllers
{

    public class MunicipalProfileController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;


        public MunicipalProfileController(ApplicationDbContext context)
        {
            db = context;

        }


        private IQueryable<muni_profile> GetData(

                 AngularFilterModel item
             )
        {
            var model = db.muni_profile.Where(x => x.is_deleted != true).AsQueryable();

            //if (item.record_id != null)
            //{

            //    model = model.Where(x => x.muni_profile_id == item.record_id);

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


            return model;
        }






        [HttpPost]
        [Route("api/offline/v1/municipal_profile/get_dto")]
        public PagedCollection<muni_profileDTO> get_dto(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;
            
            return
                       new PagedCollection<muni_profileDTO>()
                       {
                           TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),
                           TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                           Page = currPage,
                           TotalCount = totalCount,
                           TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                           Items = model.OrderBy(x => x.muni_profile_id).Skip(currPage * pageSize).Select(muni_profileDTO.SELECT).Take(pageSize).ToList()
                       };
        }

        [HttpPost]
        [Route("api/offline/v1/municipal_profile/get_recently_edited")]
        public PagedCollection<muni_profileDTO> get_recently_edited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;

            return
                       new PagedCollection<muni_profileDTO>()
                       {
                           TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true)).Count(),
                           TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                           Page = currPage,
                           TotalCount = totalCount,
                           TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                           Items = model
                                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.is_deleted != true))
                                    .OrderBy(x => x.muni_profile_id).Skip(currPage * pageSize).Select(muni_profileDTO.SELECT).Take(pageSize).ToList()
                       };
        }

        [HttpPost]
        [Route("api/offline/v1/municipal_profile/get_recently_added")]
        public PagedCollection<muni_profileDTO> get_recently_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;

            return
                       new PagedCollection<muni_profileDTO>()
                       {
                           TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true)).Count(),
                           TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                           Page = currPage,
                           TotalCount = totalCount,
                           TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                           Items = model
                                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.is_deleted != true))
                                    .OrderBy(x => x.muni_profile_id).Skip(currPage * pageSize).Select(muni_profileDTO.SELECT).Take(pageSize).ToList()
                       };
        }

        [HttpPost]
        [Route("api/offline/v1/municipal_profile/get_recently_edited_and_added")]
        public PagedCollection<muni_profileDTO> get_recently_edited_and_added(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count();
            int currPage = item.currPage ?? 0;
            int pageSize = item.pageSize ?? 10;

            return
                       new PagedCollection<muni_profileDTO>()
                       {
                           TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true)).Count(),
                           TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                           Page = currPage,
                           TotalCount = totalCount,
                           TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize),
                           Items = model
                                    .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.is_deleted != true))
                                    .OrderBy(x => x.muni_profile_id).Skip(currPage * pageSize).Select(muni_profileDTO.SELECT).Take(pageSize).ToList()
                       };
        }



        [Route("api/offline/v1/municipal_profile/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.muni_profile 

                .FirstOrDefault(x => x.muni_profile_id == id && x.is_deleted != true);



            if (model == null)
            {
                var item = new muni_profile();

                item.muni_profile_id = id;

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


        [Route("api/offline/v1/municipal_profile/fund_use/get")]
        public IActionResult GetFundUse(Guid id)
        {

            var model = db.muni_profile_fund_use

                .Where(x => x.muni_profile_id == id && x.is_deleted != true);
             
            return Ok(model);
        }


        [Route("api/offline/v1/municipal_profile/fund_source/get")]
        public IActionResult GetFundSource(Guid id)
        {

            var model = db.muni_profile_income

                .Where(x => x.muni_profile_income_id == id && x.is_deleted != true);

            return Ok(model);
        }

        [Route("api/offline/v1/municipal_profile/save")]
        public async Task<IActionResult> Save(muni_profile model, bool? api)
        {

 

            var record = db.muni_profile.AsNoTracking().FirstOrDefault(x => x.muni_profile_id == model.muni_profile_id);

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

                db.muni_profile.Add(model);


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
        [Route("Sync/Get/municipal_profile")]
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/municipal_profile/get_mapped?city_code=" + city_code + "&record_id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<muni_profile>>(responseJson.Result);

               //     var all = Mapper.DynamicMap<List<muni_profile_mapping>, List<muni_profile>>(model);

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



        [Route("api/offline/v1/municipal_profile/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid muni_profile_id, int push_status_id)
        {
            var muni_profile = db.muni_profile.Find(muni_profile_id);

            if (muni_profile == null)
            {
                return BadRequest("Error");
            }

            muni_profile.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Sync/Post/municipal_profile")]
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

                // ----------------------------- NEW CODE: ------------------------------------ //

                var items_preselected = db.muni_profile.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();

                if (!items_preselected.Any())
                { 
                    var items = db.muni_profile.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

                    if (record_id != null)
                    {
                        items = items.Where(x => x.muni_profile_id == record_id);
                    }

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/municipal_profile/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    var items = db.muni_profile.Where(x => x.push_status_id == 5 && x.is_deleted != true);
                    if (record_id != null)
                    {
                        items = items.Where(x => x.muni_profile_id == record_id);
                    }

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/municipal_profile/save", data).Result;

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







        private bool record_exists(Guid id)
        {
            return db.muni_profile.Count(e => e.muni_profile_id == id) > 0;
        }




    }

}
