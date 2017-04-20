using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeskApp.DataLayer
{

    public class display
    {
        public string record_id { get; set; }
        public string lib_approval_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_region_region_name { get; set; }
    }

    public class muni_profileDTO
    {
        public string lib_approval_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_region_region_name { get; set; }
        public Guid muni_profile_id { get; set; }
        public string old_id { get; set; }
        public int push_status_id { get; set; }

        public static System.Linq.Expressions.Expression<Func<muni_profile, muni_profileDTO>> SELECT =
            x => new muni_profileDTO
            {
                lib_approval_name = x.lib_approval.name,

                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_name = x.lib_cycle.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_region_region_name = x.lib_region.region_name,
                muni_profile_id = x.muni_profile_id,
                old_id = x.old_id,
                push_status_id = x.push_status_id,

            };

    }

    public class brgy_profileDTO
    {
        public string lib_approval_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_region_region_name { get; set; }
        public Guid brgy_profile_id { get; set; }
        public string old_id { get; set; }
        public int push_status_id { get; set; }

        public static System.Linq.Expressions.Expression<Func<brgy_profile, brgy_profileDTO>> SELECT =
            x => new brgy_profileDTO
            {
                lib_approval_name = x.lib_approval.name,

                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_name = x.lib_cycle.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_region_region_name = x.lib_region.region_name,
                lib_brgy_brgy_name = x.lib_brgy.brgy_name,
                brgy_profile_id = x.brgy_profile_id,
                old_id = x.old_id,
                push_status_id = x.push_status_id

            };

    }


    public class oversight_committeeDTO
    {
        public string lib_approval_name { get; set; }
        public string lib_brgy_brgy_name { get; set; }
        public string lib_city_city_name { get; set; }
        public string lib_cycle_name { get; set; }
        public string lib_fund_source_name { get; set; }
        public string lib_province_prov_name { get; set; }
        public string lib_region_region_name { get; set; }
        public Guid oversight_committee_id { get; set; }
        public string old_id { get; set; }
        public string name { get; set; }
        public DateTime? date_organized { get; set; }
        public int? no_male { get; set; }
        public int? no_female { get; set; }
        public int push_status_id { get; set; }
        public int? frequency { get; set; }

        public static System.Linq.Expressions.Expression<Func<oversight_committee, oversight_committeeDTO>> SELECT =
            x => new oversight_committeeDTO
            {
                lib_approval_name = x.lib_approval.name,

                lib_city_city_name = x.lib_city.city_name,
                lib_cycle_name = x.lib_cycle.name,
                lib_fund_source_name = x.lib_fund_source.name,
                lib_province_prov_name = x.lib_province.prov_name,
                lib_region_region_name = x.lib_region.region_name,
                oversight_committee_id = x.oversight_committee_id,
                old_id = x.old_id,
                name = x.name,
                date_organized = x.date_organized,
                no_male = x.no_male,
                no_female = x.no_female,
                frequency = x.frequency,
                push_status_id = x.push_status_id
            };

    }
}
