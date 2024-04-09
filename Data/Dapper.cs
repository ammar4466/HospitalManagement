using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class Dapper : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DBCon";
        private ConnectionStrings _connectionStrings;
        public Dapper(IConfiguration config, ConnectionStrings connectionStrings)
        {
            _config = config;
            _connectionStrings = connectionStrings;
        }
        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ExecuteAsync(string sp, object parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            //using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                return await db.ExecuteAsync(sp, parms, commandType: commandType);
            }
        }
        public int GetOutputParaByCommand(string command, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            int returnValue = 0;
            parms.Add("@returnVal", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                connection.Open();
                connection.Execute(command, parms, commandType: commandType);
                returnValue = parms.Get<int>("@returnVal");
            }

            return returnValue;
        }
        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text, ConnectionHelper connectionHelper = null)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
            }
        }

        public T Get<T>(string sp, object parms, CommandType commandType = CommandType.Text, ConnectionHelper connectionHelper = null)
        {
            //using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
            }
        }
        public async Task<T> GetAsync<T>(string sp, object parms, CommandType commandType = CommandType.Text, ConnectionHelper connectionHelper = null)
        {
            T result;
            try
            {
                using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
                {
                    var response = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                    return response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //await SaveDBError(ex.Message, this.GetType().Name, $"{nameof(GetAsync)}-->{sp}");
                throw new Exception(ex.Message);
            }
        }
        //public async Task SaveDBError(string Error, string ClassName, string MethodName)
        //{
        //    using (IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring)))
        //        await db.ExecuteAsync("Proc_PageErrorLog", new { ClsName = ClassName, FnName = MethodName, UserID = 0, Error, LoginTypeID = 0 }, commandType: CommandType.StoredProcedure);
        //}
        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                return db.Query<T>(sp, parms, commandType: commandType).ToList();
            }
        }
        public async Task<List<T>> GetAllAsync<T>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
                {
                    return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).ToList();
                }
            }
            catch (Exception ex)
            {
                //SaveDBError(ex.Message, this.GetType().Name, "GetAllAsync");
                throw ex;
            }
        }
        public async Task<DapperMultiResponse<T, T2>> GetAllMultiAsync<T, T2>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            DapperMultiResponse<T, T2> dapperMultiResponse = new DapperMultiResponse<T, T2>();
            try
            {
                using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
                {
                    using (var data = await db.QueryMultipleAsync(sp, parms, commandType: commandType))
                    {
                        dapperMultiResponse.Result = data.Read<T>();
                        if (!data.IsConsumed)
                        {
                            dapperMultiResponse.Result2 = data.Read<T2>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dapperMultiResponse;
        }
        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            T result;
            using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var tran = db.BeginTransaction())
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            T result;
            using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var tran = db.BeginTransaction())
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }
            }
            return result;
        }
        public async Task<dynamic> GetMultiple<T1, T2>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null)
        {
            using (IDbConnection db = new SqlConnection(GetConnectionString(connectionHelper)))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    var result = await db.QueryMultipleAsync(sp, parms, commandType: commandType).ConfigureAwait(false);

                    var res = new
                    {
                        Table1 = result.Read<T1>(),
                        Table2 = !result.IsConsumed ? result.Read<T2>() : null
                    };
                    return res;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }
            }
        }
        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }
        private string GetConnectionString(ConnectionHelper helper)
        {
            helper = helper != null ? helper : new ConnectionHelper();
            string cs = _connectionStrings.DBCon;
            switch (helper.Type)
            {
                case Data.ConnectionStringType.CurrentMonth:
                    cs = _connectionStrings.CurrentMonth;
                    break;
                case ConnectionStringType.PreviousMonth:
                    cs = _connectionStrings.CurrentMonth.Replace("MM_YYYY", helper.MM_YYYY);
                    break;
                case ConnectionStringType.AllPlanAPI:
                    cs = _connectionStrings.All_Plan_Sync;
                    break;
                default:
                    cs = _connectionStrings.DBCon;
                    break;
            }
            return cs;
        }
    }
}
