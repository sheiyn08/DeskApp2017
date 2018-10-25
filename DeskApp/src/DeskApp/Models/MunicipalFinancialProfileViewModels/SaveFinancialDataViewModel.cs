using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.Models.MunicipalFinancialProfileViewModels
{
    public class SaveFinancialDataViewModel
    {
        public decimal mlgu_ira_share_2011 { get; set; }
        public decimal mlgu_ira_share_2012 { get; set; }
        public decimal mlgu_ira_share_2013 { get; set; }
        public decimal mlgu_ira_share_2014 { get; set; }
        public decimal mlgu_ira_share_2015 { get; set; }
        public decimal mlgu_ira_share_2016 { get; set; }
        public decimal mlgu_ira_share_2017 { get; set; }
        public decimal mlgu_ira_share_2018 { get; set; }
        public decimal mlgu_ira_share_2019 { get; set; }
        public string source_ira_share_2011 { get; set; }
        public string source_ira_share_2012 { get; set; }
        public string source_ira_share_2013 { get; set; }
        public string source_ira_share_2014 { get; set; }
        public string source_ira_share_2015 { get; set; }
        public string source_ira_share_2016 { get; set; }
        public string source_ira_share_2017 { get; set; }
        public string source_ira_share_2018 { get; set; }
        public string source_ira_share_2019 { get; set; }
        
        public decimal mlgu_locally_sourced_revenues_2011 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2012 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2013 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2014 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2015 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2016 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2017 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2018 { get; set; }
        public decimal mlgu_locally_sourced_revenues_2019 { get; set; }
        public string source_locally_sourced_revenues_2011 { get; set; }
        public string source_locally_sourced_revenues_2012 { get; set; }
        public string source_locally_sourced_revenues_2013 { get; set; }
        public string source_locally_sourced_revenues_2014 { get; set; }
        public string source_locally_sourced_revenues_2015 { get; set; }
        public string source_locally_sourced_revenues_2016 { get; set; }
        public string source_locally_sourced_revenues_2017 { get; set; }
        public string source_locally_sourced_revenues_2018 { get; set; }
        public string source_locally_sourced_revenues_2019 { get; set; }







        ////view model:
        //public class mlgu_financial_data_view_model
        //{
        //    public decimal mlgu_locally_sourced_revenues_2011 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2012 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2013 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2014 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2015 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2016 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2017 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2018 { get; set; }
        //    public decimal mlgu_locally_sourced_revenues_2019 { get; set; }

        //}

        ////aaaa
        //private void SaveOrUpdateMlgu(municipal_financial_profile model)
        //{
        //    if (db.municipal_financial_profile.FirstOrDefault(x => x.municipal_financial_profile_id == model.municipal_financial_profile_id) == null)
        //    {

        //    }
        //    else
        //    {

        //    }
        //}

        //#region SAVE FUNCTION
        //[Route("api/offline/v1/municipal_financial_profile/save")]
        ////public async Task<IActionResult> Save(mlgu_financial_data_view_model model, bool? api)
        //public async Task<IActionResult> Save(municipal_financial_profile model, bool? api)
        //{

        //    var record = db.mlgu_financial_data.AsNoTracking().FirstOrDefault(x => x.mlgu_financial_data_record_id == model.mlgu_financial_data_record_id);


        //    //var mlgu_2011 = new municipal_financial_profile
        //    //{
        //    //    year_id = 3,
        //    //    mlgu_locally_sourced_revenues = model.mlgu_locally_sourced_revenues_2011,


        //    //};

        //    //check 

        //    if (record == null)
        //    {
        //        if (api != true)
        //        {
        //            model.push_status_id = 2;
        //            model.push_date = null;
        //            model.approval_id = 3;
        //        }
        //        model.created_by = 0;
        //        model.created_date = DateTime.Now;

        //        model.is_deleted = false;
        //        db.mlgu_financial_data.Add(model);


        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            return Ok();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        model.push_date = null;

        //        if (api != true)
        //        {
        //            model.push_status_id = 3;
        //            model.approval_id = 3;
        //        }

        //        model.created_by = record.created_by;
        //        model.created_date = record.created_date;

        //        model.last_modified_by = 0;
        //        model.last_modified_date = DateTime.Now;

        //        db.Entry(model).State = EntityState.Modified;

        //        try
        //        {
        //            await db.SaveChangesAsync();
        //            return Ok();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}
        //#endregion
    }
}
