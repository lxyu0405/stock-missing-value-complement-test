using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Repository.Interface;
using StockMissingValueComplement.Stock.Complement.Repository.Implement;

namespace StockMissingValueComplement.Stock.Complement.Repository
{
    public class RepositoryFactory
    {
        public static ICurveDao GetCurveRepository(string dbType)
        {
            switch(dbType.ToLower())
            {
                case "mysql":
                    return new CurveDaoMySQLImpl();
                case "sqlserver":
                    return new CurveDaoSqlServerImpl();
                default:
                    return new CurveDaoMySQLImpl();
            }
        }
        

        public static ICurveDataDao GetCurveDataRepository(string dbType)
        {
            switch (dbType.ToLower())
            {
                case "mysql":
                    return new CurveDataDaoMySQLImpl();
                case "sqlserver":
                    return new CurveDataDaoSqlServerImpl();
                default:
                    return new CurveDataDaoMySQLImpl();
            }
        }
    }
}
