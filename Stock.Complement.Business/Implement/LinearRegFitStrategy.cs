using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using StockMissingValueComplement.Stock.Complement.Business.Interface;
using StockMissingValueComplement.Stock.Complement.Business.DTO;
using StockMissingValueComplement.Stock.Complement.Tools;



namespace StockMissingValueComplement.Stock.Complement.Business.Implement
{
    public class LinearRegFitStrategy : IFitStrategy
    {
        //最小二乘法进行线性回归
        public PointDto FitCalculation(List<PointDto> points, int targetX)
        {
            if (points.Count < 2)
            {
                LogHelper.Warn(typeof(LinearRegFitStrategy), "Not Enough Point to Perform Linear Regression!");
                return null;
            }
            PointDto fitPoint = new PointDto(targetX, 0.0);

            //求出横纵坐标的平均值
            double averagex = 0, averagey = 0;
            foreach(PointDto p in points)
            {
                averagex += p.X;
                averagey += p.Y;
            }

            //点List包含将要补全的点
            averagex /= points.Count - 1;
            averagey /= points.Count - 1;

            //经验回归系数的分子与分母
            double numerator = 0;
            double denominator = 0;

            foreach (PointDto p in points)
            {
                numerator += (p.X - averagex) * (p.Y - averagey);
                denominator += (p.X - averagex) * (p.X - averagex);
            }

            //回归系数b（Regression Coefficient）
            double RCB = numerator / denominator;

            //回归系数a
            double RCA = averagey - RCB * averagex;

            LogHelper.Info(typeof(LinearRegFitStrategy), "回归系数A： " + RCA.ToString("0.0000"));
            LogHelper.Info(typeof(LinearRegFitStrategy), "回归系数B： " + RCB.ToString("0.0000"));
            LogHelper.Info(typeof(LinearRegFitStrategy), string.Format("方程为： y = {0} + {1} * x", RCA.ToString("0.0000"), RCB.ToString("0.0000")));

            fitPoint.Y = RCB * targetX + RCA;
            LogHelper.Info(typeof(SimpleLinearFitStrategy), "LinearRegFitStrategy Result: " + JsonConvert.SerializeObject(fitPoint));

            return fitPoint;
        }
    }
}
