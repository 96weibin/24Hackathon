using knowledgeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace knowledgeBase.DAL
{
    public class DbContextFactory: IDisposable
    {        
        //public static Model1Container GetDbContext()
        //{
        //    //在HttpContext中有一个Items属性，它也可以用来保存key-value，一次请求正好对应着一个HttpContext，请求结束，它自动释放，EF上下文也就不存在了。
        //    Model1Container dbContext = HttpContext.Current.Items["dbContext"] as Model1Container;
        //    if (dbContext == null)
        //    {
        //        dbContext = new Model1Container();
        //        HttpContext.Current.Items["dbContext"] = dbContext;
        //    }
        //    return dbContext;
        //}

        //public static int SaveChanges()
        //{
        //    return DbContextFactory.GetDbContext().SaveChanges();
        //}


        private static SqlConnection _sqlConnection;
        private bool disposedValue;

        public DbContextFactory()
        {

        }

        public static SqlConnection GetOpenSqlConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AupResultsDB"].ConnectionString);
            }
            if (_sqlConnection.State != System.Data.ConnectionState.Open)
            {
                _sqlConnection.Open();
            }
            return _sqlConnection;
        }
        public static Model1Container GetDbContext()
        {
            //在HttpContext中有一个Items属性，它也可以用来保存key-value，一次请求正好对应着一个HttpContext，请求结束，它自动释放，EF上下文也就不存在了。
            Model1Container dbContext = HttpContext.Current.Items["dbContext"] as Model1Container;
            if (dbContext == null)
            {
                dbContext = new Model1Container();
                HttpContext.Current.Items["dbContext"] = dbContext;
            }
            return dbContext;
        }

        public static int SaveChanges()
        {
            return DbContextFactory.GetDbContext().SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_sqlConnection != null && _sqlConnection.State != System.Data.ConnectionState.Closed)
                    {
                        _sqlConnection.Close();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DbContextFactory()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
