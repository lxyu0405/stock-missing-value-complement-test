using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMissingValueComplement.Stock.Complement.Business.Interface
{
    interface IValueComplementBusiness
    {
        //开始拟合
        void DoFitCalculation(string stockName, string fitMethod);

        //获取现有股票名称
        string GetStoredStockNames();

    }
}
