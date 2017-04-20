//using DeskApp.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DeskApp
//{
//    public static class Helpers
//    {
//        private readonly ApplicationDbContext db;


//        private Helpers(ApplicationDbContext context)
//        {
//            db = context;

//        }

//        public static class TableNamer
//        {
         
//            public static int table_name_id(string table_name)
//            {
               
//            var table = db.table_name.FirstOrDefault(x => x.name == table_name);

//                if (table == null)
//                {
//                    var x = new table_name
//                    {
//                        description = "",
//                        name = table_name
//                    };
//            db.table_name.Add(x);
//                    db.SaveSystemAudit();

//                    return x.table_name_id;
//                }
//                else
//                {
//                    return table.table_name_id;
//                }

//}
//        }
//    }
//}
