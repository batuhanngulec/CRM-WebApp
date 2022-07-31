using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmosCrmWebApp.Dapper
{
    public interface IDapperTools
    {
        #region generic repo

        T Get<T>(object id) where T : class;

        IEnumerable<T> GetAll<T>() where T : class;

        long Insert<T>(T obj) where T : class;

        long Insert<T>(IEnumerable<T> list) where T : class;

        bool Update<T>(T obj) where T : class;

        bool Update<T>(IEnumerable<T> list) where T : class;

        bool Delete<T>(T obj) where T : class;

        bool Delete<T>(IEnumerable<T> list) where T : class;

        bool DeleteAll<T>() where T : class;

        #endregion generic repo

        #region db commands

        int Execute(string sql, object param = null);

        IEnumerable<T> Query<T>(string sql, object param = null) where T : class;

        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map,
            object param = null, string splitOn = "Id")
            where TFirst : class
            where TSecond : class
            where TReturn : class;
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TReturn> map, object param = null)
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TReturn : class;

        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null)
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TFourth : class
            where TReturn : class;

        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null)
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TFourth : class
            where TFifth : class
            where TReturn : class;

        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql,
           Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null)
           where TFirst : class
           where TSecond : class
           where TThird : class
           where TFourth : class
           where TFifth : class
           where TSixth : class
           where TReturn : class;


        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql,
           Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null)
           where TFirst : class
           where TSecond : class
           where TThird : class
           where TFourth : class
           where TFifth : class
           where TSixth : class
           where TSeventh : class
           where TReturn : class;




        object ExecuteScalar(string sql, object param = null);
        #endregion db commands
    }
}
