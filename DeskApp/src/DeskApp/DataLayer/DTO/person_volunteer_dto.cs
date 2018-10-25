using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{
    public class person_volunteer_recordDTO
    {
        public string lib_cycle_name { get; set; }
        public string lib_enrollment_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_volunteer_committee_name { get; set; }
        public string lib_volunteer_committee_position_name { get; set; }
        public string person_profile_old_id { get; set; }
        public System.Guid person_volunteer_record_id { get; set; }
        public System.Guid person_profile_id { get; set; }
        public System.Int32 volunteer_committee_id { get; set; }
        public System.Int32 volunteer_committee_position_id { get; set; }
        public System.Int32 fund_source_id { get; set; }
        public System.Int32 cycle_id { get; set; }
        public System.Int32 enrollment_id { get; set; }
        public System.DateTime? start_date { get; set; }
        public System.DateTime? end_date { get; set; }
        public System.Int32 volunteer_committee_membership_position_id { get; set; }

        public static System.Linq.Expressions.Expression<Func<person_volunteer_record, person_volunteer_recordDTO>> SELECT =
            x => new person_volunteer_recordDTO
            {
                lib_cycle_name = x.lib_cycle.name,
                lib_enrollment_name = x.lib_enrollment.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_volunteer_committee_name = x.lib_volunteer_committee.name,
                lib_volunteer_committee_position_name = x.lib_volunteer_committee_position.name,
                person_profile_old_id = x.person_profile.old_id,
                person_volunteer_record_id = x.person_volunteer_record_id,
                person_profile_id = x.person_profile_id,
                volunteer_committee_id = x.volunteer_committee_id,
                volunteer_committee_position_id = x.volunteer_committee_position_id,
                fund_source_id = x.fund_source_id,
                cycle_id = x.cycle_id,
                enrollment_id = x.enrollment_id,
                start_date = x.start_date,
                end_date = x.end_date,

            };

    }
}
