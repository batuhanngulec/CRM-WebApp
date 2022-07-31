using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmosCrmWebApp.Dapper
{
    public class DapperTools : IDapperTools
    {
        #region private variables

        private readonly IDapperContext _context;

        #endregion private variables

        #region constructor

        public DapperTools(string connectionString)
        {
            _context = new DapperContext(connectionString);
        }

        public DapperTools(IDapperContext context)
        {
            _context = context;
        }

        #endregion constructor

        #region generic repo

        public T Get<T>(object id) where T : class
        {
            return _context.Connection.Get<T>(id);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Connection.GetAll<T>();
        }

        public long Insert<T>(T obj) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Insert(obj, transaction, 0);
                return result;
            });
        }

        public long Insert<T>(IEnumerable<T> list) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Insert(list, transaction);
                return result;
            });
        }

        public bool Update<T>(T obj) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Update(obj, transaction);
                return result;
            });
        }

        public bool Update<T>(IEnumerable<T> list) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Update(list, transaction);
                return result;
            });
        }

        public bool Delete<T>(T obj) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Delete(obj, transaction);
                return result;
            });
        }

        public bool Delete<T>(IEnumerable<T> list) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Delete(list, transaction);
                return result;
            });
        }

        public bool DeleteAll<T>() where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.DeleteAll<T>();
                return result;
            });
        }

        #endregion generic repo

        #region db commands

        public int Execute(string sql, object param = null)
        {
            return _context.Transaction(transaction => _context.Connection.Execute(sql, param, transaction));
        }

        public IEnumerable<T> Query<T>(string sql, object param = null) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query<T>(sql, param, transaction);
                return result;
            });
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id") where TFirst : class where TSecond : class where TReturn : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query(sql, map, param, transaction, true, splitOn);
                return result;
            });
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TReturn : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query(sql, map, param, transaction);
                return result;
            });
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TReturn : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query(sql, map, param, transaction);
                return result;
            });
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TReturn : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query(sql, map, param, transaction);
                return result;
            });
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TReturn : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query(sql, map, param, transaction);
                return result;
            });
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class where TReturn : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query(sql, map, param, transaction);
                return result;
            });
        }


        public IEnumerable<object> Query(string sql, object param = null)
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query<object>(sql, param, transaction);
                return result;
            });
        }

        public SqlMapper.GridReader QueryMultiple(string sql, object param = null)
        {
            if (_context.IsConnection())
                _context.Connection.Close();

            return _context.Connection.QueryMultiple(sql, param);
        }

        public IEnumerable<T> QueryProc<T>(string sql, object param = null)
        {
            return _context.Transaction(transaction =>
                {
                    var result = _context.Connection.Query<T>(sql, param, transaction, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                    return result;
                });

        }

        public IEnumerable<TReturn> QueryProc<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null)
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query(sql, map, param, transaction, commandTimeout: null, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            });
        }

        public List<T> QueryFnc<T>(string fncName, object param = null)
        {
            var propInfo = param.GetType().GetProperties();
            List<string> parametreler = new List<string>();

            foreach (var item in propInfo)
            {
                string parametre;

                if (item.PropertyType == typeof(DateTime))
                    parametre = Convert.ToDateTime(item.GetValue(param, null)).ToString("yyyy-MM-dd");
                else
                    parametre = Convert.ToString(item.GetValue(param, null));

                parametreler.Add(parametre);
            }

            string sql = "Select * From " + fncName + "('" + string.Join("', '", parametreler) + "')";

            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query<T>(sql, param, transaction, commandTimeout: null);
                return result;
            }).ToList();
        }

        public bool CmdBool(string sql, object param = null)
        {
            try
            {
                _context.Transaction(transaction =>
                {
                    var result = _context.Connection.Query(sql, param, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    return result;
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool QueryBool(string sql, object param = null)
        {
            try
            {
                _context.Transaction(transaction =>
                {
                    var result = _context.Connection.Query(sql, param, transaction, commandType: System.Data.CommandType.Text);
                    return result;
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsConnection()
        {
            return _context.IsConnection();
        }

        public object ExecuteScalar(string sql, object param = null)
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.ExecuteScalar(sql, param, transaction);
                return result;
            });
        }

        public object ExecuteScalarNonTransaction(string sql, object param = null)
        {
            try
            {
                return _context.Connection.ExecuteScalar(sql, param);
            }
            finally
            {
                _context.Connection.Close();
            }
        }

        #endregion db commands
    }
}
