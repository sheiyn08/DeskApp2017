using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeskApp.DataLayer;

namespace DeskApp.Controllers.Repository
{
    public interface IPSARepository
    {

        Task<psa_problem> SaveProblem(psa_problem model, bool? api);
        Task<psa_solution> SaveSolution(psa_solution model, bool? api);

        Task<mibf_criteria> SaveCriteria(mibf_criteria model, bool? api);

        Task<mibf_prioritization> SavePriority(mibf_prioritization model, bool? api);

        int table_name_id(string name);

    }
}
