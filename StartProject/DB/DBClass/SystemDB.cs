using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace StartProject.DB.DBClass
{
    public class SystemDB
    {

        //DynamicParameters parameters = new DynamicParameters();
        //parameters.Add("@id", id);
        //parameters.AddTable("@paraTable", "table", list);
        //parameters.Add("@code", dbType: DbType.Int32, size: 100, direction: ParameterDirection.Output);
        //SystemDB.DB_Execute_Output(str_conn, "sp_name", ref parameters);
        //int code = parameters.Get<int>("@code");


        #region DB_Query 資料庫連線
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="str_conn">連線字串</param>
        /// <param name="sp_name">SP 名稱</param>
        /// <param name="parameters">輸入的值與類型</param>
        static public List<T> DB_Query<T>(string str_conn, string sqlStatement, DynamicParameters parameters = null, IDbTransaction trans = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str_conn))
                {
                    return conn.Query<T>(sqlStatement, parameters, trans, commandType: type).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Select and output parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str_conn"></param>
        /// <param name="sp_name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        static public List<T> DB_Query<T>(string str_conn, string sqlStatement, ref DynamicParameters parameters , IDbTransaction trans = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str_conn))
                {
                    return conn.Query<T>(sqlStatement, parameters, trans, commandType: type).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region DB_QueryFirstOrDefault 資料庫連線
        /// <summary>
        /// select 1 rows
        /// </summary>
        /// <param name="str_conn">連線字串</param>
        /// <param name="sp_name">SP 名稱</param>
        /// <param name="parameters">輸入的值與類型</param>
        static public T DB_QueryFirstOrDefault<T>(string str_conn, string sqlStatement, DynamicParameters parameters = null, IDbTransaction trans = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str_conn))
                {
                    return conn.QueryFirstOrDefault<T>(sqlStatement, parameters, trans, commandType: type);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// select 1 rows and output parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str_conn"></param>
        /// <param name="sp_name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        static public T DB_QueryFirstOrDefault<T>(string str_conn, string sqlStatement, ref DynamicParameters parameters, IDbTransaction trans = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str_conn))
                {
                    return conn.QueryFirstOrDefault<T>(sqlStatement, parameters, trans, commandType: type);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        #endregion



        static public int DB_Execute(string str_conn, string sqlStatement, DynamicParameters parameters = null, IDbTransaction trans = null, bool DoTransaction = true)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(str_conn))
            {
                if (DoTransaction)
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            result = conn.Execute(sqlStatement, parameters, transaction);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    try
                    {
                        result = conn.Execute(sqlStatement, parameters, trans);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }


            }

            return result;
        }



        #region DB_Execute_Output
        /// <summary>
        /// exec sp and output parameters
        /// </summary>
        /// <param name="str_conn"></param>
        /// <param name="sp_name"></param>
        /// <param name="parameters"></param>
        static public void DB_Execute_Output(string str_conn, string sp_name, ref DynamicParameters parameters, IDbTransaction trans = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str_conn))
                {
                    conn.Execute(sp_name, parameters, trans, commandType: type);
                }
            }
            catch (Exception ex)
            {
                //return null;
            }
        }
        #endregion

    }
}
