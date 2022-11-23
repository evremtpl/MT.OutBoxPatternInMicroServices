using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.OutboxJob.Service.Context
{
    public static class OrderSingletonDatabase
    {
        static OrderSingletonDatabase()
        {
            _connection = new SqlConnection("server=.\\MERVESERVER; database=PersonSODb; user id=sa; password=951413Mt.X");
        }

        static IDbConnection _connection;
        public static IDbConnection Connection
        {
            get
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection;
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql)
            => await _connection.QueryAsync<T>(sql);
        public static async Task<int> ExecuteAsync(string sql)
            => await _connection.ExecuteAsync(sql);

        static bool _dataReaderState = true;
        public static bool DataReaderState { get => _dataReaderState; }

        public static void DataReaderReady() => _dataReaderState = true;
        public static void DataReaderBusy() => _dataReaderState = false;
    }
}
