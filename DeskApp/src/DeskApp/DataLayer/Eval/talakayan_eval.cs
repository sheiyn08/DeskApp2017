using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeskApp.DataLayer.Eval
{
    public class talakayan_eval : base_record_no_fs_req_brgy2
    {
        [Key]
        public Guid talakayan_evaluation_id { get; set; }

        public int? evaluation_form_version { get; set; }
        public string talakayan_name { get; set; }
        public DateTime? talakayan_date_start { get; set; }
        public DateTime? talakayan_date_end { get; set; }
        public DateTime? evaluation_date_start { get; set; }
        public DateTime? evaluation_date_end { get; set; }
        public string talakayan_venue { get; set; }

        public int? participant_type { get; set; }
        public string person_name { get; set; }
        public bool? is_male { get; set; }
        public int talakayan_yr_id { get; set; }

        public int? v2016_obj { get; set; }                  // Were the objectives fully explained?
        public int? v2016_time_alloted { get; set; }         // Was time alloted to the different activities sufficient?
        public int? v2016_venue_a { get; set; }              // Venue: a. Clean
        public int? v2016_venue_b { get; set; }              // Venue: b. Well-ventilated
        public int? v2016_venue_c { get; set; }              // Venue: c. Spacious
        public int? v2016_venue_d { get; set; }              // Venue: d. Well-lighted
        public int? v2016_sound_system { get; set; }         // Equipped with good sound system
        public int? v2016_visual_a { get; set; }             // Visual aids in the presentation are: a. Easily read from where I am seated
        public int? v2016_visual_b { get; set; }             // Visual aids in the presentation are: b. Attractive
        public int? v2016_visual_c { get; set; }             // Visual aids in the presentation are: c. Easy to understand
        public int? v2016_meals_a { get; set; }              // Meals: a. Delicious
        public int? v2016_meals_b { get; set; }              // Meals: b. Sufficient
        public int? v2016_meals_c { get; set; }              // Meals: c. Clean
        public int? v2016_gallery_walk { get; set; }         // Did you like the gallery walk?
        public int? v2016_fgd { get; set; }                  // Did you like the Focus Group Discussion?
        public string v2016_comments { get; set; }          // Comments or Suggestions
        public int? v2016_knowledge_part1 { get; set; }      // Knowledge gained: Part 1: Presentation of the Municipal Profile and Development Status of the Municipality
        public int? v2016_knowledge_part2 { get; set; }      // Knowledge gained: Part 2: Presentation of the Municipal Development Priorities, Interventions and Gaps
        public int? v2016_knowledge_part3 { get; set; }      // Knowledge gained: Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year
        public int? v2016_knowledge_part4 { get; set; }      // Knowledge gained: Part 4: Gallery Walk
        public int? v2016_knowledge_part5 { get; set; }      // Knowledge gained: Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan
        public int? v2016_overall_satisfaction { get; set; } // Overall satisfaction on the conduct of Municipal Talakayan
        public int? v2016_team_effectiveness { get; set; }   // Please evaluate the whole teams' effectiveness in the conduct of the different parts of the Municipal Talakayan


        public int? v2015_obj_a { get; set; }                  // Were the objectives fully explained?
        public int? v2015_obj_b { get; set; }                  // Was time alloted to the different activities sufficient?
        public int? v2015_timeallotment_a_part1 { get; set; }  // Part 1: Presentation of the Municipal Profile and Development Status of the Municipality
        public int? v2015_timeallotment_a_part2 { get; set; }  // Part 2: Presentation of Summary of Needs, Interventions (including KC), and Gaps
        public int? v2015_timeallotment_a_part3 { get; set; }  // Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year
        public int? v2015_timeallotment_a_part4 { get; set; }  // Part 4: Gallery Walk
        public int? v2015_timeallotment_a_part5 { get; set; }  // Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan
        public int? v2015_timeallotment_b_part1 { get; set; }  // Part 1: Presentation of the Municipal Profile and Development Status of the Municipality
        public int? v2015_timeallotment_b_part2 { get; set; }  // Part 2: Presentation of Summary of Needs, Interventions (including KC), and Gaps
        public int? v2015_timeallotment_b_part3 { get; set; }  // Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year
        public int? v2015_timeallotment_b_part4 { get; set; }  // Part 4: Gallery Walk
        public int? v2015_timeallotment_b_part5 { get; set; }  // Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan
        public int? v2015_partsoftalakayan_part1 { get; set; }  // Part 1: Presentation of the Municipal Profile and Development Status of the Municipality
        public int? v2015_partsoftalakayan_part2 { get; set; }  // Part 2: Presentation of Summary of Needs, Interventions (including KC), and Gaps
        public int? v2015_partsoftalakayan_part3 { get; set; }  // Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year
        public int? v2015_partsoftalakayan_part4 { get; set; }  // Part 4: Gallery Walk
        public int? v2015_partsoftalakayan_part5 { get; set; }  // Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan
        public int? v2015_evaluation_a { get; set; }             // Please evaluate the whole teams' effectiveness in the conduct of the different parts of the Municipal Talakayan
        public string v2015_evaluation_b { get; set; }          // Any special concern regarding the team that you want to bring to our attention?
        public int? v2015_venue_a { get; set; }                  // Venue is.... a. Clean
        public int? v2015_venue_b { get; set; }                  // Venue is.... b. Well-ventilated
        public int? v2015_venue_c { get; set; }                  // Venue is.... c. Spacious
        public int? v2015_venue_d { get; set; }                  // Venue is.... d. Well-lighted
        public int? v2015_venue_e { get; set; }                  // Venue is.... e. Equipped with good sound system
        public int? v2015_visual_a { get; set; }                 // Visual Aids in the presentation are... a. Sufficient
        public int? v2015_visual_b { get; set; }                 // Visual Aids in the presentation are... b. Easily read from where I am seated
        public int? v2015_visual_c { get; set; }                 // Visual Aids in the presentation are... c. Informative
        public int? v2015_visual_d { get; set; }                 // Visual Aids in the presentation are... d. Attractive
        public int? v2015_visual_e { get; set; }                 // Visual Aids in the presentation are... e. Easy to understand
        public int? v2015_satisfaction_a { get; set; }           // a. Knowledge gained
        public string v2015_satisfaction_a_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_b { get; set; }           // b. Overall Content
        public string v2015_satisfaction_b_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_c { get; set; }           // c. Competency of the Facilitators
        public string v2015_satisfaction_c_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_d { get; set; }           // d. Schedule of different parts of the Municipal Talakayan
        public string v2015_satisfaction_d_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_e { get; set; }           // e. Part 1: Presentation of the Municipal Profile and Development Status of the Municipality
        public string v2015_satisfaction_e_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_f { get; set; }           // f. Part 2: Presentation of Summary of Needs, Interventions (including KC), and Gaps
        public string v2015_satisfaction_f_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_g { get; set; }           // g. Part 3: Presentation of the Municipal Development Agenda - Plans and Activities for the coming year
        public string v2015_satisfaction_g_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_h { get; set; }           // h. Part 4: Gallery Walk
        public string v2015_satisfaction_h_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_i { get; set; }           // i. Part 5: Talakayan Synthesis, Next Steps, Participants' and Evaluation of the Municipal Talakayan
        public string v2015_satisfaction_i_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_j { get; set; }           // j. Venue
        public string v2015_satisfaction_j_remarks { get; set; }   // [Remarks]
        public int? v2015_satisfaction_k { get; set; }           // k. Overall satisfaction on the Municipal Talakayan
        public string v2015_satisfaction_k_remarks { get; set; }   // [Remarks]

    }

    public class base_record_no_fs_req_brgy2
    {
        [JsonIgnore]
        public virtual lib_region lib_region { get; set; }
        [JsonIgnore]
        public virtual lib_province lib_province { get; set; }
        [JsonIgnore]
        public virtual lib_city lib_city { get; set; }
        [JsonIgnore]
        public virtual lib_brgy lib_brgy { get; set; }

        public int region_code { get; set; }
        public int prov_code { get; set; }
        public int city_code { get; set; }
        public int? brgy_code { get; set; } //08-24-17: for v3.0 remove brgy code as required by Eval
        
        #region Audit
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? last_modified_by { get; set; }
        public DateTime? last_modified_date { get; set; }

        public bool is_deleted { get; set; }
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


        #endregion
    }
}
