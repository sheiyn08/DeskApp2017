using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskApp.Data;
using DeskApp.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace DeskApp.Controllers.Repository
{
    public class PSARepository : IPSARepository
    {
        private readonly ApplicationDbContext db;

        public PSARepository(ApplicationDbContext context)
        {
            db = context;

        }


        public int table_name_id(string name)
        {

         
            var model =   db.table_name.FirstOrDefault(x => x.name == name);

            return model.table_name_id;

        }
        public async Task<psa_problem> SaveProblem(psa_problem model, bool? api)
        {



            var record = db.psa_problem.AsNoTracking().FirstOrDefault(x => x.psa_problem_id == model.psa_problem_id);

            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;


                    int rank =
                        db.psa_problem.Where(
                            x => x.community_training_id == model.community_training_id && x.is_deleted != true).Count();

                    model.rank = rank + 1;
                }

                model.psa_problem_id = Guid.NewGuid();
                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;
                db.psa_problem.Add(model);


                await db.SaveChangesAsync();
                return model;

            }
            else
            {
                model.push_date = null;


                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

                db.Entry(model).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return model;


            }
        }



        public async Task<psa_solution> SaveSolution(psa_solution model, bool? api)
        {



            var record = db.psa_solution.AsNoTracking().FirstOrDefault(x => x.psa_solution_id == model.psa_solution_id);

            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }


                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;
                model.psa_solution_id = Guid.NewGuid();

                db.psa_solution.Add(model);


                await db.SaveChangesAsync();

                return model;

            }
            else
            {
                model.push_date = null;


                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

                db.Entry(model).State = EntityState.Modified;


                await db.SaveChangesAsync();
                return model;


            }
        }


        public async Task<mibf_criteria> SaveCriteria(mibf_criteria model, bool? api)
        {



            var record = db.mibf_criteria.AsNoTracking().FirstOrDefault(x => x.mibf_criteria_id == model.mibf_criteria_id);

            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }


                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;
             
                db.mibf_criteria.Add(model);


                await db.SaveChangesAsync();

                return model;

            }
            else
            {
                model.push_date = null;


                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

                db.Entry(model).State = EntityState.Modified;


                await db.SaveChangesAsync();
                return model;


            }
        }

        public async Task<mibf_prioritization> SavePriority(mibf_prioritization model, bool? api)
        {



            var record = db.mibf_prioritization.AsNoTracking().FirstOrDefault(x => x.mibf_prioritization_id == model.mibf_prioritization_id);

            if (record == null)
            {


                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                }


                model.created_by = 0;
                model.created_date = DateTime.Now;

                model.is_deleted = false;

                db.mibf_prioritization.Add(model);


                await db.SaveChangesAsync();

                return model;

            }
            else
            {
                model.push_date = null;


                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                }



                model.created_by = record.created_by;
                model.created_date = record.created_date;


                model.last_modified_by = 0;
                model.last_modified_date = DateTime.Now;

                db.Entry(model).State = EntityState.Modified;


                await db.SaveChangesAsync();
                return model;


            }
        }

        public async void Delete(Guid id)
        {


            var record = db.psa_problem.FirstOrDefault(x => x.psa_problem_id == id);

            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

           

        }

        [HttpPost]
        [Route("solution/delete")]
        public async void DeleteSolution(Guid id)
        {


            var record = db.psa_solution.FirstOrDefault(x => x.psa_solution_id == id);

            record.is_deleted = true;
            record.push_status_id = 3;

            await db.SaveChangesAsync();

           

        }


    }
}
