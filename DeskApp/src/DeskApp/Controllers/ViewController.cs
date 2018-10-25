using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DeskApp.Controllers.Repository;

namespace DeskApp.Controllers
{
    [Authorize]
    public class ViewController : Controller
    {
        private IPSARepository repository;

        public ViewController(IPSARepository _psa)
        {
          
            repository = _psa;
        }

        [Route("api/table_name")]
        public IActionResult GetTable(string name)
        {
            return Ok(repository.table_name_id(name));

        }

        public ActionResult Oversight()
        {
            
            return View();
        }
        public ActionResult MPTA()
        {
           

            return View();
        }
        public ActionResult MLCC()
        {
            return View();
        }
        public ActionResult Organizations()
        {
            return View();
        }
        public ActionResult BarangayProfiles()
        {
            return View();
        }

        public ActionResult MunicipalProfile()
        {
            return View();
        }

        public ActionResult BarangayAssembly()
        {
            return View();

        }

        public ActionResult Grievances()
        {
            return View();
        }

        public ActionResult Trainings()
        {
            return View();

        }

        public ActionResult TrainingsPSA()
        {
            return View();

        }


        public ActionResult People()
        {
            return View();
        }

        public ActionResult GrievanceInstallation()
        {
            return View();
        }

        public ActionResult SubProjects()
        {
            return View();
        }
        public ActionResult MunicipalProfiles()
        {
            return View();
        }

        public ActionResult CeacTracking()
        {
            return View();
        }
        

    }
}
