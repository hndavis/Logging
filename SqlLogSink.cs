using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
   
    public class SqlLogSink
        : LogSinkBase, IDisposable
    {



        private string _name;
        private bool _disposed = false;
        public string SqlConnectionString { get; set; }
      
        private SqlConnection _conn;
       readonly private ILogger _logger;

        private string _insertIntoLog = "insert into UserLog ( DrId, PatId, [StudyId],[SeriesUid], EventName, TStampOffSet )" +
                                        "Values ({0},'{1}','{2}','{3}','{4}','{5}')";

        public SqlLogSink(string name, ILogger logger = null)
        {
            _name = name;
            
            _logger = logger;   //todo idea is to write to the file logger if sql logger fails -- needs more work

        }

        public override bool IsOpen
        {
            get
            {

                if (_conn == null)
                    return false;
             
                return _conn.State == ConnectionState.Open;

            }
        }


        public override void Open()
        {
            if (string.IsNullOrEmpty(SqlConnectionString))
                return;
    
           
            try
            {
                _conn = new SqlConnection(SqlConnectionString);
                _conn.Open();
            }
            catch (SqlException exSql)
            {
                // for debug
                var m = exSql.Message;
                _logger.Error(m);
                throw;
            }
            catch (Exception exFile)
            {
                // for debug
                var m = exFile.Message;
                _logger.Error(m);
                throw;
            }
        }



        public void Write(int drId, string patId, string index, TimeSpan ts, string methodName )
        {
            string insertWithData = String.Format(_insertIntoLog,
                drId, patId,index, methodName, ts);
            try
            {
                SqlCommand cmd = new SqlCommand(insertWithData, _conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 1)
                    throw new Exception("Rows not inserted correctly");
             
            }
            catch (SqlException exSql)
            {
                // for debug
                var m = exSql.Message;
                _logger.Error(m);
                throw;
            }
            catch (Exception exFile)
            {
                // for debug
                var m = exFile.Message;
                _logger.Error(m);
                throw;
            }
                     
        }

        public override void Write( object[] l)
        {
            if (!IsOpen)
            {
                Open();
                if (!IsOpen)
                    return;         // no sql available
            }
            string insertWithData="";
            try
            {
                if (l.Length == 6)
                {
                     insertWithData = String.Format(_insertIntoLog, (int) l[0], (string) l[1],
                        (string) l[2], (string) l[3], (string) l[5], (TimeSpan) l[4]);
                    SqlCommand cmd = new SqlCommand(insertWithData, _conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected != 1)
                        throw new Exception("Rows not inserted correctly");
                }
                else if (l.Length == 7)
                {
                    insertWithData = String.Format(_insertIntoLog, (int)l[0], (string)l[1],
                       (string)l[2], (string)l[5], (TimeSpan)l[3]);
                    SqlCommand cmd = new SqlCommand(insertWithData, _conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected != 1)
                        throw new Exception("Rows not inserted correctly");
                }
                else
                {
                    throw new Exception("Not Implementated");
                }
            }
            catch (SqlException exSql)
            {
                // for debug
                var m = exSql.Message + Environment.NewLine+insertWithData;
                _logger.Error(m);
                throw;
            }
            catch (Exception exFile)
            {
                // for debug
                var m = exFile.Message + Environment.NewLine + insertWithData;
                _logger.Error(m);
                throw;
            }

        }

        public override void Flush()
        {
               

        }



        public override  void Dispose()
        {
            Close();
        }

        public void Close()
        {
            Dispose(true);
          
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_conn != null)
                    {
                        if (_conn.State == ConnectionState.Open || _conn.State == ConnectionState.Connecting ||
                            _conn.State == ConnectionState.Executing || _conn.State == ConnectionState.Fetching)
                            _conn.Close();
                        _conn = null;
                    }
                    _disposed = true;
                }
            }


        }

        ~SqlLogSink()
        {
            Dispose(false);
        }

    }


}
