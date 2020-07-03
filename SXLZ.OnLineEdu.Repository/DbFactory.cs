using System;
using SXLZ.OnLineEdu.Utilies;
using FreeSql;
namespace SXLZ.OnLineEdu.Repository
{
    public sealed class DbFactory
    {
        static IFreeSql freeSql;
        private static readonly object objectClient_lock = new object();
        private static string conn = ConfigExtensions.Configuration["DbConnection:ConnectionString"];
        private static string databaseType = ConfigExtensions.Configuration["DbConnection:Dbtype"];

        public static IFreeSql Instance()
        {
            if (freeSql == null)
            {
                lock (objectClient_lock)
                {
                    switch (databaseType.ToLower())
                    {
                        case "mysql":
                            freeSql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(FreeSql.DataType.MySql, conn)
                            .Build();
                            break;
                        case "oracle":
                            freeSql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(FreeSql.DataType.Oracle, conn)
                            .Build();
                            break;
                        case "sqlite":
                            freeSql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(FreeSql.DataType.Sqlite, conn)
                            .Build();
                            break;
                        case "postgresql":
                            freeSql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(FreeSql.DataType.PostgreSQL, conn)
                            .Build();
                            break;
                        case "sqlserver":
                        default:
                            freeSql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(FreeSql.DataType.SqlServer, conn)
                            .Build();
                            break;
                    }
                }
            }
            freeSql.Aop.CurdAfter += (s, e) =>
            {
                Console.WriteLine($"线程：{e.Sql}\r\n");
                if (e.ElapsedMilliseconds > 200)
                {
                    //记录日志
                    //发送短信给负责人
                }
            };
            return freeSql;
        }
        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="type"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        IFreeSql GetClient(string type, string connStr)
        {
            return new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(GetDbType(type), connStr)
                            .Build();
        }

        static DataType GetDbType(string dbtype)
        {
            DataType type = DataType.MySql;
            switch (dbtype.ToLower())
            {
                case "mysql":
                    type = DataType.MySql;
                    break;
                case "sqlserver":
                    type = DataType.SqlServer;
                    break;
                case "oracle":
                    type = DataType.Oracle;
                    break;
                case "sqlite":
                    type = DataType.Sqlite;
                    break;
                default:
                    type = DataType.MySql;
                    break;
            }
            return type;
        }
    }
}