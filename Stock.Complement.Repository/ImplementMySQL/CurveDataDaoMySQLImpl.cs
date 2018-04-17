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
    public class CurveDataDaoMySQLImpl : ICurveDataDao
    {
        private MySqlConnection connection;

        public CurveDataDaoMySQLImpl()
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


        public void Update(CurveDataEntity curveData)
        {
            if(curveData == null)
            {
                LogHelper.Warn(typeof(CurveDaoMySQLImpl), "Empty update operation");
            }
            StringBuilder buildSQL = new StringBuilder(200);
            buildSQL.Append(" UPDATE curve_data ");
            buildSQL.AppendFormat(" SET CurveId={0} ", curveData.CurveId);
            buildSQL.AppendFormat(", EffectiveDate='{0}' ", curveData.EffectiveDate);
            buildSQL.AppendFormat(", Value={0} ", curveData.Value);
            buildSQL.AppendFormat(" WHERE CurveDataId = {0} ", curveData.CurveDataId);

            // open connection
            if(this.OpenConnection() == true)
            {
                MySqlCommand mysqlCmd = new MySqlCommand(buildSQL.ToString(), connection);
                mysqlCmd.ExecuteNonQuery();

                // close connection
                this.CloseConnection();
            }
        }

        public List<CurveDataEntity> GetCurveDataByCurveId(long curveId)
        {
            List<CurveDataEntity> results = new List<CurveDataEntity>();
            if (curveId <= 0)
            {
                return results;
            }
            StringBuilder buildSQL = new StringBuilder(200);
            buildSQL.Append(" SELECT CurveDataId, CurveId, EffectiveDate, Value, DataChange_CreateTime, DataChange_LastTime FROM curve_data WHERE 1=1 ");
            buildSQL.AppendFormat(" AND CurveId = {0} ", curveId);
            buildSQL.Append(" Order By CurveDataId ");
            // open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand mysqlCmd = new MySqlCommand(buildSQL.ToString(), connection);
                MySqlDataReader dataReader = mysqlCmd.ExecuteReader();

                while(dataReader.Read())
                {
                    CurveDataEntity curveItem = new CurveDataEntity() 
                    { 
                        CurveDataId = dataReader.GetInt64(0),
                        CurveId = dataReader.GetInt64(1),
                        EffectiveDate = dataReader.GetString(2),
                        Value = dataReader.GetDecimal(3),
                        DataChange_CreateTime = dataReader.GetDateTime(4),
                        DataChange_LastTime = dataReader.GetDateTime(5)
                    };
                    results.Add(curveItem);
                }
                // close connection
                this.CloseConnection();
            }
            return results;
        }

    }
}
