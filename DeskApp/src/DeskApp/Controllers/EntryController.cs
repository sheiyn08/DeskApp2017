using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DeskApp.Controllers
{
   
    [Authorize]
    public class EntryController : Controller
    {
        public ActionResult Oversight(Guid? id = null)
        {

            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }
        public ActionResult MPTA(Guid? id = null)
        {

            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }
        public ActionResult MLCC(Guid? id = null)
        {

            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }
        public ActionResult Organizations(Guid? id = null)
        {

            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }
        public ActionResult Grievances(Guid? id = null)
        {

            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }

        public ActionResult Trainings(Guid? id = null)
        {

            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }


        public ActionResult GrievanceInstallation(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }
        public ActionResult Profiles(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }
        public ActionResult MunicipalProfiles(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }
        public ActionResult BarangayProfile(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }

        public ActionResult BarangayAssembly(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }




        public ActionResult SubProjects(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }

        public ActionResult ReferenceSubProjects(int id)
        {
            

            ViewBag.id = id;



            return View();
        }

        public ActionResult CeacTracking(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            ViewBag.id = id;



            return View();
        }

    }
}