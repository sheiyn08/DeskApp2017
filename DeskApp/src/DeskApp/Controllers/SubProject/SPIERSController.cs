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

    public class SpiErsController : Controller
    {
        public static string url = @"http://ncddpdb.dswd.gov.ph";

        private readonly ApplicationDbContext db;


        public SpiErsController(ApplicationDbContext context)
        {
            db = context;

        }


   


    }
}