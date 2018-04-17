using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Business.Interface;
using StockMissingValueComplement.Stock.Complement.Business.Implement;

namespace StockMissingValueComplement.Stock.Complement.Business.Factory
{
    public class StrategyFactory
    {
        public static IFitStrategy GetStrategyByName(string normal)
        {
            switch(normal.ToLower())
            {
                case "simplelinear":
                    return new SimpleLinearFitStrategy();
                case "regressionlinear":
                    return new LinearRegFitStrategy();
                default:
                    return new SimpleLinearFitStrategy();
            }
        }
    }
}
