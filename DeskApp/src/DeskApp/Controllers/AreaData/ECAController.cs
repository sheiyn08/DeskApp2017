//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using DeskApp.DataLayer;
//using DeskApp.Data;
//
//using Microsoft.EntityFrameworkCore; 
//using System.Text;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using Newtonsoft.Json;

//namespace DeskApp.Controllers
//{
//    public class ECAController : Controller
//    {
//        public static string url = @"http://ncddpdb.dswd.gov.ph";

//        private readonly ApplicationDbContext db;


//        public ECAController(ApplicationDbContext context)
//        {
//            db = context;

//        }


//        private IQueryable<brgy_eca> GetData(
//                 AngularFilterModel item
//           )
//        {
//            var model = db.brgy_eca.Where(x => x.is_deleted != true).AsQueryable();





//            #region query

//            if (item.record_id != null)
//            {

//                model = model.Where(x => x.brgy_eca_id == item.record_id);

//            }

//            if (item.eca_purpose_id != null)
//            {

//                model = model.Where(x => x.eca_purpose_id == item.eca_purpose_id);

//            }

//            if (item.region_code != null)
//            {
//                model = model.Where(m => m.region_code == item.region_code);
//            }
//            if (item.prov_code != null)
//            {
//                model = model.Where(m => m.prov_code == item.prov_code);
//            }
//            if (item.city_code != null)
//            {
//                model = model.Where(m => m.city_code == item.city_code);
//            }
//            if (item.brgy_code != null)
//            {
//                model = model.Where(m => m.brgy_code == item.brgy_code);
//            }
//            if (item.enrollment_id != null)
//            {
//                model = model.Where(x => x.enrollment_id == item.enrollment_id);
//            }
//            if (item.push_status_id != null)
//            {
//                model = model.Where(m => m.push_status_id == item.push_status_id);
//            }
//            if (item.approval_id != null)
//            {
//                model = model.Where(m => m.approval_id == item.approval_id);
//            }




//            if (item.fund_source_id != null)
//            {
//                model = model.Where(m => m.fund_source_id == item.fund_source_id);
//            }
//            if (item.cycle_id != null)
//            {
//                model = model.Where(m => m.cycle_id == item.cycle_id);
//            }


//            #endregion

//            return model;
//        }








//        [HttpPost]
//        [Route("api/offline/v1/eca/get_dto")]
//        public PagedCollection<dynamic> GetAll(AngularFilterModel item
//                )
//        {

//            var model = GetData(item);


//            var totalCount = model.Count();

//            int currPages = item.currPage ?? 0;
//            int size = item.pageSize ?? 10;



//            return new PagedCollection<dynamic>()
//            {
//                Page = currPages,
//                TotalCount = totalCount,
//                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),

//                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),

//                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
//                Items = model

                            
//                           //.Include(x => x.lib_province)
//                           //.Include(x => x.lib_city)
//                           //.Include(x => x.lib_brgy)
//                           .Include(x => x.lib_eca_type)
//                           .OrderBy(x => x.brgy_eca_id)
//                           .Select(x => new  
//                           {
//                               //lib_approval_name = x.lib_approval.name,
//                               lib_brgy_brgy_name = db.lib_brgy.First(c => c.brgy_code == x.brgy_code).brgy_name,
//                               lib_city_city_name = db.lib_city.First(c => c.city_code == x.city_code).city_name,
                            
//                               lib_province_prov_name = db.lib_province.First(c => c.prov_code == x.prov_code).prov_name,
//                               lib_region_region_name = db.lib_region.First(c => c.region_code == x.region_code).region_nick,

//                               lib_eca_type_name = x.lib_eca_type.name,
//                                x.brgy_eca_id
//                           })
//                           .Skip(currPages * size).Take(size).ToList(),


//            };

//        }




//        [Route("api/offline/v1/eca/get")]
//        public IActionResult Get(Guid? id = null)
//        {

//            var model = db.brgy_eca

//                .FirstOrDefault(x => x.brgy_eca_id == id && x.is_deleted != true);



//            if (model == null)
//            {
//                var item = new brgy_eca();

