using Dapper;
using StartProject.Common;
using StartProject.DB;
using StartProject.Models.Test;
using StartProject.Services.IServices;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartProject.Services
{
    public class DBDemoService : IDBDemoService
    {
        private ITestDB _testDB;
        private ICacheService _cache;
        private IMyService _myService;

        public DBDemoService(ITestDB testDB, ICacheService cache, IMyService myService)
        {
            _testDB = testDB;
            _cache = cache;
            _myService = myService;
        }

        public List<Table_1Model> Table_1_DB_Query(int? id)
        {
            // from cache
            string cacheKey = $"{_myService.GetWhoCallMethod()}_{id}";
            var registerInfoCacheJson = _cache.Get(cacheKey);

            if (registerInfoCacheJson != null)
            {
                return JsonSerializer.Deserialize<List<Table_1Model>>(Encoding.UTF8.GetString(registerInfoCacheJson));
            }


            List<Table_1Model> result = _testDB.Table_1_DB_Query(id);


            // write cache
            _cache.Set(cacheKey, result, TimeSpan.FromHours(_myService.CacheHours()));

            return result;
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
