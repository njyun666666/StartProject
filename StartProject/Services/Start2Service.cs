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
    public class Start2Service : IStart2Service
    {
        TestDB testDB = new TestDB();

        public List<Table_1Model> Table_1_Get(int? id)
        {
            return testDB.Table_1_Get(id);
        }

        public BaseViewModel<Table_1Model> Table_1_ref()
        {
            BaseViewModel<Table_1Model> baseViewModel = new BaseViewModel<Table_1Model>();
            DynamicParameters parameters = null;
            List<Table_1Model> list = testDB.Table_1_ref(1, ref parameters);

            baseViewModel.code = parameters.Get<int>("@code");
            baseViewModel.message = parameters.Get<string>("@message");
            baseViewModel.data = list;

            return baseViewModel;
        }

        public BaseViewModel Table_1_Para_Output()
        {
            BaseViewModel baseViewModel = new BaseViewModel();

            DynamicParameters parameters = testDB.Table_1_Para_Output(1);

            baseViewModel.code = parameters.Get<int>("@code");
            baseViewModel.message = parameters.Get<string>("@message");
            
            return baseViewModel;
        }
    }
}
