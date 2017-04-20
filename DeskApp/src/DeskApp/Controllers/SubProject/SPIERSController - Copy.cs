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

using Newtonsoft.Json;

namespace DeskApp.Controllers
{

    public class SPISPCRProfileController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;


        public SPISPCRProfileController(ApplicationDbContext context)
        {
            db = context;

        }


        [Route("api/offline/v1/subproject/get_photos")]
        public ActionResult get_photos(Guid id)
        {

            var project = db.sub_project.FirstOrDefault(x => x.sub_project_unique_id == id && x.IsActive == true);

            if (project == null)
            {
                return null;

            }

            if (!User.Identity.IsAuthenticated)
            {

                var model = db.SPPhoto.Where(x => x.sub_project_unique_id == id && x.IsOtherTypeOfProject != true && x.is_deleted != true && (x.approval_id == 1 || x.approval_id == 2))
               .Select(x => new
               {
                   x.Id,
                   x.sub_project_id,
                 //  approval_name = x.lib_approval.name,
                //   x.lib_approval.is_approved,
                   x.UniqueName,
                  // x.sub_project.region_code,
                   lat = x.Latitude,
                   lng = x.Longitude,
                   alt = x.Altitude,
                   x.album_id,
                 //  album_name = x.lib_album.name,
                   x.GpsDateTimeStamp,
                   x.GetDateTaken,
                   x.lib_functionality_id,
                   x.reject_id,
                   x.approval_id,
                   x.geo_category_id,
                //   uploaded_by = db.UserProfiles.FirstOrDefault(u => u.UserName == x.CreatedBy).LastName + ", " + db.UserProfiles.FirstOrDefault(u => u.UserName == x.CreatedBy).FirstName,
                   uploaded_date = x.CreatedDate,
                   sequence_id = x.sequence_id
               });


                return Ok(model);
            }
            else
            {

                var model = db.SPPhoto.Where(x => x.sub_project_unique_id == id && x.IsOtherTypeOfProject != true && x.is_deleted != true)
                    .Select(x => new
                    {
                        x.Id,
                        x.sub_project_id,
                   //     approval_name = x.lib_approval.name,
                      //  x.lib_approval.is_approved,
                        x.UniqueName,
                  //      x.sub_project.region_code,
                        lat = x.Latitude,
                        lng = x.Longitude,
                        alt = x.Altitude,
                        x.album_id,
                     //   album_name = x.lib_album.name,
                        x.GpsDateTimeStamp,
                        x.GetDateTaken,
                        x.lib_functionality_id,
                        x.reject_id,
                        x.approval_id,
                        x.geo_category_id,
                      //  uploaded_by = db.UserProfiles.FirstOrDefault(u => u.UserName == x.CreatedBy).LastName + ", " + db.UserProfiles.FirstOrDefault(u => u.UserName == x.CreatedBy).FirstName,
                        uploaded_date = x.CreatedDate,
                        sequence_id = x.sequence_id
                    });


                return Json(model);
            }
        }




    }
}