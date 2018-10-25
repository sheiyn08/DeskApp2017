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

        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing
        //public static string url = @"http://localhost:2609"; //---- solution to solution

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

            if (item.module_id != null)
            {
                //Barangay Profile
                if (item.module_id == 1) {
                    model = model.Where(m => m.mov_list_id == 3);
                }
                //Municipal Profile
                else if (item.module_id == 2)
                {
                    model = model.Where(m => m.mov_list_id == 2);
                }
                //BA
                else if (item.module_id == 3)
                {
                    model = model.Where(m => m.mov_list_id == 4 || m.mov_list_id == 5 || m.mov_list_id == 6 || m.mov_list_id == 52);
                }
                //Person Profile
                else if (item.module_id == 4)
                {
                    model = model.Where(m => m.mov_list_id == 14 || m.mov_list_id == 51);
                }
                //Training
                else if (item.module_id == 5)
                {
                    model = model.Where(m => m.mov_list_id == 7 || m.mov_list_id == 8 || m.mov_list_id == 9 || m.mov_list_id == 10 || m.mov_list_id == 11 || m.mov_list_id == 12 || m.mov_list_id == 13 || m.mov_list_id == 19);
                }
                //Grievance
                else if (item.module_id == 6)
                {
                    model = model.Where(m => m.mov_list_id == 22);
                }
                //GRS Installation
                else if (item.module_id == 7)
                {
                    model = model.Where(m => m.mov_list_id == 20 || m.mov_list_id == 21);
                }
                //SP
                else if (item.module_id == 8)
                {
                    model = model.Where(m => m.mov_list_id == 24 || m.mov_list_id == 25 || m.mov_list_id == 26 || m.mov_list_id == 27 || m.mov_list_id == 28 || m.mov_list_id == 29 || m.mov_list_id == 30 || m.mov_list_id == 31 || m.mov_list_id == 32 || m.mov_list_id == 33 || m.mov_list_id == 34 || m.mov_list_id == 35 || m.mov_list_id == 36 || m.mov_list_id == 37 || m.mov_list_id == 38 || m.mov_list_id == 39 || m.mov_list_id == 40 || m.mov_list_id == 41 || m.mov_list_id == 42 || m.mov_list_id == 43 || m.mov_list_id == 44 || m.mov_list_id == 45 || m.mov_list_id == 46 || m.mov_list_id == 55 || m.mov_list_id == 56 || m.mov_list_id == 57);
                }
                //MPTA
                else if (item.module_id == 9)
                {
                    model = model.Where(m => m.mov_list_id == 16 || m.mov_list_id == 53 || m.mov_list_id == 54);
                }
                //MLCC
                else if (item.module_id == 10)
                {
                    model = model.Where(m => m.mov_list_id == 23);
                }
                //Oversight
                else if (item.module_id == 15)
                {
                    model = model.Where(m => m.mov_list_id == 3);
                }
                //Comm. Organizations
                else if (item.module_id == 12)
                {
                    model = model.Where(m => m.mov_list_id == 50);
                }
                //Perception
                else if (item.module_id == 13)
                {
                    model = model.Where(m => m.mov_list_id == 47);
                }
                //Talakayan
                else if (item.module_id == 14)
                {
                    model = model.Where(m => m.mov_list_id == 49);
                }
                //All
                else
                {
                    model = model.Where(m => m.is_deleted != true);
                }
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

                    attachment_id = s.mov_list.mov_list_id,

                    s.record_id,
                    s.push_status_id,
                    s.push_date


  
                }).OrderBy(x => x.attached_document_id).Skip(currPages * size).Take(size).ToList()



            });

        }


        #region 4.0 updates
        [Route("api/offline/v1/attachments/post/mov_for_uploading")]
        public async Task<IActionResult> SavePushStatusID(Guid attached_document_id, int push_status_id)
        {
            var mov = db.attached_document.Find(attached_document_id);

            if (mov == null)
            {
                return BadRequest("Error");
            }

            mov.push_status_id = push_status_id;

            await db.SaveChangesAsync();
            return Ok();
        }

        //New Sync Post code: RDR 052118
        [HttpPost]
        [Route("Sync/Post/attachments")]
        public async Task<ActionResult> PostOnlineNew(string username, string password)
        {
            string token = username + ":" + password;
            byte[] toBytes = Encoding.ASCII.GetBytes(token);
            string key = Convert.ToBase64String(toBytes);

            //1. upload first details of attachment as JSON, normal sync:
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

                var path01 = PlatformServices.Default.Application.ApplicationBasePath;
                var item = db.attached_document.FirstOrDefault(x => x.push_status_id == 5);

                string module_name = "";
                string attachment_type = "";

                #region switch
                switch (item.mov_list_id)
                {
                    case 2:
                        module_name = "MP_";
                        attachment_type = "MunicipalProfileForm_";
                        break;
                    case 3:
                        module_name = "BP_";
                        attachment_type = "BrgyProfileForm_";
                        break;
                    case 4:
                        module_name = "BA_";
                        attachment_type = "BrgyActivityMinutesForm_";
                        break;
                    case 5:
                        module_name = "BA_";
                        attachment_type = "BAHouseholdParticipation_";
                        break;
                    case 6:
                        module_name = "BA_";
                        attachment_type = "BAAttendanceSheet_";
                        break;
                    case 52:
                        module_name = "BA_";
                        attachment_type = "BrgyResolution_";
                        break;
                    case 14:
                        module_name = "PP_";
                        attachment_type = "CommunityVolunteersProfile_";
                        break;
                    case 51:
                        module_name = "PP_";
                        attachment_type = "CDDSPWorkerBasicProfile_";
                        break;
                    case 7:
                        module_name = "TR_";
                        attachment_type = "MunicipalActivityMinutesForm_";
                        break;
                    case 8:
                        module_name = "TR_";
                        attachment_type = "MunicipalAttendanceSheet_";
                        break;
                    case 9:
                        module_name = "TR_";
                        attachment_type = "BrgyActivityMinutesForm_";
                        break;
                    case 10:
                        module_name = "TR_";
                        attachment_type = "BrgyAttendanceSheet_";
                        break;
                    case 11:
                        module_name = "TR_";
                        attachment_type = "BrgyActionPlan_";
                        break;
                    case 12:
                        module_name = "TR_";
                        attachment_type = "BrgyResolution_";
                        break;
                    case 13:
                        module_name = "TR_";
                        attachment_type = "OtherDocuments_";
                        break;
                    case 19:
                        module_name = "TR_";
                        attachment_type = "MIBFMunicipalResolution_";
                        break;
                    case 22:
                        module_name = "GR_";
                        attachment_type = "GRSIntakeForm_";
                        break;
                    case 20:
                        module_name = "GI_";
                        attachment_type = "GRSInstallationChecklistMunicipal_";
                        break;
                    case 21:
                        module_name = "GI_";
                        attachment_type = "GRSInstallationChecklistBrgy_";
                        break;
                    case 24:
                        module_name = "SP_";
                        attachment_type = "BrgySPWorkSchedandPhysicalProgressReport_";
                        break;
                    case 25:
                        module_name = "SP_";
                        attachment_type = "SuspensionOrder_";
                        break;
                    case 26:
                        module_name = "SP_";
                        attachment_type = "ResumptionOrder_";
                        break;
                    case 27:
                        module_name = "SP_";
                        attachment_type = "VariationOrder_";
                        break;
                    case 28:
                        module_name = "SP_";
                        attachment_type = "TargetHouseholdBeneficiaries_";
                        break;
                    case 29:
                        module_name = "SP_";
                        attachment_type = "CNC_";
                        break;
                    case 30:
                        module_name = "SP_";
                        attachment_type = "CNO_";
                        break;
                    case 31:
                        module_name = "SP_";
                        attachment_type = "CP_";
                        break;
                    case 32:
                        module_name = "SP_";
                        attachment_type = "UsufructAgreement_";
                        break;
                    case 33:
                        module_name = "SP_";
                        attachment_type = "BLGUResolution_";
                        break;
                    case 34:
                        module_name = "SP_";
                        attachment_type = "DepEdCertification_";
                        break;
                    case 35:
                        module_name = "SP_";
                        attachment_type = "EmploymentRecordSheet_";
                        break;
                    case 36:
                        module_name = "SP_";
                        attachment_type = "CDDSPWorkerBasicProfile_";
                        break;
                    case 37:
                        module_name = "SP_";
                        attachment_type = "SPFundUtilizationReport_";
                        break;
                    case 38:
                        module_name = "SP_";
                        attachment_type = "CADT_";
                        break;
                    case 39:
                        module_name = "SP_";
                        attachment_type = "RequestforValidationtoNCIP_";
                        break;
                    case 40:
                        module_name = "SP_";
                        attachment_type = "Tariff_";
                        break;
                    case 41:
                        module_name = "SP_";
                        attachment_type = "SPCompletionReport_";
                        break;
                    case 42:
                        module_name = "SP_";
                        attachment_type = "FinalInspectionReport_";
                        break;
                    case 43:
                        module_name = "SP_";
                        attachment_type = "CertofCompletionandAcceptance_";
                        break;
                    case 44:
                        module_name = "SP_";
                        attachment_type = "FunctionalAuditTool_";
                        break;
                    case 45:
                        module_name = "SP_";
                        attachment_type = "ActualHouseholdBeneficiaries_";
                        break;
                    case 46:
                        module_name = "SP_";
                        attachment_type = "SustainabilityEvaluationTool_";
                        break;
                    case 55:
                        module_name = "SP_";
                        attachment_type = "ESSC_";
                        break;
                    case 56:
                        module_name = "SP_";
                        attachment_type = "ESMP_";
                        break;
                    case 57:
                        module_name = "SP_";
                        attachment_type = "ECC_";
                        break;
                    case 16:
                        module_name = "PTA_";
                        attachment_type = "PTAIntegrationPlansChecklist_";
                        break;
                    case 53:
                        module_name = "PTA_";
                        attachment_type = "BrgyResolution_";
                        break;
                    case 54:
                        module_name = "PTA_";
                        attachment_type = "MunicipalResolution_";
                        break;
                    case 23:
                        module_name = "MLCC_";
                        attachment_type = "MunicipalConsolidatedStatusofLCC_";
                        break;
                    case 15:
                        module_name = "OC_";
                        attachment_type = "OversightandCoordCommitteeChecklist_";
                        break;
                    case 50:
                        module_name = "CO_";
                        attachment_type = "CommunityOrganizationProfileForm_";
                        break;
                    case 47:
                        module_name = "PS_";
                        attachment_type = "PerceptionSurveyForm_";
                        break;
                    case 49:
                        module_name = "MT_";
                        attachment_type = "MunicipalTalakayanEvaluationForm_";
                        break;
                    default:
                        module_name = "NA";
                        attachment_type = "NA";
                        break;
                }
                #endregion

                StringContent data = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("api/offline/v1/attachment/save", data).Result;

                if (response.IsSuccessStatusCode)
                {
                    item.push_status_id = 1;
                    item.push_date = DateTime.Now;
                    Guid attachment_id = item.attached_document_id;
                    string file_name = module_name + attachment_type + item.attached_document_id.ToString() + ".pdf";
                    int? region_code = item.region_code;
                    int? city_code = item.city_code;
                    //2. then upload the actual file
                    await PostMOVFile(username, password, file_name, attachment_id, region_code, city_code);
                    await db.SaveChangesAsync();
                }
                else
                {
                    item.push_status_id = 2;
                    return BadRequest();
                }
            }
            return Ok();
        }

        public async Task<bool> PostMOVFile(string username, string password, string file_name, Guid attachment_id, int? region_code, int? city_code)
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

                var path01 = PlatformServices.Default.Application.ApplicationBasePath;
                string filelocationandname = path01 + @"\wwwroot\MOVs\" + file_name;

                using (var fileStream = new FileStream(filelocationandname, FileMode.Open, FileAccess.Read))
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        byte[] Bytes = new byte[fileStream.Length + 1];
                        fileStream.Read(Bytes, 0, Bytes.Length);
                        var fileContent = new ByteArrayContent(Bytes);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = attachment_id.ToString() };
                        content.Add(fileContent);

                        var requestUri = "api/offline/v1/mov/upload?file_name=" + file_name + "&region_code=" + region_code + "&city_code=" + city_code;
                        var result = client.PostAsync(requestUri, content).Result;

                        if (result.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            return false;
                        }

                    }
                }

            }

            return true;
        }


        //4.0 rename the file to new format: module_form_id.pdf for old attachments wherein file name is only in id.pdf format
        //[HttpPost]
        [Route("api/offline/v1/attachment/rename_old_filename")]
        public async Task<IActionResult> RenameOldAttachment(attached_document model)
        {
            var path01 = PlatformServices.Default.Application.ApplicationBasePath;
            string old_filename = path01 + @"\wwwroot\MOVs\" + model.attached_document_id.ToString() + ".pdf";

            string module_name = "";
            string attachment_type = "";

            #region if-else
            //Barangay Profile attachments:
            if (model.mov_list_id == 3)
            {
                module_name = "BP_";
                attachment_type = "BrgyProfileForm_";
            }

            //Municipal Profile attachments:
            if (model.mov_list_id == 2)
            {
                module_name = "MP_";
                attachment_type = "MunicipalProfileForm_";
            }

            //Brgy Assembly attachments:
            if (model.mov_list_id == 4)
            {
                module_name = "BA_";
                attachment_type = "BrgyActivityMinutesForm_";
            }
            if (model.mov_list_id == 5)
            {
                module_name = "BA_";
                attachment_type = "BAHouseholdParticipation_";
            }
            if (model.mov_list_id == 6)
            {
                module_name = "BA_";
                attachment_type = "BAAttendanceSheet_";
            }
            if (model.mov_list_id == 52)
            {
                module_name = "BA_";
                attachment_type = "BrgyResolution_";
            }

            //Person Profile attachments:
            if (model.mov_list_id == 14)
            {
                module_name = "PP_";
                attachment_type = "CommunityVolunteersProfile_";
            }
            if (model.mov_list_id == 51)
            {
                module_name = "PP_";
                attachment_type = "CDDSPWorkerBasicProfile_";
            }

            //Trainings attachments:
            if (model.mov_list_id == 7)
            {
                module_name = "TR_";
                attachment_type = "MunicipalActivityMinutesForm_";
            }
            if (model.mov_list_id == 8)
            {
                module_name = "TR_";
                attachment_type = "MunicipalAttendanceSheet_";
            }
            if (model.mov_list_id == 9)
            {
                module_name = "TR_";
                attachment_type = "BrgyActivityMinutesForm_";
            }
            if (model.mov_list_id == 10)
            {
                module_name = "TR_";
                attachment_type = "BrgyAttendanceSheet_";
            }
            if (model.mov_list_id == 11)
            {
                module_name = "TR_";
                attachment_type = "BrgyActionPlan_";
            }
            if (model.mov_list_id == 12)
            {
                module_name = "TR_";
                attachment_type = "BrgyResolution_";
            }
            if (model.mov_list_id == 13)
            {
                module_name = "TR_";
                attachment_type = "OtherDocuments_";
            }
            if (model.mov_list_id == 19)
            {
                module_name = "TR_";
                attachment_type = "MIBFMunicipalResolution_";
            }

            //Grievance attachments:
            if (model.mov_list_id == 22)
            {
                module_name = "GR_";
                attachment_type = "GRSIntakeForm_";
            }

            //GRS Installation attachments:
            if (model.mov_list_id == 20)
            {
                module_name = "GI_";
                attachment_type = "GRSInstallationChecklistMunicipal_";
            }
            if (model.mov_list_id == 21)
            {
                module_name = "GI_";
                attachment_type = "GRSInstallationChecklistBrgy_";
            }

            //SubProject attachments:
            if (model.mov_list_id == 24)
            {
                module_name = "SP_";
                attachment_type = "BrgySPWorkSchedandPhysicalProgressReport_";
            }
            if (model.mov_list_id == 25)
            {
                module_name = "SP_";
                attachment_type = "SuspensionOrder_";
            }
            if (model.mov_list_id == 26)
            {
                module_name = "SP_";
                attachment_type = "ResumptionOrder_";
            }
            if (model.mov_list_id == 27)
            {
                module_name = "SP_";
                attachment_type = "VariationOrder_";
            }
            if (model.mov_list_id == 28)
            {
                module_name = "SP_";
                attachment_type = "TargetHouseholdBeneficiaries_";
            }
            if (model.mov_list_id == 29)
            {
                module_name = "SP_";
                attachment_type = "CNC_";
            }
            if (model.mov_list_id == 30)
            {
                module_name = "SP_";
                attachment_type = "CNO_";
            }
            if (model.mov_list_id == 31)
            {
                module_name = "SP_";
                attachment_type = "CP_";
            }
            if (model.mov_list_id == 32)
            {
                module_name = "SP_";
                attachment_type = "UsufructAgreement_";
            }
            if (model.mov_list_id == 33)
            {
                module_name = "SP_";
                attachment_type = "BLGUResolution_";
            }
            if (model.mov_list_id == 34)
            {
                module_name = "SP_";
                attachment_type = "DepEdCertification_";
            }
            if (model.mov_list_id == 35)
            {
                module_name = "SP_";
                attachment_type = "EmploymentRecordSheet_";
            }
            if (model.mov_list_id == 36)
            {
                module_name = "SP_";
                attachment_type = "CDDSPWorkerBasicProfile_";
            }
            if (model.mov_list_id == 37)
            {
                module_name = "SP_";
                attachment_type = "SPFundUtilizationReport_";
            }
            if (model.mov_list_id == 38)
            {
                module_name = "SP_";
                attachment_type = "CADT_";
            }
            if (model.mov_list_id == 39)
            {
                module_name = "SP_";
                attachment_type = "RequestforValidationtoNCIP_";
            }
            if (model.mov_list_id == 40)
            {
                module_name = "SP_";
                attachment_type = "Tariff_";
            }
            if (model.mov_list_id == 41)
            {
                module_name = "SP_";
                attachment_type = "SPCompletionReport_";
            }
            if (model.mov_list_id == 42)
            {
                module_name = "SP_";
                attachment_type = "FinalInspectionReport_";
            }
            if (model.mov_list_id == 43)
            {
                module_name = "SP_";
                attachment_type = "CertofCompletionandAcceptance_";
            }
            if (model.mov_list_id == 44)
            {
                module_name = "SP_";
                attachment_type = "FunctionalAuditTool_";
            }
            if (model.mov_list_id == 45)
            {
                module_name = "SP_";
                attachment_type = "ActualHouseholdBeneficiaries_";
            }
            if (model.mov_list_id == 46)
            {
                module_name = "SP_";
                attachment_type = "SustainabilityEvaluationTool_";
            }
            if (model.mov_list_id == 55)
            {
                module_name = "SP_";
                attachment_type = "ESSC_";
            }
            if (model.mov_list_id == 56)
            {
                module_name = "SP_";
                attachment_type = "ESMP_";
            }
            if (model.mov_list_id == 57)
            {
                module_name = "SP_";
                attachment_type = "ECC_";
            }

            //MPTA attachments:
            if (model.mov_list_id == 16)
            {
                module_name = "PTA_";
                attachment_type = "PTAIntegrationPlansChecklist_";
            }
            if (model.mov_list_id == 53)
            {
                module_name = "PTA_";
                attachment_type = "BrgyResolution_";
            }
            if (model.mov_list_id == 54)
            {
                module_name = "PTA_";
                attachment_type = "MunicipalResolution_";
            }

            //MLCC attachments:
            if (model.mov_list_id == 23)
            {
                module_name = "MLCC_";
                attachment_type = "MunicipalConsolidatedStatusofLCC_";
            }

            //Oversight attachments:
            if (model.mov_list_id == 15)
            {
                module_name = "OC_";
                attachment_type = "OversightandCoordCommitteeChecklist_";
            }

            //Organizations attachments:
            if (model.mov_list_id == 50)
            {
                module_name = "CO_";
                attachment_type = "CommunityOrganizationProfileForm_";
            }

            //Perception Survey attachments:
            if (model.mov_list_id == 47)
            {
                module_name = "PS_";
                attachment_type = "PerceptionSurveyForm_";
            }

            //Municipal Talakayan attachments:
            if (model.mov_list_id == 49)
            {
                module_name = "MT_";
                attachment_type = "MunicipalTalakayanEvaluationForm_";
            }
            #endregion

            string new_filename = path01 + @"\wwwroot\MOVs\" + module_name + attachment_type + model.attached_document_id.ToString() + ".pdf";

            System.IO.File.Move(old_filename, new_filename);            

            return Ok();
        }



        #endregion

        #region 4.2 updates
        //4.2: change records with push_status_id 5 to 2 if not uploaded
        [Route("api/offline/v1/mov/clean_push_status_id")]
        public async Task<IActionResult> CleanPushStatus()
        {
            var items = db.attached_document.Where(s => s.push_status_id == 5);

            if (items != null) {
                foreach (var i in items)
                {
                    i.push_status_id = 2;
                }
            }

            await db.SaveChangesAsync();
            return Ok();
        }
        #endregion

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
                if (mov_list_id == 52)
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

                //Organizations attachments:
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

        [Route("api/mov/get/module_form_name")]
        public IActionResult GetModuleFormName(Guid id)
        {
            var model = db.attached_document.FirstOrDefault(x => x.attached_document_id == id);
            return Ok(model);

            //return Json(db.attached_document.Select(x => new { mov_list_id = x.mov_list_id }));

            //return Json(model.Select(x => new { mov_list_id = x.mov_list_id }));
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

            //var path01 = PlatformServices.Default.Application.ApplicationBasePath;
            //string savePath = path01 + @"\wwwroot\MOVs\" + id.ToString() + ".pdf";

            var path01 = PlatformServices.Default.Application.ApplicationBasePath;
            string savePath = "";

            //------------- v3.2 fix deleting of attachment in MOVs folder -------------//

            //Muni Profile
            if (record.mov_list_id == 3) {
                savePath = path01 + @"\wwwroot\MOVs\" + "BP_BrgyProfileForm_" + id.ToString() + ".pdf";
            }
            //Muni Profile
            else if (record.mov_list_id == 2)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "MP_MunicipalProfileForm_" + id.ToString() + ".pdf";
            }
            //Brgy Assembly
            else if (record.mov_list_id == 4)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "BA_BrgyActivityMinutesForm_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 5)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "BA_BAHouseholdParticipation_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 6)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "BA_BAAttendanceSheet_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 52)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "BA_BrgyResolution_" + id.ToString() + ".pdf";
            }
            //Person Profile
            else if (record.mov_list_id == 14)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "PP_CommunityVolunteersProfile_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 51)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "PP_CDDSPWorkerBasicProfile_" + id.ToString() + ".pdf";
            }
            //Training
            else if (record.mov_list_id == 7)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_MunicipalActivityMinutesForm_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 8)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_MunicipalAttendanceSheet_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 9)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_BrgyActivityMinutesForm_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 10)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_BrgyAttendanceSheet_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 11)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_BrgyActionPlan_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 12)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_BrgyResolution_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 13)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_OtherDocuments_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 19)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "TR_MIBFMunicipalResolution_" + id.ToString() + ".pdf";
            }
            //Grievances
            else if (record.mov_list_id == 22)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "GR_GRSIntakeForm_" + id.ToString() + ".pdf";
            }
            //GRS Installation
            else if (record.mov_list_id == 20)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "GI_GRSInstallationChecklistMunicipal_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 21)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "GI_GRSInstallationChecklistBrgy_" + id.ToString() + ".pdf";
            }
            //SubProject
            else if (record.mov_list_id == 24)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_BrgySPWorkSchedandPhysicalProgressReport_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 25)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_SuspensionOrder_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 26)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_ResumptionOrder_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 27)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_VariationOrder_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 28)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_TargetHouseholdBeneficiaries_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 29)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_CNC_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 30)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_CNO_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 31)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_CP_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 32)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_UsufructAgreement_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 33)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_BLGUResolution_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 34)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_DepEdCertification_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 35)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_EmploymentRecordSheet_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 36)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_CDDSPWorkerBasicProfile_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 37)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_SPFundUtilizationReport_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 38)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_CADT_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 39)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_RequestforValidationtoNCIP_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 40)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_Tariff_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 41)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_SPCompletionReport_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 42)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_FinalInspectionReport_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 43)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_CertofCompletionandAcceptance_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 44)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_FunctionalAuditTool_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 45)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_ActualHouseholdBeneficiaries_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 46)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_SustainabilityEvaluationTool_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 55)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_ESSC_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 56)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_ESMP_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 57)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "SP_ECC_" + id.ToString() + ".pdf";
            }
            //PTA
            else if (record.mov_list_id == 16)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "PTA_PTAIntegrationPlansChecklist_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 53)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "PTA_BrgyResolution_" + id.ToString() + ".pdf";
            }
            else if (record.mov_list_id == 54)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "PTA_MunicipalResolution_" + id.ToString() + ".pdf";
            }
            //MLCC
            else if (record.mov_list_id == 23)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "MLCC_MunicipalConsolidatedStatusofLCC_" + id.ToString() + ".pdf";
            }
            //Oversight
            else if (record.mov_list_id == 15)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "OC_OversightandCoordCommitteeChecklist_" + id.ToString() + ".pdf";
            }
            //Organizations
            else if (record.mov_list_id == 50)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "CO_CommunityOrganizationProfileForm_" + id.ToString() + ".pdf";
            }
            //Perception
            else if (record.mov_list_id == 47)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "PS_PerceptionSurveyForm_" + id.ToString() + ".pdf";
            }
            //Talakayan
            else if (record.mov_list_id == 49)
            {
                savePath = path01 + @"\wwwroot\MOVs\" + "MT_MunicipalTalakayanEvaluationForm_" + id.ToString() + ".pdf";
            }


            System.IO.File.Delete(savePath);
            
            await db.SaveChangesAsync();
            return Ok();

        }
        



        //OLD: JESSY's CODE
        //[Route("Sync/Post/attachments")]
        //public async Task<ActionResult> PostOnline() //(string username, string password, Guid? record_id = null)
        //{

        //    string username = "jmbalanag@e-dswd.net";
        //    string password = "dswd@123";

        //    string token = username + ":" + password;

        //    byte[] toBytes = Encoding.ASCII.GetBytes(token);


        //    string key = Convert.ToBase64String(toBytes);


        //    // foreach (var item in db.file_attachment.Where(x => x.push_status_id != 1).ToList())
        //    // {

        //    using (var client = new HttpClient())
        //    {
        //        ////setup client
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);

        //        var path01 = PlatformServices.Default.Application.ApplicationBasePath;

        //        string filelocationandname = path01 + @"\Uploads\" + "sample" + ".pdf";



        //        using (var fileStream = new FileStream(filelocationandname, FileMode.Open, FileAccess.Read))
        //        {
        //            using (var content = new MultipartFormDataContent())
        //            {

        //                byte[] Bytes = new byte[fileStream.Length + 1];

        //                fileStream.Read(Bytes, 0, Bytes.Length);

        //                var fileContent = new ByteArrayContent(Bytes);

        //                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = Guid.NewGuid().ToString() };

        //                content.Add(fileContent);


        //                //  [Route("api/offline/v1/mov/upload")]
        //                //        public IHttpActionResult upload_tracks(Guid file_attachment_id, Guid record_id, string region_code, string prov_code, string city_code, string brgy_code)


        //                var requestUri = "http://localhost:2609/api/offline/v1/mov/upload?file_attachment_id=" + Guid.NewGuid().ToString() + "&record_id=" + Guid.NewGuid().ToString();

        //                var result = client.PostAsync(requestUri, content).Result;

        //                if (result.StatusCode == System.Net.HttpStatusCode.Created)
        //                {

        //                    //  item.push_status_id = 1;
        //                    await db.SaveChangesAsync();


        //                }
        //                else
        //                {
        //                    //item.push_status_id = 4;
        //                    await db.SaveChangesAsync();
        //                }

        //            }
        //        }
        //    }

        //    // }


        //    return Ok();
        //}



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
