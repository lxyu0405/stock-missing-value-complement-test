using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Business.DTO;

namespace StockMissingValueComplement.Stock.Complement.Business.Interface
{
    public interface IFitStrategy
    {
        //拟合计算
        PointDto FitCalculation(List<PointDto> points, int targetX);

    }
}
