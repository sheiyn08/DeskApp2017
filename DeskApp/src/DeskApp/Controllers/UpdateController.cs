using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DeskApp.Controllers.Repository;

namespace DeskApp.Controllers
{
    public class UpdateController : Controller
    {
        public ActionResult UpdateSPOthers()
        {
            return View();
        }


    }
}
