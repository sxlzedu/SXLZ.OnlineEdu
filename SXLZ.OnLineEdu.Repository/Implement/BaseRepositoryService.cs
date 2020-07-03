using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SXLZ.OnLineEdu.Utilies;
using System.Linq;
using System.Data;
using FreeSql;
using System.Data.Common;

namespace SXLZ.OnLineEdu.Repository
{
    public class BaseRepositoryServiceImpl : IBaseRepositoryService
    {
        private IFreeSql freeSql;
        public BaseRepositoryServiceImpl(IFreeSql freeSql)
        {
            this.freeSql = freeSql;
        }
        public bool Add<T>(T parm) where T : class, new()
        {
            return freeSql.Insert(parm).ExecuteAffrows() > 0;
        }
        public long AddReturnId<T>(T parm) where T : class, new()
        {
            return freeSql.Insert<T>().AppendData(parm).ExecuteIdentity();
        }
        public int ReturnEntityAdd<T>(List<T> parm) where T : class, new()
        {
            return freeSql.Insert(parm).ExecuteAffrows();
        }

        public bool Update<T>(T parm, Expression<Func<T, bool>> where) where T : class, new()
        {
            return freeSql.Update<T>().Where(where).SetSource(parm).ExecuteAffrows() > 0;
        }
        public bool Update<T>(T parm, string where, object whereParam = null) where T : class, new()
        {
            return freeSql.Update<T>().Where(where, whereParam).SetSource(parm).ExecuteAffrows() > 0;
        }

        public int Update<T>(Expression<Func<T, object>> exp, Expression<Func<T, bool>> where) where T : class, new()
        {
            return freeSql.Update<T>().Set(exp).Where(where).ExecuteAffrows();
        }
        public int Update<T>(List<T> parm, Expression<Func<T, bool>> where) where T : class, new()
        {
            return freeSql.Update<T>().Where(where).SetSource(parm).ExecuteAffrows();
        }
        public int AdoUpdate(string where, object param = null)
        {
            return freeSql.Ado.ExecuteNonQuery(where, param);
        }

        public bool IsExist<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return freeSql.Select<T>().Where(where).Count() > 0;
        }

        public bool IsExist<T>(string where, object param = null) where T : class, new()
        {
            return freeSql.Select<T>().Where(where, param).Count() > 0;
        }

        public int Delete<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return freeSql.Delete<T>().Where(where).ExecuteAffrows();
        }
        public int Delete<T>(string where, object param = null) where T : class, new()
        {
            return freeSql.Delete<T>().Where(where, param).ExecuteAffrows();
        }

        public async Task<List<T>> GetListAsync<T>(string where, object param = null) where T : class, new()
        {
            return await freeSql.Select<T>().Where(where, param).ToListAsync();
        }

        public List<T> GetList<T>(string where, object param = null) where T : class, new()
        {
            return freeSql.Select<T>().Where(where, param).ToList();
        }
        public List<T> GetList<T>(Expression<Func<T, bool>> where, bool flag, Expression<Func<T, object>> order, bool desc = true) where T : class, new()
        {
            var rows = freeSql.Select<T>().Where(where);
            return (desc == true ? rows.OrderByDescending(flag, order).ToList() : rows.OrderBy(flag, order).ToList());
        }
        public List<T> GetList<T>(string where, bool flag, Expression<Func<T, object>> order, bool desc = true, object param = null, int pageSize = 10) where T : class, new()
        {
            var rows = freeSql.Select<T>().Where(where, param);
            return (desc == true ? rows.OrderByDescending(flag, order).Take(pageSize).ToList() : rows.OrderBy(flag, order).Take(pageSize).ToList());
        }

        public List<T> GetAllList<T>() where T : class, new()
        {
            return freeSql.Select<T>().ToList();
        }

        public async Task<List<T>> GetAllListAsync<T>() where T : class, new()
        {
            return await freeSql.Select<T>().ToListAsync();
        }


        public List<T> GetList<T>(string where, bool flag, Expression<Func<T, object>> order, bool desc = true, object param = null) where T : class, new()
        {
            var rows = freeSql.Select<T>().Where(where, param);
            return (desc == true ? rows.OrderByDescending(order).ToList() : rows.OrderBy(order).ToList());
        }
        public T Get<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return freeSql.Select<T>().Where(where).ToOne();
        }
        public async Task<T> GetAsync<T>(string where, object param = null) where T : class, new()
        {
            return await freeSql.Select<T>().Where(where, param).ToOneAsync();
        }
        public T Get<T>(string where, object param = null) where T : class, new()
        {
            return freeSql.Select<T>().Where(where, param).ToOne();
        }
        public object GetPageList<T>(Expression<Func<T, bool>> where, int pageIndex, int pageSize, Expression<Func<T, object>> order, bool desc = true) where T : class, new()
        {
            long total = 0;
            var rows = freeSql.Select<T>().Where(where).Count(out total);
            rows = desc == true ? rows.OrderByDescending(order) : rows.OrderBy(order);
            rows = rows.Page(pageIndex, pageSize);
            return new { rows = rows.ToList(), total = total };
        }
        public object GetPageList<T>(string where, int pageIndex, int pageSize, Expression<Func<T, object>> order, bool desc = true, object param = null) where T : class, new()
        {
            long total = 0;
            var rows = freeSql.Select<T>()
            .Where(where, param)
            .Count(out total);
            rows = desc == true ? rows.OrderByDescending(order) : rows.OrderBy(order);
            rows = rows.Page(pageIndex, pageSize);
            return new { rows = rows.ToList(), total = total };
        }
        public object GetPageList<T>(string where, int pageIndex, int pageSize, Expression<Func<T, object>> order, bool desc = true) where T : class, new()
        {
            long total = 0;
            var rows = freeSql.Select<T>().WithSql(where).Count(out total);
            rows = desc == true ? rows.OrderByDescending(order) : rows.OrderBy(order);
            rows = rows.Page(pageIndex, pageSize);
            return new { rows = rows, total = total };
        }
        public long GetCount<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            return freeSql.Select<T>().Where(where).Count();
        }

        public long GetCount<T>(string where, object param) where T : class, new()
        {
            return freeSql.Select<T>().Where(where, param).Count();
        }
        public async Task<long> GetCountAsync<T>(string where, object param) where T : class, new()
        {
            return await freeSql.Select<T>().Where(where, param).CountAsync();
        }
    }
}
