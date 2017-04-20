using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp.DataLayer
{ 
    public class psa_solution_mapping
    {
       
        public Guid psa_solution_id { get; set; }
        public string old_id { get; set; }

        public Guid psa_problem_id { get; set; }


        public string solution { get; set; }
        public int psa_solution_category_id { get; set; }
    

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
     
        #endregion



  
    }

    public class psa_problem_mapping
    {
       

      public List<psa_solution_mapping> psa_solution_mapping { get; set; }
        public Guid psa_problem_id { get; set; }
        public string old_id { get; set; }



        public Guid community_training_id { get; set; }
   



        public int? rank { get; set; }


        public string problem { get; set; }
        public int psa_problem_category_id { get; set; }
  
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
     
        #endregion


 
        
    }
}