//                item.brgy_eca_id = Guid.NewGuid();
//                item.push_status_id = 3;
//                item.created_by = 0;


//                item.created_date = DateTime.Now;
//                item.approval_id = 3;
//                item.is_deleted = false;


//                model = item;
//            }
//            else
//            {

//                model.push_status_id = 3;
//                model.last_modified_by = 0;
//                model.last_modified_date = DateTime.Now;
//                model.approval_id = 3;


//            }

//            return Ok(Mapper.DynamicMap<brgy_eca, brgy_eca_mapping>(model));
//        }




//        [Route("api/offline/v1/eca/save")]
//        public async Task<IActionResult> Save(brgy_eca model, bool? api)
//        {


//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }

//            var record = db.brgy_eca.AsNoTracking().FirstOrDefault(x => x.brgy_eca_id == model.brgy_eca_id);

//            if (record == null)
//            {


//                if (api != true)
//                {
//                    model.push_status_id = 2;
//                    model.push_date = null;

//                }


//                model.created_by = 0;
//                model.created_date = DateTime.Now;
//                model.approval_id = 3;
//                model.is_deleted = false;

//                db.brgy_eca.Add(model);


//                try
//                {
//                    await db.SaveChangesAsync();
//                    return Ok();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    return BadRequest();
//                }
//            }
//            else
//            {
//                model.push_date = null;


//                if (api != true)
//                {
//                    model.push_status_id = 3;
//                }



//                model.created_by = record.created_by;
//                model.created_date = record.created_date;


//                model.last_modified_by = 0;
//                model.last_modified_date = DateTime.Now;

//                db.Entry(model).State = EntityState.Modified;

//                try
//                {
//                    await db.SaveChangesAsync();
//                    return Ok();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    return BadRequest();
//                }
//            }
//        }


//        [HttpPost]
//        [Route("Sync/Get/eca")]
//        public async Task<ActionResult> GetOnline(string username, string password, string city_code = null, Guid? record_id = null)
//        {



//            string token = username + ":" + password;

//            byte[] toBytes = Encoding.ASCII.GetBytes(token);


//            string key = Convert.ToBase64String(toBytes);

//            using (var client = new HttpClient())
//            {
//                //setup client
//                client.BaseAddress = new Uri(url);
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

//                // var model = new auth_messages();

//                HttpResponseMessage response = client.GetAsync("api/offline/v1/eca/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

//                if (response.IsSuccessStatusCode)
//                {
//                    var responseJson = response.Content.ReadAsStringAsync();

//                    var model = JsonConvert.DeserializeObject<List<brgy_eca_mapping>>(responseJson.Result);

//                    var all = Mapper.DynamicMap<List<brgy_eca_mapping>, List<brgy_eca>>(model);

//                    foreach (var item in all.ToList())
//                    {
//                        await Save(item, true);
//                    }

//                    return Ok();
//                }
//                else
//                {
//                    return BadRequest();
//                }
//            }


//        }

//        [HttpPost]

//        [Route("Sync/Post/eca")]
//        public async Task<ActionResult> PostOnline(string username, string password, Guid record_id)
//        {

//            string token = username + ":" + password;

//            byte[] toBytes = Encoding.ASCII.GetBytes(token);


//            string key = Convert.ToBase64String(toBytes);


//            using (var client = new HttpClient())
//            {
//                //setup client
//                client.BaseAddress = new Uri(url);
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

//                // var model = new auth_messages();


//                var items = db.brgy_eca.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).ToList();

//                foreach (var item in items)
//                {

//                    var push = Mapper.DynamicMap<brgy_eca, brgy_eca_mapping>(item);

//                    StringContent data = new StringContent(JsonConvert.SerializeObject(push), Encoding.UTF8, "application/json");

//                    HttpResponseMessage response = client.PostAsync("api/offline/v1/eca/save", data).Result;

//                    // response.EnsureSuccessStatusCode();

//                    if (response.IsSuccessStatusCode)
//                    {

//                        item.push_status_id = 1;
//                        item.push_date = DateTime.Now;

//                        await db.SaveChangesAsync();

//                    }
//                }

//            }

//            return Ok();
//        }








//    }
//}