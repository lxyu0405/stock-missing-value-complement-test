using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Repository.Entity;


namespace StockMissingValueComplement.Stock.Complement.Repository.Interface
{
    public interface ICurveDataDao
    {
        //更新股票信息
        void Update(CurveDataEntity curveData);

        //获取股票信息
        List<CurveDataEntity> GetCurveDataByCurveId(long curveId);
    }
}
