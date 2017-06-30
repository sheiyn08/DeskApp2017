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
using DeskApp.Controllers.Repository;

namespace DeskApp.Controllers.AreaData
{
    public class mptaController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;

        private readonly IPSARepository repository;


        public mptaController(ApplicationDbContext context, IPSARepository _psa)
        {
            db = context;
            repository = _psa;
        }



        private IQueryable<municipal_pta> GetData(
               AngularFilterModel item
         )
        {
            var model = db.municipal_pta.Where(x => x.is_deleted != true).AsQueryable();





            #region query

            if (item.record_id != null)
            {

                model = model.Where(x => x.municipal_pta_id == item.record_id);

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


        [Route("api/export/municipal_pta/list")]
        public IActionResult export_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var export = from s in model
                         select new
                         {
                             s.municipal_pta_id,
                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             fund_source = s.lib_fund_source.name,
                             cycle = s.lib_cycle.name,
                             kc_mode = s.lib_enrollment.name,

                             s.no_lgu_cso_meeting,
                             s.no_gad_plan_assessment,
                             s.no_ngo_pim,
                       
                             s.old_id,
                             s.moa_signed_date,
                             s.pta_resolution_no,
                             s.pta_approval_date,
                             s.nga_resolution_no,
                             s.nga_approval_date,
                             s.miacmct_resolution_no,
                             s.miacmct_approval_date,
                             s.ngopo_resolution_no,
                             s.ngopo_approval_date,
                             s.gad_resolution_no,
                             s.gad_approval_date,
                             s.lcc_resolution_no,
                             s.lcc_approval_date,
                             s.trust_account_no,
                             s.trust_opened_date,
                             s.kc_office,
                             s.kc_equipment,
                             s.no_staff,
                             s.no_tas,
                             s.incexp_location_post,
                             s.incexp_post_date,
                             s.budget_location_post,
                             s.budget_post_date,
                             s.plan_location_post,
                             s.plan_post_date,
                             s.no_ngopo_accredited,
                             s.no_ngopo_represented,
                             s.ngo_total,
                             s.no_4p_male,
                             s.no_4p_female,
                             s.no_ip_male,
                             s.no_ip_female,
                             s.no_women_male,
                             s.no_women_female,
                             s.no_youth_male,
                             s.no_youth_female,
                             s.no_elderly_male,
                             s.no_elderly_female,
                             s.no_pwd_male,
                             s.no_pwd_female,
                             s.miac_eo_no,
                             s.miac_eo_date,
                             s.mct_eo_no,
                             s.mct_eo_date,
                             s.focal_person,
                             s.encoder,
                             s.office_address,
                             s.longitude,
                             s.latitude,
                             s.mlgu_logistics,
                             s.mdc_resolution_no,
                             s.mdc_date,
                             s.no_cdd_male,
                             s.no_cdd_female,
                             s.no_ngopo_male,
                             s.no_ngopo_female,
                             s.no_lgu_male,
                             s.no_lgu_female,
                             s.no_ngopo_represented_male,
                             s.no_ngopo_represented_female,
                             s.miac_resolution_no,
                             s.miac_approval_date,
                             s.mct_resolution_no,
                             s.mct_approval_date,
                             s.mdcmem_resolution_no,
                             s.mdcmem_approval_date,
                             s.mdcmem_male_no,
                             s.mdcmem_female_no,
                             s.with_equipment,
                             s.lsb_represented_male,
                             s.lsb_represented_female,
                             s.ngopo_accredited,


                         };

            return Ok(export);

        }




        [HttpPost]
        [Route("api/offline/v1/mpta/get_dto")]
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
                    .OrderBy(x => x.municipal_pta_id)
                    .Select(x => new
                               {
                                lib_city_city_name = x.lib_city.city_name,
                                lib_cycle_name = x.lib_cycle.name,
                                lib_fund_source_name = x.lib_fund_source.name,
                                lib_province_prov_name = x.lib_province.prov_name,
                                lib_region_region_name = x.lib_region.region_nick,
                                lib_enrollment_name = x.lib_enrollment.name,
                                municipal_pta_id = x.municipal_pta_id,
                                push_date = x.push_date,
                                push_status_id = x.push_status_id,
                                last_modified_date = x.last_modified_date
                              })
                   .Skip(currPages * size).Take(size).ToList(),
            };
        }


        [HttpPost]
        [Route("api/offline/v1/mpta/get_recently_edited")]
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
                    .OrderBy(x => x.municipal_pta_id)
                    .Select(x => new
                    {
                        lib_city_city_name = x.lib_city.city_name,
                        lib_cycle_name = x.lib_cycle.name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_nick,
                        lib_enrollment_name = x.lib_enrollment.name,
                        municipal_pta_id = x.municipal_pta_id,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                        last_modified_date = x.last_modified_date
                    })
                   .Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/mpta/get_recently_added")]
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
                    .OrderBy(x => x.municipal_pta_id)
                    .Select(x => new
                    {
                        lib_city_city_name = x.lib_city.city_name,
                        lib_cycle_name = x.lib_cycle.name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_nick,
                        lib_enrollment_name = x.lib_enrollment.name,
                        municipal_pta_id = x.municipal_pta_id,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                        last_modified_date = x.last_modified_date
                    })
                   .Skip(currPages * size).Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/mpta/get_recently_edited_and_added")]
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
                    .OrderBy(x => x.municipal_pta_id)
                    .Select(x => new
                    {
                        lib_city_city_name = x.lib_city.city_name,
                        lib_cycle_name = x.lib_cycle.name,
                        lib_fund_source_name = x.lib_fund_source.name,
                        lib_province_prov_name = x.lib_province.prov_name,
                        lib_region_region_name = x.lib_region.region_nick,
                        lib_enrollment_name = x.lib_enrollment.name,
                        municipal_pta_id = x.municipal_pta_id,
                        push_date = x.push_date,
                        push_status_id = x.push_status_id,
                        last_modified_date = x.last_modified_date
                    })
                   .Skip(currPages * size).Take(size).ToList(),
            };
        }


        [Route("api/offline/v1/mpta/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.municipal_pta

                .FirstOrDefault(x => x.municipal_pta_id == id);// && (x.is_deleted.HasValue && x.is_deleted == true));



            if (model == null)
            {
                var item = new municipal_pta();

            item.municipal_pta_id = id;
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





        // ------------ OLD save: used for version 2.0, 2.1
        //[Route("api/offline/v1/mpta/save")]
        //public async Task<IActionResult> Save(municipal_pta model, bool? is_ba, bool? api)
        //{


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }




        //    var record = db.municipal_pta.AsNoTracking().FirstOrDefault(x => x.city_code == model.city_code

        //    && x.fund_source_id == model.fund_source_id
        //    && x.cycle_id == model.cycle_id
        //    && x.enrollment_id == model.enrollment_id


        //    );


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

        //        db.municipal_pta.Add(model);


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

        //        model.municipal_pta_id = record.municipal_pta_id;



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


            //---------------- NEW: to be used for version 2.2
        [Route("api/offline/v1/mpta/save")]
        public async Task<IActionResult> Save(municipal_pta model, bool? is_ba, bool? api)
        {
            var record = db.municipal_pta.AsNoTracking().FirstOrDefault(x => x.city_code == model.city_code
            && x.fund_source_id == model.fund_source_id
            && x.cycle_id == model.cycle_id
            && x.enrollment_id == model.enrollment_id);


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

                db.municipal_pta.Add(model);


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
                
                model.municipal_pta_id = record.municipal_pta_id;
                
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
        [Route("Sync/Get/mpta")]
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/mpta/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<municipal_pta>>(responseJson.Result);


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


        [Route("api/offline/v1/municipal_pta/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid municipal_pta_id, int push_status_id)
        {
            var mpta = db.municipal_pta.Find(municipal_pta_id);

            if (mpta == null)
            {
                return BadRequest("Error");
            }

            mpta.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        //new:
        [HttpPost]
        [Route("Sync/Post/mpta")]
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

                var items_preselected = db.municipal_pta.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.municipal_pta.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).ToList();
                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/mpta/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            item.push_date = DateTime.Now;
                            await db.SaveChangesAsync();
                        }
                    }
                }
                else {
                    var items = db.municipal_pta.Where(x => x.push_status_id == 5 && x.is_deleted != true).ToList();
                    foreach (var item in items)
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/mpta/save", data).Result;
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

        //old:
        //[HttpPost]
        //[Route("Sync/Post/mpta")]
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


        //        var items = db.municipal_pta.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).ToList();

        //        foreach (var item in items)
        //        {


        //            StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

        //            HttpResponseMessage response = client.PostAsync("api/offline/v1/mpta/save", data).Result;

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
