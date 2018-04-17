using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockMissingValueComplement.Stock.Complement.Tools;
using StockMissingValueComplement.Stock.Complement.Business.Interface;
using StockMissingValueComplement.Stock.Complement.Business.DTO;

namespace StockMissingValueComplement.Stock.Complement.Business.Implement
{
    public class SimpleLinearFitStrategy : IFitStrategy
    {
        //简单线性计算
        public PointDto FitCalculation(List<PointDto> points, int targetX)
        {
            if(targetX <= 0 || ListHelper.IsNullOrEmpty(points) || points.Count <= targetX)
            {
                return null;
            }
            LogHelper.Info(typeof(SimpleLinearFitStrategy), " points: " + JsonConvert.SerializeObject(points) + " targetX: " + targetX);
            PointDto fitPoint = new PointDto(targetX, 0.0);
            //得到前一个点和后一个点
            PointDto prePoint = points[targetX - 1];
            PointDto aftPoint = points[targetX + 1];
            if (prePoint.Y == 0.0 || aftPoint.Y == 0.0 || prePoint.X == aftPoint.X)
            {
                LogHelper.Error(typeof(SimpleLinearFitStrategy), "Data Error. Null Value Found");
                return null;
            }

            double slope = (aftPoint.Y - prePoint.Y) / (aftPoint.X - prePoint.X);
            fitPoint.Y = prePoint.Y + slope * (targetX - prePoint.X);
            LogHelper.Info(typeof(SimpleLinearFitStrategy), "SimpleLinearFitStrategy Result: " + JsonConvert.SerializeObject(fitPoint));
            return fitPoint;
        }
    }
}
