using StartProject.Models.Test;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services.IServices
{
    public interface IDBDemoService
    {
        public List<Table_1Model> Table_1_DB_Query(int? id);
        public BaseViewModel<List<Table_1Model>> Table_1_DB_Query_Output();
        public BaseViewModel Table_1_Execute_Output();
        public Table_1Model Table_1_DB_QueryFirstOrDefault(int id);


    }
}
