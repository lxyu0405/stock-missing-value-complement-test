using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Repository.Interface;
using StockMissingValueComplement.Stock.Complement.Repository.Entity;
using StockMissingValueComplement.Stock.Complement.Tools;


namespace StockMissingValueComplement.Stock.Complement.Repository.Implement
{
    public class CurveDaoMySQLImpl : ICurveDao
    {
        private MySqlConnection connection; 

        public CurveDaoMySQLImpl()
        {
            Initialize();
        }

        private void Initialize()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MySQL_StocksDB"].ConnectionString;
            connection = new MySqlConnection(connStr);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                LogHelper.Error(typeof(CurveDaoMySQLImpl), ex);
            }
            return false;
        }

        //close connection
        private bool CloseConnection()
        {
            try 
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                LogHelper.Error(typeof(CurveDaoMySQLImpl), ex);
            }
            return false;
        }


        public void Update(CurveEntity curveData)
        {
            if(curveData == null)
            {
                LogHelper.Warn(typeof(CurveDaoMySQLImpl), "Empty update operation");
            }
            StringBuilder buildSQL = new StringBuilder(200);
            buildSQL.Append(" UPDATE curve ");
            buildSQL.AppendFormat(" SET CurveName='{0}' ", curveData.CurveName);
            buildSQL.AppendFormat(" WHERE CurveId = {0} ", curveData.CurveId);

            // open connection
            if(this.OpenConnection() == true)
            {
                MySqlCommand mysqlCmd = new MySqlCommand()
                {
                    CommandText = buildSQL.ToString(),
                    Connection = connection
                };
                mysqlCmd.ExecuteNonQuery();

                // close connection
                this.CloseConnection();
            }
        }

        public List<CurveEntity> GetAllCurves()
        {
            List<CurveEntity> results = new List<CurveEntity>();
            string selectAllQuery = " SELECT CurveId, CurveName, DataChange_CreateTime, DataChange_LastTime FROM curve ";
            // open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand mysqlCmd = new MySqlCommand(selectAllQuery, connection);
                MySqlDataReader dataReader = mysqlCmd.ExecuteReader();

                while(dataReader.Read())
                {
                    CurveEntity curveItem = new CurveEntity() 
                    { 
                        CurveId = dataReader.GetInt64(0),
                        CurveName = dataReader.GetString(1),
                        DataChange_CreateTime = dataReader.GetDateTime(2),
                        DataChange_LastTime = dataReader.GetDateTime(3)
                    };
                    results.Add(curveItem);
                }
                // close connection
                this.CloseConnection();
            }
            return results;
        }

        public CurveEntity GetCurveByName(string curveName)
        {
            CurveEntity result = null;
            if(String.IsNullOrWhiteSpace(curveName))
            {
                return result;
            }
            StringBuilder buildSQL = new StringBuilder(200);
            buildSQL.Append(" SELECT CurveId, CurveName, DataChange_CreateTime, DataChange_LastTime FROM curve WHERE 1=1 ");
            buildSQL.AppendFormat(" AND CurveName = '{0}' ", curveName);
            // open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand mysqlCmd = new MySqlCommand(buildSQL.ToString(), connection);
                MySqlDataReader dataReader = mysqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    result = new CurveEntity()
                    {
                        CurveId = dataReader.GetInt64(0),
                        CurveName = dataReader.GetString(1),
                        DataChange_CreateTime = dataReader.GetDateTime(2),
                        DataChange_LastTime = dataReader.GetDateTime(3)
                    };
                }
                // close connection
                this.CloseConnection();
            }
            return result;
        }

        //Count statement
        private int Count()
        {
            string query = "SELECT Count(*) FROM curve";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            return Count;
        }

    }
}
