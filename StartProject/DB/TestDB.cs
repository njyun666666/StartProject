using Dapper;
using Microsoft.Extensions.Configuration;
using StartProject.DB.DBClass;
using StartProject.Models.Test;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.DB
{
    public interface ITestDB
    {
        public List<Table_1Model> Table_1_DB_Query(int? id);
        public List<Table_1Model> Table_1_DB_Query_Output(int? id, ref DynamicParameters parameters);
        public Table_1Model Table_1_QueryFirstOrDefault(int id);
        public DynamicParameters Table_1_Execute_Output(int? id);

    }

    public class TestDB : ITestDB
    {
        
        public readonly string DbName = "Test";
        public string str_conn;

        public IDBConnection _dBConnection;

        public TestDB(IDBConnection dBConnection)
        {
            _dBConnection = dBConnection;
            str_conn = _dBConnection.Connection(DbName);
        }


        public List<Table_1Model> Table_1_DB_Query(int? id)
        {
            // DB_Query
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            return SystemDB.DB_Query<Table_1Model>(str_conn, "SP_Table_Get", parameters);
        }
        public List<Table_1Model> Table_1_DB_Query_Output(int? id, ref DynamicParameters parameters)
        {
            // DB_Query
            parameters.Add("@id", id);
            parameters.Add("@code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@message", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

            return SystemDB.DB_Query<Table_1Model>(str_conn, "SP_Table_Get",ref parameters);
        }

        public Table_1Model Table_1_QueryFirstOrDefault(int id)
        {
            // DB_QueryFirstOrDefault
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            return SystemDB.DB_QueryFirstOrDefault<Table_1Model>(str_conn, "SP_Table_Get", parameters);
        }


        public DynamicParameters Table_1_Execute_Output(int? id)
        {
            // DB_Execute_Output
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@message", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

            SystemDB.DB_Execute_Output(str_conn, "SP_Table_Get", ref parameters);

            //int code = parameters.Get<int>("@code");
            //string message = parameters.Get<string>("@message");

            return parameters;
        }


    }
}
