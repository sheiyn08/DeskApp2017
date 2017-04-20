using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 
using Microsoft.AspNetCore.Mvc;
 
using DeskApp.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KnightDev.Controllers.Library
{

    public partial class LibraryAPIController : Controller
    {

        private readonly ApplicationDbContext db;


        public LibraryAPIController(ApplicationDbContext context)
        {
            db = context;

        }

        [Route("api/lib_implementation_status")]
        public ActionResult lib_implementation_status()
        {
            var source = db.lib_implementation_status.AsQueryable();

            return Json(source.Select(x => new { Id = x.implementation_status_id, Name = x.name }));

        }
        [Route("api/lib_grs_intake_level")]
        public ActionResult lib_grs_intake_level()
        {
            var source = db.lib_grs_intake_level.AsQueryable();

            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_intake_level_id, Name = x.name }));

        }

        [Route("api/lib_grs_form")]
        public ActionResult lib_grs_form()
        {
            var source = db.lib_grs_form.AsQueryable();

            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_form_id, Name = x.name }));

        }
        [Route("api/lib_grs_filling_mode")]
        public ActionResult lib_grs_filling_mode()
        {
            var source = db.lib_grs_filling_mode.AsQueryable();

            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_filling_mode_id, Name = x.name }));

        }

        [Route("api/lib_grs_resolution_status")]
        public ActionResult lib_grs_resolution_status()
        {
            var source = db.lib_grs_resolution_status.AsQueryable();

            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_resolution_status_id, Name = x.name }));

        }


        [Route("api/lib_grs_feedback")]
        public ActionResult lib_grs_feedback()
        {
            var source = db.lib_grs_feedback.AsQueryable();

            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_feedback_id, Name = x.name }));

        }


        [Route("api/lib_grs_category")]
        public ActionResult lib_grs_category()
        {
            var source = db.lib_grs_category.AsQueryable();

            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_category_id, Name = x.name }));

        }


        [Route("api/lib_grs_complaint_subject")]
        public ActionResult lib_grs_complaint_subject()
        {
            var source = db.lib_grs_complaint_subject.AsQueryable();

            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_complaint_subject_id, Name = x.name }));

        }


        [Route("api/lib_grs_nature")]
        public ActionResult lib_grs_nature()
        {
            var source = db.lib_grs_nature.AsQueryable();
            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_nature_id, Name = x.description}));

        }



        [Route("api/lib_ip_group")]
        public ActionResult lib_ip_group()
        {
            var source = db.lib_ip_group.AsQueryable();
            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.ip_group_id, Name = x.name }));

        }


        [Route("api/lib_grs_sender_designation")]
        public ActionResult lib_grs_sender_designation()
        {
            var source = db.lib_grs_sender_designation.AsQueryable();
            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_sender_designation_id, Name = x.name }));

        }
        [Route("api/lib_sex")]
        public ActionResult lib_sex()
        {
            var source = db.lib_sex.AsQueryable();
            return Json(source.Select(x => new { Id = x.sex_id, Name = x.name }));

        }


        [Route("api/lib_grs_intake_officer")]
        public ActionResult lib_grs_intake_officer()
        {
            var source = db.lib_grs_intake_officer.AsQueryable();
            return Json(source.Where(x => x.is_active != null).Select(x => new { Id = x.grs_intake_officer_id, Name = x.name }));

        }


    }
}
