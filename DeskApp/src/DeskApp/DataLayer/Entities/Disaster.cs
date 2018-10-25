using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskApp.DataLayer.Entities
{
    public class disaster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid disaster_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int disaster_type_id { get; set; }

        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        [JsonIgnore]
        public virtual lib_disaster_type lib_disaster_type { get; set; }
    }

    public class lib_disaster_type
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int disaster_type_id { get; set; }
        public string name { get; set; }
    }

    public class diaster_coverage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int disaster_coverage_id { get; set; }
        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public bool? affected { get; set; }

        public Guid disaster_id { get; set; }


        [JsonIgnore]
        public virtual disaster disaster { get; set; }
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }

    }

    public class sub_project_post_disaster
    {
        [Key]
        public Guid sub_project_post_disaster_id { get; set; }
        public int sub_project_id { get; set; }

        public Guid disaster_id { get; set; }
        [JsonIgnore]
        public virtual disaster disaster { get; set; }

        public bool? affected { get; set; }


        /// <summary>
        //Utilized During Disaster ? 
        /// </summary>
        public bool? utilized { get; set; }
        /// <summary>
        /// If Utilized no pf people servered by the facility
        /// </summary>
        public int? no_served { get; set; }

        public int condition_id { get; set; }

        public double? estimated_damage { get; set; }
        public double? estimated_repair { get; set; }

        public string remarks { get; set; }
        [JsonIgnore]
        public virtual lib_condition lib_condition { get; set; }


        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
    }

    public class sub_project_pre_disaster
    {

        [Key]
        public Guid sub_project_pre_disaster_id { get; set; }
        public int sub_project_id { get; set; }

        public int condition_id { get; set; }
        public DateTime date_assessed { get; set; }
        public bool? can_be_used_as_evac_site { get; set; }
        public int capacity { get; set; }
        public bool? with_fist_aid_kit { get; set; }
        public string contact_person { get; set; }
        public string contact_no { get; set; }


        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool? is_deleted { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        [JsonIgnore]
        public virtual lib_condition lib_condition { get; set; }
    }

    /// <summary>
    /// Post Disaster Condition Dropdown,
    /// No Applicable if not affected
    /// </summary>
    public class lib_condition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int condition_id { get; set; }
        public string name { get; set; }
    }


  
}
