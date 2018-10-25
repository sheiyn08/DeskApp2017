using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
 

namespace DeskApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Location()
        {
          var path01 = PlatformServices.Default.Application.ApplicationBasePath;
           //   var path01 = "Data Source = " + PlatformServices.Default.Application.ApplicationBasePath  + "pantawid.sdf;" + " providerName=\"System.Data.SqlServerCe.4.0\"";

            return Ok(path01);

        }

        public IActionResult Perception()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {


            var path01 = PlatformServices.Default.Application.ApplicationBasePath;

            string source = path01 + @"\Uploads";



  
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(source + @"\" + file.FileName, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return View();
        }


        public IActionResult Path()
        {
            var path01 = PlatformServices.Default.Application.ApplicationBasePath;

            return Ok(path01);

        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
