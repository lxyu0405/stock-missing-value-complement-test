using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Repository.Interface;
using StockMissingValueComplement.Stock.Complement.Repository.Entity;
using StockMissingValueComplement.Stock.Complement.Tools;


namespace StockMissingValueComplement.Stock.Complement.Repository.Implement
{
    public class CurveDataDaoSqlServerImpl : ICurveDataDao
    {
        public void Update(CurveDataEntity curveData)
        {
            
        }

        public List<CurveDataEntity> GetCurveDataByCurveId(long curveId)
        {
            List<CurveDataEntity> results = new List<CurveDataEntity>();
            
            return results;
        }

    }
}
