using DeskApp.Data;
using DeskApp.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DeskApp.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using DeskApp.DataLayer.Eval;

namespace DeskApp.Controllers
{

    [Authorize]
    public class SyncController : Controller
    {

        public static string url = @"http://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:8079"; //---- to be used for testing
        public static string geoUrl = @"http://geotagging.dswd.gov.ph";

        private readonly ApplicationDbContext db;

      
        public SyncController(ApplicationDbContext context)
        {
            db = context;
         
        }


        public IActionResult OTS()
        {
            return View();
        }
        public bool mov_list(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/mov_list").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<mov_list>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.mov_list.AsNoTracking().FirstOrDefault(x => x.mov_list_id == item.mov_list_id) == null)
                        {
                            db.mov_list.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    return true;

                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }


        public bool report_list(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/report_list").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<report_list>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.report_list.AsNoTracking().FirstOrDefault(x => x.report_list_id == item.report_list_id) == null)
                        {
                            db.report_list.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    return true;

                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }

        public bool lib_disaster_type(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/lib_disaster_type").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_disaster_type>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_disaster_type.AsNoTracking().FirstOrDefault(x => x.disaster_type_id == item.disaster_type_id) == null)
                        {
                            db.lib_disaster_type.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    return true;

                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }

        public bool lib_disaster(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/v1/android/sub_project/get/disaster_list").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<disaster>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.disaster.AsNoTracking().FirstOrDefault(x => x.disaster_id == item.disaster_id) == null)
                        {
                            db.disaster.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    return true;

                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }


        public bool lib_source_income(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_source_income").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_source_income>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_source_income.AsNoTracking().FirstOrDefault(x => x.source_income_id == item.source_income_id) == null)
                        {
                            db.lib_source_income.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    return true;

                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }


        public bool lib_ers_delivery_mode(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_ers_delivery_mode").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_ers_delivery_mode>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_ers_delivery_mode.AsNoTracking().FirstOrDefault(x => x.ers_delivery_mode_id == item.ers_delivery_mode_id) == null)
                        {
                            db.lib_ers_delivery_mode.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    return true;

                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }

        public bool table_name(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/table_name").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<table_name>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.table_name.AsNoTracking().FirstOrDefault(x => x.table_name_id == item.table_name_id) == null)
                        {
                            db.table_name.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    return true;

                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }

       
        public bool GetRegions(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_region").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    dynamic model = JsonConvert.DeserializeObject(responseJson.Result);


                    foreach (var item in model)
                    {

                        var _items = new lib_region
                        {
                            region_code = item["region_code"],
                            region_nick = item["region_nick"],
                            region_name = item["region_nick"],
                        };

                        if (db.lib_region.AsNoTracking().FirstOrDefault(x => x.region_code == _items.region_code) == null)

                        {
                            db.lib_region.Add(_items);
                            db.SaveChanges();
                        }
                    }


                    return true;
                }
                else
                {
                    return false;
                }


            }

            //  return key;
        }

        private void GetProvinces(string username, string password, int region_code)
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


                string code = region_code.ToString();

                if(code.Length == 8)
                {
                    code = "0" + code;
                }
                HttpResponseMessage response = client.GetAsync("api/offline/lib_province?id=" + code).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    dynamic model = JsonConvert.DeserializeObject(responseJson.Result);

                    foreach (var item in model)
                    {

                        var _items = new lib_province
                        {
                            region_code = region_code,
                            prov_code =  item["prov_code"] ,
                            prov_name = item["prov_name"],

                        };

                        if (db.lib_province.AsNoTracking().FirstOrDefault(x => x.prov_code == _items.prov_code) == null)

                        {
                            db.lib_province.Add(_items);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {

                }


            }
        }

        private void GetMunicipality(string username, string password, int id)
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


                string code = id.ToString();

                if (code.Length == 8)
                {
                    code = "0" + code;
                }


                HttpResponseMessage response = client.GetAsync("api/offline/lib_city?id=" + code).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    dynamic model = JsonConvert.DeserializeObject(responseJson.Result);

                    foreach (var item in model)
                    {

                        var _items = new lib_city
                        {

                            prov_code = id,
                            city_code = item["city_code"] ,
                            city_name = item["city_name"],

                        };

                        if (db.lib_city.AsNoTracking().FirstOrDefault(x => x.city_code == _items.city_code) == null)

                        {
                            db.lib_city.Add(_items);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {

                }


            }
        }

        private void GetBarangay(string username, string password, int id)
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


                string code = id.ToString();

                if (code.Length == 8)
                {
                    code = "0" + code;
                }


                HttpResponseMessage response = client.GetAsync("api/offline/lib_brgy?id=" + code).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    dynamic model = JsonConvert.DeserializeObject(responseJson.Result);

                    foreach (var item in model)
                    {

                        var _items = new lib_brgy
                        {

                            city_code = id,
                            brgy_code =  item["brgy_code"] ,
                            brgy_name = item["brgy_name"],

                        };

                        if (db.lib_brgy.AsNoTracking().FirstOrDefault(x => x.brgy_code == _items.brgy_code) == null)

                        {
                            db.lib_brgy.Add(_items);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {

                }


            }
        }


        public void lib_cycle(string username, string password, int fund_source_id)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_cycle?id=" + fund_source_id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_cycle>>(responseJson.Result);

                    foreach (var item in model)
                    {
                        var i = db.lib_cycle.AsNoTracking().FirstOrDefault(x => x.cycle_id == item.cycle_id);

                        if (i == null)
                        {

                            item.lib_fund_source = null;

                            db.lib_cycle.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {


                            item.lib_fund_source = null;

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                    }
                }
                else
                {

                }
            }

        }

        public void lib_fund_source(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_fund_source").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_fund_source>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_fund_source.AsNoTracking().FirstOrDefault(x => x.fund_source_id == item.fund_source_id) == null)
                        {
                            db.lib_fund_source.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_enrollment(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_enrollment").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_enrollment>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_enrollment.AsNoTracking().FirstOrDefault(x => x.enrollment_id == item.enrollment_id) == null)
                        {
                            db.lib_enrollment.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_approval(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_approval").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_approval>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_approval.AsNoTracking().FirstOrDefault(x => x.approval_id == item.approval_id) == null)
                        {
                            db.lib_approval.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_push_status(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_push_status").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_push_status>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_push_status.AsNoTracking().FirstOrDefault(x => x.push_status_id == item.push_status_id) == null)
                        {
                            db.lib_push_status.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        #region GRS
        public void lib_grs_category(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_category").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_category>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_category.AsNoTracking().FirstOrDefault(x => x.grs_category_id == item.grs_category_id) == null)
                        {
                            db.lib_grs_category.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_complainant_position(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_complainant_position").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_complainant_position>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_complainant_position.AsNoTracking().FirstOrDefault(x => x.grs_complainant_position_id == item.grs_complainant_position_id) == null)
                        {
                            db.lib_grs_complainant_position.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_resolution_status(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_resolution_status").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_resolution_status>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_resolution_status.AsNoTracking().FirstOrDefault(x => x.grs_resolution_status_id == item.grs_resolution_status_id) == null)
                        {
                            db.lib_grs_resolution_status.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_form(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_form").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_form>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_form.AsNoTracking().FirstOrDefault(x => x.grs_form_id == item.grs_form_id) == null)
                        {
                            db.lib_grs_form.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_intake_level(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_intake_level").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_intake_level>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_intake_level.AsNoTracking().FirstOrDefault(x => x.grs_intake_level_id == item.grs_intake_level_id) == null)
                        {
                            db.lib_grs_intake_level.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_filling_mode(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_filling_mode").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_filling_mode>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_filling_mode.AsNoTracking().FirstOrDefault(x => x.grs_filling_mode_id == item.grs_filling_mode_id) == null)
                        {
                            db.lib_grs_filling_mode.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_sex(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_sex").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_sex>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_sex.AsNoTracking().FirstOrDefault(x => x.sex_id == item.sex_id) == null)
                        {
                            db.lib_sex.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_grs_sender_designation(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_sender_designation").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_sender_designation>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_sender_designation.AsNoTracking().FirstOrDefault(x => x.grs_sender_designation_id == item.grs_sender_designation_id) == null)
                        {
                            db.lib_grs_sender_designation.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_nature(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_nature").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_nature>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_nature.AsNoTracking().FirstOrDefault(x => x.grs_nature_id == item.grs_nature_id) == null)
                        {
                            db.lib_grs_nature.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_complaint_subject(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_complaint_subject").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_complaint_subject>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_complaint_subject.AsNoTracking().FirstOrDefault(x => x.grs_complaint_subject_id == item.grs_complaint_subject_id) == null)
                        {
                            db.lib_grs_complaint_subject.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_grs_intake_officer(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_intake_officer").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_intake_officer>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_intake_officer.AsNoTracking().FirstOrDefault(x => x.grs_intake_officer_id == item.grs_intake_officer_id) == null)
                        {
                            db.lib_grs_intake_officer.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }
        public void lib_grs_feedback(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_grs_feedback").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_grs_feedback>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_grs_feedback.AsNoTracking().FirstOrDefault(x => x.grs_feedback_id == item.grs_feedback_id) == null)
                        {
                            db.lib_grs_feedback.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        #endregion


        #region profiles
        public void lib_lgu_position(string username, string password)
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


                HttpResponseMessage response = client.GetAsync("api/offline/lib_lgu_position").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_lgu_position>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_lgu_position.AsNoTracking().FirstOrDefault(x => x.lgu_position_id == item.lgu_position_id) == null)
                        {
                            db.lib_lgu_position.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_civil_status(string username, string password)
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


                HttpResponseMessage response = client.GetAsync("api/offline/lib_civil_status").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_civil_status>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_civil_status.AsNoTracking().FirstOrDefault(x => x.civil_status_id == item.civil_status_id) == null)
                        {
                            db.lib_civil_status.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_indigenous_groups(string username, string password)
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


                HttpResponseMessage response = client.GetAsync("api/offline/lib_indigenous_groups").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_ip_group>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_ip_group.AsNoTracking().FirstOrDefault(x => x.ip_group_id == item.ip_group_id) == null)
                        {
                            db.lib_ip_group.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_barangay_assembly_purpose(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_barangay_assembly_purpose").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_barangay_assembly_purpose>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_barangay_assembly_purpose.AsNoTracking().FirstOrDefault(x => x.barangay_assembly_purpose_id == item.barangay_assembly_purpose_id) == null)
                        {
                            db.lib_barangay_assembly_purpose.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_occupation(string username, string password)
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


                HttpResponseMessage response = client.GetAsync("api/offline/lib_occupation").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_occupation>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_occupation.AsNoTracking().FirstOrDefault(x => x.occupation_id == item.occupation_id) == null)
                        {
                            db.lib_occupation.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_education_attainment(string username, string password)
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


                HttpResponseMessage response = client.GetAsync("api/offline/lib_education_attainment").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_education_attainment>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_education_attainment.AsNoTracking().FirstOrDefault(x => x.education_attainment_id == item.education_attainment_id) == null)
                        {
                            db.lib_education_attainment.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_volunteer_committee(string username, string password)
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


                HttpResponseMessage response = client.GetAsync("api/offline/lib_volunteer_committee").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_volunteer_committee>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_volunteer_committee.AsNoTracking().FirstOrDefault(x => x.volunteer_committee_id == item.volunteer_committee_id) == null)
                        {
                            db.lib_volunteer_committee.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }
        public void lib_volunteer_committee_position(string username, string password)
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


                HttpResponseMessage response = client.GetAsync("api/offline/lib_volunteer_committee_position").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_volunteer_committee_position>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_volunteer_committee_position.AsNoTracking().FirstOrDefault(x => x.volunteer_committee_position_id == item.volunteer_committee_position_id) == null)
                        {
                            db.lib_volunteer_committee_position.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }
        #endregion


        #region training

        public void lib_training_category(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_training_category").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_training_category>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_training_category.AsNoTracking().FirstOrDefault(x => x.training_category_id == item.training_category_id) == null)
                        {
                            db.lib_training_category.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_lgu_level(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/lib_lgu_level").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_lgu_level>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_lgu_level.AsNoTracking().FirstOrDefault(x => x.lgu_level_id == item.lgu_level_id) == null)
                        {
                            db.lib_lgu_level.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_psa_problem_category(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/lib_psa_problem_category").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_psa_problem_category>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_psa_problem_category.AsNoTracking().FirstOrDefault(x => x.psa_problem_category_id == item.psa_problem_category_id) == null)
                        {
                            db.lib_psa_problem_category.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_psa_solution_category(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/lib_psa_solution_category").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_psa_solution_category>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_psa_solution_category.AsNoTracking().FirstOrDefault(x => x.psa_solution_category_id == item.psa_solution_category_id) == null)
                        {
                            db.lib_psa_solution_category.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_ers_current_work(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/lib_ers_current_work").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_ers_current_work>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_ers_current_work.AsNoTracking().FirstOrDefault(x => x.ers_current_work_id == item.ers_current_work_id) == null)
                        {
                            db.lib_ers_current_work.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_eca_type(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/lib_eca_type").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_eca_type>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_eca_type.AsNoTracking().FirstOrDefault(x => x.eca_type_id == item.eca_type_id) == null)
                        {
                            db.lib_eca_type.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_transpo_mode(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/lib_transpo_mode").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_transpo_mode>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_transpo_mode.AsNoTracking().FirstOrDefault(x => x.transpo_mode_id == item.transpo_mode_id) == null)
                        {
                            db.lib_transpo_mode.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        public void lib_implementation_status(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/lib_implementation_status").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_implementation_status>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_implementation_status.AsNoTracking().FirstOrDefault(x => x.implementation_status_id == item.implementation_status_id) == null)
                        {
                            db.lib_implementation_status.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        #endregion

        #region Municipal Financial Data

        public void lgpms_data(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lgpms_data").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lgpms_data>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lgpms_data.AsNoTracking().FirstOrDefault(x => x.lgpms_data_id == item.lgpms_data_id) == null)
                        {
                            db.lgpms_data.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void dof_blgf_financial_data(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/dof_blgf_financial_data").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<dof_blgf_financial_data>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.dof_blgf_financial_data.AsNoTracking().FirstOrDefault(x => x.dof_blgf_financial_data_record_id == item.dof_blgf_financial_data_record_id) == null)
                        {
                            db.dof_blgf_financial_data.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        #endregion

        #region Library update for type of org (Community Organizations module)
        public void lib_organization(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_organization").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_organization>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_organization.AsNoTracking().FirstOrDefault(x => x.organization_id == item.organization_id) == null)
                        {
                            db.lib_organization.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }
        #endregion


        //v3.0 new table: lib_mode
        public void lib_mode(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_mode").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<lib_mode>>(responseJson.Result);
                    foreach (var item in model)
                        if (db.lib_mode.AsNoTracking().FirstOrDefault(x => x.mode_id == item.mode_id) == null)
                        {
                            db.lib_mode.Add(item);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Entry(item).State = EntityState.Modified;
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public ActionResult UpdateLibrary(string username, string password)
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

                HttpResponseMessage response = client.GetAsync("api/offline/lib_fund_source").Result;
                if (response.IsSuccessStatusCode)
                {


                }
                else
                {
                    return BadRequest("Credentials Did Not Work");
                }
            }

            lib_fund_source(username, password);

            foreach (var item in db.lib_fund_source.ToList())
            {
                lib_cycle(username, password, item.fund_source_id);
            }

            lib_enrollment(username, password);

            lib_approval(username, password);

            lib_push_status(username, password);

            GetRegions(username, password);

            foreach (var item in db.lib_region.ToList())
            {
                GetProvinces(username, password, item.region_code);
            }

            foreach (var item in db.lib_province.ToList())
            {
                GetMunicipality(username, password, item.prov_code);
            }

            foreach (var item in db.lib_city.ToList())
            {
                GetBarangay(username, password, item.city_code);
            }

            lib_grs_feedback(username, password);
            lib_grs_category(username, password);
            lib_grs_complainant_position(username, password);
            lib_grs_resolution_status(username, password);
            lib_grs_form(username, password);
            lib_grs_intake_level(username, password);
            lib_grs_filling_mode(username, password);
            lib_indigenous_groups(username, password);
            lib_sex(username, password);
            lib_grs_sender_designation(username, password);
            lib_grs_nature(username, password);
            lib_grs_complaint_subject(username, password);
            lib_grs_intake_officer(username, password);

            lib_education_attainment(username, password);
            lib_lgu_position(username, password);
            lib_civil_status(username, password);

            lib_occupation(username, password);
            lib_volunteer_committee(username, password);
            lib_volunteer_committee_position(username, password);


            lib_lgu_level(username, password);
            lib_training_category(username, password);

            lib_psa_problem_category(username, password);
            lib_psa_solution_category(username, password);

            lib_barangay_assembly_purpose(username, password);

            lib_ers_current_work(username, password);
            lib_eca_type(username, password);

            lib_transpo_mode(username, password);

            lib_implementation_status(username, password);
            lib_major_classification(username, password);
            lib_project_type(username, password);


            table_name(username, password);


            lib_source_income(username, password);
            lib_ers_delivery_mode(username, password);

            lib_physical_status_category(username, password);
            lib_physical_status(username, password);

            report_list(username, password);

            lib_disaster_type(username, password);

            lib_disaster(username, password);

            mov_list(username, password);

            lgpms_data(username, password);
            dof_blgf_financial_data(username, password);

            lib_organization(username, password);

            lib_mode(username, password);

            return Ok();

        }


        public ActionResult Index()
        {

            return View();
        }


        #region Geotagging
        public void lib_project_type(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/geo/lib_project_type").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_project_type>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_project_type.AsNoTracking().FirstOrDefault(x => x.project_type_id == item.project_type_id) == null)
                        {
                            db.lib_project_type.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }
        public void lib_physical_status(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/geo/lib_physical_status").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_physical_status>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_physical_status.AsNoTracking().FirstOrDefault(x => x.physical_status_id == item.physical_status_id) == null)
                        {
                            db.lib_physical_status.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_physical_status_category(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/geo/lib_physical_status_category").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_physical_status_category>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_physical_status_category.AsNoTracking().FirstOrDefault(x => x.physical_status_category_id == item.physical_status_category_id) == null)
                        {
                            db.lib_physical_status_category.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }

        public void lib_major_classification(string username, string password)
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
                HttpResponseMessage response = client.GetAsync("api/offline/geo/lib_major_classification").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync();

                    var model = JsonConvert.DeserializeObject<List<lib_major_classification>>(responseJson.Result);

                    foreach (var item in model)

                        if (db.lib_major_classification.AsNoTracking().FirstOrDefault(x => x.major_classification_id == item.major_classification_id) == null)
                        {
                            db.lib_major_classification.Add(item);
                            db.SaveChanges();

                        }
                        else
                        {

                            db.Entry(item).State = EntityState.Modified;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbUpdateConcurrencyException)
                            {

                            }
                        }

                }
                else
                {

                }
            }
        }


        #endregion
    }

}
