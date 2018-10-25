using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult MunicipalReports()
        {
            return View();
        }

        public ActionResult ACTAccomplishmentReportIndex()
        {
            return View();
        }

        public ActionResult act_accomplishment_report()
        {
            return View();
        }

        public ActionResult ACTAccomplishmentReport()
        {
            return View();
        }

        public ActionResult AddressedPSAPriorities()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Talakayan()
        {
            return View();
        }

        public ActionResult Evaluation()
        {
            return View();
        }

        public ActionResult MunicipalFinancialProfile()
        {
            return View();
        }


        //public ActionResult _ba_breakdown_of_attendees()
        //{
        //    ViewBag.header = "KC Municipal Report - Barangay Assembly";
        //    ViewBag.title = "Breakdown of Activities";





        //    return View();
        //}
        //public ActionResult _ba_hhs_fam_representation()
        //{
        //    ViewBag.header = "KC Municipal Report - Barangay Assembly";
        //    ViewBag.title = "Household & Families Representation";




        //    return View();
        //}


        //public ActionResult _training_participants()
        //{
        //    ViewBag.header = "KC Municipal Report - Municipal Training";



        //    return View();
        //}
    }
}
