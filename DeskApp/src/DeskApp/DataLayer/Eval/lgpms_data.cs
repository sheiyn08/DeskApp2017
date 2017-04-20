using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Eval
{
    public class lgpms_data : base_record_location_muni
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lgpms_data_id { get; set; }

        public int psgc_code { get; set; }

        public int? overall_performance_index_2009 { get; set; }
        public int? overall_performance_index_2010 { get; set; }
        public int? overall_performance_index_2011 { get; set; }
        public int? overall_performance_index_2012 { get; set; }

        public int? administrative_governance_2009 { get; set; }
        public int? administrative_governance_2010 { get; set; }
        public int? administrative_governance_2011 { get; set; }
        public int? administrative_governance_2012 { get; set; }

        public int? social_governance_2009 { get; set; }
        public int? social_governance_2010 { get; set; }
        public int? social_governance_2011 { get; set; }
        public int? social_governance_2012 { get; set; }

        public int? economic_governance_2009 { get; set; }
        public int? economic_governance_2010 { get; set; }
        public int? economic_governance_2011 { get; set; }
        public int? economic_governance_2012 { get; set; }

        public int? environmental_governance_2009 { get; set; }
        public int? environmental_governance_2010 { get; set; }
        public int? environmental_governance_2011 { get; set; }
        public int? environmental_governance_2012 { get; set; }

        public int? valuing_fundamentals_of_good_gov_2009 { get; set; }
        public int? valuing_fundamentals_of_good_gov_2010 { get; set; }
        public int? valuing_fundamentals_of_good_gov_2011 { get; set; }
        public int? valuing_fundamentals_of_good_gov_2012 { get; set; }

    }

    public class base_record_location_muni
    {
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
       
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }

    }
}
