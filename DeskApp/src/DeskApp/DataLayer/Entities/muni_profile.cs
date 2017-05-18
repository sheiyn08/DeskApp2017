using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskApp.DataLayer
{

    public class oversight_committee
    {
        [Key]
        public Guid oversight_committee_id { get; set; }
        public string old_id { get; set; }
        public string name { get; set; }
        public int? no_male { get; set; }
        public int? no_female { get; set; }
        public DateTime? date_organized { get; set; }

        public int? frequency { get; set; }

        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }
        public int enrollment_id { get; set; }

        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }

        [JsonIgnore]
        public virtual lib_enrollment lib_enrollment { get; set; }


        #endregion

        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        #endregion

        #region Sync
        //used for offline sync purposes
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion


    }
    public class muni_profile
    {
        public string transportation { get; set; }

        public int? socio_econ_hhs_1 { get; set; }
        public int? socio_econ_hhs_2 { get; set; }
        public int? socio_econ_hhs_3 { get; set; }
        public string socio_econ_activity_1 { get; set; }
        public double? socio_econ_amount_1 { get; set; }
        public string socio_econ_activity_2 { get; set; }
        public double? socio_econ_amount_2 { get; set; }
        public string socio_econ_activity_3 { get; set; }
        public double? socio_econ_amount_3 { get; set; }

        //additional: 4-25-17 Workitem 351
        public string socio_econ_seasonality_from_1 { get; set; }
        public string socio_econ_seasonality_to_1 { get; set; }
        public string socio_econ_seasonality_from_2 { get; set; }
        public string socio_econ_seasonality_to_2 { get; set; }
        public string socio_econ_seasonality_from_3 { get; set; }
        public string socio_econ_seasonality_to_3 { get; set; }


        /// <summary>
        /// Array
        /// </summary>
        public string gad_activity { get; set; }
        public double? gad_utilized { get; set; }
        public int? no_gad_activities { get; set; }

        /// <summary>
        /// Array
        /// </summary>
        public string drrm_activity { get; set; }
        public double? drrm_utilized { get; set; }
        public int? no_drmm_activities { get; set; }

        /// <summary>
        /// Array
        /// </summary>
        public string other_source { get; set; }
        public double? ira_amount { get; set; }
        public double? others_amount { get; set; }


        [Key]
        public Guid muni_profile_id { get; set; }

        public string old_id { get; set; }
        public int? year_source { get; set; }
        public int? no_of_brgys { get; set; }
        public double? pop_index { get; set; }
        public int? population { get; set; }
        public int? alloc_env { get; set; }
        public int? alloc_econ { get; set; }
        public int? alloc_infra { get; set; }
        public int? alloc_basic { get; set; }
        public int? alloc_educ { get; set; }
        public int? alloc_peace { get; set; }
        public int? alloc_inst { get; set; }
        public int? alloc_gender { get; set; }
        public int? alloc_drrm { get; set; }
        public int? alloc_others { get; set; }


        public int? hhs_owner { get; set; }
        public int? headmale_owner { get; set; }
        public int? headffemale_owner { get; set; }
        public int? hhs_tenant { get; set; }
        public int? headmale_tenant { get; set; }
        public int? headfemale_tenant { get; set; }
        public int? hhs_renting { get; set; }
        public int? headmale_renting { get; set; }
        public int? headfemale_renting { get; set; }
        public int? hhs_squatting { get; set; }
        public int? headmale_squatting { get; set; }
        public int? headfemale_squatting { get; set; }
        public double? householdincome_average { get; set; }
        public int? maleheaded_hhs { get; set; }
        public int? femaleheaded_hhs { get; set; }
        public int? ipheaded_hhs { get; set; }







        #region Location
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int fund_source_id { get; set; }
        public int cycle_id { get; set; }

        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_fund_source lib_fund_source { get; set; }
        [JsonIgnore]
        public virtual lib_cycle lib_cycle { get; set; }



        #endregion

        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        #endregion

        #region Sync
        //used for offline sync purposes
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion

        public string old_activity_id { get; set; }
    }


    public class muni_profile_income
    {

        [Key]
        public Guid muni_profile_income_id { get; set; }

        public double? amount { get; set; }

        public int families { get; set; }
        public int households { get; set; }
        [JsonIgnore]
        public virtual lib_source_income lib_source_income { get; set; }


        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        #endregion

        #region Sync
        //used for offline sync purposes
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion


        public Guid muni_profile_id { get; set; }
        [JsonIgnore]
        public virtual muni_profile muni_profile { get; set; }

    }

    public class muni_profile_fund_use
    {
        [Key]
        public Guid muni_profile_fund_use_id { get; set; }
        public string name { get; set; }
        public double? amount { get; set; }

        /// <summary>
        /// true GAD, false, DRRM
        /// </summary>
        public bool? is_gad { get; set; }
        public Guid muni_profile_id { get; set; }

        [JsonIgnore]
        public virtual muni_profile muni_profile { get; set; }


        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }


        #endregion

        #region Sync
        //used for offline sync purposes
        public int push_status_id { get; set; }
        public DateTime? push_date { get; set; }
        #endregion

        #region Approval
        public int approval_id { get; set; }
        [JsonIgnore]
        public virtual lib_approval lib_approval { get; set; }
        #endregion



    }

    public class lib_source_income
    {
        [Key]
        public int source_income_id { get; set; }
        public string name { get; set; }
    }
}
