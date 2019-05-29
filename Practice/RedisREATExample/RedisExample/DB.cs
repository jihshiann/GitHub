using Dapper;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Redis_LinuxServer
{
    public sealed class DBList//sealed?
    {
        public static readonly string redisDB = ConfigurationManager.ConnectionStrings["RedisServer"].ConnectionString;
        public static readonly string erpDB = ConfigurationManager.ConnectionStrings["erpDB"].ConnectionString;
    }

    public class RedisFactory
    {
        #region -Properties-
        private static readonly Lazy<ConnectionMultiplexer> Connection;
        private static readonly IServer RedisServer;
        #endregion

        #region -建構子-
        static RedisFactory()
        {
            var options = ConfigurationOptions.Parse(DBList.redisDB);
            Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
            RedisServer = GetConnection().GetServer(options.EndPoints.First());
        }
        #endregion

        #region -Methods-
        public static ConnectionMultiplexer GetConnection() { return Connection.Value; }
        public static IDatabase GetDatabase() { return GetConnection().GetDatabase(); }//GetDatabase?
        public static IServer GetServer(string hostAndPort = null) { return string.IsNullOrWhiteSpace(hostAndPort) ? RedisServer : Connection.Value.GetServer(hostAndPort); }
        #endregion
    }

    public abstract class Redis
    {
        /// <summary>
        /// 數據庫
        /// </summary>
        protected IDatabase _db;
        /// <summary>
        /// 數據庫
        /// </summary>
        protected IServer _server;


        public Redis()
        {
            _db = RedisFactory.GetDatabase();
            _server = RedisFactory.GetServer();
        }

        /// <summary>
        /// 設置
        /// </summary>
        /// <param name="key">鍵</param>
        /// <param name="data">值</param>
        /// <param name="expireTime">時間</param>
        public virtual void Set(string key, object data, TimeSpan? expireTime = null)
        {
            if (data == null)
            {
                return;
            }
            var entryStr = Serialize(data);
            var expiresIn = expireTime == null ? TimeSpan.FromSeconds(-1) : expireTime;

            _db.StringSet(key, entryStr, expiresIn);
        }

        /// <summary>
        /// 根據鍵獲取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual List<string> GetAllKeys()
        {
            List<string> rValue = _server.Keys().Select(s => (string)s).ToList();

            if (rValue?.Any() != true)
            {
                return new List<string>();
            }

            return rValue;
        }

        /// <summary>
        /// 根據鍵獲取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string Get(string key)
        {
            var rValue = _db.StringGet(key);

            if (!rValue.HasValue)
            {
                return string.Empty;
            }

            return rValue;
        }

        /// <summary>
        /// 判斷是否已經設置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string</returns>
        private string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }

    public class Repository : Redis
    {

        private string _sqlstr;
        private DynamicParameters _sqlparams;

        public List<EratStruct> GetErat()
        {
            GetEratSql();
            using (SqlConnection conn = new SqlConnection(DBList.erpDB))
            {
                conn.Open();
                return conn.Query<EratStruct>(_sqlstr, _sqlparams).ToList();
            }
        }

        private void GetEratSql()
        {
            _sqlstr = string.Concat(new object[]
            {
                    "SELECT DISTINCT A.TB31_CURR CURR", System.Environment.NewLine,
                    "	 , A.TB31_TCURR TCURR", System.Environment.NewLine,
                    "     , A.TB31_ERAT ERAT", System.Environment.NewLine,
                    "  FROM [ERP].[DBO].[FNTBM31] A", System.Environment.NewLine,
                    " WHERE A.TB31_ERATKIND = 'WEBHT'", System.Environment.NewLine,
                    "   AND A.TB31_STS = 0", System.Environment.NewLine,
                    " ORDER BY A.TB31_CURR", System.Environment.NewLine,
            });
            _sqlparams = new Dapper.DynamicParameters();
        }
    }
}