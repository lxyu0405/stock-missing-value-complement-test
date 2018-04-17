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
    public class CurveDaoSqlServerImpl : ICurveDao
    {
        //更新股票信息
        public void Update(CurveEntity curveData)
        {

        }

        //获取全部股票信息
        public List<CurveEntity> GetAllCurves()
        {
            List<CurveEntity> results = new List<CurveEntity>();

            return results;
        }

        public CurveEntity GetCurveByName(string curveName)
        {
            return null;
        }
    }
}
