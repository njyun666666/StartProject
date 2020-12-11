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
        TestDB testDB = new TestDB();

        public List<Table_1Model> Table_1_DB_Query(int? id)
        {
            return testDB.Table_1_DB_Query(id);
        }

        public BaseViewModel<List<Table_1Model>> Table_1_DB_Query_Output()
        {
            BaseViewModel<List<Table_1Model>> baseViewModel = new BaseViewModel<List<Table_1Model>>();
            DynamicParameters parameters = null;
            List<Table_1Model> list = testDB.Table_1_DB_Query_Output(1, ref parameters);

            baseViewModel.code = parameters.Get<int>("@code");
            baseViewModel.message = parameters.Get<string>("@message");
            baseViewModel.data = list;

            return baseViewModel;
        }

        public Table_1Model Table_1_DB_QueryFirstOrDefault(int id)
        {
            return testDB.Table_1_QueryFirstOrDefault(1);
        }

        public BaseViewModel Table_1_Execute_Output()
        {
            BaseViewModel baseViewModel = new BaseViewModel();

            DynamicParameters parameters = testDB.Table_1_Execute_Output(1);

            baseViewModel.code = parameters.Get<int>("@code");
            baseViewModel.message = parameters.Get<string>("@message");
            
            return baseViewModel;
        }
    }
}
