using StartProject.Models.Test;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services.IServices
{
    public interface IStart2Service
    {
        public List<Table_1Model> Table_1_Get(int? id);
        public BaseViewModel Table_1_Para_Output();
        public BaseViewModel<Table_1Model> Table_1_ref();
    }
}
