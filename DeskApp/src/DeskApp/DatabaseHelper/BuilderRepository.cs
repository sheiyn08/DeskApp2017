using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DeskApp.DatabaseHelper
{
    public class BuilderRepository : IDisposable
    {
        DbConnection connection;
        DbConnetionType databaseType;
        ParameterHelper parameterHelper;

        public BuilderRepository(string connectionString,
                    DbConnetionType _databaseType)
        {
            databaseType = _databaseType;
            parameterHelper = new ParameterHelper();
            var connectionHelper = new ConnectionHelper();
            connection = connectionHelper.GetDbConnection(connectionString, _databaseType);
        }

        private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
        }

        private void CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }

        // DataReader Demo

            public void CreatePerceptionTable()
        {
            OpenConnection();
            using (var command = connection.CreateCommand())
            {

                command.CommandText = @"

                        CREATE TABLE IF NOT EXISTS  perception_survey (
                        perception_survey_id  BLOB NOT NULL,
                        access_1  INTEGER,
                        access_10  INTEGER,
                        access_11  INTEGER,
                        access_12  INTEGER,
                        access_13  INTEGER,
                        access_14  INTEGER,
                        access_15  INTEGER,
                        access_16  INTEGER,
                        access_2  INTEGER,
                        access_3  INTEGER,
                        access_4  INTEGER,
                        access_5  INTEGER,
                        access_6  INTEGER,
                        access_7  INTEGER,
                        access_8  INTEGER,
                        access_9  INTEGER,
                        approval_id  INTEGER NOT NULL,
                        age  INTEGER,
                        brgy_code  INTEGER,
                        city_code  INTEGER,
                        created_by  INTEGER NOT NULL,
                        created_date  TEXT NOT NULL,
                        date_assessed  TEXT,
                        deleted_by  INTEGER,
                        deleted_date  TEXT,
                        disaster_1  INTEGER,
                        disaster_2  INTEGER,
                        disaster_3  INTEGER,
                        disaster_4  INTEGER,
                        disaster_5  INTEGER,
                        disaster_6  INTEGER,
                        disaster_7  INTEGER,
                        disaster_8  INTEGER,
                        disaster_9  INTEGER,
                        ip_group_id  INTEGER,
                        is_deleted  INTEGER NOT NULL,
                        is_ip  INTEGER NOT NULL,
                        is_male  INTEGER NOT NULL,
                        is_pantawid  INTEGER NOT NULL,
                        is_slp  INTEGER NOT NULL,
                        last_modified_by  INTEGER,
                        last_modified_date  TEXT,
                        lib_ip_groupip_group_id  INTEGER,
                        participation_1  INTEGER,
                        participation_10  INTEGER,
                        participation_11  INTEGER,
                        participation_12  INTEGER,
                        participation_2  INTEGER,
                        participation_3  INTEGER,
                        participation_4  INTEGER,
                        participation_5  INTEGER,
                        participation_6  INTEGER,
                        participation_7  INTEGER,
                        participation_8  INTEGER,
                        participation_9  INTEGER,
                        person_name  TEXT,
                        prov_code  INTEGER NOT NULL,
                        push_date  TEXT,
                        push_status_id  INTEGER NOT NULL,
                        region_code  INTEGER NOT NULL,
                        trust_1  INTEGER,
                        trust_2  INTEGER,
                        trust_3  INTEGER,
                        trust_4  INTEGER,
                        trust_5  INTEGER,
                        trust_6  INTEGER,
                        trust_7  INTEGER,
                        trust_8  INTEGER,
                        year  INTEGER NOT NULL,
                        activity_name TEXT,
                        cycle_id INTEGER,
                        talakayan_date_from TEXT,
                        talakayan_date_to TEXT,
                        survey_date_from TEXT,
                        survey_date_to TEXT,
                        PRIMARY KEY (perception_survey_id ASC),
                        CONSTRAINT FK_perception_survey_lib_brgy_brgy_code FOREIGN KEY (brgy_code) REFERENCES lib_brgy(brgy_code) ON DELETE CASCADE,
                        CONSTRAINT FK_perception_survey_lib_city_city_code FOREIGN KEY(city_code) REFERENCES lib_city(city_code) ON DELETE CASCADE,
                        CONSTRAINT FK_perception_survey_lib_ip_group_lib_ip_groupip_group_id FOREIGN KEY(lib_ip_groupip_group_id) REFERENCES lib_ip_group(ip_group_id) ON DELETE RESTRICT,
                        CONSTRAINT FK_perception_survey_lib_province_prov_code FOREIGN KEY(prov_code) REFERENCES lib_province(prov_code) ON DELETE CASCADE,
                        CONSTRAINT FK_perception_survey_lib_region_region_code FOREIGN KEY(region_code) REFERENCES lib_region(region_code) ON DELETE CASCADE
                        CONSTRAINT FK_community_organization_lib_cycle_cycle_id FOREIGN KEY(cycle_id) REFERENCES lib_cycle(cycle_id) ON DELETE CASCADE
                        );
                ";

                command.ExecuteNonQuery();
            }
        }

        //Talakayan Table:


        public void CreateTalakayanTable()
        {
            OpenConnection();
            using (var command = connection.CreateCommand())
            {

                command.CommandText = @"

                CREATE TABLE IF NOT EXISTS  talakayan_eval (
                         talakayan_evaluation_id   BLOB NOT NULL,
                         talakayan_year INTEGER,
                         talakayan_name TEXT NOT NULL,
                         talakayan_date_start TEXT,
                         talakayan_date_end TEXT,
                         evaluation_date_start TEXT,
                         evaluation_date_end TEXT,
                         talakayan_venue   TEXT,
                         participant_type   INTEGER,
                         person_name   TEXT,
                         is_male   INTEGER NOT NULL,
                         v2016_obj   INTEGER,
                         v2016_time_alloted   INTEGER,
                         v2016_venue_a   INTEGER,
                         v2016_venue_b   INTEGER,
                         v2016_venue_c   INTEGER,
                         v2016_venue_d   INTEGER,
                         v2016_sound_system   INTEGER,
                         v2016_visual_a   INTEGER,
                         v2016_visual_b   INTEGER,
                         v2016_visual_c   INTEGER,
                         v2016_meals_a   INTEGER,
                         v2016_meals_b   INTEGER,
                         v2016_meals_c   INTEGER,
                         v2016_gallery_walk   INTEGER,
                         v2016_fgd   INTEGER,
                         v2016_comments   TEXT,
                         v2016_knowledge_part1   INTEGER,
                         v2016_knowledge_part2   INTEGER,
                         v2016_knowledge_part3   INTEGER,
                         v2016_knowledge_part4   INTEGER,
                         v2016_knowledge_part5   INTEGER,
                         v2016_overall_satisfaction   INTEGER,
                         v2016_team_effectiveness   INTEGER,
                         v2015_obj_a   INTEGER,
                         v2015_obj_b   INTEGER,
                         v2015_timeallotment_a_part1   INTEGER,
                         v2015_timeallotment_a_part2   INTEGER,
                         v2015_timeallotment_a_part3   INTEGER,
                         v2015_timeallotment_a_part4   INTEGER,
                         v2015_timeallotment_a_part5   INTEGER,
                         v2015_timeallotment_b_part1   INTEGER,
                         v2015_timeallotment_b_part2   INTEGER,
                         v2015_timeallotment_b_part3   INTEGER,
                         v2015_timeallotment_b_part4   INTEGER,
                         v2015_timeallotment_b_part5   INTEGER,
                         v2015_partsoftalakayan_part1   INTEGER,
                         v2015_partsoftalakayan_part2   INTEGER,
                         v2015_partsoftalakayan_part3   INTEGER,
                         v2015_partsoftalakayan_part4   INTEGER,
                         v2015_partsoftalakayan_part5   INTEGER,
                         v2015_evaluation_a   INTEGER,
                         v2015_evaluation_b   INTEGER,
                         v2015_venue_a   INTEGER,
                         v2015_venue_b   INTEGER,
                         v2015_venue_c   INTEGER,
                         v2015_venue_d   INTEGER,
                         v2015_venue_e   INTEGER,
                         v2015_visual_a   INTEGER,
                         v2015_visual_b   INTEGER,
                         v2015_visual_c   INTEGER,
                         v2015_visual_d   INTEGER,
                         v2015_visual_e   INTEGER,
                         v2015_satisfaction_a   INTEGER,
                         v2015_satisfaction_a_remarks   TEXT,
                         v2015_satisfaction_b   INTEGER,
                         v2015_satisfaction_b_remarks   TEXT,
                         v2015_satisfaction_c   INTEGER,
                         v2015_satisfaction_c_remarks   TEXT,
                         v2015_satisfaction_d   INTEGER,
                         v2015_satisfaction_d_remarks   TEXT,
                         v2015_satisfaction_e   INTEGER,
                         v2015_satisfaction_e_remarks   TEXT,
                         v2015_satisfaction_f   INTEGER,
                         v2015_satisfaction_f_remarks   TEXT,
                         v2015_satisfaction_g   INTEGER,
                         v2015_satisfaction_g_remarks   TEXT,
                         v2015_satisfaction_h   INTEGER,
                         v2015_satisfaction_h_remarks   TEXT,
                         v2015_satisfaction_i   INTEGER,
                         v2015_satisfaction_i_remarks   TEXT,
                         v2015_satisfaction_j   INTEGER,
                         v2015_satisfaction_j_remarks   TEXT,
                         v2015_satisfaction_k   INTEGER,
                         v2015_satisfaction_k_remarks   TEXT,
                         region_code   INTEGER NOT NULL,
                         prov_code   INTEGER NOT NULL,
                         city_code   INTEGER NOT NULL,
                         brgy_code   INTEGER,
                         created_by   INTEGER NOT NULL,
                         created_date   TEXT NOT NULL,
                         last_modified_by   INTEGER,
                         last_modified_date   TEXT,
                         is_deleted   INTEGER NOT NULL,
                         deleted_by   INTEGER,
                         deleted_date   TEXT,
                         push_status_id   INTEGER NOT NULL,
                         push_date   TEXT,
                         approval_id   INTEGER NOT NULL,
                        PRIMARY KEY (talakayan_evaluation_id ASC),
                        CONSTRAINT FK_talakayan_eval_lib_brgy_brgy_code FOREIGN KEY(brgy_code) REFERENCES lib_brgy(brgy_code) ON DELETE CASCADE,
                        CONSTRAINT FK_talakayan_eval_lib_city_city_code FOREIGN KEY(city_code) REFERENCES lib_city(city_code) ON DELETE CASCADE,
                        CONSTRAINT FK_talakayan_eval_lib_province_prov_code FOREIGN KEY(prov_code) REFERENCES lib_province(prov_code) ON DELETE CASCADE,
                        CONSTRAINT FK_talakayan_eval_lib_region_region_code FOREIGN KEY(region_code) REFERENCES lib_region(region_code) ON DELETE CASCADE
                        );
                        
                ";

                command.ExecuteNonQuery();
            }
        }

        public void Cech()
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {
                 
            }
        }
       
  public void CreateAttachedDocuments()
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {

                // Create table if not exist

                command.CommandText = @"

                CREATE TABLE IF NOT EXISTS attached_document (
                attached_document_id BLOB NOT NULL CONSTRAINT PK_attached_document PRIMARY KEY,
                approval_id INTEGER NOT NULL,
                brgy_code INTEGER,
                city_code INTEGER NOT NULL,
                count INTEGER NOT NULL,
                created_by INTEGER NOT NULL,
                created_date TEXT NOT NULL,
                cycle_id INTEGER,
                deleted_by INTEGER,
                deleted_date TEXT,
                enrollment_id INTEGER,
                fund_source_id INTEGER,
                is_deleted INTEGER,
                last_modified_by INTEGER,
                last_modified_date TEXT,
                mov_list_id INTEGER NOT NULL,
                prov_code INTEGER NOT NULL,
                push_date TEXT,
                push_status_id INTEGER NOT NULL,
                record_id BLOB NOT NULL,
                region_code INTEGER NOT NULL,
                CONSTRAINT FK_attached_document_mov_list_mov_list_id FOREIGN KEY(mov_list_id) REFERENCES mov_list(mov_list_id) ON DELETE CASCADE
            );



                ";


                


                command.ExecuteNonQuery();
            }
        }

        public void CreateUserTable()
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {

                // Create table if not exist

                command.CommandText = @"

                CREATE TABLE IF NOT EXISTS community_organization (
                    community_organization_id BLOB NOT NULL CONSTRAINT PK_community_organization PRIMARY KEY,
                    approval_id INTEGER NOT NULL,
                    brgy_code INTEGER,
                    city_code INTEGER NOT NULL,
                    created_by INTEGER NOT NULL,
                    created_date TEXT NOT NULL,
                    cycle_id INTEGER NOT NULL,
                    deleted_by INTEGER,
                    deleted_date TEXT,
                    enrollment_id INTEGER NOT NULL,
                    fund_source_id INTEGER NOT NULL,
                    is_active INTEGER,
                    is_deleted INTEGER,
                    is_formal INTEGER,
                    is_lgu_accredited INTEGER,
                    is_onm INTEGER,
                    is_sector_academe INTEGER,
                    is_sector_business INTEGER,
                    is_sector_farmer INTEGER,
                    is_sector_fisherfolks INTEGER,
                    is_sector_government INTEGER,
                    is_sector_ip INTEGER,
                    is_sector_ngo INTEGER,
                    is_sector_po INTEGER,
                    is_sector_pwd INTEGER,
                    is_sector_religios INTEGER,
                    is_sector_senior INTEGER,
                    is_sector_women INTEGER,
                    is_sector_youth INTEGER,
                    last_modified_by INTEGER,
                    last_modified_date TEXT,
                    lgu_level_id INTEGER NOT NULL,
                    list_activities TEXT,
                    list_advocacy TEXT,
                    list_area_operation TEXT,
                    name TEXT,
                    no_female INTEGER,
                    no_male INTEGER,
                    old_id TEXT,
                    organization_type_id INTEGER NOT NULL,
                    prov_code INTEGER NOT NULL,
                    push_date TEXT,
                    push_status_id INTEGER NOT NULL,
                    region_code INTEGER NOT NULL,
                    years_operating INTEGER,
                    CONSTRAINT FK_community_organization_lib_approval_approval_id FOREIGN KEY (approval_id) REFERENCES lib_approval(approval_id) ON DELETE CASCADE,
                    CONSTRAINT FK_community_organization_lib_brgy_brgy_code FOREIGN KEY(brgy_code) REFERENCES lib_brgy(brgy_code) ON DELETE RESTRICT,
                    CONSTRAINT FK_community_organization_lib_city_city_code FOREIGN KEY(city_code) REFERENCES lib_city(city_code) ON DELETE CASCADE,
                    CONSTRAINT FK_community_organization_lib_cycle_cycle_id FOREIGN KEY(cycle_id) REFERENCES lib_cycle(cycle_id) ON DELETE CASCADE,
                    CONSTRAINT FK_community_organization_lib_enrollment_enrollment_id FOREIGN KEY(enrollment_id) REFERENCES lib_enrollment(enrollment_id) ON DELETE CASCADE,
                    CONSTRAINT FK_community_organization_lib_fund_source_fund_source_id FOREIGN KEY(fund_source_id) REFERENCES lib_fund_source(fund_source_id) ON DELETE CASCADE,
                    CONSTRAINT FK_community_organization_lib_lgu_level_lgu_level_id FOREIGN KEY(lgu_level_id) REFERENCES lib_lgu_level(lgu_level_id) ON DELETE CASCADE,
                    CONSTRAINT FK_community_organization_lib_province_prov_code FOREIGN KEY(prov_code) REFERENCES lib_province(prov_code) ON DELETE CASCADE,
                    CONSTRAINT FK_community_organization_lib_region_region_code FOREIGN KEY(region_code) REFERENCES lib_region(region_code) ON DELETE CASCADE
                );




                ";


                //CREATE INDEX main.IX_community_organization_approval_id
                //    ON community_organization(approval_id ASC);
                //                    CREATE INDEX main.IX_community_organization_brgy_code
                //    ON community_organization(brgy_code ASC);
                //                    CREATE INDEX main.IX_community_organization_city_code
                //    ON community_organization(city_code ASC);
                //                    CREATE INDEX main.IX_community_organization_cycle_id
                //    ON community_organization(cycle_id ASC);
                //                    CREATE INDEX main.IX_community_organization_enrollment_id
                //    ON community_organization(enrollment_id ASC);
                //                    CREATE INDEX main.IX_community_organization_fund_source_id
                //    ON community_organization(fund_source_id ASC);
                //                    CREATE INDEX main.IX_community_organization_lgu_level_id
                //    ON community_organization(lgu_level_id ASC);
                //                    CREATE INDEX main.IX_community_organization_prov_code
                //    ON community_organization(prov_code ASC);
                //                    CREATE INDEX main.IX_community_organization_region_code
                //    ON community_organization(region_code ASC);




                command.ExecuteNonQuery();
            }
        }

        public void AddColumn()
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {

                // Create table if not exist


                Dictionary<string, string> columnNameToAddColumnSql = new Dictionary<string, string>
                    {
                        {
                            "Column1",
                            "ALTER TABLE mov_list ADD COLUMN is_deleted INTEGER"
                        },
                        {
                            "Column2",
                            "ALTER TABLE mov_list ADD COLUMN is_deleted INTEGER"
                        }
                    };

                foreach (var pair in columnNameToAddColumnSql)
                {
                    string columnName = pair.Key;
                    string sql = pair.Value;

                    try
                    {

                        if (!isFieldExist("mov_list", "is_deleted"))
                        {
                            command.CommandText = sql;
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString(), string.Format("Failed to create column [{0}]. Most likely it already exists, which is fine.", columnName));
                    }
                }



                command.ExecuteNonQuery();
            }
        }

        public bool isFieldExist(string table, string column)
        {
            OpenConnection();

            using (var command = connection.CreateCommand())
            {


                // make sure table exists
                command.CommandText = string.Format("SELECT sql FROM sqlite_master WHERE type = 'table' AND name = '{0}'", table);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //does column exists?
                    bool hascol = reader.GetString(0).Contains(String.Format("\"{0}\"", column));

                    return hascol;
                }

                return false;
            }



        }




        public void Dispose()
        {
            CloseConnection();
        }
    }
}
