using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SXLZ.OnLineEdu.Utilies;

namespace SXLZ.OnLineEdu.Repository
{
    public interface IBaseRepositoryService
    {
        /// <summary>
        /// 添加一条记录，返回成功与否
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        bool Add<T>(T parm) where T : class, new();

        /// <summary>
        /// 添加一条记录，返回唯一id
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        long AddReturnId<T>(T parm) where T : class, new();

        /// <summary>
        /// 添加一个记录，返回是否添加成功
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        int ReturnEntityAdd<T>(List<T> parm) where T : class, new();

        /// <summary>
        /// 根据条件返回是否更新成功
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="sql">id = ?id</param>
        /// <param name="whereParam">更新的条件 new { id = 1 }</param>
        /// <returns></returns>
        bool Update<T>(T parm, string where, object whereParam = null) where T : class, new();
        /// <summary>
        /// 根据条件返回是否更新成功
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Update<T>(T parm, Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 根据条件返回是否更新成功，更新可以是部分列 new {Topic=1,Title=234234};
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int Update<T>(Expression<Func<T, object>> exp, Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 批量更新，根据条件返回是否更新成功
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int Update<T>(List<T> parm, Expression<Func<T, bool>> where) where T : class, new();
        /// <summary>
        /// ado更新条件
        /// </summary>
        /// <param name="sql">纯sql语句 update t set t.Id=@Id</param>
        /// <param name="param">new {Id=1}</param>
        /// <returns></returns>
        int AdoUpdate(string where, object param);
        /// <summary>
        /// 返回记录是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool IsExist<T>(Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 返回记录是否存在
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        bool IsExist<T>(string where, object param = null) where T : class, new();

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Delete<T>(Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Delete<T>(string where, object param = null) where T : class, new();
        /// <summary>
        /// 返回查询条件的匹配操作记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="flag">order是否起作用</param>
        /// <param name="order">排序条件</param>
        /// <param name="desc">默认降序</param>
        /// <returns></returns>
        List<T> GetList<T>(Expression<Func<T, bool>> where, bool flag, Expression<Func<T, object>> order, bool desc = true) where T : class, new();

        /// <summary>
        /// 返回查询条件的匹配操作记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="flag">order是否起作用</param>
        /// <param name="order">排序条件</param>
        /// <returns></returns>
        List<T> GetList<T>(string where, bool flag, Expression<Func<T, object>> order, bool desc = true, object param = null) where T : class, new();

        /// <summary>
        /// 返回查询条件的匹配操作记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="flag">order是否起作用</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageSize">取几条数据</param>
        /// <returns></returns>
        List<T> GetList<T>(string where, bool flag, Expression<Func<T, object>> order, bool desc = true, object param = null, int pageSize = 10) where T : class, new();

        /// <summary>
        /// 返回查询条件的匹配操作记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="flag">order是否起作用</param>
        /// <param name="order">排序条件</param>
        /// <returns></returns>
        List<T> GetList<T>(string where, object param = null) where T : class, new();

        /// <summary>
        /// 返回查询条件的匹配操作记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="flag">order是否起作用</param>
        /// <param name="order">排序条件</param>
        /// <returns></returns>
        Task<List<T>> GetListAsync<T>(string where, object param = null) where T : class, new();

        /// <summary>
        /// 返回所有记录，请慎用，除非你有缓存或者数据量比较小
        /// </summary>
        /// <returns></returns>
        List<T> GetAllList<T>() where T : class, new();

        /// <summary>
        /// 返回所有记录，请慎用，除非你有缓存或者数据量比较小
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllListAsync<T>() where T : class, new();

        /// <summary>
        /// 返回条件记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        T Get<T>(Expression<Func<T, bool>> where) where T : class, new();

        /// <summary>
        /// 根据查询条件获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        T Get<T>(string where, object param = null) where T : class, new();


        /// <summary>
        /// 根据查询条件获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string where, object param = null) where T : class, new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        object GetPageList<T>(Expression<Func<T, bool>> where, int pageIndex, int pageSize, Expression<Func<T, object>> order, bool desc = true) where T : class, new();
        /// <summary>
        /// 获取分页结果
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="order">排序方式</param>
        /// <param name="param">sql语句中的赋值</param>
        /// <param name="desc"></param>
        /// <returns></returns>
        object GetPageList<T>(string where, int pageIndex, int pageSize, Expression<Func<T, object>> order, bool desc = true, object param = null) where T : class, new();
        /// <summary>
        /// 获取分页结果
        /// </summary>
        /// <param name="sql">纯sql语句</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="order">排序方式</param>
        /// <param name="desc"></param>
        /// <returns></returns>
        object GetPageList<T>(string where, int pageIndex, int pageSize, Expression<Func<T, object>> order, bool desc = true) where T : class, new();

        /// <summary>
        /// 根据条件查询记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        long GetCount<T>(Expression<Func<T, bool>> where) where T : class, new();

        Task<long> GetCountAsync<T>(string where, object param) where T : class, new();
        /// <summary>
        /// 根据查询条件获取记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        long GetCount<T>(string where, object param = null) where T : class, new();

    }
}
