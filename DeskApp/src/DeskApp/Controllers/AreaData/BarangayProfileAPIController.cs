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
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

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


        //For v3.0: to check if items on the list has attachment
        //[HttpPost]
        //[Route("api/offline/v1/barangay_profile/check_if_item_has_attachment")]
        //public PagedCollection<brgy_profileDTO> check_if_item_has_attachment(AngularFilterModel item)
        //{
        //    bool? item_has_attachment;
        //    var items = db.brgy_profile.Where(x => x.is_deleted != true);

        //    var result = from i in items
        //                 join a in db.attached_document.Where(x => x.is_deleted != true) on i.brgy_profile_id equals a.record_id
        //                 select a;            

        //    foreach (var aa in result.ToList())
        //    {
        //        item_has_attachment = true;
        //    }

        //}


        #region Brgy Profile Downloads

        [Route("api/export/barangay_profile/list")]
        public IActionResult export_list(AngularFilterModel item)
        {
            var model = GetData(item);

            var result = from s in model
                         select new
                         {
                             s.brgy_profile_id,
                                                             
                             s.lib_region.region_name,
                             s.lib_province.prov_name,
                             s.lib_city.city_name,
                             s.lib_brgy.brgy_name,
                             no_of_sitio = s.no_sitios,
                             inclusive_years_of_kc = s.year_source,
                             fund_source = s.lib_fund_source.name,
                             cycle = s.lib_cycle.name,

                             total_no_of_household = s.no_households,
                             total_no_of_families = s.no_families, 
                             no_of_male = s.no_male,
                             no_of_female = s.no_female,
                             no_of_male_children_ages_0_to_5 = s.no_male0_5,
                             no_of_female_children_ages_0_to_5 = s.no_female0_5,
                             no_of_male_ages_6_to_12 = s.no_male6_12,
                             no_of_female_ages_6_to_12 = s.no_female6_12,
                             no_of_male_ages_13_to_17 = s.no_male13_16,
                             no_of_female_ages_13_to_17 = s.no_female13_16,
                             total_male_voting_population = s.no_voting_male,   
                             total_female_voting_population = s.no_voting_female,
                             total_male_labor_force = s.no_labor_male,
                             total_female_labor_force = s.no_labor_female,

                             is_poblacion = s.is_poblacion == true ? "Yes" : "No",
                             hrs_travel_time_to_poblacion_if_not_poblacion = s.hrs_totown,
                             mins_travel_time_to_poblacion_if_not_poblacion = s.mins_totown,
                             no_of_km_if_not_poblacion = s.km_frmtown,

                             is_upland = s.is_upland == true ? "Yes" : "No",
                             is_hilly = s.is_hilly == true ? "Yes" : "No",
                             is_lowland = s.is_lowland == true ? "Yes" : "No",
                             is_island = s.is_island == true ? "Yes" : "No",
                             is_coastal = s.is_coastal == true ? "Yes" : "No",
                             is_brgy_isolated = s.is_isolated == true ? "Yes" : "No",

                             is_brgy_affected_by_armed_conflict = s.is_armedconflict == true ? "Yes" : "No",
                             is_brgy_affected_by_political_and_ejk = s.is_poldispute == true ? "Yes" : "No",
                             is_brgy_affected_by_family_and_genderbased_violence = s.is_genderviolence == true ? "Yes" : "No",
                             is_brgy_affected_by_rido_war = s.is_rido_war == true ? "Yes" : "No",
                             is_brgy_affected_by_crime = s.is_crime == true ? "Yes" : "No",
                             armed_conflict_area_details = s.baragay_additiondetails,

                             no_of_pantawid_hh = s.no_pantawid_household,
                             no_of_pantawid_family = s.no_pantawid_family,
                             no_of_slp_hh = s.no_slp_household,
                             no_of_slp_family = s.no_slp_family,

                             s.alloc_env,
                             s.alloc_econ,
                             s.alloc_infra,
                             s.alloc_basic,
                             s.alloc_inst,
                             s.alloc_gender,
                             s.alloc_drrm,
                             s.alloc_others,
                             allocation_total = s.alloc_env + s.alloc_econ + s.alloc_infra + s.alloc_basic + s.alloc_inst + s.alloc_gender + s.alloc_drrm + s.alloc_others,

                             has_bank = s.has_bank,
                             has_barangayhall = s.has_barangay_hall,
                             has_cap_agri = s.has_cap_agri,
                             has_cap_org_dev = s.has_cap_org_dev,
                             has_cap_others = s.has_cap_others,
                             has_cementery = s.has_cementery,
                             has_college = s.has_college,
                             has_credit = s.has_credit,
                             has_daycare = s.has_daycare,
                             has_drainage = s.has_drainage,
                             has_electricity = s.has_electricity,
                             has_elementary = s.has_elementary,
                             has_emergency_service = s.has_emergency_service,
                             has_evac_center = s.has_evac_center,
                             has_harvest = s.has_harvest,
                             has_health = s.has_health,
                             has_hospital = s.has_hospital,
                             has_housing = s.has_housing,
                             has_irrigation = s.has_irrigation,
                             has_miniport = s.has_miniport,
                             has_multipurpose = s.has_multipurpose,
                             has_secondary = s.has_secondary,
                             has_stores = s.has_stores,
                             has_tanod = s.has_tanod,
                             has_telecom = s.has_telecom,
                             has_tribal = s.has_tribal,
                             has_waste = s.has_waste,
                             has_water_supply_system = s.has_water_supply_system,
                             nearest_bank = s.nearest_bank,
                             nearest_barangay_hall = s.nearest_barangay_hall,
                             nearest_cap_agri = s.nearest_cap_agri,
                             nearest_cap_health = s.nearest_cap_health,
                             nearest_cap_org_dev = s.nearest_cap_org_dev,
                             nearest_cap_others = s.nearest_cap_others,
                             nearest_cementery = s.nearest_cementery,

                             s.access_details,
                             s.access_addressed,
                             s.access_remarks,
                             s.water_details,
                             s.water_address,
                             s.water_remarks,
                             s.health_details,
                             s.health_address,
                             s.health_remarks,
                             s.literacy_details,
                             s.literacy_addressed,
                             s.literacy_remarks,
                             s.employment_details,
                             s.employment_addressed,
                             s.employment_remarks,
                             s.landownership_details,
                             s.landownership_addressed,
                             s.landownership_remarks,
                             s.agriculture_details,
                             s.agriculture_addressed,
                             s.agriculture_remarks,
                             s.peace_details,
                             s.peace_addressed,
                             s.peace_remarks,
                             s.environment_details,
                             s.environment_addressed,
                             s.environment_remarks,
                             s.powersupply_details,
                             s.powersupply_addressed,
                             s.powersupply_remarks,
                             s.communication_details,
                             s.communication_addressed,
                             s.communication_remarks,
                             s.others_details,
                             s.others_addressed,
                             s.others_remarks,

                             s.health_number_0_5_value,
                             s.health_number_0_5_reference,
                             s.children_0_5_value,
                             s.children_0_5_reference,
                             s.pregnant_died_value,
                             s.pregnant_died_reference,
                             s.pregnant_total_value,
                             s.pregnant_total_reference,
                             s.malnourished_0_5value,
                             s.malnourished_0_5reference,
                             s.total_malnourished_0_5value,
                             s.safewater_value,
                             s.safewater_reference,
                             s.sanity_value,
                             s.sanity_reference,
                             s.totalsanity_value,
                             s.totalsanity_reference,
                             s.squatting_value,
                             s.squatting_reference,
                             s.totalsquatting_value,
                             s.totalsquatting_reference,
                             s.makeshift_value,
                             s.makeshift_reference,
                             s.totalmakeshift_value,
                             s.totalmakeshift_reference,
                             s.victimized_value,
                             s.victimized_reference,
                             s.totalvictimized_value,
                             s.totalvictimized_reference,
                             s.threshold_value,
                             s.threshold_reference,
                             s.totalthreshold_value,
                             s.totalthreshold_reference,
                             s.incomeless_value,
                             s.incomeless_reference,
                             s.totalincomeless_value,
                             s.totalincomeless_reference,
                             s.lessthan_3_meals_value,
                             s.lessthan_3_meals_reference,
                             s.totallessthan_3_meals_value,
                             s.children_6_12_elem_value,
                             s.children_6_12_elem_reference,
                             s.totalchildren_6_12_elem_value,
                             s.children_13_16_secondary_value,
                             s.laborforce_value,
                             s.laborforce_reference,
                             s.totallaborforce_value,
                             s.totallaborforce_reference,
                             s.totalsafewater_value,
                             s.totalsafewater_reference,
                             s.tot_malnourished_0_5_ref,
                             s.tot_lessthan_3_meals_ref,
                             s.totchild_6_12_elem_ref,
                             s.child_13_16_secondary_ref,
                             s.totchild_13_16_secondary_ref,
                             s.totchild_13_16_secondary_val,


                         };


            return Ok(result);
        }

        #endregion








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

                    await GetOnlineEca(username, password, city_code, record_id);

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
                
                var items_preselected = db.brgy_profile.Where(x => x.push_status_id == 5).ToList();
                
                if (!items_preselected.Any())
                { //check if the list (items_preselected) is empty which means no items were selected
                    var items = db.brgy_profile.Where(x => x.push_status_id == 2 || x.push_status_id == 3 || (x.push_status_id == 3 && x.is_deleted == true));

                    if (record_id != null)
                    {
                        items = items.Where(x => x.brgy_profile_id == record_id);
                    }
                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_profile/save", data).Result;

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
                    var items = db.brgy_profile.Where(x => x.push_status_id == 5 || (x.push_status_id == 3 && x.is_deleted == true));

                    if (record_id != null)
                    {
                        items = items.Where(x => x.brgy_profile_id == record_id);
                    }

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/barangay_profile/save", data).Result;

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
                
                var items = db.brgy_eca.Where(x => x.push_status_id != 1 || x.push_status_id == 3 || (x.push_status_id == 3 && x.is_deleted == true));
                
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
