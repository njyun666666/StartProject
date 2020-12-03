﻿using Dapper;
using StartProject.DB.DBClass;
using StartProject.Models.Test;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.DB
{
    public class TestDB : DBConnection
    {
        public TestDB()
        {
            DbName = "Test";
            Connection(DbName);
        }


        public List<Table_1Model> Table_1_Get(int? id)
        {
            // DB_Query
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            return SystemDB.DB_Query<Table_1Model>(str_conn, "SP_Table_Get", parameters);
        }
        public List<Table_1Model> Table_1_ref(int? id, ref DynamicParameters parameters)
        {
            // DB_Query
            parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@code", dbType: DbType.Int32, size: 100, direction: ParameterDirection.Output);
            parameters.Add("@message", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

            return SystemDB.DB_Query<Table_1Model>(str_conn, "SP_Table_Get",ref parameters);
        }

        public Table_1Model Table_1_Single(int? id)
        {
            // DB_Query
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            return SystemDB.DB_Query_Single<Table_1Model>(str_conn, "SP_Table_Get", parameters);
        }


        public DynamicParameters Table_1_Para_Output(int? id)
        {
            // DB_Query
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@code", dbType: DbType.Int32, size: 100, direction: ParameterDirection.Output);
            parameters.Add("@message", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

            SystemDB.DB_Execute_Output(str_conn, "SP_Table_Get", ref parameters);

            //int code = parameters.Get<int>("@code");
            //string message = parameters.Get<string>("@message");

            return parameters;
        }


    }
}
