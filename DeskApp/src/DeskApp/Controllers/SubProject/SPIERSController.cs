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
        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;


        public SpiErsController(ApplicationDbContext context)
        {
            db = context;

        }


   


    }
}