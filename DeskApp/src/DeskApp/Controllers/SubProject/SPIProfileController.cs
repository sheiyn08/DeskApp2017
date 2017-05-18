using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using DeskApp.Data;
using DeskApp.DataLayer;

using DeskApp.DataLayer.DTO;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace DeskApp.Controllers
{

    public class SPIProfileController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";
        public static string geoUrl = @"http://geotagging.dswd.gov.ph";


        private readonly ApplicationDbContext db;


        public SPIProfileController(ApplicationDbContext context)
        {
            db = context;

        }

        [Route("api/export/sub_project/list")]

        public IActionResult export_list(AngularFilterModel model)
        {
            var list = GetData(model);

            var result = from s in list
                         select
                         new
                         {
                             geotagging_id = s.sub_project_id,
                             erf_sp_id = s.modality_id == 7 ? s.erfr_project_id : null,

                             //     fund_source = db.lib_fund_source.Find(s.modality_id).name, // s.modality.name,


                             //funding = db.erfr_sub_projects.FirstOrDefault(x => x.SPID == s.erfr_project_id) == null ? db.erfr_sub_projects.FirstOrDefault(x => x.SPID == s.erfr_project_id).fund_source : "",



                             cycle = s.lib_cycle.name,
                             s.lib_regions.region_name,
                             s.lib_provinces.prov_name,
                             s.lib_cities.city_name,
                             s.lib_brgy.brgy_name,
                             s.brgy_code,
                             s.lib_brgy.district,


                             //ncddp_grouping = s.modality_id == 7 ? s.lib_modality_category.name : "",
                             //batch_actual = s.modality_id == 7 ? (db.ncddp_grouping.FirstOrDefault(x => x.city_code == s.city_code) != null ? db.ncddp_grouping.FirstOrDefault(x => x.city_code == s.city_code).batch_actual : "") : "",
                             //batch_target = s.modality_id == 7 ? (db.ncddp_grouping.FirstOrDefault(x => x.city_code == s.city_code) != null ? db.ncddp_grouping.FirstOrDefault(x => x.city_code == s.city_code).batch_target : "") : "",

                             s.sub_project_name,
                             project_type = s.lib_project_type.name,
                             //    major_category = s.lib_project_type.lib_major_classification.name,

                             kalahi_amount = s.Kalahi_Amount,
                             lcc_amount = s.LCC_Amount,

                             with_local_counterpart = s.has_local_counterpart == true ? "Yes" : "No",

                             no_of_household_beneficiaries_target = s.NoOfHH,
                             no_of_household_beneficiaries_actual = s.NoOfHHActual,

                             Date_First_Download = s.Tranche1date,
                             s.Date_Started,
                             s.Date_of_Completion,
                             s.PlannedDate_Completion,

                             physical_accomplishment_percentage = s.Phy_Perc_To_Date,
                             physical_status = s.lib_physical_status.name,
                             status_or_whereabouts = s.modality_id == 7 ? s.lib_physical_status_category.name : null,

                             //       movement = s.movement_id != null ? s.lib_movement.name : "",
                             with_fund_release = s.Tranche1amount > 0 ? "Yes" : "No",



                             //primary

                             s.s_phy_construction_target,//   physical_target_construction = s.phy_has_construction_target == true ? s.phy_construction_actual : null,
                             s.s_phy_construction_actual,
                             s.lib_project_type.phy_construction_unit, // == true ? s.lib_project_type.phy_improvement_unit : null,


                             s.s_phy_improvement_target,//   physical_target_improvement = s.phy_has_improvement_target == true ? s.phy_improvement_actual : null,
                             s.s_phy_improvement_actual,
                             s.lib_project_type.phy_improvement_unit, // == true ? s.lib_project_type.phy_improvement_unit : null,



                             s.s_phy_rehabilitation_target,//    physical_target_rehabilitation = s.phy_has_rehabilitation_target == true ? s.phy_rehabilitation_actual : null,
                             s.s_phy_rehabilitation_actual,
                             s.lib_project_type.phy_rehabilitation_unit, // == true ? s.lib_project_type.phy_rehabilitation_unit : null,


                             s.s_phy_purchase_target,//   physical_target_purchase = s.phy_has_purchase_target == true ? s.phy_purchase_actual : null,
                             s.s_phy_purchase_actual,
                             s.lib_project_type.phy_purchase_unit, // == true ? s.lib_project_type.phy_rehabilitation_unit : null,





                             s.s_phy_construction_target_secondary,//   physical_target_construction_secondary = s.phy_has_construction_target_secondary == true ? s.phy_construction_actual_secondary : null,
                             s.s_phy_construction_actual_secondary,
                             s.lib_project_type.phy_construction_unit_secondary, // == true ? s.lib_project_type.phy_improvement_unit_secondary : null,


                             s.s_phy_improvement_target_secondary,//   physical_target_improvement_secondary = s.phy_has_improvement_target_secondary == true ? s.phy_improvement_actual_secondary : null,
                             s.s_phy_improvement_actual_secondary,
                             s.lib_project_type.phy_improvement_unit_secondary, // == true ? s.lib_project_type.phy_improvement_unit_secondary : null,



                             s.s_phy_rehabilitation_target_secondary,//    physical_target_rehabilitation_secondary = s.phy_has_rehabilitation_target_secondary == true ? s.phy_rehabilitation_actual_secondary : null,
                             s.s_phy_rehabilitation_actual_secondary,
                             s.lib_project_type.phy_rehabilitation_unit_secondary, // == true ? s.lib_project_type.phy_rehabilitation_unit_secondary : null,


                             s.s_phy_purchase_target_secondary,//   physical_target_purchase_secondary = s.phy_has_purchase_target_secondary == true ? s.phy_purchase_actual_secondary : null,
                             s.s_phy_purchase_actual_secondary,
                             s.lib_project_type.phy_purchase_unit_secondary, // == true ? s.lib_project_type.phy_rehabilitation_unit_secondary : null,






                             s.Tranche1amount,
                             s.Tranche2amount,
                             s.Tranche3amount,

                             s.Tranche1date,
                             s.Tranche2date,
                             s.Tranche3date,

                             with_ip_presence = s.has_ip_presence,// == true ? "Yes" : null,
                             with_esmp = s.has_sc_esmp,//== true ? "Yes" : null,
                             with_ecc = s.has_sc_ecc,// == true ? "Yes" : null,
                             with_cnc = s.has_sc_cnc,//== true ? "Yes" : null,
                             with_cp = s.has_sc_cp,// == true ? "Yes" : null,
                             with_cno = s.has_sc_cno,// == true ? "Yes" : null,

                             land_acq_with_deed_of_donation = s.land_aq_deed_of_donation,//== true ? "Yes" : null,
                             land_acq_with_usfruct = s.land_aq_unsfruct,//== true ? "Yes" : null,
                             land_acq_with_blgu_resolution = s.land_aq_blgu_resolution,// == true ? "Yes" : null,
                             land_acq_with_deped_certification = s.land_aq_deped_certification,//== true ? "Yes" : null,
                             land_acq_with_other_land_acquisition = s.land_aq_other == true ? s.other_land_acquisition : null,

                             land_ownership_dod = s.land_ownership_dod,// == true ? "Yes" : null,
                             land_ownership_government = s.land_ownership_gov,// == true ? "Yes" : null,
                             land_ownership_public_lands = s.land_ownership_public_lands,// == true ? "Yes" : null,
                             land_ownership_private_untitle = s.land_ownership_private_untitled,//== true ? "Yes" : null,
                             land_ownership_private_title = s.land_ownership_private_titled,// == true ? "Yes" : null,



                             s.has_after_photo,
                             s.has_before_photo,

                             s.has_scanned_spcr,
                             s.has_turnover_certificate,
                             s.has_marker,
                             s.has_set,
                             set_score = s.has_set_score,

                             s.has_onm_group,
                             s.has_closed_account,
                             //   has_variation_order = s.has_variation,


                             //     movement = s.movement_id != null ? s.lib_movement.name : null,

                             s.geotagged_co_approved_1,
                             s.geotagged_co_disapproved_5,
                             s.geotagged_fo_approved_2,
                             s.geotagged_fo_disapproved_4,
                             s.geotagged_srpmo_approved_3,
                             s.geotagged_srpmo_disapproved_6,
                             s.geotagged_act_uploaded_7,

                             s.Longitude,
                             s.Latitude,


                             s.created_by,
                             s.created_date,

                             s.last_updated_by,
                             s.last_updated_date,

                             if_bub = s.bub_unique_id,
                             s.Remarks
                         };

            return Ok(result);
        }

        private async Task<IActionResult> SaveERFR(erfr_sub_project model, bool? api)
        {


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            var record = db.erfr_sub_project.AsNoTracking().FirstOrDefault(x => x.SPID == model.SPID);

            if (record == null)
            {



                db.erfr_sub_project.Add(model);


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


                db.Entry(model).State = EntityState.Modified;

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
        }

        private async Task<IActionResult> SaveSPPhotos(SPPhoto model, bool? api)
        {


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            var record = db.SPPhoto.AsNoTracking().FirstOrDefault(x => x.UniqueName == model.UniqueName);

            if (record == null)
            {

                //using (WebClient client = new WebClient())
                //{
                //    client.DownloadFileAsync(new Uri(url), @"c:\temp\image35.png");
                //    client.DownloadFile(new Uri(url), @"c:\temp\image35.png");
                //}
                var path01 = PlatformServices.Default.Application.ApplicationBasePath;

                string savePath = path01 + @"\wwwroot\SPPhotos\" + model.UniqueName.ToString() + ".jpeg";

                string sub_project = db.sub_project.Find(model.sub_project_unique_id).region_code.ToString();

                if (sub_project.Length == 8) sub_project = "0" + sub_project;


                string url = @"http://geotagging.dswd.gov.ph/" + "SPPhotos/thumbnails/" + sub_project + "/" + model.UniqueName + ".jpg";
                try
                {
                    using (var client = new HttpClient())

                    using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                    using (
                        Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                        stream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
                    {
                        await contentStream.CopyToAsync(stream);
                    }
                }
                catch
                {

                }

                db.SPPhoto.Add(model);


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
                model.Id = record.Id;

                db.Entry(model).State = EntityState.Modified;

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
        }


        #region Multiple SET

        [Route("api/offline/v1/sub_project/set/get")]
        public IActionResult GetSET(Guid id)
        {
            var model = db.sub_project_set.Select(sub_project_setDTO.SELECT).Where(x => x.sub_project_unique_id == id && x.is_deleted != true);
            return Ok(model);            
        }


        [Route("api/offline/v1/spi/set/details/get")]
        public IActionResult GetSpiSETdetails(Guid id)
        {
            var model = db.sub_project_set.FirstOrDefault(x => x.sub_project_set_id == id && x.is_deleted != true);

            if (model == null)
            {
                var item = new sub_project_set();

                item.sub_project_set_id = id;
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


        [Route("api/offline/v1/sub_projects/set/save")]
        public async Task<IActionResult> SaveSET(sub_project_set model, bool? api)
        {
            var record = db.sub_project_set.AsNoTracking().FirstOrDefault(x => x.sub_project_set_id == model.sub_project_set_id);

            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }

                model.created_by = 0;
                model.created_date = DateTime.Now;
                model.is_deleted = false;
                db.sub_project_set.Add(model);
                
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
                    model.approval_id = 3;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;

                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

                db.Entry(model).State = EntityState.Modified;

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
        }

        [HttpPost]
        [Route("api/delete/sub_project/set")]
        public async Task<IActionResult> sub_project_set(Guid id)
        {
            var record = db.sub_project_set.FirstOrDefault(x => x.sub_project_set_id == id);
            record.is_deleted = true;
            record.push_status_id = 3;
            await db.SaveChangesAsync();
            return Ok();
        }

        #endregion Multiple SET


        #region ERS
        [Route("api/offline/v1/sub_project/ers/get")]
        public IActionResult GetERS(Guid id)
        {

            var model = db.sub_project_ers.Select(sub_project_ersDTO.SELECT)

                .Where(x => x.sub_project_unique_id == id && x.is_deleted != true);


            return Ok(model);
        }


        [Route("api/offline/v1/sub_projects/ers/save")]
        public async Task<IActionResult> SaveERS(sub_project_ers model, bool? api)
        {


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            var record = db.sub_project_ers.AsNoTracking().FirstOrDefault(x => x.sub_project_ers_id == model.sub_project_ers_id);

            if (record == null)
            {


                if (api != true)
                {
                    //   model.push_status_id = 2;
                    //  model.push_date = null;
                    model.approval_id = 3;
                }


                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;
                db.sub_project_ers.Add(model);


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
                // model.push_date = null;


                if (api != true)
                {
                    //   model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

                db.Entry(model).State = EntityState.Modified;

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
        }

        //------------------------------------------------------------------ ORIGINAL CODE FOR SAVING ERS WORKER --------------------------------------------//  
        //[Route("api/offline/v1/sub_projects/ers/worker/save")] 
        //public async Task<IActionResult> SaveERSWorker(person_ers_work model, bool? api)
        //{
        //    var record = db.person_ers_work.AsNoTracking().FirstOrDefault(x => x.person_profile_id == model.person_profile_id && x.sub_project_ers_id == model.sub_project_ers_id && x.is_deleted != true);

        //    if (record == null)
        //    {
        //        if (api != true)
        //        {
        //            //   model.push_status_id = 2;
        //            //  model.push_date = null;
        //            model.approval_id = 3;
        //        }
        //        model.created_by = 0;
        //        model.created_date = DateTime.Now;
        //        model.is_deleted = false;
        //        db.person_ers_work.Add(model);

        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            if (api == true)
        //            {
        //                return Ok();
        //            }

        //            var person =
        //                db.person_ers_work
        //                .Include(x => x.lib_ers_current_work)
        //                .Include(x => x.person_profile)
        //                .Where(x => x.person_ers_work_id == model.person_ers_work_id).Select(s => new
        //                {
        //                    s.person_profile.first_name,
        //                    s.person_profile.last_name,
        //                    s.person_profile.person_profile_id,
        //                    s.ers_current_work_id,
        //                    ers_current_work_name = s.lib_ers_current_work.name,
        //                    s.rate_day,
        //                    s.rate_hour,
        //                    s.actual_cash,
        //                    s.actual_lcc,
        //                    s.plan_cash,
        //                    s.plan_lcc,
        //                    s.work_days,
        //                    s.work_hours,
        //                    s.work_hauling,
        //                    s.rate_hauling,
        //                    s.unit_hauling,
        //                    s.percent,
        //                    s.sub_project_ers_id,
        //                    s.person_ers_work_id,
        //                    s.person_profile.sex,
        //                    s.person_profile.contact_no,
        //                });
        //            return Ok(person.FirstOrDefault());
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }

        //    else
        //    {
        //        // model.push_date = null;
        //        if (api != true)
        //        {
        //            //   model.push_status_id = 3;
        //            model.approval_id = 3;
        //        }
        //        model.person_ers_work_id = record.person_ers_work_id;
        //        model.created_by = record.created_by;
        //        model.created_date = record.created_date;
        //        model.last_modified_by = 0;
        //        model.last_modified_date = DateTime.Now;
        //        db.Entry(model).State = EntityState.Modified;

        //        try
        //        {
        //            await db.SaveChangesAsync();

        //            if (api == true)
        //            {
        //                return Ok();
        //            }

        //            var person =
        //              db.person_ers_work
        //              .Include(x => x.lib_ers_current_work)
        //            .Include(x => x.person_profile)
        //              .Where(x => x.person_ers_work_id == model.person_ers_work_id).Select(s => new
        //              {
        //                  s.person_profile.first_name,
        //                  s.person_profile.last_name,
        //                  s.person_profile.person_profile_id,
        //                  s.ers_current_work_id,
        //                  ers_current_work_name = s.lib_ers_current_work.name,
        //                  s.rate_day,
        //                  s.rate_hour,
        //                  s.actual_cash,
        //                  s.actual_lcc,
        //                  s.plan_cash,
        //                  s.plan_lcc,
        //                  s.work_days,
        //                  s.work_hours,
        //                  s.work_hauling,
        //                  s.rate_hauling,
        //                  s.unit_hauling,
        //                  s.percent,
        //                  s.sub_project_ers_id,
        //                  s.person_ers_work_id,
        //                  s.person_profile.sex,
        //                  s.person_profile.contact_no,
        //              });

        //            return Ok(person);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}
        //------------------------------------------------------------------- END ORIGINAL CODE ------------------------------------------------------------//

            
        #region New ERS: Worker can be added multiple times in one ERS 05-02-17

        //check if user already exists on the current ERS:
        [Route("api/offline/v1/sub_project/ers/worker_existence/get")]
        public bool check_if_worker_exists(Guid person_id, Guid ers_id)
        {
            var worker = db.person_ers_work.Where(x => x.person_profile_id == person_id && x.sub_project_ers_id == ers_id && x.is_deleted != true).ToList();

            if (worker.Count() >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //save for NEW record:
        [Route("api/offline/v1/sub_projects/ers/new_worker/save")]
        public async Task<IActionResult> SaveERSWorker(person_ers_work model, bool? api)
        {
            var record = db.person_ers_work.AsNoTracking().FirstOrDefault(x => x.person_ers_work_id == model.person_ers_work_id);

            //if saving a new record:
            if (record == null)
            {
                //inner if
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }
                //end inner if

                model.created_by = 0;
                model.created_date = DateTime.Now;
                model.is_deleted = false;
                db.person_ers_work.Add(model);

                try
                {
                    await db.SaveChangesAsync();
                    if (api == true)
                    {
                        return Ok();
                    }

                    var person =
                        db.person_ers_work
                        .Include(x => x.lib_ers_current_work)
                        .Include(x => x.person_profile)
                        .Where(x => x.person_ers_work_id == model.person_ers_work_id).Select(s => new
                        {
                            s.person_profile.first_name,
                            s.person_profile.last_name,
                            s.person_profile.person_profile_id,
                            s.person_profile.middle_name,
                            s.person_profile.birthdate,
                            s.person_profile.sex,
                            s.ers_current_work_id,
                            ers_current_work_name = s.lib_ers_current_work.name,
                            s.rate_day,
                            s.rate_hour,
                            s.actual_cash,
                            s.actual_lcc,
                            s.plan_cash,
                            s.plan_lcc,
                            s.work_days,
                            s.work_hours,
                            s.work_hauling,
                            s.rate_hauling,
                            s.unit_hauling,
                            s.percent,
                            s.sub_project_ers_id,
                            s.person_ers_work_id,
                            s.person_profile.contact_no,
                        });

                    return Ok(person.FirstOrDefault());
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }

            } //end-if


            //else, saving existing record based on person_ers_work_id
            else
            {
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }
                else
                {
                    model.push_status_id = 1;
                }

                model.push_date = null;
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

        //save for EDIT record:    
        [Route("api/offline/v1/sub_projects/ers/edit_worker/save")]
        public async Task<IActionResult> SaveEditedERSWorker(person_ers_work model, bool? api)
        {
            var record = db.person_ers_work.AsNoTracking().FirstOrDefault(x => x.person_profile_id == model.person_profile_id && x.sub_project_ers_id == model.sub_project_ers_id && x.is_deleted != true);

            if (api != true)
            {
                model.approval_id = 3;
            }

            model.person_ers_work_id = record.person_ers_work_id;
            model.created_by = record.created_by;
            model.created_date = record.created_date;
            model.last_modified_by = 0;
            model.last_modified_date = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();

                if (api == true)
                {
                    return Ok();
                }

                var person = db.person_ers_work
                    .Include(x => x.lib_ers_current_work)
                    .Include(x => x.person_profile)
                    .Where(x => x.person_ers_work_id == model.person_ers_work_id).Select(s => new
                    {
                        s.person_profile.first_name,
                        s.person_profile.last_name,
                        s.person_profile.person_profile_id,
                        s.person_profile.middle_name,
                        s.person_profile.birthdate,
                        s.person_profile.sex,
                        s.ers_current_work_id,
                        ers_current_work_name = s.lib_ers_current_work.name,
                        s.rate_day,
                        s.rate_hour,
                        s.actual_cash,
                        s.actual_lcc,
                        s.plan_cash,
                        s.plan_lcc,
                        s.work_days,
                        s.work_hours,
                        s.work_hauling,
                        s.rate_hauling,
                        s.unit_hauling,
                        s.percent,
                        s.sub_project_ers_id,
                        s.person_ers_work_id,
                        s.person_profile.contact_no,
                    });

                return Ok(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

        }

        #endregion

        

        #endregion ERS

        #region GetSETData
        private IQueryable<sub_project_set> GetSETData(
                 DataLayer.AngularFilterModel item
           )
        {
            var model = db.sub_project_set.Where(x => x.is_deleted != true).AsQueryable();


            if (item.set_date_eval != null)
            {
                model = model.Where(m => m.set_date_eval == item.set_date_eval);
            }
            if (item.set_physical_description != null)
            {
                model = model.Where(m => m.set_physical_description == item.set_physical_description);
            }
            if (item.set_total_score != null)
            {
                model = model.Where(m => m.set_total_score == item.set_total_score);
            }
            

            return model;
        }

        #endregion




        private IQueryable<sub_project> GetData(AngularFilterModel item)

        {
            var model = db.sub_project.AsQueryable(); //.Where(x => x.IsActive == true).AsQueryable();

            //for single sync

            if (item.id != null)
            {

                model = model.Where(x => x.sub_project_id == item.id);

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
                if (item.enrollment_id == 1) model = model.Where(x => x.enrollment_type_id == 2);
                if (item.enrollment_id == 2) model = model.Where(x => x.enrollment_type_id == 1);
            }
            //if (item.push_status_id != null)
            //{
            //    model = model.Where(m => m.push_status_id == item.push_status_id);
            //}
            if (item.approval_id != null)
            {
                model = model.Where(m => m.approval_id == item.approval_id);
            }

            if (item.physical_status_id != null)
            {
                model = model.Where(m => m.physical_status_id == item.physical_status_id);
            }

            if (item.project_type_id != null)
            {
                model = model.Where(m => m.project_type_id == item.project_type_id);
            }
            //if (item.lgu_level_id != null)
            //{
            //    model = model.Where(m => m.lgu_level_id == item.lgu_level_id);
            //}


            if (item.name != null)
            {
                model = model.Where(m => m.sub_project_name.Contains(item.name));
            }


            if (item.fund_source_id != null)
            {
                model = model.Where(m => m.modality_id == item.fund_source_id);
            }
            if (item.cycle_id != null)
            {
                model = model.Where(m => m.cycle_id == item.cycle_id);
            }


            return model;
        }




        [HttpPost]
        [Route("api/offline/v1/sub_projects/get_dto")]
        public PagedCollection<dynamic> GetDTO(AngularFilterModel item)
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
                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.IsActive != true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.IsActive == true).Count(),

                Items = model
                    .OrderBy(x => x.sub_project_id).Skip(size * currPages)
                    .Select(x => new
                    {
                        sub_project_unique_id = x.sub_project_unique_id,
                        sub_project_id = x.sub_project_id,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                        lib_city_city_name = x.lib_cities.city_name,
                        lib_province_prov_name = x.lib_provinces.prov_name,
                        lib_region_region_name = x.lib_regions.region_name,
                        lib_project_type_name = x.lib_project_type.name,
                        lib_physical_status_name = x.lib_physical_status.name,
                        sub_project_name = x.sub_project_name,
                        lib_fund_source_name = db.lib_fund_source.FirstOrDefault(c => c.fund_source_id == x.modality_id).name,
                        lib_cycle_name = x.lib_cycle.name,
                        Phy_Perc_To_Date = x.Phy_Perc_To_Date,
                        last_updated_date = x.last_updated_date
                    })
                    .Take(size).ToList(),
                };
        }

        [HttpPost]
        [Route("api/offline/v1/sub_projects/get_recently_edited")]
        public PagedCollection<dynamic> GetRecentlyEdited(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.IsActive == true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.IsActive == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.IsActive == true).Count(),

                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 3 && x.IsActive == true))
                    .OrderBy(x => x.sub_project_id).Skip(size * currPages)
                    .Select(x => new
                    {
                        sub_project_unique_id = x.sub_project_unique_id,
                        sub_project_id = x.sub_project_id,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                        lib_city_city_name = x.lib_cities.city_name,
                        lib_province_prov_name = x.lib_provinces.prov_name,
                        lib_region_region_name = x.lib_regions.region_name,
                        lib_project_type_name = x.lib_project_type.name,
                        lib_physical_status_name = x.lib_physical_status.name,
                        sub_project_name = x.sub_project_name,
                        lib_fund_source_name = db.lib_fund_source.FirstOrDefault(c => c.fund_source_id == x.modality_id).name,
                        lib_cycle_name = x.lib_cycle.name,
                        Phy_Perc_To_Date = x.Phy_Perc_To_Date
                    })
                    .Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/sub_projects/get_recently_added")]
        public PagedCollection<dynamic> GetRecentlyAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.IsActive == true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.IsActive == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.IsActive == true).Count(),

                Items = model
                    .Where(x => x.push_status_id != 1 && (x.push_status_id == 2 && x.IsActive == true))
                    .OrderBy(x => x.sub_project_id).Skip(size * currPages)
                    .Select(x => new
                    {
                        sub_project_unique_id = x.sub_project_unique_id,
                        sub_project_id = x.sub_project_id,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                        lib_city_city_name = x.lib_cities.city_name,
                        lib_province_prov_name = x.lib_provinces.prov_name,
                        lib_region_region_name = x.lib_regions.region_name,
                        lib_project_type_name = x.lib_project_type.name,
                        lib_physical_status_name = x.lib_physical_status.name,
                        sub_project_name = x.sub_project_name,
                        lib_fund_source_name = db.lib_fund_source.FirstOrDefault(c => c.fund_source_id == x.modality_id).name,
                        lib_cycle_name = x.lib_cycle.name,
                        Phy_Perc_To_Date = x.Phy_Perc_To_Date
                    })
                    .Take(size).ToList(),
            };
        }

        [HttpPost]
        [Route("api/offline/v1/sub_projects/get_recently_edited_and_added")]
        public PagedCollection<dynamic> GetRecentlyEditedandAdded(AngularFilterModel item)
        {
            var model = GetData(item);
            var totalCount = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.IsActive == true)).Count();
            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),
                TotalSync = model.Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.IsActive == true)).Count(),
                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.IsActive == true).Count(),

                Items = model
                    .Where(x => x.push_status_id != 1 && ((x.push_status_id == 2 || x.push_status_id == 3) && x.IsActive == true))
                    .OrderBy(x => x.sub_project_id).Skip(size * currPages)
                    .Select(x => new
                    {
                        sub_project_unique_id = x.sub_project_unique_id,
                        sub_project_id = x.sub_project_id,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                        lib_city_city_name = x.lib_cities.city_name,
                        lib_province_prov_name = x.lib_provinces.prov_name,
                        lib_region_region_name = x.lib_regions.region_name,
                        lib_project_type_name = x.lib_project_type.name,
                        lib_physical_status_name = x.lib_physical_status.name,
                        sub_project_name = x.sub_project_name,
                        lib_fund_source_name = db.lib_fund_source.FirstOrDefault(c => c.fund_source_id == x.modality_id).name,
                        lib_cycle_name = x.lib_cycle.name,
                        Phy_Perc_To_Date = x.Phy_Perc_To_Date
                    })
                    .Take(size).ToList(),
            };
        }


        [Route("api/offline/v1/sub_projects/save")]
        public async Task<IActionResult> Save(sub_project model, bool? api)
        {
            // return Ok(model);

            //if (!ModelState.IsValid)
            
            //{
            //    return BadRequest();
            //}




            if(api != true)
            {
                int physical_status_category_id = 0;
                //category 1
                if ((model.Tranche1amount == null || model.Tranche1amount == 0) && (model.Phy_Perc_To_Date == 0 || model.Phy_Perc_To_Date == null))
                {
                    physical_status_category_id = 1;
                }
                //category 2

                if ((model.Phy_Perc_To_Date == 0) && model.Tranche1amount > 0)
                {
                    physical_status_category_id = 3;
                }

                //category 3

                if (model.Phy_Perc_To_Date > 0 && model.Phy_Perc_To_Date < 51)
                {
                    physical_status_category_id = 4;
                }


                //category 4
                if (model.Phy_Perc_To_Date >= 51 && model.Phy_Perc_To_Date < 81)
                {
                    physical_status_category_id = 5;
                }


                //category 5
                if (model.Phy_Perc_To_Date >= 81 && model.Phy_Perc_To_Date < 100)
                {
                    physical_status_category_id = 6;
                }


                //category 6
                if (model.Phy_Perc_To_Date == 100)
                {
                    physical_status_category_id = 7;
                }


                model.physical_status_category_id = physical_status_category_id;


                

                if (model.physical_status_id == 1 && model.Date_of_Completion == null)
                {
                    return Json(new { success = false, message = "Date of Completion Required!" });
                }


                if (model.physical_status_id != 1 && model.Phy_Perc_To_Date >= 100)
                {
                    return Json(new { success = false, message = "Incomplete Projects cannot be 100!" });
                }

                if (model.physical_status_id == 1 && model.Phy_Perc_To_Date > 100)
                {
                    return Json(new { success = false, message = "Completed Projects cannot be more than 100%!" });
                }

                if (model.physical_status_id == 1 && model.Date_Started == null)
                {
                    return Json(new { success = false, message = "Date of Start Required!" });
                }

                if (model.physical_status_id == 1)
                {
                    if (model.Date_Started > model.Date_of_Completion)
                    {
                        return Json(new { success = false, message = "Date Start cannot be greater than date of completion!" });
                    }

                    if (model.Phy_Perc_To_Date != 100)
                    {
                        return Json(new { success = false, message = "Please check physical progress!" });
                    }
                }

                if (model.Phy_Perc_To_Date > 100)
                {

                    return Json(new { success = false, message = "You cannot achieve more than 100%!" });
                }

                if (model.physical_status_id == 1)
                {
                    if (model.Date_of_Completion == null)
                    {
                        return Json(new { success = false, message = "Date of Completion is required for Completed Projects" });
                    }
                    else
                    {
                        if (model.Date_of_Completion > DateTime.Now)
                        {
                            return Json(new { success = false, message = "Date of Completion cannot be in Advance@" });
                        }
                    }
                }

                if (model.Phy_Perc_To_Date > 0)
                {
                    if (model.Date_Started == null)
                    {
                        return Json(new { success = false, message = "Date of Start is required for Projects with >0 Accomplishment" });
                    }
                }

                if (model.physical_status_id != 1)
                {
                    if (model.Date_of_Completion != null)
                    {
                        return Json(new { success = false, message = "Date of Completion must be removed for not yet completed projects" });
                    }

                }
                if (model.Date_Started != null)
                {
                    if (model.Date_Started > DateTime.Now)
                    {
                        return Json(new { success = false, message = "You cannot report in advance!" });
                    }
                }
            }

            var record = db.sub_project.AsNoTracking().FirstOrDefault(x => x.sub_project_unique_id == model.sub_project_unique_id);




            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    //  model.push_date = null;
                    //    model.approval_id = 3;

                }
                else
                {
                    model.push_status_id = 1;
                }


                model.created_by = "";
                model.created_date = DateTime.Now;

                model.IsActive = true;
                db.sub_project.Add(model);



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
                // model.push_date = null;


                if (api != true)
                {
                    //   model.push_status_id = 3;
                    model.approval_id = 3;
                }
                else
                {

                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_updated_by = "";
                model.last_updated_date = DateTime.Now;

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

        [Route("api/offline/v1/sub_project/get")]
        public IActionResult Get(Guid id)
        {

            var model = db.sub_project

                .FirstOrDefault(x => x.sub_project_unique_id == id);



            if (model == null)
            {



                var item = new sub_project
                {
                    sub_project_id = 0,
                    sub_project_unique_id = id,
                    push_status_id = 2,
                    IsActive = true
                };

                return Ok(item);

            }
            else
            {

                //model.push_status_id = 3;
                //  model.last_modified_by = 0;
                //    model.last_modified_date = DateTime.Now;
                model.approval_id = 3;


            }

            return Ok(model);
        }






        [HttpPost]
        [Route("Sync/Get/sub_project")]
        public async Task<ActionResult> GetOnline(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/geo/projects/get_by_city_code?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<sub_project>>(responseJson.Result);

                    //var all = Mapper.DynamicMap<List<sub_project_mapping>, List<sub_project>>(model);

                    foreach (var item in model.ToList())
                    {
                        await Save(item, true);
                    }

                    await GetOnlineERSList(username, password, city_code, record_id);

                    await GetOnlineERSWorkers(username, password, city_code, record_id);

                    await GetOnlineERFR(username, password, city_code, record_id);

                    await GetOnlinePhotos(username, password, city_code, record_id);

                    await GetOnlineCoverage(username, password, city_code, record_id);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }

        public async Task<bool> GetOnlineERSList(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/ers/mapping/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var all = JsonConvert.DeserializeObject<List<sub_project_ers>>(responseJson.Result);



                    foreach (var item in all.ToList())
                    {
                        await SaveERS(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public async Task<bool> GetOnlineERSWorkers(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/ers/workers/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var all = JsonConvert.DeserializeObject<List<person_ers_work>>(responseJson.Result);



                    foreach (var item in all.ToList())
                    {
                        await SaveERSWorker(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public async Task<bool> GetOnlineERFR(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/erfr/projects/get_by_city_code?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var all = JsonConvert.DeserializeObject<List<erfr_sub_project>>(responseJson.Result);



                    foreach (var item in all.ToList())
                    {
                        await SaveERFR(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public async Task<bool> GetOnlinePhotos(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/geo/projects/photos/get_by_city_code?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var all = JsonConvert.DeserializeObject<List<SPPhoto>>(responseJson.Result);



                    foreach (var item in all.ToList())
                    {
                        await SaveSPPhotos(item, true);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public async Task<bool> GetOnlineCoverage(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/geo/projects/coverage/get_by_city_code?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var all = JsonConvert.DeserializeObject<List<sub_project_coverage>>(responseJson.Result);



                    foreach (var item in all.ToList())
                    {
                        var record = db.sub_project_coverage.AsNoTracking().FirstOrDefault(x => x.sub_project_unique_id == item.sub_project_unique_id && x.brgy_code == item.brgy_code);

                        if (record == null)
                        {



                       
                            item.push_status_id = 1;

                            db.sub_project_coverage.Add(item);


                            try
                            {
                                await db.SaveChangesAsync();
 
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                               
                            }
                        }
                        else
                        {

                            item.id = record.id;

                            item.sub_project_id = record.sub_project_id;
 

                            db.Entry(item).State =  EntityState.Modified;

                            try
                            {
                                await db.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                 
                            }
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }


        [Route("api/offline/v1/sub_projects/post/selected_items_to_sync")]
        public async Task<IActionResult> SavePushStatusID(Guid sub_project_unique_id, int push_status_id)
        {
            var subproject = db.sub_project.Find(sub_project_unique_id);

            if (subproject == null)
            {
                return BadRequest("Error");
            }

            subproject.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }


        //new code:

        [HttpPost]
        [Route("Sync/Post/sub_project")]
        public async Task<ActionResult> PostOnline(string username, string password, int? record_id = null)
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

                var items_preselected = db.sub_project.Where(x => x.push_status_id == 5).ToList();

                if (!items_preselected.Any())
                {
                    var items = db.sub_project.Where(x => x.push_status_id != 1 && (x.push_status_id != 4));

                    if (record_id != null)
                    {
                        items = items.Where(x => x.sub_project_id == record_id);
                    }

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            await db.SaveChangesAsync();
                        }
                    }
                }
                else {
                    var items = db.sub_project.Where(x => x.push_status_id == 5);

                    if (record_id != null)
                    {
                        items = items.Where(x => x.sub_project_id == record_id);
                    }

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            await db.SaveChangesAsync();
                        }
                    }
                }               

            }
            await PostOnlineERS(username, password, record_id);
            await PostOnlineERSWorker(username, password, record_id);
            return Ok();
        }


        //old:

        //[HttpPost]
        //[Route("Sync/Post/sub_project")]
        //public async Task<ActionResult> PostOnline(string username, string password, int? record_id = null)
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

        //        var items = db.sub_project.AsEnumerable(); //.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

        //        if (record_id != null)
        //        {
        //            items = items.Where(x => x.sub_project_id == record_id);
        //        }

        //        foreach (var item in items.ToList())
        //        {
        //            // var push = Mapper.DynamicMap<sub_project, sub_project_mapping>(item);
        //            StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

        //            HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/save", data).Result;

        //            // response.EnsureSuccessStatusCode();

        //            if (response.IsSuccessStatusCode)
        //            {

        //                item.push_status_id = 1;
        //                //    item.push_date = DateTime.Now;

        //                await db.SaveChangesAsync();

        //            }
        //            else
        //            {
        //                item.push_status_id = 4;
        //                // item.push_date = DateTime.Now;
        //                await db.SaveChangesAsync();
        //            }
        //        }

        //    }


        //    await PostOnlineERS(username, password, record_id);

        //    await PostOnlineERSWorker(username, password, record_id);

        //    return Ok();
        //}

        public async Task<bool> PostOnlineERS(string username, string password, int? record_id = null)
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


                var items = db.sub_project_ers.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

                if (record_id != null)
                {
                    items = items.Where(x => x.sub_project_id == record_id);
                }

                foreach (var item in items.ToList())
                {

                    // var push = Mapper.DynamicMap<sub_project, sub_project_mapping>(item);

                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync("api/offline/v1/spi/ers/list/save", data).Result;

                    // response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {

                        item.push_status_id = 1;
                        //    item.push_date = DateTime.Now;

                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        item.push_status_id = 4;
                        // item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                }

            }

            return true;
        }

        public async Task<bool> PostOnlineERSWorker(string username, string password, int? record_id = null)
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


                var items = db.person_ers_work.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

              

                foreach (var item in items.ToList())
                {

                    // var push = Mapper.DynamicMap<sub_project, sub_project_mapping>(item);

                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync("api/offline/v1/person/ers/save", data).Result;

                    // response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {

                        item.push_status_id = 1;
                        //    item.push_date = DateTime.Now;

                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        item.push_status_id = 4;
                        // item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                }

            }

            return true;
        }

        public async Task<bool> PostOnlineCoverage(string username, string password, int? record_id = null)
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


                var items = db.sub_project_coverage.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2));



                foreach (var item in items.ToList())
                {

                    // var push = Mapper.DynamicMap<sub_project, sub_project_mapping>(item);

                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/coverage/save", data).Result;

                    // response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {

                        item.push_status_id = 1;
                        //    item.push_date = DateTime.Now;

                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        item.push_status_id = 4;
                        // item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                }

            }

            return true;
        }

        [Route("api/subproject/get_location_coverage")]
        public List<sub_project_coverage> get_location_coverage(Guid id)
        {


            var model = db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == id);


            if(model == null)
            {
                return null;

            }
            var area = db.lib_brgy.Where(x => x.city_code == model.city_code);


            var list = new List<sub_project_coverage>();

            foreach (var item in area)
            {
                var coverage = db.sub_project_coverage.SingleOrDefault(x => x.sub_project_unique_id == id && x.brgy_code == item.brgy_code);

                if (coverage == null)
                {
                    var x = new sub_project_coverage
                    {
                        brgy_code = item.brgy_code,
                        brgy_name = item.brgy_name,
                        sub_project_id = model.sub_project_id,
                        sub_project_unique_id = model.sub_project_unique_id,
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



        [Route("api/subproject/post_location_coverage")]
        public bool post_location_coverage(int brgy_code, Guid id)
        {

            string Area = null;


            //if (brgy_code.Length != 9)
            //{
            //    brgy_code = "0" + brgy_code;
            //}

            Area = db.lib_brgy.SingleOrDefault(x => x.brgy_code == brgy_code).brgy_name;

            var project = db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == id);


            var access = db.sub_project_coverage.FirstOrDefault(x => x.sub_project_unique_id == id && x.brgy_code == brgy_code);

            access.sub_project_id = project.sub_project_id;
            access.sub_project_unique_id = id;

            if (access != null)
            {


                access.Selected = false;


                db.SaveChanges();



            }

            else
            {


                var item = new sub_project_coverage
                {
                    sub_project_unique_id = id,
                    sub_project_id = project.sub_project_id,
                    brgy_code = brgy_code,
                    Selected = true,

                    brgy_name = Area,
                };
                db.sub_project_coverage.Add(item);
                db.SaveChanges();


            }






            return true;
        }


        #region ERS

        //[Route("api/offline/v1/sub_projects/save")]
        //public async Task<IActionResult> SaveERSList(sub_project_ers model, bool? api)
        //{


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var record = db.sub_project_ers.AsNoTracking().FirstOrDefault(x => x.sub_project_ers_id == model.sub_project_ers_id);

        //    if (record == null)
        //    {


        //        if (api != true)
        //        {
        //            //   model.push_status_id = 2;
        //            //  model.push_date = null;
        //            model.approval_id = 3;
        //        }


        //        model.created_by = "";
        //        model.created_date = DateTime.Now;

        //        model.IsActive = true;
        //        db.sub_project.Add(model);


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
        //        // model.push_date = null;


        //        if (api != true)
        //        {
        //            //   model.push_status_id = 3;
        //            model.approval_id = 3;
        //        }



        //        model.created_by = record.created_by;
        //        model.created_date = record.created_date;


        //        model.last_updated_by = "";
        //        model.last_updated_date = DateTime.Now;

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

        #endregion

    }
}