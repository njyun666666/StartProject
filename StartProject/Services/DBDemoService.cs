using Dapper;
using StartProject.DB;
using StartProject.Models.Test;
using StartProject.Services.IServices;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services
{
    public class DBDemoService : IDBDemoService
    {
        private ITestDB _testDB;
        public DBDemoService(ITestDB testDB )
        {
            _testDB = testDB;
        }

        public List<Table_1Model> Table_1_DB_Query(int? id)
        {
            return _testDB.Table_1_DB_Query(id);
        }

        public List<Table_1Model> Table_1_DB_Query_Output()
        {
            DynamicParameters parameters = new DynamicParameters();
            List<Table_1Model> list = _testDB.Table_1_DB_Query_Output(1, ref parameters);
            return list;
        }

        public Table_1Model Table_1_DB_QueryFirstOrDefault(int id)
        {
            return _testDB.Table_1_QueryFirstOrDefault(id);
        }

        public string Table_1_Execute_Output()
        {
            DynamicParameters parameters = _testDB.Table_1_Execute_Output(1);
            return parameters.Get<string>("@message");
        }
    }
}
