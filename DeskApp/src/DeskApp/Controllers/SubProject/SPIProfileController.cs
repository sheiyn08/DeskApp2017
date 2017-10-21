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
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing
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

        //private async Task<IActionResult> SaveERFR(erfr_sub_project model, bool? api)
        //{


        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    var record = db.erfr_sub_project.AsNoTracking().FirstOrDefault(x => x.SPID == model.SPID);

        //    if (record == null)
        //    {



        //        db.erfr_sub_project.Add(model);


        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            //return Ok(model);
        //            return Ok(); //modified June 2, 2017 
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {


        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {


        //        db.Entry(model).State = EntityState.Modified;

        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            //return Ok(model);
        //            return Ok(); //modified June 2, 2017 
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}

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
               
                if (sub_project.Length == 8)
                {
                    sub_project = "0" + sub_project;
                }                   


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
                    //return Ok(model);
                    return Ok(); //modified June 2, 2017 
                }
                catch (DbUpdateConcurrencyException)
                {


                    return BadRequest();
                }
            }
            else
            {
                //model.Id = record.Id; //commented June 2, 2017

                db.Entry(model).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                    //return Ok(model);
                    return Ok(); //modified June 2, 2017 
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
        }


        #region SPCF v3.o
        [Route("api/offline/v1/sub_project/spcf/get")]
        public IActionResult GetSPCF(Guid id)
        {
            var model = db.sub_project_spcf.Select(sub_project_spcfDTO.SELECT).Where(x => x.sub_project_unique_id == id && x.is_deleted != true);
            return Ok(model);
        }
            
        [Route("api/offline/v1/spi/spcf/get")]
        public IActionResult GetSpiSPCF(Guid id)
        {
            var model = db.sub_project_spcf.FirstOrDefault(x => x.sub_project_unique_id == id && x.is_deleted != true);

            if (model == null)
            {
                var item = new sub_project_spcf();

                item.sub_project_spcf_id = id;
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
        

        [Route("api/offline/v1/sub_projects/spcf/save")]
        public async Task<IActionResult> SaveSPCF(sub_project_spcf model, bool? api)
        {
            var record = db.sub_project_spcf.AsNoTracking().FirstOrDefault(x => x.sub_project_spcf_id == model.sub_project_spcf_id);

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
                db.sub_project_spcf.Add(model);

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

        #endregion


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
                    model.push_status_id = 2;
                    model.push_date = null;
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
                model.push_date = null;
                
                if (api != true)
                {
                    model.push_status_id = 3;
                    //model.approval_id = 3;
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
            var model = db.sub_project.Where(x => x.IsActive == true).AsQueryable();
            //old:
            //var model = db.sub_project.AsQueryable();


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

        private IQueryable<sub_project_reference_table> GetReferenceData(AngularFilterModel item)

        {
            var model = db.sub_project_reference_table.Where(x => x.IsActive == true).AsQueryable();
            
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
            if (item.fund_source_id != null)
            {
                model = model.Where(m => m.modality_id == item.fund_source_id);
            }
            if (item.cycle_id != null)
            {
                model = model.Where(m => m.cycle_id == item.cycle_id);
            }
            if (item.enrollment_id != null)
            {
                if (item.enrollment_id == 1) model = model.Where(x => x.enrollment_type_id == 1);
                if (item.enrollment_id == 2) model = model.Where(x => x.enrollment_type_id == 2);
            }
            if (item.project_type_id != null)
            {
                model = model.Where(m => m.project_type_id == item.project_type_id);
            }
            if (item.physical_status_id != null)
            {
                model = model.Where(m => m.physical_status_id == item.physical_status_id);
            }
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
                        last_updated_date = x.last_updated_date,
                        push_status_id = x.push_status_id,
                    })
                    .Take(size).ToList(),
                };
        }


        [HttpPost]
        [Route("api/offline/v1/sub_project_references/get_dto")]
        public PagedCollection<dynamic> GetReferencesDTO(AngularFilterModel item)
        {
            var model = GetReferenceData(item);
            var ref_totalCount = model.Count();
            int ref_currPages = item.ref_currPage ?? 0;
            int ref_size = item.ref_pageSize ?? 10;

            return new PagedCollection<dynamic>()
            {
                Ref_Page = ref_currPages,
                Ref_TotalCount = ref_totalCount,
                Ref_TotalPages = (int)Math.Ceiling((decimal)ref_totalCount / ref_size),

                References = model
                    .OrderBy(x => x.sub_project_id).Skip(ref_size * ref_currPages)
                    .Select(x => new
                    {
                        sub_project_id = x.sub_project_id,
                        lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                        lib_city_city_name = x.lib_cities.city_name,
                        lib_province_prov_name = x.lib_provinces.prov_name,
                        lib_region_region_name = x.lib_regions.region_name,
                        lib_project_type_name = x.lib_project_type.name,
                        lib_fund_source_name = db.lib_fund_source.FirstOrDefault(c => c.fund_source_id == x.modality_id).name,
                        lib_cycle_name = x.lib_cycle.name,
                        sub_project_name = x.sub_project_name,
                        lib_physical_status_name = x.lib_physical_status.name,
                        is_paired = x.is_paired == null ? null : x.is_paired == true ? "Yes" : "No"
                    })
                    .Take(ref_size).ToList(),
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
                        Phy_Perc_To_Date = x.Phy_Perc_To_Date,
                        push_status_id = x.push_status_id,
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
                        Phy_Perc_To_Date = x.Phy_Perc_To_Date,
                        push_status_id = x.push_status_id,
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
                        Phy_Perc_To_Date = x.Phy_Perc_To_Date,
                        push_status_id = x.push_status_id,
                    })
                    .Take(size).ToList(),
            };
        }


        [Route("api/offline/v1/sub_projects/save")]
        public async Task<IActionResult> Save(sub_project model, bool? api)
        {
            if (api != true)
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

            //checking if record already exists using the sub_project_unique_id
            var record = db.sub_project.AsNoTracking().FirstOrDefault(x => x.sub_project_unique_id == model.sub_project_unique_id);
            

            if (record == null)
            {                
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.created_by = "";
                    model.created_date = DateTime.Now;
                    model.IsActive = true;
                }

                //because api is set to TRUE in sync/get
                if (api == true)
                {
                    model.push_status_id = 1;
                    model.IsActive = true;
                }

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
                //model.push_date = null; -- no push_date column for sub_project

                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
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


        //Test 09-27-17 save to reference table to be used for sync/get
        [Route("api/offline/v1/sub_project_reference/save")]
        public async Task<IActionResult> SavetoReferenceTable(sub_project_reference_table model, bool? api)
        {
            var record = db.sub_project_reference_table.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id);

            if (record == null)
            {
                //because api is set to TRUE in sync/get
                if (api == true)
                {
                    model.push_status_id = 1;
                    model.IsActive = true;
                    model.is_paired = false;
                }

                db.sub_project_reference_table.Add(model);

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


        //get details from reference table : to be displayed on mdDialog
        [Route("api/offline/v1/sub_project_reference/get")]
        public IActionResult GetReference(int id)
        {
            var model = db.sub_project_reference_table.Where(x => x.sub_project_id == id);

            //if (model == null)
            //{
            //    var item = new sub_project_reference_table
            //    {
            //        sub_project_id = 0,
            //        sub_project_unique_id = id,
            //        push_status_id = 2,
            //        IsActive = true
            //    };
            //    return Ok(item);
            //}
            //else
            //{
            //    //model.push_status_id = 3;
            //    //  model.last_modified_by = 0;
            //    //    model.last_modified_date = DateTime.Now;
            //    model.approval_id = 3;
            //}

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
                    await GetOnlinePhotos(username, password, city_code, record_id);
                    await GetOnlineCoverage(username, password, city_code, record_id);
                    await GetOnlineSET(username, password, city_code, record_id);
                    await GetOnlineSPCF(username, password, city_code, record_id);

                    await GetOnlineReferenceTable(username, password, city_code, record_id);

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

        //public async Task<bool> GetOnlineERFR(string username, string password, string city_code = null, int? record_id = null)
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

        //        HttpResponseMessage response = client.GetAsync("api/offline/v1/erfr/projects/get_by_city_code?city_code=" + city_code + "&id=" + record_id).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseJson = response.Content.ReadAsStringAsync();

        //            var all = JsonConvert.DeserializeObject<List<erfr_sub_project>>(responseJson.Result);



        //            foreach (var item in all.ToList())
        //            {
        //                await SaveERFR(item, true);
        //            }

        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }


        //}

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
                            //item.push_status_id = 1;

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
                            //Commented: June 2, 2017
                            //item.id = record.id;
                            //item.sub_project_id = record.sub_project_id;
 

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


        //v3.0 Sync get for SET: 10-12-2017
        public async Task<bool> GetOnlineSET(string username, string password, string city_code = null, int? record_id = null)
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/set/mapping/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;
                    
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<sub_project_set>>(responseJson.Result);
                    foreach (var item in all.ToList())
                    {
                        await SaveSET(item, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //v3.0 Sync get for SPCF: 10-12-2017
        public async Task<bool> GetOnlineSPCF(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/spcf/mapping/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<sub_project_spcf>>(responseJson.Result);
                    foreach (var item in all.ToList())
                    {
                        await SaveSPCF(item, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //v3.0 Sync get for Reference Table: 10-17-2017
        public async Task<bool> GetOnlineReferenceTable(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/geo/projects/get_by_city_code?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var ref_model = JsonConvert.DeserializeObject<List<sub_project_reference_table>>(responseJson.Result); //--testing 09-27-17
                    
                    foreach (var item in ref_model.ToList())
                    {
                        await SavetoReferenceTable(item, true);
                    }                    

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //v3.0 updating SP based on Reference details:
        [Route("api/offline/v1/sub_projects/update")]
        public async Task<IActionResult> UpdateSP(sub_project model, bool? api)
        {
            var sp_reference = db.sub_project_reference_table.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id);

            //-------------- START PAIRING: --------------//

            //details tab:
            model.erfr_project_id = sp_reference.erfr_project_id;
            model.sub_project_name = sp_reference.sub_project_name;
            model.project_type_id = sp_reference.project_type_id;
            model.is_public_school_for_ip = sp_reference.is_public_school_for_ip;
            model.training_category_id = sp_reference.training_category_id;
            model.modality_id = sp_reference.modality_id;
            model.fund_source_id = sp_reference.fund_source_id;
            model.cycle_id = sp_reference.cycle_id;
            model.is_lcc = sp_reference.is_lcc;
            model.is_incentive = sp_reference.is_incentive;
            model.is_savings = sp_reference.is_savings;
            model.is_lgu_led = sp_reference.is_lgu_led;
            model.mode_id = sp_reference.mode_id;
            model.modality_category_id = sp_reference.modality_category_id;
            model.region_code = sp_reference.region_code;
            model.prov_code = sp_reference.prov_code;
            model.city_code = sp_reference.city_code;
            model.brgy_code = sp_reference.brgy_code;            
            //covered barangays here

            //SPCF tab:
            model.NoOfHH = sp_reference.NoOfHH;

            //SPCR tab:
            model.NoOfHHActual = sp_reference.NoOfHHActual;
            model.no_actual_families = sp_reference.no_actual_families;
            model.no_actual_pantawid_households = sp_reference.no_actual_pantawid_households;
            model.no_actual_pantawid_families = sp_reference.no_actual_pantawid_families;
            model.no_actual_slp_households = sp_reference.no_actual_slp_households;
            model.no_actual_slp_families = sp_reference.no_actual_slp_families;
            model.no_actual_ip_households = sp_reference.no_actual_ip_households;
            model.no_actual_ip_families = sp_reference.no_actual_ip_families;
            model.no_actual_male = sp_reference.no_actual_male;
            model.no_actual_female = sp_reference.no_actual_female;

            //Financial Info tab:
            model._dep_ed_amount = sp_reference._dep_ed_amount;
            model.is_deped_funded = sp_reference.is_deped_funded;
            model.Kalahi_Amount = sp_reference.Kalahi_Amount;
            model.Tranche1amount = sp_reference.Tranche1amount;
            model.Tranche2amount = sp_reference.Tranche2amount;
            model.Tranche3amount = sp_reference.Tranche3amount;
            model.has_local_counterpart = sp_reference.has_local_counterpart;
            model.LCC_Amount = sp_reference.LCC_Amount;
            model.LCC_Adj = sp_reference.LCC_Adj;
            model.operation_maintainance_cost = sp_reference.operation_maintainance_cost;
            model.is_multiple_sps = sp_reference.is_multiple_sps;
            //multiple sps here
            model.target_tranching_first = sp_reference.target_tranching_first;
            model.target_tranching_second = sp_reference.target_tranching_second;
            model.target_tranching_third = sp_reference.target_tranching_third;
            model.has_t1 = sp_reference.has_t1;
            model.Tranche1date = sp_reference.Tranche1date;
            model.Tranche1amount = sp_reference.Tranche1amount;
            model.erfr_t1 = sp_reference.erfr_t1;
            model.Tranche1amount_utilized = sp_reference.Tranche1amount_utilized;
            model.has_t2 = sp_reference.has_t2;
            model.Tranche2date = sp_reference.Tranche2date;
            model.Tranche2amount = sp_reference.Tranche2amount;
            model.erfr_t2 = sp_reference.erfr_t2;
            model.Tranche2amount_utilized = sp_reference.Tranche2amount_utilized;
            model.has_t3 = sp_reference.has_t3;
            model.Tranche3date = sp_reference.Tranche3date;
            model.Tranche3amount = sp_reference.Tranche3amount;
            model.erfr_t3 = sp_reference.erfr_t3;
            model.Tranche3amount_utilized = sp_reference.Tranche3amount_utilized;

            //Physical Progress tab:
            model.physical_status_id = sp_reference.physical_status_id;
            model.replaced_sub_project_id = sp_reference.replaced_sub_project_id;
            model.Date_Started = sp_reference.Date_Started;
            model.Phy_Perc_To_Date = sp_reference.Phy_Perc_To_Date;
            model.PlannedDate_Completion = sp_reference.PlannedDate_Completion;
            model.Date_of_Completion = sp_reference.Date_of_Completion;
            model.has_variation = sp_reference.has_variation;
            model.is_sp_functional = sp_reference.is_sp_functional;
            model.is_enhancement_functionality = sp_reference.is_enhancement_functionality;
            model.variation_physical_status_id = sp_reference.variation_physical_status_id;
            model.variation_phy_perc_to_date = sp_reference.variation_phy_perc_to_date;
            model.variation_target_date_completion = sp_reference.variation_target_date_completion;
            model.variation_actual_date_completion = sp_reference.variation_actual_date_completion;
            model.phy_has_construction_target = sp_reference.phy_has_construction_target;
            model.phy_construction_target = sp_reference.phy_construction_target;
            model.phy_construction_actual = sp_reference.phy_construction_actual;
            model.phy_has_improvement_target = sp_reference.phy_has_improvement_target;
            model.phy_improvement_target = sp_reference.phy_improvement_target;
            model.phy_improvement_actual = sp_reference.phy_improvement_actual;
            model.phy_has_rehabilitation_target = sp_reference.phy_has_rehabilitation_target;
            model.phy_rehabilitation_target = sp_reference.phy_rehabilitation_target;
            model.phy_rehabilitation_actual = sp_reference.phy_rehabilitation_actual;
            model.phy_has_purchase_target = sp_reference.phy_has_purchase_target;
            model.phy_purchase_target = sp_reference.phy_purchase_target;
            model.phy_purchase_actual = sp_reference.phy_purchase_actual;
            model.phy_has_construction_target_secondary = sp_reference.phy_has_construction_target_secondary;
            model.phy_construction_target_secondary = sp_reference.phy_construction_target_secondary;
            model.phy_construction_actual_secondary = sp_reference.phy_construction_actual_secondary;
            model.phy_has_improvement_target_secondary = sp_reference.phy_has_improvement_target_secondary;
            model.phy_improvement_target_secondary = sp_reference.phy_improvement_target_secondary;
            model.phy_improvement_actual_secondary = sp_reference.phy_improvement_actual_secondary;
            model.phy_has_rehabilitation_target_secondary = sp_reference.phy_has_rehabilitation_target_secondary;
            model.phy_rehabilitation_target_secondary = sp_reference.phy_rehabilitation_target_secondary;
            model.phy_rehabilitation_actual_secondary = sp_reference.phy_rehabilitation_actual_secondary;
            model.phy_has_purchase_target_secondary = sp_reference.phy_has_purchase_target_secondary;
            model.phy_purchase_target_secondary = sp_reference.phy_purchase_target_secondary;
            model.phy_purchase_actual_secondary = sp_reference.phy_purchase_actual_secondary;

            //Safeguards tab:
            model.has_sc_essc = sp_reference.has_sc_essc;
            model.has_sc_esmp = sp_reference.has_sc_esmp;
            model.has_sc_ecc = sp_reference.has_sc_ecc;
            model.has_sc_cnc = sp_reference.has_sc_cnc;
            model.has_ip_presence = sp_reference.has_ip_presence;
            model.has_cadt = sp_reference.has_cadt;
            model.has_on_process = sp_reference.has_on_process;
            model.has_cadteable = sp_reference.has_cadteable;
            model.has_ncip = sp_reference.has_ncip;
            model.ncip_date = sp_reference.ncip_date;
            model.has_validation_conducted = sp_reference.has_validation_conducted;
            model.validation_conducted_date = sp_reference.validation_conducted_date;
            model.has_sc_cp = sp_reference.has_sc_cp;
            model.sc_cp_date = sp_reference.sc_cp_date;
            model.has_sc_cno = sp_reference.has_sc_cno;
            model.sc_cno_date = sp_reference.sc_cno_date;
            model.land_aq_deed_of_donation = sp_reference.land_aq_deed_of_donation;
            model.quit_claim = sp_reference.quit_claim;
            model.land_aq_unsfruct = sp_reference.land_aq_unsfruct;
            model.right_of_way_agreement = sp_reference.right_of_way_agreement;
            model.permit_to_construct_enter = sp_reference.permit_to_construct_enter;
            model.land_aq_blgu_resolution = sp_reference.land_aq_blgu_resolution;
            model.land_aq_deped_certification = sp_reference.land_aq_deped_certification;
            model.land_aq_other = sp_reference.land_aq_other;
            model.other_land_acquisition = sp_reference.other_land_acquisition;

            //changing push status:
            model.push_status_id = 3;
            model.last_modified_by = 0;
            model.last_updated_date = DateTime.Now;

            //updating the reference table:
            sp_reference.is_paired = true;
            sp_reference.paired_sp_unique_id = model.sub_project_unique_id;
            sp_reference.paired_by = 0;
            sp_reference.paired_date = DateTime.Now;


            db.Entry(model).State = EntityState.Modified;
            //db.Entry(db.sub_project_reference_table).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                await UpdateReferenceTable(sp_reference);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            
        }


        //v3.0 check if the unique id in view has been paired to sp id (in the reference table)
        [Route("api/offline/v1/sub_projects/check_pairing_status")]
        public bool CheckSPPairing(Guid sub_project_unique_id, int sub_project_id)
        {
            var is_paired = db.sub_project_reference_table.Where(x => x.is_paired == true && x.paired_sp_unique_id == sub_project_unique_id && x.sub_project_id == sub_project_id).ToList();

            if (is_paired.Count() >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public async Task<IActionResult> UpdateReferenceTable(sub_project_reference_table model)
        {
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
                        //else
                        //{
                        //    item.push_status_id = 4;
                        //    await db.SaveChangesAsync();
                        //}
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
                        //else
                        //{
                        //    item.push_status_id = 4;
                        //    await db.SaveChangesAsync();
                        //}
                    }
                }               

            }
            await PostOnlineERS(username, password, record_id);
            await PostOnlineERSWorker(username, password, record_id);
            await PostOnlineSet(username, password, record_id);
            await PostOnlineSPCF(username, password, record_id);
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
                        item.push_date = DateTime.Now;

                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        //item.push_status_id = 4;
                        // item.push_date = DateTime.Now;
                        //await db.SaveChangesAsync();
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

            return true;
        }


        //------v3.0 sync upload for SET: by Glenn 09/15/17

        public async Task<bool> PostOnlineSet(string username, string password, int? record_id = null)
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
                
                var items = db.sub_project_set.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

                if (record_id != null)
                {
                    items = items.Where(x => x.sub_project_id == record_id);
                }

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/spi/set/list/save", data).Result;
                    
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


        //------v3.0 sync upload for SPCF: 10/12/2017

        public async Task<bool> PostOnlineSPCF(string username, string password, int? record_id = null)
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

                var items = db.sub_project_spcf.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true));

                if (record_id != null)
                {
                    items = items.Where(x => x.sub_project_id == record_id);
                }

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/spi/spcf/list/save", data).Result;

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