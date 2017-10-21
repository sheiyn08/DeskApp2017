using DeskApp.Data;
using DeskApp.DataLayer;
using DeskApp.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.Controllers
{
    public class MovController : Controller
    {

        private readonly ApplicationDbContext db;

        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing

        public MovController(ApplicationDbContext context)
        {
            db = context;

        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("api/offline/v1/attachments/get_dto")]
        public IActionResult GetData(AngularFilterModel item)
        {
            var model = db.attached_document
                .Include(x => x.mov_list)
                .Where(x => x.is_deleted != true);


            if (item.table_name_id != null)
            { model = model.Where(x => x.mov_list.table_name_id == item.table_name_id); }

            if(item.mov_list_id != null)
            {
                model = model.Where(x => x.mov_list_id == item.mov_list_id);

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


            var totalCount = model.Count();

            int currPages = item.currPage ?? 0;
            int size = item.pageSize ?? 10;



            return Ok (new PagedCollection<dynamic>()
            {
                Page = currPages,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / size),

                TotalSync = model.Where(x => x.push_status_id != 1 && !(x.push_status_id == 2 && x.is_deleted == true)).Count(),

                TotalUnAuthorized = model.Where(x => x.push_status_id == 4 && x.is_deleted != true).Count(),
                Items = model.Select
                (s => new {
                    s.attached_document_id,
                    lib_region_region_name = db.lib_region.FirstOrDefault(x => x.region_code == s.region_code).region_name,
                    lib_province_prov_name = db.lib_province.FirstOrDefault(x => x.prov_code == s.prov_code).prov_name,

                    lib_city_city_name = db.lib_city.FirstOrDefault(x => x.city_code == s.city_code).city_name,
                    lib_brgy_brgy_name = s.brgy_code == null ? "" : db.lib_brgy.FirstOrDefault(x => x.brgy_code == s.brgy_code).brgy_name,


                    lib_fund_source_name = db.lib_fund_source.FirstOrDefault(x => x.fund_source_id == s.fund_source_id).name,
                    lib_cycle_name = s.cycle_id == null ? "" : db.lib_cycle.FirstOrDefault(x => x.cycle_id == s.cycle_id).name,


                    lib_enrollment_name = db.lib_enrollment.FirstOrDefault(x => x.enrollment_id == s.enrollment_id).name,
                    
                    module = db.table_name.FirstOrDefault(x => x.table_name_id == s.mov_list.table_name_id).name,

                    attachment = s.mov_list.name,

                    s.record_id,


  
                }).OrderBy(x => x.attached_document_id).Skip(currPages * size).Take(size).ToList()



            });

        }



        [HttpPost]
        public IActionResult UploadFilesAjax(Guid id, int mov_list_id, int region_code, int prov_code, int city_code,  int? fund_source_id, int? cycle_id, int? brgy_code, int? enrollment_id)
        {
            long size = 0;
            var files = Request.Form.Files;
            string module_name = "";
            string attachment_type = "";
            foreach (var file in files)
            {
                if(files.Count() != 1)
                {
                    return BadRequest("Select 1 File to upload only");
                }

                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');


                if (!filename.Contains("pdf"))
                {
                    return BadRequest("Upload PDF Files Only");
                }

                Guid attached_document_id = Guid.NewGuid();     
                var path01 = PlatformServices.Default.Application.ApplicationBasePath;
                

                //Barangay Profile attachments:
                if (mov_list_id == 3)
                {
                    module_name = "BP_";
                    attachment_type = "BrgyProfileForm_";
                }

                //Municipal Profile attachments:
                if (mov_list_id == 2)
                {
                    module_name = "MP_";
                    attachment_type = "MunicipalProfileForm_";
                }

                //Brgy Assembly attachments:
                if (mov_list_id == 4)
                {
                    module_name = "BA_";
                    attachment_type = "BrgyActivityMinutesForm_";
                }
                if (mov_list_id == 5)
                {
                    module_name = "BA_";
                    attachment_type = "BAHouseholdParticipation_";
                }
                if (mov_list_id == 6)
                {
                    module_name = "BA_";
                    attachment_type = "BAAttendanceSheet_";
                }
                if (mov_list_id == 2)
                {
                    module_name = "BA_";
                    attachment_type = "BrgyResolution_";
                }

                //Person Profile attachments:
                if (mov_list_id == 14)
                {
                    module_name = "PP_";
                    attachment_type = "CommunityVolunteersProfile_";
                }
                if (mov_list_id == 51)
                {
                    module_name = "PP_";
                    attachment_type = "CDDSPWorkerBasicProfile_";
                }

                //Trainings attachments:
                if (mov_list_id == 7)
                {
                    module_name = "TR_";
                    attachment_type = "MunicipalActivityMinutesForm_";
                }
                if (mov_list_id == 8)
                {
                    module_name = "TR_";
                    attachment_type = "MunicipalAttendanceSheet_";
                }
                if (mov_list_id == 9)
                {
                    module_name = "TR_";
                    attachment_type = "BrgyActivityMinutesForm_";
                }
                if (mov_list_id == 10)
                {
                    module_name = "TR_";
                    attachment_type = "BrgyAttendanceSheet_";
                }
                if (mov_list_id == 11)
                {
                    module_name = "TR_";
                    attachment_type = "BrgyActionPlan_";
                }
                if (mov_list_id == 12)
                {
                    module_name = "TR_";
                    attachment_type = "BrgyResolution_";
                }
                if (mov_list_id == 13)
                {
                    module_name = "TR_";
                    attachment_type = "OtherDocuments_";
                }
                if (mov_list_id == 19)
                {
                    module_name = "TR_";
                    attachment_type = "MIBFMunicipalResolution_";
                }

                //Grievance attachments:
                if (mov_list_id == 22)
                {
                    module_name = "GR_";
                    attachment_type = "GRSIntakeForm_";
                }

                //GRS Installation attachments:
                if (mov_list_id == 20)
                {
                    module_name = "GI_";
                    attachment_type = "GRSInstallationChecklistMunicipal_";
                }
                if (mov_list_id == 21)
                {
                    module_name = "GI_";
                    attachment_type = "GRSInstallationChecklistBrgy_";
                }

                //SubProject attachments:
                if (mov_list_id == 24)
                {
                    module_name = "SP_";
                    attachment_type = "BrgySPWorkSchedandPhysicalProgressReport_";
                }
                if (mov_list_id == 25)
                {
                    module_name = "SP_";
                    attachment_type = "SuspensionOrder_";
                }
                if (mov_list_id == 26)
                {
                    module_name = "SP_";
                    attachment_type = "ResumptionOrder_";
                }
                if (mov_list_id == 27)
                {
                    module_name = "SP_";
                    attachment_type = "VariationOrder_";
                }
                if (mov_list_id == 28)
                {
                    module_name = "SP_";
                    attachment_type = "TargetHouseholdBeneficiaries_";
                }
                if (mov_list_id == 29)
                {
                    module_name = "SP_";
                    attachment_type = "CNC_";
                }
                if (mov_list_id == 30)
                {
                    module_name = "SP_";
                    attachment_type = "CNO_";
                }
                if (mov_list_id == 31)
                {
                    module_name = "SP_";
                    attachment_type = "CP_";
                }
                if (mov_list_id == 32)
                {
                    module_name = "SP_";
                    attachment_type = "UsufructAgreement_";
                }
                if (mov_list_id == 33)
                {
                    module_name = "SP_";
                    attachment_type = "BLGUResolution_";
                }
                if (mov_list_id == 34)
                {
                    module_name = "SP_";
                    attachment_type = "DepEdCertification_";
                }
                if (mov_list_id == 35)
                {
                    module_name = "SP_";
                    attachment_type = "EmploymentRecordSheet_";
                }
                if (mov_list_id == 36)
                {
                    module_name = "SP_";
                    attachment_type = "CDDSPWorkerBasicProfile_";
                }
                if (mov_list_id == 37)
                {
                    module_name = "SP_";
                    attachment_type = "SPFundUtilizationReport_";
                }
                if (mov_list_id == 38)
                {
                    module_name = "SP_";
                    attachment_type = "CADT_";
                }
                if (mov_list_id == 39)
                {
                    module_name = "SP_";
                    attachment_type = "RequestforValidationtoNCIP_";
                }
                if (mov_list_id == 40)
                {
                    module_name = "SP_";
                    attachment_type = "Tariff_";
                }
                if (mov_list_id == 41)
                {
                    module_name = "SP_";
                    attachment_type = "SPCompletionReport_";
                }
                if (mov_list_id == 42)
                {
                    module_name = "SP_";
                    attachment_type = "FinalInspectionReport_";
                }
                if (mov_list_id == 43)
                {
                    module_name = "SP_";
                    attachment_type = "CertofCompletionandAcceptance_";
                }
                if (mov_list_id == 44)
                {
                    module_name = "SP_";
                    attachment_type = "FunctionalAuditTool_";
                }
                if (mov_list_id == 45)
                {
                    module_name = "SP_";
                    attachment_type = "ActualHouseholdBeneficiaries_";
                }
                if (mov_list_id == 46)
                {
                    module_name = "SP_";
                    attachment_type = "SustainabilityEvaluationTool_";
                }
                if (mov_list_id == 55)
                {
                    module_name = "SP_";
                    attachment_type = "ESSC_";
                }
                if (mov_list_id == 56)
                {
                    module_name = "SP_";
                    attachment_type = "ESMP_";
                }
                if (mov_list_id == 57)
                {
                    module_name = "SP_";
                    attachment_type = "ECC_";
                }
                
                //MPTA attachments:
                if (mov_list_id == 16)
                {
                    module_name = "PTA_";
                    attachment_type = "PTAIntegrationPlansChecklist_";
                }
                if (mov_list_id == 53)
                {
                    module_name = "PTA_";
                    attachment_type = "BrgyResolution_";
                }
                if (mov_list_id == 54)
                {
                    module_name = "PTA_";
                    attachment_type = "MunicipalResolution_";
                }

                //MLCC attachments:
                if (mov_list_id == 23)
                {
                    module_name = "MLCC_";
                    attachment_type = "MunicipalConsolidatedStatusofLCC_";
                }

                //Oversight attachments:
                if (mov_list_id == 15)
                {
                    module_name = "OC_";
                    attachment_type = "OversightandCoordCommitteeChecklist_";
                }

                //Oversight attachments:
                if (mov_list_id == 50)
                {
                    module_name = "CO_";
                    attachment_type = "CommunityOrganizationProfileForm_";
                }

                //Perception Survey attachments:
                if (mov_list_id == 47)
                {
                    module_name = "PS_";
                    attachment_type = "PerceptionSurveyForm_";
                }

                //Municipal Talakayan attachments:
                if (mov_list_id == 49)
                {
                    module_name = "MT_";
                    attachment_type = "MunicipalTalakayanEvaluationForm_";
                }



                string savePath = path01 + @"\wwwroot\MOVs\" + module_name + attachment_type + attached_document_id.ToString() + ".pdf";

                //    filename = @"C:\DeskApp\" + @filename;  // hostingEnv.WebRootPath + $@"\{ filename}";

                size += file.Length;


                using (FileStream fs = System.IO.File.Create(savePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                var model = new attached_document
                {
                    attached_document_id = attached_document_id,
                    record_id = id, //unique_id of the record
                    region_code = region_code,
                    prov_code = prov_code,
                    city_code = city_code,
                    brgy_code = brgy_code,
                    fund_source_id = fund_source_id,
                    cycle_id = cycle_id,
                    enrollment_id = enrollment_id,
                    push_status_id =2,
                    approval_id = 3,
                    created_by =1,
                    created_date = DateTime.Now,
                    mov_list_id = mov_list_id,
                };

                db.attached_document.Add(model);
                
                db.SaveChanges();

                //string message = "File Uploaded Successfully";


                model.mov_list = db.mov_list.Find(mov_list_id);

                return Ok(model);
            }

            return BadRequest();
          
        }


        [Route("api/mov/get/uploaded")]
        public IActionResult GetUploaded(Guid id)
        {

            var model = db.attached_document.Include(x => x.mov_list).Where(x => x.record_id == id && x.is_deleted != true);

            return Ok(model);
        }


        [HttpPost]
        [Route("Sync/Get/attachments")]
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

                HttpResponseMessage response = client.GetAsync("api/offline/v1/attachments/get_mapped?city_code=" + city_code + "&id=" + record_id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<attached_document>>(responseJson.Result);

                    //     var all = Mapper.DynamicMap<List<grievance_record_mapping>, List<grievance_record>>(model);

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

        #region Delete Attachment:

        //[WebMethod]
        //public static bool DeleteFile(string fileName)
        //{
        //    try
        //    {
        //        File.Delete(fileName);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        ////delete file on MOVs folder
        //[HttpGet]
        //public IActionResult DeleteFileAjax(Guid id)
        //{
        //    var path = PlatformServices.Default.Application.ApplicationBasePath;
        //    string source_path = path + @"\wwwroot\MOVs\";

        //    if (Directory.Exists(source_path))
        //    {
        //        string[] filePaths = Directory.GetFiles(path);
        //        List<ListItem> files = new List<ListItem>();
        //        htmlStr = "<table class='tblDocList'><tr class='trDocListHead'><td colspan='3'>Existing Documents</td></tr>";
        //        foreach (string filePath in filePaths)
        //        {
        //            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
        //            htmlStr += "<tr class='trDataRow'>";
        //            htmlStr += "<td class='tdFileName'>" + Path.GetFileName(filePath) + "</td>";
        //            htmlStr += "<td class='tdDownloadLink'><a href class='downloadLink' name='" + filePath + "'>Download</a></td>";
        //            htmlStr += "<td class='tdDownloadLink'><a href class='deleteLink' name='" + filePath + "'>Delete</a></td>";
        //            htmlStr += "</tr>";
        //        }
        //        htmlStr += "</table>";

        //    }






            

        //    if (source_path.Any()) {
        //        //delete the file
        //    }

        //    long size = 0;
        //    var files = Request.Form.Files;
        //    foreach (var file in files)
        //    {
        //        if (files.Count() != 1)
        //        {
        //            return BadRequest("Select 1 File to upload only");
        //        }

        //        var filename = ContentDispositionHeaderValue
        //                        .Parse(file.ContentDisposition)
        //                        .FileName
        //                        .Trim('"');


        //        if (!filename.Contains("pdf"))
        //        {
        //            return BadRequest("Upload PDF Files Only");
        //        }

        //        Guid attached_document_id = Guid.NewGuid();

        //        var path01 = PlatformServices.Default.Application.ApplicationBasePath;

        //        string savePath = path01 + @"\wwwroot\MOVs\" + attached_document_id.ToString() + ".pdf";
                
        //        size += file.Length;


        //        using (FileStream fs = System.IO.File.Create(savePath))
        //        {
        //            file.CopyTo(fs);
        //            fs.Flush();
        //        }

        //        var model = new attached_document
        //        {
        //            attached_document_id = attached_document_id,
        //            record_id = id,
        //            region_code = region_code,
        //            prov_code = prov_code,
        //            city_code = city_code,
        //            brgy_code = brgy_code,
        //            fund_source_id = fund_source_id,
        //            cycle_id = cycle_id,
        //            enrollment_id = enrollment_id,
        //            push_status_id = 2,
        //            approval_id = 3,
        //            created_by = 1,
        //            created_date = DateTime.Now,
        //            mov_list_id = mov_list_id,
        //        };

        //        db.attached_document.Add(model);

        //        db.SaveChanges();

        //        //string message = "File Uploaded Successfully";


        //        model.mov_list = db.mov_list.Find(mov_list_id);

        //        return Ok(model);
        //    }

        //    return BadRequest();

        //}



        #endregion


        [HttpPost]
        [Route("api/delete/attached_document")]
        public async Task<IActionResult> attached_document_delete(Guid id)
        {
            var record = db.attached_document.FirstOrDefault(x => x.attached_document_id == id);
            record.is_deleted = true;
            record.push_status_id = 3;

            //added: July 17
            //string sourceDir = @"D:\DeskApp2017\DeskApp\src\DeskApp\bin\Debug\netcoreapp1.0\win10-x64\wwwroot\MOVs";
            var path01 = PlatformServices.Default.Application.ApplicationBasePath;
            string savePath = path01 + @"\wwwroot\MOVs\" + id.ToString() + ".pdf";

            System.IO.File.Delete(savePath);
            
            await db.SaveChangesAsync();
            return Ok();

        }
        




        [Route("Sync/Post/attachments")]
        public async Task<ActionResult> PostOnline() //(string username, string password, Guid? record_id = null)
        {

            string username = "jmbalanag@e-dswd.net";
            string password = "dswd@123";

            string token = username + ":" + password;

            byte[] toBytes = Encoding.ASCII.GetBytes(token);


            string key = Convert.ToBase64String(toBytes);


            // foreach (var item in db.file_attachment.Where(x => x.push_status_id != 1).ToList())
            // {

            using (var client = new HttpClient())
            {
                ////setup client
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                var path01 = PlatformServices.Default.Application.ApplicationBasePath;

                string filelocationandname = path01 + @"\Uploads\" + "sample" + ".pdf";



                using (var fileStream = new FileStream(filelocationandname, FileMode.Open, FileAccess.Read))
                {
                    using (var content = new MultipartFormDataContent())
                    {

                        byte[] Bytes = new byte[fileStream.Length + 1];

                        fileStream.Read(Bytes, 0, Bytes.Length);

                        var fileContent = new ByteArrayContent(Bytes);

                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = Guid.NewGuid().ToString() };

                        content.Add(fileContent);


                        //  [Route("api/offline/v1/mov/upload")]
                        //        public IHttpActionResult upload_tracks(Guid file_attachment_id, Guid record_id, string region_code, string prov_code, string city_code, string brgy_code)


                        var requestUri = "http://localhost:2609/api/offline/v1/mov/upload?file_attachment_id=" + Guid.NewGuid().ToString() + "&record_id=" + Guid.NewGuid().ToString();

                        var result = client.PostAsync(requestUri, content).Result;

                        if (result.StatusCode == System.Net.HttpStatusCode.Created)
                        {

                            //  item.push_status_id = 1;
                            await db.SaveChangesAsync();


                        }
                        else
                        {
                            //item.push_status_id = 4;
                            await db.SaveChangesAsync();
                        }

                    }
                }
            }

            // }


            return Ok();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Record Id</param>
        /// <param name="table_name_id"></param>
        /// <returns></returns>
       [Route("api/offline/v1/attachments/get")]
       public IActionResult GetAttachments(Guid id, int table_name_id)
        {
            var model = db.file_attachment.Where(x => x.record_id == id && x.means_of_verification.table_name_id == table_name_id);

            var result = model.Select(x => new
            {
                x.means_of_verification.name,


            });

            return Ok(result);


        }



        [Route("api/offline/v1/attachment/save")]
        public async Task<IActionResult> Save(attached_document model, bool? api)
        {

            //return Ok(model);


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            var record = db.attached_document.AsNoTracking().FirstOrDefault(x => x.attached_document_id == model.attached_document_id);

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
                db.attached_document.Add(model);

                //var errorList = ModelState.Values.SelectMany(m => m.Errors)
                //               .Select(e => e.ErrorMessage)
                //               .ToList();

                try
                {


                    #region download file

                    var path01 = PlatformServices.Default.Application.ApplicationBasePath;

                    string savePath = path01 + @"\wwwroot\Movs\" + model.attached_document_id + ".pdf";


                    string region_code = model.region_code.ToString();

                    if (region_code.ToString().Length == 8)
                    {
                        region_code = "0" + region_code;
                    }


                    string url = @"http://ncddpdb.dswd.gov.ph/" + "MOVs/" + region_code + "/" + model.attached_document_id + ".pdf";

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
                    #endregion



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



        //[HttpPost]
        //public async Task<IActionResult> Index(ICollection<IFormFile> files)
        //{


        //    var path01 = PlatformServices.Default.Application.ApplicationBasePath;

        //    string source = path01 + @"\Uploads";




        //    foreach (var file in files)
        //    {
        //        if (file.Length > 0)
        //        {
        //            using (var fileStream = new FileStream(source + @"\" + file.FileName, FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //        }
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public virtual string UploadProfilePic(object obj)
        //{
        //    var length = Request.ContentLength;
        //    var bytes = new byte[length];
        //    Request.InputStream.Read(bytes, 0, length);




        //    var fileName = Request.Headers["X-File-Name"];
        //    var fileSize = Request.Headers["X-File-Size"];
        //    var fileType = Request.Headers["X-File-Type"];

        //    var project_id = Request.Headers["X-File-employee-id"];



        //    if (!fileType.ToString().Contains("jp"))
        //    {
        //        throw new System.InvalidOperationException("Not Authorized to upload non JPEG pictures");
        //    }

        //    int employee_id = Int32.Parse(project_id);


        //    var project = db.employee.Find(employee_id);


        //    //if (accessrepository. == false)
        //    //{
        //    //    throw new System.InvalidOperationException("Not Athorized for project");

        //    //}


        //    string pic = employee_id.ToString() + ".jpg";





        //    string savePath = "~/Content/EmployeeProfiles/";

        //    bool isExists = System.IO.Directory.Exists(Server.MapPath(savePath));

        //    if (!isExists)
        //    {
        //        System.IO.Directory.CreateDirectory(Server.MapPath(savePath));
        //    }



        //    string path = System.IO.Path.Combine(
        //                           Server.MapPath(savePath), pic);



        //    var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 10000, true);
        //    fileStream.Write(bytes, 0, length);
        //    fileStream.Close();


        //    return string.Format("{0} bytes uploaded", bytes.Length);


        //}
    }
}
