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
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing
        public static string geoUrl = @"https://geotagging.dswd.gov.ph";


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

                             Date_First_Download = s.Tranche1date == null ? "" : s.Tranche1date.Value.ToString("dd/MM/yyyy"),
                             Date_Started = s.Date_Started == null ? "" : s.Date_Started.Value.ToString("dd/MM/yyyy"),
                             Date_of_Completion = s.Date_of_Completion == null ? "" : s.Date_of_Completion.Value.ToString("dd/MM/yyyy"),
                             PlannedDate_Completion = s.PlannedDate_Completion == null ? "" : s.PlannedDate_Completion.Value.ToString("dd/MM/yyyy"),

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

                             Tranche1date = s.Tranche1date == null ? "" : s.Tranche1date.Value.ToString("dd/MM/yyyy"),
                             Tranche2date = s.Tranche2date == null ? "" : s.Tranche2date.Value.ToString("dd/MM/yyyy"),
                             Tranche3date = s.Tranche3date == null ? "" : s.Tranche3date.Value.ToString("dd/MM/yyyy"),

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
                             created_date = s.created_date == null ? null : s.created_date.ToString("dd/MM/yyyy"),

                             s.last_updated_by,
                             last_updated_date = s.last_updated_date == null ? null : s.last_updated_date.Value.ToString("dd/MM/yyyy"),

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


        #region 4.2 export SET
        [Route("api/export/sub_project/list_set")]
        public IActionResult export_set(AngularFilterModel model)
        {
            var list = GetData(model);

            //var sps_with_set = db.sub_project.Where(s => db.sub_project_set.Select(ss => ss.sub_project_id).Contains(s.sub_project_id));
                
            var model_set = db.sub_project_set.Where(x => x.is_deleted != true);

            var result = from s in list
                         select
                         new
                         {                             
                             Region = s.lib_regions.region_name,
                             Province = s.lib_provinces.prov_name,
                             Municipality = s.lib_cities.city_name,
                             Barangay = s.lib_brgy.brgy_name,
                             SPID = s.sub_project_id,
                             SP_Title = s.sub_project_name,
                             SP_Type = s.lib_project_type.name,
                             Cycle = s.lib_cycle.name,
                             With_SET_encoded = db.sub_project_set.Select(ss => ss.sub_project_id).Contains(s.sub_project_id) ? "Yes" : "N/A"
                         };

            var convert_list = result.ToList();

            var join = from x in convert_list
                          join p in model_set
                          on x.SPID equals p.sub_project_id into g
                          from p in g.DefaultIfEmpty()
                          orderby x.With_SET_encoded descending
                          select new
                          {
                              x.Region,
                              x.Province,
                              x.Municipality,
                              x.Barangay,
                              x.SPID,
                              x.SP_Title,
                              x.SP_Type,
                              x.Cycle,
                              x.With_SET_encoded,
                              Date_of_Evaluation = p?.set_date_eval != null ? p.set_date_eval.Value.Month + "/" + p.set_date_eval.Value.Day + "/" + p.set_date_eval.Value.Year : "",     
                              Physical_Decription = p != null ? p.set_physical_description : "",
                              SP_Utilization = p != null ? p.set_sp_utilization.ToString() : "",
                              Organization = p != null ? p.set_organization.ToString() : "",
                              Institutional_linkage = p != null ? p.set_institutional_linkage.ToString() : "",
                              Financial = p != null ? p.set_financial.ToString() : "",
                              Physical = p != null ? p.set_physical.ToString() : "",
                              Total_score = p != null ? p.set_total_score.ToString() : "",
                              Adjectival_Rating_for_Set =
                                                          p?.set_total_score >= 5.1 ? "N/A" : p?.set_total_score >= 4.76 ? "Excellent" :
                                                          p?.set_total_score >= 3.51 ? "Very Satisfactory" :
                                                          p?.set_total_score >= 2.75 ? "Satisfactory" :
                                                          p?.set_total_score >= 2.50 ? "Fair" :
                                                          p?.set_total_score >= 0 ? "Poor" : "",
                          };

            return Ok(join);
        }
        #endregion


        private async Task<IActionResult> SaveSPPhotos(SPPhoto model, bool? api)
        {
            //var record = db.SPPhoto.AsNoTracking().FirstOrDefault(x => x.UniqueName == model.UniqueName);
            var record = db.SPPhoto.AsNoTracking().FirstOrDefault(x => x.Id == model.Id && x.is_deleted != true);

            if (record == null)
            {
                //4.1 commenting this section:

                //var path01 = PlatformServices.Default.Application.ApplicationBasePath;
                //string savePath = path01 + @"\wwwroot\SPPhotos\" + model.UniqueName.ToString() + ".jpeg";
                //string sub_project = db.sub_project.Find(model.sub_project_unique_id).region_code.ToString();
               
                //if (sub_project.Length == 8)
                //{
                //    sub_project = "0" + sub_project;
                //}   

                //string url = @"http://geotagging.dswd.gov.ph/" + "SPPhotos/thumbnails/" + sub_project + "/" + model.UniqueName + ".jpg";
                //try
                //{
                //    using (var client = new HttpClient())
                //    using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                //    using (
                //        Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                //        stream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
                //    {
                //        await contentStream.CopyToAsync(stream);
                //    }
                //}
                //catch
                //{

                //}

                //4.1 adding this section:
                if (api == true) {
                    model.Id = model.Id;
                    model.UniqueName = model.UniqueName;
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


        //try:
        //[Route("api/offline/get_spid")]
        //public int get_spid(Guid? sp_unique_id)
        //{
        //    return db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == sp_unique_id).sub_project_id;
        //}



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
            var record = db.sub_project_spcf.AsNoTracking().FirstOrDefault(x => x.sub_project_spcf_id == model.sub_project_spcf_id && x.is_deleted != true);

            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else {
                    model.push_status_id = 1;
                }
                
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
                

                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;               

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

        #region 4.0 updates

        #region SP deskapp others
        //Get the values for Other fields saved on sp_deskapp_others
        [Route("api/offline/v1/sub_project/others/get")]
        public IActionResult GetSPOtherDetails(Guid id)
        {
            var model = db.sp_deskapp_others.Select(sp_deskapp_others_DTO.SELECT).Where(x => x.sub_project_unique_id == id && x.is_deleted != true);
            return Ok(model);
        }

        [Route("api/offline/v1/spi/others/get")]
        public IActionResult GetSpiOthers(Guid id)
        {
            var model = db.sp_deskapp_others.FirstOrDefault(x => x.sub_project_unique_id == id && x.is_deleted != true);

            if (model == null)
            {
                var item = new sp_deskapp_others();

                item.sp_deskapp_others_id = id;
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

        //Save inputted values to sp_deskapp_others table
        [Route("api/offline/v1/sub_projects/others/save")]
        public async Task<IActionResult> SaveSpOtherDetails(sp_deskapp_others model, bool? api)
        {
            var record = db.sp_deskapp_others.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id && x.is_deleted != true);

            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else
                {
                    model.push_status_id = 1;
                }

                db.sp_deskapp_others.Add(model);

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
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;

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

        #region SPI land acquisition
        //Get the values for safeguard fields saved on spi_land_acquisition
        [Route("api/sub_project/get/land_acquisition")]
        public IActionResult GetSPLandAcquisition(int id)
        {
            var model = db.spi_land_acquisition.Select(spi_land_acquisitionDTO.SELECT).Where(x => x.sub_project_id == id && x.is_deleted != true);
            return Ok(model);
        }

        [Route("api/offline/v1/spi/land_acquisition/get")]
        public IActionResult GetSpiLandAcquisition(int id)
        {
            var model = db.spi_land_acquisition.FirstOrDefault(x => x.sub_project_id == id && x.is_deleted != true);

            //if (model == null)
            //{
            //    var item = new spi_land_acquisition();

            //    item.spi_land_acquisition_id = id;
            //    item.created_by = "0";
            //    item.created_date = DateTime.Now;
            //    item.is_deleted = false;

            //    model = item;
            //}
            //else
            //{
                
            //}

            return Ok(model);
        }

        //Save inputted values to spi_land_acquisition table
        [Route("api/offline/v1/sub_projects/spi_land_acquisition/save")]
        public async Task<IActionResult> SaveSPLandAcquisition(spi_land_acquisition model, bool? api)
        {
            var record = db.spi_land_acquisition.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id && x.spi_land_acquisition_id == model.spi_land_acquisition_id && x.is_deleted != true);
           
            if (record == null)
            {
                if (api != true)
                {
                    //model.push_status_id = 2;
                    //model.push_date = null;
                    //model.approval_id = 3;
                    model.created_by = "0";
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else
                {
                    model.spi_land_acquisition_id = model.spi_land_acquisition_id;
                }

                db.spi_land_acquisition.Add(model);

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
                //if (api != true)
                //{
                //    model.push_status_id = 3;
                //    model.approval_id = 3;
                //    model.push_date = null;
                //    model.last_modified_by = 0;
                //    model.last_modified_date = DateTime.Now;
                //}

                model.spi_land_acquisition_id = record.spi_land_acquisition_id;
                model.created_by = record.created_by;
                model.created_date = record.created_date;

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

        #region Other Barangays
        //Get the list of Other Barangays and display it on View:
        [Route("api/subproject/get/spi_ancestral_domain")]
        public IActionResult GetOtherBrgys(int id)
        {
            var model = db.spi_ancestral_domain.Select(spi_ancestral_domainDTO.SELECT).Where(x => x.sub_project_id == id && x.is_deleted != true);
            return Ok(model);
        }

        [Route("api/offline/v1/sub_projects/spi_ancestral_domain/save")]
        public async Task<IActionResult> SaveOtherBrgys(spi_ancestral_domain model, bool? api)
        {
            var record = db.spi_ancestral_domain.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id && x.spi_ancestral_domain_id == model.spi_ancestral_domain_id && x.is_deleted != true);

            if (record == null)
            {
                if (api != true)
                {
                    model.created_by = "0";
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else
                {
                    model.spi_ancestral_domain_id = model.spi_ancestral_domain_id;
                }

                db.spi_ancestral_domain.Add(model);

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
                //if (api != true)
                //{
                //    model.push_status_id = 3;
                //    model.approval_id = 3;
                //    model.push_date = null;
                //    model.last_modified_by = 0;
                //    model.last_modified_date = DateTime.Now;
                //}

                model.spi_ancestral_domain_id = record.spi_ancestral_domain_id;
                model.created_by = record.created_by;
                model.created_date = record.created_date;

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

        #region SPI multiple SP
        //Get the list of SPI Multiple SP
        [Route("api/subproject/get/spi_multiple_sp")]
        public IActionResult GetSPIMultipleSP(int id)
        {
            var model = db.spi_multiple_sp.Select(spi_multiple_spDTO.SELECT).Where(x => x.sub_project_id == id && x.is_deleted != true);
            return Ok(model);
        }

        [Route("api/offline/v1/sub_projects/spi_multiple_sp/save")]
        public async Task<IActionResult> SaveMultipleSP(spi_multiple_sp model, bool? api)
        {
            var record = db.spi_multiple_sp.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id && x.spi_multiple_sp_id == model.spi_multiple_sp_id && x.is_deleted != true);

            if (record == null)
            {
                if (api != true)
                {
                    model.created_by = "0";
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else
                {
                    model.spi_multiple_sp_id = model.spi_multiple_sp_id;
                }

                db.spi_multiple_sp.Add(model);

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
                //if (api != true)
                //{
                //    model.push_status_id = 3;
                //    model.approval_id = 3;
                //    model.push_date = null;
                //    model.last_modified_by = 0;
                //    model.last_modified_date = DateTime.Now;
                //}

                model.spi_multiple_sp_id = record.spi_multiple_sp_id;
                model.created_by = record.created_by;
                model.created_date = record.created_date;

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

        #region BTF
        //Get the list of BTF
        [Route("api/btf/get/tranche/releases")]
        public IActionResult GetBTF(int id)
        {
            var model = db.barangay_trasnfer_of_funds.Select(btf_DTO.SELECT).Where(x => x.erfr_project_id == id && x.is_deleted != true);
            return Ok(model);
        }
        
        [Route("api/offline/v1/sub_projects/btf/save")]
        public async Task<IActionResult> SaveBTF(barangay_trasnfer_of_funds model, bool? api)
        {
            var record = db.barangay_trasnfer_of_funds.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id && x.id == model.id && x.is_deleted != true);

            if (record == null)
            {
                if (api != true)
                {
                    model.created_by = "0";
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else
                {
                    model.id = model.id;
                }

                db.barangay_trasnfer_of_funds.Add(model);

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
                //if (api != true)
                //{
                //    model.push_status_id = 3;
                //    model.approval_id = 3;
                //    model.push_date = null;
                //    model.last_modified_by = 0;
                //    model.last_modified_date = DateTime.Now;
                //}

                model.id = record.id;
                model.created_by = record.created_by;
                model.created_date = record.created_date;

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

        #region SPI multiple variation date
        //Get the list of Multiple Variation Date
        [Route("api/subproject/get/spi_multiple_variation_date")]
        public IActionResult GetSPIMultipleVariationDate(int id)
        {
            var model = db.spi_multiple_variation_date.Select(spi_multiple_variation_date_DTO.SELECT).Where(x => x.sub_project_id == id && x.is_deleted != true);
            return Ok(model);
        }

        [Route("api/offline/v1/sub_projects/spi_multiple_variation_date/save")]
        public async Task<IActionResult> SaveMultipleVariationDate(spi_multiple_variation_date model, bool? api)
        {
            var record = db.spi_multiple_variation_date.AsNoTracking().FirstOrDefault(x => x.sub_project_id == model.sub_project_id && x.id == model.id && x.is_deleted != true);

            if (record == null)
            {
                if (api != true)
                {
                    model.created_by = "0";
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                }
                else
                {
                    model.id = model.id;
                }

                db.spi_multiple_variation_date.Add(model);

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
                //if (api != true)
                //{
                //    model.push_status_id = 3;
                //    model.approval_id = 3;
                //    model.push_date = null;
                //    model.last_modified_by = 0;
                //    model.last_modified_date = DateTime.Now;
                //}

                model.id = record.id;
                model.created_by = record.created_by;
                model.created_date = record.created_date;

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
            var record = db.sub_project_set.AsNoTracking().FirstOrDefault(x => x.sub_project_set_id == model.sub_project_set_id && x.is_deleted != true);
            var main_sp_record = db.sub_project.FirstOrDefault(x => x.sub_project_id == model.sub_project_id);

            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;

                    main_sp_record.push_status_id = 3;
                    db.Entry(main_sp_record).State = EntityState.Modified;
                }
                else
                {
                    model.push_status_id = 1;
                }
                
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
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    main_sp_record.push_status_id = 3;
                    db.Entry(main_sp_record).State = EntityState.Modified;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;               

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
            var main_sp_record = db.sub_project.FirstOrDefault(x => x.sub_project_id == record.sub_project_id);

            record.is_deleted = true;
            record.push_status_id = 3;
            main_sp_record.push_status_id = 3;
            await db.SaveChangesAsync();
            return Ok();
        }

        #endregion Multiple SET

        #region ERS
        [Route("api/offline/v1/sub_project/ers/get")]
        public IActionResult GetERS(Guid id)
        {
            var model = db.sub_project_ers.Select(sub_project_ersDTO.SELECT).Where(x => x.sub_project_unique_id == id && x.is_deleted != true);
            return Ok(model);
        }

        //v3.2 get floating ers with the same brgy and cycle as the SP
        [Route("api/offline/v1/sub_project/ers/get_floating_ers")]
        public IActionResult GetFloatingERS(int brgy_code, int cycle_id)
        {
            if (brgy_code.ToString().Length == 8)
            {
                var floating_ers = db.sub_project_ers.Where(x => (x.brgy_code == "0" + brgy_code || x.brgy_code == "" + brgy_code) && x.cycle_id == cycle_id && x.sub_project_id == null && x.sub_project_unique_id == null && x.is_deleted != true);
                return Ok(floating_ers);
            }
            else if (brgy_code.ToString().Length == 9)
            {                
                var converted_brgy_code = brgy_code.ToString();
                var floating_ers = db.sub_project_ers.Where(x => (x.brgy_code == "" + brgy_code || x.brgy_code == converted_brgy_code.Substring(1)) && x.cycle_id == cycle_id && x.sub_project_id == null && x.sub_project_unique_id == null && x.is_deleted != true);
                return Ok(floating_ers);
            }
            else {
                return null;
            }


        }

        //v3.2 match the checked floating ers to SP
        [Route("api/offline/v1/sub_project/ers/match_floating_ers")]
        public async Task<IActionResult> MatchFloatingERS(Guid id, int spid, Guid sp_unique_id)
        {
            var model = db.sub_project_ers.FirstOrDefault(x => x.sub_project_ers_id == id);

            model.sub_project_id = spid;
            model.sub_project_unique_id = sp_unique_id;

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


        [Route("api/offline/v1/sub_projects/ers/save")]
        public async Task<IActionResult> SaveERS(sub_project_ers model, bool? api)
        {
            var record = db.sub_project_ers.AsNoTracking().FirstOrDefault(x => x.sub_project_ers_id == model.sub_project_ers_id && x.is_deleted != true);
            var main_sp_record = db.sub_project.FirstOrDefault(x => x.sub_project_id == model.sub_project_id);

            if (record == null)
            {
                if (api != true)
                {
                    //saved using the save button
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;

                    main_sp_record.push_status_id = 3;
                    db.Entry(main_sp_record).State = EntityState.Modified;
                }
                else {
                    //saved using sync download because api is true in sync/get
                    model.push_status_id = 1;
                }
                
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
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    main_sp_record.push_status_id = 3;
                    db.Entry(main_sp_record).State = EntityState.Modified;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;

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
        [Route("api/offline/v1/sub_projects/ers/worker/save")]
        public async Task<IActionResult> SaveERSWorker(person_ers_work model, bool? api)
        {
            var record = db.person_ers_work.AsNoTracking().FirstOrDefault(x => x.person_ers_work_id == model.person_ers_work_id && x.is_deleted != true);
            var ers_record = db.sub_project_ers.FirstOrDefault(x => x.sub_project_ers_id == model.sub_project_ers_id);
            var main_sp_record = db.sub_project.FirstOrDefault(x => x.sub_project_id == ers_record.sub_project_id);
            var person_profile_record = db.person_profile.FirstOrDefault(x => x.person_profile_id == model.person_profile_id);
            
            //if saving a new record:
            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;

                    main_sp_record.push_status_id = 3;
                    person_profile_record.is_worker = true;
                    person_profile_record.push_status_id = 3;
                }
                else
                {
                    //saved using sync download because api is true in sync/get
                    model.push_status_id = 1;
                }

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

            }

            //else, saving existing record based on person_ers_work_id
            else
            {
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;
                    model.push_date = null;

                    main_sp_record.push_status_id = 3;
                }
                
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

        //save for EDIT record:    
        [Route("api/offline/v1/sub_projects/ers/edit_worker/save")]
        public async Task<IActionResult> SaveEditedERSWorker(person_ers_work model, bool? api)
        {
            var record = db.person_ers_work.AsNoTracking().FirstOrDefault(x => x.person_profile_id == model.person_profile_id && x.sub_project_ers_id == model.sub_project_ers_id && x.is_deleted != true);
            var ers_record = db.sub_project_ers.FirstOrDefault(x => x.sub_project_ers_id == record.sub_project_ers_id);
            var main_sp_record = db.sub_project.FirstOrDefault(x => x.sub_project_id == ers_record.sub_project_id);

            if (api != true)
            {
                model.approval_id = 3;
            }

            model.person_ers_work_id = record.person_ers_work_id;
            model.created_by = record.created_by;
            model.created_date = record.created_date;
            model.last_modified_by = 0;
            model.last_modified_date = DateTime.Now;

            main_sp_record.push_status_id = 3;

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

            if (item.mode_id != null)
            {
                model = model.Where(m => m.mode_id == item.mode_id);
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

        //v3.2 fix for ERS records with 0 region code or 0 sub_project_id
        [Route("api/offline/v1/sub_projects/update_zero_values")]
        public async Task<IActionResult> UpdateZeroValues()
        {
            var zero_spid = db.sub_project_ers.Where(s => s.sub_project_id == 0 || s.sub_project_id == null);
            var zero_unique_id = db.sub_project_ers.Where(s => s.sub_project_unique_id == null);
            var zero_region_code = db.sub_project_ers.Where(s => s.region_code == 0 || s.region_code == null);

            foreach (var r in zero_spid)
            {
                if (r.sub_project_unique_id != null)
                {
                    var sp = db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == r.sub_project_unique_id);
                    if (sp != null)
                    {
                        r.sub_project_id = sp.sub_project_id;
                    }
                    else {

                    }                 
                }
            }

            foreach (var r in zero_unique_id)
            {
                if (r.sub_project_id != null && r.sub_project_id != 0)
                {
                    var sp = db.sub_project.FirstOrDefault(x => x.sub_project_id == r.sub_project_id);
                    if (sp != null)
                    {
                        r.sub_project_unique_id = sp.sub_project_unique_id;
                    }
                    else {

                    }                    
                }
            }

            foreach (var r in zero_region_code)
            {
                if ((r.sub_project_id != 0 && r.sub_project_id != null) && r.sub_project_unique_id != null)
                {
                    var sp = db.sub_project.FirstOrDefault(x => x.sub_project_id == r.sub_project_id);
                    if (sp != null)
                    {
                        r.region_code = sp.region_code;
                    }
                    else {

                    }                    
                }
                else if ((r.sub_project_id != 0 && r.sub_project_id != null) && r.sub_project_unique_id == null)
                {
                    var sp = db.sub_project.FirstOrDefault(x => x.sub_project_id == r.sub_project_id);
                    if (sp != null)
                    {
                        r.region_code = sp.region_code;
                    }
                    else {

                    }
                }
                else if (r.sub_project_unique_id != null && (r.sub_project_id == 0 || r.sub_project_id == null))
                {
                    var sp = db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == r.sub_project_unique_id);
                    if (sp != null)
                    {
                        r.region_code = sp.region_code;
                    }
                    else {

                    }
                }
            }

            await db.SaveChangesAsync();
            return Ok();
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
            var record = db.sub_project.AsNoTracking().FirstOrDefault(x => x.sub_project_unique_id == model.sub_project_unique_id && x.sub_project_id == model.sub_project_id && x.IsActive == true);
            
            if (record == null)
            {                
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.created_by = "";
                    model.created_date = DateTime.Now;
                    model.IsActive = true;
                }
                else
                {
                    model.push_status_id = 1;
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
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.last_updated_by = "";
                    model.last_updated_date = DateTime.Now;
                }

                model.created_by = record.created_by;
                model.created_date = record.created_date;

                //v3.1 temporarily open cycle for editing. So when cycle is changed thru sync download, cycle in ERS should also be updated
                //commented on 4.2
                //var ers_record = db.sub_project_ers.Where(x => x.sub_project_id == model.sub_project_id && x.is_deleted != true);
                //foreach (var ers in ers_record.ToList())
                //{
                //    ers.cycle_id = model.cycle_id;
                //}

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
        public async Task<IActionResult> SavetoReferenceTable(sub_project_reference_table ref_model, bool? api)
        {
            var record = db.sub_project_reference_table.AsNoTracking().FirstOrDefault(x => x.sub_project_id == ref_model.sub_project_id);

            if (record == null)
            {
                //because api is set to TRUE in sync/get
                if (api == true)
                {
                    ref_model.push_status_id = 1;
                    ref_model.IsActive = true;
                    ref_model.is_paired = false;
                }

                db.sub_project_reference_table.Add(ref_model);

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
                ref_model.created_by = record.created_by;
                ref_model.created_date = record.created_date;
                ref_model.is_paired = record.is_paired;
                ref_model.paired_sp_unique_id = record.paired_sp_unique_id;

                db.Entry(ref_model).State = EntityState.Modified;

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
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                //10-16-18 v4.3
                //To solve issue on "A task was canceled" error because of occassional timeout due to bandwidth issue (when downloading too many SPs)
                client.Timeout = TimeSpan.FromMinutes(10);

                HttpResponseMessage response = client.GetAsync("api/offline/v1/geo/projects/get_by_city_code?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<sub_project>>(responseJson.Result);
                    var ref_model = JsonConvert.DeserializeObject<List<sub_project_reference_table>>(responseJson.Result);

                    foreach (var item in model.ToList())
                    {
                        record_id = item.sub_project_id;
                        await Save(item, true);

                        GetOnlineSPCF(username, password, city_code, record_id);
                        GetOnlineSET(username, password, city_code, record_id);
                        GetOnlineERSList(username, password, city_code, record_id);
                        GetOnlineCoverage(username, password, city_code, record_id);
                        GetOnlineSPDeskAppOthers(username, password, record_id);
                        GetOnlineSPILandAcquisition(username, password, record_id);
                        GetOnlineSPIMultipleSP(username, password, record_id);
                        GetOnlineSPIMultipleVariationDate(username, password, record_id);
                        GetOnlineOtherBrgys(username, password, city_code, record_id);
                    }

                    foreach (var item in ref_model.ToList())
                    {
                        await SavetoReferenceTable(item, true);
                    }

                    GetOnlineBTF(username, password, city_code); //no values on geotagging for sub_project_id column, so city_code will be the reference
                    GetOnlinePhotos(username, password, city_code, record_id); //will use the city_code as the reference

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
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/ers/mapping/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<sub_project_ers>>(responseJson.Result);

                    foreach (var item in all.ToList())
                    {
                        Guid? ers_record_id = item.sub_project_ers_id;
                        await SaveERS(item, true);
                        await GetOnlineERSWorkers(username, password, city_code, ers_record_id);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public async Task<bool> GetOnlineERSWorkers(string username, string password, string city_code, Guid? ers_record_id)
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/ers/workers/get_mapped?city_code=" + city_code + "&id=" + ers_record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<person_ers_work>>(responseJson.Result);
                    
                    foreach (var item in all.ToList())
                    {
                        await SaveERSWorker(item, true);
                        //await SaveERSWorker(item, true);
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

        public async Task<bool> GetOnlinePhotos(string username, string password, string city_code, int? record_id)
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
                
                HttpResponseMessage response = client.GetAsync("api/offline/v1/geo/projects/photos/get_by_city_code?city_code=" + city_code + "&id=").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<SPPhoto>>(responseJson.Result);
                    
                    foreach (var item in all.ToList())
                    {
                        //SaveSPPhotos(item, true);
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
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);
                
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
                            //4.3 revert item.id = record.id to original code
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
                        //SaveSET(item, true);
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
        public async Task<bool>  GetOnlineSPCF(string username, string password, string city_code = null, int? record_id = null)
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
                        //SaveSPCF(item, true);
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
                    var ref_model = JsonConvert.DeserializeObject<List<sub_project_reference_table>>(responseJson.Result); 
                    
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
            model.sub_project_id = sp_reference.sub_project_id;
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

        #region 4.0 Additional SYNC/GET for new tables
        //SP DeskApp Others
        public async Task<bool> GetOnlineSPDeskAppOthers(string username, string password, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/sp_deskapp_others/mapping/get_mapped?id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    //---------4.2 change: to resolve issue on cannot deserialize json object to json array
                    //---------4.3 revert back to original code, adjustment done on GetOnlineCoverage

                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<sp_deskapp_others>>(responseJson.Result); //-- ORIGINAL CODE
                    //var all = JsonConvert.DeserializeObject<sp_deskapp_others>(responseJson.Result);

                    if (all.ToList() != null)
                    {
                        //-- ORIGINAL CODE
                        foreach (var item in all.ToList())
                        {
                            await SaveSpOtherDetails(item, true);
                        }
                    }
                    //await SaveSpOtherDetails(all, true);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //SPI Land Acquisition
        public async Task<bool> GetOnlineSPILandAcquisition(string username, string password, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/spi_land_acquisition/mapping/get_mapped?id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<spi_land_acquisition>>(responseJson.Result);
                    foreach (var item in all.ToList())
                    {
                        //SaveSPLandAcquisition(item, true);
                        await SaveSPLandAcquisition(item, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //GetOnlineOtherBrgys
        public async Task<bool> GetOnlineOtherBrgys(string username, string password, string city_code = null, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/spi_ancestral_domain/mapping/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<spi_ancestral_domain>>(responseJson.Result);
                    foreach (var item in all.ToList())
                    {
                        //SaveOtherBrgys(item, true);
                        await SaveOtherBrgys(item, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //GetOnlineSPIMultipleSP
        public async Task<bool> GetOnlineSPIMultipleSP(string username, string password, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/spi_multiple_sp/mapping/get_mapped?id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<spi_multiple_sp>>(responseJson.Result);
                    foreach (var item in all.ToList())
                    {
                        //SaveMultipleSP(item, true);
                        await SaveMultipleSP(item, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //GetOnlineBTF
        public async Task<bool> GetOnlineBTF(string username, string password, string city_code = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/barangay_trasnfer_of_funds/mapping/get_mapped?city_code=" + city_code + "&id=").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<barangay_trasnfer_of_funds>>(responseJson.Result);
                    foreach (var item in all.ToList())
                    {
                        //SaveBTF(item, true);
                        await SaveBTF(item, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //GetOnlineSPIMultipleVariationDate
        public async Task<bool> GetOnlineSPIMultipleVariationDate(string username, string password, int? record_id = null)
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/spi/spi_multiple_variation_date/mapping/get_mapped?id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<spi_multiple_variation_date>>(responseJson.Result);
                    foreach (var item in all.ToList())
                    {
                        //SaveMultipleVariationDate(item, true);
                        await SaveMultipleVariationDate(item, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion


        //old code: version 3.3 to lower version
        //[HttpPost]
        //[Route("Sync/Post/sub_project")]
        //public async Task<ActionResult> PostOnline(string username, string password, int? record_id = null)
        //{
        //    string token = username + ":" + password;
        //    byte[] toBytes = Encoding.ASCII.GetBytes(token);
        //    string key = Convert.ToBase64String(toBytes);

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

        //        var items_preselected = db.sub_project.Where(x => x.push_status_id == 5).ToList();

        //        if (!items_preselected.Any())
        //        {
        //            var items = db.sub_project.Where(x => x.push_status_id == 2 || x.push_status_id == 3);

        //            foreach (var item in items.ToList())
        //            {
        //                StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        //                HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/save", data).Result;
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    item.push_status_id = 1;
        //                    record_id = item.sub_project_id;
        //                    PostOnlineERS(username, password, record_id);
        //                    PostOnlineSet(username, password, record_id);
        //                    PostOnlineSPCF(username, password, record_id);

        //                    db.SaveChangesAsync();
        //                }
        //                else
        //                {
        //                    item.push_status_id = 4;
        //                    await db.SaveChangesAsync();
        //                    //return BadRequest();
        //                }
        //            }
        //        }
        //        else {
        //            var items = db.sub_project.Where(x => x.push_status_id == 5);

        //            foreach (var item in items.ToList())
        //            {
        //                StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        //                HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/save", data).Result;

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    item.push_status_id = 1;
        //                    record_id = item.sub_project_id;
        //                    PostOnlineERS(username, password, record_id);
        //                    PostOnlineSet(username, password, record_id);
        //                    PostOnlineSPCF(username, password, record_id);
        //                    db.SaveChangesAsync();
        //                }
        //                else
        //                {
        //                    item.push_status_id = 4;
        //                    await db.SaveChangesAsync();
        //                    //return BadRequest();
        //                }
        //            }
        //        }               

        //    }
        //    //ALL AWAITS ARE MOVED INSIDE FOREACH: v3.1
        //    //await PostOnlineERS(username, password, record_id);
        //    //await PostOnlineERSWorker(username, password, record_id);
        //    //await PostOnlineSet(username, password, record_id);
        //    //await PostOnlineSPCF(username, password, record_id);
        //    return Ok();
        //}

        #region 4.0 NEW CODE FOR SYNC UPLOAD. Details to upload are the fields only encoded in DeskApp
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
                    var items = db.sub_project.Where(x => x.push_status_id == 2 || x.push_status_id == 3);

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/save", data).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            record_id = item.sub_project_id;
                            PostOnlineSPDeskappOthers(username, password, record_id);
                            PostOnlineSPCF(username, password, record_id);
                            PostOnlineSet(username, password, record_id);
                            //PostOnlineERS(username, password, record_id); //v4.3                                           
                            
                            db.SaveChangesAsync();
                        }
                        else
                        {
                            item.push_status_id = 4;
                            await db.SaveChangesAsync();
                        }
                    }
                }

                else
                {
                    var items = db.sub_project.Where(x => x.push_status_id == 5);

                    foreach (var item in items.ToList())
                    {
                        StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("api/offline/v1/sub_project/save", data).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            item.push_status_id = 1;
                            record_id = item.sub_project_id;
                            PostOnlineSPDeskappOthers(username, password, record_id);
                            PostOnlineSPCF(username, password, record_id);
                            PostOnlineSet(username, password, record_id);
                            //PostOnlineERS(username, password, record_id); //v4.3                           
                            
                            db.SaveChangesAsync();
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
        #endregion
        
        public async Task<bool> PostOnlineERS(string username, string password, int? record_id)
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
                
                //var items = db.sub_project_ers.Where(x => x.sub_project_id == record_id || x.is_deleted == true);
                var items = db.sub_project_ers.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1));

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/spi/ers/list/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        Guid? ers_id = item.sub_project_ers_id;
                        //PostOnlineERSWorker(username, password, ers_id);
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

        public async Task<bool> PostOnlineERSWorker(string username, string password, int? record_id)
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

                //var items = db.person_ers_work.Where(x => x.sub_project_ers_id == record_id || x.is_deleted == true);
                var items = db.person_ers_work.Where(x => x.push_status_id != 1 || (x.is_deleted == true && x.push_status_id != 1));

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/person/ers/save", data).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        item.push_status_id = 1;
                        item.push_date = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.push_status_id = 4;
                        await db.SaveChangesAsync();
                        //return false;
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
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);
                
                var items = db.sub_project_set.Where(x => x.sub_project_id == record_id || x.is_deleted == true);
                
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
                    else
                    {
                        return false;
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
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                var items = db.sub_project_spcf.Where(x => x.sub_project_id == record_id || x.is_deleted == true);
                
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
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        #region 4.0 Post APIs for fields encoded in DeskApp
        public async Task<bool> PostOnlineSPDeskappOthers(string username, string password, int? record_id = null)
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

                var items = db.sp_deskapp_others.Where(x => x.sub_project_id == record_id || x.is_deleted == true);

                foreach (var item in items.ToList())
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("api/offline/v1/sp_deskapp_others/save", data).Result;

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
        #endregion



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

        
        //For Developer's use: 07-03-18
        [Route("api/offline/v1/sub_project/update_sp_deskapp_others")]
        public async Task<IActionResult> UpdateSPOthers()
        {
            var sp_model = db.sub_project.Where(x => x.IsActive == true);            
            
            foreach (var item in sp_model.ToList())
            {
                //await SaveUpdateToSPOthers(item);
                var record_in_sp_others = db.sp_deskapp_others.AsNoTracking().FirstOrDefault(x => x.sub_project_id == item.sub_project_id && x.sub_project_unique_id == item.sub_project_unique_id);

                if (record_in_sp_others == null)
                {
                    var model = new sp_deskapp_others();

                    model.sub_project_id = item.sub_project_id;
                    model.sub_project_unique_id = item.sub_project_unique_id;

                    model.no_target_families = item.no_target_families;
                    model.no_target_pantawid_households = item.no_target_pantawid_households;
                    model.no_target_pantawid_families = item.no_target_pantawid_families;
                    model.no_target_slp_households = item.no_target_slp_households;
                    model.no_target_slp_families = item.no_target_slp_families;
                    model.no_target_ip_households = item.no_target_ip_households;
                    model.no_target_ip_families = item.no_target_ip_families;
                    model.no_target_male = item.no_target_male;
                    model.no_target_female = item.no_target_female;
                    model.target_male = item.target_male;
                    model.target_female = item.target_female;

                    model.suspension_order_list = item.suspension_order_list;
                    model.resume_order_list = item.resume_order_list;
                    model.variation_order_list = item.variation_order_list;
                    model.community_formation_list = item.community_formation_list;

                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                    model.push_status_id = 2;
                    model.approval_id = 3;

                    db.sp_deskapp_others.Add(model);

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    record_in_sp_others.last_modified_date = DateTime.Now;
                    record_in_sp_others.push_status_id = 3;

                    db.Entry(record_in_sp_others).State = EntityState.Modified;

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return BadRequest();
                    }
                }
            }

            return Ok();
        }
        
    }
}