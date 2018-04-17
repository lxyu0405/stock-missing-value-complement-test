using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Repository.Entity;

namespace StockMissingValueComplement.Stock.Complement.Repository.Interface
{
    public interface ICurveDao
    {
        //更新股票信息
        void Update(CurveEntity curveData);

        //获取全部股票信息
        List<CurveEntity> GetAllCurves();

        CurveEntity GetCurveByName(string curveName);
    }
}
