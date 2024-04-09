using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Data
{
    public interface IDapper
    {
        DbConnection GetDbconnection();
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        Task<int> ExecuteAsync(string sp, object parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        int GetOutputParaByCommand(string command, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        T Get<T>(string sp, object parms, CommandType commandType = CommandType.Text, ConnectionHelper connectionHelper = null);
        Task<T> GetAsync<T>(string sp, object parms, CommandType commandType = CommandType.Text, ConnectionHelper connectionHelper = null);
        //Task SaveDBError(string Error, string ClassName, string MethodName);
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        Task<List<T>> GetAllAsync<T>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        Task<dynamic> GetMultiple<T1, T2>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        Task<DapperMultiResponse<T, T2>> GetAllMultiAsync<T, T2>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure, ConnectionHelper connectionHelper = null);
        //Task<IEnumerable<T>> GetAllAsync<T>(string sp, CommandType commandType);
    }
}
