using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace 批量导入程序重制版
{
    public class SqlSugarRepository<T> where T : class, new()
    {

        private readonly ISqlSugarClient db;

        public SqlSugarRepository(ISqlSugarClient db)
        {
            this.db = db;
        }

        #region 数据新增操作

        /// <summary>
        ///  增加单条数据，异步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(T model)
        {
            var result = await db.Insertable(model).ExecuteCommandAsync();
            return result > 0;
        }

        /// <summary>
        /// 添加单条数据，并返回自增列，异步
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public async Task<int> AddReturnIdentityAsync(T model)
        {
            return await db.Insertable(model).ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 增加单条数据 ，并返回实体,异步
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public async Task<T> AddReturnEntityAsync(T model)
        {
            return await db.Insertable(model).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 增加多条数据，异步
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>操作是否成功</returns>
        public async Task<bool> AddRangeAsync(List<T> list)
        {
            if (!list.Any())
            {
                return true;
            }
            var result = await db.Insertable(list).ExecuteCommandAsync();
            return result > 0;
        }

        /// <summary>
        /// 增加多条数据返回自增列，异步
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>操作是否成功</returns>
        public async Task<List<int>> AddRangeIdentityAsync(List<T> list)
        {
            if (!list.Any())
            {
                return new List<int>();
            }
            return await db.Insertable(list).ExecuteReturnPkListAsync<int>();
        }

        #endregion

        #region 数据删除操作

        /// <summary>
        /// 根据条件删除，并返回操作是否成功，异步
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<bool> DeleteWhereAsync(Expression<Func<T, bool>> where)
        {
            return await db.Deleteable<T>().Where(where).ExecuteCommandAsync() >= 0;
        }

        #endregion

        #region 数据修改操作

        /// <summary>
        /// 根据主键更新，返回操作是否成功，异步
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T model)
        {
            return await db.Updateable(model).ExecuteCommandAsync() >= 0;
        }

        /// <summary>
        /// 根据指定条件更新指定列，并返回操作是否成功，异步
        /// </summary>
        /// <param name="columns">要更新列</param>
        /// <param name="expression">列条件</param>
        /// <returns></returns>
        public async Task<bool> UpdateWhereAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> expression)
        {
            return await db.Updateable<T>().SetColumns(columns).Where(expression).ExecuteCommandAsync() >= 0;
        }
        /// <summary>
        /// 根据指定条件更新数据，不更新空值，异步
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="expression">列条件</param>
        /// <returns></returns>
        public async Task<bool> UpdateWhereAsync(T model, Expression<Func<T, bool>> expression)
        {
            return await db.Updateable(model).IgnoreColumns(ignoreAllNullColumns: true).Where(expression).ExecuteCommandAsync() >= 0;
        }

        /// <summary>
        /// 根据指定列作为条件更新数据，不更新某列，异步
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="expression">条件列</param>
        /// <param name="columns">不更新列</param>
        /// <returns></returns>
        public async Task<bool> UpdateWhereAsync(T model, Expression<Func<T, object>> expression, Expression<Func<T, object>> columns)
        {
            return await db.Updateable(model).WhereColumns(expression).IgnoreColumns(columns).ExecuteCommandAsync() >= 0;
        }

        /// <summary>
        /// 根据指定列作为条件更新新数据，只更新某列，异步
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <param name="where">条件列</param>
        /// <param name="columns">更新列</param>
        /// <returns></returns>
        public async Task<bool> UpdateWhereAsync(List<T> list, Expression<Func<T, object>> where, Expression<Func<T, object>> columns)
        {
            if (!list.Any())
            {
                return true;
            }
            return await db.Updateable(list).WhereColumns(where).UpdateColumns(columns).ExecuteCommandAsync() >= 0;
        }

        #endregion

        #region 数据库事务,存储

        /// <summary>
        /// 使用事务 ，无返回值异步
        /// </summary>
        /// <param name="action">执行动作</param>
        /// <param name="errorCallBack">错误回调</param>
        public Task<DbResult<bool>> UseTranAsync(Func<Task> action, Action<Exception> errorCallBack)
        {
            return db.Ado.UseTranAsync(action, errorCallBack);
        }

        #endregion

    }
}
