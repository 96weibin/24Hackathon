using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AIQuestionAnswer.DAL
{
    public class DbContextFactory: IDisposable
    {        


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
