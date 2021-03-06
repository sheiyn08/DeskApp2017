using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeskApp.DataLayer;
using DeskApp.Data;
using Microsoft.EntityFrameworkCore;
//using AutoMapper;
using DeskApp.Controllers.Repository;

namespace DeskApp.Controllers
{

    [Route("api/offline/v1/trainings/psa")]
    public class TrainingPSAController : Controller
    {


        public static string url = @"https://ncddpdb.dswd.gov.ph";
        //public static string url = @"http://10.10.10.157:9999"; //---- to be used for testing

        private readonly ApplicationDbContext db;
        private readonly IPSARepository psa;


        public TrainingPSAController(ApplicationDbContext context, IPSARepository _psa)
        {
            db = context;
            psa = _psa;
        }
        [Route("problem/get_solution")]
        public IActionResult GetProblemSolution(Guid id)
        {
            var solution = db.psa_solution.Where(x => x.psa_problem_id == id && x.is_deleted != true);
            return Ok(solution);
        }

        [Route("problem/get_mapped")]
        public IActionResult GetPSAProblem(
                     Guid community_training_id
           )
        {

            var training = db.community_training.FirstOrDefault(x => x.community_training_id == community_training_id);

            if (training == null)
            {
                return null;
            }

            if (training.is_deleted == true)
            {
                return null;
            }

            var container = new List<psa_problem_mapping>();

            var problems = db.psa_problem.Where(x => x.community_training_id == community_training_id && x.is_deleted != true);

            //var model = Mapper.DynamicMap<List<psa_problem>, List<psa_problem_mapping>>(problems.ToList());


            //foreach (var i in model)
            //{

            //    var sol = db.psa_solution.Where(x => x.psa_problem_id == i.psa_problem_id && x.is_deleted != true).ToList();

            //    var solutions = Mapper.DynamicMap<List<psa_solution>, List<psa_solution_mapping>>(sol.ToList());

            //    i.psa_solution_mapping = solutions;


            //}

            return Ok(problems);
        }

        [Route("problem/save")]
        public async Task<IActionResult> SaveProblem(psa_problem model, bool? api)
        {
            // var i = psa.SaveProblem(model, false);
            //return Ok(i);

            var record = db.psa_problem.AsNoTracking().FirstOrDefault(x => x.psa_problem_id == model.psa_problem_id);
            var main_record = db.community_training.FirstOrDefault(x => x.community_training_id == model.community_training_id);

            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.psa_problem_id = Guid.NewGuid();
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;

                    int rank = db.psa_problem.Where(x => x.community_training_id == model.community_training_id && x.is_deleted != true).Count();
                    model.rank = rank + 1;

                    //v3.1 any changes on problem, main training record should be updated as well
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;
                    db.Entry(main_record).State = EntityState.Modified;
                }                
                else
                {
                    model.push_status_id = 1;
                }
                
                db.psa_problem.Add(model);                

                try
                {
                    await db.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
            else
            {
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    //v3.1 any changes on problem, main training record should be updated as well
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;
                    db.Entry(main_record).State = EntityState.Modified;
                }                

                model.created_by = record.created_by;
                model.created_date = record.created_date;
                
                db.Entry(model).State = EntityState.Modified;                

                try
                {
                    await db.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
        }


        [Route("solution/save")]
        public async Task<IActionResult> SaveSolution(psa_solution model, bool? api)
        {
            var record = db.psa_solution.AsNoTracking().FirstOrDefault(x => x.psa_solution_id == model.psa_solution_id);
            var problem = db.psa_problem.FirstOrDefault(x => x.psa_problem_id == model.psa_problem_id);
            var main_record = db.community_training.FirstOrDefault(x => x.community_training_id == problem.community_training_id);
            
            if (record == null)
            {
                if (api != true)
                {
                    model.push_status_id = 2;
                    model.push_date = null;
                    model.approval_id = 3;
                    model.created_by = 0;
                    model.created_date = DateTime.Now;
                    model.is_deleted = false;
                    model.psa_solution_id = Guid.NewGuid();

                    //v3.1 any changes on problem and solution, main training record should be updated as well
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;

                    //v3.1 any changes on solution, problem should be updated as well
                    problem.push_status_id = 3;
                    problem.last_modified_by = 0;
                    problem.last_modified_date = DateTime.Now;

                    db.Entry(problem).State = EntityState.Modified;
                    db.Entry(main_record).State = EntityState.Modified;
                }
                else
                {
                    model.push_status_id = 1;
                }               
                
                db.psa_solution.Add(model);                

                try
                {
                    await db.SaveChangesAsync();
                    return Ok(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }

            else
            {
                if (api != true)
                {
                    model.push_status_id = 3;
                    model.approval_id = 3;
                    model.push_date = null;
                    model.last_modified_by = 0;
                    model.last_modified_date = DateTime.Now;

                    //v3.1 any changes on solution, problem should be updated as well
                    problem.push_status_id = 3;
                    problem.last_modified_by = 0;
                    problem.last_modified_date = DateTime.Now;

                    //v3.1 any changes on problem and solution, main training record should be updated as well
                    main_record.push_status_id = 3;
                    main_record.last_modified_by = 0;
                    main_record.last_modified_date = DateTime.Now;

                    db.Entry(problem).State = EntityState.Modified;
                    db.Entry(main_record).State = EntityState.Modified;
                }
                
                model.created_by = record.created_by;
                model.created_date = record.created_date;
                
                db.Entry(model).State = EntityState.Modified;                

                try
                {
                    await db.SaveChangesAsync();
                    return Ok(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
        }



        [HttpPost]
        [Route("problem/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var record = db.psa_problem.FirstOrDefault(x => x.psa_problem_id == id);
            var solutions = db.psa_solution.Where(x => x.psa_problem_id == id);
            var main_record = db.community_training.FirstOrDefault(x => x.community_training_id == record.community_training_id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            //v3.1 when problem is deleted, main training record should be updated
            main_record.push_status_id = 3;
            main_record.last_modified_by = 0;
            main_record.last_modified_date = DateTime.Now;

            foreach (var sol in solutions)
            {
                sol.is_deleted = true;
                sol.push_status_id = 3;
                sol.deleted_by = 0;
                sol.deleted_date = DateTime.Now;
            }
            
            await db.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("solution/delete")]
        public async Task<IActionResult> DeleteSolution(Guid id)
        {
            var record = db.psa_solution.FirstOrDefault(x => x.psa_solution_id == id);
            var problem = db.psa_problem.FirstOrDefault(x => x.psa_problem_id == record.psa_problem_id);
            var main_record = db.community_training.FirstOrDefault(x => x.community_training_id == problem.community_training_id);

            record.is_deleted = true;
            record.push_status_id = 3;
            record.deleted_by = 0;
            record.deleted_date = DateTime.Now;

            //v3.1 once deleted, problem should be updated as well
            problem.push_status_id = 3;
            problem.last_modified_by = 0;
            problem.last_modified_date = DateTime.Now;

            //v3.1 once deleted, main training record should be updated as well
            main_record.push_status_id = 3;
            main_record.last_modified_by = 0;
            main_record.last_modified_date = DateTime.Now;

            await db.SaveChangesAsync();
            return Ok();
        }
    }
}